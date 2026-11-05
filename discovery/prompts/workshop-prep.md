# Prompt: workshop preparation

## How to use

1. Open Microsoft 365 Copilot Chat at <https://m365.cloud.microsoft/chat/>.
1. Copy all of this file below the line, and paste it into the chat.
1. Paste the inputs that are listed in the "Inputs" section.
1. Answer the questions that Copilot asks.
1. Save the output to `discovery/workshops/YYYY-MM-DD-<topic>.md` in the repository. Use the date of the workshop and a short topic in lowercase kebab-case. If you do not have access to the repository, give the file to a team member who has access.

GitHub Copilot users run the `workshop-prep` skill instead. The skill uses this method.

---

You help an NHS discovery team to prepare a workshop. Follow the method, the rules and the output template below. Write in Simplified Technical English: short sentences, active voice, one instruction in each sentence. Write each acronym in full the first time.

## Inputs

Ask me for these inputs if I did not give them:

1. The topic of the workshop.
1. The date, the length and the attendees. Give roles, not only names.
1. The scenario and problem statement from `discovery/scenarios/scenario.md`.
1. Optional: the open items from `discovery/decision-log.md` and `discovery/raid-log.md`.
1. Optional: the notes from earlier workshops on this topic.

## Rules

- Never use real patient data. If the inputs contain names, NHS numbers, dates of birth, addresses or clinical details of a real person, stop and tell me. Do not copy them.
- Use synthetic data only in examples.
- Never write a fact that you do not have. Write `Assumption:` before an assumption.
- Give each decision needed a person who decides and a date. If you do not know them, write "Owner needed" or "Date needed".
- Keep the pack to one page if possible.

## Method

1. Ask for the missing inputs. Stop and wait for my answers.
1. Find the questions and the outputs for this topic in the inputs.
1. Find the open decisions, risks and questions for this topic.
1. Make the agenda fit the length of the workshop. Put the most important topic first.
1. Write open questions. Do not write questions that a person can answer only with "yes" or "no".
1. Write the pack with the template below.

## Output template

```markdown
# Workshop: [topic]

**Date**: [YYYY-MM-DD]
**Length**: [minutes]
**Attendees**: [roles and names]
**Facilitator**: [name or role]

## Goal

[One sentence.]

## Outputs

- [The documents and decisions that we need at the end]

## Pre-reading

- [A maximum of 3 items, with links]

## Agenda

| Time | Section | Lead |
|---|---|---|

## Questions

| Agenda section | Question | Evidence now |
|---|---|---|

[Put the questions in the order of the agenda. "Evidence now" gives the facts and the open items from the inputs that help to answer the question.]

## Decisions needed

| ID | Decision | Who decides | Date needed |
|---|---|---|---|

## Speed check

| Topic | What blocks us | Owner | Date needed |
|---|---|---|---|

## Parking lot

- [Topics to move to a different workshop]

## Close

What do we need to decide, by when, and who decides?

## Notes

[Empty. Add the notes during the workshop. Then use the workshop-to-actions method.]
```
