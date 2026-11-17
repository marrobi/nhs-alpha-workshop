# Traceability

<!-- Copy this file to discovery/traceability.md. Delete the example row and this comment. -->

**Service**: [service name]
**Owner**: [named person or role]
**Last updated**: [YYYY-MM-DD]

This table links each use case to its requirements, user stories, test approach, possible hazards and data protection points.

## Columns

| Column | Content |
|---|---|
| Use case | `UC-NN` from `discovery/scenarios/scenario.md` |
| Journey | The journey file in `discovery/user_journeys/data/` |
| Requirement | `FR-NNN` or `NFR-NNN` |
| Story | `story-NNN` from `user_stories/` |
| Test approach | How we prove the requirement. Example: automated end-to-end test |
| Hazard | `HAZ-NNN`, if there is one |
| DPIA item | The data protection impact assessment (DPIA) item, if there is one |

## Rules

- Each requirement links to a use case.
- Each requirement has a test approach before the playback.
- Each hazard mitigation links to a requirement.
- If a link is not known yet, write "To be confirmed".

## Table

| Use case | Journey | Requirement | Story | Test approach | Hazard | DPIA item |
|---|---|---|---|---|---|---|
| UC-01 | journey-book-appointment.md | FR-001 | story-001 | Automated end-to-end test | HAZ-001 | To be confirmed |
