---
applyTo: "**"
---

# Tech Stack Profile

This is the **single source of truth** for the current technology choices. All agents and instructions reference this file rather than hardcoding specific frameworks or platforms.

To change the tech stack, update this file and swap the corresponding tech-specific agents, instructions, and skills.

## Current Stack

| Concern | Choice |
|---|---|
| **Backend** | Python 3.12 / FastAPI / Uvicorn |
| **Frontend** | React 18 / Vite / TypeScript / govuk-react |
| **Design System** | GOV.UK Frontend (`govuk-frontend` CSS + `govuk-react`) |
| **Database** |  PostgreSQL, SQL Server, CosmosDB, also consider Azure HDS FHIR when storing health data. |
| **Backend Testing** | pytest + httpx (unit/integration) |
| **Frontend Testing** | Vitest |
| **E2E Testing** | Playwright (Python) + axe-playwright-python |
| **Performance Testing** | k6 (JavaScript) |
| **IaC** | Terraform (`azurerm` provider) |
| **Hosting** | Azure App Service on Linux (UK South) |
| **Secrets** | Azure Key Vault via Managed Identity |
| **Monitoring** | Azure Application Insights |
| **CI/CD** | GitHub Actions |
| **Linting** | ruff (Python), ESLint (frontend) |

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

## Backend Implementation (FastAPI)

### API Route Structure

- Define routers in `app/routers/` using `APIRouter(prefix="/...", tags=["..."])`
- Use Pydantic models for all request/response schemas
- Use `async def` for route handlers — FastAPI is async-first
- Include routers in `app/main.py` with `app.include_router()`
- Use Pydantic models for input validation — FastAPI returns 422 automatically for invalid input
- Use a Pydantic validator for NHS Number check digit validation
- Apply `slowapi` rate limiting to all API endpoints
- Frontend type definitions for API responses MUST use the exact field names from the corresponding Pydantic response model
- Do not rename, alias, or case-convert fields between backend and frontend without an explicit serialisation layer

### Security Middleware (FastAPI)

- Apply security headers middleware before any route handlers (CSP, HSTS, X-Content-Type-Options, X-Frame-Options)
- Configure strict Content Security Policy: `default-src 'self'`; allowlist only `govuk-frontend` CDN assets if used
- Apply `slowapi` rate limiting to all public-facing routes
- Enable CSRF protection on all state-changing routes (POST, PUT, DELETE)

### Secrets (Python / Azure)

- Use `os.environ["VARIABLE_NAME"]` for local development
- Use Azure Key Vault references for production: `@Microsoft.KeyVault(SecretUri=...)`
- Terraform outputs containing secrets must use `sensitive = true`

### Input Validation (FastAPI / React)

- Validate all user input at the route handler boundary using Pydantic models
- Never pass user input to `eval()`, `exec()`, `subprocess.run(shell=True)`, or f-string interpolation in queries
- Never use `dangerouslySetInnerHTML` with user-supplied content in React components

### Dependencies (Python)

- Pin exact versions in `requirements.txt` — no `>=` or `~=` ranges
- Run `pip audit` and resolve critical/high vulnerabilities before merging
- Configure Dependabot for automated security updates

---

## Frontend Implementation (React / govuk-react)

### Setup

- Install: `npm install govuk-react govuk-frontend`
- Import CSS in entry point: `import 'govuk-frontend/dist/govuk/all.css'`
- Import components individually: `import { Header, Footer, Button } from 'govuk-react'`

### Component Usage

- Always use `govuk-react` — never hand-code components that exist in the design system
- See [GOV.UK Design System components](https://design-system.service.gov.uk/components) for available components
- Follow the prop patterns from the govuk-react docs, which mirror the [GOV.UK Design System components](https://design-system.service.gov.uk/components)

### Layout

- Wrap pages in `<Header>` and `<Footer>` from govuk-react
- Set service name in the Header component
- Use GOV.UK grid: `<GridRow>`, `<GridCol setWidth="two-thirds">`
- Include a skip link as the first element

### Forms

- Use `<Input>`, `<Radios>`, `<DateInput>`, `<Select>` components from govuk-react
- Use `<ErrorSummary>` at the top of the page on validation failure
- Use `error` prop on form components for inline error messages

---

## Testing Implementation (pytest)

### Framework

- pytest with httpx `AsyncClient` for FastAPI route testing
- pytest-anyio for async test support
- pytest-cov for coverage reporting

### File Structure

```
tests/
├── conftest.py          # Shared fixtures (test client, mock data)
├── unit/                # Unit tests mirror app/ structure
│   ├── test_health.py
│   └── test_<router>.py
├── integration/         # Integration tests for cross-module flows
├── e2e/                 # Playwright browser tests (separate agent)
└── performance/         # k6 load tests (separate agent)
```

### Fixtures

- Define shared fixtures in `conftest.py`
- Use `@pytest.fixture` with appropriate scope
- Always create a reusable `client` fixture for the FastAPI test client
- Mock external dependencies with `unittest.mock.patch` or `pytest-mock`

### Test Naming

- Files: `test_<module>.py`
- Functions: `test_<what>_<condition>_<expected>()`
- Example: `test_health_endpoint_returns_200()`

### Running

```bash
pytest                                          # All tests
pytest tests/unit/                              # Unit only
pytest --cov=app --cov-report=term-missing      # With coverage
pytest --cov=app --cov-fail-under=80            # Enforce threshold
```

### Rules

- Never use `@pytest.mark.skip` without a reason string
- Mock external dependencies with `unittest.mock.patch` or `pytest-mock`

---

## Infrastructure Implementation (Terraform / Azure)

### App Service

- `azurerm_linux_web_app` with Python 3.12 runtime stack
- Configure startup command: `uvicorn app.main:app --host 0.0.0.0 --port 8000`
