---
name: gds-service-standard
description: 'Use when assessing a UKHSA digital service against the 14-point GDS Service Standard ahead of an Alpha, Beta, or Live assessment.'
---

# GDS Service Standard — UKHSA Assessment

This skill produces a GDS Service Standard assessment for UKHSA services. The standard applies to all government services and is the basis for Alpha/Beta/Live spend approval.

## When to Use

- Preparing for an Alpha, Beta, or Live assessment
- Internal pre-assessment / readiness review
- Auditing an existing service against the standard

## Output

Create or update `docs/gds-assessment.md`. Include one section per point with evidence references (links to ADRs, research artefacts, monitoring dashboards, etc.).

## The 14 Points

| # | Point | Evidence to look for |
|---|---|---|
| 1 | Understand users and their needs | User research artefacts, personas (see `ukhsa-user-stories`), accessibility research |
| 2 | Solve a whole problem | Service blueprint, end-to-end journey maps |
| 3 | Provide a joined-up experience across all channels | Integration with phone/post/in-person channels documented |
| 4 | Make the service simple to use | Usability test results, task completion rates |
| 5 | Make sure everyone can use the service | WCAG 2.2 AA audit (axe-core results), assisted-digital plan |
| 6 | Have a multidisciplinary team | Team composition (delivery, design, research, eng, policy, ops) |
| 7 | Use agile ways of working | Sprint cadence, retro outputs, backlog discipline |
| 8 | Iterate and improve frequently | Deployment frequency, change lead time |
| 9 | Create a secure service which protects users' privacy | DPIA (see `ukhsa-dpia`), threat model, NCSC CAF mapping |
| 10 | Define what success looks like and publish performance data | KPI definitions, performance dashboard |
| 11 | Choose the right tools and technology | ADRs (see `ukhsa-adr-writer`), tech stack rationale |
| 12 | Make new source code open | Repo visibility, OSS licence, dependency provenance |
| 13 | Use and contribute to open standards, common components and patterns | FHIR, OpenAPI, GOV.UK Design System, NHS Data Dictionary where relevant |
| 14 | Operate a reliable service | SLOs, on-call rota, runbooks, incident process |

## UKHSA-Specific Evidence

| Area | UKHSA additions |
|---|---|
| Point 5 (accessibility) | Reference UKHSA Engineering Standards accessibility expectations and the GOV.UK Design System |
| Point 9 (security/privacy) | DPIA + Safety Hazard Log + NCSC CAF + Cyber Essentials Plus |
| Point 11 (tech choices) | ADRs justify .NET 10 / ASP.NET Core / EF Core / Azure UK South choices |
| Point 13 (open standards) | NHS Number ISB 0149, FHIR UK Core, GOV.UK design patterns |
| Point 14 (reliability) | Application Insights dashboards, Azure Service Health subscriptions, DR plan to UK West |

## Assessment Output Structure

```
# GDS Service Standard Assessment — [Service Name]

**Stage**: Alpha / Beta / Live
**Date**: YYYY-MM-DD
**Assessment lead**: [Name]

## Summary
- Strengths
- Areas to improve
- Recommendation

## Point-by-point evidence
### 1. Understand users and their needs
- Evidence: [links / artefacts]
- Notes: [text]

…

## Actions before next stage
| # | Action | Owner | Target |
```

## Rules

- Every point gets a section, even if "not yet applicable" — say so explicitly.
- Evidence links resolve from within the repo or to authoritative internal systems — never to private cloud drives.
- Pre-assessment must happen at least 2 weeks before the formal assessment date.
- Assessment outcomes feed back into the backlog as tracked work.

## References

- [GDS Service Standard](https://www.gov.uk/service-manual/service-standard)
- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
- [NCSC CAF](https://www.ncsc.gov.uk/collection/caf)
