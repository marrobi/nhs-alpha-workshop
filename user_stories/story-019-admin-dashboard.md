# Story 019 — Admin Registration Dashboard

**Journey**: Admin qualification review (`journey-admin-qualification-review.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As David (ImmForm Helpdesk Operative),
I need to view all registration requests in a filterable dashboard showing status, timestamps, and SLA position,
So that I can prioritise my workload and identify registrations that need attention.

## Acceptance Criteria

### Functional
- [ ] Given I am authenticated via Entra ID with the `ImmFormAdmin` role, when I access `/api/admin/registrations`, then I see a list of all registration requests
- [ ] Given I am on the dashboard, then each registration row displays: applicant name, account number, organisation name, current status, submitted date, and time since submission
- [ ] Given I am on the dashboard, then I can filter registrations by status (Draft, Submitted, AwaitingApproval, Approved, Rejected, Expired, AccountCreated, Qualified, QualificationRejected)
- [ ] Given I am on the dashboard, then I can filter by date range (submitted date)
- [ ] Given I am on the dashboard, then registrations are sorted by most recent first by default
- [ ] Given I am not authenticated or do not have the `ImmFormAdmin` role, when I attempt to access the dashboard, then I receive a 401 or 403 response — the dashboard is not accessible
- [ ] Given the dashboard is loaded, then the data is retrieved from the registration database — not from a cache or stale snapshot

### Accessibility
- [ ] Keyboard navigable using Tab between filter controls and table rows
- [ ] Screen reader announces table headers, row content, and filter state
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-table`, `govuk-select` (filters), `govuk-tag` (status badges)

### Clinical Safety
- [ ] N/A — admin dashboard displays registration data, not clinical data

### Data Protection
- [ ] Access is restricted to authenticated users with the `ImmFormAdmin` Entra ID role
- [ ] The dashboard does not display applicant email addresses or telephone numbers in the list view — only name, account, organisation, and status
- [ ] All dashboard access is logged for audit purposes
