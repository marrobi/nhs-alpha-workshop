# Story 035 — ImmForm API Mocks with Configurable Failure States

**Journey**: All journeys — supports testing of API-dependent flows
**Priority**: 1 (Wave 1 — prerequisite for integration testing and demo)

## User Story

As a UKHSA development team,
We need Minimal API mock endpoints for the ImmForm Organisation API and ImmForm Registration API with configurable success and failure responses,
So that we can develop and test the full registration journey (including error handling, retry logic, and circuit breaker behaviour) without depending on the real ImmForm APIs during alpha.

## Acceptance Criteria

### Functional
- [ ] Given the mock project is `src/ImmForm.Mocks/`, then it implements `app.MapGet` / `app.MapPost` endpoints matching the expected request/response contracts of the ImmForm Organisation API and ImmForm Registration API
- [ ] Given the Organisation API mock, when called with a valid account number and organisation code, then it returns the expected organisation details (name, address, AP contact details)
- [ ] Given the Organisation API mock, when called with an unrecognised account number, then it returns a 404 response matching the real API's error format
- [ ] Given the Registration API mock, when called with a valid registration payload, then it returns a success response with an external registration ID
- [ ] Given the Registration API mock, when configured for failure mode, then it returns: 500 (server error), 503 (service unavailable), or a configurable timeout — so that Polly retry and circuit breaker behaviour can be exercised
- [ ] Given failure modes are configurable, then the mock supports a query parameter or header (e.g. `X-Mock-Scenario: timeout`) to trigger specific failure states without restarting the service
- [ ] Given the mock project is built into the same solution, then it is registered only in non-production environments via configuration — it is never deployed to production

### Accessibility
- [ ] N/A — API mocks have no user interface

### Clinical Safety
- [ ] N/A — mocks are development tooling

### Data Protection
- [ ] Mock responses use synthetic data only — no real patient or organisation data
- [ ] The mock project is excluded from production deployments via build configuration
