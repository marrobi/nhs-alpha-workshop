---
name: nhs-adr-writer
description: 'Use when documenting architectural decisions for an NHS digital service using the ADR format, or when reviewing technology choices for GDS assessment evidence.'
---

# NHS ADR Writer — Architectural Decision Records

This skill generates Architectural Decision Records (ADRs) for NHS digital services. ADRs provide the evidence trail required by GDS Service Standard point 11 ("Choose the right tools and technology") and point 12 ("Make new source code open").

## When to Use

- Documenting a technology choice (framework, hosting, database, auth strategy)
- Recording a design decision that affects the system architecture
- Preparing evidence for a GDS Alpha assessment
- Reviewing or updating an existing ADR after a decision changes

## ADR Format

Use the MADR (Markdown Any Decision Records) format. Store ADRs in `docs/adr/` with sequential numbering:

```
docs/adr/
├── README.md           # Index of all ADRs
├── 0001-backend-framework.md
├── 0002-frontend-framework.md
├── 0003-hosting-platform.md
├── 0004-infrastructure-as-code.md
├── 0005-authentication-strategy.md
└── ...
```

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

## NHS Constraints

[Document any NHS-specific constraints that influenced this decision, such as:]
- Data sovereignty (UK hosting)
- NHS Digital standards compliance
- Clinical safety implications (DCB0129)
- Data protection (UK GDPR Art. 9 special category)
- NHS Design System requirements
- Integration with NHS systems (PDS, Spine, MESH)

## References

- [Links to relevant documentation, RFCs, NHS standards]
```

## Standard ADRs for NHS Alpha Services

Every NHS Alpha built during the workshop should document at minimum:

### ADR-0001: Backend Framework — Python/FastAPI
- **Context**: Need a lightweight, async-capable API framework with strong type validation
- **Decision**: FastAPI with Pydantic for input validation, Uvicorn as ASGI server
- **NHS constraints**: NHS Digital Python community of practice, strong ecosystem for health data processing
- **Alternatives**: Django REST Framework (heavier, more opinionated), Flask (no built-in validation)

### ADR-0002: Frontend Framework — React with nhsuk-react-components
- **Context**: Need to build user-facing pages compliant with NHS Design System
- **Decision**: React 18 with TypeScript, Vite, and nhsuk-react-components
- **NHS constraints**: Must use NHS Design System components, WCAG 2.2 AA mandatory
- **Alternatives**: Jinja2 server-side rendering (simpler but less interactive), Next.js (more complex than needed for Alpha)

### ADR-0003: Hosting — Azure App Service (UK South)
- **Context**: NHS data sovereignty requires UK-based hosting; team has Azure subscription
- **Decision**: Azure App Service on Linux in UK South region
- **NHS constraints**: Data must remain in UK, Managed Identity for authentication, Key Vault for secrets
- **Alternatives**: Azure Container Apps (more complex), NHS Cloud Centre of Excellence recommends Azure

### ADR-0004: Infrastructure as Code — Terraform
- **Context**: Infrastructure must be reproducible and auditable
- **Decision**: Terraform with azurerm provider, state stored remotely
- **NHS constraints**: Multiple Alpha teams may share a subscription; naming convention uses `var.app_name` for isolation
- **Alternatives**: Bicep (Azure-only, less portable), Pulumi (smaller community)

### ADR-0005: Authentication Strategy — Managed Identity
- **Context**: Service-to-service auth needed for Key Vault, Application Insights
- **Decision**: User Assigned Managed Identity, no service principal secrets
- **NHS constraints**: No client secrets in code or CI/CD; aligns with NHS Cloud Centre of Excellence guidance
- **Alternatives**: Service Principal with client secret (less secure, secret rotation burden)

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

- Number ADRs sequentially — never reuse a number
- Set status to "Accepted" when the team agrees, not before
- When a decision changes, create a new ADR that supersedes the old one — never delete ADRs
- Include NHS-specific constraints — assessors look for evidence of constraint-aware decision making
- Keep ADRs concise — 1–2 pages maximum
- Write in plain English — ADRs may be read by non-technical assessors

## References

- [MADR — Markdown Any Decision Records](https://adr.github.io/madr/)
- [GDS Service Standard Point 11](https://www.gov.uk/service-manual/service-standard/point-11-choose-the-right-tools-and-technology)
- [GDS Service Standard Point 12](https://www.gov.uk/service-manual/service-standard/point-12-make-new-source-code-open)
- [NHS Cloud Centre of Excellence](https://digital.nhs.uk/services/cloud-centre-of-excellence)
