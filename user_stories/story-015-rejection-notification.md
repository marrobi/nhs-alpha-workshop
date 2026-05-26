# Story 015 — AP Rejection with Reason and Applicant Notification

**Journey**: Registration rejection outcome (`journey-registration-rejection-outcome.md`)
**Priority**: 2 (Wave 2 — Exception Paths)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to receive a clear notification if my registration is rejected, including the reason and guidance on what to do next,
So that I can correct the issue or seek alternative support without being left in the dark.

## Acceptance Criteria

### Functional
- [ ] Given the Authorised Person rejects my application with a mandatory reason (see Story 011), then a GOV.UK Notify email is sent to me containing: the rejection reason, my CorrelationId reference, and guidance on next steps
- [ ] Given I receive a rejection email, then the next steps guidance includes: (a) if the rejection is due to a correctable data issue, I can submit a new registration; (b) if the rejection is due to an account ownership issue, I should contact the ImmForm helpdesk
- [ ] Given the rejection is recorded, then the registration status is set to Rejected, the RejectedAt timestamp is set, and the RejectionReason is stored
- [ ] Given the rejection is recorded, then the rejected record is preserved in the audit log and accessible to the QA/WDA RP via the audit interface
- [ ] Given I submit a new registration after rejection, then it is treated as a new application — the rejected record is not overwritten
- [ ] Given the rejection notification is sent, then a corresponding entry is written to the NotifyLog table

### Accessibility
- [ ] N/A — this story covers notification content and backend processing
- [ ] The rejection email follows GDS email content standards: plain English, empathetic tone, clear next steps

### Clinical Safety
- [ ] N/A — no clinical data in rejection notifications

### Data Protection
- [ ] The rejection email includes only the CorrelationId, applicant name, rejection reason, and next steps — no account numbers or AP details
- [ ] The rejection reason is visible to the applicant (via email) and to the QA/WDA RP (via audit) but not to other applicants
- [ ] Rejected records follow the data retention policy: 2 years from rejection date
