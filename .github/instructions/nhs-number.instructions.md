---
applyTo: "**"
---

# NHS Number Standard (ISB 0149)

Rules for storing, displaying, validating, and transmitting the NHS Number. Derived from [ISB 0149 NHS Number Standard](https://digital.nhs.uk/data-and-information/information-standards/information-standards-and-data-collections-including-extractions/publications-and-notifications/standards-and-collections/isb-0149-nhs-number) — the authoritative specification for NHS Number usage in applicable systems.

See `tech-stack.instructions.md` for the current stack. The rules below are framework-agnostic.

---

## Storage

- Store the NHS Number as a **10-digit numeric string** per the [NHS Data Dictionary](https://www.datadictionary.nhs.uk/attributes/nhs_number.html) — never as an integer (leading zeros would be lost)
- Systems MAY store whether the NHS Number has been **verified against the Personal Demographics Service (PDS)** and when verification last occurred

## Display

- **Every screen** showing patient-identifiable data MUST display the NHS Number (if available). The PDS verification status SHOULD also be displayed if maintained
- **All hard-copy outputs** containing patient-identifiable data MUST include the NHS Number (if available at time of output)
- Display and print the NHS Number in **3-3-4 format** with spaces: `943 476 5919` — never as a continuous 10-digit string
- Where full display is not necessary, mask the first 7 digits and show only the last 3: `*** *** 5919`
- Label the field clearly as "NHS number" (lowercase "number") in user-facing text

## Input

- Accept the NHS Number as **10 digits with or without spaces** — do not reject input that includes spaces between digit groups
- Strip all whitespace before storing or validating

## Validation

- **MUST validate both format and check digit** on every NHS Number input — never store an unvalidated NHS Number
- Format: exactly 10 digits after whitespace removal
- Check digit: **modulus 11 algorithm** — multiply each of the first 9 digits by a weight (10, 9, 8, … 2), sum the products, take modulus 11, subtract from 11 to get the check digit. A result of 11 means check digit 0; a result of 10 means the number is invalid
- Use a Pydantic validator (or equivalent in the current stack) at the API boundary
- Return a clear, user-friendly error message on failure: "Enter a valid NHS number" — not a technical description of the algorithm

## Search

- Systems MUST allow searching for a patient record using the **NHS Number as the only search criterion**
- Systems MUST also allow searching for a patient record **without** the NHS Number (using other demographics)
- Systems MAY support combined search using NHS Number alongside other demographic fields

## Electronic Transmission

- The NHS Number MUST be included in **all electronic communications** containing patient-identifiable data, unless:
  - The NHS Number is not available at the time of transmission, or
  - Including it conflicts with other standards, legislation, or confidentiality requirements
- Never send the NHS Number in URL query parameters — use POST request bodies

## Identity Confirmation

- When a record is retrieved by NHS Number, **other demographic information** (name, date of birth) MUST be used to confirm the patient's identity and that the correct record has been retrieved
- Do not rely on the NHS Number alone to confirm identity

## Duplicate Detection

- Systems MUST be capable of **detecting and reporting** where the same NHS Number (verified or not) appears on more than one patient record
- Systems SHOULD be capable of reporting all patient records that have **no NHS Number** recorded

## Logging

- **Never log** full NHS Numbers in application logs — see `nhs-security.instructions.md` for PII logging rules
- If an NHS Number must appear in logs for tracing, mask it: `*** *** 5919`

## Testing

- Use **synthetic NHS numbers only** in tests and development — never real patient data
- Standard synthetic NHS number for tests: `943 476 5919` (valid check digit)
- Test both valid and invalid NHS numbers, including numbers that fail the check digit
