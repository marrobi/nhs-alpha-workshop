---
name: fastapi-react-azure
description: 'Use when scaffolding or building a UKHSA service with the FastAPI + React + Azure stack. Contains project structure, scaffold steps, and deployment commands.'
---

# FastAPI + React + Azure — Implementation Skill

This skill provides the concrete implementation detail for building a UKHSA service with the current default tech stack. It is referenced by the UKHSA Service Builder agent and can be swapped for an alternative (e.g. `django-htmx-azure`) when changing stacks.

## Tech Stack

- **Backend**: Python 3.12 with FastAPI and Uvicorn — API-only (JSON)
- **Frontend**: React 18 with Vite and TypeScript, using [govuk-react](https://github.com/govuk-react/govuk-react) + `govuk-frontend` CSS
- **Design System**: [GOV.UK Frontend](https://design-system.service.gov.uk) — all user-facing pages
- **Testing**: pytest + httpx (backend), Vitest (frontend) — write tests alongside features
- **IaC**: Terraform with `azurerm` provider — see [Terraform Azure Provider docs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)
- **Hosting**: Azure App Service on Linux (UK South region only)
- **Secrets**: Azure Key Vault, referenced via App Service configuration
- **Monitoring**: Azure Application Insights

## Project Structure

```
app/
  main.py           # FastAPI app with CORS, middleware, health endpoint
  routers/           # API route modules
  middleware/         # Security, logging middleware
frontend/
  src/
    components/      # React components using govuk-react
    pages/           # Page components
    App.tsx           # Root component with React Router
    main.tsx          # Entry point — imports govuk-frontend CSS
  package.json
  vite.config.ts
  tsconfig.json
requirements.txt     # Pinned Python dependencies
infra/
  main.tf            # Terraform resources
  variables.tf       # Input variables
  outputs.tf         # Output values
```

## Scaffold Steps

### Dependencies

1. Create `requirements.txt` with pinned production and dev dependencies:
   - `fastapi`, `uvicorn[standard]`, `pydantic`, `slowapi`, `python-multipart`, `structlog`, `httpx` (production)
   - `pytest`, `pytest-asyncio`, `httpx` (testing)
   - `pytest-playwright`, `axe-playwright-python` (E2E testing — pre-installed in devcontainer, pinned here for CI)
   - `ruff` (linting)
   - Pin **exact** versions (`==`) — no loose ranges

### Backend — FastAPI

1. Set up FastAPI app in `app/main.py` with:
   - Security headers middleware (CSP, HSTS, X-Content-Type-Options)
   - CORS middleware configured for the React dev server
   - Rate limiting (slowapi)
   - `GET /api/health` returning `{ "status": "ok" }` with 200
2. Define routers in `app/routers/` using `APIRouter(prefix="/api/v1/...", tags=[...])`
3. Use Pydantic models for all request/response schemas
4. Use `async def` for route handlers

### Frontend — React + govuk-react

1. Scaffold React app with Vite:
   - `npm create vite@latest frontend -- --template react-ts`
   - Install: `npm install govuk-react govuk-frontend react-router-dom`
   - Import `govuk-frontend/dist/govuk/all.css` in `main.tsx`
2. Create UKHSA-branded layout with `<Header>`, `<Footer>` from govuk-react
3. Create the start page at `/`
4. Configure Vite to proxy `/api` to FastAPI during development

### Infrastructure — Terraform + Azure

1. Write Terraform in `infra/` using `var.app_name` for resource naming:
   - Resource Group, App Service Plan (Linux, B1), Linux Web App
   - Key Vault with Managed Identity access policy
   - Application Insights
   - All in `uksouth` region
2. Run `terraform init && terraform plan` to validate

### Testing

1. Write pytest tests for `/api/health` using httpx `AsyncClient`
2. Write Vitest tests for React components
3. Target: 80% coverage

## Build & Deploy Commands

```bash
# Backend
pip install -r requirements.txt
uvicorn app.main:app --reload --port 8000

# Frontend
cd frontend && npm ci && npm run dev

# Build for production
cd frontend && npm run build && cd ..

# Terraform
cd infra && terraform init && terraform plan -var="app_name=my-service" -out=tfplan
terraform apply tfplan

# Deploy to Azure
zip -r app.zip app/ frontend/dist/ requirements.txt
az webapp deploy \
  --resource-group "rg-${APP_NAME}-dev" \
  --name "app-${APP_NAME}-dev" \
  --src-path app.zip --type zip

# Verify
curl https://app-${APP_NAME}-dev.azurewebsites.net/api/health
```

## Troubleshooting

- If `terraform apply` fails, read the error, fix the HCL, and re-run
- If `pytest` fails, fix the code (not the test) unless the test is wrong
- If the Azure deployment fails, check logs with `az webapp log tail`
- Always verify live by hitting the Azure URL with `curl`
