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
| **Frontend** | React 18 / Vite / TypeScript / nhsuk-react-components |
| **Design System** | NHS.UK Frontend (`nhsuk-frontend` CSS + `nhsuk-react-components`) |
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
| Tech agents | `nhs-service-builder`, `testing`, `playwright-e2e`, `performance`, `security-reviewer`, `cicd-pipeline-builder`, `accessibility-auditor` |
| Tech instructions | `nhsuk-frontend`, `performance`, `terraform-azure-nhs`, `testing`, `nhs-api` (framework section), `nhs-security` (framework section) |
| Tech skills | `fastapi-react-azure`, `azure-nhs-deploy`, `playwright-nhs-e2e`, `nhs-synthetic-data` (code examples) |
| Domain agents (no change needed) | `nhs-architect`, `nhs-product-owner`, `day2-issue-generator`, `nhs-clinical-safety`, `nhs-dpia-advisor`, `nhs-gds-assessor`, `nhs-content-designer` |
| Domain skills (no change needed) | `dcb0129-hazard-log`, `nhs-dpia`, `nhs-user-stories`, `gds-service-standard`, `nhs-adr-writer` |
