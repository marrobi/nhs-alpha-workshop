# Discovery Notes: Constraints, Risks, and Scope Boundaries

Source used: [ImmForm requirements v0.7](../discovery/requirements/ImmForm-Registration-Service-Requirements-v07.md)

## 1. Technical constraints (from requirements)

1. Platform and stack are fixed for alpha:
- .NET 10 LTS with ASP.NET Core MVC.
- GOV.UK component implementation via GovUk.Frontend.AspNetCore (or validated equivalent).
- Azure SQL + EF Core 10 for persistence.
- nlog + Application Insights for structured logging/tracing.
- Polly-based timeout/retry/circuit breaker on all outbound integrations.

2. Hosting and architecture constraints:
- Stateless app required; horizontal scale via Azure Container Apps or AKS.
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
- CI/CD gates require security scans (Trivy/CodeQL/etc.) to pass before environment promotion.

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

1. Container Apps vs AKS decision will be made early enough not to delay environment setup and observability design.
2. Reusable step-framework abstraction will remain simple while still meeting all current and future onboarding variants.
3. AP resend and expiry journey will be understandable enough to avoid significant helpdesk fallback volume.
4. Existing organisational data quality (account/org/AP mappings) is sufficient for self-service at scale.
5. Duplicate-check endpoint design will prevent user/account enumeration while still giving useful guidance to applicants.

## Lower risk but important assumptions

1. Accessibility testing cadence (automated + manual) can be sustained in each release cycle.
2. Required teams (helpdesk, QA/WDA RP, technical services) can operate new role-based processes from day one.

## 4. Scope boundaries (what will not be built)

The following are explicitly out of scope for this alpha service:

1. Authorised person registration.
2. Account revalidation / reconfirmation journeys.
3. Delivery point address changes.
4. Organisation code changes due to merger.
5. Billing and invoice changes.
6. New delivery location/account creation.
7. Account deactivation/offboarding.
8. CIS2/NHSmail/federated SSO.
9. Welsh language support.
10. WDA(H) document upload and storage (email fallback remains).
11. Multi-account registration in one journey (one account per journey only).
12. AP record maintenance in ImmForm (must be handled by existing account management process).

## 5. Open questions for team discussion (not fully resolved by spec)

1. GDPR legal basis and roles:
- What are the agreed Article 6 and (if applicable) Article 9 lawful bases?
- Is UKHSA sole controller for this registration flow, or are any joint-controller arrangements needed?

2. API contract and ownership:
- What endpoint/source provides "existing active ImmForm user by email" for duplicate checks?
- What are production SLAs and support hours for Organisation and Registration APIs?
- What are agreed rate-limit thresholds for duplicate-check requests (for example per IP, per session, and per email plus account tuple)?

3. Data retention implementation detail:
- How will deactivation date be sourced for retention calculations where required?
- How will wholesaler vs non-wholesaler account type be determined reliably at runtime?

4. Operational and service design detail:
- Container Apps or AKS for v1?
- PDF or CSV as the mandatory FR-24 export format at go-live (or both)?
- Which team owns anomaly triage and remediation workflow from NFR-22 alerts?

5. Assisted digital fallback:
- What helpdesk runbook and SLA applies when resend limit is reached or AP record is wrong?

## 6. Suggested immediate decisions

1. Freeze API contracts for Organisation/Registration and duplicate detection.
2. Decide runtime target (Container Apps vs AKS) and FR-24 export format.
3. Agree alert ownership and helpdesk operational model for expired/failed approval flows.
