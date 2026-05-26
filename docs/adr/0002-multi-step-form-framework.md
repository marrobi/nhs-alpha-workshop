# ADR-0002: Reusable Multi-Step Form Framework

**Status**: Accepted

**Date**: 2026-05-25

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

The ImmForm New User Registration service requires a multi-step form journey: start page, applicant details, organisation/account validation, check your answers, declaration, and confirmation. Story 034 identifies this as a reusable framework requirement (FR-22) — the same pattern will be needed for future ImmForm onboarding journeys (AP registration, account revalidation).

Each step needs: strongly typed model binding, server-side validation with GDS error patterns, server-side session state (Redis), sequential and non-sequential navigation (back links, Change links from check-your-answers), CSRF protection, and the Post-Redirect-Get (PRG) pattern. Duplicating this logic per step would create maintenance burden and inconsistency.

The framework must support conditional steps (e.g. wholesaler-specific screens visible only when the account type matches) and allow different journeys to define different step sequences without modifying the base code.

**Driven by**: Story 034 (form framework), Stories 001–006 (registration journey steps), Story 008 (field validation), Story 029 (account disambiguation)

## Decision

Build a reusable multi-step form framework with three components:

1. **`FormStepController<TModel>`** — abstract base controller providing: `[HttpGet]` rendering of the current step's Razor view, `[HttpPost]` with model binding, `[ValidateAntiForgeryToken]`, PRG redirect on success, `View()` return with errors on failure, server-side session read/write via Redis, check-your-answers payload assembly with Change links, and back-link generation.

2. **`IFormStep<TModel>`** — interface for each step: a strongly typed model with Data Annotations or FluentValidation, a Razor view using `GovUk.Frontend.AspNetCore` tag helpers, and an optional visibility predicate (`Func<TModel, bool>`) for conditional steps.

3. **JSON step sequence configuration** — the step order is defined in a JSON file per journey, not hardcoded in the controller. Different journeys provide different JSON files.

Session state is stored server-side in Redis. No form data is stored in hidden fields, query parameters, or client-side storage.

## Consequences

### Positive
- New journeys can be built by defining step classes and a JSON config — no changes to the base framework
- Consistent GDS error handling (error summary at page top, inline error messages) across all steps
- Check-your-answers with Change links is generated automatically from completed step data
- Conditional steps (wholesaler screens, programme-specific fields) are supported without controller modification
- Unit testable via `WebApplicationFactory<Program>` without a browser

### Negative
- Upfront investment in framework design before the first step can be built
- Framework abstractions add indirection — developers must understand the base class contract
- JSON step configuration requires a loading and validation mechanism at startup

### Risks
- Over-engineering the framework for alpha — the registration journey is the only consumer initially. Mitigated by keeping the framework minimal and extending only when a second journey is needed.

## Alternatives Considered

### Monolithic controller per journey
- **Pros**: Simple; no abstraction overhead; each journey is self-contained
- **Cons**: Duplicated navigation, session, and validation logic across every step; GDS error patterns must be reimplemented per step; no reuse for future journeys
- **Why rejected**: The registration journey has 6+ steps and future journeys are planned — duplication cost exceeds framework cost

### Razor Pages workflow
- **Pros**: Built-in page model with `OnGet`/`OnPost`; one file per step
- **Cons**: `tech-stack.instructions.md` explicitly prohibits Razor Pages — the project uses MVC controllers with Razor views only; Razor Pages have a different routing model that conflicts with the agreed URL scheme
- **Why rejected**: Violates the agreed tech stack constraint

### Third-party form library (e.g. FormFlow, custom NuGet)
- **Pros**: Pre-built multi-step logic; less code to maintain
- **Cons**: No GOV.UK Design System integration; adds an external dependency; unlikely to support GDS error patterns, `GovUk.Frontend.AspNetCore` tag helpers, or the specific session/validation requirements
- **Why rejected**: No library integrates with the GOV.UK Design System tag helpers; the framework is small enough to build in-house

## UKHSA Constraints

- **GOV.UK Design System**: All form steps must use `GovUk.Frontend.AspNetCore` tag helpers — the framework must generate HTML compatible with GDS components
- **WCAG 2.2 Level AA**: Error handling must follow GDS error patterns (error summary with anchor links, inline error messages, "Error: " page title prefix)
- **Server-side session only**: No form data in hidden fields or query parameters — enforced by Redis-backed session state with HttpOnly/Secure/SameSite=Strict cookie
- **CSRF protection**: `[ValidateAntiForgeryToken]` on all POST actions

## References

- [GOV.UK Design System — Ask users for information](https://design-system.service.gov.uk/patterns/question-pages/)
- [GOV.UK Design System — Check answers](https://design-system.service.gov.uk/patterns/check-answers/)
- [GOV.UK Design System — Error summary](https://design-system.service.gov.uk/components/error-summary/)
- Story 034 — Reusable multi-step form framework
- `tech-stack.instructions.md` — MVC controllers with Razor views (not Razor Pages)
