---
applyTo: "**/tests/**,**/*Tests.cs,**/*Tests/**"
---

# Testing Standards

See `tech-stack.instructions.md` for the current test framework, file structure, fixtures, naming conventions, and run commands.

## Coverage

- Target: 80% coverage on lines, branches, and methods, enforced in CI
- New code SHOULD aim higher (≥ 90%) — coverage debt is paid down, not accrued

## Rules

- Never share mutable state between tests — use fresh fixtures, per-test setup, or `IClassFixture` / `ICollectionFixture` with clean teardown
- Never call real databases, APIs, or file systems in unit tests
- Use synthetic data only (e.g. NHS number `943 476 5919` from the 9xx test range — see `health-identifiers.instructions.md`). Never use production data in any test
- All API routes MUST have at least one happy-path test
- Error paths MUST be tested (invalid input, missing resource, unauthorized access, downstream failure)
- After making code changes, rebuild and restart the application before running tests — always verify tests run against the latest code, not a stale build
- Never use `[Fact(Skip = "...")]` or `[Theory(Skip = "...")]` without a reason string that references a tracked issue
- Tests MUST be deterministic — flaky tests are bugs and MUST be fixed or quarantined with a tracked issue, not retried

## Mocking Boundary

- **Unit tests** MAY mock external dependencies (databases, APIs, file systems) using `Moq`, `NSubstitute`, or equivalent — this is standard isolation practice
- **Cloud services with no local emulator** (e.g. hosted AI/LLM APIs, GOV.UK Notify, third-party identity providers): unit tests MUST mock the SDK client using the test framework's mocking library — there is no alternative for local testing. An ADR authorising the integration is sufficient justification; a separate user story for the mock is not required. Integration tests SHOULD use the real service endpoint, the vendor's sandbox, or be skipped with a clear reason if no sandbox exists
- **Integration tests** MUST use real services or real test instances (e.g. test Azure SQL database, Azure sandbox subscription, Azurite for storage). Do not substitute with in-memory fakes unless there is an explicit user story for that mock
- **E2E tests** MUST hit the real running application and its real backing services — no service mocking at this layer
- **Application code** MUST never contain mock/stub implementations of external services in non-test assemblies. If a service client is needed, integrate with the real SDK and real configuration. If the real service is unavailable, the code MUST fail fast — not silently degrade
- Do not create mock service implementations unless an explicit user story requests it, with the decision recorded in an ADR

## E2E Tests

- Run via Playwright for .NET against a running test instance
- E2E tests MUST assert content correctness, not just element visibility
- Checking an element is visible does not verify it displays the correct data
- Assert expected text values, counts, and states — not just DOM presence
- Every page that displays dynamic data MUST have assertions verifying the rendered values match expected data
- Every page tested MUST include an `axe-core` accessibility scan; the test MUST fail on any new Level A or AA violation
- E2E test data MUST be seeded via the application's APIs or via dedicated test fixtures — never by direct database insertion that bypasses validation logic

## Integration Tests

- Use `WebApplicationFactory<Program>` to host the application in-process with the real `Program.cs` pipeline
- Use a per-test or per-class database (transactional rollback or schema-per-test) so tests do not interfere with each other
- Replace external HTTP dependencies with `HttpMessageHandler` fakes or WireMock.Net — these are integration boundaries, not application mocks

## Performance Tests

- See `performance.instructions.md` for k6 targets and structure

## Test Data

- Synthetic data only — across all test layers
- A `TestData/` folder or shared library SHOULD host reusable builders / factories
- Personal data, even synthetic, MUST NOT be committed to source control if it could be mistaken for real data (use the documented synthetic ranges)
