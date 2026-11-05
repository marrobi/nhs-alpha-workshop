---
name: start-project
description: 'Use when a team starts a new project from this kit, or changes how the project runs. Sets the delivery mode (2-day workshop or multi-week), discovery length, sprint length and owners in delivery-mode.instructions.md, then gives the playbook and the first skill to run. Triggers: start project, set up project, delivery mode, multi-week, workshop mode, /start-project.'
---

# Start Project

Set how the project runs. Write the settings to `.github/instructions/delivery-mode.instructions.md`. Agents and skills read that file. They do not assume a schedule.

This skill does **not** replace the root `README.md`. The `problem-statement` skill does that.

## Inputs

1. Read `.github/instructions/delivery-mode.instructions.md`.
1. Read [docs/playbooks/README.md](../../../docs/playbooks/README.md) for the phases.

## Questions

Ask these questions with the ask questions tool, if it is available. Otherwise, ask them in the chat. Stop and wait for the answers.

1. **Mode.** Do you run a 2-day workshop (`workshop-2-day`) or deliver over several weeks (`multi-week`)?
1. **Discovery length.** How long is discovery? Example: "pre-work before the workshop" or "4 weeks".
1. **Sprint length.** How long is a sprint? In `workshop-2-day` mode, write "Not used in workshop mode".
1. **Owners.** Who is the product owner, the Clinical Safety Officer, the data protection officer (DPO) or information governance (IG) lead, and the delivery lead? Record the role. Record a name only if the team chooses.

If the user does not know an answer, use the default from the file. Write "Owner needed" for an owner that is not known.

## Rules

- If the file has values that are not the defaults, show them. Ask before you overwrite them.
- Never record real patient data.
- Never record secrets, tenant IDs or credentials.
- Do not change the other instruction files.
- Keep the file short. It loads on every request.

## Output

1. Update the table in `.github/instructions/delivery-mode.instructions.md`:
   - **Mode**: `workshop-2-day` or `multi-week`.
   - **Discovery length** and **Sprint length**: the answers.
   - **Engineering review cadence**: in `workshop-2-day` mode, "After each batch (2 engineers), and at the end of each day (3 engineers)". In `multi-week` mode, "After each batch or pull request (2 engineers), and at the end of each sprint (3 engineers)".
   - **Governance records required**: optional in `workshop-2-day` mode. Required in `multi-week` mode.
   - **Owners**: the answers.
   - **Playbook**: a link to the playbook for the mode.
1. Tell the user which playbook to follow:
   - `workshop-2-day`: [docs/playbooks/workshop-2-day/README.md](../../../docs/playbooks/workshop-2-day/README.md).
   - `multi-week`: [docs/playbooks/multi-week/README.md](../../../docs/playbooks/multi-week/README.md).
1. Tell the user the first skill to run:
   - In both modes: `problem-statement`.
   - In `multi-week` mode, also: `discovery-plan`.

## Validation

- Make sure that the file has the frontmatter `applyTo: "**"`.
- Make sure that each setting in the table has a value.
- Make sure that the playbook link goes to a file that exists.
- Make sure that the file has no real patient data, secrets or tenant IDs.
