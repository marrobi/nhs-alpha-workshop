---
name: day2-issue-planning
description: 'Use at the start of Day 2 to plan GitHub issues from the actual NHS service, tech stack, architecture, and role-based agent/skill assignments.'
---

# Day 2 Issue Planning

Plan the GitHub issues for Day 2 of the NHS Alpha workshop. Each issue is tailored to the **actual tech stack, architecture, and service** built on Day 1 — never generic.

You produce issue template files — you do **not** write application code, tests, or infrastructure.

## Prerequisites

Day 1 must be complete:
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
| 06 | Azure Infra Security Review | NHS Quality Engineer | `azure-infrastructure-review` |
| 07 | Accessibility Audit | NHS Quality Engineer | `accessibility-audit` |
| 08 | Performance Load Tests | NHS Quality Engineer | `performance-testing` |
| 09 | DCB0129 Clinical Safety | NHS Clinical Safety | `dcb0129-hazard-log` |
| 10 | DPIA | NHS DPIA Advisor | `nhs-dpia` |
| 11 | GDS Assessment Evidence | NHS Quality Engineer | `gds-service-standard` |
| 12 | Runbook & Deployment Docs | NHS Service Builder | `azure-nhs-deploy` |

## Workflow

### Step 1 — Read the Codebase

Read `tech-stack.instructions.md`, `org-standards.instructions.md`, the architecture ADR, user stories, and scan actual code (routers, pages, config). Summarise findings and **wait for user confirmation** before proceeding.

### Step 2 — Generate Issues

For each issue, read the role agent and skill listed above, then write acceptance criteria using **actual** tools, commands, file paths, and frameworks from the tech stack.

Save each to `docs/workshop/day2-issues/NN-short-slug.md`:

```markdown
## [Issue Title]

### Goal
[One sentence describing what this achieves]

### Acceptance Criteria
- [ ] [Specific, testable criterion using actual tech stack tools]

### Labels
`label1`, `label2`
```

### Step 3 — Present Summary and Create on GitHub

Present the issue titles and role agent/skill assignments from above. **Wait for user confirmation** before creating on GitHub.

Use `gh issue create --title "..." --body-file docs/workshop/day2-issues/NN-slug.md --label "..."` for each issue in order (01–12). Do **not** use `--assignee` — the user will assign Copilot with the listed role agent on GitHub.

## Rules

- **Do not assign issues** — create issues without `--assignee`; the user assigns Copilot manually
- **Read the tech stack, don't guess** — every tool/command/framework reference must come from `tech-stack.instructions.md` or the actual codebase
- **Read organisational standards** — read `.github/instructions/org-standards.instructions.md` for organisational policies. Generate issues that reference org standards for acceptance criteria (e.g. coverage thresholds, secret scanning, deployment strategy). Standards defined in org-standards take precedence over values that may be defined anywhere else in the repository.
- **Read the role and skill files** — each issue's acceptance criteria should align with the assigned role and skill
- **Save first, then summarise** — never type out full issue content in chat
- **One file per issue** — independently copyable into a GitHub Issue
- **No hardcoded technology references** — if you can't find the tech choice, ask the user
