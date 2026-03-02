---
applyTo: "**/tests/**,**/test_*,**/*_test.py,**/conftest.py"
---

# Testing Standards — pytest

## Framework

- pytest with httpx `AsyncClient` for FastAPI route testing
- pytest-anyio for async test support
- pytest-cov for coverage reporting
- Target: 80% coverage on lines, branches, functions

## File Structure

```
tests/
├── conftest.py          # Shared fixtures (test client, mock data)
├── unit/                # Unit tests mirror app/ structure
│   ├── test_health.py
│   └── test_<router>.py
├── integration/         # Integration tests for cross-module flows
├── e2e/                 # Playwright browser tests (separate agent)
└── performance/         # k6 load tests (separate agent)
```

## Fixtures

- Define shared fixtures in `conftest.py`
- Use `@pytest.fixture` with appropriate scope
- Always create a reusable `client` fixture for the FastAPI test client
- Mock external dependencies with `unittest.mock.patch` or `pytest-mock`

## Test Naming

- Files: `test_<module>.py`
- Functions: `test_<what>_<condition>_<expected>()`
- Example: `test_health_endpoint_returns_200()`

## Running

```bash
pytest                                          # All tests
pytest tests/unit/                              # Unit only
pytest --cov=app --cov-report=term-missing      # With coverage
pytest --cov=app --cov-fail-under=80            # Enforce threshold
```

## Rules

- Never use `@pytest.mark.skip` without a reason string
- Never share mutable state between tests
- Never call real databases, APIs, or file systems in unit tests
- Use synthetic NHS data only (e.g. NHS number `943 476 5919`)
