# ImmForm User Onboarding — User Personas

**Service:** ImmForm new-user registration (UKHSA)
**Author / requester:** Maya Hoang, Solution Engineer, Cloud & AI PS
**Compiled:** 13 May 2026
**Framework anchors:** [GDS Service Standard, Point 1 "Understand users and their needs"](https://www.gov.uk/service-manual/service-standard); [Public Sector Bodies (Websites and Mobile Applications) Accessibility Regulations 2018 (SI 2018/952), WCAG 2.2 AA](https://www.legislation.gov.uk/uksi/2018/952/contents); [MHRA Guidance Note 6 — Good Distribution Practice](https://assets.publishing.service.gov.uk/media/67ea784e0678ace40a7f275c/GN_6_Brexit_changes-GDP.pdf).

---

## How to read this document

These personas describe **specific individual people** — not generalised role archetypes — who interact with the ImmForm new-user registration journey. Each persona maps to one of three framework levels (Strategic, Tactical, Operational) and one of seven workflow stages.

**Scope note on patient personas.** The ImmForm new-user registration journey serves *product orderers* (clinical, administrative and wholesale staff) and the people who approve, govern and support their accounts. It is not a public-facing service. Members of the public, patients, family carers, and patient advocacy groups are not in scope as direct users of the registration journey, but the *outcome* of the service — timely vaccine availability through their GP, sexual health clinic, occupational health provider, or hospital — directly affects them. We have therefore not written patient/carer personas for the onboarding journey itself, but flagged downstream impact under "Wider Journey & Touchpoints" in each operational persona. This decision is documented for the GDS Service Standard assessment.

**Identity binding is non-negotiable.** Every applicant persona must hold (or be sponsored by someone holding) a GMC, GPhC, NMC or Wholesale Dealer's Licence number — this is the regulator identifier that ImmForm uses to bind an account to a delivery point ([How to register: ImmForm helpsheet, UKHSA](https://www.gov.uk/government/publications/how-to-register-immform-helpsheet-8/how-to-register-immform-helpsheet)).

**Names are fictional**, drawn from a UK-representative range; all job titles, organisations, statutory frames, and regulatory references are real. Where a factual claim could not be verified to a primary source it is omitted or marked clearly as illustrative.

---

## Coverage matrix

| # | Persona | Workflow stage | Framework level | User group / role |
|---|---------|----------------|-----------------|-------------------|
| 1 | Priya Shah | A — Registration applicants | Operational | Routine immunisation: GP practice manager |
| 2 | Daniel Okonkwo | A — Registration applicants | Operational | Routine immunisation: practice nurse (named RHCP) |
| 3 | Yusuf Rahman | A — Registration applicants | Operational | GBMSM + Mpox: sexual health service lead |
| 4 | Margaret Findlay | A — Registration applicants | Operational | Occupational health (BCG/Tuberculin PPD, private) |
| 5 | Chioma Adebayo | A — Registration applicants | Tactical | COVID-19 programme: NHS trust vaccination coordinator |
| 6 | Iain MacLeod | A — Registration applicants | Operational | Immunoglobulin Holding Centre pharmacist |
| 7 | Eleanor Fairclough | A — Registration applicants | Tactical | Wholesaler Responsible Person (WDA(H)) |
| 8 | Dr Helen Vickers | B — Approvers | Tactical | GP senior partner (small-org approver) |
| 9 | Marcus Doyle | B — Approvers | Tactical | NHS trust vaccination service lead (large-org approver) |
| 10 | Sarah Mitchell | C — UKHSA service ops | Operational | ImmForm helpdesk agent |
| 11 | James Patterson | C — UKHSA service ops | Tactical | ImmForm helpdesk / service manager |
| 12 | Amrita Chopra | D — UKHSA product & design | Tactical | ImmForm product manager |
| 13 | Theo Brennan | D — UKHSA product & design | Tactical | Senior user researcher / service designer |
| 14 | Dr Olu Babatunde | E — Compliance & governance | Tactical / Strategic | UKHSA GDP compliance lead / Responsible Person |
| 15 | Rachel Goldstein | E — Compliance & governance | Tactical | Information Governance and Data Protection Officer |
| 16 | Dr Catriona Lewis | F — UKHSA strategic | Strategic | Head of Immunisation & Vaccine Preventable Diseases |
| 17 | Steve Mukherjee | G — External partners | Tactical / Strategic | NHS England regional vaccinations lead |
| 18 | Dr Aisha Bello | G — External partners | Strategic | Local authority Director of Public Health |
| 19 | Dr Charlotte Penrose | G — External partners | Strategic | DHSC immunisation policy lead |

**Accessibility coverage** is distributed across personas (dyslexia, screen-reader use, hearing impairment, English-as-additional-language, low digital confidence, intermittent rural connectivity) rather than concentrated in a separate "disabled user" persona — accessibility is a property of every user, in line with WCAG 2.2 AA expectations under [SI 2018/952](https://www.legislation.gov.uk/uksi/2018/952/contents).

---

# Workflow Stage A — Registration Applicants

The seven user groups defined in the project scope all share the same registration journey. The personas below illustrate the *meaningfully different contexts* in which that single journey is encountered.

---

## 1. Priya Shah — GP Practice Manager (Routine Immunisation Programme)

**Workflow stage:** A — Registration applicants
**Framework level:** Operational
**User group:** Routine immunisation programme staff
**Approver relationship:** Submits the registration; the approver is one of the GP partners.

### Persona Name & Role
- **Name:** Priya Shah
- **Role:** Practice Manager
- **Organisation:** Cherrywood Medical Centre — a five-partner GP practice in suburban Leicester, list size ~9,800 patients (slightly above the England average of 2,257 patients per fully qualified GP, [RCGP, Sept 2025](https://www.rcgp.org.uk/News/Number-of-GP-practices)).
- **Position level:** Senior administrative leadership, reports directly to the senior partner. General practice staff are not on NHS Agenda for Change — Priya's salary is set by the partnership using AfC as a benchmark ([NHS Employers, AfC 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526); role context from [NHS Health Careers — Practice Manager](https://www.healthcareers.nhs.uk/explore-roles/management/roles-management/practice-manager)).
- **Background:** 14 years in general practice administration, the last six as practice manager. Trained originally as a medical secretary; holds an IGPM Level 5 qualification. First language Gujarati; fluent professional English.

### Goals & Outcomes
- **Primary objective:** Keep the practice's flu and routine childhood immunisation orders flowing without interruption — the practice runs ~4,000 flu doses in a peak autumn season and any delay in ordering hits the QOF-linked income directly.
- **Personally tracks:** Cold-chain incidents per quarter (target zero), seasonal flu uptake rate against PCN benchmark, time from joining-clinician-arrival to ImmForm access (currently >2 weeks because of registration lag plus partner-signature delay).
- **Success metric:** A new salaried GP or practice nurse joining the practice has working ImmForm access on their first clinical day, not their fifteenth.
- **Wants from this service, in her words:** *"I want a clinician walking through the door on day one to be able to order their own vaccines on day one. Right now, I'm chasing a partner for a wet signature on a PDF for a fortnight."*

### Wants, Needs & Expectations
- **Daily workflow:** Manages clinical rota, payroll, CQC evidence, complaints, QOF returns; immunisation is one of forty things she touches in a week. Registration tasks need to be **completable in under 10 minutes** including approver chase.
- **Information she relies on:** Practice ODS code (currently has to look this up every time on the [NHS Digital ODS portal](https://digital.nhs.uk/services/organisation-data-service)), partners' GMC numbers, new clinician's NMC or GMC number, the practice's central NHS.net mailbox.
- **Technology expectations:** Uses EMIS Web all day; comfortable with web forms; expects services to remember her organisation details across sessions. Has digital practice telephony (NHSE reports 99% of practices now do, [NHSE press release 2 June 2025](https://www.england.nhs.uk/2025/06/gp-practices-improve-access-embracing-technology-increasing-appointments/)).
- **Support/training needs:** None — she is a competent business administrator. She wants self-service, not handholding.
- **Preferred communication:** Email (NHS.net), automated status notifications, no phone calls during morning surgery (08:00–11:30).

### Biggest Pain Points & Unmet Needs
- The current PDF asks for the practice's ODS code, ImmForm account number, and partner GMC numbers — **the system already knows all of these** for an existing practice, but she has to re-key them anyway.
- Approver signature delay: the partners' GP sessions don't overlap with her working hours and the PDF needs a wet signature.
- No status visibility: once the form is emailed to the helpdesk it disappears into a black box for up to five working days ([How to register helpsheet](https://www.gov.uk/government/publications/how-to-register-immform-helpsheet-8/how-to-register-immform-helpsheet)).
- Validation errors are discovered late: a typo in an NMC number means the whole cycle restarts.
- She has been asked twice in the past year to use a **shared mailbox** as the account email, which the helpdesk refused — the rule was not visible at the point of entry.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Prevention (immunisation delivery).
- **Organisations she interacts with:** her PCN, the local NHSE regional vaccination team, the practice's CQC inspector, NHS Digital (ODS), local authority public health for school-age handovers, the cold-chain supplier visiting twice a year.
- **Offline channels:** the partners still sign some things on paper; she keeps a paper logbook of cold-chain fridge temperatures because that's what the last CQC inspection asked to see.
- **Handoffs and dependencies:** Once an ImmForm account is live, ordering routes through to UKHSA's logistics contractor (Movianto for centrally procured childhood vaccines, [NHS England ImmForm Manual Input Guidance 2025/26](https://www.england.nhs.uk/south/wp-content/uploads/sites/8/2025/09/ImmForm-Manual-Input-Guidance-2526.docx)).
- **Downstream impact on the public:** Each week of registration delay is a week in which one of her clinicians can't order routine childhood vaccines — directly affecting the children on her ~9,800-patient list.

### Additional Context
- **A typical day:** Arrives 07:45, opens the practice, runs the morning huddle at 08:00, deals with locum-cover crises, signs payslips, processes one or two complaints, prepares the QOF return, leaves at 18:30.
- **Technical proficiency:** High for clinical and administrative systems she uses daily; moderate for one-off government services where the friction shows.
- **Digital access:** Practice-provided Windows laptop with HSCN connectivity, NHS.net mailbox, smartphone with NHS mail mobile app.
- **Accessibility needs:** None disclosed; reads in a second language so values plain-English forms with no jargon, in line with [GOV.UK style guidance](https://www.gov.uk/guidance/content-design/writing-for-gov-uk).
- **Decision-making authority:** Initiates the registration, but the *clinical authorisation* is the partner's.
- **Regulatory/compliance considerations:** CQC registration, GMS/PMS contract, IG Toolkit / DSPT — she is the practice's Data Security & Protection Toolkit lead.
- **Workload:** consistently 50–55 hours/week, peak in autumn flu season.
- **Cultural/linguistic:** Gujarati first language; reads English fluently; some of her older patients also speak Gujarati — she's aware that *patient*-facing materials need translation, and she expects *staff*-facing services to be plain English.

---

## 2. Daniel Okonkwo — Practice Nurse (Named Registered Healthcare Practitioner)

**Workflow stage:** A — Registration applicants
**Framework level:** Operational
**User group:** Routine immunisation programme staff (the clinician who must be named on the account)
**Approver relationship:** Sponsored by the GP partner; the practice manager (Priya, persona 1) usually submits.

### Persona Name & Role
- **Name:** Daniel Okonkwo
- **Role:** Senior Practice Nurse and immunisation lead
- **Organisation:** Cherrywood Medical Centre (same practice as Priya).
- **Position level:** Equivalent of Agenda for Change Band 7 if benchmarked to a community trust ([NHS Employers AfC 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526)); in practice he is salaried by the partnership.
- **Reporting structure:** Reports to the senior partner clinically and to the practice manager operationally.
- **Background:** Qualified as a registered nurse in 2009 (NMC registered, NMC PIN active); completed the UKHSA-compliant foundation immunisation training and does the annual update ([UKHSA National Minimum Standards for immunisation training 2025](https://www.gov.uk/government/publications/national-minimum-standards-and-core-curriculum-for-immunisation-training-for-registered-healthcare-practitioners)). Six years at Cherrywood, previously eight years on a hospital paediatric ward. Nigerian-British, came to the UK in 2007.

### Goals & Outcomes
- **Primary objective:** Run safe, on-time immunisation clinics (routine childhood, school-age catch-up, seasonal flu, shingles, pneumococcal, RSV) within his PGD/PSD scope.
- **Personally tracks:** Cold-chain logs (he signs the fridge log twice daily), DNA rate, vaccine wastage, his own annual training currency.
- **Success metric:** Zero adverse events, no vaccine wastage, his name correctly bound to the practice's ImmForm delivery point so deliveries clear customs and arrive next-day.
- **Desired outcome from the service:** *"I just want the ordering portal to recognise that I am the nurse on this practice — once. Not every time we onboard a new colleague."*

### Wants, Needs & Expectations
- **Daily workflow:** Six-hour clinic block, one administrative hour at the end of the day. Registration is not part of his routine — he encounters it when he starts at a new practice, joins a second practice as a sessional, or covers a flu-clinic pop-up site.
- **Information he relies on:** His NMC PIN (memorised), his immunisation training certificate, his National Insurance number, his practice ODS code (he doesn't usually know this — has to ask the practice manager).
- **Technology expectations:** Uses EMIS, the practice's e-consult tool, NHS App for his own care, plus ImmForm when ordering. Comfortable on a desktop; less confident on mobile for work tasks.
- **Support/training needs:** None for the act of ordering; some context-help on what "delivery point" means in ImmForm vs the way the partnership describes the practice estate.
- **Preferred communication:** NHS.net email; occasional SMS for two-factor codes.

### Biggest Pain Points & Unmet Needs
- The PDF form asks for information he doesn't routinely carry (organisation codes, the practice's existing ImmForm account number); the practice manager has to find it for him.
- He is sometimes asked to provide a personal mobile number for verification — he resists this for privacy and because he uses a personal pay-monthly phone.
- When he moved into a sessional role across **two** practices last year, ImmForm could not represent his dual affiliation cleanly. He ended up with one account he uses for both, which neither he nor the practice managers are sure is compliant.
- Approver-signature delay affects him the same way as Priya, but with the added clinical risk that a child's MMR dose is held up.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Prevention (delivery).
- **Organisations he interacts with:** the practice, the PCN immunisation lead, school-age vaccination provider (for transitions out at age 16), local authority health visiting team (handovers in), UKHSA when reporting an adverse event via [Yellow Card](https://yellowcard.mhra.gov.uk/).
- **Offline channels:** wet-signature consent forms for children under 16 in some cohorts; the practice's paper fridge log.
- **Handoffs and dependencies:** Vaccine delivery via Movianto for centrally procured stock ([NHSE ImmForm Manual Input Guidance 2025/26](https://www.england.nhs.uk/south/wp-content/uploads/sites/8/2025/09/ImmForm-Manual-Input-Guidance-2526.docx)).
- **Downstream impact on the public:** Each child on his clinic list who can't be vaccinated this fortnight gets pushed to the next clinic.

### Additional Context
- **A typical day:** 08:00–08:30 clinic prep and fridge check; 08:30–13:00 immunisation clinic (15-minute slots); 13:00–13:30 lunch; 13:30–17:00 mixed clinic (wound care, B12, NHS health checks, more immunisation); 17:00–18:00 documentation, ordering, training catch-up.
- **Technical proficiency:** High for clinical, moderate for ordering systems.
- **Digital access:** Practice desktop, NHS Smartcard, NHS.net mailbox, personal smartphone.
- **Accessibility needs:** Mild dyslexia, undiagnosed until university; relies on plain language, generous spacing, and the ability to re-read confirmation screens.
- **Decision-making authority:** Clinical responsibility for safe administration, but no authority to authorise his own ImmForm account — the practice partner must approve.
- **Regulatory/compliance considerations:** NMC revalidation every three years, annual immunisation update training, PGD signatory record.
- **Workload:** Typically 37.5 hours/week; takes on extra flu clinics in autumn.
- **Cultural/linguistic:** Nigerian heritage; trilingual (English, Igbo, conversational Yoruba); none of which is needed for ImmForm but informs his patience with poor service design.

---

## 3. Yusuf Rahman — Sexual Health Service Lead Nurse (GBMSM + Mpox Programmes)

**Workflow stage:** A — Registration applicants
**Framework level:** Operational
**User group:** GBMSM programme orderers AND Mpox programme orderers (merged — both delivered through specialist sexual health services per UKHSA / NHSE guidance: [HPV GBMSM info](https://www.gov.uk/government/publications/hpv-vaccination-for-msm-posters-and-leaflets/information-on-hpv-for-gbmsm-from-september-2023); [Mpox vaccination info](https://www.gov.uk/government/publications/vaccination-against-mpox-information-for-healthcare-practitioners/mpox-vaccination-information-for-healthcare-practitioners)).
**Approver relationship:** Approved by the clinical service manager / consultant lead.

### Persona Name & Role
- **Name:** Yusuf Rahman
- **Role:** Lead Nurse, Integrated Sexual Health Service
- **Organisation:** A central London integrated sexual health service hosted by an NHS foundation trust, commissioned by the local authority. England has 241 publicly commissioned sexual health services overall ([Mohammed et al., PLoS ONE 2026, via UKHSA research portal](https://researchportal.ukhsa.gov.uk/en/publications/enhancing-surveillance-of-sexually-transmitted-infections-in-engl/)).
- **Position level:** Agenda for Change Band 7 ([NHS Employers AfC 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526)).
- **Reporting structure:** Reports to the consultant in genitourinary medicine who leads the service.
- **Background:** NMC-registered nurse since 2012; specialist sexual health training; PGD signatory for HPV, Hep A/B and Mpox (operating to the [NHSE-UKHSA HPV GBMSM PGD v5.0](https://www.england.nhs.uk/london/wp-content/uploads/sites/8/2025/10/NHSE-UKHSA-HPV-GBMSM-PGD-v5.0.pdf)). Bengali-British, raised in Tower Hamlets.

### Goals & Outcomes
- **Primary objective:** Hit the JCVI-recommended GBMSM HPV coverage in his catchment (offer expanded September 2023 to ages up to 45, [UKHSA](https://www.gov.uk/government/publications/hpv-vaccination-for-msm-posters-and-leaflets/information-on-hpv-for-gbmsm-from-september-2023)) and maintain Mpox coverage for the eligible high-risk cohort ([UKHSA Mpox HCP guidance](https://www.gov.uk/government/publications/vaccination-against-mpox-information-for-healthcare-practitioners/mpox-vaccination-information-for-healthcare-practitioners)).
- **Personally tracks:** Weekly first-dose and completion numbers per programme; vial wastage (Mpox MVA-BN intradermal/subcutaneous fractional dosing makes wastage politically visible); cold-chain log; GUMCAD return ([UKHSA GUMCAD](https://www.gov.uk/guidance/gumcad-sti-surveillance-system)).
- **Success metric:** No clinic-day where a service user is turned away for lack of vaccine in the fridge.
- **Desired outcome from the service:** *"When my new nurse starts on Monday, she shouldn't be queuing behind a five-day registration delay before she can order vials for a Friday clinic."*

### Wants, Needs & Expectations
- **Daily workflow:** Two morning walk-in clinics, a booked PrEP clinic in the afternoon, a Saturday GBMSM-targeted outreach clinic monthly. Account registration touches him whenever a colleague joins, leaves, or rotates.
- **Information he relies on:** His NMC PIN, the trust's ODS code, the service's existing ImmForm delivery-point reference, the consultant's GMC number for sponsorship.
- **Technology expectations:** Uses the trust EPR (Cerner), a separate sexual health record (Lilie), NHS.net email; expects mobile-friendly responsive design because half his admin happens on the trust-issued iPad between clinic rooms.
- **Support/training needs:** None for the act of registration; would value clearer guidance on which "programme" to select for a service that runs **both** GBMSM HPV and Mpox out of the same fridge.
- **Preferred communication:** NHS.net; he reads but does not write quickly in long emails — bullet points please.

### Biggest Pain Points & Unmet Needs
- **Identity bleed:** in the 2022 Mpox response, registration delays meant his service ran out of vials for a high-risk cohort over a long weekend — a known equity issue he is determined never to repeat.
- The PDF form has no way to register one nurse against **two** programmes (GBMSM and Mpox) without re-stating delivery-point detail twice.
- Mpox eligibility criteria changed mid-programme; his staff sometimes don't realise their ImmForm permissions are mis-aligned until the order is rejected.
- He does not have a shared service mailbox for his team — he wishes he could register against `sh.vaccines@trust.nhs.uk` rather than each individual nurse's personal NHS.net.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Prevention (high-risk targeted), and outbreak response (Mpox).
- **Organisations he interacts with:** the local authority commissioner (statutory commissioner of sexual health services, [HSC Act 2012 / NHS Act 2006 s.73A explanatory notes](https://www.legislation.gov.uk/ukpga/2012/7/notes/division/5/1/4/2)), UKHSA's local Health Protection Team, the trust's pharmacy, third-sector partners (e.g. a local LGBTQ+ outreach charity), national HIV charities for co-promotion.
- **Offline channels:** outreach pop-ups, fridges at partner venues; paper consent for non-English-speaking attendees.
- **Handoffs and dependencies:** GUMCAD return back to UKHSA monthly.
- **Downstream impact on the public:** Eligible GBMSM service users who can't access a clinic-stocked vaccine are at higher risk of HPV-attributable cancers and (during outbreaks) Mpox.

### Additional Context
- **A typical day:** 08:00 fridge check; 08:30–12:30 walk-in clinic; 12:30 huddle; 13:00–17:00 booked PrEP clinic and Mpox vaccinations; 17:00–18:00 GUMCAD coding, stock check, ordering.
- **Technical proficiency:** High; teaches the trust's digital-skills induction for new nurses.
- **Digital access:** Trust desktop, trust iPad, NHS.net mailbox, personal smartphone.
- **Accessibility needs:** None disclosed for himself. Strongly aware that his patient-facing service needs to support service users with hearing impairment, English-as-additional-language, and HIV-related cognitive impairment — but for ImmForm itself, he just wants legible plain English.
- **Decision-making authority:** Operational lead; does not approve his own account.
- **Regulatory/compliance considerations:** NMC, PGD compliance, GDPR special-category data, GUMCAD reporting.
- **Workload:** 40 hours contracted, ~45 actual; on-call rota during outbreak periods.
- **Cultural/linguistic:** Bengali first language; relevant because the service users he sees include other Bengali-speaking GBMSM whose registration disclosure risks are higher.

---

## 4. Margaret Findlay — Occupational Health Nurse (BCG and Tuberculin PPD, Private Setting)

**Workflow stage:** A — Registration applicants
**Framework level:** Operational
**User group:** Occupational health and private (BCG and Tuberculin PPD only) — accounts operate under a private customer account number.
**Approver relationship:** Approved by the medical director of the occupational health provider (the named MHRA Responsible Person if the org also holds a WDA(H), otherwise the OH service's clinical lead).

### Persona Name & Role
- **Name:** Margaret Findlay
- **Role:** Occupational Health Nurse Adviser
- **Organisation:** A SEQOHS-accredited independent occupational health provider based in Glasgow, with clients across higher education, construction, and care home groups. Privately owned; not part of an NHS trust.
- **Position level:** Equivalent to Agenda for Change Band 6 ([NHS Employers AfC 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526)) — the firm benchmarks against AfC for retention.
- **Reporting structure:** Reports to the Chief Nurse / Clinical Director of the firm.
- **Background:** NMC-registered since 1998; Specialist Community Public Health Nurse (Occupational Health) qualification; 17 years in OH after a decade in respiratory wards. Scottish, raised in Stirlingshire.

### Goals & Outcomes
- **Primary objective:** Maintain pre-employment and in-service TB screening (Mantoux/PPD interpretation) and BCG vaccination for client organisations whose roles involve healthcare exposure or international travel for fieldwork.
- **Personally tracks:** Mantoux read-back rate at 48–72 hours, BCG uptake by client cohort, vial cold-chain audit.
- **Success metric:** No client missed their statutory pre-employment screening because she couldn't order Tuberculin PPD or BCG in time.
- **Desired outcome from the service:** *"I'm not NHS — I'm a private OH nurse — but my BCG and PPD orders go through ImmForm because that's the only legal route. The service treats me like a stranger every time I onboard a new colleague."*

### Wants, Needs & Expectations
- **Daily workflow:** Diary-driven appointments at client sites and at the firm's clinic; volume is lower than a sexual health service but governance is heavier because every vial is on a private customer account number.
- **Information she relies on:** Her NMC PIN, the firm's MHRA-issued private customer account number, the medical director's GMC number for sponsorship, the firm's CQC registration.
- **Technology expectations:** Uses her firm's bespoke OH-soft package; comfortable on Windows; less confident on mobile but uses an iPad for site visits. Lives in a Stirlingshire village with **patchy mobile signal** — depends on home Wi-Fi for evening admin.
- **Support/training needs:** Help understanding what counts as "the organisation" when her firm has multiple delivery sites across Scotland.
- **Preferred communication:** Email; phone if urgent during working hours (09:00–17:00).

### Biggest Pain Points & Unmet Needs
- The current PDF assumes the applicant is NHS — language, examples, and field labels (e.g. "Trust", "PCN") don't map onto her private OH provider context.
- The five-working-day SLA is the entire booking lead time for her next BCG clinic — a registration delay literally cancels a clinic.
- BCG is intermittently supply-constrained nationally; when supply restarts she needs to onboard fast or her firm loses a client.
- No way to distinguish between "private customer" and "NHS" account context at point of registration — she ends up calling the helpdesk to clarify every time a new nurse joins.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Prevention (occupational and selected high-risk).
- **Organisations she interacts with:** Client HR teams, NHS occupational health departments of trust clients, [SEQOHS](https://www.seqohs.org/), the local UKHSA Health Protection Team if a TB case is detected, the firm's MHRA-licensed pharmacy.
- **Offline channels:** Site visits with vials in a Validated Cold Chain Box; paper consent forms for clients without digital onboarding.
- **Handoffs and dependencies:** TB cases detected through her screening are referred to NHS TB services; BCG cohort lists are negotiated with each client HR team.
- **Downstream impact on the public:** Misses in OH TB screening flow through into community transmission risk; her firm screens ~3,000 staff per year for client organisations.

### Additional Context
- **A typical day:** Drive to a client site, run 15–20 appointments, drive back, late-afternoon admin.
- **Technical proficiency:** Moderate — competent but not fast. Doesn't enjoy fixing forms.
- **Digital access:** Firm laptop, Mi-Fi dongle for site visits, intermittent mobile signal at home, broadband at the office.
- **Accessibility needs:** Reading glasses for screens (presbyopia from her late 40s); finds dense forms tiring and benefits from chunked progress indicators.
- **Decision-making authority:** None on her own account; she submits, the medical director approves.
- **Regulatory/compliance considerations:** NMC, SEQOHS standards, MHRA private-customer account rules, ICO data protection.
- **Workload:** 0.8 WTE (Tuesday–Friday); takes school holidays for her teenagers.
- **Cultural/linguistic:** English first language; Scots accent and Scottish English idiom; not a UKHSA pain point but a reminder the service is **not London-centric**.

---

## 5. Chioma Adebayo — NHS Trust COVID-19 Vaccination Coordinator

**Workflow stage:** A — Registration applicants
**Framework level:** Tactical
**User group:** COVID-19 programme orderers (NHS and other provider staff ordering COVID-19 vaccines under the national programme).
**Approver relationship:** Approved by the trust's Chief Pharmacist or Vaccination Service Clinical Lead.

### Persona Name & Role
- **Name:** Chioma Adebayo
- **Role:** COVID-19 Vaccination Programme Coordinator
- **Organisation:** A large NHS acute foundation trust in the West Midlands running a hospital hub and three community vaccination sites for the seasonal autumn-booster cohort.
- **Position level:** Agenda for Change Band 8a ([NHS Employers AfC 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526)).
- **Reporting structure:** Reports to the Director of Pharmacy; matrix-reports to the regional NHSE vaccinations team during seasonal campaigns.
- **Background:** Started as a Band 5 staff nurse in 2014, moved into vaccination programme operations during the 2021 COVID-19 mass-vaccination response, has stayed in seasonal coordination since.

### Goals & Outcomes
- **Primary objective:** Order, store, distribute and account for COVID-19 vaccine across all four sites during the autumn-booster window; reconcile to dose-administered records.
- **Personally tracks:** Stock-on-hand vs. clinic-day demand; wastage by site (target <5%); time-to-account-creation for ~25 staff she onboards each autumn (locum nurses, vaccinators, admin support).
- **Success metric:** No clinic cancelled because a staff member couldn't be onboarded onto ImmForm in time.
- **Desired outcome from the service:** *"Twenty-five new joiners in September. I need to bulk-onboard them in a day, not stagger over five weeks because the helpdesk can only process forty-something forms a day."*

### Wants, Needs & Expectations
- **Daily workflow:** Procurement and logistics for COVID-19 stock; staff rota; cold-chain governance; data return into the Foundry-based national booking system; weekly call with NHSE region.
- **Information she relies on:** ODS codes for each delivery site, GMC/NMC/GPhC numbers for ~25 vaccinators, the trust's existing ImmForm account references, the Chief Pharmacist's regulator number for sponsorship.
- **Technology expectations:** Power-user. Uses Excel daily, Trust SharePoint, the National Booking Service, the trust EPR. Strong on data integrity; expects an API or bulk CSV import for staff registrations.
- **Support/training needs:** None for ImmForm itself; needs predictable behaviour from the service so she can plan the campaign timeline.
- **Preferred communication:** Microsoft Teams within the trust; email for cross-org; an ImmForm dashboard or notification feed if it existed.

### Biggest Pain Points & Unmet Needs
- **Scale.** She onboards 20–30 people in a two-week window before the campaign starts; the current PDF + email + 5-day SLA does not scale ([How to register helpsheet](https://www.gov.uk/government/publications/how-to-register-immform-helpsheet-8/how-to-register-immform-helpsheet)). One ImmForm registration delay can hold up a 1,200-dose clinic.
- No visibility into helpdesk queue depth — she can't tell whether to escalate or wait.
- Approver-signature chase: the Chief Pharmacist is signing dozens of PDFs in the same window.
- She has been told "shared mailboxes aren't allowed" but the rule isn't visible at form entry, so she's wasted hours.
- The trust's information-governance team requires that her audit trail meets MHRA GDP standards ([MHRA Guidance Note 6](https://assets.publishing.service.gov.uk/media/67ea784e0678ace40a7f275c/GN_6_Brexit_changes-GDP.pdf)) — an email thread doesn't.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Prevention and outbreak response — COVID-19 vaccination is the bridge between the two.
- **Organisations she interacts with:** Trust pharmacy, NHSE regional vaccination team, ICB primary care contracting, local authority for housebound cohorts, UKHSA logistics contractor for stock delivery, the National Booking Service team.
- **Offline channels:** Some housebound and care-home cohort delivery via paper consent and clinical notes.
- **Handoffs and dependencies:** ImmForm → Foundry NBS → trust pharmacy → vaccinator on the clinic floor → patient.
- **Downstream impact on the public:** the trust's catchment includes ~1.2 million people; each delayed clinic shifts ~1,200 doses by a week.

### Additional Context
- **A typical day in September:** Daily stand-up at 08:30; supplier and helpdesk emails 09:00–10:00; site walk-rounds across sites 10:00–15:00; performance return 16:00; rota for next week 17:00.
- **Technical proficiency:** High; designed her own Power BI dashboard for stock vs. demand.
- **Digital access:** Trust laptop, Trust mobile, NHS.net mailbox, multi-factor authentication everywhere.
- **Accessibility needs:** None disclosed. Welcomes services that pass back machine-readable receipts she can ingest into her reconciliation spreadsheet.
- **Decision-making authority:** Operational decisions on stock distribution; cannot approve her own ImmForm account.
- **Regulatory/compliance considerations:** MHRA GDP audit trail expectations, trust IG/DSPT, NHSE national programme letters.
- **Workload:** 60–70 hours/week in campaign launch period; 37.5 in steady state.
- **Cultural/linguistic:** Nigerian-British; English first language at work; not a service-design constraint but a reminder of the diverse trust workforce she onboards.

---

## 6. Iain MacLeod — Pharmacist, Immunoglobulin Holding Centre

**Workflow stage:** A — Registration applicants
**Framework level:** Operational (but with regulatory exposure equivalent to Tactical)
**User group:** Immunoglobulin Holding Centre staff receiving immunoglobulin deliveries ordered by the UKHSA Rabies and Immunoglobulin Service (RIgS).
**Approver relationship:** Approved by the Chief Pharmacist of the host NHS trust (also the trust's MHRA RP if the trust holds a WDA(H) Specials Licence).

### Persona Name & Role
- **Name:** Iain MacLeod
- **Role:** Senior Pharmacist — Aseptic Services, designated as Immunoglobulin Holding Centre lead
- **Organisation:** A teaching hospital in Edinburgh designated by UKHSA as one of a small number of Immunoglobulin Holding Centres holding HRIG, HNIG, VZIG, HBIG and tetanus immunoglobulin for emergency release ([RIgS, UKHSA](https://www.gov.uk/government/publications/immunoglobulin-when-to-use/rabies-and-immunoglobulin-service-rigs)).
- **Position level:** Agenda for Change Band 8a ([NHS Employers AfC 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526)).
- **Reporting structure:** Reports to the Chief Pharmacist; functional accountability to UKHSA RIgS for product stewardship.
- **Background:** GPhC-registered pharmacist since 2008; postgraduate diploma in clinical pharmacy; 11 years in aseptic services, the last four with IHC lead responsibility. Highland Scottish background.

### Goals & Outcomes
- **Primary objective:** 24/7 availability of immunoglobulin products for emergency clinical release, in compliance with GDP and Specials Licence conditions.
- **Personally tracks:** Stock-on-hand by product; near-expiry alerts; UKHSA-issued batch records; out-of-hours release log; temperature excursions.
- **Success metric:** Zero stock-outs of HRIG when a patient with potential rabies exposure presents to the emergency department.
- **Desired outcome from the service:** *"My ImmForm account is functionally a regulatory artefact. If I retire and my replacement can't order for two weeks, the centre is non-compliant — that's reportable."*

### Wants, Needs & Expectations
- **Daily workflow:** Aseptic compounding oversight, immunoglobulin stock review, RIgS calls (24/7 line `0330 128 1020`, [RIgS, UKHSA](https://www.gov.uk/government/publications/immunoglobulin-when-to-use/rabies-and-immunoglobulin-service-rigs)), MHRA inspection prep.
- **Information he relies on:** His GPhC number, the trust's IHC designation reference, the Chief Pharmacist's GPhC for sponsorship, the trust ODS code.
- **Technology expectations:** Heavy spreadsheet user; uses the trust's e-prescribing and stock-control systems; prefers structured forms with strict validation because the consequences of error are clinical.
- **Support/training needs:** None on the ordering side; would value training material aimed at IHC-specific terminology (most ImmForm guidance is written for routine immunisation).
- **Preferred communication:** Email; written confirmation of any phone agreement.

### Biggest Pain Points & Unmet Needs
- The ImmForm registration journey is designed around routine vaccine ordering, not specialist biological release; he often selects fields that don't quite fit (e.g. "programme").
- An email approval chain is a poor audit substitute when MHRA inspects the trust's Specials Licence holdings.
- Out-of-hours staffing changes — locum pharmacists on bank holiday rotas — can't be onboarded fast enough to take RIgS calls.
- No reciprocal-trust between his trust's HR-verified staff record and ImmForm: every new pharmacist starts a fresh registration from zero.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Response (urgent release of immunoglobulin for exposure-prophylaxis cases).
- **Organisations he interacts with:** UKHSA RIgS (the central authoriser of release), the trust's emergency department, microbiology, ICB pharmacy network, MHRA (inspection), the trust's R&D for clinical-trial-affiliated immunoglobulin use.
- **Offline channels:** 24/7 phone line to RIgS; paper batch release records for some legacy products.
- **Handoffs and dependencies:** RIgS authorises issue → IHC pharmacist physically releases → ward administers → adverse event reporting via Yellow Card.
- **Downstream impact on the public:** Each delayed HRIG release is a patient at risk; each delayed VZIG release is a high-risk pregnant contact unprotected.

### Additional Context
- **A typical day:** 08:00 morning ward round of aseptic queue; 09:30 stock review; 11:00 multidisciplinary call; 13:00 lunch; afternoon mix of compounding oversight, near-expiry transfers and MHRA evidence pack maintenance; 17:00 home.
- **Technical proficiency:** High for clinical systems and Excel; moderate for one-off government services.
- **Digital access:** Trust laptop, trust mobile, NHS Mail account, on-call mobile.
- **Accessibility needs:** None disclosed; spends long hours at screens and benefits from generous typography.
- **Decision-making authority:** Clinical decisions on release in collaboration with RIgS; no authority to self-approve ImmForm account.
- **Regulatory/compliance considerations:** GPhC revalidation; MHRA WDA(H) / Specials Licence audit; GDP; Yellow Card adverse event reporting.
- **Workload:** 37.5 hours plus on-call; on-call premium pay during weekends.
- **Cultural/linguistic:** Scottish Gaelic family connection but English at work; clinically multilingual when interacting with the trust's international medical workforce.

---

## 7. Eleanor Fairclough — Wholesaler Responsible Person (WDA(H))

**Workflow stage:** A — Registration applicants
**Framework level:** Tactical / Strategic (single named regulatory officer of the organisation)
**User group:** Wholesalers — Responsible Persons and authorised staff at organisations holding a Wholesale Dealer Authorisation WDA(H) who order medicinal products through ImmForm.
**Approver relationship:** *Is* the approver for her own organisation's staff; her own account is sponsored at organisational level by the company's Quality Director.

### Persona Name & Role
- **Name:** Eleanor Fairclough
- **Role:** Responsible Person (RP), as named on the Wholesale Dealer's Authorisation (Human) — the statutory MHRA-named individual under the Human Medicines Regulations 2012 ([MHRA Guidance Note 6](https://assets.publishing.service.gov.uk/media/67ea784e0678ace40a7f275c/GN_6_Brexit_changes-GDP.pdf); [Apply for manufacturer or wholesaler of medicines licences](https://www.gov.uk/guidance/apply-for-manufacturer-or-wholesaler-of-medicines-licences)).
- **Organisation:** A mid-size pharmaceutical wholesaler based in Manchester, distributing vaccines and biological products to NHS trusts, occupational health firms and private clinics under WDA(H).
- **Position level:** Strategic for her own organisation (named on the licence; personal regulatory liability); tactical from UKHSA's point of view.
- **Reporting structure:** Reports to the company's Managing Director; statutorily accountable to MHRA.
- **Background:** GPhC-registered pharmacist since 2005; named RP since 2017; multiple MHRA inspections under her belt; has personally negotiated the post-Brexit GDP transition in her firm ([MHRA GN6](https://assets.publishing.service.gov.uk/media/67ea784e0678ace40a7f275c/GN_6_Brexit_changes-GDP.pdf)).

### Goals & Outcomes
- **Primary objective:** Maintain the WDA(H) in good standing through every MHRA inspection; ensure every order placed on ImmForm by her firm has a defensible audit trail.
- **Personally tracks:** Inspection findings; deviation reports; CAPA closure rate; cold-chain breach incidents; staff training currency.
- **Success metric:** Clean MHRA inspection. Her personal regulatory record (and the licence) depends on it.
- **Desired outcome from the service:** *"If ImmForm is part of my distribution chain, then its onboarding journey is part of my MHRA inspection. I need it to produce evidence — not emails."*

### Wants, Needs & Expectations
- **Daily workflow:** GDP self-inspections, batch release oversight, customer-due-diligence reviews, change-control sign-off, MHRA correspondence.
- **Information she relies on:** Her own GPhC PIN and Wholesale Dealer Licence (WDL) number, the firm's MHRA Site Master File, her CAPA register.
- **Technology expectations:** Heavy user of a Quality Management System (QMS); expects e-signatures, timestamped audit logs, and the ability to export records for inspectors.
- **Support/training needs:** None for the act of registration; needs absolute clarity on which fields constitute *regulated* statements (e.g. GDP assurances on storage, pharmacovigilance, recall readiness, disposal arrangements — the current scenario flags these as collected but not enforced).
- **Preferred communication:** Email with attachments (her audit trail); secure portals where she has account-level credentials.

### Biggest Pain Points & Unmet Needs
- The current PDF collects GDP assurances as **paper signatures**. From an MHRA-defensibility point of view, that creates regulatory exposure — there is no enforced confirmation, no version stamping, and no machine-readable record.
- Her firm has a Quality Management System that handles internal change control; ImmForm is an external system she can't easily integrate with.
- Helpdesk email threads as the *only* record of an ImmForm registration would not survive a Chapter 4 GDP records-management inspection.
- She wants the ability to **register multiple staff** under one corporate envelope, with role-based access defined by her — not by re-keying a separate form per person.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Supply (the regulated link between manufacturer and end-user).
- **Organisations she interacts with:** MHRA Inspectorate, the company's customers (NHS trusts, OH providers, private clinics), the firm's logistics partner, UKHSA logistics if onward-distributing centrally procured stock, customs/HMRC for any imports, professional indemnity insurer.
- **Offline channels:** Physical inspection visits by MHRA; physical batch quarantine; paper temperature charts as a fallback to digital loggers.
- **Handoffs and dependencies:** Manufacturer → her firm under WDA(H) → end-user clinic or NHS trust. ImmForm is one of several ordering channels her end-users use.
- **Downstream impact on the public:** Failure in her supply chain = failure of public immunisation supply.

### Additional Context
- **A typical day:** 09:00 inspection-readiness review; 10:30 batch deviation triage; 12:30 lunch; 13:30 supplier qualification audit; 15:00 staff training oversight; 17:30 next-day inspection brief.
- **Technical proficiency:** High; deeply systems-literate but conservative — adopts new tools only when they survive regulatory scrutiny.
- **Digital access:** Company laptop, company mobile, hardware security token for QMS, dedicated inspection PC at the site.
- **Accessibility needs:** Hearing impairment in left ear since childhood (mild); prefers written confirmation of any phone agreement (this is also her regulatory instinct).
- **Decision-making authority:** Highest in her firm for GDP matters; her sign-off is a regulatory act.
- **Regulatory/compliance considerations:** Human Medicines Regulations 2012, EU/UK GDP, MHRA inspection regime, GPhC professional registration, GDPR/UK GDPR for personal data she holds on customer staff.
- **Workload:** 45–55 hours/week; intense pre-inspection.
- **Cultural/linguistic:** English first language; conservative tone; deeply procedural — by professional necessity.

---

# Workflow Stage B — Approvers

The two approver personas differ on **organisational size and governance**, which is the meaningful axis of variation. The journey is identical; the friction is not.

---

## 8. Dr Helen Vickers — GP Senior Partner (Small-Organisation Approver)

**Workflow stage:** B — Approvers
**Framework level:** Tactical
**Approver context:** Small partnership; signs ImmForm registrations for her own practice staff.

### Persona Name & Role
- **Name:** Dr Helen Vickers
- **Role:** Senior Partner and CQC-registered Manager, Cherrywood Medical Centre (same practice as Priya, persona 1)
- **Position level:** Practice partner — sets her own salary as part of profit-share; benchmarks against [BMA salaried GP guidance](https://www.bma.org.uk/pay-and-contracts/pay) but not directly on AfC.
- **Reporting structure:** Accountable to her co-partners; CQC-registered manager for the practice.
- **Background:** GMC-registered since 1996; MRCGP since 2001; partner at Cherrywood since 2008. Welsh, raised in Newport.

### Goals & Outcomes
- **Primary objective:** Run a safe, financially sustainable practice. Approving ImmForm accounts is one of forty governance touches in her week — she wants it done, done well, and over.
- **Personally tracks:** Her clinical sessions, partnership P&L, QOF, significant-event log.
- **Success metric:** Her staff have access to the systems they need to deliver the immunisation contract. She isn't tracking ImmForm specifically — she'd notice it if it broke.
- **Desired outcome:** *"I want a notification, on my phone, with the applicant's name, regulator number, and a clear yes/no button. Not a PDF in my email at 23:00."*

### Wants, Needs & Expectations
- **Daily workflow:** Two clinical sessions a day (~30 patients each); one administrative session twice a week; one half-day at home for paperwork.
- **Information she relies on:** Her own GMC number, NHS Smartcard, knowledge of who her staff are.
- **Technology expectations:** Comfortable with EMIS and NHS Mail on desktop; uses an iPad and NHS Mail mobile app; would adopt a one-tap mobile approval flow eagerly.
- **Support/training needs:** None. Expects the approval flow to **explain itself** in one screen.
- **Preferred communication:** SMS or mobile-app push for urgent things; NHS.net for everything else.

### Biggest Pain Points & Unmet Needs
- The current PDF assumes she will sit at a desktop and print, sign, and scan. She approves at midnight on her sofa.
- No structured time-bound ask: she has been asked informally over email by helpdesk staff, and she's not always sure whether it's a real approval request or a question.
- No partner-pooling: if she's on annual leave there's no clean way for another partner to approve in her stead.
- She has had to chase the helpdesk to *confirm* her own approval was received.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Prevention (oversight).
- **Organisations she interacts with:** PCN clinical directors, ICB primary care contract managers, CQC inspectors, the LMC, indemnity provider.
- **Offline channels:** wet-signature contracts, hand-signed prescription pads for controlled drugs.
- **Handoffs and dependencies:** Her approval gates Priya's submission; without it, the new joiner can't order vaccines.
- **Downstream impact on the public:** A delayed approval = a delayed nurse onboard = delayed vaccinations.

### Additional Context
- **A typical day:** Surgery 08:00–12:30, home visits 13:00–14:00, admin and meetings 14:00–17:30, evening paperwork 21:00–23:00 a few nights a week.
- **Technical proficiency:** Moderate-to-high; uses what she has to.
- **Digital access:** Practice desktop, NHS Smartcard, personal iPad and iPhone, home broadband.
- **Accessibility needs:** None disclosed; presbyopia (reading glasses) — appreciates large tap targets on mobile.
- **Decision-making authority:** Sole CQC-registered manager; can authorise any business decision unilaterally up to a partnership-agreed threshold.
- **Regulatory/compliance considerations:** GMC revalidation, CQC, GMS/PMS contract, partnership financial-disclosure rules.
- **Workload:** 9 clinical sessions plus management — ~50 hours.
- **Cultural/linguistic:** Welsh-English bilingual at home; English at work.

---

## 9. Marcus Doyle — NHS Trust Vaccination Service Lead (Large-Organisation Approver)

**Workflow stage:** B — Approvers
**Framework level:** Tactical
**Approver context:** Acute NHS trust; signs ImmForm registrations for many staff each year.

### Persona Name & Role
- **Name:** Marcus Doyle
- **Role:** Lead Pharmacist for Vaccination Services
- **Organisation:** The same large West Midlands acute trust as Chioma (persona 5).
- **Position level:** Agenda for Change Band 8b ([NHS Employers AfC 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526)).
- **Reporting structure:** Reports to the Chief Pharmacist; matrix into the trust's Medical Director for clinical-governance issues.
- **Background:** GPhC since 2005; MSc in Clinical Pharmacy; led the trust's COVID-19 mass-vaccination response 2021–2022; remained in the role through transition to seasonal cycle.

### Goals & Outcomes
- **Primary objective:** A defensible governance chain over every ImmForm account associated with the trust's delivery points — for both clinical-safety and MHRA GDP reasons.
- **Personally tracks:** Number of active ImmForm accounts; staff leavers whose accounts should have been revoked; audit evidence packs for the trust's annual internal-audit cycle.
- **Success metric:** The trust passes its internal pharmacy audit on the vaccination supply chain — no orphan accounts, no missing approvals.
- **Desired outcome:** *"I will approve thirty registrations in a flu campaign launch. I need them queued, batch-reviewable, and timestamped — not arriving as separate emails."*

### Wants, Needs & Expectations
- **Daily workflow:** Pharmacy huddle, governance meetings, mandatory training oversight, MHRA evidence pack maintenance, audit response.
- **Information he relies on:** His GPhC number, the trust's ODS codes, the trust's HR establishment register, the existing ImmForm account list.
- **Technology expectations:** Power user. Expects structured approval queues, dashboards, audit-export.
- **Support/training needs:** None on the operational side; needs UKHSA to publish what evidence the trust must retain on its side to align with MHRA expectations.
- **Preferred communication:** Trust SharePoint and Teams internally; NHS.net for external; an admin portal externally.

### Biggest Pain Points & Unmet Needs
- Approving via informal email leaves no audit artefact he can show internal auditors.
- He has no way to delegate; if he's on annual leave, registrations pile up.
- There's no way to **revoke** access during off-boarding through the same system — leavers' accounts linger.
- Helpdesk turnaround is slow at peak times; he can't prioritise high-impact onboardings.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Prevention and response.
- **Organisations he interacts with:** trust HR, internal audit, MHRA via the Chief Pharmacist, NHSE region.
- **Offline channels:** Paper-based investigation files for serious adverse events.
- **Handoffs and dependencies:** Approves → helpdesk creates → operational staff can order.
- **Downstream impact on the public:** Same as Chioma's — campaign launch slips if approvals slip.

### Additional Context
- **A typical day:** 08:00 huddle; 09:00–12:30 governance work; 13:00–14:00 lunch + email triage; 14:00–17:30 meetings and audit-pack preparation.
- **Technical proficiency:** High.
- **Digital access:** Trust laptop, Trust mobile, NHS Mail, NHS Smartcard.
- **Accessibility needs:** Long-sightedness — uses 125% zoom by default.
- **Decision-making authority:** Approves staff access on behalf of the trust; cannot create his own account independently of the trust governance chain.
- **Regulatory/compliance considerations:** MHRA GDP, GPhC, NHSE programme letters, trust IG/DSPT, ICO.
- **Workload:** 37.5–45 hours/week; peaks during autumn flu and COVID-19 cycle.
- **Cultural/linguistic:** Irish-British; English first language; no service-design constraint.

---

# Workflow Stage C — UKHSA Service Operations

---

## 10. Sarah Mitchell — ImmForm Helpdesk Agent

**Workflow stage:** C — UKHSA service operations
**Framework level:** Operational

### Persona Name & Role
- **Name:** Sarah Mitchell
- **Role:** Helpdesk Agent, ImmForm
- **Organisation:** Reached via `helpdesk@immform.org.uk` / `020 7183 8580` ([How to register: ImmForm helpsheet, UKHSA](https://www.gov.uk/government/publications/how-to-register-immform-helpsheet-8/how-to-register-immform-helpsheet)). ImmForm is operationally supported under UKHSA's Managed Service contract ([UKHSA ImmForm Managed Service requirements, Contracts Finder](https://www.contractsfinder.service.gov.uk/Notice/Attachment/66644d3e-8722-473c-b737-e8e90d0a1c22)); the named supplier in May 2026 is not confirmed in public records and is therefore not asserted here.
- **Position level:** Operational frontline.
- **Background:** Started as a contact-centre agent in financial services; moved to a public-sector helpdesk three years ago; learned ImmForm on the job.

### Goals & Outcomes
- **Primary objective:** Process inbound account registration requests accurately within SLA; close tickets cleanly.
- **Personally tracks:** Tickets per day; right-first-time rate; back-and-forth cycles per ticket; her own performance review metrics.
- **Success metric:** Ticket queue stays under control; no escalated complaints attributable to her work.
- **Desired outcome:** *"Anything that stops me from re-keying twenty fields from a PDF into a back-office screen is the win."*

### Wants, Needs & Expectations
- **Daily workflow:** Triage inbox, open ticket, validate the PDF against existing records, email the named manager for approval, wait, create the account, send credentials, close ticket.
- **Information she relies on:** ImmForm back-office user-management screens; the master organisation/ODS list; her own knowledge of common error patterns.
- **Technology expectations:** Modern, fast, keyboard-shortcut-friendly tools. Currently moves between an inbox, a ticketing system, and an admin console.
- **Support/training needs:** Better access to the *rules* — she often discovers a registration violates an unwritten rule (e.g. shared mailbox) only after the user has invested time.
- **Preferred communication:** Internal Teams chat; structured ticket trail externally.

### Biggest Pain Points & Unmet Needs
- **Manual re-keying** from PDFs introduces typos that cost her future tickets.
- Long email chains to chase approvers — she does the chasing.
- No structured way to record GDP assurances at the point of submission; she ends up storing scanned PDFs.
- Volume spikes (campaign launch, pandemic) overwhelm the team — no automated overflow.
- Repeated questions from the same applicants who can't see ticket progress.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Service enablement (supports prevention and response indirectly).
- **Organisations she interacts with:** Applicants (everyone in stage A); approvers (stage B); her own team lead (persona 11); UKHSA product team (persona 12); compliance team (persona 14) when something goes wrong.
- **Offline channels:** Phone overflow.
- **Handoffs and dependencies:** Applicant → her → approver → her → back-office system.
- **Downstream impact on the public:** Each ticket she clears = a clinician able to order vaccines.

### Additional Context
- **A typical day:** 09:00 inbox triage; 09:30–12:30 first ticket batch; 12:30 lunch; 13:30–17:30 second batch; team huddle Wednesdays.
- **Technical proficiency:** Moderate-high; experienced at navigating clunky back-office systems.
- **Digital access:** UKHSA / contractor-issued laptop; hybrid working (mostly home, one office day).
- **Accessibility needs:** Mild repetitive strain injury from years of keyboard work; uses a vertical mouse; benefits from keyboard-only navigation paths.
- **Decision-making authority:** Operational — decides when to escalate.
- **Regulatory/compliance considerations:** Handles personal data under UK GDPR; bound by UKHSA's data-handling policies.
- **Workload:** 37.5 hours/week; peaks correlate with campaign launches.
- **Cultural/linguistic:** English first language; works with applicants whose first language is not English.

---

## 11. James Patterson — ImmForm Helpdesk / Service Manager

**Workflow stage:** C — UKHSA service operations
**Framework level:** Tactical

### Persona Name & Role
- **Name:** James Patterson
- **Role:** Service Delivery Manager, ImmForm Managed Service
- **Organisation:** UKHSA Managed Service contractor working under the [ImmForm Managed Service requirements](https://www.contractsfinder.service.gov.uk/Notice/Attachment/66644d3e-8722-473c-b737-e8e90d0a1c22).
- **Position level:** Tactical; reports to the contractor's Account Director and is accountable to UKHSA's Service Owner.
- **Background:** ITIL-certified service manager; ten years in NHS-adjacent service delivery; previously ran the contact-centre for an NHS Digital service.

### Goals & Outcomes
- **Primary objective:** Meet contractual SLAs; reduce ticket volume; protect the service through volume spikes.
- **Personally tracks:** SLA performance, ticket volumes by category, first-contact-resolution rate, helpdesk team headcount, attrition.
- **Success metric:** Reduction in account-registration ticket volume by 70%+ (the project's explicit target); zero severity-1 incidents in a campaign window.
- **Desired outcome:** *"I want my helpdesk fighting exceptions, not data-entry. Self-service for the routine 80%."*

### Wants, Needs & Expectations
- **Daily workflow:** Stand-up, SLA dashboard review, exception triage, UKHSA service-owner meetings, contract reporting, capacity planning.
- **Information he relies on:** ITSM dashboards, contractual KPIs, headcount plans.
- **Technology expectations:** Modern observability and ticketing; APIs from any new onboarding service so he can ingest events into his ITSM tool.
- **Support/training needs:** Transparency on the new service's roadmap; advance warning of campaign spikes.
- **Preferred communication:** Weekly steerco with UKHSA; ServiceNow / Jira tickets internally.

### Biggest Pain Points & Unmet Needs
- Volume spikes during pandemic response are unforecastable and he can't surge staffing fast enough.
- Repetitive ticket types (account registration) eat capacity he'd rather use on incident resolution.
- No structured audit trail in the current process means he can't defend his team's compliance posture under audit.
- He's nervous about a new service shifting the *failure mode* to him without giving him telemetry.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Service enablement.
- **Organisations he interacts with:** UKHSA service owner, contractor's account director, his helpdesk team (Sarah, persona 10), UKHSA product team (persona 12), security and IG teams (persona 15).
- **Offline channels:** Phone bridge for major incidents.
- **Handoffs and dependencies:** Receives applicants from all of stage A; depends on UKHSA approving change.
- **Downstream impact on the public:** SLA performance ripples into vaccination programme delivery.

### Additional Context
- **A typical day:** 09:00 stand-up; 10:00 SLA review; 11:00 UKHSA call; 13:00 lunch; 14:00 exception triage; 15:00 1-1s; 16:30 contract reporting.
- **Technical proficiency:** High at the service-management level; relies on engineers for deep technical work.
- **Digital access:** Contractor laptop; hybrid working; on-call rota.
- **Accessibility needs:** None disclosed.
- **Decision-making authority:** Operational decisions within contract scope; escalates change to UKHSA.
- **Regulatory/compliance considerations:** ITIL, contract terms, MHRA GDP for the service his team operates.
- **Workload:** 40–45 hours/week; on-call coverage.
- **Cultural/linguistic:** English; UK-based.

---

# Workflow Stage D — UKHSA Product & Design

---

## 12. Amrita Chopra — ImmForm Product Manager

**Workflow stage:** D — UKHSA product & design
**Framework level:** Tactical

### Persona Name & Role
- **Name:** Amrita Chopra
- **Role:** Product Manager, ImmForm
- **Organisation:** UKHSA Data, Analytics & Surveillance / Digital, Data & Technology function — UKHSA is an executive agency of DHSC, governance documented at [UKHSA Our Governance](https://www.gov.uk/government/organisations/uk-health-security-agency/about/our-governance).
- **Position level:** UKHSA tactical product leadership (Civil Service Grade 7 equivalent).
- **Background:** Civil-service digital career; ran a discovery for a DHSC-adjacent service before joining UKHSA; Government Digital and Data Profession.

### Goals & Outcomes
- **Primary objective:** Deliver an ImmForm onboarding redesign that passes a [GDS Service Standard](https://www.gov.uk/service-manual/service-standard) assessment, meets [WCAG 2.2 AA](https://www.legislation.gov.uk/uksi/2018/952/contents) and produces an MHRA-defensible audit trail.
- **Personally tracks:** Discovery → Alpha → Private Beta → Public Beta assessment progress; user-research participant diversity; KPIs (time-to-account-creation, ticket deflection rate, audit-completeness).
- **Success metric:** Median time-to-account-creation < 1 working day for the straightforward path; 70%+ ticket reduction for registration; 100% audit-trail completeness.
- **Desired outcome:** *"A service so boring to use that no-one writes about it. That's how I know it worked."*

### Wants, Needs & Expectations
- **Daily workflow:** Roadmap grooming, sprint planning with engineering, user-research synthesis, stakeholder management across UKHSA, NHSE, MHRA compliance.
- **Information she relies on:** GDS Service Standard, WCAG 2.2, MHRA GN6, telemetry from the existing service, the user-research repository.
- **Technology expectations:** Jira, Miro, Figma, GitHub, analytics on the existing portal.
- **Support/training needs:** Reachable subject-matter experts in MHRA GDP and UKHSA logistics.
- **Preferred communication:** Slack/Teams within team; written status reports to seniors.

### Biggest Pain Points & Unmet Needs
- Fragmented user community (seven groups) makes user research expensive.
- The riskiest assumption (every applicant has a digitally-reachable approver) is hard to validate without going live.
- Integration with the ImmForm backend is a known unknown (project assumption 6).
- Compliance hand-offs are slow; she can't get a definitive answer on what specifically the audit trail must contain.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Service design.
- **Organisations she interacts with:** All of stage A representative users, helpdesk, compliance, MHRA (indirectly), NHSE digital, DHSC.
- **Offline channels:** Workshops, lab-based user research with assistive-tech users.
- **Handoffs and dependencies:** She is the integrator across product, design, engineering, and compliance.
- **Downstream impact on the public:** Faster, more compliant onboarding = faster vaccination delivery.

### Additional Context
- **A typical day:** 09:00 stand-up; 10:00 user-research synthesis; 11:00 1-1; 13:00 lunch; 14:00 stakeholder meetings; 16:00 backlog grooming; 17:00 wrap.
- **Technical proficiency:** High; ex-engineer.
- **Digital access:** UKHSA-issued laptop; hybrid working from London/Birmingham.
- **Accessibility needs:** Wears glasses for screen work; runs design reviews with NVDA and VoiceOver.
- **Decision-making authority:** Product roadmap; sign-off on user-facing language; cannot unilaterally commit UKHSA to compliance positions.
- **Regulatory/compliance considerations:** GDS, WCAG, UK GDPR, MHRA GDP, [Technology Code of Practice](https://www.gov.uk/guidance/the-technology-code-of-practice).
- **Workload:** 37–45 hours/week; spikes during service-assessment prep.
- **Cultural/linguistic:** British-Indian; English first language professionally.

---

## 13. Theo Brennan — Senior User Researcher / Service Designer

**Workflow stage:** D — UKHSA product & design
**Framework level:** Tactical

### Persona Name & Role
- **Name:** Theo Brennan
- **Role:** Senior User Researcher, ImmForm redesign
- **Organisation:** UKHSA Digital, Data & Technology, Government Digital and Data Profession.
- **Position level:** Senior Civil Service-equivalent specialist (SEO/G7).
- **Background:** Trained as a service designer in central government; previously at GDS itself; passionate advocate for [Point 1: Understand users and their needs](https://www.gov.uk/service-manual/service-standard).

### Goals & Outcomes
- **Primary objective:** Build evidence — qualitative and quantitative — across every user group in stage A and every supporting role in stages B–G.
- **Personally tracks:** Research sessions completed per group; participant diversity (digital confidence, accessibility, geography); insights logged in the research repository.
- **Success metric:** GDS assessor concludes Point 1 is met; downstream usability is high; assisted-digital users can complete the journey.
- **Desired outcome:** *"Every applicant — including the rural OH nurse on intermittent mobile signal and the dyslexic practice nurse — completes the journey first time."*

### Wants, Needs & Expectations
- **Daily workflow:** Recruit → interview → synthesise → present → iterate.
- **Information he relies on:** GDS user research guidance, UKHSA's existing helpdesk data, the [DfE accessibility persona set](https://design.education.gov.uk/design-system/personas-accessibility) as a calibration reference, the [Home Office accessibility personas](https://design.homeoffice.gov.uk/accessibility/personas).
- **Technology expectations:** Recording tools (with explicit consent), Lookback or equivalent, Miro for synthesis, EnjoyHQ / Dovetail-equivalent for repository.
- **Support/training needs:** Access to MHRA / GDP context experts so he can correctly interpret regulated-system constraints.
- **Preferred communication:** Whatever works for the participant — phone, video, in-person, written.

### Biggest Pain Points & Unmet Needs
- **Recruitment is hard.** Wholesalers (persona 7) and IHC pharmacists (persona 6) are tiny populations; he has to negotiate access carefully.
- Some applicants are reluctant to engage in research during campaign launch — exactly when their views are most valuable.
- Helpdesk telemetry is patchy — he doesn't know how many registrations fail and why.
- Accessibility recruitment costs more and takes longer; risk of squeezing it under delivery pressure.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Service design.
- **Organisations he interacts with:** All persona communities, accessibility recruitment partners, NHSE service designers, GDS / CDDO assessors.
- **Offline channels:** Field visits to GP practices, sexual health services, IHCs.
- **Handoffs and dependencies:** Feeds insights into Amrita (persona 12) and engineering team.
- **Downstream impact on the public:** Indirect; ensures the service works for everyone.

### Additional Context
- **A typical day:** Half on fieldwork or interviews, half on synthesis and write-up.
- **Technical proficiency:** High in research tools; moderate in engineering.
- **Digital access:** UKHSA laptop; hybrid; assistive-tech testing rig.
- **Accessibility needs:** Mild colour-blindness (deuteranomaly); informs his testing protocols.
- **Decision-making authority:** Research direction; not the product roadmap.
- **Regulatory/compliance considerations:** Ethics, UK GDPR for participant data, participant safeguarding.
- **Workload:** 37.5 hours/week.
- **Cultural/linguistic:** English; recruits with translators for non-English-fluent participants where appropriate.

---

# Workflow Stage E — Compliance & Governance

---

## 14. Dr Olu Babatunde — UKHSA GDP Compliance Lead / Responsible Person

**Workflow stage:** E — Compliance & governance
**Framework level:** Tactical / Strategic

### Persona Name & Role
- **Name:** Dr Olu Babatunde
- **Role:** UKHSA Responsible Person for centrally procured medicinal products / GDP Compliance Lead. UKHSA's central distribution chain is regulated under WDA(H) and EU/UK GDP ([MHRA Guidance Note 6](https://assets.publishing.service.gov.uk/media/67ea784e0678ace40a7f275c/GN_6_Brexit_changes-GDP.pdf)).
- **Position level:** Senior UKHSA professional in the Vaccines & Countermeasures function (Grade 7 / SCS-feeder).
- **Background:** GPhC-registered pharmacist; MHRA-named RP for a national-scale supply chain; postgraduate quality-management qualifications.

### Goals & Outcomes
- **Primary objective:** Ensure the ImmForm onboarding redesign produces an audit trail and assurance evidence acceptable to an MHRA inspection of UKHSA's wholesale operation.
- **Personally tracks:** MHRA inspection findings, CAPA closure, deviation reports, audit-trail completeness on every regulated workflow.
- **Success metric:** Clean MHRA inspection; UKHSA's wholesale operation remains in good standing.
- **Desired outcome:** *"Every GDP assurance the applicant ticks must be timestamped, attributable, and exportable. Anything less and we're back in the inspection finding."*

### Wants, Needs & Expectations
- **Daily workflow:** Quality oversight, change control, supplier audits, MHRA correspondence.
- **Information she/he relies on:** UKHSA QMS, the EU/UK GDP guidelines, MHRA inspection feedback, the project team's design artefacts.
- **Technology expectations:** Enforced confirmation (no nullable assurance fields), version-stamped records, exportable JSON/PDF evidence packs, role-based access control with separation of duties.
- **Support/training needs:** Briefings on the new design before each phase-gate; participation in design reviews from discovery onward.
- **Preferred communication:** Email and document review with formal sign-off; structured workshops for design.

### Biggest Pain Points & Unmet Needs
- Today's email-thread audit trail is **non-compliant** for an MHRA Chapter 4 records-management view ([MHRA GN6](https://assets.publishing.service.gov.uk/media/67ea784e0678ace40a7f275c/GN_6_Brexit_changes-GDP.pdf)).
- Paper assurances on the existing PDF give him no defence in an inspection.
- He is often pulled into design at late stages; he would rather be a discovery participant.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Supply assurance.
- **Organisations he interacts with:** MHRA, UKHSA logistics contractor, wholesalers (persona 7), the product team (persona 12), DHSC.
- **Offline channels:** MHRA on-site inspections.
- **Handoffs and dependencies:** His sign-off is a gate for go-live.
- **Downstream impact on the public:** A failed inspection could lead to product hold, with knock-on effect on programme supply.

### Additional Context
- **A typical day:** Quality reviews, MHRA correspondence, deviation triage, supplier audits.
- **Technical proficiency:** Moderate digital; deep regulatory.
- **Digital access:** UKHSA laptop; secure access to QMS.
- **Accessibility needs:** None disclosed.
- **Decision-making authority:** GDP sign-off on UKHSA-operated systems within wholesale scope.
- **Regulatory/compliance considerations:** Personal regulatory liability under HMR 2012; GDP; GPhC.
- **Workload:** 40–50 hours/week.
- **Cultural/linguistic:** Nigerian-British; English first language professionally.

---

## 15. Rachel Goldstein — UKHSA Information Governance & Data Protection Officer

**Workflow stage:** E — Compliance & governance
**Framework level:** Tactical

### Persona Name & Role
- **Name:** Rachel Goldstein
- **Role:** Data Protection Officer (DPO) and Head of Information Governance, UKHSA
- **Position level:** Statutory DPO under UK GDPR.
- **Background:** Solicitor (non-practising), former NHS IG manager, IAPP CIPP/E and CIPM-certified.

### Goals & Outcomes
- **Primary objective:** Ensure the redesign meets UK GDPR / DPA 2018, NHS Data Security and Protection Toolkit standards, and UKHSA's information-asset register obligations.
- **Personally tracks:** DPIAs completed; breach incidents; subject-rights requests served; DSPT status.
- **Success metric:** No ICO enforcement; DPIA accepted; no breaches arising from the new service.
- **Desired outcome:** *"Lawful, fair, transparent processing — and a privacy notice the applicant actually reads because it's plain English."*

### Wants, Needs & Expectations
- **Daily workflow:** DPIA review, supplier due diligence, breach response, ROPA maintenance, Subject Access Request triage.
- **Information she relies on:** ICO guidance, NHS DSPT, the project's DPIA, processor contracts.
- **Technology expectations:** Strong audit logging, role-based access, retention enforcement, secure transit (TLS), minimum-necessary data.
- **Support/training needs:** Briefings on the new design's data flows; sight of contractor processing agreements.
- **Preferred communication:** Email with documents; structured DPIA workshops.

### Biggest Pain Points & Unmet Needs
- The PDF-and-email workflow scatters personal data across mailboxes.
- No structured retention policy on the email artefacts that today **are** the audit trail.
- Applicants are asked for personal data (e.g. personal mobile numbers) without clear lawful basis.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Governance.
- **Organisations she interacts with:** ICO, UKHSA Senior Information Risk Owner (SIRO), the product team, the helpdesk, DHSC IG, ICB IG counterparts.
- **Offline channels:** Privacy notice on paper for assisted-digital users.
- **Handoffs and dependencies:** DPIA sign-off gates go-live.
- **Downstream impact on the public:** Applicants' personal data — though they are professionals, their data deserves the same care as patient data.

### Additional Context
- **A typical day:** DPIA reviews, training delivery, breach response coordination, contracts.
- **Technical proficiency:** Moderate-high.
- **Digital access:** UKHSA laptop.
- **Accessibility needs:** Hard-of-hearing in noisy environments; uses Teams live captions in larger meetings.
- **Decision-making authority:** DPIA advice (statutory); not a veto, but escalates to SIRO.
- **Regulatory/compliance considerations:** UK GDPR, DPA 2018, NHS DSPT, NIS Regulations.
- **Workload:** 40–45 hours/week.
- **Cultural/linguistic:** Anglo-Jewish; English first language professionally.

---

# Workflow Stage F — UKHSA Strategic

---

## 16. Dr Catriona Lewis — Head of Immunisation & Vaccine Preventable Diseases

**Workflow stage:** F — UKHSA strategic
**Framework level:** Strategic

### Persona Name & Role
- **Name:** Dr Catriona Lewis
- **Role:** Head of Immunisation & Vaccine Preventable Diseases, UKHSA (Senior Civil Service equivalent). UKHSA governance documented at [UKHSA Our Governance](https://www.gov.uk/government/organisations/uk-health-security-agency/about/our-governance).
- **Position level:** Strategic.
- **Background:** Public health physician (FFPH), consultant in communicable disease control; published author on immunisation coverage; previously at Public Health England.

### Goals & Outcomes
- **Primary objective:** Sustain and improve immunisation coverage across all national programmes; protect outbreak response capability.
- **Personally tracks:** Programme coverage rates (e.g. MMR, HPV, flu, MenACWY); JCVI policy outputs; UKHSA's relationship with NHSE delegated commissioning ([NHS England delegation paper](https://www.england.nhs.uk/long-read/delegation-proposals-for-vaccination-and-screening/)).
- **Success metric:** Coverage rates rise; no avoidable outbreaks; new programmes deploy on time.
- **Desired outcome:** *"ImmForm onboarding should never be the bottleneck that delays a national programme launch."*

### Wants, Needs & Expectations
- **Daily workflow:** JCVI input, programme oversight, ministerial briefings, sector engagement.
- **Information she relies on:** Coverage data, programme delivery reports, JCVI minutes, NHSE plans.
- **Technology expectations:** Briefings in plain English; data on a single page.
- **Support/training needs:** Trust in operational delivery so she can focus on strategy.
- **Preferred communication:** Briefing notes; weekly senior leadership meeting; ministerial submission cycle.

### Biggest Pain Points & Unmet Needs
- Surprise bottlenecks: she does not want to discover that pandemic-onboarding delays affected the national response from a Cabinet Office post-incident review.
- Limited visibility into the operational metrics of the supporting digital services.
- Co-ordinating across UKHSA, NHSE, DHSC, MHRA — and now ICBs — for any programme launch.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Strategy across prevention, response and recovery.
- **Organisations she interacts with:** JCVI, NHSE national vaccination team (persona 17), DHSC policy team (persona 19), MHRA, devolved-administration counterparts, WHO Europe and ECDC.
- **Offline channels:** Ministerial submissions; in-person committees.
- **Handoffs and dependencies:** Sets policy direction that the rest of UKHSA delivers against.
- **Downstream impact on the public:** Coverage rates translate directly into morbidity / mortality.

### Additional Context
- **A typical day:** Meetings 09:00–17:00, with briefings to read before and after.
- **Technical proficiency:** Moderate; relies on technical advisers.
- **Digital access:** UKHSA laptop; secure mobile; chauffeur-driven car for Whitehall meetings.
- **Accessibility needs:** Presbyopia.
- **Decision-making authority:** Strategic prioritisation within her remit; commits UKHSA at SCS level.
- **Regulatory/compliance considerations:** Accountable through UKHSA governance to the Permanent Secretary and Secretary of State.
- **Workload:** 55–65 hours/week.
- **Cultural/linguistic:** Scottish; English first language.

---

# Workflow Stage G — External Partners

---

## 17. Steve Mukherjee — NHS England Regional Vaccinations Lead

**Workflow stage:** G — External partners
**Framework level:** Tactical / Strategic

### Persona Name & Role
- **Name:** Steve Mukherjee
- **Role:** Regional Director, Vaccinations & Screening — Midlands. NHS England's delegation of public-health commissioning for vaccinations and screening to ICBs is documented in [the delegation proposals paper](https://www.england.nhs.uk/long-read/delegation-proposals-for-vaccination-and-screening/).
- **Position level:** NHS England senior leadership equivalent.
- **Background:** Public health background; ran the Midlands COVID-19 vaccination response; now manages the transition of commissioning into ICBs.

### Goals & Outcomes
- **Primary objective:** Achieve commissioned coverage targets across his region; manage the ICB delegation transition smoothly.
- **Personally tracks:** Regional coverage; provider performance; ICB readiness; complaints.
- **Success metric:** No coverage gap in his region; smooth campaign launches.
- **Desired outcome:** *"My providers should be able to onboard their staff faster than their HR department issues an NHS Smartcard. If they can't, I lose campaign days."*

### Wants, Needs & Expectations
- **Daily workflow:** Provider performance review, ICB liaison, regional incident response, ministerial briefings.
- **Information he relies on:** Regional coverage data; provider returns; ImmForm coverage data ([ImmForm About Us](https://portal.immform.ukhsa.gov.uk/Footer-Pages/About-Us)).
- **Technology expectations:** Dashboards over emails.
- **Support/training needs:** None; wants UKHSA to publish metrics he can plan against.
- **Preferred communication:** NHS.net, Teams, weekly steerco.

### Biggest Pain Points & Unmet Needs
- Onboarding bottlenecks cascade through commissioned providers and into his coverage figures.
- No regional-level view of ImmForm onboarding throughput.
- Variable digital maturity across providers in his region.

### Wider Journey & Touchpoints
- **Where he sits in the public health lifecycle:** Commissioning and delivery oversight.
- **Organisations he interacts with:** ICBs, primary care providers, NHS trusts, UKHSA region, DHSC.
- **Offline channels:** Site visits; regional clinical reference groups.
- **Handoffs and dependencies:** UKHSA → his region → ICB → provider → patient.
- **Downstream impact on the public:** Regional coverage outcomes.

### Additional Context
- **A typical day:** Back-to-back meetings; reading on the train into Birmingham.
- **Technical proficiency:** Moderate-high.
- **Digital access:** NHSE laptop, NHS.net, NHS Mail mobile.
- **Accessibility needs:** None disclosed.
- **Decision-making authority:** Significant regional remit.
- **Regulatory/compliance considerations:** NHSE governance; ICB statutory duties under [Health and Care Act 2022](https://www.legislation.gov.uk/ukpga/2022/31/contents/enacted).
- **Workload:** 55+ hours/week.
- **Cultural/linguistic:** British-Bengali heritage; English first language professionally.

---

## 18. Dr Aisha Bello — Director of Public Health, Local Authority

**Workflow stage:** G — External partners
**Framework level:** Strategic

### Persona Name & Role
- **Name:** Dr Aisha Bello
- **Role:** Director of Public Health, a London Borough Council. The DPH is a statutory chief officer appointed jointly by the local authority and the Secretary of State under [section 30 of the Health and Social Care Act 2012](https://www.legislation.gov.uk/ukpga/2012/7/notes/division/5/1/4/2), described in [DHSC's role of the DPH guidance](https://www.gov.uk/government/publications/role-of-the-director-of-public-health-in-local-authorities/directors-of-public-health-in-local-government-roles-responsibilities-and-context).
- **Position level:** Strategic — statutory chief officer.
- **Background:** Public health consultant (FFPH); local authority career; led the borough's COVID-19 response.

### Goals & Outcomes
- **Primary objective:** Protect and improve the health of her local population, including responsibility for the local authority's contribution to health protection.
- **Personally tracks:** Local coverage, inequalities, outbreak metrics, sexual health service commissioning performance (LAs are the statutory commissioners of sexual health services).
- **Success metric:** Coverage rises across deprivation deciles; outbreaks contained early.
- **Desired outcome:** *"My commissioned sexual health service should be able to get GBMSM and Mpox vaccines into the arms of high-risk service users without an onboarding bottleneck for new staff."*

### Wants, Needs & Expectations
- **Daily workflow:** Council committee, system meetings (ICS/ICB), UKHSA Health Protection Team, commissioning oversight.
- **Information she relies on:** ImmForm coverage data, GUMCAD returns ([UKHSA GUMCAD](https://www.gov.uk/guidance/gumcad-sti-surveillance-system)), local public-health intelligence.
- **Technology expectations:** Dashboards; not a transactional user of ImmForm but a heavy user of its outputs.
- **Support/training needs:** None.
- **Preferred communication:** Council email; UKHSA Health Protection Team direct line.

### Biggest Pain Points & Unmet Needs
- Variable provider digital maturity in her borough — small charities, OH providers, hospices.
- Onboarding bottlenecks in commissioned services degrade her commissioning outcomes.
- Information she needs is fragmented across UKHSA, NHSE, and her own borough's intelligence team.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Strategy and statutory health protection at local level.
- **Organisations she interacts with:** UKHSA Health Protection Team, the borough's commissioned providers, ICB, NHSE, third-sector partners, schools.
- **Offline channels:** Council chamber, public meetings, school visits.
- **Handoffs and dependencies:** UKHSA → her DPH function → commissioned providers → residents.
- **Downstream impact on the public:** Her residents — including high-deprivation cohorts — are directly affected.

### Additional Context
- **A typical day:** Council meeting in the morning; system meeting at lunchtime; commissioning review in the afternoon; ministerial-correspondence reading on the Tube home.
- **Technical proficiency:** Moderate.
- **Digital access:** Council laptop; secure mobile.
- **Accessibility needs:** None disclosed.
- **Decision-making authority:** Statutory chief officer.
- **Regulatory/compliance considerations:** [Health and Care Act 2022](https://www.legislation.gov.uk/ukpga/2022/31/contents/enacted), s.73A NHS Act 2006, ICO.
- **Workload:** 50–60 hours/week.
- **Cultural/linguistic:** Yoruba-English bilingual; English first language professionally; her borough includes large communities for whom English is not a first language and digital exclusion is a real issue.

---

## 19. Dr Charlotte Penrose — DHSC Immunisation Policy Lead

**Workflow stage:** G — External partners
**Framework level:** Strategic

### Persona Name & Role
- **Name:** Dr Charlotte Penrose
- **Role:** Deputy Director, immunisation policy portfolio, Department of Health and Social Care. DHSC employed 3,540 FTE staff at 30 June 2024 ([DHSC About us](https://www.gov.uk/government/organisations/department-of-health-and-social-care/about)); ministerial leadership for prevention and public health is held by the Parliamentary Under-Secretary of State for Public Health and Prevention ([DHSC organisation page](https://www.gov.uk/government/organisations/department-of-health-and-social-care)).
- **Position level:** Senior Civil Service (Deputy Director, SCS Pay Band 1).
- **Background:** Career civil servant; cross-departmental policy career; led the immunisation-policy response post-COVID.

### Goals & Outcomes
- **Primary objective:** Ensure the policy environment supports UKHSA and NHSE in delivering immunisation programmes.
- **Personally tracks:** Ministerial commitments, programme coverage, public sentiment.
- **Success metric:** Policy commitments delivered; ministers can answer parliamentary questions confidently.
- **Desired outcome:** *"I should not be reading about an ImmForm onboarding bottleneck in a Times Health Commission feature. It should be fixed before it becomes news."*

### Wants, Needs & Expectations
- **Daily workflow:** Ministerial submissions, cross-government coordination, JCVI liaison, parliamentary business.
- **Information she relies on:** UKHSA briefings, NHSE briefings, JCVI minutes, public-engagement intelligence.
- **Technology expectations:** Read briefings; she is not a transactional user.
- **Support/training needs:** None.
- **Preferred communication:** Cleared submission notes via her private office.

### Biggest Pain Points & Unmet Needs
- Operational delivery failures landing as ministerial reputational issues.
- Limited visibility into the digital plumbing — she relies on UKHSA to flag risk.

### Wider Journey & Touchpoints
- **Where she sits in the public health lifecycle:** Policy oversight.
- **Organisations she interacts with:** UKHSA leadership (Catriona, persona 16), NHSE leadership, Cabinet Office (during emergencies), Treasury (for funding), devolved administrations.
- **Offline channels:** Ministerial meetings; parliamentary committees.
- **Handoffs and dependencies:** Approves policy stance; depends on UKHSA for delivery.
- **Downstream impact on the public:** Mediated through UKHSA, NHSE and local authority delivery.

### Additional Context
- **A typical day:** Submissions reviewed, ministerial meetings, cross-Whitehall calls.
- **Technical proficiency:** Moderate.
- **Digital access:** DHSC laptop; secure mobile.
- **Accessibility needs:** None disclosed.
- **Decision-making authority:** Significant within the policy portfolio.
- **Regulatory/compliance considerations:** Ministerial Code; Civil Service Code.
- **Workload:** 55–65 hours/week; spikes around fiscal events.
- **Cultural/linguistic:** English first language; Whitehall idiom.

---

# Appendix A — Persona Coverage by Workflow Stage & Framework Level

| Framework level | Stage A: Applicants | Stage B: Approvers | Stage C: Service Ops | Stage D: Product & Design | Stage E: Compliance | Stage F: UKHSA Strategy | Stage G: External Partners |
|---|---|---|---|---|---|---|---|
| **Operational** | Priya (1), Daniel (2), Yusuf (3), Margaret (4), Iain (6) | — | Sarah (10) | — | — | — | — |
| **Tactical** | Chioma (5), Eleanor (7) | Helen (8), Marcus (9) | James (11) | Amrita (12), Theo (13) | Olu (14), Rachel (15) | — | Steve (17) |
| **Strategic** | — | — | — | — | Olu (14, dual) | Catriona (16) | Aisha (18), Charlotte (19) |

# Appendix B — Accessibility & Inclusion Coverage

| Persona | Accessibility / inclusion consideration |
|---|---|
| Priya (1) | English as additional language; reads in second language; works in a non-statutory benchmark org (private partnership) |
| Daniel (2) | Mild dyslexia; benefits from plain language and confirmation re-reads |
| Yusuf (3) | None for himself, but acutely aware of patient-facing accessibility — service-user-facing |
| Margaret (4) | Presbyopia; intermittent rural mobile signal; private-sector context |
| Chioma (5) | None disclosed; works at high scale and pace |
| Iain (6) | Long screen time, generous typography preference |
| Eleanor (7) | Mild hearing impairment; written-record professional instinct |
| Helen (8) | Presbyopia; mobile-first approval needs |
| Marcus (9) | Long-sightedness; uses 125% zoom |
| Sarah (10) | Mild RSI; keyboard-first navigation |
| Amrita (12) | Glasses; tests with screen readers |
| Theo (13) | Mild deuteranomaly colour-blindness |
| Rachel (15) | Hard-of-hearing in noisy environments; live-captions user |
| Aisha (18) | Bilingual; serves multilingual / digitally excluded constituents |

Accessibility is **distributed** across personas, in line with the principle that disability and access needs are not a separate user type but a property of every user, per [GOV.UK accessibility requirements guidance](https://www.gov.uk/guidance/accessibility-requirements-for-public-sector-websites-and-apps).

# Appendix C — Primary Sources

1. [How to register: ImmForm helpsheet, UKHSA, 25 Nov 2024](https://www.gov.uk/government/publications/how-to-register-immform-helpsheet-8/how-to-register-immform-helpsheet)
2. [ImmForm Registration page](https://portal.immform.ukhsa.gov.uk/Registration)
3. [ImmForm About Us](https://portal.immform.ukhsa.gov.uk/Footer-Pages/About-Us)
4. [UKHSA ImmForm Managed Service requirements, Contracts Finder](https://www.contractsfinder.service.gov.uk/Notice/Attachment/66644d3e-8722-473c-b737-e8e90d0a1c22)
5. [NHS England South: ImmForm Manual Input Guidance 2025/26](https://www.england.nhs.uk/south/wp-content/uploads/sites/8/2025/09/ImmForm-Manual-Input-Guidance-2526.docx)
6. [MHRA Guidance Note 6 — Post-Brexit GDP changes](https://assets.publishing.service.gov.uk/media/67ea784e0678ace40a7f275c/GN_6_Brexit_changes-GDP.pdf)
7. [MHRA: apply for manufacturer or wholesaler of medicines licences](https://www.gov.uk/guidance/apply-for-manufacturer-or-wholesaler-of-medicines-licences)
8. [GDS Service Standard](https://www.gov.uk/service-manual/service-standard)
9. [Public Sector Bodies Accessibility Regulations 2018 (SI 2018/952)](https://www.legislation.gov.uk/uksi/2018/952/contents)
10. [GOV.UK Accessibility requirements for public sector websites and apps](https://www.gov.uk/guidance/accessibility-requirements-for-public-sector-websites-and-apps)
11. [UKHSA National Minimum Standards for immunisation training, 2025](https://www.gov.uk/government/publications/national-minimum-standards-and-core-curriculum-for-immunisation-training-for-registered-healthcare-practitioners)
12. [HPV vaccination for GBMSM, UKHSA Sept 2023](https://www.gov.uk/government/publications/hpv-vaccination-for-msm-posters-and-leaflets/information-on-hpv-for-gbmsm-from-september-2023)
13. [NHSE-UKHSA HPV GBMSM PGD v5.0, Oct 2025](https://www.england.nhs.uk/london/wp-content/uploads/sites/8/2025/10/NHSE-UKHSA-HPV-GBMSM-PGD-v5.0.pdf)
14. [Mpox vaccination information for healthcare practitioners, UKHSA](https://www.gov.uk/government/publications/vaccination-against-mpox-information-for-healthcare-practitioners/mpox-vaccination-information-for-healthcare-practitioners)
15. [Rabies and Immunoglobulin Service (RIgS), UKHSA](https://www.gov.uk/government/publications/immunoglobulin-when-to-use/rabies-and-immunoglobulin-service-rigs)
16. [NHS England — Delegation proposals for vaccination and screening](https://www.england.nhs.uk/long-read/delegation-proposals-for-vaccination-and-screening/)
17. [Role of the Director of Public Health in local authorities, DHSC](https://www.gov.uk/government/publications/role-of-the-director-of-public-health-in-local-authorities/directors-of-public-health-in-local-government-roles-responsibilities-and-context)
18. [Health and Social Care Act 2012 — explanatory notes, s.30](https://www.legislation.gov.uk/ukpga/2012/7/notes/division/5/1/4/2)
19. [Health and Care Act 2022](https://www.legislation.gov.uk/ukpga/2022/31/contents/enacted)
20. [The Topol Review (2019)](https://rpsg.org.uk/wp-content/uploads/2020/04/The-Topal-Review-2019.pdf)
21. [NHS Health Careers — Practice Manager](https://www.healthcareers.nhs.uk/explore-roles/management/roles-management/practice-manager)
22. [NHS Employers — Pay scales 2025/26](https://www.nhsemployers.org/articles/pay-scales-202526)
23. [NHS Digital — Patients Registered at a GP Practice, December 2025](https://digital.nhs.uk/data-and-information/publications/statistical/patients-registered-at-a-gp-practice/december-2025)
24. [RCGP — Number of GP practices in England, Sept 2025](https://www.rcgp.org.uk/News/Number-of-GP-practices)
25. [Mohammed et al., PLoS ONE 2026 — Enhancing GUMCAD STI surveillance](https://researchportal.ukhsa.gov.uk/en/publications/enhancing-surveillance-of-sexually-transmitted-infections-in-engl/)
26. [UKHSA GUMCAD STI Surveillance System](https://www.gov.uk/guidance/gumcad-sti-surveillance-system)
27. [Pharmacy Business / NHSBSA — England community pharmacy count, April 2025](https://www.pharmacy.biz/england-community-pharmacy-closures-nhs-funding-gap-ownerhip-trends/)
28. [DHSC — Our governance](https://www.gov.uk/government/organisations/department-of-health-and-social-care/about/our-governance)
29. [DHSC — About us](https://www.gov.uk/government/organisations/department-of-health-and-social-care/about)
30. [DHSC organisation page (ministers and senior leadership)](https://www.gov.uk/government/organisations/department-of-health-and-social-care)
31. [UKHSA — Our Governance](https://www.gov.uk/government/organisations/uk-health-security-agency/about/our-governance)
32. [NHS England — GP practices improve access, 2 June 2025](https://www.england.nhs.uk/2025/06/gp-practices-improve-access-embracing-technology-increasing-appointments/)
33. [Technology Code of Practice](https://www.gov.uk/guidance/the-technology-code-of-practice)

# Appendix D — Facts Deliberately Not Asserted

These items could not be verified to a primary source during research and have been kept out of the persona narratives to avoid fabrication:

1. The current named ImmForm Managed Service supplier.
2. The exact number of ImmForm registered users.
3. The current named NHS England National Director responsible for vaccination and screening (the historical reference to Steve Russell in [NHSE delegation paper](https://www.england.nhs.uk/long-read/delegation-proposals-for-vaccination-and-screening/) may be out of date).
4. The named DHSC immunisation policy team in the current departmental structure.
5. Quantitative claims about ImmForm helpdesk volumes — not in the public domain.
6. The total count of designated Immunoglobulin Holding Centres.

The redesign discovery should resolve these privately with UKHSA, NHSE, and the contract holder.

---

*End of document.*
