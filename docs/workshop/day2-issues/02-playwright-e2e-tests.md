## Playwright E2E Tests

### Goal
Write end-to-end browser tests for all NHS user journeys with accessibility checks on every page.

### Acceptance Criteria
- [ ] `tests/e2e/` contains Playwright tests using pytest-playwright
- [ ] Page Object Model used for NHS pages
- [ ] Happy path test covers the full user journey (start → input → check answers → confirmation)
- [ ] Error handling test covers validation errors
- [ ] Every page tested with axe-core for WCAG 2.2 AA violations
- [ ] Keyboard navigation verified on form pages
- [ ] Tests pass with `pytest tests/e2e/ --browser chromium`
