## Security Hardening

### Goal
Audit and harden the service against OWASP Top 10 vulnerabilities.

### Acceptance Criteria
- [ ] Security headers middleware returns CSP, HSTS, X-Content-Type-Options, X-Frame-Options on all responses
- [ ] Rate limiting (slowapi) applied to all public endpoints
- [ ] All user input validated with Pydantic models
- [ ] No secrets, API keys, or connection strings in source code
- [ ] `.env` files listed in `.gitignore`
- [ ] `pip audit` shows no critical or high vulnerabilities
- [ ] Structured JSON logging with no PII (NHS numbers, names, DOB, addresses)
- [ ] Security review report generated at `docs/security-review.md`
