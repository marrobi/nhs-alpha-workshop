terraform {
  required_version = ">= 1.5.0"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.14.0"
    }
    time = {
      source  = "hashicorp/time"
      version = "~> 0.11.0"
    }
  }
}

provider "azurerm" {
  features {}
  subscription_id = var.subscription_id
}

# Resource Group
resource "azurerm_resource_group" "main" {
  name     = "rg-${var.app_name}-${var.environment}"
  location = "uksouth"

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# User-Assigned Managed Identity
resource "azurerm_user_assigned_identity" "app" {
  name                = "id-${var.app_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Virtual Network
resource "azurerm_virtual_network" "main" {
  name                = "vnet-${var.app_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  address_space       = ["10.0.0.0/16"]

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Subnet for Container Apps
resource "azurerm_subnet" "container_apps" {
  name                 = "snet-container-apps"
  resource_group_name  = azurerm_resource_group.main.name
  virtual_network_name = azurerm_virtual_network.main.name
  address_prefixes     = ["10.0.1.0/24"]

  delegation {
    name = "container-apps-delegation"
    service_delegation {
      name    = "Microsoft.App/environments"
      actions = ["Microsoft.Network/virtualNetworks/subnets/join/action"]
    }
  }
}

# Subnet for Private Endpoints
resource "azurerm_subnet" "private_endpoints" {
  name                 = "snet-private-endpoints"
  resource_group_name  = azurerm_resource_group.main.name
  virtual_network_name = azurerm_virtual_network.main.name
  address_prefixes     = ["10.0.2.0/24"]
}

# Log Analytics Workspace (required for Container Apps and Application Insights)
resource "azurerm_log_analytics_workspace" "main" {
  name                = "law-${var.app_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  sku                 = "PerGB2018"
  retention_in_days   = 90

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Application Insights
resource "azurerm_application_insights" "main" {
  name                = "ai-${var.app_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  workspace_id        = azurerm_log_analytics_workspace.main.id
  application_type    = "web"

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Key Vault
resource "azurerm_key_vault" "main" {
  name                       = "kv-${var.app_name}-${var.environment}"
  resource_group_name        = azurerm_resource_group.main.name
  location                   = azurerm_resource_group.main.location
  tenant_id                  = data.azurerm_client_config.current.tenant_id
  sku_name                   = "standard"
  purge_protection_enabled   = true
  enable_rbac_authorization  = true
  public_network_access_enabled = false

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Key Vault Private Endpoint
resource "azurerm_private_endpoint" "key_vault" {
  name                = "pe-kv-${var.app_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  subnet_id           = azurerm_subnet.private_endpoints.id

  private_service_connection {
    name                           = "psc-kv-${var.app_name}-${var.environment}"
    private_connection_resource_id = azurerm_key_vault.main.id
    is_manual_connection           = false
    subresource_names              = ["vault"]
  }

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Key Vault Secrets User role for Managed Identity
resource "azurerm_role_assignment" "kv_secrets_user" {
  scope                = azurerm_key_vault.main.id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = azurerm_user_assigned_identity.app.principal_id
}

# Azure SQL Server
resource "azurerm_mssql_server" "main" {
  name                          = "sql-${var.app_name}-${var.environment}"
  resource_group_name           = azurerm_resource_group.main.name
  location                      = azurerm_resource_group.main.location
  version                       = "12.0"
  minimum_tls_version           = "1.2"
  public_network_access_enabled = false

  azuread_administrator {
    login_username              = azurerm_user_assigned_identity.app.name
    object_id                   = azurerm_user_assigned_identity.app.principal_id
    azuread_authentication_only = true
  }

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Azure SQL Database
resource "azurerm_mssql_database" "main" {
  name      = "sqldb-${var.app_name}-${var.environment}"
  server_id = azurerm_mssql_server.main.id
  sku_name  = "S0"

  transparent_data_encryption_enabled = true

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# SQL Server Private Endpoint (in same VNet subnet as other PEs — uksouth)
# Note: Cross-region private endpoints are supported for Azure SQL
resource "azurerm_private_endpoint" "sql" {
  name                = "pe-sql-${var.app_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  subnet_id           = azurerm_subnet.private_endpoints.id

  private_service_connection {
    name                           = "psc-sql-${var.app_name}-${var.environment}"
    private_connection_resource_id = azurerm_mssql_server.main.id
    is_manual_connection           = false
    subresource_names              = ["sqlServer"]
  }

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Azure Cache for Redis
resource "azurerm_redis_cache" "main" {
  name                          = "redis-${var.app_name}-${var.environment}"
  resource_group_name           = azurerm_resource_group.main.name
  location                      = azurerm_resource_group.main.location
  capacity                      = 0
  family                        = "C"
  sku_name                      = "Basic"
  minimum_tls_version           = "1.2"
  public_network_access_enabled = false

  redis_configuration {}

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Redis Private Endpoint
resource "azurerm_private_endpoint" "redis" {
  name                = "pe-redis-${var.app_name}-${var.environment}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  subnet_id           = azurerm_subnet.private_endpoints.id

  private_service_connection {
    name                           = "psc-redis-${var.app_name}-${var.environment}"
    private_connection_resource_id = azurerm_redis_cache.main.id
    is_manual_connection           = false
    subresource_names              = ["redisCache"]
  }

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Container Registry
resource "azurerm_container_registry" "main" {
  name                          = "cr${var.app_name}${var.environment}"
  resource_group_name           = azurerm_resource_group.main.name
  location                      = azurerm_resource_group.main.location
  sku                           = "Basic"
  admin_enabled                 = false
  public_network_access_enabled = true # Required for CI/CD image push in alpha

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# ACR Pull role for Managed Identity
resource "azurerm_role_assignment" "acr_pull" {
  scope                = azurerm_container_registry.main.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_user_assigned_identity.app.principal_id
}

# Wait for RBAC propagation before container apps pull from ACR
resource "time_sleep" "wait_for_rbac" {
  depends_on      = [azurerm_role_assignment.acr_pull]
  create_duration = "90s"
}

# Container Apps Environment
resource "azurerm_container_app_environment" "main" {
  name                       = "cae-${var.app_name}-${var.environment}"
  resource_group_name        = azurerm_resource_group.main.name
  location                   = azurerm_resource_group.main.location
  log_analytics_workspace_id = azurerm_log_analytics_workspace.main.id
  infrastructure_subnet_id   = azurerm_subnet.container_apps.id

  tags = {
    project     = var.app_name
    environment = var.environment
  }

  lifecycle {
    ignore_changes = [infrastructure_resource_group_name]
  }
}

# Container App — Web (MVC form UI)
resource "azurerm_container_app" "web" {
  name                         = "ca-${var.app_name}-web-${var.environment}"
  container_app_environment_id = azurerm_container_app_environment.main.id
  resource_group_name          = azurerm_resource_group.main.name
  revision_mode                = "Single"
  workload_profile_name        = "Consumption"

  depends_on = [time_sleep.wait_for_rbac]

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.app.id]
  }

  registry {
    server   = azurerm_container_registry.main.login_server
    identity = azurerm_user_assigned_identity.app.id
  }

  template {
    container {
      name   = "immform-web"
      image  = "${azurerm_container_registry.main.login_server}/${var.app_name}-web:${var.container_image_tag}"
      cpu    = 0.5
      memory = "1Gi"

      env {
        name  = "APPLICATIONINSIGHTS_CONNECTION_STRING"
        value = azurerm_application_insights.main.connection_string
      }

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = var.environment == "prod" ? "Production" : "Development"
      }
    }

    min_replicas = 1
    max_replicas = 3
  }

  ingress {
    external_enabled = true
    target_port      = 8080
    transport        = "http"

    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Container App — API
resource "azurerm_container_app" "api" {
  name                         = "ca-${var.app_name}-api-${var.environment}"
  container_app_environment_id = azurerm_container_app_environment.main.id
  resource_group_name          = azurerm_resource_group.main.name
  revision_mode                = "Single"
  workload_profile_name        = "Consumption"

  depends_on = [time_sleep.wait_for_rbac]

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.app.id]
  }

  registry {
    server   = azurerm_container_registry.main.login_server
    identity = azurerm_user_assigned_identity.app.id
  }

  template {
    container {
      name   = "immform-api"
      image  = "${azurerm_container_registry.main.login_server}/${var.app_name}-api:${var.container_image_tag}"
      cpu    = 0.5
      memory = "1Gi"

      env {
        name  = "APPLICATIONINSIGHTS_CONNECTION_STRING"
        value = azurerm_application_insights.main.connection_string
      }

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = var.environment == "prod" ? "Production" : "Development"
      }
    }

    min_replicas = 1
    max_replicas = 3
  }

  ingress {
    external_enabled = true
    target_port      = 8080
    transport        = "http"

    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }

  tags = {
    project     = var.app_name
    environment = var.environment
  }
}

# Data source for current Azure client config
data "azurerm_client_config" "current" {}
