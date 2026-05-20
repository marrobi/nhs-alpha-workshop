# ImmForm User Registration Service -- Requirements Specification

**GDS-Compliant Self-Service Onboarding | .NET 10 | Azure | GitHub Actions**

Version 0.8 Draft | Author: Tim Rickeard, Head of ImmForm Technical Services, UKHSA | May 2026

---

## 1. Problem Statement

ImmForm is a nationally critical digital platform supporting immunisation programme management, vaccine supply chain operations, and pandemic flu back-office systems for UKHSA, NHS England, and the wider public health system. It currently serves thousands of users across health and local authority settings.

The existing user onboarding process is entirely manual: a prospective user completes a PDF form (the ImmForm Account Change/Revalidation Form, V2.6) and emails it to helpdesk@immform.org.uk. A helpdesk agent manually validates the submission, chases an authorising manager informally via email, and creates or amends the account directly in the ImmForm backend. The current form covers both new contact registration and account changes; this project covers new user registration only.

Specific problems with the current process:

- Every registration requires direct helpdesk intervention. Applications take up to five working days to process.
- Helpdesk have to manually re-key data from the form into the system.
- There is no automated approval workflow. Approval is handled ad hoc via email with no tracking or time boundary.
- There is no structured, machine-readable audit trail. The entire record of a registration is an email thread, which is not good practice for MHRA GDP compliant systems.
- Validation errors (invalid ImmForm account number and organisation code pairing, shared mailbox, incomplete details) are caught manually, generating back-and-forth that extends processing time.
- The form includes regulated GDP assurances (storage capabilities, pharmacovigilance processes, product recall readiness, disposal arrangements) that are collected on paper with no enforced confirmation or digital record.
- Not scalable: volume spikes during pandemic response or new programme rollouts create immediate helpdesk bottlenecks.
- Not reusable: the process is specific to this form and cannot be extended to future onboarding scenarios without rebuilding from scratch.
- User has to input information which is already available in the system.

---

## 2. Objectives

- Deliver a GDS-compliant, self-service web application that digitises the new user registration journey from the ImmForm Account Change/Revalidation Form V2.6, scoped to new orderer registration only.
- Automate the approval workflow using GOV.UK Notify, with full state tracking, 72-hour expiry, and resend handling.
- Automate the internal ImmForm account qualification and role assignment workflow (currently the 'Official Use Only' paper section), surfaced as an admin dashboard action that calls the ImmForm Registration API.
- Enforce digital capture of all GDP-regulated assurances with a complete immutable audit trail, meeting MHRA Annex 11 equivalent requirements for computerised systems.
- Reduce mean time to account activation from five working days to two working days as a first target, with a path to same-day activation for standard NHS site applications.
- Build the solution as a reusable, configurable multi-step form framework in .NET 10 that can be used as a template for future UKHSA digital onboarding applications.
- Deploy via a fully automated GitHub Actions CI/CD pipeline with environment promotion gating, infrastructure-as-code, and security scanning at every stage.

---

## 3. Scope

### 3.1 In Scope

- New orderer registration to an existing ImmForm delivery point (mapping to section 5 of the current form: 'List all existing orderers and new orderers').
- Digital capture of all mandatory user registration fields including applicant details, organisation and account details.
- Authorised Person approval workflow with GOV.UK Notify email integration.
- ImmForm Registration API integration for automated account creation on approval.
- Real-time ImmForm Organisation API validation of the ImmForm account number and organisation code pair.
- Immutable audit log for all registration lifecycle events.
- GOV.UK Notify transactional emails to applicant and Authorised Person.
- Reusable multi-step form framework and UKHSA GitHub template repository.
- GitHub Actions CI/CD pipeline with Terraform IaC for Azure.

### 3.2 Out of Scope

- Authorised person registration.
- Account revalidation (re-confirmation of existing account details) -- separate process, separate future application.
- Delivery point address changes for existing accounts.
- Organisation code changes due to merger.
- Billing and invoice detail changes.
- Creation of a new delivery location (separate 'Application for a new ImmForm account' form -- a distinct future application).
- Account deactivation or offboarding.
- CIS2, NHSmail, or federated SSO identity (deferred to future iteration; architecture shall not preclude it).
- Welsh language support (deferred; application structure shall accommodate it).
- WDA(H) document upload and storage (v1 shall instruct the user to email the document separately to the helpdesk pending a document management decision).
- Multi-account registration in a single journey: a user who needs to register against more than one ImmForm ordering account must complete a separate registration journey per account. This is a deliberate alpha constraint. A consolidated multi-account journey is deferred to a post-alpha iteration and will require confirmation of ImmForm API support. Users with multiple accounts are a known scenario (locum GPs, health board administrators) and are accommodated by repeat use of this service, not by a single combined journey.

> **Baseline template:** nhs-alpha-workshop (github.com/marrobi/nhs-alpha-workshop) provides GDS service standard structural conventions and GitHub Actions workflow stubs. This project adapts that template, replacing the JavaScript/Python stack with ASP.NET Core 10 MVC (.NET 10) and targeting UKHSA rather than NHS branding.

### 3.3 User Groups

All users registering through this service are product orderers seeking access to an existing ImmForm account. The user groups reflect the organisation types defined in the ImmForm account setup process. Their programme access and role level are inherited from the account they are registering against; users do not select roles during registration.

- **Routine immunisation programme staff:** GP practice managers, practice nurses, and administrative staff ordering centrally supplied vaccines for the national immunisation programme.
- **GBMSM programme orderers:** clinicians and administrators at sexual health services ordering vaccines under the gay, bisexual and other men who have sex with men programme.
- **Occupational health and private (BCG and TB PPD only):** staff at authorised occupational health or private settings ordering BCG vaccine and Tuberculin PPD. These accounts operate under a private customer account number.
- **COVID-19 programme orderers:** NHS and other provider staff ordering COVID-19 vaccines under the national programme.
- **Mpox programme orderers:** staff ordering Mpox vaccines under the national Mpox programme.
- **Immunoglobulin Holding Centre staff:** staff at centres receiving immunoglobulin deliveries ordered by the RIGs team.
- **Wholesalers:** Responsible Persons and authorised staff at organisations holding a Wholesale Dealer Authorisation (WDA(H)) who order medicinal products through ImmForm.

All user groups share the same registration journey. The ImmForm account type determines programme access; the user registration service does not differentiate the journey by user group.

---

## 4. Registration Journey Step Sequence

The application implements two tracks determined by the answer to the programme/context selection step. The standard track applies to all NHS site registrations.

### 4.1 Standard Track

1. **Start page:** served within the service subdomain. States the purpose of the service ("Use this service to register as a new orderer on an existing ImmForm account"), lists what the user will need (ImmForm account number, ImmForm organisation code, professional email address, job title, telephone), states that the Authorised Person is looked up automatically so the user does not need to know who it is, states expected processing time (approximately 2 working days), includes a "Start now" primary button which initiates the server-side session, includes an other ways to register section with the helpdesk address as the assisted digital fallback, and links to the separate new delivery location application. No eligibility gating on this page.

2. **Applicant details:** first name, surname, job title, telephone, email address (not shared mailbox). Follow the relevant GDS design patterns for these question types.

3. **Organisation and account:** ImmForm account number (10-digit) and ImmForm organisation code. The system shall validate that this pair exists and is active via the ImmForm Organisation API. Organisation name is pre-filled from the ImmForm account record on successful lookup. The applicant's role level and programme access are inherited from the existing account; the applicant does not select roles.

4. **Check your answers:** GDS summary list with change links for all steps. Follow standard GDS pattern https://design-system.service.gov.uk/patterns/check-answers/

5. **Declaration:** mandatory confirmation checkbox, full name, job title. Follow pattern https://design.homeoffice.gov.uk/design-system/patterns/ask-users-for/declarations

6. **Confirmation page:** CorrelationId reference number, expected processing time, next steps. Follow pattern https://design-system.service.gov.uk/patterns/confirmation-pages/

---

## 5. Functional Requirements

Priority key: **Must** = mandatory for go-live | **Should** = targeted for initial release | **Could** = future iteration.

| ID | Category | Requirement | Priority |
|----|----------|-------------|----------|
| FR-01 | Journey | The system shall implement a multi-step, GDS one-thing-per-page registration journey. Steps shall be navigable via Back links with server-side session state preserved throughout. | Must |
| FR-02 | Start Page | The service shall have a GDS-compliant start page served within the service subdomain (not Whitehall Publisher). The page shall: (a) state the purpose of the service in plain English -- "Use this service to register as a new orderer on an existing ImmForm account"; (b) include a "What you will need" section listing the ImmForm account number (10-digit), ImmForm organisation code, professional email address, job title, and telephone number; (c) state explicitly that users do not need to know their Authorised Person as this is looked up automatically; (d) state the expected processing time (approximately 2 working days once the Authorised Person has approved); (e) include a "Start now" button as the primary call to action, which initiates the server-side session; (f) include an "Other ways to register" section with the ImmForm helpdesk contact (helpdesk@immform.org.uk) as the assisted digital fallback; (g) include a link to the separate new delivery location application for users who need to set up a new account rather than add a user to an existing one. The start page shall not attempt to enforce eligibility; eligibility is determined by the ImmForm account number and organisation code validation in FR-04. | Must |
| FR-03 | Applicant Details | The system shall collect: first name, surname, job title, telephone number, and email address (mandatory). Shared mailboxes shall be explicitly rejected using the GDS error message component with the message "Enter an individual email address. Shared mailboxes cannot be used for ImmForm registration." | Must |
| FR-04 | Account and Organisation | The system shall collect: existing ImmForm account number (10-digit, validated format) and ImmForm organisation code. The system shall validate that this account number and organisation code pair is active via the ImmForm Organisation API. Organisation name shall be pre-filled from the account record on successful validation. The applicant's programme access and role level are determined by the existing account; the applicant does not select roles. Validation failure shall display a GDS error summary and inline error message on the organisation step specifying whether the account number format is invalid, the organisation code format is invalid, or the pair is not found in ImmForm. | Must |
| FR-10 | Declaration | The applicant shall confirm a mandatory declaration: that all information is true and correct; that the site meets all legal requirements for possession of medicines; that appropriate cold chain facilities exist. The declaration shall capture full name and job title at the point of submission. Pre-populate data with data already entered. Reference: https://design.homeoffice.gov.uk/design-system/patterns/ask-users-for/declarations | Must |
| FR-11 | Check Your Answers | A GDS-compliant check answers page shall be presented before the declaration step, implemented using the govuk-summary-list component. The page shall: (a) use the heading "Check your answers before sending your application"; (b) group answers into labelled sections using govuk-heading-m headings (Personal details, Organisation details); (c) display every field collected during the journey as a summary list row with a Change link per row -- Change links shall include visually hidden text describing what is being changed for screen reader accessibility; (d) pre-populate all fields if the user navigates back via a Change link, returning them to the check answers page after editing without requiring them to re-traverse subsequent steps; (e) display skipped optional fields as "Not provided"; (f) use a two-thirds layout on desktop; (g) not include the declaration or submit button -- submission is handled on the separate declaration step (FR-10); (h) if the user changes their ImmForm account number or organisation code via a Change link, the ImmForm Organisation API validation (FR-04) shall be re-triggered before returning the user to the check answers page -- a changed pair that fails validation shall surface the GDS error summary on the organisation step and not return the user to check answers until the pair is valid. | Must |
| FR-12 | Confirmation | On successful submission the applicant shall receive a GDS-style confirmation page with a unique reference number (CorrelationId) and an indication of processing time (target 2 working days vs current up to 5). | Must |
| FR-13 | Manager Approval | On submission the system shall send a time-limited (72-hour) GOV.UK Notify email to the single Authorised Person on record for the ImmForm account being registered against. The Authorised Person is retrieved from the ImmForm Organisation API at submission time and is not entered by the applicant. There is one Authorised Person per account. The approval email contains an approve or reject link. Registration shall not proceed to account creation without explicit Authorised Person approval. The applicant is informed by GOV.UK Notify email that the approval request has been sent. If the ImmForm Organisation API returns no Authorised Person for the account number and organisation code pair, the system shall not proceed to submission. A GDS error summary and inline error message shall be displayed on the organisation step with the message: "We cannot find an Authorised Person for this account. Check your ImmForm account number and organisation code are correct, or contact the ImmForm helpdesk at helpdesk@immform.org.uk." The error shall be surfaced at the point of API lookup, not at the check your answers step. The reason for surfacing it at the lookup step rather than at submission is that by the time the user reaches check your answers they have traversed several steps with data tied to that account. Failing late on a missing AP would require them to navigate back through the journey. Failing at the organisation step keeps the error proximate to the field that caused it, which is consistent with the GDS error pattern principle of telling users about errors as close to the point of entry as possible. | Must |
| FR-14 | Manager Rejection | The Authorised Person shall be able to reject the application with a mandatory free-text reason. The applicant shall receive a GOV.UK Notify email with the rejection reason. The rejected record shall be retained in the audit log. | Must |
| FR-15 | Approval Link Expiry | If the approval link expires without action, the applicant shall be notified by email and offered the ability to resend the approval request. Maximum two resend attempts per application. If the Authorised Person is on leave for an extended period of time then the user will need to contact the helpdesk. This should be made clear after second resend. If the Authorised Person record held against the account is incorrect or the named individual has left the organisation, the applicant should contact the helpdesk (helpdesk@immform.org.uk). The helpdesk will identify the correct Authorised Person and request that the organisation updates their AP record in ImmForm before the applicant resubmits a new registration. Updating the AP record against an ImmForm account is out of scope of this build; it is handled through the existing ImmForm account management process. | Must |
| FR-16 | Duplicate Detection | Before submission the system shall perform two duplicate checks: (a) whether the applicant email address already exists as an active ImmForm user account -- if so, present a clear GDS error message directing them to contact the helpdesk; (b) whether the applicant email address already has a pending registration against the same ImmForm account number -- if so, present a clear GDS error message advising them that a registration is already in progress. A pending registration against a different account number shall not block submission. | Must |
| FR-17 | Automated Account Creation | On Authorised Person approval, the system shall call the ImmForm Registration API to create the user account and assign programme access without any manual helpdesk step. | Must |
| FR-18 | Applicant Notifications | GOV.UK Notify confirmation emails shall be sent to the applicant at: (a) submission received, (b) Authorised Person approval, (c) account activation. Each email shall include the CorrelationId reference. | Must |
| FR-19 | Admin Review Dashboard | ImmForm administrators shall be able to view all registration requests with status (Pending Submission, Awaiting Manager Approval, Approved, Rejected, Expired, Account Created), timestamps, and the full application detail. | Should |
| FR-21 | Manual Override | Administrators shall be able to manually approve or reject at the account qualification stage, re-trigger manager approval, and add internal notes to any registration record. | Should |
| FR-22 | Reusable Framework | The multi-step form journey shall be implemented as a configurable framework (base controller, step model interface, session-backed state) allowing future UKHSA onboarding applications to be created by defining a step configuration and API integration without modifying core framework code. | Must |
| FR-23 | Audit Access | The system shall provide a named QA / WDA Responsible Person role with read-only access to the registration audit log, distinct from the administrator role defined in FR-19. This role shall require no helpdesk or product team involvement to exercise. The role shall be able to: (a) search registration records by applicant name, account number, organisation code, registration state, and date range; (b) view the complete chronological event history for any registration, including all lifecycle events, actor identities, state transitions, and GOV.UK Notify dispatch records; (c) retrieve full detail of any admin manual override (EVT-16) including previous state, new state, actor identity, timestamp, and mandatory reason; (d) view the account state recorded at the time of each registration event per section 6.2. This access profile shall be provisioned as a separate named role in the application, not as a subset of the admin dashboard. | Must |
| FR-24 | Audit Export | The system shall provide an export function accessible to the QA / WDA RP role (FR-23) that produces a structured, self-contained record of a specified registration lifecycle. The export shall be suitable for attachment to an MHRA inspection pack or GDP quality dossier without further manual collation. The export shall include: all lifecycle events in chronological order with UTC timestamps; actor type and identity at each event; state transitions; CorrelationId; GOV.UK Notify dispatch records (template ID, recipient type, dispatch timestamp); and any admin override records with reasons. Export format shall be PDF or structured CSV. The export function shall not require helpdesk or product team involvement. Wholesaler account exports shall include a statement of the applicable 5-year retention period. | Must |

---

## 6. Registration Lifecycle Events

The following table defines all auditable events in the registration lifecycle. Every event shall generate an immutable audit log entry. Events marked in the Notify column shall also trigger a GOV.UK Notify email to the indicated recipient.

| Event ID | Event | Trigger | Actor | Audit Log | Notify Recipient |
|----------|-------|---------|-------|-----------|-----------------|
| EVT-01 | Session started | Applicant loads start page and begins journey | Applicant | Yes | None |
| EVT-02 | Submission received | Applicant completes declaration and submits | Applicant | Yes | Applicant (confirmation) |
| EVT-03 | Duplicate email blocked | Email address matched to existing ImmForm account at submission | System | Yes | None |
| EVT-04 | Account/org code pair validation failed | ImmForm Organisation API returns no match | System | Yes | None |
| EVT-05 | Session abandoned | Session expires after 60 minutes inactivity without submission | System | Yes | None |
| EVT-06 | Manager approval email sent | System dispatches approval link to Authorised Person | System | Yes | Authorised Person |
| EVT-07 | Manager approved | Authorised Person clicks approve within 72-hour window | Manager | Yes | Applicant |
| EVT-08 | Manager rejected | Authorised Person clicks reject and submits reason | Manager | Yes | Applicant (with reason) |
| EVT-09 | Approval link expired | 72-hour window elapsed without manager action | System | Yes | Applicant |
| EVT-10 | Approval resend requested (attempt 1) | Applicant requests resend following expiry | Applicant | Yes | Authorised Person |
| EVT-11 | Approval resend requested (attempt 2) | Applicant requests second resend | Applicant | Yes | Authorised Person |
| EVT-12 | Resend limit reached | Applicant attempts third resend | System | Yes | Applicant (advise contact helpdesk) |
| EVT-13 | Admin qualification approved | Administrator approves account qualification check | Admin | Yes | None |
| EVT-14 | Admin qualification rejected | Administrator rejects account qualification check | Admin | Yes | Applicant (with reason) |
| EVT-15 | Admin pricelist assigned | Administrator selects and confirms pricelist access | Admin | Yes | None |
| EVT-16 | Admin manual override applied | Administrator manually changes application state via dashboard | Admin | Yes | None |
| EVT-17 | ImmForm Registration API call made | System submits account creation payload to ImmForm API | System | Yes | None |
| EVT-18 | ImmForm Registration API call failed | API returns error or timeout after Polly retry exhaustion | System | Yes | ImmForm helpdesk alert (Teams) |
| EVT-19 | Account creation confirmed | ImmForm API returns successful account creation response | System | Yes | Applicant (account active) |

**Notes:**

EVT-03 and EVT-04 are system validation outcomes, not user errors. Both shall be logged with the submitted values (hashed where PII) to support pattern detection and abuse monitoring.

EVT-05 (session abandoned) is a system event only. No Notify email is sent. The event is logged for operational reporting on journey completion rates.

EVT-12 marks the point at which the automated process is exhausted. The Notify email to the applicant at this point shall include the ImmForm helpdesk contact, making this the only re-entry point for the manual process.

EVT-17 and EVT-18 are distinct events. The audit log must record both the attempt and the outcome separately so that a failed account creation is fully traceable even where no confirmation was received.

EVT-16 shall record the previous state, new state, and a mandatory admin-entered reason in the audit log. This is the primary evidence trail for any MHRA inspection of an account that was manually processed.

---

## 6. Non-Functional Requirements

| ID | Category | Requirement | Priority |
|----|----------|-------------|----------|
| NFR-01 | Accessibility | All pages shall meet WCAG 2.2 Level AA. Automated axe-core checks in CI pipeline. Manual testing with NVDA and VoiceOver prior to go-live. No GDS pattern deviation without documented justification. | Must |
| NFR-02 | GDS Compliance | UI shall use GovUk.Frontend.AspNetCore (or a validated equivalent) with ImmForm Logo. All error patterns, page titles (format: `Error: [page title] - ImmForm - GOV.UK`), back links, and summary lists shall follow the GOV.UK Design System specification. | Must |
| NFR-03 | Performance | 95th percentile page load under 2 seconds at expected load. All outbound API calls (ImmForm APIs, GOV.UK Notify) shall have a 5-second timeout and Polly-based circuit breaker with retry. | Must |
| NFR-04 | Scalability | Application shall be stateless and horizontally scalable via Azure Container Apps or AKS, consistent with the ImmForm Azure platform. Session state held server-side (Azure SQL or distributed cache), not in-process. | Must |
| NFR-05 | Security: TLS | All traffic TLS 1.2 minimum. HSTS enforced. Certificates managed via Azure Key Vault and Acmebot (consistent with ImmForm CDN/AFD certificate management). | Must |
| NFR-06 | Security: Input Validation | All input validated server-side. No client-side-only validation. All output HTML-encoded. All validation failures shall be surfaced using the GDS error summary component at the top of the page and the GDS error message component inline on the affected field, following the GOV.UK Design System error pattern. Validation rules shall cover as a minimum: ImmForm account number (10 digits, numeric only); ImmForm organisation code (format TBC from ImmForm API contract); email address (RFC 5322 format, shared mailbox rejection per FR-03); telephone number (UK format, numeric with spaces and leading plus permitted, minimum 10 digits); name fields (not blank, maximum 100 characters); job title (not blank, maximum 100 characters). Optional fields that are skipped shall not trigger validation errors. Validation error messages shall follow GDS content style: specific, in plain English, telling the user what went wrong and how to fix it -- for example "Enter a telephone number, like 01632 960 001 or 07700 900 982" not "Invalid telephone number". | Must |
| NFR-07 | Security: Secrets | No secrets in source control. All API keys, connection strings, and credentials in Azure Key Vault. Application accesses Key Vault via Managed Identity. GOV.UK Notify API key rotated at minimum annually. | Must |
| NFR-08 | Security: CSRF | All form POST operations protected by ASP.NET Core anti-forgery token validation. | Must |
| NFR-09 | Session | User session held server-side with a secure, HttpOnly, SameSite=Strict cookie. Session expires after 60 minutes of inactivity. Session data purged on completion or abandonment. | Must |
| NFR-10 | Observability: Logging | Structured JSON logging via nlog with Application Insights sink. All registration lifecycle events logged with: CorrelationId, EventType, Timestamp (UTC), ActorType, ActorId, ApplicationState. | Must |
| NFR-11 | Observability: Alerting | Azure Monitor alerts for: ImmForm API call failures, manager approval timeout (72-hour threshold), application error rate spike, pipeline deployment failures. Alerts routed to ImmForm helpdesk Teams channel. | Must |
| NFR-12 | Observability: Tracing | Distributed tracing across web app and all outbound API calls via Application Insights. CorrelationId propagated through all log events and included in all GOV.UK Notify email payloads. | Must |
| NFR-13 | MHRA GDP: Audit Trail | All data creation, state transitions, and approval events shall be written to an immutable AuditLog table: EventType, RegistrationId, Timestamp (UTC), ActorType (System/Manager/Admin), ActorId, PreviousState, NewState, CorrelationId, HashedIPAddress. Application service account shall have no DELETE or UPDATE permission on this table. | Must |
| NFR-14 | MHRA GDP: Data Integrity | All form data shall be persisted at submission with a SHA-256 checksum of the payload. Data at rest encrypted via Azure SQL TDE. Any out-of-band modification shall be detectable. | Must |
| NFR-15 | MHRA GDP: Electronic Records | The system shall maintain a complete, time-stamped, queryable record of each registration from submission to account activation, sufficient for MHRA inspection to demonstrate the chain of approval events. | Must |
| NFR-16 | MHRA GDP: System Validation | An IQ/OQ/PQ computer system validation pack shall be produced prior to production release, in accordance with UKHSA computer system validation policy. The WDA(H) related workflow is subject to MHRA GDP Annex 11 equivalent scrutiny. | Must |
| NFR-17 | GDPR | DPIA completed and signed off before user testing with real data. PII minimised. Configurable retention periods enforced by a scheduled Azure Function cleanup job. Shared mailboxes explicitly blocked (FR-03). | Must |
| NFR-18 | Availability | Target 99.5% availability during operational hours (07:00-20:00 Mon-Fri). Planned maintenance communicated via GOV.UK service unavailable banner pattern. | Should |
| NFR-19 | Browser Support | All modern evergreen browsers (Chrome, Firefox, Edge, Safari latest stable). Internet Explorer explicitly out of scope. | Must |
| NFR-20 | GitHub Actions | Must include steps for lint/format, tests+coverage (>=70%), dependency scan. | Must |
| NFR-21 | Documentation | Must contain Architectural Decision Record. | Must |
| NFR-22 | MHRA GDP: Audit Integrity | The system shall detect and surface the following audit integrity conditions: (a) SHA-256 checksum mismatch on any persisted registration payload (extending NFR-14): flagged as a critical anomaly; (b) registration records with structurally incomplete event sequences -- for example EVT-02 present without a subsequent EVT-06, or EVT-07 present without EVT-17 -- flagged as a workflow anomaly; (c) EVT-16 records without a recorded reason: flagged as a compliance anomaly. Anomalies shall be surfaced as flagged records within the QA / WDA RP audit log view (FR-23) and shall generate an alert to the ImmForm service owner via the Azure Monitor Teams channel (consistent with NFR-11). Anomaly detection shall run as a scheduled check aligned to the Azure Function cleanup job (NFR-17) and on-demand from the audit log interface. | Must |
| NFR-23 | MHRA GDP: Data Retention | The existing 5-year wholesaler retention period (section 6.2) is extended to a documented policy covering all account types, enforced by the scheduled Azure Function cleanup job (NFR-17). Minimum retention periods from the later of account activation date or account deactivation date: (a) Wholesaler (WDA(H)) accounts: 5 years; (b) NHS site registrations (all programme types): 3 years; (c) Rejected applications: 2 years from rejection date; (d) Expired applications: 2 years from expiry date; (e) Abandoned sessions (EVT-05): 6 months. The complete retention policy shall be documented in the service's GDP quality dossier, accessible to the QA / WDA RP via FR-23 without a product team request. Any scheduled deletion event shall be recorded in the audit log with the retention basis stated. | Must |

### 6.1 Mandated Technology Stack

- **Runtime:** .NET 10 LTS, ASP.NET Core MVC.
- **GDS components:** GovUk.Frontend.AspNetCore NuGet package (or validated equivalent). UKHSA header and footer branding applied via layout override.
- **Notifications:** GOV.UK Notify .NET client for all transactional emails.
- **Persistence:** Azure SQL (ImmForm SQL MI or dedicated registration database TBC on data classification decision). EF Core 10.
- **Logging:** nlog with Application Insights sink.
- **HTTP resilience:** Polly for retry, circuit breaker, and timeout on all outbound calls (ImmForm Organisation API, ImmForm Registration API, GOV.UK Notify).
- **Containerisation:** Docker multi-stage build, deployed to Azure Container Apps or AKS (to be confirmed against ImmForm platform roadmap).
- **Secrets:** Azure Key Vault with Managed Identity.
- **IaC:** Terraform (consistent with ImmForm Azure platform).
- **API mocking (alpha only):** ASP.NET Core minimal API, used to implement stub versions of the ImmForm Organisation API and ImmForm Registration API during alpha. The mock projects shall reside in the same solution, share the same runtime, and implement the expected request and response contracts so that failure states (API unavailability, no Authorised Person found, Registration API error) can be exercised and validated. Mock projects shall not be deployed to production.

### 6.2 MHRA GDP Compliance Detail

ImmForm is a computerised system used in the supply of medicinal products under a Wholesale Distribution Authorisation. The registration application forms part of the customer validation process referenced in the ImmForm Terms of Use and the MHRA GDP licence. The following shall apply:

- The AuditLog table shall contain: EventType, RegistrationId (CorrelationId), Timestamp (UTC), ActorType (System/Manager/Admin), ActorId, PreviousState, NewState, IPAddressHash, UserAgent. No DELETE or UPDATE permission shall be granted to the application service account.
- All GDP assurance confirmations on the existing account (storage capabilities, disposal, recall readiness, pharmacovigilance) are held against the organisational account record and are not re-collected during user registration. The audit log shall record the account state at the time of each user registration event.
- A computer system validation pack (IQ/OQ/PQ) shall be produced before production release, covering Azure infrastructure installation qualification, registration workflow operational qualification, and performance qualification under expected load.
- Change control for any post-validation modification shall follow the UKHSA computer system validation change control procedure and be recorded in the service risk register.
- The wholesaler track (FR-09) is subject to heightened scrutiny as it directly supports MHRA GDP Annex 11 equivalent obligations; all wholesaler application records shall be retained for a minimum of 5 years.
- The QA / WDA Responsible Person shall have independent, read-only access to the audit log and export function as specified in FR-23 and FR-24, without dependency on the ImmForm helpdesk or product team. Audit integrity monitoring and data retention enforcement are specified in NFR-22 and NFR-23 respectively.

---

## 7. GitHub Actions CI/CD Requirements

All workflows shall use OIDC-based Azure authentication. No credentials stored as GitHub repository secrets. Deployment to staging and production requires a passing Snyk scan with no Critical or High vulnerabilities.

| Workflow | Trigger | Steps |
|----------|---------|-------|
| pr-checks.yml | Pull request to main | Checkout, NuGet restore, dotnet build, dotnet test with code coverage, axe-core accessibility scan against localhost, SonarQube SAST scan, Dependabot alert check, post coverage and accessibility summary as PR comment. PR must be green before merge. |
| build-push.yml | Push to main or semver tag | Multi-stage Docker build, tag with Git SHA and semver, push to Azure Container Registry via OIDC federated identity (no stored credentials), generate SBOM (Syft), run Snyk container vulnerability scan (fail on Critical or High), publish signed image digest as build artefact. |
| deploy-dev.yml | Successful build-push on main | Terraform plan and apply (dev environment), deploy to Azure Container App dev slot, run smoke test suite against dev, post deployment summary to Teams channel. |
| deploy-staging.yml | Release tag or manual trigger | Terraform plan and apply (staging), deploy to staging, run full integration test suite, run OWASP ZAP baseline security scan, post scan report as artefact. Requires manual approval from ImmForm Technical Services team before proceeding. |
| deploy-prod.yml | Manual approval after staging gate | Terraform apply (prod), blue/green slot swap on Container App, production smoke tests, GitHub release tag, post deployment audit record to Azure Monitor custom event log. |
| scheduled-security.yml | Nightly at 02:00 UTC | Dependabot full dependency audit, Snyk scan on latest production image digest (including secrets detection), alert via Teams webhook on any Critical or High finding. |
| validate-terraform.yml | PR touching infra/** | terraform fmt -check, terraform validate, tflint, Checkov IaC security scan, post plan output as PR comment. |

### 7.1 Branch and Environment Strategy

- **feature/\* branches:** pr-checks.yml on push. No deployment.
- **main branch:** build-push.yml + deploy-dev.yml run automatically on merge.
- **release/\* tags:** deploy-staging.yml triggers automatically. deploy-prod.yml requires named ImmForm Technical Services member approval.
- All deployments to staging and production require Snyk scan passing and integration tests green.

### 7.2 Infrastructure as Code -- Azure Resources

- Azure Container App or AKS namespace with Managed Identity.
- Azure Container Registry (shared ImmForm or dedicated TBC).
- Azure SQL Database: registration schema (applications, audit log, step state) with TDE enabled.
- Azure Key Vault: secrets, GOV.UK Notify API key, ImmForm API key.
- Application Insights workspace.
- Azure Monitor alert rules (API failure, approval timeout, error rate).
- Parameterised per environment (dev, staging, prod) via Terraform workspace or variable file.

---

## 8. Reusable Framework Pattern

### 8.1 Multi-Step Form Framework (.NET)

- `FormStepController<TModel>` base class providing: server-side session-backed step state, sequential and non-sequential navigation, back-link generation, model validation orchestration per step, and check-your-answers payload assembly.
- Each step implements `IFormStep<TModel>` with a strongly typed model, a Razor view using GDS tag helpers, and an optional step-visibility predicate (used for conditional wholesaler steps).
- Step sequence defined in a JSON configuration file or fluent builder, not hardcoded, enabling different applications to define different journeys without modifying base framework code.
- API integration abstracted behind `IRegistrationApiClient`, allowing different applications to substitute their own API client.

### 8.2 UKHSA GitHub Repository Template

- **Template repository:** ukhsa-immform/gds-webapp-template, derived from nhs-alpha-workshop, adapted for .NET 10, UKHSA branding, and ImmForm Azure platform conventions.
- **Template contents:** AGENTS.md with .NET Copilot agent context, .github/instructions/ with security and GDS coding standards for .NET, GitHub Actions workflow stubs (pr-checks, build-push, deploy-*, scheduled-security, validate-terraform), Terraform module stubs for ImmForm Azure resources, skeleton ASP.NET Core MVC project with multi-step form framework pre-wired.
- New onboarding applications are created by using the template and configuring: step JSON definition, API client implementation, GOV.UK Notify template IDs, and organisation-specific eligibility rules.

---

## 9. Assumptions and Constraints

- The ImmForm Registration API will be mocked for the alpha. The API will support whatever payload the registration service requires; there are no pre-existing API constraints to design around.
- GOV.UK Notify access will be requested and onboarded before development completion. Template IDs for all email types will be agreed and documented.
- The ImmForm Organisation API will be mocked during development and is the authoritative source for validating the account number and organisation code pair. A fallback error state shall be implemented for API unavailability.
- A DPIA will be initiated at project start and signed off by the UKHSA DPO before any user testing involving real data.
- The Azure SQL MI instance (or a new Azure SQL Database) will be available in the ImmForm subscription. Database schema and naming conventions will align with existing ImmForm standards.
- The solution will go through a private beta phase with a controlled group of NHS site users before public launch.
- WDA(H) document upload is deferred to v2 pending a decision on document management approach within the ImmForm platform.

> **Note:** Shared mailboxes are explicitly blocked in FR-03 per UKHSA traceability requirements. This is a behaviour change from the current paper process and will need to be communicated clearly to users and helpdesk ahead of go-live.

---

## 10. Success Criteria

- A new user completes end-to-end registration without contacting the ImmForm helpdesk.
- Authorised Person approval is captured digitally with timestamp and CorrelationId. No approval is possible after the 72-hour link expiry.
- Account creation is fully automated, with no manual helpdesk step for standard NHS site applications.
- All registration lifecycle events, GDP assurance confirmations, and admin qualification decisions are present in the immutable audit log.
- The application passes WCAG 2.2 AA automated and manual accessibility checks.
- The GitHub Actions pipeline achieves green status from feature branch through to production, with no manual steps except the production approval gate and admin qualification.
- Mean time to account activation reduces from five working days to two working days within three months of go-live.
- A second UKHSA onboarding application can be created from the template framework with step configuration changes only, no core framework modification.

---

## Document Control

| Version | Date | Author | Change |
|---------|------|--------|--------|
| 0.1 | May 2026 | Tim Rickeard | Initial draft. |
| 0.2 | May 2026 | Tim Rickeard | Updated with field mapping from ImmForm Change/Revalidation Form V2.6; wholesaler track; GDP assurances; admin qualification workflow; shared mailbox constraint. |
| 0.3 | May 2026 | Tim Rickeard | Scoped to user registration only (org account assumed pre-existing); user groups section added; ODS API replaced with ImmForm Organisation API account/org code pair validation; role level inheritance from account stated explicitly; dangling FR-07/FR-08 GDP references corrected; .NET 10 reference fixed throughout. |
| 0.4 | May 2026 | Tim Rickeard | Multi-account registration scoped to one account per journey (future iteration noted in 3.2); FR-16 duplicate detection updated for two cases; FR-13 AP clarified as single per account, looked up from ImmForm API not entered by applicant; ImmForm Registration API assumption updated (mocked for alpha, no pre-existing constraints); WCAG updated to 2.2 throughout; UCD comments incorporated. |
| 0.5 | May 2026 | Tim Rickeard | FR-02 Start Page added (GDS start pattern, not Whitehall Publisher, served within service subdomain); section 4.1 step 1 updated with full start page content spec; FR-11 Check Your Answers updated with full GDS check answers pattern requirements (govuk-summary-list, Change links with visually hidden text, two-thirds layout, pre-population on back navigation, declaration on separate step). |
| 0.6 | May 2026 | Tim Rickeard | FR-11 updated: re-validation of ImmForm account/org code pair required when changed via Check Your Answers Change link. NFR-06 updated: all validation failures use GDS error summary and inline error message components; validation rules and example error message wording specified for all fields. FR-03 shared mailbox error message text specified. |
| 0.7 | May 2026 | Tim Rickeard | FR-15 updated: departed or incorrect AP fallback path added; helpdesk-assisted AP update process documented as out of scope of build. FR-23 added: QA / WDA RP read-only audit log access role. FR-24 added: audit log export for MHRA inspection packs. NFR-22 added: audit integrity anomaly detection. NFR-23 added: data retention policy extended to all account types. Section 6.2 updated: cross-references to FR-23, FR-24, NFR-22, NFR-23 added. Section 6.1 updated: ASP.NET Core minimal API added as mandated alpha mocking approach for ImmForm Organisation API and Registration API. |
| 0.8 | May 2026 | Tim Rickeard | Section 7 updated: CodeQL SAST replaced with SonarQube; Trivy container and dependency scanning replaced with Snyk (covering CVE scanning, container image scanning, and secrets detection) throughout all pipeline workflow definitions. Rationale: CodeQL and Trivy are not available for private GitHub repositories; SonarQube and Snyk are the approved internal toolchain equivalents. |

---

*OFFICIAL | ImmForm Technical Services | UKHSA*
