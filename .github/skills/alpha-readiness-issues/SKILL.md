---
name: alpha-readiness-issues
description: 'Use when the service is built and you need to plan the hardening and assessment work as GitHub issues: CI/CD, tests, reviews, security, the optional alerting and Azure SRE Agent issue, runbook, clinical safety, DPIA and GDS evidence. Tailors each issue to the actual tech stack, architecture and code, and gives the role agent and skill for each issue.'
---

# Alpha Readiness Issues

Plan the GitHub issues that harden the alpha and collect the assessment evidence. Each issue is tailored to the **actual tech stack, architecture, and service** that the team built — never generic.

You produce issue template files — you do **not** write application code, tests, or infrastructure.

## Delivery mode

Read `.github/instructions/delivery-mode.instructions.md`.

- **`workshop-2-day`**: prepare all issues for Day 2 of the workshop. The team works through them in the order of the table.
- **`multi-week`**: propose a sprint for each issue. Use the sprint length and keep the order of the table. Put issue 01 in the first hardening sprint. Show the proposal and wait for confirmation. Record the sprint in the Sprint section of each issue file.

## Prerequisites

The design and build phases must be complete:
- `.github/instructions/tech-stack.instructions.md` — tech stack choices
- `.github/instructions/org-standards.instructions.md` — organisational policies (if defined)
- `docs/adr/001-architecture.md` — agreed architecture
- `user_stories/story-*.md` — user stories
- Application code in the repository

## Issues

| # | Issue | Role agent | Skill |
|---|---|---|---|
| 01 | CI/CD Pipeline | NHS Service Builder | `github-actions-pipeline` |
| 02 | Unit & Integration Tests | NHS Quality Engineer | `automated-testing` |
| 03 | Playwright E2E Tests | NHS Quality Engineer | `playwright-nhs-e2e` |
| 04 | Code Quality Review | NHS Quality Engineer | `code-quality-review` |
| 05 | Security Hardening | NHS Quality Engineer | `security-review` |
| 06 | Alerting & Azure SRE Agent _(optional)_ | NHS Service Builder | `azure-sre-agent` |
| 07 | Azure Infra Security Review | NHS Quality Engineer | `azure-infrastructure-review` |
| 08 | Accessibility Audit | NHS Quality Engineer | `accessibility-audit` |
| 09 | Performance Load Tests | NHS Quality Engineer | `performance-testing` |
| 10 | Runbook & Deployment Docs | NHS Service Builder | `azure-nhs-deploy` |
| 11 | DCB0129 Clinical Safety | NHS Clinical Safety | `dcb0129-hazard-log` |
| 12 | DPIA | NHS DPIA Advisor | `nhs-dpia` |
| 13 | GDS Assessment Evidence | NHS Quality Engineer | `gds-service-standard` |

## Workflow

### Step 1 — Read the Codebase

Read `tech-stack.instructions.md`, `org-standards.instructions.md`, the architecture ADR, user stories, and scan actual code (routers, pages, config). Summarise findings and **wait for user confirmation** before proceeding.

### Step 2 — Generate Issues

For each issue, read the role agent and skill listed above, then write acceptance criteria using **actual** tools, commands, file paths, and frameworks from the tech stack.

Save each to `docs/alpha-readiness-issues/NN-short-slug.md`:

```markdown
## [Issue Title]

### Goal
[One sentence describing what this achieves]

### Acceptance Criteria
- [ ] [Specific, testable criterion using actual tech stack tools]

### Labels
`label1`, `label2`

### Sprint
[multi-week mode only: the proposed sprint]
```

### Step 3 — Present Summary and Create on GitHub

Present the issue titles and role agent/skill assignments from above. In `multi-week` mode, also present the proposed sprint for each issue. **Wait for user confirmation** before creating on GitHub.

Use `gh issue create --title "..." --body-file docs/alpha-readiness-issues/NN-slug.md --label "..."` for each issue in order (01–13). Do **not** use `--assignee` — the user will assign Copilot with the listed role agent on GitHub.

### Issue 06 — Alerting & Azure SRE Agent (optional)

Ask the user whether the team has Azure SRE Agent access in its subscription. Record the answer at the top of issue 06 as **Variant: Alerting and SRE Agent** or **Variant: Alerting only**. Use that variant for issue 06 and for the later issues.

Read `.github/skills/azure-sre-agent/SKILL.md` and its [build workflow](../azure-sre-agent/references/build-workflow.md). Map every alert to the hosting platform, database and monitoring resources that `infra/` **actually** deploys.

**Alerting only.** Acceptance criteria must:
- Add the alert rules and action group, with plan-time `terraform test` coverage, as in Step 3 of the build workflow
- Record the decision in an ADR, with "Azure SRE Agent: not adopted, no access" as a rejected or deferred option
- Create the operations section the alerts link to (for example `docs/runbook.md`)
- Not add the `sre-agent` module, the `sre_agent_enabled` variable, agent configuration, scripts or operator rehearsal

**Alerting and SRE Agent.** Name the alpha environment that will have the agent enabled. Acceptance criteria must:
- Split what the Copilot Coding Agent delivers in the PR (ADR, Terraform, config files, scripts, offline tests, runbook section) from **operator steps** done after merge (apply the plan, prove private telemetry access, apply and verify agent configuration, rehearse one fault)
- Include the guardrails from the skill as testable criteria (UK region, approved model provider, `Low` access, `Review` mode, read-only roles, no public telemetry, required Agent Unit cap, feature flag)
- Require plan-time `terraform test` coverage and an offline CI test for the agent configuration
- Create the operations section the alerts and agent skills link to (for example `docs/runbook.md`); it is written before the Runbook issue
- Hand off to the NHS DPIA Advisor, NHS Clinical Safety and NHS Architect agents

Because issue 06 comes first, make the later issues build on it:

| Issue | Both variants | Alerting and SRE Agent only |
|---|---|---|
| 07 Infra security review | Audit the alert rules and action group | Audit the agent's roles, subnet, NSG, region and access level |
| 10 Runbook | Extend the operations section from 06; do not replace it | Keep the agent section: open, guardrails, disable, delete threads, rehearsal record |
| 11 Clinical safety | Cite alerts and the runbook as detection controls for "service unavailable" hazards | Add the hazard "agent proposes an incorrect cause or remediation" |
| 12 DPIA | — | Record Microsoft as processor for agent conversations, storage region and inference location |
| 13 GDS evidence | Use alerts and the runbook for point 14 | Use the rehearsal record and guardrails for point 14 |

If issue 06 is skipped completely, issue 10 creates the operations section, and none of the later issues refer to alerts from 06 or to the SRE Agent.

## Rules

- **Do not assign issues** — create issues without `--assignee`; the user assigns Copilot manually
- **Read the tech stack, don't guess** — every tool/command/framework reference must come from `tech-stack.instructions.md` or the actual codebase
- **Read organisational standards** — read `.github/instructions/org-standards.instructions.md` for organisational policies. Generate issues that reference org standards for acceptance criteria (e.g. coverage thresholds, secret scanning, deployment strategy). Standards defined in org-standards take precedence over values that may be defined anywhere else in the repository.
- **Read the role and skill files** — each issue's acceptance criteria should align with the assigned role and skill
- **Save first, then summarise** — never type out full issue content in chat
- **One file per issue** — independently copyable into a GitHub Issue
- **No hardcoded technology references** — if you can't find the tech choice, ask the user
