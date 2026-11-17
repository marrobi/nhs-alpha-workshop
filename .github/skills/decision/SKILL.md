---
name: decision
description: 'Use when you need to record a decision or an open decision in the discovery decision log, and draft an ADR if the decision is large, or to collate the open questions and get a direction for each. Triggers: decision, decide, decision log, open decision, open questions, collate questions, /decision. Writes discovery/decision-log.md and discovery/open-questions.md.'
---

# Record a Decision

Follow the method, the rules and the output template in [discovery/prompts/decision.md](../../../discovery/prompts/decision.md). Read that file first. The columns agree with [discovery/templates/decision-log.md](../../../discovery/templates/decision-log.md).

The steps below add only the repository steps.

## Inputs

1. Use the decision or the question that the user gives. If there is none, ask for it.
1. Read `discovery/decision-log.md`. If it does not exist, copy `discovery/templates/decision-log.md` to `discovery/decision-log.md` first. Delete the example row.

## Questions

Ask for the missing facts with the ask questions tool, if it is available. Otherwise, ask in the chat. Stop and wait for the answers.

## Output

1. Add the row to `discovery/decision-log.md` with the next free `DEC-` identifier.
1. If the decision is large, draft an ADR in `docs/adr/` with the `nhs-adr-writer` skill. Set the status to "Proposed". Add the ADR number to the decision log.
1. If the decision is open and its date needed is in the past, add a risk to `discovery/raid-log.md`.
1. If the decision changes clinical safety, tell the user to look at it with the `dcb0129-hazard-log` skill.
1. If the decision changes data protection, tell the user to look at it with the `nhs-dpia` skill.

## Validation

- Make sure that the identifier is not used already.
- Make sure that a decision with the status "Decided" has a named person in "Decided by".
- Make sure that the table still has the same columns as the template.

## Collate mode

Use collate mode when the user asks to collate, collect or list the open questions. The method is in the "Collate mode" section of the prompt file.

1. Read `discovery/open-questions.md`. If it does not exist, copy `discovery/templates/open-questions.md` to `discovery/open-questions.md` first. Delete the example row.
1. Read the sources:
   - The "Open Questions" section of each file in `discovery/user_journeys/data/`.
   - `discovery/raid-log.md`: the assumptions and the open items.
   - `discovery/decision-log.md`: the open decisions.
   - `discovery/workshops/`.
   - `user_stories/` and `docs/adr/`, if they exist.
1. Read the priority journey and the first slice candidate from the "Steer" section of `discovery/discovery-notes.md`, or from `user_stories/backlog.md` if it exists.
1. Follow the collate method. Add each new question with the next free `Q-` identifier.
1. Ask the user for a direction for each question that blocks the first slice. Use the ask questions tool, if it is available. Stop and wait for the answers.
1. Write each direction, the name of the person who gave it, and the date. Change the status to "Direction given" or "Assumed".
1. For each large decision, add a row to `discovery/decision-log.md`. For each assumption, add a row to `discovery/raid-log.md`, if it exists.
1. If a story in `user_stories/` is blocked by a question, set its status to `Not ready` with the `Q-` identifier.
