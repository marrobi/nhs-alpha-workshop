---
applyTo: "**/infra/**,**/*.tf"
---

# Terraform — Azure UKHSA Infrastructure

Standards for Terraform-managed Azure infrastructure for UKHSA services. Follows RFC 2119 terminology.

Reference: [Terraform Azure Provider docs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs).

## Provider

- Use the `azurerm` provider with a `features {}` block
- Pin the provider version in `required_providers` — no floating ranges
- All resources MUST be deployed to `uksouth` as the primary region (data sovereignty)
- DR replicas MUST use `ukwest`

## Naming Convention

All resource names MUST include `var.app_name` so multiple services can coexist in the same Azure subscription.

- Resource Group: `rg-${var.app_name}-${var.environment}`
- App Service Plan: `asp-${var.app_name}-${var.environment}`
- Linux Web App / Container App: `app-${var.app_name}-${var.environment}`
- Azure Container Apps Environment: `cae-${var.app_name}-${var.environment}`
- Container Registry: `acr${var.app_name}${var.environment}` (no hyphens — ACR naming rule)
- Key Vault: `kv-${var.app_name}-${var.environment}`
- Application Insights: `ai-${var.app_name}-${var.environment}`
- Log Analytics Workspace: `log-${var.app_name}-${var.environment}`
- Azure SQL Server: `sql-${var.app_name}-${var.environment}`
- Azure SQL Database: `sqldb-${var.app_name}-${var.environment}`
- Storage Account: `st${var.app_name}${var.environment}` (no hyphens — storage naming rule)
- Virtual Network: `vnet-${var.app_name}-${var.environment}`
- Subnet: `snet-<purpose>-${var.app_name}-${var.environment}`
- User-Assigned Managed Identity: `id-${var.app_name}-${var.environment}`

All resources MUST be tagged at minimum with:

```hcl
tags = {
  project     = var.app_name
  environment = var.environment
  owner       = var.team_name
  cost_centre = var.cost_centre
}
```

## Identity & Secrets

- Use `azurerm_user_assigned_identity` for application identities — never service principal client secrets
- GitHub Actions to Azure MUST authenticate via **OIDC federation** (`azure/login@v2` with `federated_token`), never a long-lived client secret
- **Never use shared access keys** for storage, databases, or any Azure service. Disable them where the resource supports it:
  - Storage: `shared_access_key_enabled = false`
  - SQL Server: enable Entra-only authentication
  - Cosmos DB: `local_authentication_disabled = true`
- Grant Key Vault access via RBAC roles (`Key Vault Secrets User`, `Key Vault Crypto User`) — never access policies
- Reference secrets in App Service via `@Microsoft.KeyVault(SecretUri=...)`
- Mark sensitive outputs with `sensitive = true`
- Assign least-privilege RBAC roles to each Managed Identity (e.g. `Storage Blob Data Contributor`, `SQL DB Contributor`) — never `Owner` or `Contributor` at subscription scope

## Network Isolation

- **No public endpoints for data services.** Azure SQL, Cosmos DB, Storage, Key Vault MUST use Azure Private Endpoints
- Create a VNet with at least:
  - One subnet for App Service VNet integration (`snet-app-...`)
  - One subnet for Private Endpoints (`snet-pe-...`)
- Use `azurerm_private_endpoint` for each data service and register Private DNS zones for name resolution
- Configure `virtual_network_subnet_id` on the web app (or `azurerm_app_service_virtual_network_swift_connection`) for outbound VNet integration
- Only the App Service / Container Apps HTTPS endpoint may be publicly reachable
- Apply Network Security Groups on subnets to restrict ingress and egress to required ports and protocols

## App Hosting

- `azurerm_linux_web_app` for App Service on Linux, OR `azurerm_container_app` for Container Apps — see `tech-stack.instructions.md` for runtime details
- `https_only = true`
- `site_config.minimum_tls_version = "1.2"`
- `site_config.ftps_state = "Disabled"`
- `client_affinity_enabled = false` (unless a documented reason requires sticky sessions)
- Enable Application Insights via `app_settings` (connection string via Key Vault reference)
- Configure health check path (`site_config.health_check_path = "/health"`)

## Diagnostics

- All resources MUST send diagnostic settings to the Log Analytics workspace
- App Service / Container Apps MUST send AppServiceHTTPLogs, AppServiceConsoleLogs, AppServiceAppLogs, and metrics
- Azure SQL MUST send audit logs and security events

## State

- Local state is permitted only for individual developer experimentation
- Shared environments (dev, test, pre-prod, prod) MUST use a remote backend in Azure Storage with:
  - Blob versioning enabled
  - State locking via the storage account's built-in lease
  - Access via Managed Identity — never SAS tokens or storage account keys
  - State container in a dedicated resource group separate from the deployed workload

## Variables

- Use `variable` blocks with `description`, `type`, and `default` where appropriate
- Required variables:
  - `var.app_name` — the service name, used in all resource names
  - `var.environment` — `dev`, `test`, `preprod`, or `prod`. Default `"dev"` only for local development; CI/CD pipelines MUST explicitly pass `-var='environment=...'`
  - `var.team_name` — owner team for tagging
  - `var.cost_centre` — finance allocation tag
- Never parameterise the primary region — hardcode `uksouth`

## Modules

- Reusable patterns (e.g. "web app with private SQL + Key Vault") SHOULD be extracted into modules under `infra/modules/`
- Modules MUST have a README documenting inputs, outputs, and example usage

## Validation in CI

- `terraform fmt -check -recursive` MUST pass
- `terraform validate` MUST pass on every PR
- `tflint` and `checkov` (or equivalent IaC security scanner) MUST run on every PR; high/critical findings MUST block merge
- A `terraform plan` MUST be posted as a PR comment for review before merge
- `terraform apply` to production MUST require a manual approval gate in the workflow
