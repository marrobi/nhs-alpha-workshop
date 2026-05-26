# Story 007 — Account and Organisation Code Validation Error Handling

**Journey**: Account validation failure (`journey-account-validation-failure.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to receive clear, actionable error messages when my account number or organisation code is incorrect,
So that I can correct the problem immediately without contacting the helpdesk or restarting my journey.

## Acceptance Criteria

### Functional
- [ ] Given I enter an account number that is not exactly 10 digits, when I submit, then I see the inline error "Enter a valid ImmForm account number. It is 10 digits long."
- [ ] Given I enter an account number containing non-numeric characters, when I submit, then I see the inline error "Enter a valid ImmForm account number. It must contain only numbers."
- [ ] Given I enter a valid-format account number and organisation code that do not match any active pair in the Organisation API, when I submit, then I see "We could not find this account and organisation code combination in ImmForm. Check your ImmForm account number and organisation code are correct, or contact the ImmForm helpdesk at helpdesk@immform.org.uk."
- [ ] Given validation fails, then the GDS error summary is displayed at the top of the page with links to each affected field
- [ ] Given validation fails, then inline error messages appear on the specific fields that caused the failure
- [ ] Given validation fails, then the previously entered values are preserved in the form fields so I can correct them without re-entering everything
- [ ] Given validation fails, when I correct the values and resubmit, then validation runs again and I can proceed if the pair is now valid
- [ ] Given the Organisation API is unavailable, then the system logs the error and I see a user-friendly message advising me to try again or contact the helpdesk — the system does not expose a stack trace or technical error detail
- [ ] Given validation fails, then audit event EVT-04 is written with hashed submitted values for abuse monitoring

### Accessibility
- [ ] Error summary receives keyboard focus automatically on validation failure
- [ ] Each error summary link navigates to the corresponding field when clicked
- [ ] Screen reader announces the number of errors and each error message
- [ ] Error messages follow GDS content style: specific, plain English, telling the user what went wrong and how to fix it
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-error-summary`, `govuk-error-message`

### Clinical Safety
- [ ] N/A — no clinical data involved in account validation

### Data Protection
- [ ] Failed account numbers and organisation codes are not logged in full — hashed values are used in EVT-04 audit entries
- [ ] Error messages do not reveal whether an account number exists in the system without a matching organisation code (anti-enumeration)
- [ ] The Organisation API response details are not exposed to the user beyond the validation outcome
