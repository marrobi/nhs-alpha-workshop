---
applyTo: "**"
---

# NHS Security Standards

Follow the [OWASP Top 10](https://owasp.org/www-project-top-ten/) for all security decisions. See `tech-stack.instructions.md` for the current stack. The principles below are universal.

---

## Security Principles (tech-agnostic)

### Secrets

- Never hardcode secrets, API keys, connection strings, or passwords in source code
- Store `.env` files locally only — they must be in `.gitignore`
- No sensitive data in URL query parameters

### Input Validation

- Validate all user input at the API boundary
- Never concatenate user input into SQL queries — use parameterised queries only
- Never pass user input to dynamic code execution or shell commands
- Never render unsanitised user input as raw HTML in the frontend

### Authorization

- Data-access endpoints MUST enforce authorization, not just authentication
- An authenticated user must only access data they are authorised to see
- Authentication confirms identity; authorization confirms permission — both are required
- For alpha/prototype mock auth, document any relaxed authorization as a risk

### Sessions & Cookies

- Session cookies: httponly, secure, samesite strict
- Session secrets loaded from environment variables, never hardcoded

### Logging — PII Rules

- Use structured JSON logging
- **Never log**: NHS numbers, patient names, dates of birth, addresses, postcodes, session tokens, authorization headers
- Include `X-Request-ID` in every log entry for tracing
- Log errors server-side only; never expose stack traces to end users

### Dependencies

- Pin exact dependency versions — no loose ranges for production dependencies
- Docker images must use specific version tags — never use `latest`
- Run dependency audit and resolve critical/high vulnerabilities before merging
- Configure automated dependency update tooling

### Azure Network & Identity

- **No shared access keys** — never use storage account keys, database connection strings with passwords, or shared access signatures (SAS) for service-to-service or service-to-data communication. Use Managed Identity (MSI) with Azure RBAC role assignments instead.
- **No public endpoints for backend services** — databases, storage accounts, Key Vault, and other data-plane services must not be accessible from the public internet. Use Private Endpoints with VNet integration for all service-to-service and service-to-data communication.
- **App Service VNet integration** — the App Service must be integrated into a VNet so it can reach Private Endpoints. Only the App Service's public-facing HTTPS endpoint should be internet-accessible.
- **Least privilege RBAC** — grant Managed Identities only the minimum roles required (e.g. `Storage Blob Data Reader`, `Key Vault Secrets User`, `SQL DB Contributor`). Never assign `Owner` or `Contributor` at the resource group level for data access.
- **Disable local auth where possible** — for Azure resources that support it (Storage, Key Vault, SQL), disable key-based/local authentication and enforce Entra ID-only access.

> See `tech-stack.instructions.md` for framework-specific security implementation details (middleware, secrets, input validation, dependencies).
