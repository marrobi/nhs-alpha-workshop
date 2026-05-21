---
name: dotnet-aspnet-azure
description: 'Use when scaffolding a UKHSA .NET 10 / ASP.NET Core service with GOV.UK Design System on Azure UK South.'
---

# .NET 10 / ASP.NET Core / Azure — UKHSA Scaffold

This skill scaffolds a UKHSA digital service using the standard tech stack: .NET 10 LTS, ASP.NET Core MVC + Razor Pages + minimal APIs, Entity Framework Core 10, GOV.UK Design System via `GovUk.Frontend.AspNetCore`, and Azure UK South.

## When to Use

- Creating a new UKHSA service from scratch
- Adding a frontend MVC area to an existing API
- Setting up the standard test/CI/IaC scaffolding expected by UKHSA Engineering Standards

## Tech Stack (Pinned)

| Layer | Technology | Version |
|---|---|---|
| Runtime | .NET | 10 LTS |
| Web framework | ASP.NET Core | 10 |
| ORM | Entity Framework Core | 10 |
| Frontend tag helpers | GovUk.Frontend.AspNetCore | latest stable |
| Validation | FluentValidation | latest stable |
| Resilience | Microsoft.Extensions.Http.Resilience (Polly) | latest stable |
| Unit test | xUnit + FluentAssertions + Moq | latest stable |
| Integration test | `WebApplicationFactory<TEntryPoint>` | from `Microsoft.AspNetCore.Mvc.Testing` |
| E2E test | Microsoft.Playwright + Deque.AxeCore.Playwright | latest stable |
| Load test | k6 | latest stable |
| IaC | Terraform with `azurerm` provider | `>= 1.7` / provider `>= 3.100` |
| Cloud | Azure | UK South primary, UK West DR |

## Project Structure

```
.
├── src/
│   ├── Web/                       # ASP.NET Core MVC + Razor Pages host (frontend)
│   │   ├── Pages/                 # Razor Pages
│   │   ├── Controllers/           # MVC controllers (if used)
│   │   ├── ViewComponents/
│   │   ├── wwwroot/
│   │   └── Program.cs
│   ├── Api/                       # Minimal API host (if separate)
│   │   └── Program.cs
│   ├── Application/               # Use cases, services, FluentValidation validators
│   ├── Domain/                    # Entities, value objects
│   └── Infrastructure/            # EF Core DbContext, external clients
├── tests/
│   ├── Unit/                      # xUnit unit tests
│   ├── Integration/               # WebApplicationFactory tests
│   └── e2e/                       # Playwright for .NET
├── infra/                         # Terraform (see azure-ukhsa-deploy skill)
├── docs/
│   ├── adr/
│   ├── dpia/
│   ├── safety/
│   └── gds-assessment.md
└── .github/workflows/             # GitHub Actions
```

## Scaffold Steps

1. **Solution**
   ```bash
   dotnet new sln -n MyService
   dotnet new web -o src/Web
   dotnet new classlib -o src/Application
   dotnet new classlib -o src/Domain
   dotnet new classlib -o src/Infrastructure
   dotnet new xunit -o tests/Unit
   dotnet new xunit -o tests/Integration
   dotnet sln add src/**/*.csproj tests/**/*.csproj
   ```

2. **Core packages**
   ```bash
   dotnet add src/Web package GovUk.Frontend.AspNetCore
   dotnet add src/Web package Microsoft.ApplicationInsights.AspNetCore
   dotnet add src/Web package Microsoft.Extensions.Http.Resilience
   dotnet add src/Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add src/Infrastructure package Microsoft.EntityFrameworkCore.Design
   dotnet add src/Application package FluentValidation.AspNetCore
   dotnet add tests/Integration package Microsoft.AspNetCore.Mvc.Testing
   dotnet add tests/Unit package FluentAssertions Moq
   ```

3. **GOV.UK Design System**
   Register `GovUk.Frontend.AspNetCore` in `Program.cs`:
   ```csharp
   builder.Services.AddGovUkFrontend();
   ```
   Use tag helpers in Razor: `<govuk-header>`, `<govuk-footer>`, `<govuk-back-link>`, `<govuk-error-summary>`, `<govuk-panel>`.

4. **EF Core**
   ```csharp
   builder.Services.AddDbContext<AppDbContext>(opt =>
       opt.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));
   ```
   Use migrations: `dotnet ef migrations add Init -p src/Infrastructure -s src/Web`.

5. **Health check**
   ```csharp
   builder.Services.AddHealthChecks().AddDbContextCheck<AppDbContext>();
   app.MapHealthChecks("/health");
   ```

6. **Observability**
   ```csharp
   builder.Services.AddApplicationInsightsTelemetry();
   builder.Logging.AddApplicationInsights();
   ```

7. **Security headers**
   ```csharp
   app.UseHsts();
   app.UseHttpsRedirection();
   ```

## GOV.UK / UKHSA Branding

- Use the GOV.UK Design System as the baseline.
- Override SCSS variables for UKHSA brand colours where required — never inline-style.
- Service name renders in `<govuk-header>` `service-name`.
- Skip link target `#main-content`.
- Date format `6 January 2026` (long form, no leading zero, no comma).
- NHS Number displayed as `943 476 5919` (3-3-4 with spaces).

## Configuration

- Settings live in `appsettings.json` + environment-specific overrides (`appsettings.Production.json`).
- Secrets resolved from Key Vault via App Service Key Vault references.
- Never read `Configuration["X"] ?? "default"` for security-sensitive settings — fail fast if missing.

## Run Locally

```bash
dotnet run --project src/Web
dotnet test
```

## Deploy

See `azure-ukhsa-deploy` skill for full Terraform + GitHub Actions OIDC setup. Summary:

```bash
dotnet publish src/Web -c Release -o ./publish
az webapp deploy \
  --resource-group rg-${WORKLOAD}-${ENV}-uks-001 \
  --name app-${WORKLOAD}-${ENV}-uks-001 \
  --src-path ./publish.zip --type zip
```

## Rules

- One service per repo. Monorepo only if multiple services share a single deployment unit.
- All new endpoints return RFC 9457 problem details on error.
- `[Authorize(Policy = "...")]` on every non-public endpoint — no implicit anonymous access.
- Anti-forgery enabled globally for MVC: `services.AddControllersWithViews(o => o.Filters.Add<AutoValidateAntiforgeryTokenAttribute>())`.
- All EF Core queries use parameters or `FromSqlInterpolated` — never string concatenation.
- No `@Html.Raw(userInput)` in Razor.
- Cookies set `HttpOnly`, `Secure`, `SameSite=Strict`.

## References

- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
- [GOV.UK Design System](https://design-system.service.gov.uk/)
- [GovUk.Frontend.AspNetCore](https://github.com/gunndabad/govuk-frontend-aspnetcore)
- [ASP.NET Core docs](https://learn.microsoft.com/aspnet/core)
