# Story 005 — Declaration and Submission

**Journey**: NHS new starter registration (`journey-nhs-new-starter-registration.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to confirm a mandatory declaration and submit my registration application,
So that my application is formally recorded with a timestamp and declaration of accuracy.

## Acceptance Criteria

### Functional
- [x] Given I have reviewed check your answers, when I navigate to `/register/declaration`, then I see the declaration page with a mandatory confirmation checkbox
- [x] Given I am on the declaration page, then I see the declaration text stating: that all information provided is true and correct; that the site meets all legal requirements for possession of medicines; that appropriate cold chain facilities exist
- [x] Given I am on the declaration page, then I see input fields for full name and job title pre-populated with data already entered during the journey
- [x] Given I check the confirmation checkbox, enter my full name and job title, and click "Accept and send", then the system creates the registration record with status "Submitted", generates a CorrelationId, records the DeclarationTimestamp (UTC), and computes a SHA-256 payload checksum (NFR-14)
- [x] Given submission succeeds, then the system writes audit event EVT-02 (Submission received) with applicant identity, timestamp, and CorrelationId
- [x] Given submission succeeds, then the system triggers the duplicate detection check (see Story 009) and AP approval notification dispatch (see Story 010)
- [x] Given I do not check the confirmation checkbox, when I click "Accept and send", then I see a GDS error message: "You must confirm the declaration before submitting"
- [x] Given I am on the declaration page, then I see a Back link that returns me to check your answers
- [x] Given I have submitted, when I attempt to navigate back to form steps, then I am redirected to the confirmation page — re-submission is not possible

### Accessibility
- [x] Keyboard navigable using Tab between the checkbox, input fields, and the submit button
- [x] Screen reader announces the declaration text, checkbox state, and any error messages
- [x] Error page title follows GDS format: "Error: Declaration — ImmForm — GOV.UK"
- [x] Focus moves to the error summary on validation failure
- [x] Meets WCAG 2.2 Level AA (verified via axe-core)
- [x] Uses GOV.UK Design System components: `govuk-checkboxes`, `govuk-input`, `govuk-button`, `govuk-error-summary`

### Clinical Safety
- [x] N/A — declaration relates to site compliance (cold chain, medicines possession), not clinical data; accuracy of declaration is the applicant's responsibility

### Data Protection
- [x] Declaration captures the minimum data required: full name, job title, and timestamp
- [x] Submission payload checksum (SHA-256) is computed and stored to detect any subsequent out-of-band modification (NFR-14)
- [x] No PII is included in the URL or query parameters
- [x] CorrelationId is generated as a non-guessable reference — it does not contain PII
