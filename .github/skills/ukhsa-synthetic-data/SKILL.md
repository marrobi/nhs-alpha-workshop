---
name: ukhsa-synthetic-data
description: 'Use when generating synthetic UKHSA test data including NHS numbers, person records, SNOMED codes, and seed scripts for .NET 10 development and demos.'
---

# UKHSA Synthetic Data — Test Data Generation

This skill generates realistic but entirely synthetic test data for UKHSA digital services. Real personal or health data must never be used in development, testing, or demos — this is a UK GDPR and UKHSA policy requirement.

## When to Use

- Creating seed data for local development databases (EF Core migrations / `DbContext.SeedAsync`)
- Writing xUnit / `WebApplicationFactory` test fixtures
- Building demo datasets for stakeholder presentations
- Generating realistic form submissions for Playwright E2E tests
- Populating prototype screens with example data

## NHS Number Generation

UKHSA services that use NHS Numbers follow [ISB 0149](https://digital.nhs.uk/services/nhs-number) — 10 digits with a Modulus 11 check digit.

Algorithm:
1. Multiply each of the first 9 digits by `(11 - position)`, where position is 1-indexed
2. Sum the products
3. Remainder = sum mod 11
4. Check digit = `11 - remainder`
5. If check digit is 11, use 0. If check digit is 10, the number is invalid — regenerate

Implement `Generate`, `Format` (3-3-4 with spaces), and `Validate` methods in C# (e.g. `Ukhsa.SyntheticData.NhsNumber`).

### Pre-validated Synthetic NHS Numbers

These numbers pass the Modulus 11 check and are designated for testing:

| NHS Number | Formatted | Use Case |
|---|---|---|
| 9434765919 | 943 476 5919 | Default test person |
| 9000000009 | 900 000 0009 | Boundary test |
| 9111111124 | 911 111 1124 | Second test person |
| 9222222228 | 922 222 2228 | Third test person |
| 9876543210 | 987 654 3210 | Demo presentations |

## Synthetic Person Records

Use these synthetic personas consistently across the service:

| NHS Number | Given Name | Family Name | DOB | Postcode | Locality | Sex |
|---|---|---|---|---|---|---|
| 943 476 5919 | Sarah | Thompson | 1991-03-15 | LS1 4AP | Leeds | female |
| 911 111 1124 | James | Wilson | 1954-08-22 | M1 2WD | Manchester | male |
| 922 222 2228 | Priya | Patel | 1985-11-30 | B1 1BB | Birmingham | female |
| 987 654 3210 | David | Roberts | 1968-06-10 | SW1A 1AA | London | male |
| 900 000 0009 | Fatima | Khan | 2000-01-25 | LS2 9JT | Leeds | female |

## Synthetic Domain Events

Use realistic-looking events for whichever UKHSA service you are building (notifications, lab results, vaccinations, surveillance reports). Examples for a vaccination service:

| NHS Number | Date | Vaccine | Batch | Site | Status |
|---|---|---|---|---|---|
| 943 476 5919 | 2026-03-15 | Influenza | FLU-2026-001 | Kirkstall Health Centre | administered |
| 911 111 1124 | 2026-03-10 | COVID-19 | COV-2026-005 | Manchester Vaccination Centre | administered |

For surveillance events use synthetic ONS-style geography (LSOA codes, local authority) — never real linked addresses.

## Synthetic Clinical Codes (SNOMED CT)

Use real SNOMED CT concept IDs with synthetic person associations:

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

## EF Core Seeding

Provide a seed routine in `src/Infrastructure/Seed/SyntheticSeed.cs` that:

- Inserts the five synthetic persons above (idempotent — check before insert)
- Inserts domain events for at least 2 of the 5 persons
- Is gated behind `Environment != "Production"` — never seed prod

Example:
```csharp
public static async Task SeedAsync(AppDbContext db, IHostEnvironment env)
{
    if (env.IsProduction()) return;
    // Synthetic data — see ukhsa-synthetic-data skill
    if (!await db.People.AnyAsync()) { /* insert from list */ }
}
```

## Test Fixtures

Provide `SyntheticPersons.Default` (single record) and `SyntheticPersons.All` (collection) in a test fixtures project, plus a `Faker` instance configured to use the synthetic NHS number generator for any additional records.

## Rules

- **Never use real NHS numbers** — always generate with the Modulus 11 algorithm or use the pre-validated list
- **Never use real names** — use the synthetic personas defined above
- **Never use real postcodes linked to real people** — use well-known institutional postcodes
- **Label synthetic data clearly** — add a `// Synthetic data — ukhsa-synthetic-data` comment in seed files
- **NHS number display** — always format as 3-3-4 with spaces: `943 476 5919`
- **Dates** — future dates for forthcoming events, past dates for historical records
- **Consistency** — use the same synthetic personas across all tests and demos for a coherent narrative

## References

- [NHS Number Format (ISB 0149)](https://digital.nhs.uk/services/nhs-number)
- [NHS Number Modulus 11 Check](https://www.datadictionary.nhs.uk/attributes/nhs_number.html)
- [SNOMED CT Browser](https://termbrowser.nhs.uk/)
- [NHS Synthetic Data Guidance](https://digital.nhs.uk/services/test-data)
- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
