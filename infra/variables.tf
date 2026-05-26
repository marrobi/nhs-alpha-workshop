variable "subscription_id" {
  description = "Azure subscription ID"
  type        = string
}

variable "app_name" {
  description = "The name of the application, used in all resource names"
  type        = string
  default     = "immform"
}

variable "environment" {
  description = "The deployment environment (dev, staging, prod). CI/CD must explicitly pass this — never rely on the default for non-development deployments."
  type        = string
  default     = "dev"
}

variable "sql_admin_login" {
  description = "SQL Server administrator login name"
  type        = string
  sensitive   = true
}

variable "sql_admin_password" {
  description = "SQL Server administrator password"
  type        = string
  sensitive   = true
}

variable "container_image_tag" {
  description = "Docker image tag for the container apps"
  type        = string
  default     = "latest"
}
