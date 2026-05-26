# ADR-0004: AP Approval Token Lifecycle

**Status**: Accepted

**Date**: 2026-05-25

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

When a new user submits a registration, their nominated Authorised Person (AP) must approve the request. The AP is not an authenticated system user — they are an external contact identified only by email address. The approval mechanism must allow the AP to approve or reject a registration from a link in a GOV.UK Notify email without logging into the system, while preventing token reuse, expiry exploitation, and brute-force enumeration.

The user stories reveal specific lifecycle requirements: 72-hour token expiry, single-use enforcement, a 2-resend cap per registration, distinct error pages for expired/used/invalid tokens, and full audit trail integration.

**Driven by**: Story 010 (AP approval link), Story 011 (AP decision recording), Story 013 (token expiry), Story 014 (approval resend with cap)

## Decision

### Token Design

Use **database-backed opaque tokens** stored in the ApprovalToken table:
- `Token`: cryptographically random string (32 bytes, base64url-encoded) — not guessable, not reversible
- `ExpiresAt`: UTC timestamp, 72 hours after creation
- `IsUsed`: boolean, default false
- `UsedAt`: UTC timestamp, set when the AP submits a decision

### Approval URL

The AP receives a link in the format: `GET /registration/approval/{token}`. This renders the decision page showing applicant details. The AP submits their decision via `POST /registration/approval/{token}` with Approve or Reject (+ reason).

### Single-Use Enforcement

The POST action atomically sets `IsUsed = true` and `UsedAt = DateTime.UtcNow` within the same database transaction as the registration status update. If `IsUsed` is already true, the request is rejected with a "this link has already been used" error page.

### Expiry Handling

If `ExpiresAt < DateTime.UtcNow`, the GET and POST actions both render a "this link has expired" error page with guidance to contact the helpdesk or request a resend. Expired tokens are never reactivated — a resend creates a new token.

### Resend Cap

A registration can have a maximum of 2 resend requests (total 3 tokens including the original). The resend count is tracked on the Registration entity (`ResendCount`). Exceeding the cap renders a "contact the helpdesk" page instead of resending.

When a new token is issued (via resend), the previous token is **not invalidated** — if the AP clicks the old link before it expires, it still works. This avoids a race condition where the AP opens a cached email link. The single-use flag ensures only one token can be consumed.

### Error Pages

Three distinct GDS-compliant error pages:
1. **Expired token**: "This approval link has expired" — advises the applicant to request a resend
2. **Used token**: "This approval link has already been used" — shows the decision outcome
3. **Invalid token**: "This link is not valid" — no details about why (prevents enumeration)

### Audit Integration

All token lifecycle events are recorded in the audit log:
- EVT-03: Approval requested (token created, AP emailed)
- EVT-04: Approval granted (token consumed)
- EVT-05: Approval rejected (token consumed, reason recorded)
- EVT-06: Token expired (recorded by scheduled cleanup)
- EVT-11: Approval resent (new token created)

## Consequences

### Positive
- AP can approve without an account or login — minimal friction
- Database-backed tokens allow immediate revocation and atomic consumption
- 72-hour expiry limits the exposure window for intercepted links
- Resend cap prevents infinite token generation (potential email spam vector)
- Distinct error pages give the AP clear guidance on next steps

### Negative
- Database lookup required on every token validation (no stateless verification)
- Multiple active tokens per registration (after resend) add query complexity
- Expired token cleanup requires a scheduled job

### Risks
- Token brute-force enumeration: mitigated by 32-byte random tokens (2^256 search space) and rate limiting on the approval endpoint
- Email interception: mitigated by 72-hour expiry and single-use enforcement; HTTPS-only links. The email itself is sent via GOV.UK Notify over TLS.

## Alternatives Considered

### HMAC-signed URL tokens (stateless)
- **Pros**: No database lookup; self-contained expiry; faster validation
- **Cons**: Cannot be revoked server-side once issued; no single-use enforcement without a "used token" table (negating the stateless benefit); replay attacks possible within the expiry window
- **Why rejected**: Single-use enforcement is a GDP requirement — a used token must not be accepted again. This requires server-side state, making HMAC's stateless advantage moot.

### JWT tokens in the URL
- **Pros**: Standard format; can embed claims (registration ID, expiry, AP email)
- **Cons**: Same revocation/single-use problem as HMAC; JWTs in URLs can leak via referrer headers and server logs; JWTs are decodable (exposes registration ID and AP email in the URL)
- **Why rejected**: Leaking PII (AP email) in a URL query parameter violates `ukhsa-security.instructions.md`; single-use enforcement still requires server state

### Login-based AP portal
- **Pros**: Full authentication; persistent session; richer AP experience
- **Cons**: Requires AP to have an account (onboarding friction); most APs approve 1–2 registrations per month — a full portal is disproportionate; authentication infrastructure cost for a low-frequency action
- **Why rejected**: The AP approval flow is a low-frequency, high-trust action — a one-time token is proportionate. A full portal would delay alpha delivery without validating the core assumption.

## UKHSA Constraints

- **No PII in URLs**: Token is an opaque random string — no registration ID, applicant name, or AP email encoded in the URL
- **HTTPS only**: Approval links use HTTPS; GOV.UK Notify sends emails over TLS
- **GOV.UK Notify**: Approval email uses a GOV.UK Notify template with the token URL and applicant summary
- **MHRA GDP**: Every token lifecycle event is recorded in the immutable audit log with actor attribution

## References

- [OWASP — Forgot Password Cheat Sheet (token design guidance)](https://cheatsheetseries.owasp.org/cheatsheets/Forgot_Password_Cheat_Sheet.html)
- Story 010 — AP approval email link
- Story 011 — AP decision recording
- Story 013 — Token expiry handling
- Story 014 — Approval resend with cap
- ADR-0003 — Audit trail design (EVT-03, EVT-04, EVT-05, EVT-06, EVT-11)
