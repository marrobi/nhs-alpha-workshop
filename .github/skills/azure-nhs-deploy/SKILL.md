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
├── Container Registry (acr{app_name}{env})
├── App Service Plan (asp-{app_name}-{env}, Linux, B1)
├── Linux Web App for Containers (app-{app_name}-{env})
├── User Assigned Managed Identity
├── Role Assignment (identity → AcrPull on the registry)
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

### 4. Build & Push the Container Image

The service ships as a single Docker image (see the tech-stack profile). Build it with the deployed version baked in, then push it to the registry. Avoid zip deploys — containers give an identical artefact locally and in Azure.

```bash
az acr login --name "acr${APP_NAME}dev"
docker build \
  --build-arg APP_VERSION="$(git rev-parse --short HEAD)" \
  -t "acr${APP_NAME}dev.azurecr.io/${APP_NAME}:$(git rev-parse --short HEAD)" .
docker push "acr${APP_NAME}dev.azurecr.io/${APP_NAME}:$(git rev-parse --short HEAD)"
```

The web app pulls this image. Point it at the new tag through Terraform (`docker_image_name` on `azurerm_linux_web_app`) so the change is tracked in IaC:

```bash
cd infra
terraform apply -var="app_name=${APP_NAME}" -var="image_tag=$(git rev-parse --short HEAD)"
```

Because `APP_VERSION` is baked into the image at build time, the running container reports the exact commit it was built from at the health endpoint. The application reads this value and fails loudly if it is missing — see the `org-standards` no-silent-fallback rule.

### 5. Verify

Confirm the live service returns HTTP 200 **and** that the `version` reported by the health endpoint matches the commit you deployed — this proves the correct code is live:

```bash
curl "https://app-${APP_NAME}-dev.azurewebsites.net/api/health"
# Expect HTTP 200 and a body such as {"status": "ok", "version": "<deployed git SHA>"}
```

## Key Terraform Resources

| Resource | Terraform Type |
|---|---|
| Resource Group | `azurerm_resource_group` |
| Container Registry | `azurerm_container_registry` |
| App Service Plan | `azurerm_service_plan` |
| Web App for Containers | `azurerm_linux_web_app` |
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
