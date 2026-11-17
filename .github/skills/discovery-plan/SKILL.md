---
name: discovery-plan
description: 'Use when you need a plan for a discovery of several weeks: how we work, the weekly rhythm, the workstreams, the goals, Copilot steps and checklist for each week, and the playback and sign-off gate. Triggers: discovery plan, plan discovery, discovery weeks, multi-week, /discovery-plan. Writes discovery/plan.md.'
---

# Discovery Plan

Follow the method, the rules and the output template in [discovery/prompts/discovery-plan.md](../../../discovery/prompts/discovery-plan.md). Read that file first.

The steps below add only the repository steps.

## Inputs

Read these files if they exist:

- `.github/instructions/delivery-mode.instructions.md`: use the discovery length, the sprint length and the owners.
- `discovery/scenarios/scenario.md`
- `discovery/plan.md`: if it exists, update it. Do not start again.
- `discovery/decision-log.md`, `discovery/raid-log.md`, `discovery/interface-register.md` and `discovery/access-tracker.md`

If the mode is `workshop-2-day`, tell the user that the 2-day workshop does not need a discovery plan. Ask if they want to continue, or run the `start-project` skill to change the mode.

## Questions

Ask the questions from the method with the ask questions tool, if it is available. Otherwise, ask them in the chat. Stop and wait for the answers. Do not ask again for a value that `delivery-mode.instructions.md` gives.

## Output

1. Write `discovery/plan.md`.
1. If a log does not exist in `discovery/`, copy its blank template from `discovery/templates/`. Do this for `raid-log.md`, `decision-log.md`, `interface-register.md`, `access-tracker.md` and `traceability.md`. Remove the example rows and the comment at the top. Do not change a log that exists.
1. Tell the user the first steps for week 1 from the plan.

## Validation

- Make sure that the plan has a section for each week, with a goal, Copilot steps and a checklist.
- Make sure that the plan ends with the playback and sign-off gate.
- Make sure that each skill in the plan is in `.github/skills/`.
- Make sure that each owner is a role or a name, or "Owner needed".
- Make sure that the file has no real patient data, secrets or tenant IDs.
