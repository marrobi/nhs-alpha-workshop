# Story 012 — Automated Account Creation with Retry

**Journey**: Account creation API execution (`journey-account-creation-api-execution.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need my ImmForm user account to be created automatically after the Authorised Person approves my application,
So that I can start ordering vaccines without any manual helpdesk step.

## Acceptance Criteria

### Functional
- [ ] Given the Authorised Person has approved my registration (status = Approved), then the system builds the Registration API payload from the stored registration data and submits it to the ImmForm Registration API
- [ ] Given the Registration API call is made, then audit event EVT-17 (ImmForm Registration API call made) is written with timestamp, CorrelationId, and payload reference
- [ ] Given the Registration API returns success, then audit event EVT-19 (Account creation confirmed) is written, the registration status is set to AccountCreated, and a GOV.UK Notify activation email is sent to the applicant with the CorrelationId reference
- [ ] Given the Registration API returns an error, then the system retries using Polly with exponential back-off (3 attempts, 5-second timeout per attempt)
- [ ] Given the Registration API fails after all retry attempts, then audit event EVT-18 (Registration API call failed) is written with the error detail, and an operational alert is sent to the ImmForm helpdesk Teams channel via Azure Monitor
- [ ] Given a Registration API failure after retry exhaustion, then the registration status remains Approved (not rolled back) — the operations team investigates and can re-trigger the API call
- [ ] Given the Registration API times out, then the Polly circuit breaker is engaged and subsequent calls are short-circuited until the circuit recovers
- [ ] Given account creation succeeds, then the ActivatedAt timestamp is set on the registration record

### Accessibility
- [ ] N/A — this story covers backend API integration, not a user-facing page
- [ ] The activation email sent to the applicant follows GDS email content standards

### Clinical Safety
- [ ] N/A — account creation is an administrative operation; however, delays in activation may impact vaccine ordering operations, which has indirect programme consequences

### Data Protection
- [ ] The Registration API payload contains only the minimum data needed to create the account
- [ ] API call failure details are logged without full PII — registration ID and CorrelationId are sufficient for tracing
- [ ] The operational alert to the Teams channel includes CorrelationId and error category but not applicant PII
