---
name: ukhsa-user-stories
description: 'Use when writing user stories, acceptance criteria, or persona definitions for a UKHSA digital health-security service.'
---

# UKHSA User Stories — Writing Standards

This skill helps write user stories and acceptance criteria for UKHSA services, incorporating UKHSA-specific personas, accessibility requirements (WCAG 2.2 AA), and — where in scope — safety acceptance criteria.

## When to Use

- Writing new user stories for UKHSA features
- Defining acceptance criteria for GitHub Issues
- Creating personas for UKHSA user research
- Reviewing stories for completeness against UKHSA Engineering Standards and the GDS Service Standard

## UKHSA Personas

Use these archetypes (adapt names and details for your service):

| Persona | Role | Needs | Constraints |
|---|---|---|---|
| **Sarah** | Member of the public, 35 | Report and track a notifiable event, view guidance | Uses mobile, moderate digital literacy |
| **James** | Member of the public, 72 | Access health-protection guidance, request a service | Low digital confidence, uses screen magnifier |
| **Dr. Patel** | Clinician (public-health) | Review surveillance signal quickly | Time-pressured, desktop |
| **Aisha** | UKHSA epidemiologist | Run analyses, export aggregate data | Power user, multi-monitor, keyboard-driven |
| **Mo** | Operations / service desk | Manage cases, triage referrals | Multi-tasking, keyboard-only workflow |
| **Partner Lab Tech** | NHS / private lab user | Submit results via API or portal | API-first, intermittent web use |

## User Story Format

```
As a [persona],
I need to [action],
So that [benefit].
```

## Acceptance Criteria

Every story MUST include these categories:

### Functional
- Given / When / Then format for testable behaviour

### Accessibility (mandatory for all UKHSA stories)
- Keyboard navigable (Tab, Enter, Escape, Arrow keys)
- Screen reader announces purpose and state changes
- Meets WCAG 2.2 Level AA — verified via `Deque.AxeCore.Playwright`
- Uses [GOV.UK Design System](https://design-system.service.gov.uk/) via `GovUk.Frontend.AspNetCore`
- Skip link target is `#main-content`

### Safety (mandatory where regulated workload — MHRA GDP / Annex 11 / ALCOA+)
- No regulated data is displayed incorrectly or out of context
- Error states do not lead to operational or clinical misinterpretation
- Time-sensitive data shows currency / freshness
- Actions are attributable to an authenticated identity (ALCOA+ Attributable)

### Data Protection
- Only minimum necessary data is collected (Caldicott)
- PII / NHS Number is not exposed in URLs, logs, or error messages
- Lawful basis (Art. 6) and special-category condition (Art. 9) are documented in the DPIA
- Audit log entry created for any read/write of personal data

## Example

```
As Sarah (member of the public),
I need to view my upcoming vaccination appointments,
So that I can plan my schedule.

Acceptance Criteria:
- [ ] Given I am signed in via GOV.UK One Login, when I navigate to /appointments,
      then I see my future appointments in date order
- [ ] Given I have no appointments, when I navigate to /appointments,
      then I see a clear message "You have no upcoming appointments"
- [ ] Page is keyboard navigable and screen reader accessible
- [ ] GOV.UK header, breadcrumbs, and footer are present (GovUk.Frontend.AspNetCore tag helpers)
- [ ] No NHS number or DOB is exposed in the URL
- [ ] Page meets WCAG 2.2 Level AA (verified via Deque.AxeCore.Playwright)
- [ ] Audit log entry written for the view action (ALCOA+ Attributable, Contemporaneous)
```

## Rules

- One user need per story; split if the story has more than one independent benefit.
- Always name a real persona — never "the user".
- Public-user flows use GOV.UK One Login; internal-staff flows use Microsoft Entra ID.
- Stories that change data flows must link to the DPIA section and hazard log entries they affect.
- Acceptance criteria are testable — if you can't write a test for it, rewrite it.

## References

- [GOV.UK Design System](https://design-system.service.gov.uk/)
- [GovUk.Frontend.AspNetCore](https://github.com/gunndabad/govuk-frontend-aspnetcore)
- [WCAG 2.2 Quick Reference](https://www.w3.org/WAI/WCAG22/quickref/)
- [GDS Service Standard](https://www.gov.uk/service-manual/service-standard)
- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
