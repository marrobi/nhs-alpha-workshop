# Story 003 — Organisation and Account Validation Step

**Journey**: NHS new starter registration (`journey-nhs-new-starter-registration.md`) and account validation failure (`journey-account-validation-failure.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to enter my ImmForm account number and organisation code and receive immediate validation feedback,
So that I know my account details are correct before I proceed to declaration, without waiting days to discover an error.

## Acceptance Criteria

### Functional
- [x] Given I have completed applicant details, when I navigate to `/register/organisation-account`, then I see input fields for ImmForm account number (10-digit) and ImmForm organisation code
- [x] Given I am on the organisation step, when I submit a valid account number and organisation code pair, then the system calls the ImmForm Organisation API to validate the pair
- [x] Given the Organisation API returns a valid, active pair, then the organisation name is pre-filled from the API response and displayed to me as confirmation
- [x] Given the Organisation API returns a valid pair, then the system retrieves and stores the Authorised Person name and email for later use in the approval workflow
- [x] Given the Organisation API returns no match, then I see a GDS error summary and inline error message: "We could not find this account and organisation code combination in ImmForm. Check your ImmForm account number and organisation code are correct, or contact the ImmForm helpdesk at helpdesk@immform.org.uk."
- [x] Given the Organisation API returns no Authorised Person for the pair, then I see a GDS error summary and inline error message: "We cannot find an Authorised Person for this account. Check your ImmForm account number and organisation code are correct, or contact the ImmForm helpdesk at helpdesk@immform.org.uk."
- [x] Given the account number is not exactly 10 digits or contains non-numeric characters, then I see the error message "Enter a valid ImmForm account number. It is 10 digits long."
- [x] Given the Organisation API is unavailable (503 or timeout), then I see the error message "The validation service is temporarily unavailable. Try again in a few minutes or contact the ImmForm helpdesk at helpdesk@immform.org.uk."
- [x] Given I am on the organisation step, then I see a Back link that returns me to the applicant details step
- [x] Given I change my account number or organisation code via a Change link from check your answers, then the Organisation API validation is re-triggered before returning me to check your answers
- [x] Given validation succeeds, when I submit, then I am redirected to the check your answers page
- [x] Given validation fails, when I submit, then the error is surfaced on this step — not deferred to check your answers or declaration

### Accessibility
- [x] Keyboard navigable using Tab between fields and Enter to submit
- [x] Screen reader announces field labels, hint text, and error messages
- [x] Error page title follows GDS format: "Error: Organisation and account — ImmForm — GOV.UK"
- [x] Focus moves to the error summary on validation failure
- [x] Meets WCAG 2.2 Level AA (verified via axe-core)
- [x] Uses GOV.UK Design System components: `govuk-input`, `govuk-error-summary`, `govuk-error-message`, `govuk-back-link`

### Clinical Safety
- [x] N/A — no clinical data collected; account/org validation is an administrative check

### Data Protection
- [x] Account number and organisation code are not logged in full in application logs
- [x] Authorised Person email retrieved from the API is stored only in the server-side session and registration record — not displayed to the applicant
- [x] No PII is included in the URL or query parameters
- [x] Validation failure events (EVT-04) are logged with hashed submitted values for pattern detection
