---
applyTo: "**/infra/**,**/*.tf"
---

# Terraform — Azure NHS Infrastructure

Refer to the [Terraform Azure Provider docs](https://registry.terraform.io/providers/hashicorp/azurerm/latest/docs) for resource reference.

## Provider

- Use `azurerm` provider with `features {}` block
- Pin provider version in `required_providers`
- Use `uksouth` region for all resources — data sovereignty requirement

## Naming Convention

All resource names must include `var.app_name` so multiple Alpha services can coexist in the same Azure subscription.

- Resource Group: `rg-${var.app_name}-${var.environment}`
- App Service Plan: `asp-${var.app_name}-${var.environment}`
- Web App: `app-${var.app_name}-${var.environment}`
- Key Vault: `kv-${var.app_name}-${var.environment}`
- Application Insights: `ai-${var.app_name}-${var.environment}`
- Tag all resources: `project = var.app_name`, `environment = var.environment`

## Identity & Secrets

- Use `azurerm_user_assigned_identity` — never service principal client secrets
- **Never use shared access keys** for storage accounts, databases, or any Azure service — use Managed Identity with RBAC role assignments
- Grant Key Vault access via RBAC (`Key Vault Secrets User` role) — not access policies
- Reference secrets in App Service via `@Microsoft.KeyVault(SecretUri=...)`
- Mark sensitive outputs with `sensitive = true`
- Assign least-privilege RBAC roles to the Managed Identity for each resource (e.g. `Storage Blob Data Contributor`, `SQL DB Contributor`)
- Where supported, disable local/key-based authentication on Azure resources (e.g. `shared_access_key_enabled = false` on storage accounts)

## Network Isolation

- **No public endpoints for data services** — databases, storage accounts, Key Vault, and other backend services must use Private Endpoints
- Create a VNet with at least two subnets: one for App Service VNet integration, one for Private Endpoints
- Use `azurerm_private_endpoint` for each data service and register Private DNS zones
- Configure `azurerm_app_service_virtual_network_swift_connection` (or `virtual_network_subnet_id` on the web app) for outbound VNet integration
- Only the App Service HTTPS endpoint should be publicly accessible — all service-to-service and service-to-data traffic routes through the VNet
- Use Network Security Groups (NSGs) on subnets to restrict traffic to required ports and protocols

## App Service

- `azurerm_linux_web_app` with Python 3.12 runtime stack
- Set `https_only = true`
- Set minimum TLS version to 1.2
- Configure startup command: `uvicorn app.main:app --host 0.0.0.0 --port 8000`
- Enable Application Insights via `app_settings`

## State

- Local state for workshop development
- Document migration to Azure Storage backend for production

## Variables

- Use `variable` blocks with `description`, `type`, and `default` where appropriate
- Required variable: `var.app_name` — the service name, used in all resource names
- Environment variable: `var.environment` (default: `"dev"`)
- Never parameterise the region — hardcode `uksouth`
