---
name: gds-service-standard
description: 'Use when assessing a feature or PR against the GDS Service Standard 14 points, producing NHS Alpha service assessment evidence, or adding the evidence for a slice (alpha readiness issue S09).'
---

# GDS Service Standard — Assessment Evidence

This skill maps repository evidence to the [14-point GDS Service Standard](https://www.gov.uk/service-manual/service-standard) and identifies gaps for NHS Alpha assessment readiness. For the detailed assessment workflow and evidence rules, read [assessment-workflow.md](./references/assessment-workflow.md).

## When to Use

- Preparing for an NHS Alpha or Beta assessment
- Reviewing a PR for Service Standard compliance
- Generating an evidence report showing 14-point coverage
- Identifying gaps in documentation, testing, or accessibility requirements
- Adding the evidence for a slice after it is merged (alpha readiness issue S09)

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
| 14 | Operate a reliable service | Monitoring, `/health` endpoint, alert rules and action group in IaC, runbook, SRE Agent guardrails and incident rehearsal record (if adopted) |

## Output Format

If `docs/gds-assessment.md` does not exist, create it. If it exists, **update it in place** — never rewrite it, because it holds the evidence from earlier slices (see "Updating the report"). Do **not** edit this skill file — it is a reference, not a template to fill in.

Generate `docs/gds-assessment.md` with a table:

```markdown
| # | Standard | Status | Evidence | Gaps |
|---|---|---|---|---|
| 1 | Understand users | ✅ Met / ⚠️ Partial / ❌ Not met | [file links] | [what's missing] |
```

## Updating the report

The report is cumulative. Each slice adds evidence, and the final run before the assessment updates the overall position. Never rewrite the report.

**For slice N** (alpha readiness issue S09):

1. Read the slice issue, its stories, its merged pull request and its readiness issues (S01–S08).
2. Add evidence to every point that the slice changes. Use this map:

   | Evidence from the slice | Point |
   |---|---|
   | The assumption the slice tested, and what the team learned | 1, 2 and 8 |
   | Content review (S06) and NHS Design System use | 3 and 4 |
   | Accessibility audit (S04) | 5 |
   | Clinical safety (S07) and DPIA (S08) input | 6, and the NHS angle |
   | Slice issue, pull request, CI and engineering review | 7 and 8 |
   | Security review (S03) | 9 |
   | Performance check (S05) | 10 |
   | New or changed ADRs | 11 and 13 |
   | Alerts, runbook and deployment changes | 14 |

3. Mark each new or changed entry with the slice, for example `(slice 2)`, so assessors can see what each slice added.
4. Change a status only when the evidence supports it.

**Final position** (end of Day 2, or before the assessment): update each status and gap from all the evidence. Do not remove the slice entries.

**Mocked dependencies.** Read `docs/adr/002-dependency-strategy.md`. For each external mock service that a journey depends on, list it under "Gaps" for points 2 and 13, with the plan and the access request (`AR-` identifier) to move to the real system. A point that depends on a mock cannot be ✅ Met for that journey.

## Rules

- Link to specific files and line numbers — not vague references
- An Alpha assessment accepts partial evidence with a plan to complete
- Always check both GDS standard AND NHS-specific angle (clinical safety, IG, design system)

## References

- [GDS Service Standard](https://www.gov.uk/service-manual/service-standard)
- [NHS service standard](https://service-manual.nhs.uk/standards-and-technology/service-standard-points)
- [NHS Technology Code of Practice](https://service-manual.nhs.uk/standards-and-technology/about-standards-and-technology)
