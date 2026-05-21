---
applyTo: "**"
---

# Health & Service Identifiers

How to store, validate, display, and protect identifiers used in UKHSA services. 

For .NET / EF Core implementation patterns, see `tech-stack.instructions.md`.

---

## NHS Number (ISB 0149)

The NHS Number remains the canonical patient identifier across UK health data. UKHSA services that exchange patient data with NHS systems MUST conform to **ISB 0149: NHS Number Standard for Secondary Use**.

### Storage

- Stored as a 10-digit string. Leading zeros MUST be preserved — never store as a numeric type
- Stored without spaces or separators
- Stored only where there is a documented lawful basis. NHS Number is personal data under UK GDPR

### Validation

- Validate the format (exactly 10 digits) at the system boundary using a request-model validator
- Validate the **modulus 11 check digit** before accepting the value:
  1. Multiply each of the first 9 digits by weights 10, 9, 8, 7, 6, 5, 4, 3, 2 respectively
  2. Sum the results
  3. Take the remainder of the sum divided by 11
  4. Subtract that remainder from 11
  5. If the result is 11, the check digit is 0. If the result is 10, the NHS Number is invalid
  6. Otherwise, the result MUST equal the 10th digit

- Reject invalid NHS Numbers at the API boundary with a `400 Bad Request` problem details response

### Display

- Display in the standard **3-3-4** grouping for readability: `943 476 5919`
- This is presentation only — never store with spaces
- When showing on screen alongside other identifiers, label the field "NHS number" (lowercase 'n' on "number" per GOV.UK content style)

### Masking & Logging

- NHS Numbers MUST NOT appear unmasked in logs, error messages, analytics events, or non-production environments where the operator does not need to see them
- Masked form for diagnostic display: `*** *** 5919` (last four digits only)
- A reusable formatting and masking helper SHOULD live in a shared library so the rule cannot be forgotten

### Synthetic Data

- Test data MUST use the published synthetic NHS Number ranges: **9000000001 – 9999999999** (the 9xx range is reserved for testing and will never collide with a real NHS Number)
- Example for tests and documentation: `943 476 5919`
- Never use a real NHS Number in source code, fixtures, screenshots, or example payloads

---

## Other UKHSA Identifiers

UKHSA services frequently introduce service-specific identifiers (case IDs, batch IDs, vaccine UIDs, organisation codes, surveillance event IDs). The following rules apply:

### Definition

- Every new identifier type MUST be defined in a published data dictionary before exposure on any API or UI
- The definition MUST specify: format, length, character set, check-digit (if any), case-sensitivity, uniqueness scope, and lifecycle
- Where an ODS code, UPRN, or other existing UK identifier already serves the purpose, **reuse it** — do not mint a new one

### Format

- Identifiers SHOULD be URL-safe and case-insensitive unless there is a strong reason otherwise
- Identifiers MUST NOT embed personal data
- Globally unique identifiers SHOULD be ULIDs or UUIDv7 (sortable) where the consumer benefits from chronological order; otherwise UUIDv4
- Sequential numeric IDs MUST NOT be exposed externally without an opacity layer (HMAC-based hashing) to prevent enumeration

### Validation

- All identifiers MUST be validated against the data dictionary spec at the API boundary
- Format validation MUST happen before any database lookup — never let user input drive an unbounded query

### Storage

- Stored exactly as specified in the data dictionary (canonical case, no padding)
- Indexed appropriately for the access pattern

### Display & Logging

- Display follows GOV.UK content guidance — lowercase labels, sentence case
- Identifiers that act as access tokens (e.g. one-time references in URLs) MUST be treated as secrets — never logged in plain text

---

## ODS Codes

Where a service interacts with NHS organisations, use **Organisation Data Service (ODS) codes** as the canonical organisation identifier. Validate against the ODS reference data, not against a free-text field.

## Postcodes

- Validate UK postcodes against the canonical regex from GOV.UK guidance or via a lookup service (Ordnance Survey / OS Places API where licensed)
- Store in canonical uppercase form with a single space (`SW1A 1AA`)
- Never use postcodes as a primary key or for joining records — they change

## Date of Birth

- Store as `DateOnly` (`date` in SQL) — never `DateTime`
- Validate that the date is in the past and within plausible bounds (e.g. age ≤ 130)
- Display in the GOV.UK pattern (`27 March 1985`); collect with `<govuk-date-input>` (day / month / year as separate inputs)
