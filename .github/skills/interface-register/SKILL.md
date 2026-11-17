---
name: interface-register
description: 'Use when you create or update discovery/interface-register.md: each external system, its owner, how we connect, its test environment and the access lead time. Triggers: interface register, external systems, integrations, lead time, onboarding.'
---

# Interface Register

The interface register lists each external system that the service connects to. Its main purpose is to show **lead times** early, because waiting for access is often the biggest delay.

The columns, the typical systems and the rules are in the blank template [discovery/templates/interface-register.md](../../../discovery/templates/interface-register.md). People with only Microsoft 365 Copilot can copy the same template. So this skill has no prompt file.

## Steps

1. Read the template. Follow its columns and its rules.
1. If `discovery/interface-register.md` does not exist, copy the template to it. Delete the example row and the comment at the top.
1. Read `discovery/scenarios/scenario.md`, `discovery/user_journeys/data/` and `discovery/workshops/`. Find each external system that the use cases and journeys need.
1. Confirm each system with the user. Do not assume a system. Ask with the ask questions tool, if it is available. Otherwise, ask in the chat.
1. Add or update a row for each system. Use the next free `IF-` identifier.
1. If a lead time or a contact is not known, write "Not known". Add an action to find out, with an owner and a date.
1. If the lead time is after the date that we need it, add a risk to `discovery/raid-log.md`.
1. Tell the user to run the `access-tracker` skill, so that each interface that needs access gets an access request.

## Validation

- Make sure that each interface has a named contact, or "Owner needed".
- Make sure that the file has no secrets, tenant IDs, internal IP addresses or credentials.
