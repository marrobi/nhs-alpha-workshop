---
name: azure-nhs-deploy
description: 'Use when deploying an NHS Python/FastAPI service to Azure using Terraform, or configuring App Service, Key Vault, or Application Insights.'
---

# Azure NHS Deploy — Terraform to Azure UK South

This skill provides step-by-step guidance for deploying an NHS Python/FastAPI + React service to Azure using Terraform. All infrastructure uses Azure UK South for data sovereignty compliance.

## When to Use

- Scaffolding Terraform for a new NHS service
- Deploying infrastructure with `terraform apply`
- Configuring App Service settings, Key Vault, or Application Insights
- Troubleshooting Azure deployment failures

## Architecture

All resource names use `var.app_name` so multiple Alphas can coexist in one subscription:

```
Resource Group (rg-{app_name}-{env})
├── App Service Plan (asp-{app_name}-{env}, Linux, B1)
├── Linux Web App (app-{app_name}-{env}, Python 3.12)
├── User Assigned Managed Identity
├── Key Vault (kv-{app_name}-{env})
│   └── Access Policy → Managed Identity (get, list secrets)
└── Application Insights (ai-{app_name}-{env})
```

## Deployment Steps

### 1. Scaffold Terraform

Create `infra/` with `main.tf`, `variables.tf`, `outputs.tf`. Define:
- `variable "app_name"` — required, the service name
- `variable "environment"` — default `"dev"`
- All `azurerm` resources using `"${var.app_name}-${var.environment}"` naming

### 2. Initialise and Plan

```bash
cd infra
terraform init
terraform plan -var="app_name=my-service" -out=tfplan
```

### 3. Apply

```bash
terraform apply tfplan
```

### 4. Build Frontend & Deploy

```bash
cd ../frontend && npm run build && cd ..
zip -r app.zip app/ frontend/dist/ requirements.txt
az webapp deploy \
  --resource-group "rg-${APP_NAME}-dev" \
  --name "app-${APP_NAME}-dev" \
  --src-path app.zip \
  --type zip
```

Make the deployed code version discoverable at runtime so the health endpoint can report it. **How** you supply it depends on the deployment model — choose what fits your stack rather than hardcoding one approach:

- **App Service app setting (zip deploy)**: define an `APP_VERSION` app setting in Terraform (`azurerm_linux_web_app.app_settings`) and pass the commit SHA via `terraform apply -var="app_version=$(git rev-parse --short HEAD)"`. Keep app settings in Terraform, not ad-hoc `az` commands, so they aren't overwritten on the next apply.
- **Container/Docker**: bake the version into the image at build time (e.g. a `--build-arg APP_VERSION=...` baked into an env var or a label), so the running image reports the version it was built from.
- **Build artefact**: write the SHA to a file (e.g. `app/version.txt`) during the build and read it at runtime.

The application reads this value (failing loudly if it is missing — see the `org-standards` no-silent-fallback rule) and returns it from the health endpoint.

### 5. Configure Startup Command

```bash
az webapp config set \
  --resource-group "rg-${APP_NAME}-dev" \
  --name "app-${APP_NAME}-dev" \
  --startup-file "uvicorn app.main:app --host 0.0.0.0 --port 8000"
```

### 6. Verify

Confirm the live service returns HTTP 200 **and** that the `version` reported by the health endpoint matches the commit you deployed — this proves the correct code is live:

```bash
curl "https://app-${APP_NAME}-dev.azurewebsites.net/api/health"
# Expect HTTP 200 and a body such as {"status": "ok", "version": "<deployed git SHA>"}
```

## Key Terraform Resources

| Resource | Terraform Type |
|---|---|
| Resource Group | `azurerm_resource_group` |
| App Service Plan | `azurerm_service_plan` |
| Web App | `azurerm_linux_web_app` |
| Key Vault | `azurerm_key_vault` |
| Key Vault Access Policy | `azurerm_key_vault_access_policy` |
| Managed Identity | `azurerm_user_assigned_identity` |
| Application Insights | `azurerm_application_insights` |

## Rules

- Region: always `uksouth` — other regions might be allowd for development but production must be UK South
- Identity: always Managed Identity — never service principal secrets
- Secrets: always Key Vault — never hardcode or use app settings directly
- TLS: minimum 1.2, HTTPS only
- Tags: `project = var.app_name`, `environment = var.environment`
- Naming: always include `var.app_name` — multiple Alphas may share a subscription

## References

- [Terraform azurerm provider](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)
- [Azure App Service Python docs](https://learn.microsoft.com/en-us/azure/app-service/configure-language-python)
