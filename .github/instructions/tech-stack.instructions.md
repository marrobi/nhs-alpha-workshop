---
applyTo: "**"
---

# Tech Stack Profile

This is the **single source of truth** for the current technology choices for UKHSA service delivery. All agents and instructions reference this file rather than hardcoding specific frameworks or platforms.

To change the tech stack, update this file and swap the corresponding tech-specific agents, instructions, and skills.

## Current Stack

| Concern | Choice |
|---|---|
| **Backend** | .NET 10 LTS / ASP.NET Core MVC / Kestrel |
| **Frontend** | ASP.NET Core MVC Razor views (`.cshtml`) + GovUk.Frontend.AspNetCore |
| **Design System** | GOV.UK Design System (`govuk-frontend` CSS via `GovUk.Frontend.AspNetCore`) with UKHSA branding overrides for header/footer |
| **Database** | Azure SQL Database (default) with Entity Framework Core 10; Azure Cosmos DB or Azure Database for PostgreSQL where justified by data model |
| **ORM / Data Access** | Entity Framework Core 10 (code-first migrations); Dapper for read-optimised queries where justified |
| **Backend Testing** | xUnit + FluentAssertions + Moq + `WebApplicationFactory<TEntryPoint>` |
| **Frontend Testing** | xUnit (Razor view tests via `WebApplicationFactory`); browser-side behaviour covered by Playwright |
| **E2E Testing** | Playwright for .NET + axe-core for accessibility checks |
| **Performance Testing** | k6 (JavaScript) |
| **IaC** | Terraform (`azurerm` provider) |
| **Hosting** | Azure App Service for Linux or Azure Container Apps (UK South primary, UK West secondary) |
| **Secrets** | Azure Key Vault via User-Assigned Managed Identity |
| **Monitoring** | Azure Application Insights + Azure Monitor |
| **Logging** | `Microsoft.Extensions.Logging` with NLog provider and Application Insights sink |
| **Resilience** | Polly (retry, circuit breaker, timeout, bulkhead) via `Microsoft.Extensions.Http.Resilience` |
| **Transactional Email** | GOV.UK Notify .NET client |
| **CI/CD** | GitHub Actions with OIDC-based Azure auth (no long-lived service principal secrets) |
| **Linting / Analysis** | `dotnet format`, Roslyn analyzers, `.editorconfig`, ESLint (frontend JS) |

> **Tech Radar note.** UKHSA's published Technology Radar lists .NET in the "Discuss with UKHSA" band. This stack treats .NET as **approved by exception** for services already standardised on .NET (e.g. ImmForm). New services MUST confirm exception status with the UKHSA architecture function before adopting.


## Key Files When Swapping

| Layer | Files to swap |
|---|---|
| Tech agents | `ukhsa-service-builder`, `testing`, `playwright-e2e`, `performance`, `security-reviewer`, `cicd-pipeline-builder`, `accessibility-auditor` |
| Tech instructions | This file (`tech-stack.instructions.md`) — all tech-specific implementation details live here |
| Tech skills | `dotnet-aspnet-azure`, `azure-ukhsa-deploy`, `playwright-dotnet-e2e`, `ukhsa-synthetic-data` (code examples) |
| Domain agents (no change needed) | `ukhsa-architect`, `ukhsa-product-owner`, `day2-issue-generator`, `mhra-gdp-advisor`, `ukhsa-dpia-advisor`, `gds-assessor`, `ukhsa-content-designer` |
| Domain instructions (no change needed) | `ukhsa-api`, `ukhsa-security`, `govuk-frontend`, `testing`, `performance`, `terraform-azure-ukhsa` — these contain tech-agnostic rules only |
| Domain skills (no change needed) | `mhra-gdp-validation`, `ukhsa-dpia`, `ukhsa-user-stories`, `gds-service-standard`, `ukhsa-adr-writer` |

---

## Backend Implementation (ASP.NET Core MVC)

### Project Structure

- Solution: `<ServiceName>.sln`
- Projects:
  - `src/<ServiceName>.Web/` — ASP.NET Core MVC entry point (controllers, Razor views, `Program.cs`)
  - `src/<ServiceName>.Application/` — use cases, application services, DTOs, validation
  - `src/<ServiceName>.Domain/` — domain model, value objects, domain events
  - `src/<ServiceName>.Infrastructure/` — EF Core `DbContext`, repositories, integrations (Key Vault, Notify, external APIs)
  - `tests/<ServiceName>.UnitTests/`, `tests/<ServiceName>.IntegrationTests/`, `tests/<ServiceName>.E2ETests/`
- Nullable reference types MUST be enabled (`<Nullable>enable</Nullable>`)
- Warnings MUST be treated as errors in CI (`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`)

### Controllers & Routing

- Define controllers in `Controllers/` using attribute routing (`[Route("api/[controller]")]` for APIs, `[Route("/journey-step")]` for MVC pages)
- Use `[ApiController]` on JSON controllers — gives automatic 400 responses for invalid `ModelState`
- Use `async` / `Task<IActionResult>` for all I/O-bound handlers
- Bind request bodies to dedicated request models in `src/<ServiceName>.Application/` — never bind directly to EF entities
- Apply FluentValidation or DataAnnotations on all request models
- Apply `AspNetCoreRateLimit` (or equivalent) middleware to public-facing routes
- Frontend TypeScript / view-model type definitions MUST use the exact field names emitted by the JSON serialiser (System.Text.Json with `JsonNamingPolicy.CamelCase`). Do not rename or case-convert fields between server and client without an explicit serialisation layer

### Security Middleware

- Configure security headers via middleware (CSP, HSTS, X-Content-Type-Options, X-Frame-Options, Referrer-Policy, Permissions-Policy) — see `ukhsa-security.instructions.md`
- Set CSP to `default-src 'self'`; allowlist only `govuk-frontend` assets and approved CDNs
- Enable antiforgery tokens on all state-changing routes (`[ValidateAntiForgeryToken]` or global filter)
- Use ASP.NET Core Data Protection with keys stored in Azure Key Vault for multi-instance deployments
- Authentication via Microsoft Entra ID (OpenID Connect) where applicable; otherwise scheme MUST be approved by the security function

### Secrets

- Local development: user secrets (`dotnet user-secrets`) or environment variables — never commit `appsettings.Development.json` containing secrets
- Production: Azure Key Vault references via `Microsoft.Extensions.Configuration.AzureKeyVault` + Managed Identity
- Bind configuration to typed `IOptions<T>` classes — never read `IConfiguration` strings throughout the codebase
- Terraform outputs containing secrets MUST use `sensitive = true`

### Input Validation

- Validate all user input at the controller boundary using request models with FluentValidation or DataAnnotations
- Never concatenate user input into SQL — use EF Core LINQ or parameterised `FromSqlInterpolated`
- Never pass user input to `Process.Start`, `Assembly.Load`, or dynamic compilation APIs
- Never render user-supplied HTML without `HtmlEncoder` / `IHtmlContent` sanitisation — Razor encodes by default; `Html.Raw` MUST be reviewed

### Dependencies

- Pin exact package versions in `Directory.Packages.props` (Central Package Management)
- Run `dotnet list package --vulnerable --include-transitive` in CI; fail the build on critical/high CVEs
- Configure Dependabot or GitHub native package updates for `nuget` and `github-actions` ecosystems

---

## Frontend Implementation (Razor + GovUk.Frontend.AspNetCore)

### Setup

- Install: `dotnet add package GovUk.Frontend.AspNetCore`
- Register in `Program.cs`: `builder.Services.AddGovUkFrontend();` and `app.UseGovUkFrontend();`
- Static assets are served automatically from the package — no manual CSS import required
- Tag helpers are available via `@addTagHelper *, GovUk.Frontend.AspNetCore` in `_ViewImports.cshtml`

### Component Usage

- Always use the `<govuk-*>` tag helpers — never hand-code components that exist in the GOV.UK Design System
- See [GOV.UK Design System components](https://design-system.service.gov.uk/components/) for the canonical list and content guidance
- UKHSA branding (header logo, colours, footer links) is applied via an override partial — see the `govuk-frontend.instructions.md` file

### Layout

- Wrap pages in a shared `_Layout.cshtml` with UKHSA-branded `<govuk-header>` and `<govuk-footer>`
- Use the GOV.UK grid: `govuk-grid-row`, `govuk-grid-column-two-thirds`, etc.
- The first focusable element MUST be a skip link to `#main-content`
- Pages MUST set `<title>` following the pattern `Page name – Service name – GOV.UK`

### Forms

- Use `<govuk-input>`, `<govuk-radios>`, `<govuk-date-input>`, `<govuk-select>`, `<govuk-textarea>` tag helpers
- Use `<govuk-error-summary>` at the top of the page on validation failure, with anchor links to each erroneous field
- Bind error messages via `asp-for` and `ModelState` integration with the tag helpers
- Follow the one-question-per-page pattern unless the questions are tightly related

---

## Testing Implementation (xUnit)

### Framework

- xUnit as the test runner
- FluentAssertions for readable assertions
- Moq for mocking (or NSubstitute if preferred — pick one per repository)
- `Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<TEntryPoint>` for in-process integration tests
- `coverlet.collector` for coverage; report via `dotnet test --collect:"XPlat Code Coverage"`

### File Structure

```
tests/
├── <ServiceName>.UnitTests/        # Unit tests mirror src/ structure
│   ├── Application/
│   ├── Domain/
│   └── Web/
├── <ServiceName>.IntegrationTests/ # WebApplicationFactory + real test DB
│   └── Endpoints/
├── <ServiceName>.E2ETests/         # Playwright for .NET
└── <ServiceName>.PerformanceTests/ # k6 (.k6.js — JavaScript)
```

### Fixtures

- Shared fixtures live in `Fixtures/` and use `IClassFixture<T>` or `ICollectionFixture<T>`
- Integration tests use a `WebApplicationFactory<Program>` fixture with the real `Program.cs` and a per-test database (Azure SQL test instance or SQL Server in a container)
- Mock external HTTP dependencies with `HttpMessageHandler` fakes or WireMock.Net

### Test Naming

- Files: `<ClassUnderTest>Tests.cs`
- Methods: `MethodName_StateUnderTest_ExpectedBehaviour()`
- Example: `Submit_WhenIdentifierInvalid_ReturnsValidationProblem()`

### Running

```bash
dotnet test                                              # All tests
dotnet test tests/<ServiceName>.UnitTests                # Unit only
dotnet test --collect:"XPlat Code Coverage"              # With coverage
dotnet test /p:CollectCoverage=true /p:Threshold=80      # Enforce threshold
```

### Rules

- Never use `[Fact(Skip = "...")]` without a reason string referencing a tracked issue
- Never share mutable state between tests — use fresh fixtures or per-test setup
- Mocking boundary rules in `testing.instructions.md` apply

---

## Infrastructure Implementation (Terraform / Azure)

### App Hosting

- `azurerm_linux_web_app` for App Service on Linux, OR `azurerm_container_app` for Container Apps
- .NET 10 runtime stack for App Service (`dotnet|10.0`)
- For Container Apps, build images via GitHub Actions and push to Azure Container Registry
- Health check endpoint MUST be exposed at `/health` (liveness) and `/health/ready` (readiness) using `Microsoft.Extensions.Diagnostics.HealthChecks`

### Observability

- Application Insights connection string injected via Key Vault reference
- Structured logging via NLog → Application Insights sink — every log entry MUST include `CorrelationId`
- Distributed tracing via OpenTelemetry .NET SDK with the Azure Monitor exporter
