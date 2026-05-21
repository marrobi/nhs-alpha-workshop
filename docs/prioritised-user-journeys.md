# Prioritised User Journey Build Order

**Principle:** Build the highest-risk, highest-dependency path first. Each wave must be stable before the next begins. Persona and context variants add no new architecture — they test the core under different conditions.

---

## Wave 1 — Core happy path (build first)

| # | Journey | Why first |
|---|---|---|
| 1 | `journey-nhs-new-starter-registration` | The baseline — every other journey is a variant of or depends on this. Build nothing else until this works end-to-end. |
| 2 | `journey-account-validation-failure` | The Org API integration is the **#1 riskiest assumption**. Validate it fails gracefully before building anything that depends on it succeeding. |
| 3 | `journey-field-validation-error-correction` | GDS-compliant error handling affects every form step. Get the validation pattern right once and reuse it. Don't scale variants on top of fragile validation. |
| 4 | `journey-duplicate-detection-error` | Two pre-submit checks with **anti-enumeration controls** — a security-critical requirement. Must be in before any real email data can be tested. |
| 5 | `journey-authorised-person-approval` | Every registration terminates here. Nothing can be approved, rejected, or lapsed without this being built. GOV.UK Notify time-bound link pattern must be proven early. |
| 6 | `journey-account-creation-api-execution` | Closes the loop. Polly retry policy, EVT-17/18/19, and the activation email. The core automation is not complete without this. |

---

## Wave 2 — Exception and time-bound paths

| # | Journey | Why here |
|---|---|---|
| 7 | `journey-approval-resend-workflow` | The 72h expiry and 2-resend cap is a **compliance requirement**, not a nice-to-have. Approval link expiry is the #2 riskiest assumption. |
| 8 | `journey-registration-rejection-outcome` | AP rejections are inevitable in production. The mandatory reason field and rejection email close an important unhappy path. |
| 9 | `journey-fallback-case-resolution` | When AP is unresponsive or no AP record exists, the helpdesk must act within role constraints. This is the safety net — unresolvable without it. |
| 10 | `journey-session-timeout-abandonment` | Server-side session purge on timeout is a **security requirement** (NFR). Needed before any user testing with real data. |

---

## Wave 3 — Operational and compliance journeys

| # | Journey | Why here |
|---|---|---|
| 11 | `journey-admin-qualification-review` | Admin controls (EVT-13–16) and manual overrides require the audit trail to be stable first. Depends on Wave 1 event infrastructure. |
| 12 | `journey-audit-evidence-retrieval` | The QA/WDA RP read-only interface and CSV export (FR-23, FR-24) are compliance-mandated but read-only — they can only be built once there is data to retrieve. |

---

## Wave 4 — Persona and context variants

These share the same architecture as Wave 1. They test the existing flow under different conditions — no new screens are built; risk is in organisational edge cases and UX under pressure.

| # | Journey | Key added risk to test |
|---|---|---|
| 13 | `journey-covid-19-programme-registration` | Individual email enforcement; ordering deadline pressure |
| 14 | `journey-mpox-mobile-registration` | Mobile layout; partial-save resume where account number unavailable |
| 15 | `journey-gbsm-sexual-health-registration` | Dual-org account ownership ambiguity |
| 16 | `journey-occupational-health-private-registration` | Private account number scoping correctness |
| 17 | `journey-non-nhs-wholesaler-registration` | Shared mailbox detection; structured audit for GDP inspection |
| 18 | `journey-holding-centre-critical-supply-registration` | Patient safety risk — high-consequence AP routing failure. **Pull to Wave 2 if supply continuity is the top business risk for this cohort.** |

---

## Why this order

1. **Dependency order is hard** — you cannot test AP approval (5) without a submitted registration (1), and you cannot test account creation (6) without a successful approval (5). Waves are topologically sorted.
2. **Riskiest assumptions are front-loaded** — the Org API (journey 2) and AP approval behaviour (journeys 5, 7) are the two assumptions most likely to surface integration or behavioural surprises. Failing fast here saves rework.
3. **Security-critical paths are in Wave 1** — anti-enumeration (4), session expiry (10), and CSRF are requirements that must underpin everything built after them, not retrofitted.
4. **Persona variants last** — they validate the core under realistic pressure but introduce no new technical surface area. Building them first would be waste if the core journey changes.
