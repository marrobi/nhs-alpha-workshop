# Story 036 — Immutable Audit Log Infrastructure

**Journey**: All journeys — supports audit, compliance, and anomaly detection
**Priority**: 1 (Wave 1 — prerequisite for all state transitions and audit stories)

## User Story

As a UKHSA development team,
We need the two-schema database design with INSERT-only audit tables, 19 event types, and application-level service account restrictions,
So that every registration state transition is immutably recorded and the audit trail is tamper-evident for MHRA GDP compliance and the QA/WDA RP audit interface.

## Acceptance Criteria

### Functional
- [ ] Given the database has two schemas: `dbo` (full CRUD for Registration and ApprovalToken tables) and `audit` (INSERT-only for AuditLog and NotifyLog tables), then the application service account has no DELETE or UPDATE permissions on the `audit` schema
- [ ] Given the AuditLog table, then each record contains: Id, RegistrationId, CorrelationId, EventType, Timestamp (UTC), ActorType (System, Applicant, Manager, Admin, QaRp), ActorId, PreviousState, NewState, Detail (JSON), HashedIPAddress
- [ ] Given the NotifyLog table, then each record contains: Id, RegistrationId, CorrelationId, TemplateId, RecipientType, DispatchTimestamp, Status
- [ ] Given all 19 lifecycle events are defined (EVT-01 through EVT-19), then every state transition in the registration lifecycle writes an AuditLog record with the appropriate event type
- [ ] Given a GOV.UK Notify email is dispatched, then a NotifyLog record is written with the template ID, recipient type, and dispatch timestamp
- [ ] Given the audit log uses INSERT-only semantics, then no application code or migration can update or delete existing audit records
- [ ] Given EF Core is the ORM, then all audit writes use parameterised queries — no raw SQL with string interpolation
- [ ] Given the HashedIPAddress is stored, then the IP address is hashed with SHA-256 before storage — the original IP is never persisted

### Accessibility
- [ ] N/A — database infrastructure has no user interface

### Clinical Safety
- [ ] N/A — audit infrastructure is an administrative and compliance concern

### Data Protection
- [ ] IP addresses are hashed (SHA-256) before storage — they cannot be reversed
- [ ] Actor identities (email addresses for Admin and QaRp actors) are stored in the audit log as required for GDP traceability — access to these records is restricted to `ImmFormAdmin` and `ImmFormQaRp` roles
- [ ] Audit data is retained for 7 years per the MHRA GDP requirement defined in the architecture ADR
