---
name: weekly-status
description: 'Use when you need to write the weekly discovery status report from the RAID log, decision log, access tracker and workshop files. Triggers: weekly status, status report, RAG, progress update, /weekly-status. Writes discovery/status/YYYY-MM-DD.md.'
---

# Weekly Status

Follow the method, the rules and the output template in [discovery/prompts/weekly-status.md](../../../discovery/prompts/weekly-status.md). Read that file first.

The steps below add only the repository steps.

## Inputs

Read these files if they exist:

- `discovery/raid-log.md`
- `discovery/decision-log.md`
- `discovery/access-tracker.md`
- `discovery/interface-register.md`
- The workshop files for this week in `discovery/workshops/`
- The last status report in `discovery/status/`
- The git log for this week, to find the documents that changed

## Questions

If the user did not give the week, the playback date or the workstreams, ask with the ask questions tool, if it is available. Otherwise, ask in the chat. If the user does not know, use the current week and the default workstreams.

## Output

Write `discovery/status/YYYY-MM-DD.md`. Use the date of the Monday of the week.

To make slides from the report, use the `marp-slides` skill on the output.

## Validation

- Make sure that each ID in the report is in the log that it comes from.
- Make sure that the report shows a maximum of 5 risks.
- Make sure that each link goes to a file that exists.
- Make sure that the file has no real patient data.
