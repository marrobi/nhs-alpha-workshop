---
applyTo: "discovery/**"
---

# Discovery Documents

Discovery outputs have fixed locations and fixed formats. Skills, prompt files and the Day 1 workshop steps read these files as input. Do not change a format without a decision in the decision log.

Follow [GDS: how the discovery phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works). Define the problem before you define a solution.

## Two entry points, one method

Each discovery method has one source:

- People with GitHub Copilot run the skill in `.github/skills/<skill-name>/`.
- People with only Microsoft 365 Copilot paste the prompt file from `discovery/prompts/<skill-name>.md` into Microsoft 365 Copilot Chat.

The skill follows the method in the prompt file. Both entry points make output in the same format. Do not copy the method into the skill.

## Locations

| File | Contents | Produced by |
|---|---|---|
| `discovery/scenarios/scenario.md` | Scenario, problem statement, priority use cases, success measures, assumptions, out of scope | `problem-statement` skill or `discovery/prompts/problem-statement.md` |
| `discovery/personas/persona-report.md` | The users of the service and their needs | Microsoft 365 Copilot Researcher with `discovery/personas/persona-generation-prompt.md`, or the `personas` skill |
| `discovery/personas/data/*.json` and `discovery/personas/generated/personas-deck.md` | Persona slide data and the Marp deck that the script makes from it | `persona-slides` skill |
| `discovery/user_journeys/data/journey-<name>.md` | One user journey in each file | `user-journeys` skill or `discovery/prompts/user-journeys.md` |
| `discovery/plan.md` | Discovery plan of several weeks: how we work, weekly rhythm, workstreams, weekly checklists and the sign-off gate | `discovery-plan` skill or `discovery/prompts/discovery-plan.md` |
| `discovery/workshops/YYYY-MM-DD-<topic>.md` | Workshop packs and notes summaries | `workshop-prep` and `workshop-to-actions` skills, or their prompt files |
| `discovery/decision-log.md` | Decisions and open decisions | `decision` skill or `discovery/prompts/decision.md` |
| `discovery/raid-log.md` | Risks, assumptions, issues, dependencies | `raid-and-traceability` skill |
| `discovery/traceability.md` | Use case to requirement to story to test | `raid-and-traceability` skill |
| `discovery/interface-register.md` | External systems and access lead times | `interface-register` skill |
| `discovery/access-tracker.md` | Access requests and their status | `access-tracker` skill or `discovery/prompts/access-tracker.md` |
| `discovery/status/YYYY-MM-DD.md` | Weekly status | `weekly-status` skill or `discovery/prompts/weekly-status.md` |
| `discovery/playback/playback-pack.md` | Playback and sign-off pack | `playback-pack` skill or `discovery/prompts/playback-pack.md` |
| `<source-dir>/slides/<source-name>-deck.md` | A Marp deck made from one discovery document | `marp-slides` skill |
| `discovery/prompts/<skill-name>.md` | Prompt files for Microsoft 365 Copilot | Maintainers of this template |
| `discovery/templates/` | Blank templates for the logs and registers | Maintainers of this template |

## Identifiers

Use a stable identifier for each item. Do not use an identifier again.

| Item | Format |
|---|---|
| Use case | `UC-01` |
| Persona | `P-01` |
| Requirement | `FR-001` (functional), `NFR-001` (non-functional) |
| User story | `story-001` |
| Interface | `IF-01` |
| Access request | `AR-01` |
| Risk, assumption, issue, dependency | `R-001`, `A-001`, `I-001`, `D-001` |
| Decision | `DEC-001` |
| Action | `ACT-001` |
| Possible hazard | `HAZ-001` |

## Rules for all discovery documents

- Each item has an owner (a named person or role) and a date.
- Each item has a status: `Open`, `In progress`, `Done`, `Closed` or `Blocked`.
- Each item shows its source: the workshop, the person, or the document.
- Mark what we know and what we assume. Write `Assumption:` before an assumption.
- Link items by identifier. Example: "FR-004 supports UC-02."
- Do not delete an item. Change its status to `Closed` and give the reason.
- Never add real patient data, real NHS numbers, real names of patients or real message content.
- Never add secrets, tenant IDs, internal IP addresses or credentials.
- Never write a decision as agreed unless a named person agreed it.

## Mermaid diagrams

- Do not use parentheses `(` `)` in node labels or message text. Use square brackets.
- Do not use semicolons `;` in message text or labels. Use commas.
