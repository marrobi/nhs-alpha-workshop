---
applyTo: "**"
---

# Tech Stack Profile

This is the **single source of truth** for the current technology choices. All agents and instructions reference this file rather than hardcoding specific frameworks or platforms.

To change the tech stack, update this file and swap the corresponding tech-specific agents, instructions, and skills.

## Current Stack

| Concern | Choice |
|---|---|
| **Backend (form UI)** | .NET 10 / ASP.NET Core MVC / Razor views / Kestrel |
| **Backend (real APIs)** | .NET 10 / ASP.NET Core Web API |
| **Backend (alpha mocks only)** | .NET 10 / ASP.NET Core Minimal API — same solution, not deployed to production |
| **Design System** | `GovUk.Frontend.AspNetCore` NuGet package |
| **Database** | Azure SQL + EF Core 10; also consider CosmosDB or Azure Health Data Services FHIR when storing health data |
| **HTTP Resilience** | Polly (retry, circuit breaker, 5-second timeout on all outbound calls) |
| **Logging** | nlog + Application Insights sink |
| **Notifications** | GOV.UK Notify .NET client |
| **Testing** | NUnit + `WebApplicationFactory<Program>` + `HttpClient` |
| **E2E Testing** | Playwright (.NET) + axe-core |
| **Performance Testing** | k6 (JavaScript) |
| **IaC** | Terraform (`azurerm` provider) |
| **Hosting** | Azure Container Apps (UK South) |
| **Secrets** | Azure Key Vault via Managed Identity |
| **Monitoring** | Azure Application Insights |
| **CI/CD** | GitHub Actions |
| **SAST** | SonarQube |
| **Dependency / Container scanning** | Snyk |
| **Linting** | .NET analysers + EditorConfig |

## Key Files When Swapping

| Layer | Files to swap |
|---|---|
| Tech agents | `ukhsa-service-builder`, `testing`, `playwright-e2e`, `performance`, `security-reviewer`, `cicd-pipeline-builder`, `accessibility-auditor` |
| Tech instructions | This file (`tech-stack.instructions.md`) — all tech-specific implementation details live here |
| Tech skills | `fastapi-react-azure`, `azure-ukhsa-deploy`, `playwright-ukhsa-e2e`, `ukhsa-synthetic-data` (code examples) |
| Domain agents (no change needed) | `ukhsa-architect`, `ukhsa-product-owner`, `day2-issue-generator`, `ukhsa-clinical-safety`, `ukhsa-dpia-advisor`, `ukhsa-gds-assessor`, `ukhsa-content-designer` |
| Domain instructions (no change needed) | `ukhsa-api`, `ukhsa-security`, `ukhsa-frontend`, `testing`, `performance`, `terraform-azure-ukhsa` — these contain tech-agnostic rules only |
| Domain skills (no change needed) | `dcb0129-hazard-log`, `ukhsa-dpia`, `ukhsa-user-stories`, `gds-service-standard`, `ukhsa-adr-writer` |

---

## Backend Implementation (.NET 10)

### Project Structure

The solution has three distinct project types — do not mix them:

- **ASP.NET Core MVC** (`src/ImmForm.Web/`) — multi-step form UI, Razor views, session state, GDS tag helpers
- **ASP.NET Core Web API** (`src/ImmForm.Api/`) — real service endpoints (registration, audit, admin dashboard)
- **ASP.NET Core Minimal API** (`src/ImmForm.Mocks/`) — alpha-only stubs for external APIs; **not deployed to production**

### MVC Form Controllers

- Use ASP.NET Core MVC controllers with Razor views (`Views/` folder) — **do not use Razor Pages** (`Pages/` folder); they are a different ASP.NET Core pattern and must not be used in this project
- Inherit from `FormStepController<TModel>` base class; it provides: server-side session-backed step state, sequential and non-sequential navigation, back-link generation, model validation orchestration per step, and check-your-answers payload assembly
- Each step implements `IFormStep<TModel>` with a strongly typed model, a Razor view using GDS tag helpers, and an optional step-visibility predicate (used for conditional steps such as wholesaler-specific screens)
- Define the step sequence in a JSON configuration file or fluent builder — never hardcode the sequence in the controller; this allows different applications to define different journeys without modifying base framework code
- Abstract API integration behind `IRegistrationApiClient` — different applications substitute their own implementation; never call the registration API directly from a controller
- Use `[HttpGet]` / `[HttpPost]` with Model binding and `[ValidateAntiForgeryToken]` on all POST actions
- Server-side session state only — never store journey state in hidden fields or query parameters
- Use `ModelState.IsValid` with Data Annotations (`[Required]`, `[MaxLength]`, `[RegularExpression]`) or FluentValidation
- Return `View()` on validation failure; redirect to next step on success (PRG pattern)
- NHS Number check digit validation applies only to services that collect patient records — it does not apply to orderer registration services

### Web API Controllers

- Controllers in `src/ImmForm.Api/Controllers/` using `[ApiController]`, `[Route]` attributes
- Use record types or classes for request/response DTOs; validate with Data Annotations or FluentValidation
- Return `IActionResult` or `ActionResult<T>` with explicit status codes
- Apply rate limiting middleware (`Microsoft.AspNetCore.RateLimiting`) to all public endpoints
- Do not expose stack traces or internal exception detail in error responses

### Minimal API Mocks (alpha only)

- Define in `src/ImmForm.Mocks/Program.cs` using `app.MapGet` / `app.MapPost`
- Implement expected request/response contracts so failure states (API unavailability, missing AP record, registration error) can be exercised
- Built into the same solution but **not deployed to production**

### Security Middleware (.NET)

- Register security headers middleware before route handlers: CSP (`default-src 'self'`), HSTS, `X-Content-Type-Options: nosniff`, `X-Frame-Options: DENY`
- Enable CSRF protection: `builder.Services.AddAntiforgery()` and `[ValidateAntiForgeryToken]` on all POST actions
- Configure strict Content Security Policy — allowlist `govuk-frontend` CDN assets only if served from CDN
- Apply rate limiting to all public-facing routes

### HTTP Resilience (Polly)

- Register typed HTTP clients with Polly pipelines via `AddHttpClient<T>().AddStandardResilienceHandler()`
- All outbound calls (ImmForm Organisation API, ImmForm Registration API, GOV.UK Notify) must have:
  - 5-second timeout per attempt
  - Retry with exponential back-off (3 attempts)
  - Circuit breaker
- Log circuit breaker state transitions and retry attempts via nlog

### Secrets (.NET / Azure)

- Use `builder.Configuration["Section:Key"]` with the `IConfiguration` abstraction
- Load from environment variables in development; Azure Key Vault in production via Managed Identity
- Never use `.GetValue<T>("Key", defaultValue)` for secrets or required config — missing values must throw at startup
- Terraform outputs containing secrets must use `sensitive = true`

### Input Validation (.NET)

- Validate all user input at the controller action boundary using Model binding + Data Annotations or FluentValidation
- Never pass user input to `Process.Start`, raw SQL strings, or `Razor` `@Html.Raw()` with user content
- Use EF Core parameterised queries only — never string-interpolate into LINQ or raw SQL

### Dependencies (.NET)

- Pin exact versions in `*.csproj` — no floating version ranges (`*`, `[1.0,)`)
- Run `dotnet list package --vulnerable` and resolve critical/high vulnerabilities before merging
- Run Snyk for dependency and container image scanning in CI
- Configure Dependabot for automated security updates

---

## Frontend Implementation (ASP.NET Core MVC / Razor)

### Setup

- Install NuGet package: `GovUk.Frontend.AspNetCore`
- Register in `Program.cs`: `builder.Services.AddGovUkFrontend()`
- Add tag helper import in `_ViewImports.cshtml`: `@addTagHelper *, GovUk.Frontend.AspNetCore`
- Apply UKHSA header and footer branding via shared `_Layout.cshtml` layout override

### Tag Helper Usage

- Always use `GovUk.Frontend.AspNetCore` tag helpers — never hand-code components that exist in the design system
- See [GOV.UK Design System components](https://design-system.service.gov.uk/components) for available components
- Examples: `<govuk-input asp-for="EmailAddress" />`, `<govuk-button>Continue</govuk-button>`, `<govuk-error-summary asp-for="*" />`

### Layout

- Every Razor view uses `_Layout.cshtml` which wraps content in GDS page template with header and footer
- Set service name via `IOptions<GovUkFrontendOptions>` or layout ViewData
- Use GOV.UK grid classes: `govuk-grid-row`, `govuk-grid-column-two-thirds`
- Include skip link as the first element: `<a href="#main-content" class="govuk-skip-link">Skip to main content</a>`

### Forms

- Use `<govuk-input>`, `<govuk-radios>`, `<govuk-date-input>`, `<govuk-select>` tag helpers
- Use `<govuk-error-summary asp-for="*" />` at the top of the page on validation failure
- Add `<govuk-error-message asp-for="FieldName" />` inline on affected fields
- Page title format on error: `Error: [page title] — ImmForm — GOV.UK`

---

## Testing Implementation (NUnit)

### Framework

- NUnit as the test framework
- `WebApplicationFactory<Program>` for integration tests (spins up the real ASP.NET Core pipeline in-process)
- `HttpClient` from `factory.CreateClient()` for HTTP-level route testing
- `Moq` or `NSubstitute` for mocking external dependencies in unit tests

### File Structure

```
tests/
├── ImmForm.Tests.Unit/           # Unit tests mirror src/ structure
│   ├── Controllers/
│   ├── Services/
│   └── Validators/
├── ImmForm.Tests.Integration/    # Integration tests via WebApplicationFactory
│   ├── HealthTests.cs
│   └── <Feature>Tests.cs
├── ImmForm.Tests.E2E/            # Playwright browser tests (separate agent)
└── tests/performance/            # k6 load tests (separate agent)
```

### Test Structure

- `[TestFixture]` on the test class
- `[Test]` on each test method
- `[SetUp]` / `[TearDown]` for shared arrange/cleanup
- Inject `WebApplicationFactory<Program>` via constructor or `[OneTimeSetUp]`

### Test Naming

- Files: `<Feature>Tests.cs`
- Methods: `<Method>_<Condition>_<ExpectedOutcome>()`
- Example: `HealthEndpoint_Get_Returns200Ok()`

### Running

```bash
dotnet test                                                    # All tests
dotnet test tests/ImmForm.Tests.Unit/                          # Unit only
dotnet test --collect:"XPlat Code Coverage"                    # With coverage
dotnet test /p:CollectCoverage=true /p:CoverletThreshold=80    # Enforce threshold
```

### Rules

- Never use `[Ignore]` without a reason string
- Mock external HTTP clients using `HttpMessageHandler` substitutes — not real endpoints in unit tests
- Integration tests use `WebApplicationFactory` with test doubles for external HTTP calls (ImmForm APIs, GOV.UK Notify)

---

## Infrastructure Implementation (Terraform / Azure)

### Container Apps

- `azurerm_container_app` with .NET 10 Docker image from Azure Container Registry
- Configure ingress: HTTPS only, external traffic on port 443, target port 8080 (Kestrel default in container)
- Multi-stage `Dockerfile`: `mcr.microsoft.com/dotnet/sdk:10.0` build stage → `mcr.microsoft.com/dotnet/aspnet:10.0` runtime stage
- Pin to specific image digest — never use `:latest`
- Alpha mock projects are built into the same image but registered only in non-production environments via configuration
