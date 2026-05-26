# Story 034 — Reusable Multi-Step Form Framework

**Journey**: All applicant-facing journeys (FR-22 reusable form framework requirement)
**Priority**: 1 (Wave 1 — prerequisite infrastructure for all form journeys)

## User Story

As a UKHSA development team,
We need a reusable multi-step form framework (`FormStepController<TModel>` base class, `IFormStep<TModel>` interface, and JSON step sequence configuration),
So that the registration journey and any future ImmForm journeys can be built from composable, testable form steps without duplicating navigation, session, and validation logic.

## Acceptance Criteria

### Functional
- [ ] Given a new journey is defined, when a developer creates step classes implementing `IFormStep<TModel>` and a JSON step sequence file, then the framework handles: sequential navigation (next/previous), back-link generation, model validation per step, and session state management — without modifying the base framework code
- [ ] Given `FormStepController<TModel>` is the base class, then it provides: `[HttpGet]` rendering of the current step's Razor view, `[HttpPost]` model binding and validation with `[ValidateAntiForgeryToken]`, redirect to next step on valid submission (PRG pattern), return `View()` with errors on invalid submission, server-side session state via Redis, and check-your-answers payload assembly from all completed steps
- [ ] Given each step implements `IFormStep<TModel>`, then each step has: a strongly typed model with Data Annotations or FluentValidation, a Razor view using `GovUk.Frontend.AspNetCore` tag helpers, and an optional visibility predicate for conditional steps (e.g. wholesaler-specific screens)
- [ ] Given the step sequence is defined in a JSON configuration file, then different applications can define different journey sequences by providing different JSON files — the controller does not hardcode the step order
- [ ] Given the framework manages session state, then all form data is stored server-side in Redis — never in hidden fields, query parameters, or client-side storage
- [ ] Given the framework assembles the check-your-answers payload, then it produces a GOV.UK summary list with Change links that navigate back to the relevant step while preserving all other step data
- [ ] Given the framework is testable, then unit tests can verify step transitions, validation, and session management using `WebApplicationFactory<Program>` without a browser

### Accessibility
- [ ] Framework-generated navigation (back link, continue button) uses GOV.UK Design System components and is keyboard navigable
- [ ] Error handling follows GDS error pattern: `govuk-error-summary` at page top, inline `govuk-error-message` on affected fields
- [ ] Page title updates to include "Error: " prefix when validation fails
- [ ] All framework-generated HTML meets WCAG 2.2 Level AA

### Clinical Safety
- [ ] N/A — the form framework is generic infrastructure

### Data Protection
- [ ] Session data is stored server-side only (Redis with HttpOnly/Secure/SameSite=Strict cookie)
- [ ] Session data is cleared on completion or timeout (60-minute inactivity — see Story 018)
- [ ] No form data is exposed in URLs, query parameters, or client-side storage
