# Story 032 — GDP-Grade Registration Confirmation

**Journey**: Wholesaler procurement registration (`journey-wholesaler-procurement-registration.md`)
**Priority**: 4 (Wave 4 — Persona and Context Variants)

## User Story

As Marcus (Procurement Compliance Lead at a pharmaceutical wholesaler),
I need the confirmation page to include explicit GDP compliance language and a printable confirmation reference,
So that I can file the registration confirmation as part of my GDP audit trail and demonstrate to MHRA inspectors that the registration was completed through the approved process.

## Acceptance Criteria

### Functional
- [ ] Given my registration is submitted successfully, when the confirmation page is displayed, then it includes: the registration reference (CorrelationId in `RG-XXXXXXXX` format), a statement confirming the registration was submitted via the UKHSA ImmForm registration service, and the submission timestamp
- [ ] Given the confirmation page is displayed, then it includes GDP compliance text: "This registration confirmation forms part of your GDP audit trail. Keep this reference for your records."
- [ ] Given the confirmation page is displayed, then the page is formatted for clean printing — no navigation chrome, no extraneous links, just the confirmation panel and reference information
- [ ] Given the confirmation page uses `@media print` CSS, then the printed output includes the GOV.UK header, the confirmation panel, the reference number, the timestamp, and the GDP statement

### Accessibility
- [ ] Keyboard navigable — all content and any action links are focusable
- [ ] Screen reader announces the confirmation panel heading, reference number, and GDP compliance statement
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-panel` (confirmation), `govuk-body` (compliance text)

### Clinical Safety
- [ ] N/A — registration confirmation is an administrative record

### Data Protection
- [ ] The confirmation page does not display the applicant's full details — only the reference number, submission timestamp, and compliance statement
- [ ] The printed confirmation does not include email addresses, telephone numbers, or account numbers
