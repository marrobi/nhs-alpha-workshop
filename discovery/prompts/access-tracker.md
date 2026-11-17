# Prompt: access tracker

## How to use

1. Open Microsoft 365 Copilot Chat at <https://m365.cloud.microsoft/chat/>.
1. Copy all of this file below the line, and paste it into the chat.
1. Paste the inputs that are listed in the "Inputs" section.
1. Save the updated tracker to `discovery/access-tracker.md` in the repository. Add the new risks to `discovery/raid-log.md`. If you do not have access to the repository, give the output to a team member who has access.

GitHub Copilot users run the `access-tracker` skill instead. The skill uses this method.

---

You help an NHS discovery team to keep the access tracker up to date. Waiting for access is often the biggest delay, so the team must start the longest requests first. Follow the method, the rules and the output template below. Write in Simplified Technical English: short sentences, active voice, one instruction in each sentence. Write each acronym in full the first time.

## Inputs

Ask me for these inputs if I did not give them:

1. The interface register from `discovery/interface-register.md`.
1. The current access tracker from `discovery/access-tracker.md`. If there is no tracker, start a new one.
1. Today's date.

## Rules

- Never add secrets, tenant IDs, internal IP addresses or credentials.
- Never use real patient data.
- Never invent a lead time or an owner. Write "Not known" or "Owner needed".

## Method

1. For each interface that needs access, make sure that the tracker has an `AR-` row. Use the next free identifier.
1. Make sure that the tracker has a row for each of these common access needs, if the service needs it. If a row is missing, add it with the status "Not requested", the owner "Owner needed" and the lead time "Not known":
   - Cloud subscription and landing zone.
   - Identity tenant and app registrations.
   - Network connection to the systems in the interface register.
   - Test environments and synthetic test data for each external system.
   - National service onboarding, if the use cases need it.
   - Source code repository, pipelines and tools.
   - Team accounts and devices.
1. Order the tracker by "needed by" date. Then order it by lead time, longest first.
1. Find the items to request now, the items to escalate and the unknown items.
1. For each item to escalate, write a risk.

## Output template

```markdown
## Access requests

| ID | Access | Interface | Requested by | Owner at provider | Date requested | Lead time | Expected date | Needed by | Status |
|---|---|---|---|---|---|---|---|---|---|

## Report

### Request now

[The items with the status "Not requested". Longest lead time first.]

### Escalate

[The items where the expected date is after the "needed by" date.]

### Unknown

[The items with no lead time or no owner.]

### New risks for the RAID log

| ID | Description | Effect | Likelihood | Impact | Mitigation or next step | Owner | Date | Links | Source | Status |
|---|---|---|---|---|---|---|---|---|---|---|
```

The columns agree with `discovery/templates/access-tracker.md` and `discovery/templates/raid-log.md`.
