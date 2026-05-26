# Story 027 — Individual Email Enforcement for Shared-Access Transitions

**Journey**: COVID-19 programme registration (`journey-covid-19-programme-registration.md`)
**Priority**: 4 (Wave 4 — Persona and Context Variants)

## User Story

As Amir (COVID-19 Programme Coordinator),
I need the registration form to detect and reject shared mailbox email addresses and enforce individual NHS.net or organisational email addresses,
So that each registrant is uniquely identifiable for audit and accountability purposes, especially when transitioning from a shared COVID-19 inbox to individual accounts.

## Acceptance Criteria

### Functional
- [ ] Given I enter an email address on the applicant details step, when the email matches a known shared mailbox pattern (e.g. `covid@`, `covidvax@`, `vaccination@`, `sharedmailbox@`, or any pattern flagged in configuration), then the system displays a validation error: "Enter your individual work email address, not a shared mailbox"
- [ ] Given I enter an individual NHS.net or organisational email address, then validation passes and I proceed to the next step
- [ ] Given the shared mailbox detection patterns are configurable, then an admin can update the list without code changes (via configuration or database lookup)
- [ ] Given a valid individual email is submitted, then the applicant details step records the individual email for all subsequent communications and audit records
- [ ] Given Amir's context involves registering multiple staff under the same account, then each registration requires a unique individual email address — no two active registrations may share the same email on the same account

### Accessibility
- [ ] Keyboard navigable — validation error is focusable and linked from the error summary
- [ ] Screen reader announces the validation error message when the shared mailbox is detected
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System error pattern: `govuk-error-summary` at page top, `govuk-error-message` inline on email field

### Clinical Safety
- [ ] N/A — email validation is an administrative control

### Data Protection
- [ ] Email addresses are stored in the registration record as required for the service — they are not logged in application logs
- [ ] The shared mailbox pattern list does not contain PII — it contains only generic address prefixes
