# Day 2 — Complete the Alpha with Copilot Coding Agent

## Overview

Day 2 focuses on **completing the alpha** — hardening, testing, documenting, and demonstrating GDS readiness. You use the **Copilot Coding Agent** on GitHub: create issues, assign Copilot with the correct custom agent, and it creates PRs automatically.

> **Reference**: [How the alpha phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works) — by the end of alpha, you should be confident you can create something that meets users' needs and is cost-effective.

### What "completing the alpha" means

At the end of Day 2, your service should have evidence for:
- **Tested riskiest assumptions** — working prototype that addresses the user journeys from Day 1 discovery
- **Solving a whole problem for users** — end-to-end journeys that work, scoped correctly
- **Meeting the standard** — GDS 14-point evidence, accessibility audit, clinical safety, DPIA
- **Technical readiness** — tests, CI pipeline, security hardening, performance baseline

## Setup (15 minutes)

1. Ensure all Day 1 code is committed and pushed
2. Enable Copilot Coding Agent on your repository (Settings → Copilot → Coding Agent)

## Generate Day 2 Issues (15 minutes)

Day 2 issues are generated from your actual tech stack and codebase — not from static templates.

1. In VS Code, switch to the **Day 2 Issue Generator** agent
2. Ask it to generate the Day 2 issues
3. It reads `tech-stack.instructions.md`, the ADR, user stories, and your codebase to produce tailored issue files in `docs/workshop/day2-issues/`
4. Review the generated files and confirm
5. The agent creates the issues on GitHub using `gh` CLI — it does **not** assign them to Copilot

> **Prerequisite**: Ensure `gh auth login` has been run in the terminal before starting.

## Working Through Issues

The issue generator creates the issues on GitHub but does **not** assign them — you must assign Copilot with the correct **custom agent** for each issue:

1. Open the first issue on GitHub
2. Click **"Assign Copilot"** and select the custom agent from the dropdown (the generator tells you which one)
3. Copilot creates a PR using that agent's expertise
4. Review the PR, request changes if needed, then merge
5. Move to the next issue — assign one at a time

> **Important**: Assign issues one at a time. Copilot works on one issue per repo. Wait for the PR to be merged before assigning the next.

## Recommended Issue Order

Start with CI/CD so every subsequent PR is automatically checked, then quality and security, then documentation and GDS evidence.

| # | Issue | Custom Agent | Alpha Evidence |
|---|---|---|---|
| 01 | CI/CD Pipeline | CI/CD Pipeline Builder | Operational reliability |
| 02 | Unit & Integration Tests | Testing | Technical quality |
| 03 | Playwright E2E Tests | Playwright E2E | User journeys work end-to-end |
| 04 | Code Quality Review | Code Quality Reviewer | Code standards, type safety, test coverage |
| 05 | Security Hardening | Security Reviewer | Threat awareness |
| 06 | Azure Infra Security Review | Azure Infra Security Reviewer | Infrastructure security, network isolation |
| 07 | Accessibility Audit | Accessibility Auditor | WCAG 2.2 AA / everyone can use it |
| 08 | Performance Load Tests | Performance | Service can handle expected load |
| 09 | DCB0129 Clinical Safety | NHS Clinical Safety | Clinical risk managed |
| 10 | DPIA | NHS DPIA Advisor | Data protection considered |
| 11 | GDS Assessment Evidence | NHS GDS Assessor | 14-point standard mapped |
| 12 | Runbook & Deployment Docs | NHS Service Builder | Operational readiness |
| 13 | MKDocs Documentation Site | NHS Documentation | All alpha artefacts in one site _(optional — when all done)_ |

## GDS Alpha Assessment Readiness

By the end of Day 2, review your evidence against the [GDS Service Standard](https://www.gov.uk/service-manual/service-standard):

| Standard Point | Evidence Source |
|---|---|
| 1. Understand users and their needs | Discovery: personas, user journeys (Day 1) |
| 2. Solve a whole problem for users | Working prototype with scoped journeys |
| 3. Provide a joined up experience | NHS Design System, consistent components |
| 5. Iterate and improve | ADRs, Git history, PR iteration |
| 7. Use agile ways of working | Issues, PRs, iterative delivery |
| 9. Create a secure service | Security audit, OWASP checklist |
| 10. Define what success looks like | Performance metrics, k6 thresholds |
| 11. Choose the right tools and technology | ADRs documenting technology choices |
| 12. Make new source code open | Public repo, open source licence |
| 13. Use and contribute to open standards | NHS Design System, FHIR references |
| 14. Operate a reliable service | CI/CD, monitoring, runbook |

Use the **NHS GDS Assessor** agent (Issue 11) to generate the full evidence report.

## Tips

- **Don't create all issues at once** — Copilot works on one at a time per repo
- **Review PRs carefully** — Copilot is good but not perfect
- **Iterate on PRs** — request changes via review comments, Copilot will update
- **Link back to discovery** — Day 1 artefacts (scenarios, personas, journeys) are key evidence for GDS points 1, 2, and 3

---

## Demo Recording (VS Code Activity)

While Copilot works through the Day 2 issues, use the **Demo Recorder** agent in VS Code to produce a recorded walkthrough of the service. This runs locally because it needs access to the running application — the hosted Copilot Coding Agent may not be able to reach it.

**Agent**: Demo Recorder _(in VS Code, not as a GitHub issue)_

> Record a demo of the service. Read the user journeys, stories, and frontend/backend routes. Generate a demo narrative script, then create a Playwright recording that walks through each journey with video capture.

The agent will:

1. Discover all routes and user journeys from the codebase
2. Generate a demo narrative script at `docs/demo/demo-script.md`
3. Create a Playwright test at `tests/e2e/demo/test_demo_recording.py` with video recording enabled
4. Run the walkthrough — if any issues are found, fix them iteratively
5. Save recorded videos to `tests/e2e/demo/videos/` and screenshots to `tests/e2e/demo/screenshots/`
6. Report any issues found and fixed in `docs/demo/demo-issues-resolved.md`

> **Tip**: Run this after E2E tests are complete (issue 03) so the journeys are already verified. The demo recording serves as a final end-to-end validation and produces shareable evidence for assessors.

---

## When All Done — MKDocs Documentation Site

Once all issues are merged, use the **NHS Documentation** agent to build a single MKDocs site that pulls every alpha artefact together into one browsable, shareable site.

**Agent**: NHS Documentation

> Build the MKDocs documentation site for this service. Read the codebase, user stories, ADRs, clinical safety, DPIA, and GDS evidence artefacts to generate all pages. Use NHS blue branding. Verify with `mkdocs build`.

The agent will:

1. Install `mkdocs-material` and scaffold `mkdocs.yml`
2. Generate pages from existing artefacts — no content is invented:
   - **Home** — project overview from `README.md`
   - **Architecture** — from `docs/adr/001-architecture.md` and the draw.io diagram
   - **API Reference** — scanned from `app/routers/` with all endpoints documented
   - **User Guide** — user-facing flows from `discovery/user_journeys/` and `frontend/src/pages/`
   - **Deployment** — from `infra/` and build commands in `AGENTS.md`
   - **Testing** — test structure, how to run, and coverage targets
   - **Security** — security measures derived from the security hardening PR
   - **Accessibility** — WCAG 2.2 AA audit results
   - **Clinical Safety** — from `docs/clinical-safety/`
   - **DPIA** — from `docs/dpia/`
   - **ADRs** — index of all records in `docs/adr/`
3. Apply NHS blue (`#005eb8`) theming
4. Run `mkdocs build` to verify no broken links

Preview locally with:
```bash
mkdocs serve
```

The site provides a single artefact you can share with assessors, stakeholders, and the wider team as evidence of a complete NHS Alpha.
