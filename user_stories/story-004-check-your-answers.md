# Story 004 — Check Your Answers Summary Page

**Journey**: NHS new starter registration (`journey-nhs-new-starter-registration.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to review all the information I have entered before submitting my application,
So that I can verify my details are correct and make changes without restarting the journey.

## Acceptance Criteria

### Functional
- [x] Given I have completed all form steps, when I navigate to `/register/check-your-answers`, then I see a GDS summary list with the heading "Check your answers before sending your application"
- [x] Given I am on check your answers, then I see my data grouped into labelled sections: "Personal details" and "Organisation details"
- [x] Given I am on check your answers, then every field collected during the journey is displayed as a summary list row with a Change link
- [x] Given I click a Change link, then I am taken to the relevant form step with my data pre-populated, and after editing I am returned directly to the check your answers page without re-traversing subsequent steps
- [x] Given I change my account number or organisation code via a Change link, then the ImmForm Organisation API validation (FR-04) is re-triggered before I am returned to check your answers — a changed pair that fails validation surfaces the error on the organisation step
- [x] Given the organisation name was pre-filled from the Organisation API, then it is displayed in the Organisation details section as a read-only row (no Change link)
- [x] Given I am on check your answers, then the page does not include a declaration or submit button — submission is handled on the separate declaration step
- [x] Given I am on check your answers, then I see a "Continue" button that takes me to the declaration step
- [x] Given I am on check your answers, then I see a Back link that returns me to the previous form step

### Accessibility
- [x] Keyboard navigable using Tab between Change links and the Continue button
- [x] Change links include visually hidden text describing what is being changed (e.g. "Change first name") for screen reader accessibility
- [x] Screen reader announces section headings, row labels, and values
- [x] Page title follows GDS format: "Check your answers — ImmForm — GOV.UK"
- [x] Meets WCAG 2.2 Level AA (verified via axe-core)
- [x] Uses GOV.UK Design System components: `govuk-summary-list` with Change links, `govuk-heading-m` for section headings
- [x] Layout uses two-thirds column width on desktop

### Clinical Safety
- [x] N/A — no clinical data displayed

### Data Protection
- [x] Authorised Person name and email are not displayed on this page — they are used internally for routing only
- [x] No PII is included in the URL or query parameters
- [x] All data is retrieved from server-side session — not from URL parameters or hidden fields
