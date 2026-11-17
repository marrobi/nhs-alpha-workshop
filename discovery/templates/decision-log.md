# Decision log

<!-- Copy this file to discovery/decision-log.md. Delete the example rows and this comment. -->

**Service**: [service name]
**Owner**: [named person or role]
**Last updated**: [YYYY-MM-DD]

This log records the decisions that people made and the decisions that are still open.

## Columns

| Column | Content |
|---|---|
| ID | `DEC-NNN` |
| Decision or question | One sentence |
| Options | The options that we considered |
| Outcome | The decision, or "Open" |
| Decided by | Name and role. Empty if open |
| Date needed | The latest date for the decision |
| Date decided | The date of the decision. Empty if open |
| Blocks | What waits for this decision |
| ADR | The architecture decision record (ADR) number, if there is one |
| Source | The workshop, person or document |
| Status | Open, Decided, Superseded |

## Rules

- Never write a decision as decided without a named person.
- An open decision after its "date needed" becomes a risk in the RAID log.
- A large decision also gets an ADR in `docs/adr/`. A decision is large if it is difficult to change, has a large cost, changes the technology, or changes clinical safety or data protection.
- Do not delete a decision. Change its status to `Superseded` and give the ID of the new decision.

## Decisions

| ID | Decision or question | Options | Outcome | Decided by | Date needed | Date decided | Blocks | ADR | Source | Status |
|---|---|---|---|---|---|---|---|---|---|---|
| DEC-001 | Example: which journey do we build first? | 1. Book an appointment. 2. Change an appointment. | Open | | YYYY-MM-DD | | First build | | Kick-off workshop | Open |
