# Story 025 — Audit Evidence Export

**Journey**: Audit evidence retrieval (`journey-audit-evidence-retrieval.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As Rachel (QA Lead and WDA Responsible Person),
I need to export a registration's complete event chain as a structured CSV file,
So that I can provide portable evidence to MHRA inspectors without them needing access to the system.

## Acceptance Criteria

### Functional
- [ ] Given I am viewing a registration timeline, when I select "Export evidence", then the system generates a CSV file containing the complete event chain for that registration
- [ ] Given I request an export via GET `/api/audit/registrations/{id}/export`, then the CSV includes columns: EventType, Timestamp, ActorType, ActorId, PreviousState, NewState, Detail, CorrelationId
- [ ] Given the export is generated, then the CSV includes a header row with the registration summary: RegistrationId, ApplicantName, AccountNumber, OrganisationCode, CurrentStatus, ExportTimestamp
- [ ] Given the export is downloaded, then the filename format is: `registration-{CorrelationId}-audit-{export-date}.csv`
- [ ] Given the export includes all 19 possible event types and all Notify dispatch records, then no events are omitted or summarised
- [ ] Given I am not authenticated with the `ImmFormQaRp` role, then the export endpoint returns 401 or 403

### Accessibility
- [ ] The "Export evidence" action is keyboard accessible via Tab and activated with Enter
- [ ] Screen reader announces "Export evidence as CSV" and confirms when the download starts
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-button` (secondary variant for download action)

### Clinical Safety
- [ ] N/A — the export contains administrative registration data, not clinical data

### Data Protection
- [ ] Export files contain PII (applicant names, email addresses) — they are classified as OFFICIAL-SENSITIVE and must be handled according to the service's data handling policy
- [ ] The export action itself is logged to the audit trail: EVT event recording who exported which registration's data and when
- [ ] Export files are generated on-demand and not cached on the server
