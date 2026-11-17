---
name: problem-statement
description: 'Use when you need to define the scenario and problem statement for a new NHS service, or turn a service idea or solution into a problem. Triggers: problem statement, scenario, define the problem, discovery start, /problem-statement. Writes discovery/scenarios/scenario.md.'
---

# Problem Statement

Follow the method, the rules and the output template in [discovery/prompts/problem-statement.md](../../../discovery/prompts/problem-statement.md). Read that file first. Do not change its headings, because the design phase reads them.

The steps below add only the repository steps.

## Inputs

1. Use the service description that the user gives. If there is no description, ask for one.
1. Read these files if they exist:
   - `discovery/scenarios/scenario.md`
   - `discovery/workshops/`
   - `discovery/raid-log.md`

## Questions

Ask the questions from Step 1 of the method with the ask questions tool, if it is available. Otherwise, ask them in the chat. Stop and wait for the answers. If the user says "continue", mark the gaps as assumptions.

## Output

1. Write `discovery/scenarios/scenario.md`.
1. If `discovery/raid-log.md` exists, copy each new assumption to it with the same `A-` identifier. Use the format in [discovery/templates/raid-log.md](../../../discovery/templates/raid-log.md).
1. Replace the root `README.md` with a short project README based on the scenario. Include:
   - The service name and a one-sentence summary.
   - The problem and the people affected.
   - A short Mermaid diagram of the key workflow.
   - A link to `discovery/scenarios/scenario.md`.
   - A link to `docs/README.md`.
   - A link to the `LICENSE`.

   Do not keep the template content of this kit in the project README.

## Validation

- Make sure that the headings `Scenario Overview`, `Problem Statement`, `Assumptions`, `Out of Scope` and `Open Questions` are in the file.
- Make sure that each open question has an owner and a date, or "Owner needed" and "Date needed".
- Make sure that the file has no real patient data.
- Give the report from Step 3 of the method.
