# Open questions

<!-- Copy this file to discovery/open-questions.md. Delete the example row and this comment. -->

**Service**: [service name]
**Owner**: [named person or role]
**Last updated**: [YYYY-MM-DD]

The open questions log collects the questions that discovery and design find in one place. Its purpose is to get a direction for each question before the build, so that the agents that build the service do not guess.

## When to update

- Collate the questions at the end of discovery, before the design phase. Use the `decision` skill in collate mode.
- Collate again after the NHS Product Owner writes the stories, and after the second pass of the NHS Architect.
- Add a question when a workshop, a story or an ADR finds a new question.

## Columns

| Column | Content |
|---|---|
| ID | `Q-NNN` |
| Question | The question, in one sentence |
| Source | Where the question came from. Example: `journey-book-appointment`, `A-004`, `DEC-002`, a workshop file, `story-007` |
| Blocks | The slice or the story that cannot start without a direction. Write "None" if the question does not block work |
| Direction from | The named person or role who can give the direction |
| Date needed | The date that we need the direction. Write "Date needed" if it is not known |
| Direction | The answer, or the direction that the team gives. Write `Assumption:` before an assumption that the team agrees to use until the answer is known |
| Decided by | The named person who gave the direction, and the date |
| Linked decision | The `DEC-` identifier, if the direction is a decision for the decision log |
| Status | Open, Direction given, Assumed or Closed |

## Rules

- Each question has an owner in "Direction from", and a date needed.
- A question that blocks a slice must have the status "Direction given" or "Assumed" before the slice starts.
- If a direction is a large decision, record it in `discovery/decision-log.md` with the `decision` skill. Put the `DEC-` identifier in "Linked decision".
- If a direction is an assumption, add it to the RAID log with the next free `A-` identifier.
- Do not delete a question. Change its status to "Closed" and give the reason.
- Never add real patient data.

## Questions

| ID | Question | Source | Blocks | Direction from | Date needed | Direction | Decided by | Linked decision | Status |
|---|---|---|---|---|---|---|---|---|---|
| Q-001 | Example: can patients book for a family member? | journey-book-appointment | Slice 1 | Product owner | YYYY-MM-DD | | | | Open |
