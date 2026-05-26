# Story 006 — Confirmation Page with Reference Number

**Journey**: NHS new starter registration (`journey-nhs-new-starter-registration.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to see a confirmation page with my reference number and expected processing time after submitting my registration,
So that I have a record of my submission and know what happens next.

## Acceptance Criteria

### Functional
- [ ] Given my declaration has been submitted successfully, when I am redirected to `/register/confirmation/{correlationId}`, then I see a GDS confirmation panel with the heading "Application submitted" and my CorrelationId reference number
- [ ] Given I am on the confirmation page, then I see the expected processing time: "Your application will be processed within approximately 2 working days once your Authorised Person has approved it"
- [ ] Given I am on the confirmation page, then I see a "What happens next" section explaining that an approval request has been sent to the Authorised Person for this account
- [ ] Given I am on the confirmation page, then I see guidance on what to do if approval is not received — including a link to the resend approval page
- [ ] Given I am on the confirmation page, then I see the ImmForm helpdesk contact details (helpdesk@immform.org.uk) for further assistance
- [ ] Given I am on the confirmation page, then the session data is cleared — the user cannot navigate back to form steps
- [ ] Given I am on the confirmation page, then a GOV.UK Notify confirmation email is sent to the applicant with the CorrelationId reference (FR-18a, EVT-02 notification)

### Accessibility
- [ ] Keyboard navigable
- [ ] Screen reader announces the confirmation panel heading and reference number
- [ ] Page title follows GDS format: "Application submitted — ImmForm — GOV.UK"
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-panel` (confirmation), `govuk-body`, `govuk-link`

### Clinical Safety
- [ ] N/A — no clinical data on this page

### Data Protection
- [ ] CorrelationId in the URL is a non-guessable reference that does not contain PII
- [ ] The confirmation page does not display the Authorised Person's name or email
- [ ] Session data is purged after confirmation — no PII remains in the session store
- [ ] The confirmation email includes only the CorrelationId and applicant name — no account numbers or organisation codes
