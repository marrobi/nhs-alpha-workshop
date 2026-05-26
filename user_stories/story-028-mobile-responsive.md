# Story 028 — Mobile-Responsive Registration Journey

**Journey**: Mpox specialist nurse registration (`journey-mpox-specialist-nurse-registration.md`)
**Priority**: 4 (Wave 4 — Persona and Context Variants)

## User Story

As Donna (Mpox Specialist Nurse),
I need the registration journey to be fully usable on a mobile device with a small screen,
So that I can complete my registration on a tablet or phone in a clinical setting without a desktop computer.

## Acceptance Criteria

### Functional
- [ ] Given I access the registration journey on a mobile device (viewport width 320px–768px), when I progress through all steps, then every form field, button, link, and error message is usable without horizontal scrolling
- [ ] Given I am on a mobile device, when I interact with form fields, then touch targets meet the minimum 44x44px WCAG 2.2 target size requirement
- [ ] Given I am on a mobile device, when I use the back link or change links on the check-your-answers page, then navigation works correctly and preserves my session data
- [ ] Given the GOV.UK Design System responsive grid is used (`govuk-grid-column-two-thirds`), then on mobile viewports the content fills the full width of the screen

### Accessibility
- [ ] Keyboard and touch navigable on all form steps
- [ ] Screen reader announces all content correctly on iOS VoiceOver and Android TalkBack
- [ ] Meets WCAG 2.2 Level AA including Success Criterion 2.5.8 (Target Size minimum)
- [ ] Uses GOV.UK Design System responsive components without custom CSS overrides that break responsiveness

### Clinical Safety
- [ ] N/A — mobile responsiveness is a presentation concern

### Data Protection
- [ ] Session state is server-side (Redis) — no form data is persisted on the device
- [ ] The mobile experience does not introduce any additional data exposure (e.g. no data in browser autofill suggestions for sensitive fields)
