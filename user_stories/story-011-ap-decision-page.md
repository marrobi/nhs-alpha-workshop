# Story 011 — AP Decision Page with Approve and Reject

**Journey**: Authorised Person approval (`journey-authorised-person-approval.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Linda (ImmForm Authorised Person),
I need to review the applicant's details and approve or reject their registration from a single page accessed via the email link,
So that I can complete the approval in under two minutes without logging into any system.

## Acceptance Criteria

### Functional
- [ ] Given I click the approve or reject link in the email, when I navigate to `/api/approval/{token}`, then I see a decision page showing the applicant's name, declared job title, and the ImmForm account and organisation they are requesting access to
- [ ] Given the token is valid and unused, when I click "Approve", then the system records audit event EVT-07 (Manager approved) with my identity, timestamp, and CorrelationId, sets the registration status to Approved, marks the token as used, and sends a GOV.UK Notify email to the applicant confirming approval
- [ ] Given I click "Reject", then I am required to enter a mandatory free-text reason before the rejection is recorded
- [ ] Given I enter a rejection reason and confirm, then the system records audit event EVT-08 (Manager rejected) with my identity, timestamp, rejection reason, and CorrelationId, sets the registration status to Rejected, marks the token as used, and sends a GOV.UK Notify email to the applicant with the rejection reason
- [ ] Given I submit an approval or rejection, then I see a confirmation page (`govuk-panel`) confirming my decision: "You have approved this registration" or "You have rejected this registration"
- [ ] Given the token has already been used, when I navigate to the decision URL, then I see a message: "This approval link has already been used. No further action is needed."
- [ ] Given the token has expired (72 hours elapsed), when I navigate to the decision URL, then I see a message: "This approval link has expired. The applicant has been notified and can request a new approval email."
- [ ] Given the token is invalid or not found, when I navigate to the decision URL, then I see an error page with helpdesk contact details
- [ ] Given the token is used, then the IsUsed flag is set atomically to prevent race conditions (single-use enforcement)

### Accessibility
- [ ] Keyboard navigable using Tab, Enter, and Escape
- [ ] Screen reader announces the applicant details, decision buttons, and the rejection reason field
- [ ] Page title follows GDS format: "Approve or reject registration — ImmForm — GOV.UK"
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-button`, `govuk-textarea` (rejection reason), `govuk-panel` (confirmation), `govuk-error-summary`

### Clinical Safety
- [ ] N/A — no clinical data on the decision page

### Data Protection
- [ ] The decision page does not display the applicant's email address or telephone number — only name, job title, account, and organisation
- [ ] The rejection reason is stored in the registration record and audit log — it is visible to the applicant via notification and to the QA/WDA RP via the audit interface
- [ ] The AP's identity (email) is recorded in the audit log for attribution but is not displayed to the applicant
- [ ] Token-based access means no login is required — the token URL is the sole access mechanism
