---
name: ukhsa-user-stories
description: 'Use when writing user stories, acceptance criteria, or persona definitions for a UKHSA digital health service.'
---

# UKHSA User Stories — Writing Standards

This skill helps write user stories and acceptance criteria for UKHSA services, incorporating UKHSA-specific personas, accessibility requirements (WCAG 2.2 AA), and clinical safety acceptance criteria.

## When to Use

- Writing new user stories for UKHSA features
- Defining acceptance criteria for GitHub Issues
- Creating personas for UKHSA user research
- Reviewing stories for completeness against UKHSA standards

## UKHSA Personas

Use these archetypes (adapt names and details for your service):

| Persona | Role | Needs | Constraints |
|---|---|---|---|
| **Sarah** | Patient, 35 | Book appointments, view results | Uses mobile, moderate digital literacy |
| **James** | Patient, 72 | Manage prescriptions | Low digital confidence, uses screen magnifier |
| **Dr. Patel** | GP | Review patient history quickly | Time-pressured (10-min appointments), desktop |
| **Nurse Chen** | Practice nurse | Record observations | Shared workstation, standing |
| **Admin Mo** | Receptionist | Manage bookings, triage calls | Multi-tasking, keyboard-only workflow |

## User Story Format

```
As a [persona],
I need to [action],
So that [benefit].
```

## Acceptance Criteria

Every story MUST include these categories:

### Functional
- Given/When/Then format for testable behaviour

### Accessibility (mandatory for all UKHSA stories)
- Keyboard navigable (Tab, Enter, Escape, Arrow keys)
- Screen reader announces purpose and state changes
- Meets WCAG 2.2 Level AA — see [GOV.UK accessibility guidance](https://www.gov.uk/service-manual/helping-people-to-use-your-service/making-your-service-accessible-an-introduction)
- Uses GOV.UK Design System components — see [GOV.UK Design System](https://design-system.service.gov.uk)

### Clinical Safety (if applicable)
- No clinical data is displayed incorrectly
- Error states do not lead to clinical misinterpretation
- Time-sensitive data shows currency/freshness

### Data Protection
- Only minimum necessary data is collected
- PII is not exposed in URLs, logs, or error messages
- Consent or lawful basis is documented

## Example

```
As Sarah (patient),
I need to view my upcoming appointments,
So that I can plan my schedule.

Acceptance Criteria:
- [ ] Given I am logged in, when I navigate to /appointments, then I see my future appointments in date order
- [ ] Given I have no appointments, when I navigate to /appointments, then I see a clear message "You have no upcoming appointments"
- [ ] Page is keyboard navigable and screen reader accessible
- [ ] GOV.UK header, breadcrumbs, and footer are present
- [ ] No NHS number or DOB exposed in the URL
- [ ] Page meets WCAG 2.2 Level AA (verified via axe-core)
```
