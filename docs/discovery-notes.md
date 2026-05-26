# Discovery Notes: Constraints, Risks, and Scope Boundaries

Source used: [ImmForm requirements v0.8](../discovery/requirements/ImmForm-Registration-Service-Requirements.md)

## 1. Technical constraints (from requirements)

1. Platform and stack are fixed for alpha:
- .NET 10 LTS with ASP.NET Core MVC.
- GOV.UK component implementation via GovUk.Frontend.AspNetCore (or validated equivalent).
- Azure SQL + EF Core 10 migrations with code first approach.
- nlog + Application Insights for structured logging/tracing.
- Polly-based timeout/retry/circuit breaker on all outbound integrations.

2. Hosting and architecture constraints:
- Stateless app required; horizontal scale via Azure Container Apps.
- Session must be server-side (not in-process), with secure cookie and 60-minute inactivity expiry.
- UK cloud platform assumptions and Azure resource model are embedded in the CI/CD and IaC requirements.

3. Security and secrets constraints:
- TLS 1.2+, HSTS, anti-forgery protection on all POSTs.
- No secrets in source control; Key Vault + Managed Identity mandatory.
- OIDC-based GitHub Actions auth only (no long-lived credentials in repository secrets).
- Public duplicate-check endpoints must implement anti-enumeration controls and rate limiting.

4. Integration constraints:
- ImmForm Organisation API must validate account number and organisation code pair in real time.
- Organisation code validation baseline: NHS ODS code format, typically 1 to 9 alphanumeric characters, with modern randomized 5-character ANANA pattern; non-NHS organisations may use different values but typically same structural format.
- Duplicate detection integration baseline: use an ImmForm mock API in alpha to check whether the applicant already exists as an active user.
- Public journey duplicate-check responses should be non-enumerable (for example allow or block only) and must not expose a list or searchable directory of existing users.
- Any endpoint for viewing existing users must be separate from the public registration journey, restricted to authenticated internal roles, and fully audited.
- ImmForm Registration API is required for automated account creation after approval.
- GOV.UK Notify is mandatory for all journey notifications and approval workflow emails.

5. Workflow and operations constraints:
- Approval link is fixed to 72 hours, max two resends.
- Immutable audit logging is mandatory for all lifecycle events.
- Audit anomaly detection and retention enforcement are required (scheduled + on-demand).
- CI/CD gates require security scans to pass before environment promotion.

## 2. Legislative and assurance constraints (GDPR, GDP)

## 2.1 GDPR and data protection constraints

1. DPIA must be completed and signed off before testing with real data.
2. Data minimisation and explicit shared mailbox blocking are required.
3. Retention periods are policy-driven by account and outcome type (for example wholesaler vs NHS vs rejected/expired/abandoned).
4. Auditability and data integrity controls are required for personal data lifecycle events.

## 2.2 GDP / MHRA quality and compliance constraints

1. System must support MHRA GDP Annex 11 equivalent evidence expectations:
- Immutable, queryable audit trail.
- Detectable data integrity protections (checksum and anomaly detection).
- Exportable evidence for inspection packs.

2. Additional operational controls are required:
- Application account must not be able to UPDATE/DELETE audit log rows.
- Computer system validation artefacts (IQ/OQ/PQ) are required before production release.
- Change control process alignment to UKHSA validation governance is required post go-live.

## 2.3 DCB0129 position

Decision confirmed by service owner: DCB0129 is not required for this UKHSA service and is not a delivery constraint for alpha.

## 3. Riskiest assumptions (ranked)

## High risk assumptions

1. ImmForm API contracts will be ready and stable for production migration from alpha mocks.
2. Organisation API reliably returns a single, current Authorised Person for every valid account pair.
3. Duplicate detection checks required by the service can be fulfilled through available data sources and API endpoints.
4. GOV.UK Notify onboarding, template approval, and delivery performance will complete in time for alpha/beta milestones.
5. Full immutable audit model plus anomaly checks can be implemented without impacting journey performance targets.

## Medium risk assumptions

1. Reusable step-framework abstraction will remain simple while still meeting all current and future onboarding variants.
2. AP resend and expiry journey will be understandable enough to avoid significant helpdesk fallback volume.
3. Existing organisational data quality (account/org/AP mappings) is sufficient for self-service at scale.
4. Duplicate-check endpoint design will prevent user/account enumeration while still giving useful guidance to applicants.

## Lower risk but important assumptions

1. Accessibility testing cadence (automated + manual) can be sustained in each release cycle.
2. Required teams (helpdesk, QA/WDA RP, technical services) can operate new role-based processes from day one.

## 4. Scope boundaries (what will not be built)

The following are explicitly out of scope for this alpha service:

1. Authorised person registration.
2. Account revalidation / reconfirmation journeys.
3. Delivery point address changes.
4. Organisation code changes due to merger of organisational accounts.
5. Billing and invoice changes.
6. New delivery location/account creation.
7. Account deactivation/offboarding.
8. CIS2/NHSmail/federated SSO.
9. Welsh language support.
10. WDA(H) document upload and storage (email fallback remains).
11. Multi-account registration in one journey (one account per journey only).
12. AP record maintenance in ImmForm (must be handled by existing account management process).

## 5. Answered questions folowing a team discussion (not fully resolved by spec)

1. GDPR legal basis and roles:
- What are the agreed Article 6 and (if applicable) Article 9 lawful bases?
- Is UKHSA sole controller for this registration flow, or are any joint-controller arrangements needed?

2. API contract and ownership:
- No existing endpoint/source provides "existing active ImmForm user by email" for duplicate checks. A mock must be created for Alpha.
- Production SLAs for Organisation and Registration APIs are 24/7 365 with expected uptime of 99.95%. Production support hours are Weekdays 09:00 - 17:00.
- Industry standard rate-limit thresholds for duplicate-check requests (for example per IP, per session, and per email plus account tuple)

1. Data retention implementation detail:
- The deactivation date must be captured and used for retention calculations where applicable. Inactive records must be retained for a minimum of 5 years

1. Operational and service design detail:
- CSV as the mandatory FR-24 export format at go-live.
- The Helpdesk team owns anomaly triage and remediation workflow from NFR-22 alerts.

5. Assisted digital fallback:
- The Helpdesk manual user registration runbook features a 5-7day SLA which applies when resend limit is reached or AP record is wrong.
