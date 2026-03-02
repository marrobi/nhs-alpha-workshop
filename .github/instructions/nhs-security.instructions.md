---
applyTo: "**"
---

# NHS Security Standards

Follow the [OWASP Top 10](https://owasp.org/www-project-top-ten/) for all security decisions. See `tech-stack.instructions.md` for the current stack. The principles below are universal; the implementation section is for the current stack.

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
- Run dependency audit and resolve critical/high vulnerabilities before merging
- Configure automated dependency update tooling

---

## Current Stack Implementation

### Middleware (FastAPI)

- Apply security headers middleware before any route handlers (CSP, HSTS, X-Content-Type-Options, X-Frame-Options)
- Configure strict Content Security Policy: `default-src 'self'`; allowlist only `nhsuk-frontend` CDN assets if used
- Apply `slowapi` rate limiting to all public-facing routes
- Enable CSRF protection on all state-changing routes (POST, PUT, DELETE)

### Secrets (Python / Azure)

- Use `os.environ["VARIABLE_NAME"]` for local development
- Use Azure Key Vault references for production: `@Microsoft.KeyVault(SecretUri=...)`
- Terraform outputs containing secrets must use `sensitive = true`

### Input Validation (FastAPI / React)

- Validate all user input at the route handler boundary using Pydantic models
- Never pass user input to `eval()`, `exec()`, `subprocess.run(shell=True)`, or f-string interpolation in queries
- Never use `dangerouslySetInnerHTML` with user-supplied content in React components

### Dependencies (Python)

- Pin exact versions in `requirements.txt` — no `>=` or `~=` ranges
- Run `pip audit` and resolve critical/high vulnerabilities before merging
- Configure Dependabot for automated security updates
