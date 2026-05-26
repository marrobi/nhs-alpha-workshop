# Story 010 — AP Approval Request Email Dispatch

**Journey**: Authorised Person approval (`journey-authorised-person-approval.md`)
**Priority**: 1 (Wave 1 — Core Happy Path)

## User Story

As Linda (ImmForm Authorised Person),
I need to receive a clear, actionable approval request email when someone applies to join my account,
So that I can review the applicant's details and make an informed decision quickly.

## Acceptance Criteria

### Functional
- [ ] Given a registration has been submitted and passes duplicate detection, then the system sends a GOV.UK Notify email to the Authorised Person retrieved from the Organisation API
- [ ] Given the approval email is sent, then it contains: applicant name, declared job title, the ImmForm account they are requesting access to, and the account's organisation name
- [ ] Given the approval email is sent, then it contains two action links: one to approve and one to reject the application
- [ ] Given the approval email is sent, then it clearly states the 72-hour expiry window: "You have 72 hours to respond. After this time, the approval link will expire."
- [ ] Given the approval email is sent, then audit event EVT-06 (Manager approval email sent) is written with timestamp, CorrelationId, and AP email (hashed)
- [ ] Given the approval email is sent, then a corresponding entry is written to the NotifyLog table with template ID, recipient type (AuthorisedPerson), and dispatch timestamp
- [ ] Given the system is in alpha (GOV.UK Notify stub), then the email content is logged to the console and written to NotifyLog — the email is not sent via the Notify API
- [ ] Given the AP approval token is generated, then it is a cryptographically random, opaque string stored in the ApprovalToken table with a 72-hour expiry

### Accessibility
- [ ] N/A — this story covers backend email dispatch, not a user-facing page
- [ ] The email content follows GDS email content standards: plain English, clear call to action, no jargon

### Clinical Safety
- [ ] N/A — no clinical data in the approval email

### Data Protection
- [ ] The approval email contains only the minimum data needed for the AP to make a decision — no telephone number or email address of the applicant
- [ ] The approval token in the URL is opaque and contains no PII — it cannot be decoded to reveal applicant or account details
- [ ] AP email address is hashed in audit log entries
- [ ] Approval links use HTTPS only
