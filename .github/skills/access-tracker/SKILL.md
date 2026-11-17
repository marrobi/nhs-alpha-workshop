---
name: access-tracker
description: 'Use when you need to update the access tracker from the interface register, and list the access requests to make now and the ones to escalate. Triggers: access tracker, access requests, lead times, escalate access, /access-tracker. Writes discovery/access-tracker.md.'
---

# Access Tracker

Follow the method, the rules and the output template in [discovery/prompts/access-tracker.md](../../../discovery/prompts/access-tracker.md). Read that file first. The columns agree with [discovery/templates/access-tracker.md](../../../discovery/templates/access-tracker.md).

The steps below add only the repository steps.

## Inputs

1. Read `discovery/interface-register.md`. If it does not exist, tell the user to make it with the `interface-register` skill. You can continue with only the common access needs.
1. Read `discovery/access-tracker.md`. If it does not exist, copy `discovery/templates/access-tracker.md` to `discovery/access-tracker.md` first. Delete the example row.
1. Read `discovery/raid-log.md` to find the next free `R-` identifier.

## Questions

If a "needed by" date is missing for an item that blocks the first build, ask the user for it with the ask questions tool, if it is available. Otherwise, ask in the chat.

## Output

1. Update the table in `discovery/access-tracker.md`.
1. Add the `AR-` identifier to the "Access request" column of each interface in `discovery/interface-register.md`.
1. Add a risk to `discovery/raid-log.md` for each item to escalate.
1. Give the report from the method in the chat.

## Validation

- Make sure that the table is in order of "needed by" date, then lead time.
- Make sure that each item has an owner, or "Owner needed".
- Make sure that the file has no secrets, tenant IDs or credentials.
