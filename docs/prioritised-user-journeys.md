# Prioritised User Journeys for Workshop Build Order

## Objective

Agree a practical workshop order that tests the riskiest assumptions early, then expands coverage to additional user contexts.

Priority drivers used:

- Risk-first sequencing from discovery notes (API contracts, AP routing, duplicate checks, auditable outcomes)
- Dependency order (submission before approval, approval before account creation, exceptions after happy path)
- Day 1 delivery practicality (build in connected batches)

## Recommended Build Order

### Wave 1: Core end-to-end baseline (build first)

1. `journey-nhs-new-starter-registration.md`: Baseline majority path and core UX flow.

1. `journey-account-validation-failure.md`: Validates organisation/account integration and correction loop early.

1. `journey-field-validation-error-correction.md`: Locks in robust and accessible validation before scaling variants.

1. `journey-duplicate-detection-error.md`: Tests high-risk duplicate detection and anti-enumeration behavior.

1. `journey-authorised-person-approval.md`: Validates the critical approval dependency and AP action capture.

1. `journey-account-creation-api-execution.md`: Completes core automation and verifies account creation reliability.

### Wave 2: Time-bound and exception paths

1. `journey-approval-resend-workflow.md`: Exercises expiry and resend policy limits (72h windows).

1. `journey-registration-rejection-outcome.md`: Ensures clear rejection outcome and recovery guidance.

1. `journey-fallback-case-resolution.md`: Verifies controlled manual intervention for unresolved cases.

1. `journey-session-timeout-abandonment.md`: Confirms secure timeout handling and restart behavior.

### Wave 3: Operational and assurance journeys

1. `journey-admin-qualification-review.md`: Covers admin controls and decision traceability.

1. `journey-audit-evidence-retrieval.md`: Proves inspection-ready evidence retrieval for compliance.

### Wave 4: Persona and context variants (after core is stable)

1. `journey-covid-19-programme-registration.md`
1. `journey-mpox-mobile-registration.md`
1. `journey-gbsm-sexual-health-registration.md`
1. `journey-occupational-health-private-registration.md`
1. `journey-non-nhs-wholesaler-registration.md`
1. `journey-holding-centre-critical-supply-registration.md`

Reason for grouping:

- These journeys primarily validate context-specific rules and channel constraints after the core workflow is proven.
- `journey-holding-centre-critical-supply-registration.md` should be pulled earlier if continuity risk is the top programme priority for this workshop cohort.

## Suggested Day 1 batching

- Batch A: 1 to 3
- Batch B: 4 to 6
- Batch C: 7 to 10
- Batch D: 11 to 12
- Batch E: 13 to 18 (time permitting)

## Agreement notes

This prioritisation is proposed as the default workshop order.

If programme-critical supply continuity is the highest business risk for your team, move `journey-holding-centre-critical-supply-registration.md` into Wave 2 immediately after item 6.
