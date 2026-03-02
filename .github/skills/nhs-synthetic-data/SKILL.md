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

### Python Implementation

```python
def generate_nhs_number(seed_digits: str = "943476591") -> str:
    """Generate a valid synthetic NHS number with correct check digit.

    Args:
        seed_digits: First 9 digits. Default produces the standard
                     synthetic NHS number 943 476 5919.

    Returns:
        A 10-digit NHS number string with valid check digit.

    Raises:
        ValueError: If the seed produces an invalid check digit (remainder 10).
    """
    if len(seed_digits) != 9 or not seed_digits.isdigit():
        raise ValueError("seed_digits must be exactly 9 digits")

    total = sum(int(d) * (11 - i) for i, d in enumerate(seed_digits, 1))
    remainder = total % 11
    check = 11 - remainder

    if check == 11:
        check = 0
    if check == 10:
        raise ValueError(f"Seed {seed_digits} produces invalid check digit")

    return seed_digits + str(check)


def format_nhs_number(nhs_number: str) -> str:
    """Format NHS number in standard 3-3-4 display format.

    Example: '9434765919' → '943 476 5919'
    """
    return f"{nhs_number[:3]} {nhs_number[3:6]} {nhs_number[6:]}"


def validate_nhs_number(nhs_number: str) -> bool:
    """Validate an NHS number using the Modulus 11 algorithm.

    Args:
        nhs_number: 10-digit NHS number string (spaces are stripped).

    Returns:
        True if the check digit is valid.
    """
    cleaned = nhs_number.replace(" ", "")
    if len(cleaned) != 10 or not cleaned.isdigit():
        return False

    total = sum(int(d) * (11 - i) for i, d in enumerate(cleaned[:9], 1))
    remainder = total % 11
    check = 11 - remainder

    if check == 11:
        check = 0
    if check == 10:
        return False

    return int(cleaned[9]) == check
```

### Pre-validated Synthetic NHS Numbers

These numbers pass the Modulus 11 check and are designated for testing:

| NHS Number | Formatted | Use Case |
|---|---|---|
| 9434765919 | 943 476 5919 | Default test patient |
| 9000000009 | 900 000 0009 | Boundary test |
| 9111111124 | 911 111 1124 | Second test patient |
| 9222222228 | 922 222 2228 | Third test patient |
| 9876543210 | 987 654 3210 | Demo presentations |

## Synthetic Patient Data

### Patient Records

Use these synthetic personas consistently across the service:

```python
SYNTHETIC_PATIENTS = [
    {
        "nhs_number": "943 476 5919",
        "given_name": "Sarah",
        "family_name": "Thompson",
        "date_of_birth": "1991-03-15",
        "postcode": "LS1 4AP",
        "gp_surgery": "Kirkstall Health Centre",
        "gender": "female",
    },
    {
        "nhs_number": "911 111 1124",
        "given_name": "James",
        "family_name": "Wilson",
        "date_of_birth": "1954-08-22",
        "postcode": "M1 2WD",
        "gp_surgery": "Manchester Medical Practice",
        "gender": "male",
    },
    {
        "nhs_number": "922 222 2228",
        "given_name": "Priya",
        "family_name": "Patel",
        "date_of_birth": "1985-11-30",
        "postcode": "B1 1BB",
        "gp_surgery": "City Centre Surgery",
        "gender": "female",
    },
    {
        "nhs_number": "987 654 3210",
        "given_name": "David",
        "family_name": "Roberts",
        "date_of_birth": "1968-06-10",
        "postcode": "SW1A 1AA",
        "gp_surgery": "Westminster Health Practice",
        "gender": "male",
    },
    {
        "nhs_number": "900 000 0009",
        "given_name": "Fatima",
        "family_name": "Khan",
        "date_of_birth": "2000-01-25",
        "postcode": "LS2 9JT",
        "gp_surgery": "University Health Service",
        "gender": "female",
    },
]
```

### Synthetic Appointments

```python
from datetime import date, time

SYNTHETIC_APPOINTMENTS = [
    {
        "patient_nhs_number": "943 476 5919",
        "date": date(2026, 3, 15),
        "time": time(9, 30),
        "type": "GP appointment",
        "clinician": "Dr. Ahmed",
        "location": "Kirkstall Health Centre",
        "status": "booked",
    },
    {
        "patient_nhs_number": "943 476 5919",
        "date": date(2026, 3, 22),
        "time": time(14, 0),
        "type": "Blood test",
        "clinician": "Nurse Williams",
        "location": "Kirkstall Health Centre",
        "status": "booked",
    },
    {
        "patient_nhs_number": "911 111 1124",
        "date": date(2026, 3, 10),
        "time": time(10, 15),
        "type": "Annual health check",
        "clinician": "Dr. Chen",
        "location": "Manchester Medical Practice",
        "status": "completed",
    },
]
```

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

## pytest Fixtures

### `conftest.py` fixture for synthetic data

```python
import pytest

@pytest.fixture
def synthetic_patient():
    """A single synthetic patient for unit tests."""
    return {
        "nhs_number": "943 476 5919",
        "given_name": "Sarah",
        "family_name": "Thompson",
        "date_of_birth": "1991-03-15",
    }

@pytest.fixture
def synthetic_patients():
    """All synthetic patients for integration tests."""
    return SYNTHETIC_PATIENTS  # From the list above
```

### TypeScript fixtures for Vitest/Playwright

```typescript
export const syntheticPatient = {
  nhsNumber: '943 476 5919',
  givenName: 'Sarah',
  familyName: 'Thompson',
  dateOfBirth: '1991-03-15',
}

export const syntheticPatients = [
  syntheticPatient,
  {
    nhsNumber: '911 111 1124',
    givenName: 'James',
    familyName: 'Wilson',
    dateOfBirth: '1954-08-22',
  },
  // ... additional patients
]
```

## Rules

- **Never use real NHS numbers** — always generate with the Modulus 11 algorithm or use the pre-validated list
- **Never use real patient names** — use the synthetic personas defined above
- **Never use real postcodes linked to real people** — use well-known institutional postcodes
- **Synthetic data must be clearly labelled** — add comments marking data as synthetic in seed scripts
- **NHS number display** — always format as 3-3-4 with spaces: `943 476 5919`
- **Dates** — use future dates for appointments, past dates for historical records
- **Consistency** — use the same synthetic patients across all tests and demos for a coherent narrative

## References

- [NHS Number Format](https://digital.nhs.uk/services/nhs-number)
- [NHS Number Modulus 11 Check](https://www.datadictionary.nhs.uk/attributes/nhs_number.html)
- [SNOMED CT Browser](https://termbrowser.nhs.uk/)
- [NHS Synthetic Data Guidance](https://digital.nhs.uk/services/test-data)
