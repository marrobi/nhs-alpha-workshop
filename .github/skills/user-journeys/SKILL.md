---
name: user-journeys
description: 'Use when you need to suggest, select and write user journeys from the scenario and personas, including failure paths. Triggers: user journeys, journey mapping, happy path, edge cases, /user-journeys. Writes discovery/user_journeys/data/journey-*.md.'
---

# User Journeys

Follow the method, the rules and the output template in [discovery/prompts/user-journeys.md](../../../discovery/prompts/user-journeys.md). Read that file first. The output template agrees with [discovery/user_journeys/templates/journey-template.md](../../../discovery/user_journeys/templates/journey-template.md).

The steps below add only the repository steps.

## Inputs

Read these files:

- `discovery/scenarios/scenario.md`
- `discovery/personas/persona-report.md`
- `discovery/user_journeys/data/`, so that you do not repeat a journey.

If the scenario or the persona report does not exist, tell the user to run the `problem-statement` skill or to make the persona report first. Stop.

## Questions

Show the suggested journeys from Step 1 of the method. Ask the user to select the journeys with the ask questions tool, if it is available. Otherwise, ask in the chat. Stop and wait for the selection.

## Output

1. Write each selected journey to `discovery/user_journeys/data/journey-<short-name>.md`. Use lowercase kebab-case for the short name.
1. If `discovery/traceability.md` exists, add the journey to the rows of its use cases.
1. If `discovery/raid-log.md` exists, add each new assumption with the next free `A-` identifier.

## Validation

- Make sure that each journey file has all the headings of the template, in the same order.
- Make sure that each Mermaid label has no parentheses and no semicolons.
- Make sure that each open question has an owner and a date, or "Owner needed" and "Date needed".
- Make sure that the files have no real patient data.
- Give the report from Step 3 of the method. Tell the user to look at the failure paths with a possible clinical risk with the `dcb0129-hazard-log` skill.
