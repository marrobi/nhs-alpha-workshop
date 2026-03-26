---
applyTo: "**/routers/**,**/api/**"
---

# NHS API Standards

See `tech-stack.instructions.md` for the current backend framework and implementation details. The rules below are framework-agnostic.

---

## NHS API Rules (tech-agnostic)

### URL Design

- RESTful routes: `/api/v1/<resource>` for API endpoints
- Use plural nouns for resources: `/api/v1/patients`, `/api/v1/appointments`
- Never include PII in URL paths or query parameters — use POST with request body instead
- Use lowercase with hyphens: `/api/v1/health-records`, not `/api/v1/healthRecords`

### Request/Response

- Return JSON with consistent envelope: `{ "data": ..., "errors": [...] }`
- Use standard HTTP status codes: 200, 201, 400, 401, 403, 404, 422, 500
- Include `X-Request-ID` header in all responses for distributed tracing

### NHS Number

See `nhs-number.instructions.md` (auto-applied) for full ISB 0149 rules. Key API rules:

- Validate format + modulus 11 check digit on all inbound NHS numbers
- Never accept NHS numbers via GET query parameters — POST only

### FHIR UK Core

- When integrating with NHS services, use FHIR R4 UK Core profiles
- Return valid FHIR JSON structure with synthetic data
- Use FHIR resource types: Patient, Appointment, Encounter, Observation
- Base URL pattern for FHIR: `/fhir/R4/<ResourceType>`

### Error Handling

- Return structured error responses with field-level detail for 400/422
- Never expose stack traces, internal paths, or infrastructure details in error responses
- Log full error details server-side with `X-Request-ID` for correlation

### Rate Limiting

- Apply rate limiting to all API endpoints
- Default: 100 requests per 15-minute window per IP
- Stricter limits for auth endpoints: 10 per 15 minutes
- Exclude `/health` from rate limiting

### Frontend/Backend Field Contract

- Frontend type definitions for API responses MUST use the exact field names from the corresponding backend response model
- Do not rename, alias, or case-convert fields between backend and frontend without an explicit serialisation layer

> See `tech-stack.instructions.md` for framework-specific API implementation details.
