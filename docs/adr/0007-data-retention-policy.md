# ADR-0007: Data Retention Policy and Scheduled Cleanup

**Status**: Accepted

**Date**: 2026-05-25

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

The ImmForm registration service stores personal data (names, email addresses, job titles, telephone numbers) and audit records subject to UK GDPR data minimisation principles (Art. 5(1)(e)) and MHRA GDP record retention requirements. Different categories of registration data have different retention justifications: wholesaler records must be retained longer for GDP compliance, while abandoned sessions and rejected applications have no ongoing business need.

ADR-0001 established differentiated retention periods. The user stories confirm the need for automated cleanup (Story 013 references token expiry) and the audit trail must record all deletion events (Story 036). A scheduled cleanup mechanism is needed that runs independently of the web application.

**Driven by**: ADR-0001 (NFR-23: data retention schedule), Story 013 (token expiry handling), Story 036 (audit log), UK GDPR Art. 5(1)(e)

## Decision

### Retention Periods

| Category | Retention period | Justification |
|---|---|---|
| Wholesaler registrations (completed) | 5 years from completion | MHRA GDP record retention requirement |
| NHS site registrations (completed) | 3 years from completion | Operational audit and revalidation cycle |
| Rejected registrations | 2 years from rejection | Trend analysis and dispute resolution |
| Expired registrations (token expired, never completed) | 2 years from expiry | Operational reporting |
| Abandoned sessions (Draft status, never submitted) | 6 months from last update | No business need; data minimisation |

### Cleanup Mechanism

An **Azure Function** with a timer trigger runs daily at 02:00 UTC:

1. Queries the Registration table for records past their retention period based on status and account type
2. For each record to be deleted:
   a. Writes an EVT-19 (data retention cleanup) event to the audit log with the registration ID, category, and retention period applied
   b. Deletes the Registration record, associated ApprovalToken records, and associated NotifyLog records
   c. **Does not delete AuditLog records** — audit records are retained independently (see below)
3. Logs a summary of deletions to Application Insights

### Audit Log Retention

AuditLog records are **not deleted** by the scheduled cleanup. They have an independent retention period of **7 years** from creation, aligned with the NHS Records Management Code of Practice for administrative records. A separate cleanup process for audit records will be implemented when the first records approach the 7-year threshold.

### Expired Token Cleanup

Expired ApprovalToken records are cleaned up as part of the parent Registration cleanup. Additionally, tokens that have expired but whose parent Registration is still active (awaiting a resend) are marked with an EVT-06 event in the audit log by the same scheduled function.

### Soft Delete vs Hard Delete

**Hard delete** is used for Registration, ApprovalToken, and NotifyLog records. The audit log provides the permanent record of the registration's existence and lifecycle. Soft delete would retain PII past the retention period, defeating the purpose of data minimisation.

## Consequences

### Positive
- Automated enforcement of UK GDPR data minimisation — no manual intervention required
- Differentiated retention respects MHRA GDP requirements for wholesaler records while minimising PII retention for other categories
- Deletion events recorded in the audit log maintain an evidence trail even after data is removed
- Azure Function runs independently of the web application — cleanup continues even during deployments or outages

### Negative
- Hard delete is irreversible — if a record is deleted prematurely due to misconfigured retention periods, it cannot be recovered (audit log retains the lifecycle but not the full registration data)
- Azure Function adds an infrastructure component (Terraform, deployment, monitoring)
- Differentiated retention logic must correctly determine the account type and status of each registration — business rule complexity

### Risks
- Clock skew or timezone misconfiguration could cause premature or delayed cleanup. Mitigated by: using UTC consistently; running at 02:00 UTC to avoid boundary issues; logging each deletion with timestamps.
- Retention periods may need to change as MHRA guidance evolves. Mitigated by: retention periods are defined in configuration (`appsettings.json`), not hardcoded.

## Alternatives Considered

### Single retention period for all categories
- **Pros**: Simplest implementation; no category-based logic
- **Cons**: Either over-retains PII (applying the longest period to all) or under-retains GDP records (applying the shortest). A single 5-year period keeps abandoned sessions for years longer than necessary; a single 6-month period deletes wholesaler records before the GDP retention requirement.
- **Why rejected**: UK GDPR requires retention proportionate to purpose — a single period cannot satisfy both data minimisation and GDP retention.

### Manual cleanup by admin
- **Pros**: Human oversight on each deletion; no Azure Function needed
- **Cons**: Labour-intensive; prone to being forgotten; no guarantee of timely execution; ICO could consider delayed deletion a GDPR non-compliance
- **Why rejected**: Automated enforcement is required for consistent compliance

### Azure SQL temporal tables (automatic history)
- **Pros**: Built-in historical record; no data loss on delete
- **Cons**: Temporal tables retain all versions of a row indefinitely by default — this conflicts with data minimisation; history cleanup requires additional configuration; does not eliminate the need for a scheduled cleanup job
- **Why rejected**: Temporal tables solve a different problem (point-in-time recovery) — they do not address retention-based deletion

### No retention — keep data forever
- **Pros**: No deletion logic; no risk of premature deletion; simplest implementation
- **Cons**: Violates UK GDPR Art. 5(1)(e) (data minimisation); PII accumulates indefinitely; storage costs grow without bound
- **Why rejected**: Non-compliant with UK GDPR

## UKHSA Constraints

- **UK GDPR Art. 5(1)(e)**: Personal data must not be kept longer than necessary for its purpose
- **UK GDPR Art. 9**: Health-sector registration data is processed under Art. 9 — heightened data protection obligations
- **MHRA GDP**: Wholesaler records must be retained for a minimum of 5 years
- **NHS Records Management Code of Practice**: Administrative records retained for 7 years (applied to audit log)
- **Data sovereignty**: All data stored in Azure UK South; Azure Function runs in UK South

## References

- [UK GDPR Art. 5(1)(e) — Storage limitation](https://www.legislation.gov.uk/eur/2016/679/article/5)
- [NHS Records Management Code of Practice](https://digital.nhs.uk/data-and-information/looking-after-information/data-security-and-information-governance/codes-of-practice-for-handling-information-in-health-and-care/records-management-code-of-practice-for-health-and-social-care-2016)
- [MHRA GDP Guidelines](https://www.gov.uk/guidance/good-distribution-practice-gdp)
- ADR-0001 — System architecture (NFR-23: data retention schedule)
- ADR-0003 — Audit trail design (EVT-19: data retention cleanup)
- Story 013 — Token expiry handling
- Story 036 — Audit log infrastructure
