# ADR-0001: ImmForm New User Registration — System Architecture

**Status**: Accepted

**Date**: 2026-05-26

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

ImmForm is a nationally critical UKHSA platform for immunisation programme management and vaccine supply chain operations. The existing new user registration process is entirely manual: a PDF form emailed to the helpdesk, manually validated, re-keyed, and chased via email. This creates 5-working-day lead times, data quality errors, fragmented audit trails, and scalability bottlenecks during programme mobilisation.

This ADR documents the agreed architecture for a GDS-compliant, self-service digital registration service that replaces the manual process with automated validation, time-bound approval routing, and an immutable audit trail meeting MHRA GDP requirements.

The service registers **orderers** on existing ImmForm accounts. It does not hold patient records — NHS Number validation (ISB 0149) does not apply.

## Decision

### Tech Stack

| Concern | Choice |
|---|---|
| Backend (form UI) | .NET 10 / ASP.NET Core MVC / Razor views / `GovUk.Frontend.AspNetCore` |
| Backend (real APIs) | .NET 10 / ASP.NET Core Web API |
| Backend (alpha mocks) | .NET 10 / ASP.NET Core Minimal API — same solution, not deployed to production |
| Design System | `GovUk.Frontend.AspNetCore` NuGet package |
| Database | Azure SQL + EF Core 10 (single DB, two schemas — see below) |
| Session state | Azure Cache for Redis |
| HTTP resilience | Polly (retry, circuit breaker, 5-second timeout) |
| Notifications | GOV.UK Notify — stubbed in alpha (logged to console/audit, not sent) |
| Authentication | Azure AD / Entra ID with role claims (`ImmFormAdmin`, `ImmFormQaRp`) |
| Testing | NUnit + `WebApplicationFactory<Program>` + `HttpClient` |
| E2E testing | Playwright (.NET) + axe-core |
| Performance testing | k6 (JavaScript) |
| IaC | Terraform (`azurerm` provider) |
| Hosting | Azure Container Apps (UK South) |
| Secrets | Azure Key Vault via Managed Identity |
| Monitoring | Azure Application Insights |
| CI/CD | GitHub Actions |

### Project Structure

```
src/
├── ImmForm.Web/            # ASP.NET Core MVC — multi-step registration form UI
├── ImmForm.Api/            # ASP.NET Core Web API — admin dashboard, audit, AP decision
└── ImmForm.Mocks/          # ASP.NET Core Minimal API — ImmForm Organisation & Registration API stubs
```

### Data Model

#### Registration (dbo schema)

| Field | Type | Notes |
|---|---|---|
| Id | Guid (PK) | |
| CorrelationId | string (unique) | Used in all emails and logs |
| FirstName | string (max 100) | |
| Surname | string (max 100) | |
| JobTitle | string (max 100) | |
| Telephone | string (max 20) | UK format |
| Email | string (max 254) | Individual email only — shared mailboxes rejected |
| AccountNumber | string (10) | 10-digit ImmForm account number |
| OrganisationCode | string | ImmForm organisation code |
| OrganisationName | string | Pre-filled from Organisation API |
| AuthorisedPersonName | string | From Organisation API |
| AuthorisedPersonEmail | string | From Organisation API |
| Status | enum | Draft, Submitted, AwaitingApproval, Approved, Rejected, Expired, AccountCreated, Qualified, QualificationRejected |
| DeclarationFullName | string | Captured at declaration step |
| DeclarationJobTitle | string | Captured at declaration step |
| DeclarationTimestamp | DateTimeOffset | UTC |
| SubmittedAt | DateTimeOffset? | |
| ApprovedAt | DateTimeOffset? | |
| RejectedAt | DateTimeOffset? | |
| ActivatedAt | DateTimeOffset? | |
| RejectionReason | string? | Mandatory free-text on rejection |
| ResendCount | int | Max 2 resends |
| PayloadChecksum | string | SHA-256 of submission payload (NFR-14) |
| CreatedAt | DateTimeOffset | |
| UpdatedAt | DateTimeOffset | |

#### ApprovalToken (dbo schema)

| Field | Type | Notes |
|---|---|---|
| Id | Guid (PK) | |
| RegistrationId | Guid (FK) | |
| Token | string (unique) | Cryptographically random, opaque |
| ExpiresAt | DateTimeOffset | 72 hours from creation |
| UsedAt | DateTimeOffset? | Set when AP uses the token |
| IsUsed | bool | Single-use enforcement |

#### AuditLog (audit schema — INSERT-only)

| Field | Type | Notes |
|---|---|---|
| Id | long (PK, identity) | |
| RegistrationId | Guid | |
| CorrelationId | string | |
| EventType | string | EVT-01 through EVT-19 per requirements |
| Timestamp | DateTimeOffset | UTC |
| ActorType | string | System, Applicant, Manager, Admin, QaRp |
| ActorId | string | Email or system identifier |
| PreviousState | string? | |
| NewState | string? | |
| Detail | string? | JSON — event-specific data |
| HashedIPAddress | string? | SHA-256 hashed |

The application service account has INSERT-only permission on the `audit` schema. No DELETE or UPDATE — this satisfies MHRA GDP NFR-13.

#### NotifyLog (audit schema — INSERT-only)

| Field | Type | Notes |
|---|---|---|
| Id | long (PK, identity) | |
| RegistrationId | Guid | |
| CorrelationId | string | |
| TemplateId | string | GOV.UK Notify template reference |
| RecipientType | string | Applicant, AuthorisedPerson, Helpdesk |
| DispatchTimestamp | DateTimeOffset | UTC |
| Status | string | Logged (alpha) / Sent / Failed |

### API Endpoints

#### Registration Form (ImmForm.Web — MVC)

| Method | Route | Purpose |
|---|---|---|
| GET | `/` | Start page |
| GET | `/register/applicant-details` | Applicant details form step |
| POST | `/register/applicant-details` | Submit applicant details |
| GET | `/register/organisation-account` | Organisation and account form step |
| POST | `/register/organisation-account` | Submit org/account — triggers Org API validation |
| GET | `/register/check-your-answers` | Check your answers summary |
| GET | `/register/declaration` | Declaration page |
| POST | `/register/declaration` | Submit declaration — triggers duplicate check, creates registration, sends AP notification |
| GET | `/register/confirmation/{correlationId}` | Confirmation page with reference number |
| GET | `/register/resend-approval/{correlationId}` | Resend approval request page |
| POST | `/register/resend-approval/{correlationId}` | Submit resend request |

#### AP Decision (ImmForm.Api — Web API, unauthenticated)

| Method | Route | Purpose |
|---|---|---|
| GET | `/api/approval/{token}` | Show AP decision page with applicant summary |
| POST | `/api/approval/{token}/approve` | AP approves registration |
| POST | `/api/approval/{token}/reject` | AP rejects with mandatory reason |

#### Admin Dashboard (ImmForm.Api — Web API, Entra ID `ImmFormAdmin` role)

| Method | Route | Purpose |
|---|---|---|
| GET | `/api/admin/registrations` | List registrations with status/date filters |
| GET | `/api/admin/registrations/{id}` | Registration detail with event timeline |
| POST | `/api/admin/registrations/{id}/qualify` | Admin qualification decision (EVT-13/14) |
| POST | `/api/admin/registrations/{id}/pricelist` | Assign pricelist access (EVT-15) |
| POST | `/api/admin/registrations/{id}/override` | Manual override with mandatory reason (EVT-16) |

#### Audit Interface (ImmForm.Api — Web API, Entra ID `ImmFormQaRp` role)

| Method | Route | Purpose |
|---|---|---|
| GET | `/api/audit/registrations` | Search by name, account, org code, state, date range |
| GET | `/api/audit/registrations/{id}/timeline` | Full chronological event chain with actor attribution |
| GET | `/api/audit/registrations/{id}/export` | Export structured evidence package (CSV) |
| GET | `/api/audit/anomalies` | List audit integrity anomalies (NFR-22) |

#### Health

| Method | Route | Purpose |
|---|---|---|
| GET | `/health` | Returns `{"status": "ok"}` with 200 |

#### Mock APIs (ImmForm.Mocks — Minimal API, alpha only)

| Method | Route | Purpose |
|---|---|---|
| POST | `/api/mock/organisation/validate` | Validate account/org code pair, return AP details |
| POST | `/api/mock/registration/create` | Create user account in mock ImmForm |
| GET | `/api/mock/registration/check-duplicate` | Check for active/pending duplicates by email |

Mock APIs implement configurable failure states: API unavailability, no AP found, invalid pair, duplicate detected, registration error, timeout. Seeded with synthetic test data.

### Frontend Pages (GOV.UK Design System)

**Applicant journey (unauthenticated):**

| Page | GDS Components | Journey Step |
|---|---|---|
| Start page | `govuk-panel`, `govuk-button` (Start now), `govuk-list` (What you'll need) | Entry point |
| Applicant details | `govuk-input` × 5 (name, surname, job title, telephone, email), `govuk-error-summary` | Step 1 |
| Organisation and account | `govuk-input` × 2 (account number, org code), inline validation result | Step 2 |
| Check your answers | `govuk-summary-list` with Change links per field, grouped by section | Step 3 |
| Declaration | `govuk-checkboxes` (mandatory confirmation), `govuk-input` × 2 (full name, job title) | Step 4 |
| Confirmation | `govuk-panel` with CorrelationId, processing time estimate | Step 5 |
| Session expired | `govuk-panel` with restart link | Error state |
| Resend approval | Resend count display, `govuk-button`, helpdesk fallback after limit | Post-submission |

**AP decision (unauthenticated, token-gated):**

| Page | GDS Components | Purpose |
|---|---|---|
| AP decision | Applicant summary, `govuk-button` (Approve), `govuk-button` (Reject), `govuk-textarea` (reason) | AP action |
| AP confirmation | `govuk-panel` with decision outcome | Post-decision |
| Token expired/invalid | Error page with helpdesk contact | Error state |

**Admin dashboard (Entra ID authenticated, `ImmFormAdmin` role):**

| Page | Purpose |
|---|---|
| Registration list | Filterable queue with status, age, SLA position |
| Registration detail | Full application data, chronological event timeline, action buttons |
| Qualification decision | Approve/reject qualification with reason |
| Pricelist assignment | Select and confirm pricelist access |

**Audit interface (Entra ID authenticated, `ImmFormQaRp` role):**

| Page | Purpose |
|---|---|
| Audit search | Search by applicant name, account number, org code, date range, state |
| Audit timeline | Immutable chronological event chain with actor attribution |
| Evidence export | Generate and download structured CSV evidence package |
| Anomaly list | Flagged records (checksum mismatch, incomplete sequences, missing reasons) |

### Infrastructure Components

| Resource | Purpose | Configuration |
|---|---|---|
| Azure Container Apps (UK South) | Hosts ImmForm.Web and ImmForm.Api | VNet-integrated, HTTPS ingress on port 443 |
| Azure SQL Database (UK South) | Registration data (dbo) + audit log (audit) | TDE encryption, two SQL users with different permissions |
| Azure Cache for Redis (UK South) | Server-side session state | Private Endpoint, TLS |
| Azure Key Vault (UK South) | Secrets (DB connection string, Notify API key, Entra client ID) | Private Endpoint, Managed Identity access |
| Azure Container Registry | Docker images for Web, Api, Mocks | Private Endpoint |
| Azure Application Insights | Distributed tracing, structured logging, performance monitoring | Connected to both Web and Api |
| Azure Monitor | Alerts: API failures, approval timeouts (72h), error rate spikes | Alerts to ImmForm helpdesk Teams channel |
| Entra ID | Authentication for Admin and QA/WDA RP roles | App registration with role claims |
| Azure Function (scheduled) | Data retention cleanup job (NFR-17, NFR-23) | Runs on schedule per retention policy |

### Key Architectural Decisions

#### 1. Audit Log Isolation — Single DB, Two Schemas

Single Azure SQL database with `dbo` schema (full CRUD) and `audit` schema (INSERT-only). Two EF Core DbContext instances use different SQL users: one with full permissions on `dbo`, one with INSERT-only on `audit`. This satisfies MHRA GDP NFR-13 (application cannot modify or delete audit records) while keeping infrastructure simple for alpha.

#### 2. Authentication — Entra ID with Role Claims

Applicants are unauthenticated public users. Admin and QA/WDA RP users authenticate via Entra ID. Role claims (`ImmFormAdmin`, `ImmFormQaRp`) control access. The AP decision flow uses opaque database-backed tokens (no login required). This approach is production-ready, supports future federated identity (CIS2/NHSmail), and tests the real auth integration in alpha.

#### 3. Session State — Azure Cache for Redis

Server-side session state stored in Redis via `Microsoft.Extensions.Caching.StackExchangeRedis`. Session cookie is HttpOnly, Secure, SameSite=Strict. 60-minute inactivity timeout with server-side purge. Redis provides fast session reads/writes and supports horizontal scaling of Container Apps instances.

#### 4. GOV.UK Notify — Stubbed in Alpha

GOV.UK Notify integration is stubbed: email content is logged to the console and written to the NotifyLog table in the audit schema, but emails are not sent via the Notify API. The notification service interface (`INotificationService`) is the same — swapping to real Notify requires only a configuration change and API key. This avoids an external dependency during alpha while still exercising the full notification pipeline in code.

#### 5. Approval Tokens — Database-Backed One-Time Tokens

AP approval links use a cryptographically random opaque token stored in the database with a 72-hour expiry. Tokens are single-use: the `IsUsed` flag is set atomically when the AP submits their decision. The AP lands on a confirmation page showing applicant details before approving or rejecting. This approach ensures: tokens contain no data in the URL, single-use is enforced by the database, and expiry is validated server-side at decision time.

#### 6. ImmForm API Mocks — Contract-Faithful with Failure States

Both the ImmForm Organisation API and Registration API are mocked as ASP.NET Core Minimal API projects (`ImmForm.Mocks`). Mocks implement the expected request/response contracts including configurable failure states: API unavailability (503), no AP found, invalid account/org pair, duplicate email, registration API error, and timeout. Seeded with synthetic test data representing NHS sites, wholesalers, immunoglobulin holding centres, and sexual health services. Not deployed to production.

### Registration Lifecycle Events

All 19 events (EVT-01 through EVT-19) from the requirements specification are written to the immutable AuditLog table. Key events:

| Event | Trigger | Actor |
|---|---|---|
| EVT-01 | Session started | Applicant |
| EVT-02 | Submission received | Applicant |
| EVT-03 | Duplicate email blocked | System |
| EVT-04 | Account/org validation failed | System |
| EVT-05 | Session abandoned (60min timeout) | System |
| EVT-06 | AP approval email sent | System |
| EVT-07 | AP approved | Manager |
| EVT-08 | AP rejected (with reason) | Manager |
| EVT-09 | Approval link expired (72h) | System |
| EVT-10 | Resend attempt 1 | Applicant |
| EVT-11 | Resend attempt 2 | Applicant |
| EVT-12 | Resend limit reached | System |
| EVT-13 | Admin qualification approved | Admin |
| EVT-14 | Admin qualification rejected | Admin |
| EVT-15 | Admin pricelist assigned | Admin |
| EVT-16 | Admin manual override | Admin |
| EVT-17 | Registration API call made | System |
| EVT-18 | Registration API call failed | System |
| EVT-19 | Account creation confirmed | System |

### Data Retention Policy (NFR-23)

| Account Type | Retention Period |
|---|---|
| Wholesaler (WDA(H)) | 5 years from activation/deactivation |
| NHS site (all programmes) | 3 years from activation/deactivation |
| Rejected applications | 2 years from rejection |
| Expired applications | 2 years from expiry |
| Abandoned sessions (EVT-05) | 6 months |

Enforced by a scheduled Azure Function cleanup job. Deletion events are recorded in the audit log with retention basis stated.

## User Journey Priority Order

Prioritised by **riskiest assumption** — the thing that must work for the service to be viable.

### Wave 1 — Core Happy Path (build first)

| # | Journey | Riskiest Assumption Tested |
|---|---|---|
| 1 | NHS new starter registration | Baseline end-to-end flow — every other journey depends on this |
| 2 | Account validation failure | Organisation API integration — **#1 riskiest assumption** |
| 3 | Field validation error correction | GDS error handling pattern — affects every form step |
| 4 | Duplicate detection error | Pre-submit security checks — anti-enumeration controls |
| 5 | Authorised Person approval | AP decision flow — every registration terminates here |
| 6 | Account creation API execution | Closes the automation loop — Polly retry, EVT-17/18/19 |

### Wave 2 — Exception and Time-Bound Paths

| # | Journey | Why Here |
|---|---|---|
| 7 | Approval resend workflow | 72h expiry + 2-resend cap — **#2 riskiest assumption** (compliance) |
| 8 | Registration rejection outcome | AP rejections with mandatory reason — unhappy path closure |
| 9 | Fallback case resolution | Helpdesk intervention within role constraints — safety net |
| 10 | Session timeout/abandonment | Server-side session purge — security requirement (NFR-09) |

### Wave 3 — Operational and Compliance

| # | Journey | Why Here |
|---|---|---|
| 11 | Admin qualification review | Admin controls (EVT-13–16) — depends on stable audit trail |
| 12 | Audit evidence retrieval | QA/WDA RP read-only interface — needs data to exist first |

### Wave 4 — Persona and Context Variants

| # | Journey | Key Risk Tested |
|---|---|---|
| 13 | COVID-19 programme registration | Individual email enforcement; ordering deadline pressure |
| 14 | Mpox mobile registration | Mobile layout; partial-save resume |
| 15 | GBSM sexual health registration | Dual-org account ownership ambiguity |
| 16 | Occupational health/private registration | Private account number scoping |
| 17 | Non-NHS wholesaler registration | Shared mailbox detection; structured audit for GDP |
| 18 | Holding centre critical supply registration | Patient safety — high-consequence AP routing failure |

## Consequences

### Positive
- Reduces mean activation time from 5 working days to target of 2 working days
- Removes manual helpdesk re-keying from the standard registration pathway
- Every state transition captured in an immutable, MHRA GDP-compliant audit trail
- Error-loop rate reduced by early account/organisation code validation
- Audit trail independently retrievable by QA/WDA RP without helpdesk involvement
- Reusable multi-step form framework for future UKHSA onboarding services

### Negative
- Redis adds an infrastructure component and cost compared to SQL-backed sessions
- Entra ID requires tenant configuration and app registration setup
- Stubbing Notify means the email delivery flow is not tested end-to-end in alpha
- Contract-faithful mocks require upfront investment in synthetic data and failure state configuration

### Risks
- If the real ImmForm Organisation API contract differs from the mocked version, integration work in beta will be significant
- AP approval behaviour against the 72-hour policy is untested with real users — may need adjustment
- Entra ID configuration may require UKHSA tenant admin involvement, which could delay admin dashboard testing

## Alternatives Considered

### Two Azure SQL Databases for Audit Isolation
- **Pros**: Strongest physical separation of audit data
- **Cons**: Higher cost, more complex Terraform, cross-database queries for admin views
- **Why rejected**: SQL-level permission separation is auditable and sufficient for alpha; upgrade path to separate DB is straightforward

### Cookie-Based Auth with Local User Store
- **Pros**: Simpler setup, no Entra ID dependency
- **Cons**: Custom auth system will be replaced; password management is a security liability
- **Why rejected**: Auth for admin/compliance roles is a riskiest assumption; must test real integration

### Azure SQL for Session State
- **Pros**: No additional infrastructure; reuses existing DB
- **Cons**: Higher latency per request for session reads
- **Why rejected**: User chose Redis for performance; session operations are frequent and latency-sensitive

### Real GOV.UK Notify in Alpha
- **Pros**: Tests full email delivery pipeline; validates AP email flow end-to-end
- **Cons**: Requires Notify API key and service registration; external dependency
- **Why rejected**: User chose to stub Notify; the notification interface is identical — switching to real Notify is a configuration change

### HMAC-Signed URL Tokens for AP Approval
- **Pros**: Stateless verification; no database lookup
- **Cons**: Registration data visible in URL; single-use harder to enforce without DB
- **Why rejected**: Database-backed tokens are more robust for compliance-critical flow; opaque tokens with server-side validation

## UKHSA Constraints

- **Data sovereignty**: All data stored in Azure UK South. Azure SQL, Redis, Key Vault, and Container Apps all deployed to UK South region.
- **MHRA GDP compliance**: Immutable audit trail with named-individual attribution at every state transition. Application service account has no DELETE or UPDATE on audit tables. SHA-256 payload checksums. 5-year retention for wholesaler records.
- **GOV.UK Design System**: All user-facing pages use `GovUk.Frontend.AspNetCore` tag helpers. WCAG 2.2 Level AA mandatory. GDS error patterns enforced on all form steps.
- **No real patient data**: Service registers orderers, not patients. Synthetic data only in development and testing.
- **DCB0129**: Clinical safety implications assessed — registration delays can impact vaccine ordering which has downstream patient safety consequences for immunoglobulin holding centres and programme delivery.
- **UK GDPR Art. 9**: Service processes professional contact data (names, emails, job titles) in a health service context. DPIA required before user testing with real data.
- **Network isolation**: Azure SQL, Redis, and Key Vault accessed via Private Endpoints. Container Apps deployed into VNet. Only the Container App HTTPS ingress is internet-accessible.
- **Managed Identity**: No shared access keys. All service-to-data communication uses Managed Identity with least-privilege RBAC roles.

## References

- [ImmForm Registration Service Requirements Specification v0.8](../../discovery/requirements/ImmForm-Registration-Service-Requirements.md)
- [Discovery Scenario](../../discovery/scenarios/scenario.md)
- [Prioritised User Journey Build Order](../prioritised-user-journeys.md)
- [GDS Service Standard](https://www.gov.uk/service-manual/service-standard)
- [GOV.UK Design System](https://design-system.service.gov.uk)
- [MHRA GDP Guide](https://www.gov.uk/government/publications/rules-and-guidance-for-wholesale-distribution)
- [MADR Format](https://adr.github.io/madr/)
