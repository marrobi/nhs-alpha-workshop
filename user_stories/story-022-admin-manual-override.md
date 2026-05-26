# Story 022 — Admin Manual Override with Mandatory Reason

**Journey**: Admin qualification review (`journey-admin-qualification-review.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As David (ImmForm Helpdesk Operative),
I need to apply a manual override to change a registration's state in exceptional circumstances, with a mandatory reason recorded in the audit trail,
So that edge cases can be resolved while maintaining full traceability for MHRA GDP compliance.

## Acceptance Criteria

### Functional
- [ ] Given I am viewing a registration detail, when I select "Manual override", then I see a form requiring: the new state to apply and a mandatory free-text reason for the override
- [ ] Given I submit a manual override, then the system records audit event EVT-16 (Admin manual override applied) with: my identity, timestamp, previous state, new state, and mandatory reason
- [ ] Given I submit a manual override without a reason, then the system rejects the submission with a validation error: "Enter a reason for this override"
- [ ] Given a manual override is applied, then the event timeline on the registration detail page updates to include EVT-16 with full attribution
- [ ] Given the override changes the registration status, then any downstream effects are triggered (e.g. if status is changed to Approved, the account creation API flow is triggered)
- [ ] Given the manual override is recorded, then the override record is accessible to the QA/WDA RP via the audit interface with full detail (previous state, new state, admin identity, reason)

### Accessibility
- [ ] Keyboard navigable using Tab between form fields and the submit button
- [ ] Screen reader announces the override form fields and any validation errors
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-select` (new state), `govuk-textarea` (reason), `govuk-button`, `govuk-error-summary`

### Clinical Safety
- [ ] N/A — manual override is an administrative operation

### Data Protection
- [ ] Only authenticated users with the `ImmFormAdmin` role can perform manual overrides
- [ ] Override events are immutable — they cannot be modified or deleted from the audit log
- [ ] The override reason is the primary evidence trail for MHRA inspection of manually processed accounts
- [ ] EVT-16 records without a reason are flagged as compliance anomalies by the audit integrity check (NFR-22)
