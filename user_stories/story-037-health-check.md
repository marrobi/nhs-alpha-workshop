# Story 037 — Health Check Endpoint

**Journey**: All journeys — operational requirement (NFR)
**Priority**: 1 (Wave 1 — prerequisite for deployment and monitoring)

## User Story

As a UKHSA operations team,
We need a health check endpoint at `/health` that returns `{"status": "ok"}` with a 200 status code,
So that Azure Container Apps can perform liveness probes and Azure Monitor can track service availability.

## Acceptance Criteria

### Functional
- [ ] Given the service is running, when GET `/health` is called, then it returns HTTP 200 with body `{"status": "ok"}` and `Content-Type: application/json`
- [ ] Given the service is unable to connect to its dependencies (database, Redis), when GET `/health` is called, then it returns HTTP 503 with body `{"status": "unhealthy"}` and a list of failed dependency checks
- [ ] Given the health endpoint exists, then it does not require authentication — it is publicly accessible for load balancer and monitoring probes
- [ ] Given the health check verifies dependencies, then it checks: Azure SQL database connectivity and Redis cache connectivity
- [ ] Given the health check runs, then it completes within 5 seconds — it does not perform expensive queries or operations

### Accessibility
- [ ] N/A — the health endpoint is a machine-readable API, not a user interface

### Clinical Safety
- [ ] N/A — health monitoring is an operational concern

### Data Protection
- [ ] The health endpoint does not expose any PII, configuration details, connection strings, or internal implementation details
- [ ] Error responses include only the names of failed dependency types (e.g. "database", "cache") — not connection strings, hostnames, or error stack traces
