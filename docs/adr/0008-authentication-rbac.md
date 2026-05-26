# ADR-0008: Authentication and Role-Based Access Control

**Status**: Accepted

**Date**: 2026-05-25

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

The ImmForm registration service has three distinct access tiers with different security requirements:

1. **Applicants** — external users completing the registration form. They are not authenticated and have no system account.
2. **Authorised Persons (APs)** — external contacts who approve or reject registrations via a one-time email link. They do not log in.
3. **Internal staff** — helpdesk operatives (admin dashboard, manual overrides, resend approvals) and QA/WDA Responsible Persons (audit interface, evidence export, anomaly review).

The user stories reveal that internal staff have two distinct roles with different permissions: admin (read-write on registrations) and audit (read-only on audit trail). This requires a role-based access control model that integrates with the UKHSA identity provider.

**Driven by**: Stories 019–023 (admin dashboard), Story 022 (manual override), Stories 024–026 (audit interface), Story 011 (AP decision), Story 010 (AP approval link)

## Decision

### Three Access Tiers

| Tier | Users | Mechanism | Scope |
|---|---|---|---|
| **Unauthenticated** | Applicants | No authentication | Registration form (MVC routes) only |
| **Token-gated** | Authorised Persons | Opaque database-backed token (see ADR-0004) | AP decision page (`/registration/approval/{token}`) only |
| **Entra ID authenticated** | Helpdesk operatives, QA/WDA RPs | Microsoft Entra ID (Azure AD) with role claims | Admin dashboard API and audit interface API |

### Entra ID Integration

Internal staff authenticate via Microsoft Entra ID using OpenID Connect (OIDC). The ASP.NET Core authentication middleware validates the JWT token and extracts role claims.

**App Registration** in Entra ID defines two application roles:

| Role claim | Name | Permissions |
|---|---|---|
| `ImmFormAdmin` | Helpdesk Administrator | Read all registrations, update status, manual override, resend approval, add notes |
| `ImmFormQaRp` | QA/WDA Responsible Person | Read audit trail, search registrations (read-only), export evidence, view anomalies |

Roles are assigned to Entra ID security groups. Users inherit roles through group membership.

### Authorization Enforcement

- **MVC controllers** (registration form): no `[Authorize]` attribute — applicant-facing routes are public
- **AP approval routes**: authorization is enforced by token validation logic (ADR-0004), not by ASP.NET Core `[Authorize]`
- **Admin API controllers**: `[Authorize(Roles = "ImmFormAdmin")]` on all endpoints
- **Audit API controllers**: `[Authorize(Roles = "ImmFormQaRp")]` on all endpoints; `ImmFormAdmin` does **not** have implicit audit access — separation of duties
- **Manual override endpoint**: `[Authorize(Roles = "ImmFormAdmin")]` with additional audit logging of the admin's identity (EVT-16)

### Actor Identity in Audit Trail

All audit events record the actor identity:
- Applicant actions: `ActorType = "Applicant"`, `ActorId = email address`
- AP actions: `ActorType = "Manager"`, `ActorId = AP email address` (from Registration record)
- Admin actions: `ActorType = "Admin"`, `ActorId = Entra ID UPN` (from JWT `preferred_username` claim)
- QA/WDA RP actions: `ActorType = "QaRp"`, `ActorId = Entra ID UPN`
- System actions: `ActorType = "System"`, `ActorId = "ImmForm.Api"` or `"ImmForm.Cleanup"`

### Session Configuration

- Applicant sessions: Redis-backed, HttpOnly/Secure/SameSite=Strict cookie, 60-minute inactivity timeout
- Admin/Audit sessions: Entra ID OIDC session with sliding expiration, protected by the same cookie policy
- AP "sessions": stateless — the token URL is the entire session context

## Consequences

### Positive
- Clear separation of duties: admin cannot access audit interface, QA/WDA RP cannot modify registrations
- Entra ID integration uses UKHSA's existing identity infrastructure — no new user directory to manage
- Role claims in JWT enable stateless authorization checks on every API request
- Named-individual attribution in audit log satisfies MHRA GDP requirements
- Applicants and APs have zero authentication friction — appropriate for their interaction frequency

### Negative
- Entra ID app registration and role assignment require Azure AD admin access — initial setup is a manual step
- Two-role model may need to expand in future (e.g. read-only helpdesk, team lead with approval authority) — role structure must be extensible
- Mock authentication in local development must still enforce authorization logic — cannot skip role checks

### Risks
- If role claims are misconfigured in Entra ID, staff may be unable to access their dashboards. Mitigated by: integration tests that verify role-based access; clear documentation of the app registration setup in the deployment runbook.
- AP token-based access is not authenticated in the traditional sense — if the token is intercepted, anyone with the link can act as the AP. Mitigated by: 72-hour expiry, single-use enforcement, HTTPS-only (see ADR-0004).

## Alternatives Considered

### Cookie-based authentication with local user store
- **Pros**: No dependency on Entra ID; self-contained; works offline
- **Cons**: Requires user management (create, disable, password reset); password storage and hashing; session management; does not leverage UKHSA's existing identity infrastructure; duplicates identity management
- **Why rejected**: UKHSA staff already have Entra ID accounts — building a separate identity system creates unnecessary security surface and operational burden

### Single admin role for all internal staff
- **Pros**: Simpler role model; fewer Entra ID groups to manage
- **Cons**: No separation of duties — helpdesk operatives could view and export audit evidence; QA/WDA RPs could modify registration status. This violates the MHRA GDP principle that the QA/WDA RP should independently audit the registration process.
- **Why rejected**: MHRA GDP requires that the QA/WDA RP has independent, read-only access to the audit trail — combining roles undermines this independence

### NHS CIS2 (Care Identity Service 2) integration
- **Pros**: Standard NHS identity; smartcard-based; strong authentication
- **Cons**: Requires NHS CIS2 onboarding (lengthy process); not all UKHSA staff have CIS2 credentials; CIS2 is designed for clinical access, not administrative systems; overkill for an internal admin dashboard
- **Why rejected**: Disproportionate for the use case — UKHSA staff authenticate via Entra ID; CIS2 is appropriate for clinical systems accessing patient records, not registration admin dashboards

## UKHSA Constraints

- **Entra ID**: UKHSA uses Microsoft Entra ID as its identity provider for staff — the service must integrate with this, not introduce a separate identity system
- **MHRA GDP**: Named-individual attribution required at every state transition — ActorId must be a real person's identity (UPN or email), not a generic service account
- **Separation of duties**: QA/WDA RP must have independent read-only access to audit data — their role must not include write permissions on registrations
- **No PII in logs**: Entra ID UPNs (which are email addresses) may appear in audit records but must not appear in application logs — see `ukhsa-security.instructions.md`

## References

- [Microsoft Entra ID — Add app roles and get them from a token](https://learn.microsoft.com/en-us/entra/identity-platform/howto-add-app-roles-in-apps)
- [ASP.NET Core — Role-based authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles)
- [MHRA GDP — Named individual accountability](https://www.gov.uk/guidance/good-distribution-practice-gdp)
- Story 019 — Admin registration search and filter
- Story 022 — Admin manual override with audit trail
- Story 024 — Audit registration timeline
- ADR-0004 — AP approval token lifecycle
- ADR-0003 — Audit trail design (ActorType, ActorId)
