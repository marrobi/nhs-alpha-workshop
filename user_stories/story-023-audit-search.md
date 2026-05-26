# Story 023 — Audit Search Interface

**Journey**: Audit evidence retrieval (`journey-audit-evidence-retrieval.md`)
**Priority**: 3 (Wave 3 — Operational and Compliance)

## User Story

As Rachel (QA Lead and WDA Responsible Person),
I need to search registration records by applicant name, account number, organisation code, date range, and registration state,
So that I can find specific records quickly and independently during MHRA inspections without helpdesk involvement.

## Acceptance Criteria

### Functional
- [ ] Given I am authenticated via Entra ID with the `ImmFormQaRp` role, when I access `/api/audit/registrations`, then I see a search interface with filters for: applicant name, account number, organisation code, registration state, and date range
- [ ] Given I enter search criteria and submit, then the results display matching registration records with: applicant name, account number, organisation name, current status, submitted date, and CorrelationId
- [ ] Given no results match my search, then I see a clear message: "No registrations found matching your search criteria"
- [ ] Given I am not authenticated or do not have the `ImmFormQaRp` role, then I receive a 401 or 403 response
- [ ] Given the audit interface is separate from the admin dashboard, then my `ImmFormQaRp` role does not grant access to admin actions (qualification, override, pricelist) — it is read-only
- [ ] Given search results are displayed, then I can click any result to view the full registration timeline (see Story 024)

### Accessibility
- [ ] Keyboard navigable using Tab between search fields, the search button, and result rows
- [ ] Screen reader announces search field labels, result count, and result content
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-input`, `govuk-select`, `govuk-date-input`, `govuk-button`, `govuk-table`

### Clinical Safety
- [ ] N/A — audit search involves registration records, not clinical data

### Data Protection
- [ ] Access is restricted to authenticated users with the `ImmFormQaRp` Entra ID role
- [ ] Search queries and result access are logged for audit trail purposes
- [ ] The search interface does not expose data beyond what is needed for audit — no telephone numbers or full email addresses in the results list
