---
applyTo: "**/Controllers/**,**/Endpoints/**,**/*Controller.cs,**/*Endpoint.cs"
---

# UKHSA API Standards

Tech-agnostic rules for API design and behaviour. For .NET / ASP.NET Core specifics (controller patterns, model binding, OpenAPI generation), see `tech-stack.instructions.md`.

Reference: [UKHSA API Design Guidelines](https://ukhsa-collaboration.github.io/standards-api/) — these are the canonical UKHSA rules; this file restates the local engineering invariants.

---

## REST Principles

- APIs MUST follow REST over HTTPS with JSON bodies
- Resource URIs MUST use plural, lowercase, hyphenated nouns: `/api/registrations`, `/api/vaccination-events`
- HTTP verbs MUST carry their standard semantics:
  - `GET` — safe, idempotent, no side effects
  - `POST` — create or non-idempotent action
  - `PUT` — full replace, idempotent
  - `PATCH` — partial update (JSON Merge Patch unless JSON Patch is justified)
  - `DELETE` — idempotent removal
- APIs MUST be versioned via URI prefix (`/api/v1/...`) — never via query string or header alone

## Health Data

- Where the API exchanges health records with other systems, use **FHIR UK Core** profiles
- NHS Number remains the canonical patient identifier across UK health data — validate and store per `health-identifiers.instructions.md`
- Other UKHSA-specific identifiers (case IDs, batch IDs, vaccine UIDs) MUST be defined in a public data dictionary before exposure

## Request & Response Contract

- All request and response bodies MUST be defined as typed models with validation rules
- Field names in JSON MUST be `camelCase`; do not mix casing styles across endpoints
- Frontend / client type definitions MUST match the server's serialised field names exactly — no client-side renaming or alias layers without a documented serialisation contract
- Dates and times MUST be ISO 8601 with explicit time zone (`2026-05-20T08:30:00Z`)
- Money values MUST be transmitted as integer minor units (pence) with an explicit currency code

## Error Handling

- Error responses MUST use [RFC 9457 Problem Details for HTTP APIs](https://www.rfc-editor.org/rfc/rfc9457.html) (`application/problem+json`)
- A Problem Details body MUST include `type`, `title`, `status`, `detail`, and (where useful) `instance`
- Validation failures MUST return `400 Bad Request` with a `errors` extension listing field-level messages
- Authentication failures MUST return `401`; authorization failures MUST return `403` — never collapse the two
- Not-found resources MUST return `404` — never `200` with an empty body
- Server errors MUST return `500` with a generic `detail`; full error context goes to logs (with CorrelationId), never to the client

## Authentication & Authorization

- Public APIs MUST require authentication — anonymous endpoints are reserved for `/health` and openly published metadata
- Service-to-service authentication MUST use OAuth 2.0 client credentials backed by Microsoft Entra ID, or mTLS where justified
- User-context APIs MUST use OpenID Connect with short-lived access tokens
- Authorization decisions MUST happen at the application layer (policy-based authorization), not only at the network layer
- Every authorization check MUST be testable in isolation

## Rate Limiting & Abuse Protection

- All public endpoints MUST enforce rate limiting — default budget is **100 requests per 15 minutes per authenticated principal or source IP**
- Bulk endpoints (data export, search) MUST advertise their limits in OpenAPI and reject excess with `429 Too Many Requests` plus a `Retry-After` header
- Rate-limit counters MUST be backed by a shared store (Redis or equivalent) in multi-instance deployments

## Idempotency

- `POST` endpoints that create resources SHOULD accept an `Idempotency-Key` header and de-duplicate within a 24h window
- `PUT` and `DELETE` MUST be idempotent by definition

## Observability

- Every request MUST be traced with a `CorrelationId` propagated through downstream calls (header: `X-Correlation-ID`)
- Generate a new CorrelationId at the API gateway if the client did not supply one; echo it back in the response
- Log structured events at the controller boundary: request received, validation outcome, downstream call duration, response status
- Personally identifiable information (PII) MUST NOT appear in log messages — see `ukhsa-security.instructions.md`

## OpenAPI / Documentation

- Every API MUST publish an OpenAPI 3.1 document at `/openapi.json`
- All endpoints, parameters, request models, response models, and error schemas MUST be documented
- Examples MUST use synthetic data only — never copy a real record into documentation
- Breaking changes MUST be released under a new version prefix; the old version MUST remain available for at least one release cycle (longer where consumers cannot upgrade)

## Pagination, Filtering, Sorting

- Collection endpoints MUST paginate. Default page size 25, maximum 100
- Use cursor-based pagination (`?cursor=...&limit=...`) for streams or large data sets; offset pagination is acceptable for small static lists
- Sort parameters MUST be an allowlist — never accept arbitrary column names
- Filter parameters MUST be parsed against typed models — never interpolated into queries

## Deprecation

- Deprecated endpoints MUST return a `Deprecation` header (RFC 9745) with the planned sunset date
- A `Link: <...>; rel="successor-version"` header SHOULD point at the replacement
- Sunset MUST give consumers at least 6 months unless a shorter window is required by security
