# Phases

The kit has one method and two schedules. This page gives the method: the phases, the agents, the skills and the outputs. Each playbook gives a schedule for the same phases.

- [2-day workshop playbook](workshop-2-day/README.md)
- Multi-week playbook

## Phases table

| Phase | Goal | Role agents | Skills | Main outputs | 2-day workshop | Multi-week |
|---|---|---|---|---|---|---|
| Discovery | Understand the problem, the users and the journeys before you design a solution | NHS Discovery Analyst, NHS Delivery Lead, NHS Clinical Safety, NHS DPIA Advisor | `problem-statement`, `m365-researcher-personas`, `personas`, `persona-slides`, `user-journeys`, `workshop-prep`, `workshop-to-actions`, `decision`, `raid-and-traceability`, `interface-register`, `access-tracker`, `weekly-status`, `playback-pack`, `marp-slides` | `discovery/scenarios/scenario.md`, `discovery/personas/persona-report.md`, `discovery/user_journeys/data/journey-*.md`, the logs in `discovery/`, `discovery/playback/playback-pack.md` | Pre-work | Discovery weeks |
| Design | Agree the architecture, the user stories and the build backlog | NHS Architect, NHS Product Owner | `nhs-architecture-design`, `nhs-adr-writer`, `engineering-review` (Decide mode), `nhs-user-stories`, `nhs-product-backlog` | `docs/adr/001-architecture.md`, `docs/adr/architecture.drawio`, `user_stories/story-NNN-*.md`, `user_stories/backlog.md` | Day 1 | Sprint 0 |
| Build | Build the backlog one journey-aligned batch at a time, and review each batch | NHS Service Builder, NHS Quality Engineer, NHS Content Designer | `fastapi-react-azure`, `automated-testing`, `playwright-nhs-e2e`, `azure-nhs-deploy`, `nhs-synthetic-data`, `engineering-review` (Review mode), `visual-qa` | `app/`, `frontend/`, `infra/`, `tests/` | Day 1 | Sprints |
| Harden | Add the pipeline, tests, security, operations and compliance evidence | NHS Delivery Lead, NHS Service Builder, NHS Quality Engineer, NHS Clinical Safety, NHS DPIA Advisor | `day2-issue-planning`, `github-actions-pipeline`, `code-quality-review`, `security-review`, `azure-sre-agent`, `azure-infrastructure-review`, `accessibility-audit`, `performance-testing`, `dcb0129-hazard-log`, `nhs-dpia`, `demo-recording` | `.github/workflows/`, `tests/e2e/`, `tests/performance/`, `docs/runbook.md`, `docs/clinical-safety/hazard-log.md`, `docs/dpia/dpia.md`, `docs/demo/demo-script.md` | Day 2 | Sprints |
| Assess | Collect the evidence for the GDS service assessment and play it back | NHS Quality Engineer, NHS Delivery Lead, NHS Documentation | `gds-service-standard`, `playback-pack`, `marp-slides` | `docs/gds-assessment.md`, the documentation site | Day 2 | Before the assessment |

## Rules for all phases

- Agents propose. People decide. Record who agreed each decision, and when.
- Use synthetic data only. Never use real patient data.
- Keep the hazard log, the data protection impact assessment (DPIA) and the architecture decision records (ADRs) current in each phase. Do not leave them to the end.
- Read `.github/instructions/tech-stack.instructions.md` for the technology choices. Do not assume a technology.
