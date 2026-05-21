---
name: ukhsa-adr-writer
description: 'Use when writing or updating an Architecture Decision Record (ADR) for a UKHSA .NET 10 / Azure service.'
---

# UKHSA ADR Writer — MADR Format

This skill writes Architecture Decision Records (ADRs) for UKHSA digital services. ADRs provide the evidence trail required by GDS Service Standard point 11 ("Choose the right tools and technology") and point 12 ("Make new source code open").

## When to Use

- Documenting a technology choice (framework, hosting, database, auth strategy)
- Recording a design decision that affects the system architecture
- Preparing evidence for a GDS Alpha assessment
- Reviewing or updating an existing ADR after a decision changes

## Location and Numbering

Use the MADR (Markdown Any Decision Records) format. Store ADRs as **new files** in `docs/adr/` with sequential numbering. Do **not** edit this skill file — it is a reference, not a template to fill in.


- All ADRs live in `docs/adr/`.
- Sequential numbering: `0001-record-architecture-decisions.md`, `0002-use-net-10.md`, ...
- The first ADR is always "Record architecture decisions" — the meta-decision to use MADR.

## ADR Template

```markdown
# ADR-NNNN: [Decision Title]

**Status**: Proposed | Accepted | Deprecated | Superseded by ADR-NNNN

**Date**: YYYY-MM-DD

**Deciders**: [Who was involved in the decision]

## Context

[What is the issue that we're seeing that is motivating this decision or change?
Include NHS-specific constraints that influenced the decision.]

## Decision

[What is the change that we're proposing and/or doing?]

## Consequences

### Positive
- [What becomes easier or possible as a result of this change?]

### Negative
- [What becomes more difficult as a result of this change?]

### Risks
- [What risks does this decision introduce?]

## Alternatives Considered

### [Alternative 1]
- **Pros**: [advantages]
- **Cons**: [disadvantages]
- **Why rejected**: [reason]

### [Alternative 2]
- **Pros**: [advantages]
- **Cons**: [disadvantages]
- **Why rejected**: [reason]

## UKHSA Constraints Considered

- Data residency: Azure UK South / UK West only
- Regulatory: MHRA GDP / Annex 11 / ALCOA+ where applicable
- Privacy: UK GDPR Art. 6(1)(e), Art. 9(2)(i)/(h) where applicable
- Identity: GOV.UK One Login (public) / Entra ID (internal)
- Design: GOV.UK Design System via GovUk.Frontend.AspNetCore
- Accessibility: WCAG 2.2 Level AA mandatory for all user-facing services

## References

- [Related ADRs]
- [User stories / hazard log / DPIA sections]
```


## ADR Index Template (`docs/adr/README.md`)

```markdown
# Architectural Decision Records

This directory contains the Architectural Decision Records (ADRs) for this service.

| ADR | Title | Status | Date |
|---|---|---|---|
| [0001](0001-backend-framework.md) | Backend Framework — Python/FastAPI | Accepted | YYYY-MM-DD |
| [0002](0002-frontend-framework.md) | Frontend Framework — React/nhsuk-react-components | Accepted | YYYY-MM-DD |
| [0003](0003-hosting-platform.md) | Hosting — Azure App Service UK South | Accepted | YYYY-MM-DD |
| [0004](0004-infrastructure-as-code.md) | Infrastructure as Code — Terraform | Accepted | YYYY-MM-DD |
| [0005](0005-authentication-strategy.md) | Authentication — Managed Identity | Accepted | YYYY-MM-DD |

## What is an ADR?

An Architectural Decision Record captures a significant design decision along with
its context and consequences. See [GDS Service Standard point 11](https://www.gov.uk/service-manual/service-standard/point-11-choose-the-right-tools-and-technology).
```

## Rules

- One decision per ADR. Bundling masks the trade-offs.
- Status transitions are explicit — a deprecated ADR is never deleted; a superseding ADR links back.
- Reference the user story, hazard log entry, or DPIA section that motivated the decision.
- Decisions that change data flows or processing of personal/health data must trigger a DPIA review and a hazard log review.
- Pull requests that change architecture without a corresponding ADR change should be blocked in review.

## References

- [MADR](https://adr.github.io/madr/)
- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
- [GDS Service Standard Point 11](https://www.gov.uk/service-manual/service-standard/point-11-choose-the-right-tools-and-technology)
- [GDS Service Standard Point 12](https://www.gov.uk/service-manual/service-standard/point-12-make-new-source-code-open)