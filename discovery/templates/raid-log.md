# RAID log

<!-- Copy this file to discovery/raid-log.md. Delete the example rows and this comment. -->

**Service**: [service name]
**Owner**: [named person or role]
**Last updated**: [YYYY-MM-DD]

This log records risks, assumptions, issues and dependencies (RAID). Each item has an owner and a date, so that nothing waits without a reason.

## Columns

| Column | Content |
|---|---|
| ID | `R-NNN`, `A-NNN`, `I-NNN` or `D-NNN` |
| Description | One sentence |
| Effect | The effect on delivery, safety or cost |
| Likelihood | High, medium or low. Risks only |
| Impact | High, medium or low |
| Mitigation or next step | What we will do |
| Owner | Named person or role |
| Date | The date for the next step |
| Links | Related `DEC-`, `AR-`, `IF-`, `UC-` or `HAZ-` identifiers |
| Source | The workshop, person or document |
| Status | Open, In progress, Closed |

## Rules

- An assumption that is not tested by its date becomes a risk.
- A dependency without a date becomes a risk.
- An issue is a risk that has happened. Give its effect now.
- If an owner or a date is not known, write "Owner needed" or "Date needed".
- Do not delete an item. Change its status to `Closed` and give the reason.

## Risks

| ID | Description | Effect | Likelihood | Impact | Mitigation or next step | Owner | Date | Links | Source | Status |
|---|---|---|---|---|---|---|---|---|---|---|
| R-001 | Example: test accounts for the booking system are not available before the first build. | The team cannot test the first journey. | Medium | High | Request the accounts now. | Delivery lead | YYYY-MM-DD | AR-01 | Kick-off workshop | Open |

## Assumptions

| ID | Description | Effect | Likelihood | Impact | Mitigation or next step | Owner | Date | Links | Source | Status |
|---|---|---|---|---|---|---|---|---|---|---|
| A-001 | | | | | | | | | | Open |

## Issues

| ID | Description | Effect | Likelihood | Impact | Mitigation or next step | Owner | Date | Links | Source | Status |
|---|---|---|---|---|---|---|---|---|---|---|
| I-001 | | | | | | | | | | Open |

## Dependencies

| ID | Description | Effect | Likelihood | Impact | Mitigation or next step | Owner | Date | Links | Source | Status |
|---|---|---|---|---|---|---|---|---|---|---|
| D-001 | | | | | | | | | | Open |
