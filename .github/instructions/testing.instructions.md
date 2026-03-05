---
applyTo: "**/tests/**,**/test_*,**/*_test.py,**/conftest.py"
---

# Testing Standards

See `tech-stack.instructions.md` for the current test framework, file structure, fixtures, naming conventions, and run commands.

## Coverage

- Target: 80% coverage on lines, branches, functions

## Rules

- Never share mutable state between tests
- Never call real databases, APIs, or file systems in unit tests
- Use synthetic NHS data only (e.g. NHS number `943 476 5919`)
- All API routes must have at least one happy-path test
- Error paths must be tested (invalid input, missing resource)
- After making code changes, rebuild and restart the application before running tests — always verify tests run against the latest code, not a stale build

## E2E Tests

- E2E tests must assert content correctness, not just element visibility
- Checking an element is visible does not verify it displays the correct data
- Assert expected text values, counts, or states — not just DOM presence
- Every page that displays dynamic data must have assertions verifying the rendered values match expected data
