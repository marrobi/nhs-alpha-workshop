---
applyTo: "**/routers/**,**/api/**"
---

# NHS API Standards

See `tech-stack.instructions.md` for the current backend framework. The NHS rules below are framework-agnostic; the implementation section is specific to the current stack.

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

### NHS Number Validation

- NHS numbers are 10 digits with a modulus 11 check digit
- Display format: 3-3-4 groups (e.g. `943 476 5919`)
- Validate inbound NHS numbers with the check digit algorithm
- Never accept NHS numbers via GET query parameters

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

---

## FastAPI Implementation (current stack)

### Route Structure

- Define routers in `app/routers/` using `APIRouter(prefix="/...", tags=["..."])`
- Use Pydantic models for all request/response schemas
- Use `async def` for route handlers — FastAPI is async-first
- Include routers in `app/main.py` with `app.include_router()`
- Use Pydantic models for input validation — FastAPI returns 422 automatically for invalid input
- Use a Pydantic validator for NHS number check digit validation
- Apply `slowapi` rate limiting to all API endpoints
