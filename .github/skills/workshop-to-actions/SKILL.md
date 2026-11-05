---
name: workshop-to-actions
description: 'Use when you need to turn raw workshop notes, a transcript or a meeting recap into decisions, actions, RAID items, open questions and updates to the discovery documents. Triggers: workshop notes, meeting notes, actions, write up workshop, /workshop-to-actions.'
---

# Workshop Notes to Actions

Follow the method, the rules and the output template in [discovery/prompts/workshop-to-actions.md](../../../discovery/prompts/workshop-to-actions.md). Read that file first.

The steps below add only the repository steps.

## Inputs

1. Use the notes that the user gives. If there are no notes, ask for them.
1. Check the notes for real patient data first. If you find it, stop and tell the user. Do not write it to any file.
1. Read the workshop file in `discovery/workshops/`, if it exists.
1. Read these files to find the next free identifiers:
   - `discovery/decision-log.md`
   - `discovery/raid-log.md`
   - `discovery/interface-register.md`
   - `discovery/access-tracker.md`
   - `discovery/workshops/` for the last `ACT-` and `HAZ-` identifiers.

## Questions

Show the summary from Step 2 of the method. Ask the user to confirm with the ask questions tool, if it is available. Otherwise, ask in the chat. Do not change any file before the user confirms.

## Output

After the user confirms:

1. Add the "Notes summary" to the end of `discovery/workshops/YYYY-MM-DD-<topic>.md`. If the file does not exist, make it.
1. Add the decisions to `discovery/decision-log.md`.
1. Add the RAID items to `discovery/raid-log.md`.
1. Add the interfaces to `discovery/interface-register.md` and the access requests to `discovery/access-tracker.md`.
1. Add the open questions to the "Open Questions" section of `discovery/scenarios/scenario.md`, if they are about the problem or the scope.
1. If a log does not exist, copy it from `discovery/templates/` first.

Use the next free identifier for each item. Do not use an identifier again.

## Validation

- Make sure that each new item has an owner and a date, or "Owner needed" and "Date needed".
- Make sure that no decision shows "Decided" without a named person.
- Make sure that the files have no real patient data.
- Give the report from Step 3 of the method.
- Tell the user to look at the possible hazards with the `dcb0129-hazard-log` skill, and the data protection points with the `nhs-dpia` skill.
