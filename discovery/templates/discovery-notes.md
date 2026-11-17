# Discovery notes

<!-- Copy this file to discovery/discovery-notes.md. Delete the example rows and this comment. Keep the headings: agents find the sections by their names. -->

**Service**: [service name]
**Owner**: [named person or role]
**Last updated**: [YYYY-MM-DD]
**Review status**: Draft / Reviewed / Agreed
**Last reviewed by**: [name and role, and the date]

The discovery notes record the constraints, the riskiest assumptions, the scope of the alpha and the steer. The design phase starts from this file. The NHS Architect, the NHS Product Owner and the playback pack read it.

## Who updates this file

| Who | What they do |
|---|---|
| NHS Discovery Analyst agent | Makes the file from this template. Drafts and updates the sections from the scenario, the journeys, the workshop notes, the interface register and the RAID log. Marks each new or changed row `Draft` |
| `workshop-to-actions` skill | Proposes changes after a workshop. Marks them `Draft` |
| Other agents | Read the file only. If they find a change, they tell the user. They do not edit the file |
| The team (people) | Review each `Draft` row and change it to `Reviewed`, or correct it. Agree the steer |
| The product owner | Agrees the scope and the steer. Only a named person can set a row to `Agreed` |

## Rules

- An agent never sets a row to `Reviewed` or `Agreed`, and never writes a name in "Agreed by" or "Reviewed by".
- Review the file before the design phase, and after each workshop that changes it. In multi-week delivery, review it each week and before the playback.
- The design phase starts only when the "Steer" section is `Agreed`.
- Mark what we know and what we assume. Write `Assumption:` before an assumption.
- Link items by identifier: `UC-`, `A-`, `R-`, `IF-`, `DEC-`, `Q-`.
- Do not delete a row. Change its status to `Closed` and give the reason in the change history.
- Never add real patient data, secrets or internal network details.

## Constraints

| ID | Type | Constraint | Effect on the alpha | Source | Status |
|---|---|---|---|---|---|
| C-01 | Technical | Example: the service must connect to the trust booking system | We need an interface and an alpha approach (see IF-01) | Workshop YYYY-MM-DD | Draft |
| C-02 | Legislative | Example: health data is UK GDPR special category data | We need a DPIA before beta | Team | Draft |

Types: Technical, Legislative, Clinical safety, Organisational, Commercial, Time.

## Riskiest assumptions

| ID | Assumption | Why it is risky | How the alpha tests it | RAID link | Status |
|---|---|---|---|---|---|
| RA-01 | Example: patients will book online instead of by phone | If false, the service has no users | Slice 1: book a slot, with usability tests | A-001 | Draft |

## Scope of the alpha

### In scope

| Use case | Journey | Reason | Status |
|---|---|---|---|
| UC-01 | journey-book-appointment | Tests RA-01 | Draft |

### Out of scope

| Item | Reason | Revisit in | Status |
|---|---|---|---|
| Example: payments | Not needed to test RA-01 | Beta | Draft |

## Steer

The steer tells the design phase what to build first. The product owner agrees it.

| Item | Value |
|---|---|
| Priority journey | [journey file name] |
| Riskiest assumption | [RA- identifier and the assumption] |
| First slice candidate | [the shortest path, or the whole journey] |
| Slice type | Thin slice / Journey slice |
| Blocking questions | [Q- identifiers, each with a direction, or "None"] |
| Agreed by | [name and role — a person only] |
| Date agreed | [YYYY-MM-DD] |
| Status | Draft / Agreed |

## Change history

| Date | Change | Made by (person or agent) | Reviewed by |
|---|---|---|---|
| YYYY-MM-DD | File made from the template | NHS Discovery Analyst | |
