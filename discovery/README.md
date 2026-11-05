# Discovery

Complete discovery before you design or build. The goal is to understand the problem, your users and the journeys that you will build.

- In the [2-day workshop](../docs/playbooks/workshop-2-day/README.md), discovery is pre-work. Complete it before the workshop, so that workshop time is for building.
- In [multi-week delivery](../docs/playbooks/multi-week/README.md), discovery takes several weeks. Run the `discovery-plan` skill first. Discovery ends with a playback and sign-off.

> **Reference**: [How the discovery phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works) — understand the problem before committing to building a service.

## Two ways to work: one method

Each discovery method has one source. You can use it in two ways:

| You have | Use | How |
|---|---|---|
| GitHub Copilot | The skill in `.github/skills/<skill-name>/` | Run `/<skill-name>` in Copilot Chat, or ask for the task. Copilot reads the repository and writes the output file |
| Only Microsoft 365 Copilot | The prompt file in [`discovery/prompts/`](prompts/) | Open [Microsoft 365 Copilot Chat](https://m365.cloud.microsoft/chat/). Paste the prompt file, then paste the inputs that it lists. Save the output to the path that the prompt file gives |

Each skill follows the method in its prompt file. Both ways make output in the same format, so the next steps work with either. Some records are logs or registers. For these, copy the blank template from [`discovery/templates/`](templates/).

## What you need to produce

| Artefact | Location | GitHub Copilot | Microsoft 365 Copilot only |
|---|---|---|---|
| Scenario & problem statement | `discovery/scenarios/scenario.md` | `/problem-statement` skill | [`prompts/problem-statement.md`](prompts/problem-statement.md) |
| Persona report | `discovery/personas/persona-report.md` | M365 Copilot Researcher agent (preferred), following the `m365-researcher-personas` skill, or the `/personas` skill | [`personas/persona-generation-prompt.md`](personas/persona-generation-prompt.md) in Researcher |
| Persona slides | `discovery/personas/generated/personas-deck.md` | `/persona-slides` skill | Copilot in PowerPoint, from the persona report |
| User journeys | `discovery/user_journeys/data/journey-*.md` | `/user-journeys` skill | [`prompts/user-journeys.md`](prompts/user-journeys.md) |
| Constraints & assumptions | `docs/discovery-notes.md` | Team discussion | Team discussion |

## Step-by-step

### 1. Get started

Open your repository in GitHub Codespaces or VS Code with the provided Dev Container. All prerequisites (Node.js, Marp, Mermaid preview) are pre-installed.

Create a new branch for your discovery work: `git checkout -b discovery`

### 2. Define the Scenario & Problem Statement

In Copilot Chat, run:

> `/problem-statement` [describe your NHS service idea]

If you have only Microsoft 365 Copilot, paste [`prompts/problem-statement.md`](prompts/problem-statement.md) into Microsoft 365 Copilot Chat, then paste your service idea.

This generates a structured **Scenario Overview** and **Problem Statement** at
`discovery/scenarios/scenario.md`. The skill also replaces the root template
`README.md` with a concise overview of your service.

Key questions to answer:
- What is the problem being solved?
- Who is affected (patients, clinicians, admin staff, carers)?
- What does the current process look like and what needs to change?
- Why does it matter (impact on care, safety, efficiency)?

> **GDS guidance**: [Define the problem](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works#define-the-problem) — reframe solutions as problems to be solved.

### 3. Generate Personas

Use the **M365 Copilot Researcher** agent to generate realistic NHS personas:

1. Open [M365 Copilot Chat](https://m365.cloud.microsoft/chat/) and select the **Researcher** agent ([docs](https://learn.microsoft.com/en-us/copilot/microsoft-365/researcher-agent))
2. Paste your `discovery/scenarios/scenario.md` content into the bottom of [discovery/personas/persona-generation-prompt.md](personas/persona-generation-prompt.md) and run in Researcher
3. Save the output to `discovery/personas/persona-report.md`

Follow `.github/skills/m365-researcher-personas/SKILL.md` for the handoff and data-safety rules. This is a manual step in M365 Copilot; VS Code does not run Researcher.

If your team cannot use Researcher, run the `/personas` skill in GitHub Copilot. It uses the same method and writes the same structure. Its output is an AI draft, not user research. Check each persona with real users.

Then generate persona slides in VS Code:

> `/persona-slides` discovery/personas/persona-report.md

Output: `discovery/personas/generated/personas-deck.md` — preview with Ctrl+Shift+V (Windows) or Cmd+Shift+V (macOS).

> **GDS guidance**: [Understanding users and their context](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works#understanding-users-and-their-context)

### 4. Map User Journeys

Generate user journeys from your scenario and personas:

> `/user-journeys` with your scenario and personas

If you have only Microsoft 365 Copilot, paste [`prompts/user-journeys.md`](prompts/user-journeys.md) into Microsoft 365 Copilot Chat, then paste your scenario and personas.

Copilot will suggest user journeys covering each primary persona and key workflow. **Develop all distinct journeys in detail** — the more journeys you have, the more user stories you can build. Save detailed journeys to `discovery/user_journeys/data/`.

Each journey file should include:
- Step-by-step interactions (happy path and edge cases)
- Decision points and variations
- Touchpoints and channels
- Mermaid diagrams (sequence, flowchart, or timeline)

> **GDS guidance**: [Focus on testing your riskiest assumptions](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works#focus-on-testing-your-riskiest-assumptions)

### 5. Identify Constraints & Riskiest Assumptions

Discuss with your team and document in `docs/discovery-notes.md`:

- **Technical constraints**: existing systems to integrate with, data sovereignty, NHS network access
- **Legislative constraints**: UK GDPR / Art. 9 (health data), clinical safety (DCB0129)
- **Riskiest assumptions**: what must be true for the service to work?
- **What you will not build**: scope boundaries for the alpha

### 6. Run workshops and track the work

These skills help you to run discovery workshops and to keep the records up to date. They are optional in the 2-day workshop. They are required in multi-week delivery. See `.github/instructions/delivery-mode.instructions.md`.

| Task | Output | GitHub Copilot | Microsoft 365 Copilot only |
|---|---|---|---|
| Plan a discovery of several weeks | `discovery/plan.md` | `/discovery-plan` | [`prompts/discovery-plan.md`](prompts/discovery-plan.md) |
| Prepare a workshop | `discovery/workshops/YYYY-MM-DD-<topic>.md` | `/workshop-prep` | [`prompts/workshop-prep.md`](prompts/workshop-prep.md) |
| Change workshop notes into actions | Notes summary in the workshop file, and new rows in the logs | `/workshop-to-actions` | [`prompts/workshop-to-actions.md`](prompts/workshop-to-actions.md) |
| Record a decision | `discovery/decision-log.md` | `/decision` | [`prompts/decision.md`](prompts/decision.md) and [`templates/decision-log.md`](templates/decision-log.md) |
| Keep the RAID log (risks, assumptions, issues, dependencies) and traceability | `discovery/raid-log.md`, `discovery/traceability.md` | `raid-and-traceability` skill | [`templates/raid-log.md`](templates/raid-log.md), [`templates/traceability.md`](templates/traceability.md) |
| Record external systems | `discovery/interface-register.md` | `interface-register` skill | [`templates/interface-register.md`](templates/interface-register.md) |
| Track access requests | `discovery/access-tracker.md` | `/access-tracker` | [`prompts/access-tracker.md`](prompts/access-tracker.md) and [`templates/access-tracker.md`](templates/access-tracker.md) |
| Write the weekly status | `discovery/status/YYYY-MM-DD.md` | `/weekly-status` | [`prompts/weekly-status.md`](prompts/weekly-status.md) |
| Build the playback and sign-off pack | `discovery/playback/playback-pack.md` | `/playback-pack` | [`prompts/playback-pack.md`](prompts/playback-pack.md) |
| Make slides from a document | `<source-dir>/slides/<source-name>-deck.md` | `/marp-slides` | Copilot in PowerPoint |

To make slides with Microsoft 365 Copilot only, open PowerPoint. Ask Copilot in PowerPoint to make a deck from the source document, for example the status report or the playback pack. You do not need Marp.

### 7. Merge your discovery branch

Commit and merge your discovery branch before the design phase, so that the artefacts are on `main`.

## Checklist

Before the design phase, confirm that you have:

- [ ] `discovery/scenarios/scenario.md` — scenario overview and problem statement
- [ ] `discovery/personas/persona-report.md` — researched NHS personas (up to 10)
- [ ] `discovery/personas/generated/personas-deck.md` — Marp slide deck
- [ ] `discovery/user_journeys/data/journey-*.md` — detailed user journeys covering each primary user type
- [ ] `docs/discovery-notes.md` — constraints, riskiest assumptions, scope boundaries
- [ ] Prioritised list of which journeys to build first
- [ ] Discovery branch merged to main

> **Note**: The **NHS Product Owner** agent makes user stories from the journeys in the design phase: Day 1 of the workshop, or sprint 0 in multi-week delivery. Discovery produces journeys only — not stories.

## Where things live

- `discovery/personas/` — persona JSON inputs + Marp deck generation (see `discovery/personas/README.md`)
- `discovery/user_journeys/` — user journey templates and generated docs
- `discovery/scenarios/` — scenario and problem statement output
- `discovery/prompts/` — prompt files for Microsoft 365 Copilot Chat, one for each skill that you can use in a chat
- `discovery/templates/` — blank templates for the logs and registers
- `discovery/workshops/`, `discovery/status/`, `discovery/playback/` — workshop packs, status reports and the playback pack (made when you use them)
- `.github/skills/` — the GitHub Copilot skills for the workflow

## References

- [How the discovery phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works)
- [How the alpha phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works)
- [M365 Copilot Researcher agent](https://learn.microsoft.com/en-us/copilot/microsoft-365/researcher-agent)
