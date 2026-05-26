# Story 013 — Approval Link Expiry and Applicant Notification

**Journey**: Approval resend workflow (`journey-approval-resend-workflow.md`)
**Priority**: 2 (Wave 2 — Exception Paths)

## User Story

As Priya (GP Practice Vaccination Coordinator),
I need to be notified when my approval link expires without action from the Authorised Person,
So that I know my application is stalled and can take action to move it forward.

## Acceptance Criteria

### Functional
- [ ] Given 72 hours have elapsed since the AP approval email was sent and the AP has not responded, then the system detects the expiry and sets the approval token status to expired
- [ ] Given the approval link has expired, then audit event EVT-09 (Approval link expired) is written with timestamp and CorrelationId
- [ ] Given the approval link has expired, then a GOV.UK Notify email is sent to the applicant informing them that the approval has expired and providing instructions for requesting a resend
- [ ] Given the expiry notification is sent, then the registration status is updated to Expired
- [ ] Given the expiry notification is sent, then a corresponding entry is written to the NotifyLog table

### Accessibility
- [ ] N/A — this story covers backend expiry detection and email notification, not a user-facing page
- [ ] The expiry notification email follows GDS email content standards: plain English, clear next steps

### Clinical Safety
- [ ] N/A — no clinical data involved

### Data Protection
- [ ] The expiry notification email includes only the CorrelationId and guidance — no account numbers or AP details
- [ ] Expiry detection does not expose the AP's identity or response status to the applicant
