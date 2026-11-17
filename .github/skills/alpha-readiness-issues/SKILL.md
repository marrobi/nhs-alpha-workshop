---
name: alpha-readiness-issues
description: 'Use when a slice is built and you need to plan the hardening and assessment work as GitHub issues. Foundation mode (once): CI/CD hardening, the optional alerting and Azure SRE Agent issue, infrastructure security review and runbook. Slice mode (after each slice): tests, code quality, security, accessibility, performance, content, clinical safety, DPIA and GDS evidence for that slice. Tailors each issue to the actual tech stack, architecture and code, and gives the role agent and skill for each issue.'
---

# Alpha Readiness Issues

Plan the GitHub issues that harden the alpha and collect the assessment evidence. Each issue is tailored to the **actual tech stack, architecture, and service** that the team built — never generic.

You produce issue template files — you do **not** write application code, tests, or infrastructure.

The readiness work is a **loop**. Run the foundation issues once. Then run the slice issues after each slice is merged, so that the hardening and the evidence grow with the service. A 2-day workshop may build only 1 slice. The loop still gives complete evidence for that slice.

## Modes

| Mode | When | Issues | Folder |
|---|---|---|---|
| **Foundation** | Once, after slice 0b (deployment) or after slice 1 | F01–F04 | `docs/alpha-readiness-issues/foundation/` |
| **Slice N** | After the pull request for slice N is merged | S01–S09, plus S10–S11 when triggered, scoped to slice N | `docs/alpha-readiness-issues/slice-N/` |

Ask the user which mode to run if the request does not say. In slice mode, ask for the slice number if it is not given.

## Delivery mode

Read `.github/instructions/delivery-mode.instructions.md`.

- **`workshop-2-day`**: on Day 2, run foundation mode, then slice mode for each slice built on Day 1. If the team builds another slice on Day 2, run slice mode again for it.
- **`multi-week`**: run foundation mode in the first build sprint. Run slice mode in the sprint after each slice is merged, or in the same sprint if there is time. Propose a sprint for each issue, show the proposal and wait for confirmation. Record the sprint in the Sprint section of each issue file.

## Prerequisites

- `.github/instructions/tech-stack.instructions.md` — tech stack choices
- `.github/instructions/org-standards.instructions.md` — organisational policies (if defined)
- `docs/adr/001-architecture.md` — agreed architecture
- `docs/adr/002-dependency-strategy.md` — what is real, sandbox or mock
- `user_stories/backlog.md` and `user_stories/story-*.md` — slices and user stories
- Application code in the repository. In slice mode, the slice's pull request is merged.

## Foundation Issues (once)

| # | Issue | Role agent | Skill |
|---|---|---|---|
| F01 | CI/CD Hardening (environments, approvals, deployment slots or staged rollout, branch protection) | NHS Service Builder | `github-actions-pipeline` (hardening mode) |
| F02 | Alerting & Azure SRE Agent _(optional)_ | NHS Service Builder | `azure-sre-agent` |
| F03 | Azure Infra Security Review (baseline) | NHS Quality Engineer | `azure-infrastructure-review` |
| F04 | Runbook & Deployment Docs, including "What is mocked in this alpha" from ADR-002 | NHS Service Builder | `azure-nhs-deploy` |

The CI pipeline and the deploy pipeline already exist from slice 0. F01 hardens them. Do not create them again.

**F01 has two parts**, as F02 does. Split the acceptance criteria:

- **Pull request (Copilot coding agent)**: the `environment:` and approval settings in the workflows, the Terraform for deployment slots or a staged rollout, and the branch protection recommendation.
- **Operator steps after merge (VS Code, with the operator's own `az login` and repository admin rights)**: apply the Terraform plan, set the GitHub environment protection rules and required reviewers, and set branch protection on `main`. The Copilot coding agent cannot apply Terraform, reach Azure or change repository settings.

## Slice Issues (after each slice)

Scope each issue to slice N: its stories, its pages and endpoints, its external dependencies, and its commit range (from the merged pull request). Do not review or rewrite the rest of the service, except where slice N changed it.

| # | Issue | Role agent | Skill |
|---|---|---|---|
| S01 | Test Gaps — unit, integration and Playwright E2E for the slice's path | NHS Quality Engineer | `automated-testing`, `playwright-nhs-e2e` |
| S02 | Code Quality Review of the slice's changes | NHS Quality Engineer | `code-quality-review` |
| S03 | Security Review of the slice's changes | NHS Quality Engineer | `security-review` |
| S04 | Accessibility Audit of the slice's pages | NHS Quality Engineer | `accessibility-audit` |
| S05 | Performance Check of the slice's endpoints and pages | NHS Quality Engineer | `performance-testing` |
| S06 | Content Review of the slice's pages | NHS Content Designer | — |
| S07 | DCB0129 Clinical Safety — update the hazard log for the slice | NHS Clinical Safety | `dcb0129-hazard-log` |
| S08 | DPIA — update for the slice's data and processors | NHS DPIA Advisor | `nhs-dpia` |
| S09 | GDS Assessment Evidence — add the slice's evidence | NHS Quality Engineer | `gds-service-standard` |
| S10 | Infrastructure Security Review — only when the slice changes infrastructure, identity, networking or deployed dependencies, including external mocks | NHS Quality Engineer | `azure-infrastructure-review` |
| S11 | Runbook & Deployment Docs Update — only when the slice changes deployment, configuration, dependencies, monitoring, recovery or support procedures | NHS Service Builder | `azure-nhs-deploy` |

**Check the conditional issues for every slice.** Record whether S10 and S11 are needed and why in the proposed issue summary. Review only the changed infrastructure and its affected connections; update the existing runbook, including what is mocked. Do not rerun or recreate the whole foundation baseline. Complete S10 and S11, when triggered, before S07–S09 so the compliance evidence reflects their results. Include any required post-merge operator steps and verification in their acceptance criteria.

**Update, do not rewrite.** S07, S08 and S09 update the existing documents (`docs/clinical-safety/hazard-log.md`, `docs/dpia/dpia.md`, `docs/gds-assessment.md`). The first slice creates them. Later slices add to them. Each external mock service in the slice needs a hazard log entry and a DPIA note.

**Keep the loop light.** The slice's definition of done already covers unit tests, E2E tests with axe checks, CI and the engineering review. S01–S05 look for gaps, not for work that the slice already did. If a check has nothing to add for the slice, write that in the issue, and close it with a short reason.

## Workflow

### Step 1 — Read the Codebase

Read `tech-stack.instructions.md`, `org-standards.instructions.md`, the ADRs, the backlog, the user stories, and scan actual code (routers, pages, config). In slice mode, read the slice issue, its stories and the actual patch of its merged pull request with `gh pr diff <number>`. Also use `gh pr view <number> --json files,commits,mergeCommit` for file and commit metadata; metadata is not a substitute for the patch. Summarise findings and **wait for user confirmation** before proceeding.

### Step 2 — Generate Issues

For each issue, read the role agent and skill listed above, then write acceptance criteria using **actual** tools, commands, file paths, and frameworks from the tech stack. In slice mode, name the actual stories, pages, endpoints and files of the slice.

Save each issue to its mode folder, for example `docs/alpha-readiness-issues/foundation/F01-cicd-hardening.md` or `docs/alpha-readiness-issues/slice-1/S03-security-review.md`:

```markdown
## [Issue Title] — [Foundation | Slice N]

### Goal
[One sentence describing what this achieves]

### Scope
[Foundation: the whole service. Slice N: the stories, pages, endpoints and commit range]

### Acceptance Criteria
- [ ] [Specific, testable criterion using actual tech stack tools]

### Labels
`readiness`, `foundation` or `slice-N`, [other labels]

### Sprint
[multi-week mode only: the proposed sprint]
```

### Step 3 — Present Summary and Create on GitHub

Present the issue titles and role agent/skill assignments. In `multi-week` mode, also present the proposed sprint for each issue. **Wait for user confirmation** before creating on GitHub.

Use `gh issue create --title "..." --body-file <file> --label "..."` for each issue. Follow the table order, except schedule triggered S10 and S11 before S07–S09. Prefix slice issue titles with the slice, for example `Slice 1 — S03 Security Review`. Do **not** use `--assignee` — the user will assign Copilot with the listed role agent on GitHub.

Add a link to each new slice issue in the slice's build issue, so the slice issue shows its readiness work.

### Issue F02 — Alerting & Azure SRE Agent (optional)

Ask the user whether the team has Azure SRE Agent access in its subscription. Record the answer at the top of issue F02 as **Variant: Alerting and SRE Agent** or **Variant: Alerting only**. Use that variant for issue F02 and for the later issues.

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

Because issue F02 comes first, make the later issues build on it:

| Issue | Both variants | Alerting and SRE Agent only |
|---|---|---|
| F03 Infra security review | Audit the alert rules and action group | Audit the agent's roles, subnet, NSG, region and access level |
| F04 Runbook | Extend the operations section from F02; do not replace it | Keep the agent section: open, guardrails, disable, delete threads, rehearsal record |
| S07 Clinical safety (first slice loop) | Cite alerts and the runbook as detection controls for "service unavailable" hazards | Add the hazard "agent proposes an incorrect cause or remediation" |
| S08 DPIA (first slice loop) | — | Record Microsoft as processor for agent conversations, storage region and inference location |
| S09 GDS evidence | Use alerts and the runbook for point 14 | Use the rehearsal record and guardrails for point 14 |

If issue F02 is skipped completely, issue F04 creates the operations section, and none of the later issues refer to alerts from F02 or to the SRE Agent.

## Rules

- **Do not assign issues** — create issues without `--assignee`; the user assigns Copilot manually
- **Read the tech stack, don't guess** — every tool/command/framework reference must come from `tech-stack.instructions.md` or the actual codebase
- **Read organisational standards** — read `.github/instructions/org-standards.instructions.md` for organisational policies. Generate issues that reference org standards for acceptance criteria (e.g. coverage thresholds, secret scanning, deployment strategy). Standards defined in org-standards take precedence over values that may be defined anywhere else in the repository.
- **Read the role and skill files** — each issue's acceptance criteria should align with the assigned role and skill
- **Save first, then summarise** — never type out full issue content in chat
- **One file per issue** — independently copyable into a GitHub Issue
- **Scope slice issues to the slice** — never re-audit the whole service in slice mode; name the slice's stories, pages, endpoints and commits
- **No hardcoded technology references** — if you can't find the tech choice, ask the user
