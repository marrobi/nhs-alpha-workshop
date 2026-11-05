---
name: nhs-synthetic-data
description: 'Use when generating synthetic NHS test data including NHS numbers, patient names, SNOMED codes, and seed scripts for development and demos.'
---

# NHS Synthetic Data — Test Data Generation

This skill generates realistic but entirely synthetic test data for NHS digital services. Real patient data must never be used in development, testing, or demos — this is a UK GDPR and NHS policy requirement.

## When to Use

- Creating seed data for local development databases
- Writing test fixtures for pytest or Vitest
- Building demo datasets for stakeholder presentations
- Generating realistic form submissions for E2E tests
- Populating prototype screens with example data

## NHS Number Generation

NHS numbers are 10 digits with a Modulus 11 check digit. The algorithm:

1. Multiply each of the first 9 digits by (11 - position), where position is 1-indexed
2. Sum the products
3. Remainder = sum mod 11
4. Check digit = 11 - remainder
5. If check digit is 11, use 0. If check digit is 10, the number is invalid — regenerate

Implement `generate`, `format` (3-3-4 with spaces), and `validate` functions in the backend language from `tech-stack.instructions.md`.

Make synthetic NHS numbers in the **999 range**. The NHS does not give numbers in this range to patients.

### Pre-validated Synthetic NHS Numbers

| NHS Number | Formatted | Use Case |
|---|---|---|
| 9990000018 | 999 000 0018 | Default test patient |
| 9990000026 | 999 000 0026 | Second test patient |
| 9990000034 | 999 000 0034 | Third test patient |
| 9990000042 | 999 000 0042 | Fourth test patient. Unverified identity |
| 9990000050 | 999 000 0050 | Fifth test patient |
| 9990000019 | 999 000 0019 | **Not valid**: incorrect check digit |
| 9990000000 | 999 000 0000 | **Not valid**: the check digit is 10 |

The Personal Demographics Service (PDS) sandbox has its own test patients, for example `943 476 5919`. Use these numbers only with the PDS sandbox, because PDS does not return the 999 numbers.

## Synthetic Patient Data

### Patient Records

Use these synthetic personas consistently across the service:

| NHS Number | Given Name | Family Name | DOB | Postcode | GP Surgery | Gender |
|---|---|---|---|---|---|---|
| 999 000 0018 | Sarah | Thompson | 1991-03-15 | LS1 4AP | Kirkstall Health Centre | female |
| 999 000 0026 | James | Wilson | 1954-08-22 | M1 2WD | Manchester Medical Practice | male |
| 999 000 0034 | Priya | Patel | 1985-11-30 | B1 1BB | City Centre Surgery | female |
| 999 000 0042 | David | Roberts | 1968-06-10 | SW1A 1AA | Westminster Health Practice | male |
| 999 000 0050 | Fatima | Khan | 2000-01-25 | LS2 9JT | University Health Service | female |

### Synthetic Appointments

| Patient NHS Number | Date | Time | Type | Clinician | Location | Status |
|---|---|---|---|---|---|---|
| 999 000 0018 | 2026-03-15 | 09:30 | GP appointment | Dr. Ahmed | Kirkstall Health Centre | booked |
| 999 000 0018 | 2026-03-22 | 14:00 | Blood test | Nurse Williams | Kirkstall Health Centre | booked |
| 999 000 0026 | 2026-03-10 | 10:15 | Annual health check | Dr. Chen | Manchester Medical Practice | completed |

### Synthetic Clinical Codes (SNOMED CT)

Use real SNOMED CT concept IDs with synthetic patient associations:

| SNOMED Code | Display Term | Common Use |
|---|---|---|
| 38341003 | High blood pressure (hypertension) | Long-term condition |
| 73211009 | Diabetes mellitus (diabetes) | Long-term condition |
| 195967001 | Asthma | Long-term condition |
| 386661006 | Fever | Symptom |
| 25064002 | Headache | Symptom |
| 267036007 | Shortness of breath | Symptom |
| 182531007 | Paracetamol (medication) | Prescription |
| 318475005 | Amoxicillin (medication) | Prescription |

## Test Fixtures

Create test fixtures from the patient and appointment data above, using the test framework from `tech-stack.instructions.md`. Provide both single-patient and all-patients fixtures for unit and integration tests respectively.

## Rules

- **Never use real NHS numbers** — use the pre-validated list, or generate numbers in the 999 range with the Modulus 11 algorithm
- **PDS test numbers only in the PDS sandbox** — use published PDS test numbers such as `943 476 5919` only with the PDS sandbox
- **Never use real patient names** — use the synthetic personas defined above
- **Never use real postcodes linked to real people** — use well-known institutional postcodes
- **Synthetic data must be clearly labelled** — add comments marking data as synthetic in seed scripts
- **NHS number display** — always format as 3-3-4 with spaces: `999 000 0018`
- **Dates** — use future dates for appointments, past dates for historical records
- **Consistency** — use the same synthetic patients across all tests and demos for a coherent narrative

## References

- [NHS Number Format](https://digital.nhs.uk/services/nhs-number)
- [NHS Number Modulus 11 Check](https://www.datadictionary.nhs.uk/attributes/nhs_number.html)
- [SNOMED CT Browser](https://termbrowser.nhs.uk/)
- [NHS Synthetic Data Guidance](https://digital.nhs.uk/services/test-data)
