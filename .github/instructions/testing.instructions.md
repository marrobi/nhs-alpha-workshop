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

## Mocking Boundary

- **Unit tests**: MAY mock external dependencies (databases, APIs, file systems) using `unittest.mock.patch` or `pytest-mock` — this is standard isolation practice
- **Cloud services with no local emulator** (e.g. hosted AI/LLM APIs): unit tests MUST mock the SDK client using the test framework's mocking library (e.g. `unittest.mock.patch` for Python, `jest.mock` for JavaScript) — there is no alternative for local testing. An ADR authorising the integration is sufficient justification; a separate user story for the mock is not required. Integration tests should use the real service endpoint or be skipped with a clear reason if no sandbox exists
- **Integration tests**: MUST use real services or real test instances (e.g. test database, Azure sandbox). Do not substitute with in-memory fakes unless there is an explicit user story for that mock
- **E2E tests**: MUST hit the real running application and its real backing services — no service mocking at this layer
- **Application code**: MUST never contain mock/stub implementations of external services. If a service client is needed, integrate with the real SDK and real configuration. If the real service is unavailable, the code must fail — not silently degrade
- Do not create mock service implementations unless an explicit user story requests it, with the decision recorded in an ADR

## E2E Tests

- E2E tests must assert content correctness, not just element visibility
- Checking an element is visible does not verify it displays the correct data
- Assert expected text values, counts, or states — not just DOM presence
- Every page that displays dynamic data must have assertions verifying the rendered values match expected data
