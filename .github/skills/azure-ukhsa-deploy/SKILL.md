---
name: azure-ukhsa-deploy
description: 'Use when deploying a UKHSA .NET 10 / ASP.NET Core service to Azure via Terraform with managed identity, Key Vault, and Application Insights.'
---

# Azure UKHSA Deploy — Terraform + .NET 10 to Azure UK South

This skill provisions Azure infrastructure for UKHSA digital services in line with the [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/) and NCSC Cloud Security Principles. UK South is the primary region; UK West is the DR region.

## When to Use

- Scaffolding a new UKHSA service in Azure South
- Adding new infrastructure components (database, queue, storage) to an existing service
- Wiring up CI/CD via GitHub Actions with OIDC federation
- Provisioning environments (dev / test / prod) from the same Terraform module


## UKHSA Naming Convention

All resources follow `<resource-prefix>-${var.workload}-${var.environment}-uks-${var.instance}`:

| Resource | Prefix | Example |
|---|---|---|
| Resource Group | `rg-` | `rg-notify-prod-uks-001` |
| App Service | `app-` | `app-notify-prod-uks-001` |
| App Service Plan | `plan-` | `plan-notify-prod-uks-001` |
| Key Vault | `kv-` | `kv-notify-prod-uks-001` |
| SQL Server | `sql-` | `sql-notify-prod-uks-001` |
| SQL Database | `sqldb-` | `sqldb-notify-prod-uks-001` |
| User-Assigned Managed Identity | `id-` | `id-notify-prod-uks-001` |
| Application Insights | `appi-` | `appi-notify-prod-uks-001` |
| Log Analytics Workspace | `log-` | `log-notify-prod-uks-001` |
| Storage Account | `st` (no dash, ≤24 chars) | `stnotifyproduks001` |

## Mandatory Tags

Every resource block must set these tags:

```hcl
tags = {
  workload            = var.workload
  environment         = var.environment
  owner               = var.owner_team_email
  cost_centre         = var.cost_centre
  data_classification = var.data_classification  # OFFICIAL or OFFICIAL-SENSITIVE
}
```

## Identity and Secrets

- **User-Assigned Managed Identity** per app (`azurerm_user_assigned_identity`) — never use system-assigned in shared resources.
- App Service references secrets via Key Vault references: `@Microsoft.KeyVault(SecretUri=...)`.
- Managed identity granted `Key Vault Secrets User` role via `azurerm_role_assignment` (RBAC mode, not access policies).
- SQL authentication is Entra-only — set `azuread_authentication_only = true` on `azurerm_mssql_server`.

## Networking and Security

- Private Endpoints for SQL, Key Vault, and Storage (no public network access).
- HSTS enforced at the app level; `https_only = true` on App Service.
- TLS 1.2 minimum on App Service, SQL Server, and Storage Account.
- Diagnostic settings stream to Log Analytics — `azurerm_monitor_diagnostic_setting`.

## .NET 10 App Service Configuration

```hcl
resource "azurerm_linux_web_app" "app" {
  name                = "app-${var.workload}-${var.environment}-uks-${var.instance}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  service_plan_id     = azurerm_service_plan.plan.id
  https_only          = true

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.app.id]
  }

  site_config {
    minimum_tls_version = "1.2"
    application_stack {
      dotnet_version = "10.0"
    }
    health_check_path = "/health"
  }

  app_settings = {
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = "@Microsoft.KeyVault(SecretUri=${azurerm_key_vault_secret.appi.id})"
    "ASPNETCORE_ENVIRONMENT"                = var.environment
  }

  tags = local.tags
}
```

## CI/CD — GitHub Actions OIDC

- Use `azure/login@v2` with federated credentials — no client secrets in GitHub.
- One federated credential per environment, subject scoped to the deployment job.
- Deploy via `azure/webapps-deploy@v3` after `dotnet publish -c Release`.

## Deploy Commands

```bash
dotnet publish src/Web -c Release -o ./publish
az webapp deploy \
  --resource-group rg-${WORKLOAD}-${ENV}-uks-001 \
  --name app-${WORKLOAD}-${ENV}-uks-001 \
  --src-path ./publish.zip --type zip
```

## Module Structure

```
infra/
├── main.tf            # Resource Group, tags, providers
├── variables.tf       # workload, environment, instance, owner_team_email, cost_centre, data_classification
├── outputs.tf
├── app.tf             # App Service, Plan, Managed Identity
├── data.tf            # SQL Server + Database
├── kv.tf              # Key Vault, secrets, RBAC
├── monitoring.tf      # Log Analytics, Application Insights, diagnostic settings
├── network.tf         # VNet, subnets, Private Endpoints, Private DNS
└── envs/
    ├── dev.tfvars
    ├── test.tfvars
    └── prod.tfvars
```

## Rules

- Terraform state lives in a dedicated `tfstate` Storage Account with versioning enabled.
- No `client_secret` in code — managed identity or OIDC only.
- Every environment has its own `tfvars` file — never parameterise inline.
- Production changes go through a PR + plan review; never `terraform apply` ad-hoc.
- Region: always `uksouth` — other regions might be allowd for development but production must be UK South
- Identity: always Managed Identity — never service principal secrets
- Secrets: always Key Vault — never hardcode or use app settings directly

## References

- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
- [NCSC Cloud Security Principles](https://www.ncsc.gov.uk/collection/cloud)
- [azurerm provider docs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs)
