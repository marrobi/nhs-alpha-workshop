# ADR-0006: Shared Mailbox Detection and GDP Email Policy

**Status**: Accepted

**Date**: 2026-05-25

**Deciders**: UKHSA ImmForm Technical Services, Architecture Workshop Team

## Context

MHRA GDP requires named-individual attribution for all actions in the medicinal product supply chain. Registrations submitted from shared or generic mailboxes (e.g. `info@`, `admin@`, `orders@`, `pharmacy@`) cannot identify a named individual, which creates a GDP compliance risk for wholesaler accounts.

Story 027 requires shared mailbox detection with differentiated handling: NHS applicants receive a hard block (shared mailboxes are not permitted), while wholesaler applicants receive a GDP policy warning that can be overridden with explicit acknowledgement. Story 031 requires the GDP compliance confirmation screen with a checkbox at the declaration step.

The detection must be configurable — new patterns can be added without code changes — and the override decision must be recorded in the audit trail.

**Driven by**: Story 027 (shared mailbox detection), Story 031 (GDP compliance confirmation), Story 032 (GDP checkbox at declaration)

## Decision

### Detection Mechanism

A configurable list of email prefix patterns (loaded from `appsettings.json`) is matched against the local part of the applicant's email address:

```json
{
  "SharedMailbox": {
    "Patterns": ["info", "admin", "orders", "pharmacy", "reception", "enquiries", "office", "noreply", "helpdesk", "support", "team", "generic", "shared"]
  }
}
```

Pattern matching is case-insensitive and matches the exact local part (before the `@`). Substring matching is not used — `admin@example.com` matches, but `administrator@example.com` does not. The pattern list can be extended via configuration without a code deployment.

### Differentiated Handling

| Account type | Shared mailbox detected | Behaviour |
|---|---|---|
| NHS site | Yes | **Hard block** — validation error: "Enter a personal email address. Shared mailboxes cannot be used for NHS registrations." The form cannot proceed. |
| Wholesaler | Yes | **GDP warning** — the form proceeds to a GDP compliance warning page explaining the risk. The applicant must explicitly check a "I understand this may affect GDP compliance" checkbox to continue. The override is recorded in the audit trail (EVT-15). |
| Any | No | No intervention — the form proceeds normally. |

### Audit Integration

- EVT-15 (shared mailbox override): logged when a wholesaler applicant acknowledges the GDP warning and proceeds with a shared mailbox. Records the email address pattern matched and the applicant's acknowledgement.
- The GDP compliance confirmation checkbox state is captured in the Registration entity (`GdpOverrideAcknowledged` boolean flag).

### Declaration Step Integration

Story 032 adds a GDP-specific checkbox to the declaration step for wholesaler accounts. This is distinct from the shared mailbox override — it applies to all wholesaler registrations regardless of email type. The checkbox text confirms the applicant understands their obligations under MHRA GDP guidelines.

## Consequences

### Positive
- Named-individual attribution is enforced for NHS registrations (hard block)
- Wholesaler applicants are informed of GDP risk but not blocked — avoids disrupting legitimate business processes that use shared inboxes
- Override decisions are auditable — QA/WDA RP can identify registrations that proceeded despite shared mailbox detection
- Pattern list is configurable without code changes — new patterns can be added as they are identified

### Negative
- Pattern matching may produce false positives (e.g. a person named "Admin Singh" with `admin@` email) or false negatives (patterns not yet in the list)
- Wholesaler override flow adds an extra step to the journey — slight friction for legitimate shared mailbox users
- Configuration change requires application restart to reload `appsettings.json` (unless `IOptionsMonitor<T>` is used for hot reload)

### Risks
- Incomplete pattern list may miss some shared mailboxes. Mitigated by: the anomaly detection endpoint can flag registrations from emails matching common generic patterns not in the configured list; the pattern list can be extended operationally.

## Alternatives Considered

### No detection — accept any email address
- **Pros**: Simplest implementation; no false positives; no extra journey steps
- **Cons**: GDP non-compliance risk for wholesaler registrations; no audit trail for named-individual attribution failures; MHRA inspection finding
- **Why rejected**: GDP requires named-individual attribution — accepting shared mailboxes without any control is a compliance gap

### Hard block for all account types
- **Pros**: Strongest enforcement; simplest logic (no differentiated handling)
- **Cons**: Many legitimate wholesaler operations use shared mailboxes for ordering; hard blocking would prevent valid registrations and create helpdesk escalations
- **Why rejected**: Disproportionate — wholesalers have legitimate reasons for shared mailboxes; a warning with override is the appropriate risk-based control

### Manual admin review of all flagged emails
- **Pros**: Human judgement on each case; no false-positive risk
- **Cons**: Delays registration; requires admin queue and workflow; does not scale; adds helpdesk burden
- **Why rejected**: Adds complexity and delay without proportionate benefit — the applicant's explicit acknowledgement is sufficient for audit purposes

## UKHSA Constraints

- **MHRA GDP**: Named-individual attribution required for wholesaler supply chain actions. Shared mailbox override must be recorded in the immutable audit log.
- **GDS error patterns**: Hard block displays as a standard GDS validation error. GDP warning page uses GDS warning text component.
- **WCAG 2.2 AA**: GDP warning page and checkbox must be keyboard-accessible and screen-reader-compatible.

## References

- [MHRA GDP Guidelines — Chapter 1: Quality Management (named individual accountability)](https://www.gov.uk/guidance/good-distribution-practice-gdp)
- Story 027 — Shared mailbox detection
- Story 031 — GDP compliance confirmation screen
- Story 032 — GDP-specific declaration checkbox
- ADR-0003 — Audit trail design (EVT-15)
