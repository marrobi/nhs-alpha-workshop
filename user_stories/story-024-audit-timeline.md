# Story 024 — Audit Registration Timeline with Actor Attribution

**Journey**: Audit evidence retrieval (`journey-audit-evidence-retrieval.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As Rachel (QA Lead and WDA Responsible Person),
I need to view the complete, immutable chronological event chain for any registration with actor attribution at every state transition,
So that I can verify GDP compliance and demonstrate the chain of approval events to MHRA inspectors.

## Acceptance Criteria

### Functional
- [ ] Given I select a registration from search results, when I access `/api/audit/registrations/{id}/timeline`, then I see the complete chronological event chain from EVT-01 (session started) through to the current state
- [ ] Given I view the timeline, then every event displays: event type (EVT-01 through EVT-19), timestamp (UTC), actor type (System, Applicant, Manager, Admin, QaRp), actor identity, previous state, new state, and any event-specific detail
- [ ] Given the timeline includes AP decisions (EVT-07 or EVT-08), then the AP's identity and the decision timestamp are clearly attributed
- [ ] Given the timeline includes an admin manual override (EVT-16), then the previous state, new state, admin identity, timestamp, and mandatory reason are all displayed
- [ ] Given the timeline includes GOV.UK Notify dispatch records, then each notification shows: template ID, recipient type, dispatch timestamp, and status
- [ ] Given the timeline includes helpdesk interventions, then each intervention shows the case handler's identity, action type, and stated reason
- [ ] Given the timeline is immutable, then events are displayed in their original recorded order and content — they cannot be modified or reordered

### Accessibility
- [ ] Keyboard navigable using Tab between timeline events
- [ ] Screen reader announces each event with its timestamp, actor, and state transition
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-table` (timeline), `govuk-tag` (event type), `govuk-details` (expandable event detail)

### Clinical Safety
- [ ] N/A — audit timeline displays administrative registration data, not clinical data

### Data Protection
- [ ] Access is restricted to authenticated users with the `ImmFormQaRp` Entra ID role
- [ ] Actor identities (email addresses) are displayed to the QA/WDA RP as this is required for GDP compliance — they are not displayed to applicants or unauthorised users
- [ ] Hashed IP addresses are displayed as-is — they cannot be reversed to identify individuals
