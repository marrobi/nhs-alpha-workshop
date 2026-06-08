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

### 3. Review the plan before applying

**Always read the `tfplan` output before running `apply`.** Resources being
destroyed or recreated without warning have delayed deployments — never apply a
plan you have not inspected.

Generate a machine- and human-readable view of the plan:

```bash
terraform show tfplan            # full human-readable review of what will change
terraform show -no-color tfplan  # capture this output to summarise for the user
```

**The agent must summarise this plan back to the user and wait for explicit
confirmation before running `apply`.** Present the summary as:

- The final summary line, e.g. `Plan: 1 to add, 0 to change, 0 to destroy.`
- A short bullet list of every resource grouped by action, using the change symbols:
  - `+` create — a new resource
  - `~` update in place — safe change
  - `-` destroy — the resource will be **deleted**
  - `-/+` destroy and then create replacement — the resource will be **recreated**
    (existing data, such as Key Vault contents, may be lost)
- An explicit call-out of any `-` (destroy) or `-/+` (recreate) resources.

If anything is unexpectedly marked `-` or `-/+`, stop and investigate before
applying. Only proceed to apply once the user has confirmed the summary.

### 4. Apply

```bash
terraform apply tfplan
```

### 5. Build Frontend & Deploy

```bash
cd ../frontend && npm run build && cd ..
zip -r app.zip app/ frontend/dist/ requirements.txt
az webapp deploy \
  --resource-group "rg-${APP_NAME}-dev" \
  --name "app-${APP_NAME}-dev" \
  --src-path app.zip \
  --type zip
```

### 6. Configure Startup Command

```bash
az webapp config set \
  --resource-group "rg-${APP_NAME}-dev" \
  --name "app-${APP_NAME}-dev" \
  --startup-file "uvicorn app.main:app --host 0.0.0.0 --port 8000"
```

### 7. Verify

```bash
curl "https://app-${APP_NAME}-dev.azurewebsites.net/api/health"
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
- Plan review: always inspect `tfplan` before `apply`, summarise it for the user,
  and wait for confirmation — never apply a plan with unexpected `-` (destroy) or
  `-/+` (recreate) changes

## References

- [Terraform azurerm provider](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)
- [Azure App Service Python docs](https://learn.microsoft.com/en-us/azure/app-service/configure-language-python)
