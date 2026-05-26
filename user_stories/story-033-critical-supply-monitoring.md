# Story 033 — Priority Monitoring for Critical Supply AP Routing

**Journey**: Holding centre immunoglobulin registration (`journey-holding-centre-immunoglobulin-registration.md`)
**Priority**: 4 (Wave 4 — Persona and Context Variants)

## User Story

As Sanjay (Immunoglobulin Pharmacist at a holding centre),
I need the admin dashboard to visually flag registrations linked to critical supply programmes (e.g. immunoglobulin) that have been awaiting AP approval for longer than 24 hours,
So that the helpdesk can escalate time-sensitive registrations and prevent delays in access to controlled pharmaceutical supplies.

## Acceptance Criteria

### Functional
- [ ] Given a registration is in AwaitingApproval status and the associated account is tagged as a critical supply programme (e.g. immunoglobulin holding centre), when more than 24 hours have elapsed since the AP approval email was dispatched, then the registration is visually flagged in the admin dashboard (Story 019) with a "Critical — awaiting AP" indicator
- [ ] Given critical supply programme account tags are configurable, then an admin can add or remove account numbers from the critical supply list without code changes (via configuration or database lookup)
- [ ] Given a flagged registration is displayed, then the time elapsed since the AP email was sent is shown (e.g. "Awaiting AP approval for 36 hours")
- [ ] Given the helpdesk views a flagged registration, then they can use the resend approval workflow (Story 014) or the manual override (Story 022) to resolve the delay
- [ ] Given the AP responds within 24 hours, then the registration is not flagged

### Accessibility
- [ ] Keyboard navigable — flagged indicators are focusable and part of the table row
- [ ] Screen reader announces the critical supply flag and time elapsed for flagged registrations
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-tag--red` (critical flag), `govuk-table` (dashboard row)

### Clinical Safety
- [ ] N/A — priority monitoring is an operational workflow concern, not a clinical data concern; however, delays in immunoglobulin access could indirectly affect patient treatment timelines — the flagging mechanism is designed to mitigate this risk

### Data Protection
- [ ] The critical supply flag is visible only to authenticated admin users (`ImmFormAdmin` role)
- [ ] The flag does not expose any additional PII beyond what is already on the admin dashboard
