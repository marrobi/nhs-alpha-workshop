---
name: fastapi-react-azure
description: 'Use when scaffolding or building an NHS service with the FastAPI + React + Azure stack. Contains project structure, scaffold steps, and deployment commands.'
---

# FastAPI + React + Azure — Implementation Skill

This skill provides the concrete implementation detail for building an NHS service with the current default tech stack. It is referenced by the NHS Service Builder agent and can be swapped for an alternative (e.g. `django-htmx-azure`) when changing stacks.

## Tech Stack

- **Backend**: Python 3.12 with FastAPI and Uvicorn — API-only (JSON)
- **Frontend**: React 18 with Vite and TypeScript, using [nhsuk-react-components](https://github.com/NHSDigital/nhsuk-react-components) + `nhsuk-frontend` CSS
- **Design System**: [NHS.UK Frontend](https://service-manual.nhs.uk/design-system) — all user-facing pages
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
    components/      # React components using nhsuk-react-components
    pages/           # Page components
    App.tsx           # Root component with React Router
    main.tsx          # Entry point — imports nhsuk-frontend CSS
  package.json
  vite.config.ts
  tsconfig.json
requirements.txt     # Pinned Python dependencies
Dockerfile           # Multi-stage build: frontend assets + FastAPI app
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
   - `GET /api/health` returning the health state and deployed code version (e.g. `{ "status": "ok", "version": "<app version or git SHA>" }`) with 200 — read the version from whatever the deployment supplies it through (a required env var such as `APP_VERSION`, or a build-time version file), failing loudly if it is missing rather than silently defaulting. See the `azure-nhs-deploy` skill for supplying the version per deployment model
2. Define routers in `app/routers/` using `APIRouter(prefix="/api/v1/...", tags=[...])`
3. Use Pydantic models for all request/response schemas
4. Use `async def` for route handlers

### Frontend — React + nhsuk-react-components

1. Scaffold React app with Vite:
   - `npm create vite@latest frontend -- --template react-ts`
   - Install: `npm install nhsuk-react-components nhsuk-frontend react-router-dom`
   - Import `nhsuk-frontend/dist/nhsuk.css` in `main.tsx`
2. Create NHS-branded layout with `<Header>`, `<Footer>` from nhsuk-react-components
3. Create the start page at `/`
4. Configure Vite to proxy `/api` to FastAPI during development

### Infrastructure — Terraform + Azure

1. Write Terraform in `infra/` using `var.app_name` for resource naming:
   - Resource Group, Container Registry, App Service Plan (Linux, B1), Linux Web App for Containers
   - Key Vault with Managed Identity access policy; grant the identity the `AcrPull` role on the Container Registry so it can pull the image
   - Application Insights
   - All in `uksouth` region
2. Run `terraform init && terraform plan` to validate

### Container — Docker

1. Add a `Dockerfile` at the repo root that builds the frontend and serves the FastAPI app on a single port (`uvicorn app.main:app --host 0.0.0.0 --port 8000`)
2. Accept `ARG APP_VERSION` and set it as `ENV APP_VERSION` so the health endpoint reports the deployed commit
3. The same image runs locally (`docker run`) and on Azure — see the tech-stack profile for details

### Testing

1. Write pytest tests for `/api/health` using httpx `AsyncClient` — assert 200, the health state, and that a `version` is returned
2. Write Vitest tests for React components
3. Target: 80% coverage

## Build & Deploy Commands

```bash
# Run locally — backend (hot reload)
pip install -r requirements.txt
uvicorn app.main:app --reload --port 8000

# Run locally — frontend dev server (proxies /api to the backend)
cd frontend && npm ci && npm run dev

# Run the whole service locally as the production container(s)
docker build --build-arg APP_VERSION=$(git rev-parse --short HEAD) -t nhs-service:local .
docker run -p 8000:8000 nhs-service:local
curl http://localhost:8000/api/health   # expect 200 with a version

# Provision infrastructure
cd infra && terraform init && terraform plan -var="app_name=my-service" -out=tfplan
terraform apply tfplan

# Build and push the image, then deploy by bumping the image tag Terraform variable
# (no zip deploy — see azure-nhs-deploy skill). The web app picks up the new tag on apply.
az acr login --name "acr${APP_NAME}dev"
docker build --build-arg APP_VERSION=$(git rev-parse --short HEAD) \
  -t "acr${APP_NAME}dev.azurecr.io/${APP_NAME}:$(git rev-parse --short HEAD)" .
docker push "acr${APP_NAME}dev.azurecr.io/${APP_NAME}:$(git rev-parse --short HEAD)"
terraform apply -var="app_name=${APP_NAME}" -var="image_tag=$(git rev-parse --short HEAD)"

# Verify — expect HTTP 200 and confirm the deployed version matches the committed code
curl https://app-${APP_NAME}-dev.azurewebsites.net/api/health
```

## Troubleshooting

- If `terraform apply` fails, read the error, fix the HCL, and re-run
- If `pytest` fails, fix the code (not the test) unless the test is wrong
- If the container fails to start on Azure, check logs with `az webapp log tail`
- Always verify live by hitting the Azure URL with `curl`
