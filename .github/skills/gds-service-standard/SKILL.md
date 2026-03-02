---
name: gds-service-standard
description: 'Use when assessing a feature or PR against the GDS Service Standard 14 points, or producing NHS Alpha service assessment evidence.'
---

# GDS Service Standard — Assessment Evidence

This skill maps repository evidence to the [14-point GDS Service Standard](https://www.gov.uk/service-manual/service-standard) and identifies gaps for NHS Alpha assessment readiness.

## When to Use

- Preparing for an NHS Alpha or Beta assessment
- Reviewing a PR for Service Standard compliance
- Generating an evidence report showing 14-point coverage
- Identifying gaps in documentation, testing, or accessibility requirements

## The 14 Points — Evidence Checklist

| # | Standard | What to Look For |
|---|---|---|
| 1 | Understand users and their needs | User research artefacts, personas, user stories |
| 2 | Solve a whole problem for users | Service map, end-to-end journey |
| 3 | Provide a joined-up experience | Consistent with NHS.UK, NHS App |
| 4 | Make the service simple to use | NHS Design System components, one-thing-per-page |
| 5 | Make sure everyone can use the service | WCAG 2.2 AA, axe-core results, assistive tech testing |
| 6 | Have a multidisciplinary team | Team roles documented, clinical + IG input |
| 7 | Use agile ways of working | Sprints, backlog (GitHub Issues), retros |
| 8 | Iterate and improve frequently | Git log shows iterative commits, not big-bang |
| 9 | Create a secure service | Security headers, dependency scanning, Key Vault |
| 10 | Define what success looks like | KPIs, performance targets, k6 thresholds |
| 11 | Choose the right tools and technology | ADRs explaining technology choices |
| 12 | Make new source code open | Public repo, open licence |
| 13 | Use and contribute to open standards | FHIR, OpenAPI, NHS Data Dictionary |
| 14 | Operate a reliable service | Monitoring, `/health` endpoint, runbook |

## Output Format

Generate `docs/gds-assessment.md` with a table:

```markdown
| # | Standard | Status | Evidence | Gaps |
|---|---|---|---|---|
| 1 | Understand users | ✅ Met / ⚠️ Partial / ❌ Not met | [file links] | [what's missing] |
```

## Rules

- Link to specific files and line numbers — not vague references
- An Alpha assessment accepts partial evidence with a plan to complete
- Always check both GDS standard AND NHS-specific angle (clinical safety, IG, design system)

## References

- [GDS Service Standard](https://www.gov.uk/service-manual/service-standard)
- [NHS service standard](https://service-manual.nhs.uk/standards-and-technology/service-standard-points)
- [NHS Technology Code of Practice](https://service-manual.nhs.uk/standards-and-technology/about-standards-and-technology)
