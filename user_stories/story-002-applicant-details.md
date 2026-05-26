# Story 002 — Applicant Details Form Step

**Journey**: NHS new starter registration (`journey-nhs-new-starter-registration.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to enter my personal details (name, job title, telephone, and email) on a clear, one-thing-per-page form step,
So that my registration captures accurate, individually attributable information.

## Acceptance Criteria

### Functional
- [x] Given I have clicked "Start now", when I navigate to `/register/applicant-details`, then I see input fields for first name, surname, job title, telephone number, and email address
- [x] Given I am on the applicant details step, when I submit the form with all valid fields, then I am redirected to the organisation and account step
- [x] Given I am on the applicant details step, when I submit with missing mandatory fields, then the page re-renders with a GDS error summary at the top and inline error messages on each affected field
- [x] Given I am on the applicant details step, when I enter a shared mailbox address (e.g. noreply@, info@, admin@, team@), then I see the error message "Enter an individual email address. Shared mailboxes cannot be used for ImmForm registration."
- [x] Given I am on the applicant details step, when I enter an email that does not conform to RFC 5322 format, then I see the error message "Enter an email address in the correct format, like name@example.com"
- [x] Given I am on the applicant details step, when I enter a telephone number that is not valid UK format (minimum 10 digits, numeric with spaces and leading plus permitted), then I see the error message "Enter a telephone number, like 01632 960 001 or 07700 900 982"
- [x] Given I am on the applicant details step, then I see a Back link that returns me to the start page
- [x] Given I have completed this step, when I return via a Change link from check your answers, then all fields are pre-populated with my previously entered data

### Accessibility
- [x] Keyboard navigable using Tab between fields and Enter to submit
- [x] Screen reader announces each field label, any hint text, and error messages when present
- [x] Error page title follows GDS format: "Error: Your details — ImmForm — GOV.UK"
- [x] Focus moves to the error summary on validation failure
- [x] Meets WCAG 2.2 Level AA (verified via axe-core)
- [x] Uses GOV.UK Design System components: `govuk-input`, `govuk-error-summary`, `govuk-error-message`, `govuk-back-link`

### Clinical Safety
- [x] N/A — no clinical data collected on this page

### Data Protection
- [x] Only minimum necessary personal data is collected (name, job title, telephone, email)
- [x] No PII is included in the URL or query parameters
- [x] Form data is stored in server-side session only — not in hidden fields or client-side storage
- [x] Email address is validated but not logged in full in application logs
