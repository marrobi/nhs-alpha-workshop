# ADR-0003: MHRA GDP Audit Trail — Two-Schema Immutable Design

**Status**: Accepted

**Date**: 2026-05-25

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

The ImmForm registration service operates within a regulated medicinal product supply chain. MHRA GDP (Good Distribution Practice) requires that every state transition in the registration lifecycle is recorded with named-individual attribution, and that audit records cannot be modified or deleted by the application. The QA/WDA Responsible Person (Rachel) must be able to independently retrieve and export complete lifecycle evidence for MHRA inspection without helpdesk involvement.

ADR-0001 established the principle of audit log isolation using two SQL schemas. The user stories now reveal the full scope: 19 event types (EVT-01 through EVT-19) with actor attribution, SHA-256 payload checksums for integrity verification, anomaly detection (checksum mismatches, incomplete sequences, missing reasons), structured CSV export for evidence packages, and differentiated retention periods by account type.

**Driven by**: Story 036 (audit log infrastructure), Story 024 (audit timeline), Story 025 (audit export), Story 026 (anomaly detection), Story 022 (manual override traceability), Story 032 (GDP confirmation)

## Decision

### Database Schema Isolation

Single Azure SQL database with two schemas and two EF Core `DbContext` instances using different SQL users:

- **`dbo` schema** — Registration, ApprovalToken tables. Application service account has full CRUD.
- **`audit` schema** — AuditLog, NotifyLog tables. Application service account has **INSERT-only** permission. No UPDATE, no DELETE.

### AuditLog Table

Every registration state transition writes an AuditLog record containing: RegistrationId, CorrelationId, EventType (EVT-01–19), Timestamp (UTC), ActorType (System, Applicant, Manager, Admin, QaRp), ActorId (email or system identifier), PreviousState, NewState, Detail (JSON), and HashedIPAddress (SHA-256).

### NotifyLog Table

Every GOV.UK Notify dispatch writes a NotifyLog record containing: RegistrationId, CorrelationId, TemplateId, RecipientType, DispatchTimestamp, and Status.

### Payload Checksum (NFR-14)

At declaration submission, a SHA-256 hash of the serialised submission payload is computed and stored as `PayloadChecksum` on the Registration record and included in the EVT-02 audit event. The anomaly detection endpoint (Story 026) compares stored checksums against recomputed values to detect data tampering.

### IP Address Hashing

Client IP addresses are hashed with SHA-256 before storage in the AuditLog. The original IP is never persisted. Hashed values are displayed as-is in the audit interface — they are not reversible.

### Anomaly Detection (NFR-22)

The audit interface exposes GET `/api/audit/anomalies` which flags registrations with: payload checksum mismatches, incomplete event sequences (missing expected state transitions), manual overrides without reasons (EVT-16 records with empty reason), and expired tokens that were never acted upon.

### Evidence Export

GET `/api/audit/registrations/{id}/export` generates a CSV file containing the complete event chain for a registration, suitable for MHRA inspection evidence packs. The export action itself is logged to the audit trail.

## Consequences

### Positive
- Application code cannot modify or delete audit records — satisfies MHRA GDP NFR-13
- Named-individual attribution at every state transition supports accountability
- QA/WDA RP can independently search, view, and export evidence without helpdesk involvement
- Anomaly detection proactively identifies data integrity issues before inspection
- Single database keeps infrastructure simple for alpha while providing strong logical isolation

### Negative
- Two `DbContext` instances add complexity to the EF Core configuration and DI registration
- Two SQL users require Terraform configuration for permission grants
- INSERT-only constraint means audit corrections require a new compensating event, not an update to the original record

### Risks
- If the SQL permission configuration is incorrect, the application could gain UPDATE/DELETE on audit tables. Mitigated by: Terraform-managed permission grants, integration tests that verify permission denial, and infrastructure security review.

## Alternatives Considered

### Two separate Azure SQL databases
- **Pros**: Strongest physical separation; independent backup and scaling
- **Cons**: Higher cost; more complex Terraform; cross-database queries for admin views require linked servers or application-level joins
- **Why rejected**: SQL-level permission separation is auditable and sufficient for alpha; upgrade to separate databases is straightforward if needed in beta

### Application-level soft delete with IsDeleted flag
- **Pros**: Simpler SQL setup; single schema
- **Cons**: Application bugs could still delete or modify records; does not satisfy MHRA GDP requirement for tamper-evident audit trail; relies on application discipline rather than database enforcement
- **Why rejected**: MHRA GDP requires database-level enforcement, not application-level convention

### Append-only event store (e.g. EventStoreDB, Azure Event Hubs)
- **Pros**: Purpose-built for event sourcing; natural immutability
- **Cons**: Additional infrastructure component; team unfamiliar with event sourcing patterns; overkill for the event volume (hundreds/day, not millions)
- **Why rejected**: Azure SQL with INSERT-only permissions achieves the same immutability with familiar technology

## UKHSA Constraints

- **MHRA GDP compliance**: Immutable audit trail with named-individual attribution at every state transition. Application service account has no DELETE or UPDATE on audit tables.
- **Data retention**: Wholesaler records retained for 5 years, NHS site records for 3 years, rejected applications for 2 years, expired applications for 2 years, abandoned sessions for 6 months. Enforced by a scheduled Azure Function cleanup job. Deletion events are themselves recorded in the audit log.
- **UK GDPR**: Actor identities (email addresses) stored in audit log as required for GDP traceability. Access restricted to `ImmFormAdmin` and `ImmFormQaRp` Entra ID roles.
- **Data sovereignty**: All audit data stored in Azure SQL in UK South region.

## References

- [MHRA GDP Guidelines — Chapter 6: Documentation](https://www.gov.uk/guidance/good-distribution-practice-gdp)
- Story 036 — Immutable audit log infrastructure
- Story 024 — Audit registration timeline with actor attribution
- Story 025 — Audit evidence export
- Story 026 — Audit anomaly detection and flagging
- ADR-0001 — System architecture (Decision 1: Audit Log Isolation)
