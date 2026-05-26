output "resource_group_name" {
  description = "The name of the resource group"
  value       = azurerm_resource_group.main.name
}

output "web_app_url" {
  description = "The URL of the web container app"
  value       = "https://${azurerm_container_app.web.ingress[0].fqdn}"
}

output "api_app_url" {
  description = "The URL of the API container app"
  value       = "https://${azurerm_container_app.api.ingress[0].fqdn}"
}

output "application_insights_connection_string" {
  description = "Application Insights connection string"
  value       = azurerm_application_insights.main.connection_string
  sensitive   = true
}

output "key_vault_uri" {
  description = "Key Vault URI"
  value       = azurerm_key_vault.main.vault_uri
  sensitive   = true
}

output "sql_server_fqdn" {
  description = "SQL Server fully qualified domain name"
  value       = azurerm_mssql_server.main.fully_qualified_domain_name
  sensitive   = true
}

output "container_registry_login_server" {
  description = "ACR login server URL"
  value       = azurerm_container_registry.main.login_server
}
