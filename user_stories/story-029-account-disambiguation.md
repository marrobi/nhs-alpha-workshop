# Story 029 — Account Disambiguation for Dual-Entity Services

**Journey**: GBSM service administrator registration (`journey-gbsm-service-admin-registration.md`)
**Priority**: 4 (Wave 4 — Persona and Context Variants)

## User Story

As Keisha (Sexual Health Service Administrator),
I need the registration form to help me identify the correct ImmForm account when my organisation operates multiple service entities (e.g. GBSM clinic and sexual health clinic share the same NHS trust),
So that my registration is linked to the right ordering account and avoids delays caused by account mismatches.

## Acceptance Criteria

### Functional
- [ ] Given I enter an account number on the organisation step, when the ImmForm Organisation API returns the account details, then I see the full account name, organisation code, and organisation name displayed clearly for confirmation
- [ ] Given the displayed account details do not match my intended service entity, then I can go back and enter a different account number without losing my other form data
- [ ] Given my organisation has multiple accounts, then the confirmation display is clear enough for me to distinguish between them (showing full organisation name and account number together)
- [ ] Given I confirm the correct account, then the registration proceeds with that account number stored in the session and used for all subsequent steps
- [ ] Given the Organisation API returns an error or the account is not found, then I see the standard validation error flow (Story 007)

### Accessibility
- [ ] Keyboard navigable using Tab between account number input, confirmation details, and navigation buttons
- [ ] Screen reader announces the account details and confirmation prompt clearly
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-input` (account number), `govuk-inset-text` (account confirmation display), `govuk-button`

### Clinical Safety
- [ ] N/A — account selection is an administrative process

### Data Protection
- [ ] Account names and organisation codes are not PII — they are publicly known organisational identifiers
- [ ] The account disambiguation step does not request or display any patient data
