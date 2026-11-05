---
name: workshop-prep
description: 'Use when you need to prepare a discovery workshop pack with the goal, agenda, questions, decisions needed and outputs. Triggers: workshop prep, prepare workshop, agenda, workshop pack, /workshop-prep. Writes discovery/workshops/YYYY-MM-DD-<topic>.md.'
---

# Workshop Preparation

Follow the method, the rules and the output template in [discovery/prompts/workshop-prep.md](../../../discovery/prompts/workshop-prep.md). Read that file first.

The steps below add only the repository steps.

## Inputs

Read these files if they exist:

- `discovery/scenarios/scenario.md`
- `discovery/decision-log.md` and `discovery/raid-log.md`: find the open items for this topic.
- `discovery/access-tracker.md` and `discovery/interface-register.md`: find the blocked items for this topic.
- `discovery/workshops/`: find the earlier workshops on this topic.
- `discovery/personas/persona-report.md` and `discovery/user_journeys/data/`, if the topic needs them.

## Questions

Ask for the topic, the date, the length and the attendees with the ask questions tool, if it is available. Otherwise, ask in the chat. Stop and wait for the answers.

## Output

Write `discovery/workshops/YYYY-MM-DD-<topic>.md`. Use the date of the workshop. Use a short topic in lowercase kebab-case.

## Validation

- Make sure that each decision needed has a person who decides and a date, or "Owner needed" and "Date needed".
- Make sure that each link in the pre-reading goes to a file that exists.
- Make sure that the file has no real patient data.
- Tell the user to run the `workshop-to-actions` skill on the notes after the workshop.
