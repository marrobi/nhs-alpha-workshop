# ImmForm User Onboarding Scenario

## Scenario Overview

ImmForm is a nationally critical digital platform supporting immunisation programme management, vaccine supply chain operations, and pandemic flu back-office systems for the UK Health Security Agency (UKHSA), NHS England, and the wider public health system. The platform serves thousands of users across health and local authority settings who require access to manage immunisation programmes, coordinate vaccine supply chains, and maintain GDP-compliant pharmaceutical operations.

Currently, any health professional or local authority staff member who needs an ImmForm account must complete a PDF form (ImmForm Account Change/Revalidation Form, V2.6) and email it to a central helpdesk. A helpdesk agent manually reviews the form, validates the information against existing system records, contacts an authorising manager informally via email to confirm approval, then manually creates the account in the ImmForm backend. This process takes up to five working days. The current form handles both new registrations and account changes; this project focuses solely on new user registration.

The workflow involves multiple manual handoffs: the applicant completes the form, the helpdesk agent re-keys data and performs validation checks, the approving manager responds to an ad-hoc email request, and the helpdesk agent finalises the account creation. Throughout this process, there is no structured tracking, no automated validation, and no machine-readable audit trail. The entire record of a registration exists as an email thread, which does not meet the audit requirements for MHRA Good Distribution Practice (GDP) compliant systems.

## Problem Statement

**How might we enable health professionals and local authority staff to obtain ImmForm accounts quickly and securely, with appropriate approval and a full audit trail, without requiring direct helpdesk intervention for every request?**

People who need to access ImmForm to support immunisation programmes and vaccine supply operations currently face significant delays and administrative burden. The manual process creates a bottleneck that becomes critical during pandemic response or new programme rollouts, when onboarding volume spikes and the helpdesk cannot scale to meet demand.

### Why it matters

- **Service continuity risk**: Delays in account provisioning can prevent timely access to critical vaccine supply chain information and immunisation programme data during public health emergencies
- **Helpdesk resource burden**: Every registration requires manual data entry, validation, and coordination, consuming helpdesk capacity that could be directed to more complex support issues
- **Compliance risk**: Paper-based GDP assurances (storage capabilities, pharmacovigilance processes, product recall readiness, disposal arrangements) collected without digital record or enforced confirmation create regulatory exposure for a GDP-compliant system
- **Audit trail gaps**: Email-based approval workflows provide no structured, timestamped, machine-readable record of who requested access, who approved it, and when — failing to meet MHRA audit requirements
- **User frustration**: Applicants wait up to five working days with no visibility into progress; validation errors discovered late in the process extend timelines further
- **Data quality issues**: Manual re-keying from PDF forms introduces transcription errors; applicants are asked to provide information (organisation codes, ImmForm account numbers) that already exists in the system

### Current cost

- Average processing time: **up to 5 working days** per registration
- Manual validation effort: helpdesk agents must check organisation codes, ImmForm account numbers, email addresses against existing records
- Rework loops: validation errors discovered after submission require back-and-forth email exchanges, extending processing time
- No scalability: volume spikes during pandemic response create immediate bottlenecks with no automated overflow capacity
- No reusability: the process is specific to this form and cannot be extended to future onboarding scenarios without rebuilding from scratch

### Success looks like

- New users can complete registration without helpdesk intervention for straightforward cases (valid organisation, valid approver)
- Approving managers receive structured approval requests with clear time boundaries
- Automated validation catches errors (invalid organisation codes, shared mailboxes, missing GDP assurances) at the point of entry, not after submission
- A complete, timestamped, machine-readable audit trail exists for every registration, meeting MHRA GDP audit requirements
- GDP assurances are collected digitally with enforced confirmation, creating a defensible compliance record
- Helpdesk agents focus on exceptions and complex cases, not routine data entry
- The system is reusable and extensible for future onboarding workflows beyond new user registration

**Measurable outcomes:**
- Reduction in median time-to-account-creation from 5 days to 2 days day for straightforward cases
- Reduction in helpdesk tickets related to account registration by 70%+
- 100% of registrations have a complete, timestamped audit trail meeting MHRA requirements
- Zero manual data re-keying from PDF forms

## Assumptions

1. **Approving managers have existing ImmForm accounts** — we assume that every new user has an identifiable approving manager who is already registered in ImmForm and can be notified digitally. **(Riskiest — test this first.)**
2. **Organisations are already registered in ImmForm** — we assume that new users are joining existing organisations already known to the system, not creating entirely new organisations. **(High risk — validate in discovery.)**
3. **Users have access to email** — we assume applicants can receive email notifications and approvers can respond to email-based approval requests.
4. **GDP assurances remain static** — we assume the list of GDP assurances (storage, pharmacovigilance, recall readiness, disposal) does not change frequently and can be embedded in the digital form. **(Medium risk.)**
5. **Account changes are out of scope** — we assume the existing PDF form will continue to serve account changes/revalidation for now, and this project covers new registrations only.
6. **Integration with existing ImmForm user management backend is possible** — we assume the ImmForm backend has an API or integration point that allows automated account creation once approval is granted. **(High risk — confirm technical feasibility early.)**
7. **Helpdesk will still handle exceptions** — we assume some registrations will require manual intervention (e.g. unrecognised organisation, no approving manager) and the helpdesk will remain the escalation path.
8. **MHRA GDP audit requirements are well-defined** — we assume we can identify and meet the specific audit trail requirements for a GDP-compliant system through consultation with the compliance team. **(Medium risk.)**

## Out of Scope

- **Account changes and revalidation** — this project covers new user registration only. Changes to existing accounts (role changes, organisation changes, access revisions) remain on the existing PDF form process.
- **Organisation onboarding** — we are not creating a workflow for registering entirely new organisations into ImmForm; we assume users are joining existing organisations.
- **Approver management** — we are not building a system to define, manage, or delegate approver roles. We assume approving managers are already identified within organisations.
- **Training and induction** — we are not replacing or digitising any post-registration training, onboarding materials, or induction processes. This project ends at account creation.
- **Password reset and account recovery** — we are not changing the existing password reset or account recovery workflows.
- **Integration with external identity providers** — we are not implementing single sign-on (SSO) or integration with NHS Identity or other external authentication systems. Users will continue to have ImmForm-specific credentials.
- **Vaccine ordering or clinical workflows** — this project is purely about user onboarding. We are not changing any vaccine supply chain, ordering, or immunisation programme workflows within ImmForm itself.
- **Helpdesk ticketing system replacement** — we are not replacing the existing helpdesk system; we are reducing the volume of routine registration tickets, but complex cases will still flow through the existing helpdesk process.
- **Decommissioning the PDF form entirely** — the PDF form will remain available for account changes and as a fallback option. We are not retiring it completely.


