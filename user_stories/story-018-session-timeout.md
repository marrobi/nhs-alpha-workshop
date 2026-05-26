# Story 018 — Session Timeout and Abandonment Handling

**Journey**: Session timeout/abandonment (`journey-session-timeout-abandonment.md`)
**Priority**: 2 (Wave 2 — Exception Paths)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need the system to handle session expiry gracefully when I am inactive for too long,
So that my data is protected and I can restart my registration with a clear explanation of what happened.

## Acceptance Criteria

### Functional
- [ ] Given I have started a registration journey and am inactive for 60 minutes, then the server-side session expires and all session data is purged
- [ ] Given my session has expired, when I attempt to continue the journey (navigate to any form step), then I am redirected to a session expired page
- [ ] Given I am on the session expired page, then I see the message "Your session has expired because you were inactive for more than 60 minutes. Your information has not been saved." and a link to restart the registration
- [ ] Given my session expires, then audit event EVT-05 (Session abandoned) is written with the timestamp and a hashed session identifier — no PII is logged
- [ ] Given my session expires before I have submitted a declaration, then no registration record is created — session data is purged without persisting
- [ ] Given I click the restart link, then a new session is created and I am taken to the start page
- [ ] Given the session timeout threshold is 60 minutes, then this value is configurable via application settings — not hardcoded

### Accessibility
- [ ] Keyboard navigable — the restart link is focusable and activatable via Enter
- [ ] Screen reader announces the session expiry message and the restart link
- [ ] Page title follows GDS format: "Your session has expired — ImmForm — GOV.UK"
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-panel`, `govuk-link`

### Clinical Safety
- [ ] N/A — no clinical data involved in session management

### Data Protection
- [ ] Session data is fully purged from the server-side session store (Redis) on expiry — no PII remains
- [ ] EVT-05 audit entries do not contain PII — only a hashed session identifier and timestamp
- [ ] The session cookie is invalidated on expiry
- [ ] Abandoned session records follow the data retention policy: 6 months
