# ImmForm New User Registration (Existing Account) — Discovery Scenario

---

## Scenario Overview

ImmForm is a nationally critical digital service used by UKHSA, NHS England, and local health organisations to manage vaccine ordering and related immunisation operations. The users most directly affected by onboarding delays are people newly taking on an ordering role at an existing ImmForm account — for example, GP practice staff, sexual health services, occupational health providers, COVID-19 and Mpox programme teams, immunoglobulin holding centres, and authorised wholesaler staff. These individuals need reliable, timely access to an existing ImmForm ordering account so they can perform programme-critical activity without avoidable disruption to vaccine supply operations.

Today, onboarding is handled through a manual PDF-and-email process. Applicants complete the ImmForm Account Change/Revalidation Form and submit it to the helpdesk, where staff manually validate details, re-key submitted data into ImmForm, and chase the Authorised Person for approval by email. This creates repeated handoffs, variable lead times, and preventable error loops — for example, invalid account and organisation code pairs, incomplete data, and non-compliant email addresses. Applications routinely take up to five working days to complete.

This registration step sits within a wider, regulated medicinal product supply chain context where traceability, timeliness, and data integrity are not optional — they are compliance requirements under MHRA GDP expectations. The current journey does not produce a consistent, machine-readable record of approval and lifecycle decisions, creating both operational risk and a fragmented audit picture. The end-to-end workflow stages are: applicant starts journey; submits personal and account details; account and organisation code pair is validated; application is submitted with declaration; Authorised Person approves or rejects within a defined time window; approved requests proceed to account creation and applicant notification.

---

## Problem Statement

**How might we enable people who need access to an existing ImmForm account to complete registration quickly and safely, without relying on manual helpdesk processing and ad hoc email-based approval?**

People need a way to submit a new user registration that validates key account details early in the journey, routes approval to the correct Authorised Person with a clear and enforced time boundary, and produces a complete auditable lifecycle record from submission through to activation. This matters because delays in onboarding slow vaccine ordering operations, increase pressure on helpdesk teams, and introduce avoidable risk into a system that underpins national immunisation and medicinal product distribution.

The current cost of the problem is measurable in both operational effort and service risk: repeated manual re-keying of applicant data, fragmented email trails in place of structured event records, variable approval turnaround with no enforcement mechanism, and a baseline activation time of up to five working days. These costs compound across a high volume of registration requests, particularly during programme mobilisation periods.

Success in alpha should be evidenced by measurable outcomes:

- Mean activation time reduced to two working days or fewer for standard NHS site registrations
- Manual helpdesk intervention removed from the standard registration pathway
- Every registration state transition captured in an immutable audit trail that meets UKHSA and MHRA GDP expectations
- Measurable reduction in error-loop rate driven by early account/organisation code validation
- The audit trail is independently retrievable and exportable by the UKHSA QA / WDA Responsible Person without helpdesk or product team involvement, producing records adequate for MHRA GDP inspection

---

## Assumptions

The following assumptions must hold for the problem statement to remain valid. Each should be treated as a hypothesis to be tested rather than a given.

| Assumption | Risk level |
|---|---|
| The ImmForm Organisation API can reliably validate the account number and organisation code pair and return a single Authorised Person for the target account at the point required in the journey | **High — critical path dependency** |
| Real-world approval behaviour conforms to a 72-hour expiry window with a maximum of two resend attempts, without generating unacceptable rejection or abandonment rates | **High — directly affects completion and activation time** |
| Users can provide valid ImmForm account numbers and organisation codes during registration without disproportionate drop-off or support demand | **High — drives avoidable error loops and abandonment** |
| GOV.UK Notify can support the required approval and applicant notification templates at the reliability level required for time-bound approvals | Medium |
| Shared mailbox detection based on agreed rules and heuristics is adequate for alpha to enforce individual accountability and traceability | Medium |
| Session expiry and abandonment behaviour in alpha provides sufficient signal for completion-rate analysis without introducing unacceptable friction | Medium |
| The mocked ImmForm Registration API used in alpha is representative enough to validate the target operational flow and key failure-handling paths | Medium |
| Both the ImmForm Organisation API and ImmForm Registration API will be mocked for alpha using ASP.NET Core minimal API projects within the same solution, implementing the expected request and response contracts including key failure states (API unavailability, no Authorised Person found, Registration API error) | Medium |

**Riskiest assumptions to test first:** Organisation API accuracy and availability under expected demand; real-world approval behaviour against the 72-hour policy; and user ability to provide valid account and organisation identifiers first time.

---

## Out of Scope

The following are explicitly excluded from this alpha to maintain focus. Each represents a separate problem space that may be addressed in subsequent phases.

- Authorised Person registration
- Account revalidation for existing users
- Existing account changes (for example delivery point address, organisation code merger updates, billing or invoice details)
- Creation of new delivery locations or new ImmForm accounts
- Account deactivation or offboarding
- Federated identity integration (for example CIS2, NHSmail, or other SSO) in alpha — architecture should remain adaptable
- Welsh language implementation in alpha — architecture should remain adaptable
- WDA(H) document upload and storage in alpha
- Multi-account registration within a single journey; users requiring access to multiple accounts complete separate registrations
- Updating the Authorised Person record held against an ImmForm account where the named individual has left or is incorrect — this is handled through the existing ImmForm account management process and is triggered by the helpdesk as a fallback when automated approval cannot proceed
