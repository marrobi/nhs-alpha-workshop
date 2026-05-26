# Story 001 — GDS Start Page with Requirements Checklist

**Journey**: NHS new starter registration (`journey-nhs-new-starter-registration.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to see a clear start page that tells me exactly what I will need before I begin,
So that I can gather my ImmForm account number and organisation code in advance and complete registration without stopping mid-journey.

## Acceptance Criteria

### Functional
- [x] Given the service is available, when I navigate to `/`, then I see a GDS-compliant start page with the heading "Register as a new orderer on an existing ImmForm account"
- [x] Given I am on the start page, when I read the content, then I see a "What you will need" section listing: ImmForm account number (10-digit), ImmForm organisation code, professional email address, job title, and telephone number
- [x] Given I am on the start page, when I read the content, then I see a statement that the Authorised Person is looked up automatically and I do not need to know who it is
- [x] Given I am on the start page, when I read the content, then I see the expected processing time (approximately 2 working days once the Authorised Person has approved)
- [x] Given I am on the start page, when I click the "Start now" button, then a server-side session is initiated and I am redirected to the applicant details step
- [x] Given I am on the start page, when I read the content, then I see an "Other ways to register" section with the ImmForm helpdesk contact (helpdesk@immform.org.uk) as the assisted digital fallback
- [x] Given I am on the start page, when I read the content, then I see a link to the separate new delivery location application for users who need a new account
- [x] Given I am on the start page, then the page does not enforce any eligibility gating — eligibility is determined later by account/org code validation

### Accessibility
- [x] Keyboard navigable using Tab, Enter, and Escape
- [x] Screen reader announces the page heading, checklist items, and the "Start now" button purpose
- [x] Page title follows GDS format: "Register as a new orderer on an existing ImmForm account — ImmForm — GOV.UK"
- [x] Meets WCAG 2.2 Level AA (verified via axe-core)
- [x] Uses GOV.UK Design System components: `govuk-panel`, `govuk-button` (Start now), `govuk-list`
- [x] Skip link present as first focusable element: "Skip to main content"

### Clinical Safety
- [x] N/A — no clinical data on this page

### Data Protection
- [x] No PII is collected or displayed on the start page
- [x] No user data is stored until the "Start now" button is clicked and the session is created
- [x] Session cookie is HttpOnly, Secure, SameSite=Strict
