# Story 014 — Approval Resend with Two-Attempt Limit

**Journey**: Approval resend workflow (`journey-approval-resend-workflow.md`)
**Priority**: 2 (Wave 2 — Exception Paths)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to be able to resend the approval request to the Authorised Person up to two times if they have not responded,
So that I have a self-service mechanism to chase approval before needing to contact the helpdesk.

## Acceptance Criteria

### Functional
- [ ] Given my approval has expired, when I navigate to `/register/resend-approval/{correlationId}`, then I see a page showing my current resend count and a "Resend approval request" button
- [ ] Given this is my first resend (ResendCount = 0), when I click "Resend approval request", then a new approval token is generated with a fresh 72-hour expiry, a new AP approval email is sent, audit event EVT-10 (Resend attempt 1) is written, and ResendCount is incremented to 1
- [ ] Given this is my second resend (ResendCount = 1), when I click "Resend approval request", then a new approval token is generated, a new AP approval email is sent, audit event EVT-11 (Resend attempt 2) is written, and ResendCount is incremented to 2
- [ ] Given I attempt a third resend (ResendCount = 2), then the system blocks the resend, audit event EVT-12 (Resend limit reached) is written, and I see a message: "You have used both resend attempts. Contact the ImmForm helpdesk at helpdesk@immform.org.uk for further assistance. If the Authorised Person for your account has changed, the helpdesk can help update their details."
- [ ] Given a resend is successful, then the previous expired token is invalidated and the new token is the only valid one
- [ ] Given the resend limit has been reached, then a GOV.UK Notify email is sent to the applicant advising them to contact the helpdesk
- [ ] Given a resend is triggered, then a new entry is written to the NotifyLog table

### Accessibility
- [ ] Keyboard navigable using Tab and Enter
- [ ] Screen reader announces the resend count, the action button, and the helpdesk guidance when limit is reached
- [ ] Page title follows GDS format: "Resend approval request — ImmForm — GOV.UK"
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-button`, `govuk-body`, `govuk-inset-text`

### Clinical Safety
- [ ] N/A — no clinical data involved

### Data Protection
- [ ] The resend page is accessible only via the CorrelationId — it does not require authentication
- [ ] The page does not display the AP's name or email — only that the approval request has been resent
- [ ] CorrelationId in the URL does not contain PII
