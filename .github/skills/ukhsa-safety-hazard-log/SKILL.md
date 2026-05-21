---
name: ukhsa-safety-hazard-log
description: 'Use when creating or updating a safety hazard log for a UKHSA regulated workload under MHRA GDP, EU GMP Annex 11, and MHRA GxP Data Integrity (ALCOA+).'
---

# UKHSA Safety Hazard Log — MHRA GxP

This skill drafts and maintains the safety hazard log for UKHSA digital services that fall under regulated scope — MHRA Good Distribution Practice (GDP), EU GMP Annex 11 (Computerised Systems), and MHRA GxP Data Integrity (ALCOA+).

Use the [SIREN methodology](https://www.gov.uk/government/publications/siren-system-incident-reporting-and-emergency-notifications) for hazard identification and category structure.

## When to Use

- Starting a new UKHSA regulated service (e.g. vaccine logistics, lab data, surveillance)
- Adding a new feature that processes regulated data
- Before an MHRA inspection or internal audit
- After a near-miss or incident that affected data integrity or service safety

## Output Location

Create or update `docs/safety/hazard-log.md` using the template in `templates/hazard-log-template.md`. **Do not edit this skill file** — it is a reference.

## SIREN Hazard Categories

| Code | Category | Examples |
|---|---|---|
| **S** | Surveillance / data integrity | Wrong lab result associated with wrong sample; lost audit trail |
| **I** | Information / availability | Service outage during outbreak response |
| **R** | Records / completeness | Missing or unreadable historical record; incomplete batch data |
| **E** | External integration | PHE / NHS / lab system feed corruption |
| **N** | Notification | Alert not raised or raised to wrong recipient |

## Risk Matrix

| Severity \ Likelihood | Very Low | Low | Medium | High | Very High |
|---|---|---|---|---|---|
| **Catastrophic** | Medium | High | High | Very High | Very High |
| **Major** | Medium | Medium | High | High | Very High |
| **Considerable** | Low | Medium | Medium | High | High |
| **Significant** | Low | Low | Medium | Medium | High |
| **Minor** | Low | Low | Low | Medium | Medium |

Severity definitions:
- **Catastrophic** — death or permanent harm; regulatory action; large-scale data integrity failure
- **Major** — serious harm; significant ALCOA+ breach; MHRA reportable
- **Considerable** — moderate harm; localised data integrity issue
- **Significant** — minor harm; recoverable data quality issue
- **Minor** — no harm; cosmetic or low-impact issue

## ALCOA+ Coverage

Every hazard mitigation must address the applicable ALCOA+ attributes:

- **A**ttributable — every action traceable to an authenticated identity
- **L**egible — readable for the full retention period
- **C**ontemporaneous — recorded at the time of the activity
- **O**riginal — the first capture, or a verified true copy
- **A**ccurate — correct, complete, and current
- **+** Complete, Consistent, Enduring, Available

## Annex 11 Mapping

Cross-reference each hazard to the relevant EU GMP Annex 11 clause (e.g. 4.8 data security, 7.1 audit trail, 9 closed systems, 12.4 incident management). The hazard log table includes an `Annex 11 Ref` column for this.

## Sign-off

The hazard log must be reviewed and signed off by:
- **Safety Officer** — accountable for safety case
- **SIRO** (Senior Information Risk Owner)
- **Caldicott Guardian** (if personal/health data is in scope)
- **Quality Lead** (for MHRA GxP scope)

## Rules

- Every regulated user story or ADR that changes data flows must trigger a hazard log review.
- Residual risk above **Medium** must have a documented acceptance from the Safety Officer.
- Every mitigation must have an owner and a target date.
- Hazards are never removed — closed hazards are marked `Closed` with a closure note and date.
- The hazard log is version-controlled in the same repository as the service it covers.

## References

- [MHRA GxP Data Integrity Guidance](https://www.gov.uk/government/publications/guidance-on-gxp-data-integrity)
- [EU GMP Annex 11 — Computerised Systems](https://health.ec.europa.eu/system/files/2016-11/annex11_01-2011_en_0.pdf)
- [MHRA Good Distribution Practice](https://www.gov.uk/guidance/apply-for-manufacturer-or-wholesaler-of-medicines-licences)
- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
