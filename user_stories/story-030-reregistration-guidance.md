# Story 030 — Re-registration Guidance for Role or Email Changes

**Journey**: Occupational health coordinator registration (`journey-occupational-health-coordinator-registration.md`)
**Priority**: 4 (Wave 4 — Persona and Context Variants)

## User Story

As Colin (Occupational Health Coordinator),
I need clear guidance on the registration page when I attempt to register with an email that already exists on a different account, explaining the re-registration process for role or organisation changes,
So that I understand why my registration was flagged as a duplicate and what steps to take to update my access.

## Acceptance Criteria

### Functional
- [ ] Given I submit a registration and the duplicate detection check (Story 009) identifies an existing active registration with my email address on a different account, then I see a specific guidance message explaining: "You already have an active registration on account [existing account number]. If you have changed role or organisation, contact the ImmForm helpdesk to update your registration."
- [ ] Given the guidance message is displayed, then it includes the ImmForm helpdesk contact details (telephone and email)
- [ ] Given the guidance message is displayed, then I am not blocked from proceeding with my new registration if I choose to — the message is informational, not a hard block (the duplicate will be flagged for helpdesk review)
- [ ] Given I proceed despite the duplicate warning, then the registration is submitted with a flag indicating a potential duplicate for helpdesk review (Fatima's fallback queue — Story 016)

### Accessibility
- [ ] Keyboard navigable — the guidance message and action options are focusable
- [ ] Screen reader announces the guidance message including the existing account reference and helpdesk contact details
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-warning-text` (duplicate notice), `govuk-details` (expandable guidance on re-registration)

### Clinical Safety
- [ ] N/A — duplicate guidance is an administrative process

### Data Protection
- [ ] The existing account number is disclosed to the applicant as they own the associated email address — this is a proportionate disclosure for account management
- [ ] The existing account's organisation name or AP details are not disclosed to the applicant
