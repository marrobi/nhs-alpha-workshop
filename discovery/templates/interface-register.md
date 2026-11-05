# Interface register

<!-- Copy this file to discovery/interface-register.md. Delete the example row and this comment. -->

**Service**: [service name]
**Owner**: [named person or role]
**Last updated**: [YYYY-MM-DD]

The interface register lists each external system that the service connects to. Its main purpose is to show **lead times** early, because waiting for access is often the biggest delay.

## When to update

- Start at the beginning of discovery with the systems that you know.
- Complete it before the playback.
- Update it when a decision adds or removes a system.

## Columns

| Column | Content |
|---|---|
| ID | `IF-NN` |
| System | The name of the system |
| Owner | The organisation and the named contact |
| Direction | Inbound, outbound or both |
| Purpose | What we use it for. Give the `UC-` identifiers |
| Protocol | Example: HTTPS REST, FHIR, file. "Not known" is permitted |
| Authentication | Example: OAuth 2.0, mutual TLS. "Not known" is permitted |
| Network | Example: internet, Health and Social Care Network (HSCN), private link |
| Test environment | Yes or no, the name, and how to get access |
| Onboarding steps | Forms, approvals, assurance and tests |
| Lead time | The time from request to access, in weeks |
| Access request | The `AR-` identifier in `discovery/access-tracker.md` |
| Needed by | The date or sprint that needs it |
| Risk | The `R-` identifier, if the lead time is after the date that we need it |
| Status | Open, Requested, Available, Blocked |

## Typical systems

Confirm each system in discovery. Do not assume it.

- The systems that the users use now. Example: a booking system or a patient record system.
- National services, if the use cases need them. Examples: the Personal Demographics Service (PDS), the Organisation Data Service (ODS), NHS login, the NHS Terminology Server.
- The identity provider, monitoring and support tools.

## Rules

- Each interface has a named contact.
- If the lead time is not known, write "Not known". Add an action to find out, with an owner and a date.
- If the lead time is after the date that we need it, add a risk to the RAID log.
- Add an access request to `discovery/access-tracker.md` for each interface that needs access.

## Interfaces

| ID | System | Owner | Direction | Purpose | Protocol | Authentication | Network | Test environment | Onboarding steps | Lead time | Access request | Needed by | Risk | Status |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| IF-01 | Example: appointment booking system | Example Trust, digital team, contact to be named | Both | UC-01 | Not known | Not known | Not known | Not known | Not known | Not known | AR-01 | YYYY-MM-DD | | Open |
