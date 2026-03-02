## Unit & Integration Tests

### Goal
Achieve 80% test coverage across the FastAPI backend with pytest + httpx.

### Acceptance Criteria
- [ ] `tests/unit/` contains tests for every router module
- [ ] `tests/integration/` contains API integration tests using httpx AsyncClient
- [ ] `conftest.py` has shared fixtures (test client, mock data)
- [ ] `pytest --cov=app --cov-fail-under=80` passes
- [ ] All tests use synthetic NHS data only
- [ ] No tests call real external services
