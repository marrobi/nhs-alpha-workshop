# ADR-0005: HTTP Resilience Strategy with Polly

**Status**: Accepted

**Date**: 2026-05-25

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

The ImmForm registration service makes outbound HTTP calls to external APIs: the ImmForm Organisation API (account/org code validation and AP lookup), the ImmForm Registration API (account creation), and GOV.UK Notify (email dispatch). In alpha, these are stubbed by the `ImmForm.Mocks` project, but the resilience pipeline must be designed and wired into the real `HttpClient` registrations from the start so that switching to real endpoints requires only a URL change.

Story 012 (graceful degradation) requires the service to handle API unavailability without data loss, displaying GDS-compliant error pages and logging failures for operational alerting. The service must not hang indefinitely on slow responses, must retry transient failures, and must stop sending requests to a persistently failing dependency.

**Driven by**: Story 012 (graceful degradation under API failure), Story 035 (observability and alerting)

## Decision

### Polly Resilience Pipeline

Register typed HTTP clients with Polly via `AddHttpClient<T>().AddStandardResilienceHandler()` with the following configuration:

| Policy | Configuration |
|---|---|
| **Timeout** | 5 seconds per attempt |
| **Retry** | 3 attempts, exponential backoff (1s, 2s, 4s), jitter enabled, retry on 5xx and `HttpRequestException` |
| **Circuit breaker** | Open after 5 consecutive failures; half-open after 30 seconds; close after 2 successful requests |

### Per-Client Configuration

Three typed HTTP clients, each with the same resilience pipeline:
1. `IOrganisationApiClient` — ImmForm Organisation API
2. `IRegistrationApiClient` — ImmForm Registration API
3. `INotifyClient` — GOV.UK Notify

### Failure Handling

When all retry attempts are exhausted:
- The MVC controller catches the exception and renders a GDS-compliant "There is a problem with the service" error page (HTTP 503)
- The registration is not lost — session state persists in Redis, and the applicant can retry from the same step
- An EVT-18 audit event is logged with the correlation ID, target API, and failure detail
- An Application Insights custom metric is emitted for operational alerting

### Circuit Breaker Logging

Circuit breaker state transitions (Closed → Open, Open → Half-Open, Half-Open → Closed) are logged via nlog at Warning level with the target API name and transition reason.

### Timeout Behaviour

The 5-second per-attempt timeout is distinct from the overall request timeout. With 3 retry attempts, the maximum wall-clock time for a single outbound call is approximately 12 seconds (5s + 1s backoff + 5s + 2s backoff + 5s). The ASP.NET Core request timeout is set to 30 seconds to accommodate this.

## Consequences

### Positive
- Transient failures (network blips, temporary 503s) are retried automatically without user intervention
- Circuit breaker prevents cascading failures when a downstream API is persistently unavailable
- 5-second timeout prevents the service from hanging on slow responses
- Session state in Redis means applicants do not lose progress when retries are exhausted
- Operational alerts on retry exhaustion enable proactive incident response

### Negative
- Exponential backoff adds latency to the user experience when retries are needed (up to 12 seconds worst case)
- Circuit breaker in open state rejects requests immediately — returning an error to the user without attempting the call
- Polly configuration must be tested to ensure the pipeline behaves as expected under failure conditions

### Risks
- Retry on non-idempotent operations (e.g. account creation POST) could cause duplicate submissions. Mitigated by: the ImmForm Registration API is expected to be idempotent on the same CorrelationId; the PayloadChecksum allows detection of duplicate submissions.

## Alternatives Considered

### No resilience — fail immediately on first error
- **Pros**: Simplest implementation; immediate feedback to user
- **Cons**: Transient failures (which are common in cloud environments) result in user-visible errors; no automatic recovery
- **Why rejected**: Cloud APIs experience transient failures regularly — retrying is standard practice and significantly improves user experience

### Custom retry logic (no library)
- **Pros**: No external dependency; full control
- **Cons**: Reimplements well-tested patterns (exponential backoff, jitter, circuit breaker); higher bug risk; no integration with `HttpClientFactory`
- **Why rejected**: Polly is the standard .NET resilience library, recommended by Microsoft, and integrates directly with `HttpClientFactory` via `AddStandardResilienceHandler()`

### Azure API Management (APIM) retry policies
- **Pros**: Centralised policy management; retry logic outside the application
- **Cons**: Additional infrastructure component and cost; adds network hop latency; alpha service does not warrant APIM complexity; circuit breaker patterns are limited in APIM
- **Why rejected**: APIM is disproportionate for alpha; Polly provides equivalent capability within the application with no additional infrastructure

## UKHSA Constraints

- **5-second timeout**: Required by `tech-stack.instructions.md` — all outbound calls must have a 5-second timeout per attempt
- **Structured logging**: All retry attempts and circuit breaker transitions must be logged as structured JSON via nlog
- **Correlation ID**: Every outbound call includes `X-Request-ID` for distributed tracing
- **No data loss**: Session state in Redis ensures applicant progress is preserved across transient failures

## References

- [Microsoft — Build resilient HTTP apps](https://learn.microsoft.com/en-us/dotnet/core/resilience/http-resilience)
- [Polly documentation](https://www.thepollyproject.org/)
- Story 012 — Graceful degradation under API failure
- Story 035 — Observability and alerting (EVT-18)
- `tech-stack.instructions.md` — Polly configuration requirements
