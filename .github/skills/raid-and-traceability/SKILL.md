---
name: raid-and-traceability
description: 'Use when you create or update the RAID log (risks, assumptions, issues, dependencies) or the traceability table in discovery/. Triggers: RAID, risk log, risks, assumptions, dependencies, traceability, use case to requirement. Gives the columns and the rules for each.'
---

# RAID and Traceability

This skill keeps the discovery records consistent. Each item has an owner and a date, so that nothing waits without a reason.

The format and the rules for each record are in a blank template. People with only Microsoft 365 Copilot can copy the same templates. So this skill has no prompt file.

| Record | Template | Output |
|---|---|---|
| RAID log | [discovery/templates/raid-log.md](../../../discovery/templates/raid-log.md) | `discovery/raid-log.md` |
| Traceability | [discovery/templates/traceability.md](../../../discovery/templates/traceability.md) | `discovery/traceability.md` |
| Decision log | [discovery/templates/decision-log.md](../../../discovery/templates/decision-log.md) | `discovery/decision-log.md`. Use the `decision` skill to add a decision |
| Access tracker | [discovery/templates/access-tracker.md](../../../discovery/templates/access-tracker.md) | `discovery/access-tracker.md`. Use the `access-tracker` skill to update it |

## Steps

1. Read the template for the record. Follow its columns and its rules.
1. If the output file does not exist, copy the template to the output path. Delete the example rows and the comment at the top.
1. Read the output file. Find the next free identifier for each type.
1. Add or update the items that the user gives. If an owner or a date is missing, ask the user with the ask questions tool, if it is available. Otherwise, write "Owner needed" or "Date needed".
1. Apply the rules of the template:
   - An assumption that is not tested by its date becomes a risk.
   - A dependency without a date becomes a risk.
   - An open decision after its "date needed" becomes a risk.
   - An access request with an expected date after its "needed by" date becomes a risk.
1. For traceability, read `discovery/scenarios/scenario.md`, `discovery/user_journeys/data/` and `user_stories/`, if they exist. Link each requirement to a use case.
1. Update the "Last updated" date.

## Validation

- Make sure that each item has an owner, a date, a source and a status.
- Make sure that no identifier is used twice.
- Make sure that no item is deleted. A closed item has the status `Closed` and a reason.
- Make sure that the files have no real patient data.
