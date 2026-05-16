# ImmForm New User Registration — Persona Research Report

**Discovery phase:** New User Registration (Existing Account)
**Source:** Scenario document — `discovery/scenarios/scenario.md`
**Synthesis date:** May 2026
**Primary use:** Alpha sprint planning and user story prioritisation

---

## Overview

This report describes eleven personas derived from the ImmForm New User Registration discovery scenario. They cover the full registration workflow from applicant initiation through to compliance and audit, mapped against the six workflow stages identified in the scenario: journey start, detail submission, account/organisation validation, declaration submission, Authorised Person approval or rejection, and account creation with notification.

Personas are grouped into six workflow stages:

- **Applicant — NHS** covers five programme-specific orderer types: GP practice, sexual health service, occupational health, COVID-19 PCN, and Mpox programme staff
- **Applicant — non-NHS** covers two regulated non-NHS orderer types: authorised wholesaler and immunoglobulin holding centre
- **Authorised Person** covers the approval and rejection stage
- **ImmForm helpdesk — current state** covers the existing manual processor, relevant for transition risk and displacement planning
- **ImmForm helpdesk — digitally-assisted fallback** covers the exception case handler operating within the new digital system for cases the automated journey cannot resolve
- **Compliance and audit** covers the UKHSA QA lead and WDA Responsible Person who validates GDP compliance and provides evidence for MHRA inspection

---

## Workflow Stage: Applicant — NHS

### Priya Chandrasekaran — GP Practice Vaccination Coordinator

**Role:** Vaccination Coordinator, mid-sized urban GP surgery (NHS primary care)
**Experience:** 3 years in administrative and clinical support roles; joined current practice 6 weeks ago to cover a maternity vacancy. No prior ImmForm access. Previous role was at a different practice where a colleague handled all vaccine ordering.
**Location:** East of England
**Department:** Primary Care — GP Practice

**Background**

- Responsible for coordinating seasonal flu, childhood immunisation, and COVID booster delivery at a practice with approximately 9,800 registered patients
- Has been asked to take over vaccine ordering from a colleague who left; no formal handover documentation was provided
- Has a basic understanding of NHS supply chain processes but has never directly used ImmForm
- Relies on the practice manager for account credentials and organisational detail, who is part-time and not always available
- Works in a high-pressure environment where vaccine delivery windows are fixed and access delays have direct programme consequences
- Technically confident with NHS systems (SystmOne, NHS Mail) but unfamiliar with government service registration flows

**Goals**

- Get ordering access to the practice's existing ImmForm account before the next scheduled vaccine delivery cycle — ideally within two working days
- Understand exactly what information she needs before starting so she does not have to stop mid-journey and chase colleagues
- Receive a clear confirmation when her account is active so she can plan ordering activity
- Not have to contact the helpdesk or send emails to anyone — she wants the process to be self-contained

**Wants and needs**

- A checklist or pre-journey prompt that tells her precisely what credentials she needs before she begins (account number, organisation code)
- Inline validation feedback at the point she enters her account and organisation code — she does not want to submit and wait days to be told there was a typo
- A direct, jargon-free explanation of who the Authorised Person is and why they need to approve her access
- Progress notifications by email so she can see where her application is without logging back in
- Reassurance that her individual NHS mail address is acceptable — she is aware some colleagues use shared inboxes

**Pain points**

- Does not know her ImmForm account number and must locate it from a colleague or prior correspondence; this is a likely abandonment point
- Has previously been through NHS system registration processes that took weeks; her expectation of this journey is low and she is prepared to chase manually
- If approval expires or is rejected without clear explanation, she has no obvious route to resolution other than calling the helpdesk
- Shared responsibility for ordering with two other staff members creates ambiguity about whether to register under a personal or shared mailbox — the current PDF process does not make this clear
- Any delay beyond three to four working days risks impacting vaccine ordering operations for the current programme cycle

---

## Workflow Stage: Applicant — Non-NHS

### Marcus Obi — Procurement Compliance Lead, Authorised Wholesaler

**Role:** Procurement Compliance Lead at a licensed vaccine wholesaler holding a Wholesale Dealer's Authorisation (Human) — WDA(H)
**Experience:** 8 years in pharmaceutical supply chain and regulatory compliance; 4 years specifically in licensed wholesaling. Familiar with MHRA GDP requirements and traceability obligations. Has used ImmForm previously at a prior employer but not at the current organisation.
**Location:** West Midlands
**Department:** Procurement and Regulatory Compliance

**Background**

- Responsible for ensuring the organisation's ImmForm account access is maintained for all relevant staff, as part of broader GDP traceability compliance obligations
- Operates under stricter individual accountability requirements than NHS site staff; personal email and verifiable identity are non-negotiable in his context
- Manages a small team of three staff who all need ImmForm access under the same account; plans to register them sequentially once his own access is confirmed
- Acutely aware that shared mailboxes are common in his organisation's finance and procurement function, and expects a clear steer from the system on whether they are acceptable
- Has prior experience of ImmForm's PDF registration process being slow and poorly documented for non-NHS organisations; maintains low expectations of the current journey
- Operates in a regulated context where any gap in ordering access creates an audit finding risk under MHRA inspections

**Goals**

- Establish personally attributable, auditable ImmForm access that satisfies his organisation's internal compliance controls and MHRA GDP traceability requirements
- Understand exactly what the registration record will capture so he can reference it in his organisation's GDP documentation
- Register himself first, then cascade the process to his team members with minimal friction
- Receive written confirmation of registration status that he can retain as a compliance record

**Wants and needs**

- A clear statement at the point of registration about what data is recorded, how long it is retained, and what the audit trail covers — he will reference this in his GDP dossier
- Explicit guidance on shared mailbox policy before he commits to an email address; using a personal address in a small team context creates internal access management overhead he wants to understand upfront
- An outcome notification that is suitable for retention as a compliance record (structured, datestamped, referencing his account and organisation identifiers)
- Confidence that the Authorised Person lookup is accurate for his organisation's account — he has encountered incorrect AP details in legacy systems before
- A predictable, documented lead time he can communicate to his line manager and build into his compliance schedule

**Pain points**

- WDA(H) document upload is explicitly out of scope for alpha — he may be unclear what this means for his registration if the journey does not explain the gap or provide an alternative
- Shared mailbox detection heuristics may incorrectly flag his corporate email domain as a shared inbox, causing unnecessary rejection or friction
- If the Organisation API returns an incorrect or outdated Authorised Person for his account, the approval step will fail silently from his perspective — he has no visibility of who was contacted
- The 72-hour approval window assumes an operationally available AP; in a small wholesaler, the AP may be travelling or absent for compliance-related activity, making the window tight
- Any ambiguity about whether his non-NHS context is supported by the service risks him reverting to the manual PDF process, which he considers the safer but slower option

---

## Workflow Stage: Authorised Person

### Linda Forsythe — Practice Manager and ImmForm Authorised Person

**Role:** Practice Manager at a semi-rural GP surgery; designated Authorised Person for the practice's ImmForm account
**Experience:** 12 years as a practice manager, 6 years as the ImmForm AP. Has approved multiple staff registrations over the years, all through the informal email process.
**Location:** South West England
**Department:** Primary Care — GP Practice

**Background**

- Responsible for day-to-day operational management of a practice of 11 staff, including oversight of vaccine ordering and supply
- Has been the ImmForm AP by default — she set up the original account and has remained the named contact ever since
- Receives approval requests currently as informal emails from the ImmForm helpdesk; these are easy to miss among a high-volume shared inbox
- Has no formal training on what her AP responsibilities mean from a GDP or compliance perspective
- Does not always recognise applicant names at the point of approval — staff turnover in GP practices is high and new starters are not always communicated to her before they begin registration
- Aware that ImmForm access carries ordering authority over NHS vaccine stock; takes approval decisions seriously but does not always have context to make an informed decision quickly

**Goals**

- Receive approval requests in a clear, actionable format that tells her exactly who is applying, what access they are requesting, and what she needs to do
- Complete the approval action in under two minutes without needing to log into any system
- Have a clear record of what she approved and when, in case it is ever queried by UKHSA or an auditor
- Not receive duplicate or chaser requests for approvals she has already actioned
- Be notified promptly if a request expires so she is not left with open approvals she cannot close

**Wants and needs**

- A single-action approval mechanism accessible directly from the notification email — no login, no portal navigation
- Clear identification of the applicant: name, declared role, and the account they are requesting access to
- A simple, accessible rejection path with a free-text reason field so she can explain her decision if needed
- A confirmation email after she approves or rejects, for her own records
- A way to flag that an applicant's name is unrecognised without fully rejecting — she sometimes needs to verify with her GP principal before approving

**Pain points**

- Currently receives ImmForm approval requests as unstructured emails with no clear call to action — she has missed or delayed requests because they were not distinguishable from general helpdesk correspondence
- The 72-hour approval window is tight given her workload; she is frequently in clinical support meetings, covering reception, and managing HR matters simultaneously
- Has no mechanism today to delegate approval authority during annual leave or absence — if she is the sole AP and is unavailable, applications stall
- Receives no notification when an approval expires; she only discovers an applicant is still waiting if they contact her directly or via the helpdesk
- Does not know what the approval is legally or operationally committing her to; this creates hesitation that delays decisions

---

## Workflow Stage: ImmForm Helpdesk Operative

### David Acheampong — ImmForm Helpdesk Operative

**Role:** Helpdesk Operative, ImmForm Technical Services, UKHSA
**Experience:** 3 years on the ImmForm helpdesk; previously worked in NHS 111 call handling. Handles registration, revalidation, account changes, and general user support queries.
**Location:** UKHSA (London / remote)
**Department:** ImmForm Technical Services — Commercial, Vaccines and Countermeasures Delivery

**Background**

- Processes the majority of new user registration applications end-to-end: receives PDF forms by email, validates submitted data against ImmForm records, re-keys details into the system, and chases Authorised Persons when approvals are outstanding
- Handles approximately 20 to 30 registration requests per week, with peaks during programme mobilisation periods (autumn flu, COVID booster rollout)
- Has developed informal validation shortcuts over time — for example, spotting common organisation code errors by sight and recognising which Authorised Persons are reliably responsive
- Knows the failure modes of the current process better than anyone: the most common errors are invalid account/organisation code pairs, incomplete forms, and non-compliant email addresses
- Acutely aware that his role as it currently exists is partly a compensating control for a broken process, not a value-adding service function
- Has a reasonable level of digital literacy and uses ImmForm's admin interface daily; would engage positively with a transition to a self-service model if given adequate notice and retraining support

**Goals**

- Reduce the volume of preventable manual processing — applications that fail due to fixable errors before they reach him
- Have a clear, documented escalation path for edge cases (for example, unresponsive APs, disputed account ownership, non-NHS organisations with missing credentials)
- Understand how his role changes under the new process before it goes live — particularly what residual cases will still require manual intervention
- Maintain a reliable audit trail for every application he touches, both to protect himself and to support UKHSA compliance reporting
- Not be left as the silent fallback for a system that fails without warning

**Wants and needs**

- A clear definition of which registration cases will remain manual after alpha — he needs to know his operational boundary
- Access to a structured log of system-generated registration events so he can intervene accurately when escalations occur, without reconstructing history from emails
- Advance communication about the new process in sufficient time to update his team's operating procedures and helpdesk scripts
- A feedback mechanism so he can report patterns of failure (for example, specific account types or organisation types that repeatedly error) back to the product team
- Clarity on what constitutes a valid escalation trigger from the new automated journey versus noise that the system should handle itself

**Pain points**

- Currently holds tacit knowledge about common error patterns that exists nowhere in documented form — when he is absent, quality of processing drops noticeably
- Has no visibility of application status once he has passed it to the Authorised Person; chasing is the only tool available to him
- Re-keying submitted data is error-prone; he has made transcription errors himself and has no automated check to catch them
- Receives no structured data from applicants — form fields are free text, leading to inconsistent formats that require manual interpretation
- Is aware that his current processing role will be significantly reduced under the new journey but has received no formal communication about what this means for his post

---

## Workflow Stage: Applicant — NHS (Programme-Specific)

### Keisha Mensah — Sexual Health Service Administrator

**Role:** Service Administrator, local authority-commissioned sexual health clinic (GUM service)
**Experience:** 5 years in sexual health services administration; 2 years handling vaccine stock administration including HPV catch-up and Hepatitis B. New to ImmForm ordering — her predecessor left without a handover.
**Location:** London Borough (local authority commissioned)
**Department:** Sexual Health Services — commissioned by local authority, delivered by NHS trust

**Background**

- Works in a high-volume urban GUM clinic that delivers targeted vaccination programmes including HPV, Hepatitis B, and now Mpox for eligible patients
- Her service is commissioned by the local authority but delivered by an NHS trust, creating ambiguity about which organisational entity owns the ImmForm account — she is unsure whether to register under the trust or the commissioning body
- Has inherited account administration responsibilities from a predecessor with no documentation; the ImmForm account number exists somewhere in a shared drive but she has not yet located it
- Operates under a service that sees high staff turnover in administrative roles; ImmForm access gaps are a recurring problem when staff leave
- Deals regularly with sensitive patient data and is alert to data governance requirements; she will want to understand what ImmForm stores about her before completing registration
- Is technically capable — uses SystmOne, Cerner, and local authority case management systems daily

**Goals**

- Gain access to the clinic's ImmForm account quickly so vaccine ordering can resume without disruption to the HPV and Hepatitis B programmes
- Clarify once and for all which organisation code applies to her service — trust or local authority — without having to call the helpdesk
- Establish a process she can document and pass on to the next person in her role, because turnover in her team is a known problem
- Complete registration without needing to involve her line manager, who is a clinical lead with no time for administrative queries

**Wants and needs**

- Clear guidance on how to identify the correct account number and organisation code for a commissioned service with dual-entity ownership
- A registration process that produces a record she can save and hand over as part of an administrative handover pack
- Inline validation that confirms her organisation code is correct before she submits, so she does not wait days only to be told there is a mismatch
- Confidence that her NHS mail address is acceptable — some colleagues at the trust use shared clinic mailboxes and she is unsure which applies to her

**Pain points**

- The dual-entity commissioning structure (local authority commissioner, NHS trust provider) is a known source of account and organisation code confusion that the current PDF process does not address
- Locating the account number from a predecessor's undocumented setup is a likely abandonment point
- Any access gap that extends beyond a week has direct programme consequences — HPV appointments cannot be supported without stock
- If the Authorised Person held against the account is no longer in post, the approval routing will fail without explanation — staff turnover at her level of organisation makes this a real risk

---

### Colin Rafferty — NHS Trust Occupational Health Coordinator

**Role:** Vaccination Coordinator, NHS Trust Occupational Health Department
**Experience:** 7 years in occupational health nursing; 3 years coordinating the trust's annual staff flu vaccination programme. Has used ImmForm before but under a previous job title and email address — his existing access was deactivated when he changed roles within the same trust.
**Location:** North West England
**Department:** Occupational Health — NHS acute trust

**Background**

- Responsible for planning and executing the trust's annual staff flu vaccination programme across multiple sites, involving approximately 4,500 eligible staff
- His previous ImmForm access was deactivated during a role change within the same trust six months ago; he now needs to re-register under his current role and email address
- Understands ImmForm well from a user perspective but has never completed the registration process himself — his access was historically set up by his predecessor
- Works to a hard seasonal deadline: flu vaccine ordering must be in place by a specific date each year or the programme is at risk; delays in access have programme-level consequences
- Operates within an NHS trust procurement framework that requires all ordering activity to be personally attributable and auditable
- Confident NHS system user; familiar with ESR, Allocate, and trust procurement platforms

**Goals**

- Regain ordering access under his current role credentials before the flu programme ordering window opens
- Understand why his previous access was deactivated and whether he needs to do anything differently this time to prevent recurrence
- Complete registration without involving his line manager or the trust's IT helpdesk — he wants a self-contained process
- Have a clear confirmation of activation he can reference in his programme planning documentation

**Wants and needs**

- A pre-journey explanation of what happens to access when a user changes role or email address within the same organisation — his situation is common and he expects guidance
- Reassurance that his trust's organisation code has not changed — trust mergers and reconfigurations have made this unreliable in the past
- A predictable completion timeline he can build into his flu programme project plan
- A single point of contact or escalation route if something goes wrong, rather than a generic helpdesk queue

**Pain points**

- Is registering under a different email address and role title from his previous access — he is uncertain whether this counts as a new registration or an account change, and the current process does not distinguish
- The Authorised Person for his trust's ImmForm account may not be known to him; internal governance for ImmForm AP designation is not always well maintained in large trusts
- The seasonal deadline creates real urgency — a five-working-day registration lead time is unacceptable in the final weeks before the ordering window
- Has no way to check whether his old access is fully deactivated or merely dormant, which creates ambiguity about his account history

---

### Amir Siddiqui — PCN COVID-19 Vaccination Programme Coordinator

**Role:** COVID-19 Vaccination Coordinator, Primary Care Network
**Experience:** 4 years in primary care administration; joined the PCN COVID vaccination programme during the initial rollout and has continued in a coordination role through subsequent booster campaigns. First time completing ImmForm registration personally.
**Location:** Yorkshire and Humber
**Department:** Primary Care Network — COVID-19 vaccination programme

**Background**

- Coordinates COVID-19 vaccine ordering and stock management across five GP practices within the PCN, acting as the central point of contact for programme delivery
- Has operated informally under a shared account used by the PCN lead practice; the programme is now formalising access controls and he needs individual, attributable access
- Familiar with the rhythm of national COVID vaccination programmes — ordering cycles, stock management, and UKHSA reporting requirements — but has no prior direct ImmForm registration experience
- Works across multiple GP systems (EMIS, SystmOne) and is comfortable with digital tools, but has found government service registration processes inconsistent in the past
- Operates under ICS oversight with reporting obligations to NHS England; ordering accuracy and traceability are taken seriously at programme level
- His PCN does not have a dedicated IT function; he resolves most digital issues himself or escalates to the ICS digital team

**Goals**

- Establish individually attributable ImmForm access that satisfies the PCN's new access governance requirements and supports programme audit obligations
- Move away from a shared access model without disrupting current ordering operations during the transition
- Complete registration quickly enough to maintain continuity — the current shared access arrangement is being phased out on a fixed date
- Understand what happens to the existing shared account once individual accounts are in place, so he can advise the PCN lead practice

**Wants and needs**

- Clear guidance on whether registering under an existing PCN account number is the correct approach, or whether a new account structure is needed
- An explanation of the shared mailbox policy at the point of email entry — he currently uses a shared PCN coordination inbox and will need to switch to a personal address if required
- A registration record that is suitable for inclusion in the PCN's programme governance documentation
- A way to indicate during registration that he is transitioning from a shared access arrangement, so the helpdesk has context if an issue arises

**Pain points**

- The shift from shared to individual access means his Authorised Person may not be expecting a registration request from him — if the AP is confused by the request, rejection is likely
- His PCN coordination inbox is a shared mailbox; if shared mailbox detection flags this, he will need to identify a personal address before he can proceed, adding friction he was not anticipating
- If the existing shared account is deactivated before his individual access is confirmed, there will be a gap in ordering capability with direct programme consequences
- The PCN governance structure means his Authorised Person may sit at ICS or practice level depending on how the account is structured — he is not certain who holds AP authority for his account

---

### Donna Eze — Specialist Sexual Health Nurse, Mpox Programme

**Role:** Specialist Sexual Health Nurse, Mpox Vaccination Programme
**Experience:** 9 years as a sexual health nurse; 2 years delivering the Mpox vaccination programme to high-risk eligible patients. Leads a small outreach vaccination team at a UKHSA-commissioned specialist clinic.
**Location:** London
**Department:** Sexual Health — UKHSA-commissioned Mpox programme, delivered via specialist GUM clinic

**Background**

- Leads clinical delivery of the Mpox vaccination programme at her clinic, including patient eligibility assessment, vaccine administration, and stock management
- Has been managing Mpox vaccine ordering through a colleague who holds ImmForm access; that colleague is leaving, and Donna is taking over full ordering responsibility
- Operates under a UKHSA-commissioned programme with specific stock allocation rules and ordering constraints — her ordering activity is closely monitored at programme level
- Works with a small, highly specialist team where individual accountability for ordering decisions is essential; shared access arrangements are not appropriate for her clinical governance context
- Has limited time for administrative processes — her caseload is high and programme eligibility criteria are complex; any registration process that requires repeated engagement will be deprioritised
- Familiar with clinical information systems but less comfortable with government digital service registration flows; she will complete the process on a mobile device if needed

**Goals**

- Gain personal ImmForm ordering access before her colleague leaves, with minimal disruption to the Mpox programme stock cycle
- Ensure the Authorised Person for the account is correctly identified — she suspects the current AP may be outdated following organisational changes at the clinic
- Complete registration in a single sitting without needing to locate information she does not have readily to hand
- Receive confirmation quickly enough to place her first independent order before the next programme ordering window

**Wants and needs**

- A mobile-friendly registration journey — she is unlikely to complete this at a desktop and will use her NHS-issued iPhone
- A pre-journey prompt that lists exactly what account credentials she needs, so she can gather them from her departing colleague in a single conversation
- A clear explanation of what happens if the Authorised Person identified by the system is wrong or unreachable — she needs a fallback route
- Programme-specific context that confirms the Mpox programme account type is supported by the registration journey

**Pain points**

- If the AP lookup returns an outdated contact, her approval request will be sent to someone who no longer holds the role — she will have no visibility of this and will assume her application is in progress
- The 72-hour approval window may be insufficient if her clinic's AP is engaged in clinical activity and not monitoring administrative email — clinical staff in GUM services routinely deprioritise non-patient email
- Locating her ImmForm account number requires her departing colleague's involvement; if that conversation does not happen before the colleague leaves, she has no other route to retrieve it
- Any gap in her ordering access directly affects the Mpox vaccination programme, which operates under UKHSA programme oversight with no tolerance for unexplained stock ordering delays

---

### Sanjay Patel — Specialist Hospital Pharmacist, Immunoglobulin Holding Centre

**Role:** Lead Pharmacist, Immunoglobulin Holding Centre, NHS specialist hospital
**Experience:** 14 years as a hospital pharmacist; 6 years as lead pharmacist at a designated immunoglobulin holding centre. Holds named responsibility for stock integrity, GDP compliance, and ordering accuracy for a highly regulated product.
**Location:** East Midlands
**Department:** Pharmacy — NHS specialist hospital, designated immunoglobulin holding centre

**Background**

- Responsible for ordering, receiving, and managing immunoglobulin stock at a designated national holding centre; one of a small number of licensed centres nationally
- Operates under stringent MHRA GDP requirements with full cold-chain accountability; every ordering action must be attributable to a named, authorised individual
- Has previously held ImmForm access but this was deactivated when the pharmacy team underwent a system access audit; he is re-registering under a new individual access arrangement following the audit's recommendations
- His centre's ImmForm account is distinct from standard NHS vaccine ordering accounts — the product type, ordering constraints, and audit requirements differ materially from primary care or programme ordering contexts
- Is the most technically experienced user of any of the applicant personas; he understands supply chain data flows, regulatory traceability, and system audit trail requirements at a professional level
- Will scrutinise the registration process for compliance adequacy before he commits to it; if the audit trail and data retention documentation are insufficient, he will revert to the manual process or escalate to the ImmForm service owner

**Goals**

- Re-establish personally attributable ImmForm access that satisfies the recommendations of his pharmacy team's recent system access audit
- Confirm that the registration record produced by the new journey meets MHRA GDP documentation standards for named-person ordering authority
- Understand precisely what the system captures at each stage — submission, validation, approval, and activation — so he can assess compliance adequacy independently
- Complete registration in a way that creates no gap in ordering capability; continuity of immunoglobulin supply is a patient safety matter for his centre

**Wants and needs**

- A detailed, retrievable record of the registration event — including timestamp, account identifiers, AP identity, and approval decision — that he can incorporate into his GDP documentation
- Explicit confirmation that the immunoglobulin holding centre account type is correctly handled by the registration journey, given its distinct ordering and audit profile
- A clear statement of data retention policy for registration records — he will need to demonstrate this to an MHRA inspector if asked
- An escalation route to the ImmForm service owner or UKHSA contact if the automated journey cannot accommodate his centre's specific compliance requirements

**Pain points**

- If the registration journey does not produce an audit-grade record, he cannot use it as a GDP compliance document — this is a hard requirement, not a preference
- The Organisation API must correctly identify his centre's Authorised Person; holding centres are managed differently from standard NHS accounts and AP data may be less reliably maintained
- Any ordering gap at a holding centre has immediate patient safety implications — immunoglobulin is a critical, difficult-to-substitute product used in immunodeficiency and neurological conditions
- His technical literacy means he will notice if the journey cuts corners on audit trail completeness; a process that feels adequate for primary care may not meet the standard his context demands
- He is likely to test the system's failure handling deliberately — for example by submitting an incorrect organisation code — to assess how gracefully it responds before completing a real registration

---

## Workflow Stage: Helpdesk — Digitally-Assisted Fallback

### Fatima Osei — ImmForm Helpdesk Case Handler (Fallback Pathway)

**Role:** Helpdesk Case Handler, ImmForm Technical Services, UKHSA — operating within the new digital registration system to manage cases the automated journey cannot resolve
**Experience:** 5 years on the ImmForm helpdesk; previously handled end-to-end manual registration processing alongside David. Under the new operating model, her role shifts from routine processing to exception handling and case resolution within the digital system.
**Location:** UKHSA (London / remote)
**Department:** ImmForm Technical Services — Commercial, Vaccines and Countermeasures Delivery

**Background**

- Her role under the new model is narrower than David's current one but requires higher judgement: she works cases that the automated journey has flagged or stalled, using an admin case view within the system rather than email and re-keying
- Handles defined fallback triggers: Authorised Person unresponsive after two resend attempts and 72-hour expiry; Organisation API returning no valid AP; account type or organisation code combinations the automated validation cannot resolve; applicants who have contacted the helpdesk directly because their journey stalled without explanation
- Has access to the full registration event log for any case she is handling — submission timestamp, validation outcome, AP notification attempts, resend events, and current state — so she can intervene accurately without reconstructing history from email threads
- Has defined decision authority for specific case types: she can extend an approval window, reassign an AP contact, or close a case as unresolvable with a documented reason; she cannot approve registrations herself
- Works to a case SLA aligned to the overall registration target of two working days mean activation time; her caseload is the primary risk to that metric
- Maintains a feedback log of recurring case patterns that she shares with the ImmForm product team on a sprint cycle

**Goals**

- Resolve stalled cases within defined SLA without reverting to unstructured email or manual re-keying
- Have complete, accurate visibility of every event in a case before she intervenes, so her actions are informed and auditable
- Work within clearly defined decision authority — she needs to know exactly what she can action herself and what requires escalation to the service owner or UKHSA programme team
- Identify and report recurring failure patterns (for example, specific account types or organisations that consistently generate fallback cases) so the product team can address root causes
- Maintain a clean audit trail for every case she touches, including her own intervention actions and rationale

**Wants and needs**

- An admin case view that shows the complete registration event log in chronological order, with current state clearly indicated, accessible without navigating multiple screens
- A set of defined, system-supported intervention actions — extend approval window, update AP contact, close case with reason — rather than working around the system via email
- Clear escalation criteria documented in her operating procedure: which case types she resolves, which go to the service owner, and which require programme team involvement
- A case assignment and tracking mechanism so she knows which cases are hers, their age, and their SLA position at a glance
- A structured way to log her intervention rationale against each case, visible to auditors and the QA lead without requiring her to maintain a separate record

**Pain points**

- If the admin case view does not show the complete event history, she will be working blind — the quality of her intervention depends entirely on the accuracy and completeness of the system's audit trail
- Without defined decision authority, she will either over-escalate (creating bottlenecks at service owner level) or under-escalate (making decisions outside her remit, creating compliance risk)
- If the system has no mechanism to update an AP contact on a stalled case, her only option is manual outreach — which recreates the problem the new journey was designed to eliminate
- Cases involving non-NHS organisations (wholesalers, holding centres, commissioned services) are likely to be disproportionately complex; she needs programme-specific guidance for these account types
- Without a feedback mechanism to the product team, recurring case patterns will be invisible at a system level — she will handle them individually rather than seeing them addressed at root cause

---

## Workflow Stage: Compliance and Audit

### Rachel Thornton — UKHSA Quality Assurance Lead and WDA Responsible Person

**Role:** Quality Assurance Lead and Wholesale Dealer's Authorisation Responsible Person (WDA RP), UKHSA
**Experience:** 18 years in pharmaceutical quality assurance and regulatory compliance; 6 years at UKHSA in a QA leadership role. Holds named RP accountability for UKHSA's WDA(H), making her personally responsible for demonstrating GDP compliance to the MHRA.
**Location:** UKHSA (Colindale / remote)
**Department:** Quality Assurance — Commercial, Vaccines and Countermeasures Delivery

**Background**

- Holds the most significant compliance accountability of any user in this system: as WDA RP, she is the named individual who answers to the MHRA if UKHSA's medicinal product handling or distribution activities are found to be non-compliant
- Does not interact with the registration journey directly but is the downstream validator of whether it produces records that meet GDP Chapter 3 and MHRA WDA requirements — specifically, named-individual attribution, auditable state transitions, and retention of approval decisions
- Will be called upon to provide evidence of ImmForm registration records during MHRA inspections, self-inspections, and internal UKHSA quality audits — she needs to retrieve, export, and present specific records on demand without depending on the helpdesk or product team
- Has oversight of ImmForm's registration activity as part of her broader GDP compliance monitoring; she reviews whether the system is generating complete, immutable records and flags deficiencies to the service owner
- Is familiar with quality management systems and audit log interfaces from pharmaceutical industry experience; she will assess ImmForm's audit functionality against a professional standard, not a general digital service standard
- Collaborates with the ImmForm service owner on compliance documentation, contributing to the service's MHRA-facing quality dossier

**Goals**

- Confirm that every registration state transition produces an immutable, timestamped, named-individual record that satisfies GDP Chapter 3 documentation requirements
- Be able to retrieve a complete registration lifecycle record — from submission through to activation or rejection — for any specific applicant on demand, without helpdesk assistance
- Produce audit-ready evidence packages for MHRA inspections without manual collation from disparate sources
- Identify and escalate any gaps in audit trail completeness before they become inspection findings
- Maintain oversight of the registration system's compliance posture on an ongoing basis, not only when an inspection is imminent

**Wants and needs**

- A searchable audit log interface that allows retrieval by applicant name, account number, organisation code, date range, and registration state — she needs to find specific records quickly under inspection conditions
- Immutable event records with clearly attributed actor identity at every state transition: applicant submission, system validation outcome, AP notification, AP decision (with timestamp and decision identity), helpdesk intervention (with operator identity and action taken), and account activation
- An export function that produces a structured, self-contained record suitable for attachment to a GDP quality dossier or MHRA inspection pack
- A documented data retention policy for registration records, accessible without requiring a request to the product team, that she can reference in compliance documentation
- A mechanism to flag audit trail gaps or anomalies directly to the service owner without raising a general helpdesk ticket

**Pain points**

- If the audit log is incomplete — for example if AP decisions are recorded without the AP's identity, or if helpdesk interventions are not captured — the record fails GDP requirements regardless of how well the registration journey itself works
- If records cannot be exported in a structured format, she will have to present screen captures or manual transcriptions to MHRA inspectors — this is not an acceptable evidence standard
- Any gap between what the system records and what the GDP dossier claims the system records creates an inspection finding risk; she needs the actual system behaviour to match the documented compliance posture
- The 72-hour approval window and resend policy need to be reflected accurately in the audit log — an inspector reviewing a stalled case needs to see exactly when notifications were sent, when they expired, and what action followed
- If she cannot access the audit log independently, every inspection or audit creates a dependency on the product team to retrieve records — this is operationally unacceptable at RP level and creates a single point of failure for compliance evidence
