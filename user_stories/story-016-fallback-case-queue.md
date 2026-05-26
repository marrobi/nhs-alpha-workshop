# Story 016 — Helpdesk Fallback Case Queue and Assignment

**Journey**: Fallback case resolution (`journey-fallback-case-resolution.md`)
**Priority**: 2 (Wave 2 — Exception Paths)

## User Story

As Fatima (ImmForm Helpdesk Case Handler),
I need to see a queue of registration cases that require manual intervention, with their reason codes and SLA timers,
So that I can prioritise and resolve stalled cases within the defined service level.

## Acceptance Criteria

### Functional
- [ ] Given a registration case triggers a fallback condition (AP expiry after resend limit, Organisation API returning no valid AP, or validation exception the automated journey cannot resolve), then the system flags the case for helpdesk fallback with a reason code
- [ ] Given I am authenticated with the `ImmFormAdmin` role via Entra ID, when I access the admin dashboard, then I see a filtered view of cases assigned to the fallback queue
- [ ] Given I view the fallback queue, then each case displays: applicant name, account number, organisation name, current status, reason code, case age, and SLA position
- [ ] Given I open an assigned case, then I see the complete chronological event log showing every registration event from EVT-01 onwards, with timestamps, actor types, and actor identities
- [ ] Given I view a case, then the current state is clearly indicated (e.g. "Expired — resend limit reached", "No valid AP found")
- [ ] Given the fallback queue shows cases, then cases are sorted by SLA urgency (oldest first)

### Accessibility
- [ ] Keyboard navigable using Tab between queue items and case details
- [ ] Screen reader announces case summaries, reason codes, and SLA indicators
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-table`, `govuk-tag` (status), `govuk-breadcrumbs`

### Clinical Safety
- [ ] N/A — helpdesk case management involves no clinical data

### Data Protection
- [ ] Helpdesk case handlers authenticated via Entra ID with `ImmFormAdmin` role — no unauthorised access
- [ ] Case detail view shows the minimum data needed for resolution — AP email is displayed to the case handler (they may need to contact the AP) but is not displayed to the applicant
- [ ] All case handler actions are recorded in the audit log with the handler's identity
