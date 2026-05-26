# Story 009 — Pre-Submission Duplicate Email Detection

**Journey**: Duplicate detection error (`journey-duplicate-detection-error.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need the system to detect if my email address is already registered or has a pending application before my submission is processed,
So that duplicate accounts are prevented and I receive clear guidance on what to do.

## Acceptance Criteria

### Functional
- [ ] Given I submit my declaration, when the system checks for duplicates, then it performs two checks: (a) whether my email address already exists as an active ImmForm user account, and (b) whether my email address already has a pending registration against the same ImmForm account number
- [ ] Given my email matches an active ImmForm user account, then submission is blocked and I see the error message "This email address is already registered as an ImmForm user. Contact the ImmForm helpdesk at helpdesk@immform.org.uk if you need to update your access."
- [ ] Given my email matches a pending registration against the same account number, then submission is blocked and I see the error message "A registration for this email address is already in progress for this account. You will be notified when it is processed."
- [ ] Given my email matches a pending registration against a different account number, then submission is not blocked — the applicant may legitimately register on multiple accounts
- [ ] Given duplicate detection blocks submission, then audit event EVT-03 (Duplicate email blocked) is written with hashed email and account details
- [ ] Given duplicate detection blocks submission, then the error is displayed on the declaration page using the GDS error summary pattern
- [ ] Given no duplicates are detected, then the registration proceeds to submission and AP notification

### Accessibility
- [ ] Error summary receives keyboard focus on duplicate detection failure
- [ ] Screen reader announces the error message and guidance
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-error-summary`, `govuk-error-message`

### Clinical Safety
- [ ] N/A — no clinical data involved in duplicate detection

### Data Protection
- [ ] Duplicate check responses do not reveal details about the existing account holder — only that a match exists
- [ ] EVT-03 audit entries log hashed email values, not plaintext, for pattern detection and abuse monitoring
- [ ] The duplicate check API does not expose whether a specific email is registered to callers outside the registration flow (anti-enumeration)
