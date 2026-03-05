---
name: playwright-nhs-e2e
description: 'Playwright E2E patterns for NHS user journeys — Page Object Model, accessibility, NHS Design System. Language follows tech-stack.instructions.md.'
---

# Playwright NHS E2E Testing — Skill

Patterns and rules for Playwright browser tests that verify NHS user journeys. Check `tech-stack.instructions.md` for the E2E testing language and framework — generate all code in that language.

## Dependencies

Installed during scaffold. Use the Playwright test runner and axe-core accessibility library for the stack's language. Always run `playwright install --with-deps chromium`.

## Directory Structure

```
tests/e2e/
├── pages/        # Page Objects (one per route)
├── journeys/     # One test file per user journey
├── screenshots/  # Visual QA evidence (gitignored)
└── accessibility/ # Full-site axe audit
```

Add shared fixtures/config as appropriate for the test framework (e.g. `conftest.py`, `playwright.config.ts`). Set base URL to the local frontend dev server.

## Page Object Model

One class per page/route in `tests/e2e/pages/`. Tests call Page Object methods — never raw selectors. Each Page Object exposes navigation and interaction methods.

## Accessibility

Run axe-core on **every page** the test visits — zero violations allowed. Also verify: keyboard Tab order, screen reader landmarks (`<main>`, `<nav>`, `<header>`, `<footer>`), form labels, and NHS error summary on validation failure.

## Selectors

Role-based only: `get_by_role`, `get_by_label`, `get_by_text`. Never CSS or XPath.

## NHS Page Patterns

| Pattern | Key assertions |
|---------|---------------|
| **Start page** | NHS header, service name `<h1>`, "Start now" button |
| **Question pages** | One question per page, back link, continue button |
| **Check answers** | Summary list with "Change" links |
| **Confirmation** | Panel with reference number |
| **Error pages** | Error summary at top, inline field errors, focus moves to summary |
| **Data display** | Assert expected data values appear in page text — do not rely on element visibility alone |
| **Success pages** | Assert error summary component is NOT present |
| **Summary list / data table** | Assert every value cell is non-empty |

## Consuming User Journeys

One test file per journey. Derive tests from:

1. **Journey Main Flow table** (`discovery/user_journeys/data/`) — digital touchpoint rows → test steps
2. **Decision Points** → separate test functions or parametrised cases
3. **ADR** (`docs/adr/`) — concrete routes, endpoints, data models
4. **User stories** (`user_stories/story-*.md`) — acceptance criteria → assertions

The journey gives the **sequence**; ADR and stories give **implementation detail**.

## Screenshots

Save screenshots on failure and at key journey milestones. Store in `tests/e2e/screenshots/`. Capture at minimum: start page, each form submission result, confirmation page, and error states.

## Rules

- One test file per journey, one Page Object per page
- axe check on every page navigation — no exceptions
- Role-based selectors only
- Must work headless in CI (Chromium)
- Synthetic NHS data only (NHS number `943 476 5919`)
- Never mix E2E and unit tests
