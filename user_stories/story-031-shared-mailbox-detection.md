# Story 031 — Shared Mailbox Detection with Policy Warning

**Journey**: Wholesaler procurement registration (`journey-wholesaler-procurement-registration.md`)
**Priority**: 4 (Wave 4 — Persona and Context Variants)

## User Story

As Marcus (Procurement Compliance Lead at a pharmaceutical wholesaler),
I need the registration form to detect when I enter a shared departmental email and display the organisational email policy, requiring me to confirm or re-enter an individual email address,
So that the registration meets GDP traceability requirements that mandate individual accountability.

## Acceptance Criteria

### Functional
- [ ] Given I enter an email address on the applicant details step, when the email matches a shared mailbox pattern (same detection logic as Story 027), then I see a warning message explaining the GDP requirement for individual email addresses
- [ ] Given the GDP warning is displayed, then the message explains: "For GDP compliance, each registration must use an individual email address. Shared mailboxes cannot be used because each orderer must be individually traceable."
- [ ] Given the warning is displayed, then I can either: (a) change my email to an individual address and continue, or (b) confirm I wish to proceed with the detected email — in which case the registration is flagged for helpdesk review with a GDP compliance note
- [ ] Given I confirm a shared mailbox despite the warning, then the registration includes a flag visible in the admin dashboard (Story 019) and audit interface (Story 023) indicating "GDP shared mailbox override"
- [ ] Given the shared mailbox patterns are the same configurable list as Story 027, then updates to the pattern list apply to both NHS and wholesaler registrations

### Accessibility
- [ ] Keyboard navigable — warning message, email input, and action buttons are focusable via Tab
- [ ] Screen reader announces the GDP warning message and the available actions
- [ ] Meets WCAG 2.2 Level AA (verified via axe-core)
- [ ] Uses GOV.UK Design System components: `govuk-warning-text` (GDP notice), `govuk-radios` (change email / proceed with warning), `govuk-button`

### Clinical Safety
- [ ] N/A — email policy enforcement is an administrative and regulatory control

### Data Protection
- [ ] The GDP compliance warning does not expose any third-party PII — it describes the policy requirement only
- [ ] The flag on the registration is visible only to authenticated admin and audit users
