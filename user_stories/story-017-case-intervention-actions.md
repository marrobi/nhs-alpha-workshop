# Story 017 — Helpdesk Case Intervention Actions

**Journey**: Fallback case resolution (`journey-fallback-case-resolution.md`)
**Priority**: 2 (Wave 2 — Exception Paths)

## User Story

As Fatima (ImmForm Helpdesk Case Handler),
I need defined intervention actions I can take on stalled cases — extending the approval window, updating the AP contact, or closing unresolvable cases — with each action recorded in the audit trail,
So that I can resolve cases within my authority without reverting to email-based processing.

## Acceptance Criteria

### Functional
- [ ] Given I have opened a fallback case, when I select "Extend approval window", then a new 72-hour approval window is opened with a fresh approval token sent to the AP, and the intervention is recorded in the audit log with my identity, timestamp, and reason
- [ ] Given I have opened a fallback case, when I select "Update AP contact", then I can enter a corrected AP email address, a new approval token is generated and sent to the updated AP, and the intervention is recorded in the audit log
- [ ] Given I have opened a fallback case, when I select "Close as unresolvable", then I must enter a mandatory reason, the case status is set to a terminal state, and the closure is recorded in the audit log
- [ ] Given I take any intervention action, then the system sends a GOV.UK Notify status update email to the applicant informing them of the progress
- [ ] Given I take any action, then audit events include: my actor identity (Entra ID email), the action type, timestamp, previous state, new state, and my stated reason
- [ ] Given I do not have authority for an action (e.g. approving a registration myself — case handlers cannot approve), then the action is not available in the interface

### Accessibility
- [ ] Keyboard navigable using Tab between action buttons and form fields
- [ ] Screen reader announces available actions and confirmation dialogs
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-button`, `govuk-textarea` (reason), `govuk-radios` (action selection)

### Clinical Safety
- [ ] N/A — case intervention is an administrative operation

### Data Protection
- [ ] Only authenticated case handlers with the `ImmFormAdmin` role can perform interventions
- [ ] All interventions are immutably recorded in the audit log — they cannot be modified or deleted
- [ ] The updated AP email is validated before sending — the system does not send tokens to arbitrary email addresses without validation
- [ ] Intervention reasons are visible to the QA/WDA RP via the audit interface
