# Access tracker

<!-- Copy this file to discovery/access-tracker.md. Delete the example rows and this comment. -->

**Service**: [service name]
**Owner**: [named person or role]
**Last updated**: [YYYY-MM-DD]

The access tracker lists each access that the team needs, and when. Waiting for access is often the biggest delay, so start the longest requests first.

## Columns

| Column | Content |
|---|---|
| ID | `AR-NN` |
| Access | What access we need |
| Interface | The `IF-` identifier, if there is one |
| Requested by | Name or role |
| Owner at provider | Name and organisation |
| Date requested | |
| Lead time | Weeks, or "Not known" |
| Expected date | |
| Needed by | Date or sprint |
| Status | Not requested, Requested, Granted, Blocked |

## Rules

- Order the table by "needed by" date. Then order it by lead time, longest first.
- If the expected date is after the "needed by" date, escalate it and add a risk to the RAID log.
- If the owner or the lead time is not known, write "Owner needed" or "Not known".

## Common access needs

Make sure that the tracker has a row for each of these needs, if the service needs it:

- Cloud subscription and landing zone.
- Identity tenant and app registrations.
- Network connection to the systems in the interface register.
- Test environments and synthetic test data for each external system.
- National service onboarding, if the use cases need it.
- Source code repository, pipelines and tools.
- Team accounts and devices.

## Access requests

| ID | Access | Interface | Requested by | Owner at provider | Date requested | Lead time | Expected date | Needed by | Status |
|---|---|---|---|---|---|---|---|---|---|
| AR-01 | Example: test accounts for the appointment booking system | IF-01 | Delivery lead | Owner needed | | Not known | | YYYY-MM-DD | Not requested |
