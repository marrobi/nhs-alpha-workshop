# Story 020 — Admin Registration Detail and Event Timeline

**Journey**: Admin qualification review (`journey-admin-qualification-review.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As David (ImmForm Helpdesk Operative),
I need to view the full detail of any registration including a chronological event timeline,
So that I can understand the complete history of an application before taking any action.

## Acceptance Criteria

### Functional
- [ ] Given I am authenticated with the `ImmFormAdmin` role, when I access `/api/admin/registrations/{id}`, then I see the full registration detail including all applicant data, account and organisation details, declaration details, and current status
- [ ] Given I view a registration detail, then I see a chronological event timeline showing every audit event (EVT-01 through EVT-19) associated with this registration
- [ ] Given I view the event timeline, then each event displays: event type, timestamp (UTC), actor type (System, Applicant, Manager, Admin), actor identity, previous state, new state, and any detail payload
- [ ] Given I view the event timeline, then GOV.UK Notify dispatch records are included showing: template ID, recipient type, dispatch timestamp, and delivery status
- [ ] Given the registration has a manual override (EVT-16), then the timeline clearly shows the previous state, new state, admin identity, and mandatory reason
- [ ] Given the registration has admin qualification decisions, then the timeline shows EVT-13/14 with the decision and reason
- [ ] Given I am on the detail page, then I see action buttons appropriate to the registration's current status (see Stories 021 and 022)

### Accessibility
- [ ] Keyboard navigable using Tab between timeline entries and action buttons
- [ ] Screen reader announces each timeline event with its timestamp and actor
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-summary-list`, `govuk-table` (timeline), `govuk-tag` (status)

### Clinical Safety
- [ ] N/A — no clinical data displayed

### Data Protection
- [ ] Full applicant detail (including email and telephone) is visible only to authenticated admin users — this is necessary for case management
- [ ] Hashed IP addresses in audit entries are displayed as-is — they are not reversible
- [ ] Access to registration detail is logged for audit purposes
