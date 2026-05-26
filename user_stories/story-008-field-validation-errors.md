# Story 008 — GDS Field Validation and Error Messaging

**Journey**: Field validation error correction (`journey-field-validation-error-correction.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to see clear, specific error messages when I enter invalid data on any form step,
So that I can understand exactly what went wrong and fix it without confusion.

## Acceptance Criteria

### Functional
- [ ] Given I submit any form step with validation errors, then the page re-renders with a GDS error summary at the top listing all errors, and inline error messages on each affected field
- [ ] Given first name or surname is blank, then I see "Enter your first name" or "Enter your surname"
- [ ] Given first name or surname exceeds 100 characters, then I see "First name must be 100 characters or fewer" or "Surname must be 100 characters or fewer"
- [ ] Given job title is blank, then I see "Enter your job title"
- [ ] Given job title exceeds 100 characters, then I see "Job title must be 100 characters or fewer"
- [ ] Given email is blank, then I see "Enter your email address"
- [ ] Given email does not conform to RFC 5322 format, then I see "Enter an email address in the correct format, like name@example.com"
- [ ] Given telephone is blank, then I see "Enter your telephone number"
- [ ] Given telephone does not match UK format (minimum 10 digits, numeric with spaces and leading plus), then I see "Enter a telephone number, like 01632 960 001 or 07700 900 982"
- [ ] Given multiple fields have errors, then all errors are listed in the error summary and all affected fields show inline messages
- [ ] Given I correct the errors and resubmit, then validation passes and I proceed to the next step
- [ ] Given all validation is server-side, then no validation is client-side only — JavaScript validation is progressive enhancement only

### Accessibility
- [ ] Error summary receives keyboard focus automatically on validation failure
- [ ] Each error summary link navigates to the corresponding field when clicked
- [ ] Screen reader announces the error count and each error message
- [ ] Error page title format: "Error: [page title] — ImmForm — GOV.UK"
- [ ] Focus order follows the visual order of fields on the page
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-error-summary`, `govuk-error-message`, `govuk-input` with error state styling

### Clinical Safety
- [ ] N/A — no clinical data involved in field validation

### Data Protection
- [ ] Validation error messages do not expose sensitive data or internal system details
- [ ] Invalid form submissions are not logged with full PII — only the fact that validation failed is recorded
