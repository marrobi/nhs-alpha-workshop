---
applyTo: "**"
---

# UKHSA Security Standards

Security rules for all UKHSA service code, infrastructure, and operational practices. Follows RFC 2119 terminology.

This file restates the locally-enforceable invariants. The authoritative references are:

- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
- [NCSC Cloud Security Principles (14)](https://www.ncsc.gov.uk/collection/cloud)
- [OWASP ASVS](https://owasp.org/www-project-application-security-verification-standard/) and [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- UK GDPR & Data Protection Act 2018, including Article 9 special category data handling

For .NET / ASP.NET Core implementation patterns (middleware, options binding, Data Protection), see `tech-stack.instructions.md`.

---

## Threat Model & Defence in Depth

- Every service MUST have a documented threat model maintained alongside the code. STRIDE is the default framework
- Apply defence in depth: network controls, identity controls, application controls, and data controls MUST each independently enforce least privilege — no single layer is trusted to be sufficient
- Apply zero-trust assumptions: no caller is trusted by virtue of network position alone

## OWASP Baseline

The following classes of issue MUST be addressed before a PR is merged:

- Injection (SQL, command, LDAP, XPath) — always use parameterised queries or LINQ providers
- Broken authentication — no rolling-your-own auth; use Microsoft Entra ID / OpenID Connect via approved libraries
- Sensitive data exposure — TLS 1.2 minimum on all hops; encryption at rest on every data store
- XML External Entities (XXE) — disable external entity resolution in any XML parser used
- Broken access control — verify authorization at every state-changing endpoint, not just at the gateway
- Security misconfiguration — never deploy with default credentials, debug pages, or verbose error responses enabled
- Cross-site scripting (XSS) — Razor encodes by default; `Html.Raw` requires review
- Insecure deserialisation — do not deserialise untrusted binary formats
- Using components with known vulnerabilities — see Dependency Management below
- Insufficient logging & monitoring — see Observability below

## Secrets

- Source code MUST NOT contain secrets, connection strings with credentials, certificates, private keys, or tokens
- Secrets MUST be stored in Azure Key Vault and accessed via User-Assigned Managed Identity
- Local development MAY use `dotnet user-secrets` or `.env` files explicitly excluded from version control
- Pre-commit and CI MUST run secret scanning (gitleaks, GitHub Push Protection, or equivalent). A blocked push MUST be investigated — never bypassed
- A leaked secret MUST be revoked and rotated immediately, regardless of whether the leak was public — assume compromise

## Input Validation

- Validate all user-supplied input at the system boundary using typed request models
- Reject unknown fields by default; do not silently coerce
- Validate against an allowlist where possible; deny-lists are a fallback only
- File uploads MUST be validated by content sniffing, not by extension or `Content-Type` header
- File uploads MUST have a maximum size enforced at the gateway and at the application

## Authentication & Authorization

- Use Microsoft Entra ID (or an approved equivalent) for all user and service authentication
- Tokens MUST be validated server-side on every request — never trust client-side claims
- Authorization MUST be policy-based and testable in isolation from the transport layer
- Privileged actions MUST be logged with the acting principal, timestamp, and CorrelationId

## Logging & PII

- Logs MUST be structured (JSON) and shipped to Application Insights
- PII, special category data (UK GDPR Art. 9), authentication tokens, and session cookies MUST NEVER appear in log messages
- Use redaction / allowlist serialisation when logging objects that might contain PII
- Every log entry MUST include a CorrelationId; every audit-worthy event MUST also include the acting principal
- Audit logs (security-relevant events) MUST be immutable — the application's service account MUST NOT have UPDATE or DELETE permissions on the audit store

## Headers

ASP.NET Core middleware MUST set the following on every response:

| Header | Value |
|---|---|
| `Content-Security-Policy` | `default-src 'self'; ...` (service-specific allowlist) |
| `Strict-Transport-Security` | `max-age=63072000; includeSubDomains; preload` |
| `X-Content-Type-Options` | `nosniff` |
| `X-Frame-Options` | `DENY` (or `SAMEORIGIN` only if required) |
| `Referrer-Policy` | `strict-origin-when-cross-origin` |
| `Permissions-Policy` | service-specific; default-deny |

## CSRF / Antiforgery

- All state-changing routes (POST, PUT, PATCH, DELETE) MUST require an antiforgery token
- API-only endpoints called from non-browser clients MAY use header-based CSRF mitigation (custom header + CORS allowlist) instead

## Transport Security

- TLS 1.2 minimum on every hop, including service-to-service and service-to-data
- HTTP traffic MUST be redirected to HTTPS at the edge and refused by the application
- Certificates MUST be managed via Azure Key Vault or App Service managed certificates — never bundled in container images

## Azure Network & Identity Rules

- **No public endpoints for data services.** Azure SQL, Cosmos DB, Storage, Key Vault MUST be reached only via Azure Private Endpoints
- **No shared access keys.** Disable key-based authentication where the resource supports it (`shared_access_key_enabled = false`, `local_authentication_disabled = true`). All access goes via Managed Identity + RBAC
- **No service principal secrets.** Use User-Assigned Managed Identity for runtime access. GitHub Actions to Azure MUST use OIDC federation, never a long-lived client secret
- **No access policies on Key Vault.** Use RBAC roles (`Key Vault Secrets User`, `Key Vault Crypto User`)
- **Least privilege RBAC.** Each Managed Identity gets only the roles required for the resources it touches (e.g. `Storage Blob Data Reader`, not `Owner`)
- Only the App Service / Container Apps HTTPS endpoint may be publicly reachable; all other traffic flows through the VNet
- NSGs on subnets MUST restrict ingress and egress to the protocols and ports actually required

## Dependency Management

- Pin exact versions via Central Package Management (`Directory.Packages.props`)
- Run `dotnet list package --vulnerable --include-transitive` in CI; fail the build on critical/high CVEs
- Run container image scanning (Trivy or Defender for Containers) on every build; fail on critical findings
- Dependabot or GitHub native updates MUST be enabled for `nuget` and `github-actions`

## Vulnerability Disclosure & Incident Response

- A `security.txt` MUST be published per [RFC 9116](https://www.rfc-editor.org/rfc/rfc9116) for any public-facing service
- Suspected incidents MUST be reported to the UKHSA security function within the timescales agreed in the service's runbook; UK GDPR personal-data breach reporting to the ICO is a 72-hour duty
- Runbooks MUST cover at minimum: credential compromise, data exposure, denial of service, dependency compromise

## Data Protection (UK GDPR)

- A DPIA MUST exist before any service processes personal data; it MUST be updated when scope or risk changes materially
- Article 9 special category data (including health data) requires an explicit lawful basis under both Article 6 and Article 9
- Retention periods MUST be documented and enforced technically — orphan data is a compliance defect
- Data subject rights (access, rectification, erasure where applicable) MUST be operationally supported