# Prompt: weekly status

## How to use

1. Open Microsoft 365 Copilot Chat at <https://m365.cloud.microsoft/chat/>.
1. Copy all of this file below the line, and paste it into the chat.
1. Paste the inputs that are listed in the "Inputs" section.
1. Save the output to `discovery/status/YYYY-MM-DD.md` in the repository. Use the date of the Monday of the week. If you do not have access to the repository, give the file to a team member who has access.
1. To make slides, ask Copilot in PowerPoint to make a deck from the status report.

GitHub Copilot users run the `weekly-status` skill instead. The skill uses this method. To make slides, they use the `marp-slides` skill on the output.

---

You help an NHS discovery team to write a weekly status report. Follow the method, the rules and the output template below. Write in Simplified Technical English: short sentences, active voice, one instruction in each sentence. Write each acronym in full the first time.

## Inputs

Ask me for these inputs if I did not give them:

1. The week. If I do not give a week, use the current week.
1. The planned date for the playback or sign-off.
1. The workstreams. If I do not give them, use the default workstreams in the template.
1. The RAID log from `discovery/raid-log.md`.
1. The decision log from `discovery/decision-log.md`.
1. The access tracker from `discovery/access-tracker.md`.
1. Optional: the workshop files for this week from `discovery/workshops/`.
1. Optional: the plan or the checklist for this week.

## Rules

- Show only facts from the inputs. Write `Assumption:` before an assumption.
- Never use real patient data.
- RAG means red, amber, green. Green: on track. Amber: at risk, with a plan. Red: off track, help needed.
- Show only the top 5 risks.
- Show each item without an owner or a date as a gap.

## Method

1. Find the work that was done this week.
1. Give each workstream a RAG status and a reason.
1. Find the open decisions with a date needed in the next 2 weeks, or in the past.
1. Find the access requests that are blocked or late.
1. Find the top 5 risks by impact and likelihood.
1. Find the open actions from the workshop files.
1. Write the report with the template below.

## Output template

```markdown
# Status: week of [YYYY-MM-DD]

## Summary

[3 sentences maximum. Are we on track for the playback on the planned date?]

## Workstreams

| Workstream | RAG | Reason | Next step |
|---|---|---|---|
| Problem and users | | | |
| Journeys | | | |
| Decisions and risks | | | |
| Interfaces and access | | | |
| Playback and sign-off | | | |

## Done this week

- [Item, with a link to the document]

## Checklist for this week

[If the inputs have a checklist for this week, copy it. Mark each item done or not done. If there is no checklist, write "No checklist given".]

## Decisions needed

| ID | Decision | Owner | Date needed |
|---|---|---|---|

## Blocked or late access

| ID | Access | Owner | Needed by | Expected |
|---|---|---|---|---|

## Top risks

| ID | Risk | Owner | Next step | Date |
|---|---|---|---|---|

## Open actions

| ID | Action | Owner | Date |
|---|---|---|---|

## Gaps

- [Items without an owner or a date]

## Next week

- [Focus items]
```
