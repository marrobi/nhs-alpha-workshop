# Pre-Workshop Discovery

## Overview

Discovery **must be completed before the workshop**. The goal is to understand the problem, your users, and the journeys you will build — so that workshop time is spent building, not researching.

Use the [product-dev-copilot](https://github.com/marrobi/product-dev-copilot) toolkit to produce the artefacts below. Follow the workflow in its README.

> **Reference**: [How the discovery phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works) — understand the problem before committing to building a service.

## What you need to produce

By the end of discovery you should have these artefacts ready to bring into the workshop:

| Artefact | Location | How to produce |
|---|---|---|
| Scenario & problem statement | `scenarios/scenario.md` | `/generate_scenario_and_problem_statement` prompt |
| Persona report | `personas/persona-report.md` | M365 Copilot Researcher agent |
| Persona slides | `personas/generated/personas-deck.md` | `/generate_persona_slides_from_report` prompt |
| User journeys (5–8) | `user_journeys/data/journey-*.md` | `/generate_user_journeys` prompt |
| Constraints & assumptions | `docs/discovery-notes.md` | Team discussion |

## Step-by-step

### 1. Set up the toolkit

Clone the [product-dev-copilot](https://github.com/marrobi/product-dev-copilot) repo and open it in VS Code Insiders.

### 2. Define the Scenario & Problem Statement

In Copilot Chat, run:

> `/generate_scenario_and_problem_statement` [describe your NHS service idea]

This generates a structured **Scenario Overview** and **Problem Statement**. Save to `scenarios/scenario.md`.

Key questions to answer:
- What is the problem being solved?
- Who is affected (patients, clinicians, admin staff, carers)?
- What does the current process look like and what needs to change?
- Why does it matter (impact on care, safety, efficiency)?

> **GDS guidance**: [Define the problem](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works#define-the-problem) — reframe solutions as problems to be solved.

### 3. Generate Personas

Use the **M365 Copilot Researcher** agent to generate realistic NHS personas:

1. Open [M365 Copilot Chat](https://m365.cloud.microsoft/chat/) and select the **Researcher** agent ([docs](https://learn.microsoft.com/en-us/copilot/microsoft-365/researcher-agent))
2. Paste your `scenarios/scenario.md` content into the bottom of `personas/persona-generation-prompt.md`
3. Save the output to `personas/persona-report.md`

Then generate persona slides in VS Code:

> `/generate_persona_slides_from_report` personas/persona-report.md

Output: `personas/generated/personas-deck.md` — preview with Ctrl+Shift+V.

> **GDS guidance**: [Understanding users and their context](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works#understanding-users-and-their-context)

### 4. Map User Journeys

Generate user journeys from your scenario and personas:

> `/generate_user_journeys` with your scenario and personas

Copilot will suggest 5–8 user journeys. **Develop all of them in detail** — the more journeys you have, the more user stories you can build during the workshop. Save detailed journeys to `user_journeys/data/`.

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

### 6. (Optional) Build an NHS App Prototype

If you want to validate journeys visually before the workshop:

> `/build_nhs_app_prototype_from_user_journey` with a journey file path

This creates a clickable NHS App-style prototype you can walk through with stakeholders.

## Checklist

Before the workshop, confirm you have:

- [ ] `scenarios/scenario.md` — scenario overview and problem statement
- [ ] `personas/persona-report.md` — researched NHS personas (up to 10)
- [ ] `personas/generated/personas-deck.md` — Marp slide deck
- [ ] `user_journeys/data/journey-*.md` — at least 5 detailed user journeys
- [ ] `docs/discovery-notes.md` — constraints, riskiest assumptions, scope boundaries
- [ ] Prioritised list of which journeys/stories to build first

## References

- [How the discovery phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works)
- [How the alpha phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works)
- [product-dev-copilot toolkit](https://github.com/marrobi/product-dev-copilot)
- [M365 Copilot Researcher agent](https://learn.microsoft.com/en-us/copilot/microsoft-365/researcher-agent)
