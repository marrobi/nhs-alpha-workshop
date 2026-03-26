---
name: dcb0129-hazard-log
description: 'Use when generating a clinical safety hazard log, clinical risk matrix, or DCB0129 Clinical Safety Case Report for an NHS digital health service.'
---

# DCB0129 Hazard Log — Clinical Safety

This skill generates clinical safety documentation following the [DCB0129 standard](https://digital.nhs.uk/data-and-information/information-standards/information-standards-and-data-collections-including-extractions/publications-and-notifications/standards-and-collections/dcb0129-clinical-risk-management-its-application-in-the-manufacture-of-health-it-systems) for NHS digital health services, using the SIREN methodology for hazard identification.

## When to Use

- Creating a new hazard log for an NHS Alpha service
- Adding hazards identified during development or testing
- Producing a Clinical Safety Case Report for assessment
- Reviewing code changes that affect clinical data or patient-facing features

## Hazard Identification — SIREN Categories

Identify hazards across these categories:

| Category | Examples |
|---|---|
| **S**ystem | System unavailability, data loss, incorrect data display |
| **I**nformation | Wrong patient data shown, stale clinical data, missing alerts |
| **R**isk | Misidentification of patients, delayed referrals, incorrect dosage display |
| **E**rror | User input errors not caught, misleading UI, ambiguous clinical terms |
| **N**ew | Hazards introduced by new technology, integration points, workarounds |

## Clinical Risk Matrix

| Severity ↓ / Likelihood → | Very Low | Low | Medium | High | Very High |
|---|---|---|---|---|---|
| **Catastrophic** (death) | 3 | 4 | 5 | 5 | 5 |
| **Major** (permanent harm) | 2 | 3 | 4 | 5 | 5 |
| **Considerable** (semi-permanent) | 2 | 3 | 3 | 4 | 5 |
| **Significant** (short-term harm) | 1 | 2 | 3 | 3 | 4 |
| **Minor** (inconvenience) | 1 | 1 | 2 | 2 | 3 |

Risk levels: **1** = Acceptable, **2** = Acceptable with monitoring, **3** = Undesirable — needs mitigation, **4** = Mandatory mitigation, **5** = Unacceptable — must eliminate

## Output

The agent must **create new files**:
- Hazard log: `docs/clinical-safety/hazard-log.md`
- Clinical Safety Case Report: `docs/clinical-safety/safety-case-report.md`

Use the structure described above as guidance. Do **not** edit this skill file — it is a reference, not a template to fill in.
