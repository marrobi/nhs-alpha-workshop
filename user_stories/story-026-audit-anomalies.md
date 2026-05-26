# Story 026 — Audit Anomaly Detection and Flagging

**Journey**: Audit evidence retrieval (`journey-audit-evidence-retrieval.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As Rachel (QA Lead and WDA Responsible Person),
I need to see registrations flagged for audit anomalies such as payload checksum mismatches or incomplete event sequences,
So that I can proactively identify data integrity issues and investigate irregularities before an MHRA inspection.

## Acceptance Criteria

### Functional
- [ ] Given I access GET `/api/audit/anomalies`, then I see a list of registrations that have been automatically flagged for one or more anomaly types
- [ ] Given the anomaly detection runs, then it flags registrations with: payload checksum mismatches (submitted data hash does not match stored hash), incomplete event sequences (missing expected state transitions), manual overrides without reasons (EVT-16 records with no reason — if validation is bypassed), and expired tokens that were never acted upon
- [ ] Given a registration is flagged, then the anomaly type, detection timestamp, and affected registration CorrelationId are displayed
- [ ] Given I select a flagged registration, then I am navigated to the full registration timeline (Story 024) to investigate
- [ ] Given no anomalies are detected, then I see a clear message: "No anomalies detected"
- [ ] Given anomaly detection identifies a checksum mismatch, then the mismatch detail shows: expected hash, actual hash, and the registration step where the discrepancy occurred

### Accessibility
- [ ] Keyboard navigable using Tab between flagged items
- [ ] Screen reader announces the anomaly type and affected registration for each item
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-table`, `govuk-tag` (anomaly severity), `govuk-warning-text` (for critical anomalies)

### Clinical Safety
- [ ] N/A — anomaly detection operates on administrative registration data

### Data Protection
- [ ] Access is restricted to authenticated users with the `ImmFormQaRp` Entra ID role
- [ ] Anomaly records do not expose applicant PII in the list view — only CorrelationId, account number, and anomaly type
- [ ] Anomaly detection logic does not process or store any data beyond what is already in the audit log and registration tables
