# Story 021 — Admin Qualification Decision and Pricelist Assignment

**Journey**: Admin qualification review (`journey-admin-qualification-review.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As David (ImmForm Helpdesk Operative),
I need to approve or reject the account qualification check and assign pricelist access for approved registrations,
So that the applicant's account is fully configured and ready for ordering.

## Acceptance Criteria

### Functional
- [ ] Given a registration has status AccountCreated, when I view the detail page, then I see a "Qualification decision" action
- [ ] Given I select "Approve qualification", then the system records audit event EVT-13 (Admin qualification approved) with my identity, timestamp, and CorrelationId, and the registration status changes to Qualified
- [ ] Given I select "Reject qualification", then I must enter a mandatory reason, the system records audit event EVT-14 (Admin qualification rejected) with my identity, timestamp, reason, and CorrelationId, and a GOV.UK Notify email is sent to the applicant with the rejection reason
- [ ] Given a registration is qualified, when I view the detail page, then I see a "Assign pricelist" action
- [ ] Given I select "Assign pricelist", when I choose a pricelist and confirm, then the system records audit event EVT-15 (Admin pricelist assigned) with my identity, timestamp, selected pricelist, and CorrelationId
- [ ] Given qualification or pricelist actions are taken, then the event timeline on the registration detail page updates to include the new events

### Accessibility
- [ ] Keyboard navigable using Tab between decision options and confirmation buttons
- [ ] Screen reader announces available actions, confirmation dialogs, and decision outcomes
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-radios` (decision), `govuk-textarea` (rejection reason), `govuk-select` (pricelist), `govuk-button`

### Clinical Safety
- [ ] N/A — qualification and pricelist assignment are administrative operations

### Data Protection
- [ ] Only authenticated users with the `ImmFormAdmin` role can perform qualification decisions
- [ ] All decisions are immutably recorded in the audit log with the admin's identity
- [ ] Qualification rejection reasons are visible to the applicant (via notification) and to the QA/WDA RP (via audit)
