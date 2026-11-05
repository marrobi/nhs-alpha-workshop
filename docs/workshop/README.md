# NHS Alpha — Copilot Workshop Template

A pre-configured GitHub repository template for building NHS Alpha-phase digital services using GitHub Copilot agents. Used in a 2-day workshop format.

## What's in this repo

This repository contains **no application code**. It provides the Copilot configuration, agents, skills, instructions, and workshop guides that enable teams to build an NHS service from scratch using AI-assisted development.

```
.github/
  agents/             10 role-based custom Copilot agents
  instructions/       8 auto-applied coding instruction files
  skills/             23 task skills (SKILL.md folders)
  workflows/          GitHub Actions
docs/
  workshop/           Workshop guides and Day 2 issue templates
AGENTS.md             Copilot Coding Agent context file
```

## Workshop guides

- **[discovery/README.md](../../discovery/README.md)** — Pre-workshop: Discovery using the built-in discovery toolkit
- **[day1-guide.md](day1-guide.md)** — Day 1: Design and Build with Copilot Agent Mode in VS Code
- **[day2-guide.md](day2-guide.md)** — Day 2: Complete the Alpha with Copilot Coding Agent on GitHub
- **[day2-issues/](day2-issues/)** — Prepared by the NHS Delivery Lead using the `day2-issue-planning` skill (empty until Day 2)

## Workshop overview

This workshop runs over two days using GitHub Copilot to build an NHS Alpha-phase digital service. **Use this template** to create a new repository for your team, then open it in GitHub Codespaces or VS Code with the provided Dev Container.

**Prerequisites**: GitHub account with a Copilot seat, Azure subscription with Contributor access to UK South, and access to [M365 Copilot](https://m365.cloud.microsoft/chat/) with Researcher agent (for discovery). All other tools are pre-installed in the Dev Container.

- **Pre-workshop — Discovery**: Complete the discovery phase in your new repo before the workshop — scenario, personas, and user journeys. See [discovery/README.md](../../discovery/README.md).
- **Day 1 — Design and Build**: Design the architecture, generate user stories, group and prioritise them into a build backlog, scaffold the service, and build as many stories as possible using Copilot Agent Mode in VS Code. See [day1-guide.md](day1-guide.md).
- **Day 2 — Complete the Alpha**: Harden, test, document, and demonstrate GDS readiness using Copilot Coding Agent on GitHub. See [day2-guide.md](day2-guide.md).

## Tech stack

See [`.github/instructions/tech-stack.instructions.md`](../../.github/instructions/tech-stack.instructions.md) for the current stack, modify this as required. Summary:

| Layer | Technology |
|---|---|
| Backend | Python 3.12, FastAPI, Uvicorn |
| Frontend | React 18, Vite, TypeScript, [nhsuk-react-components](https://github.com/NHSDigital/nhsuk-react-components) |
| Design System | [NHS.UK Frontend](https://service-manual.nhs.uk/design-system) |
| Testing | pytest + httpx, Vitest, Playwright, k6 |
| Infrastructure | Terraform (`azurerm`), Azure App Service (UK South) |
| Secrets | Azure Key Vault with Managed Identity |
| CI/CD | GitHub Actions |

## Organisational standards

Review [`.github/instructions/org-standards.instructions.md`](../../.github/instructions/org-standards.instructions.md) and update it with your organisation's policies — deployment strategy, backup requirements, coverage thresholds, secret scanning, coding standards, and security requirements. The defaults are based on NHS Architecture Principles and the NHS Cloud Security Good Practice Guide, but teams should tailor them to their organisation. If your team has no specific standards to enforce, you can delete the content body (keeping the frontmatter) and the toolkit will not apply any organisational standards.

## Agents

| Agent | Purpose |
|---|---|
| **NHS Discovery Analyst** | Scenario and problem definition, M365 Researcher persona handoff, and user journeys |
| **NHS Architect** | Architecture options, diagrams, and proposed ADRs |
| **NHS Product Owner** | User stories, acceptance criteria, end-to-end journey coverage, and prioritised backlog |
| **NHS Service Builder** | Scaffolds, builds, tests, and deploys the service using the selected stack, including alerting and the Azure SRE Agent |
| **NHS Delivery Lead** | Day 2 issue planning and delivery sequencing |
| **NHS Quality Engineer** | Selects the relevant testing, review, accessibility, performance, security, and GDS skills |
| **NHS Clinical Safety** | DCB0129 hazards and safety case; drafts only |
| **NHS DPIA Advisor** | DPIA and data protection risks; the DPO or IG lead approves |
| **NHS Content Designer** | Reviews and writes user-facing copy against NHS content guidance |
| **NHS Documentation** | Builds and maintains service documentation |

## Models

The agents and skills do not pin a model. Each agent uses the model that you select in VS Code, or the default model for the Copilot coding agent on GitHub. This means that you do not need to change the repository when models are added or retired.

- Use a high-capability model for the NHS Service Builder and the NHS Architect. These roles do long, multi-step work.
- Before the workshop, check which models your organisation's Copilot policy allows, and their premium request cost.
- Do not add a `model` field to an agent unless the team agrees it. On GitHub, the field accepts only one model, with no fallback.

## Skills

Agents use task skills for repeatable procedures. Copilot can also load skills when their descriptions match the task.

| Skill | Triggered When |
|---|---|
| `m365-researcher-personas` | Following the external M365 Copilot Researcher steps for persona research |
| `nhs-architecture-design` | Turning discovery evidence into architecture options and diagrams |
| `nhs-adr-writer` | Recording proposed architectural decisions |
| `nhs-product-backlog` | Decomposing journeys and prioritising build batches |
| `nhs-user-stories` | Writing user stories and acceptance criteria |
| `fastapi-react-azure` | Scaffolding or building with the default tech stack |
| `azure-nhs-deploy` | Deploying to Azure and configuring Terraform |
| `azure-sre-agent` | Adding alerting and a read-only Azure SRE Agent as code, and rehearsing an incident |
| `github-actions-pipeline` | Creating CI/CD and dependency update workflows |
| `automated-testing` | Writing unit and integration tests |
| `playwright-nhs-e2e` | Testing user journeys in a browser |
| `code-quality-review` | Reviewing code quality and maintainability |
| `security-review` | Reviewing application security |
| `azure-infrastructure-review` | Reviewing Terraform and Azure infrastructure |
| `accessibility-audit` | Auditing WCAG 2.2 AA accessibility |
| `performance-testing` | Writing load tests and measuring web performance |
| `visual-qa` | Reviewing journeys and pages at desktop and mobile sizes |
| `demo-recording` | Producing a local scripted service walkthrough |
| `gds-service-standard` | Assessing evidence against GDS 14 points |
| `dcb0129-hazard-log` | Drafting clinical safety documentation |
| `nhs-dpia` | Drafting the DPIA and checking implemented controls |
| `nhs-synthetic-data` | Creating synthetic NHS data and test fixtures |
| `day2-issue-planning` | Preparing tailored Day 2 issue files |

## Azure naming convention

Resources use `var.app_name` so multiple Alpha services can share one subscription:

```
rg-{app_name}-{env}        Resource Group
asp-{app_name}-{env}       App Service Plan
app-{app_name}-{env}       Web App
kv-{app_name}-{env}        Key Vault
ai-{app_name}-{env}        Application Insights
```

## NHS constraints

- **No real patient data** — use synthetic data for tests and examples
- **Azure UK South only** — data sovereignty
- **Managed Identity** — no service principal secrets
- **WCAG 2.2 AA** — mandatory accessibility standard
- **NHS Design System** — all user-facing pages must use nhsuk components

## References

- [How the discovery phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works)
- [How the alpha phase works](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works)
- [GDS Service Standard](https://www.gov.uk/service-manual/service-standard) (14 points)
- [Discovery toolkit](../../discovery/README.md) — discovery prompts for scenarios, personas, and user journeys
- [NHS Design System](https://service-manual.nhs.uk/design-system)
- [NHS Content Style Guide](https://service-manual.nhs.uk/content)
- [NHS Accessibility Guidance](https://service-manual.nhs.uk/accessibility)
- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [DCB0129 Clinical Safety](https://digital.nhs.uk/data-and-information/information-standards/information-standards-and-data-collections-including-extractions/publications-and-notifications/standards-and-collections/dcb0129-clinical-risk-management-its-application-in-the-manufacture-of-health-it-systems)
- [ICO DPIA Guidance](https://ico.org.uk/for-organisations/uk-gdpr-guidance-and-resources/accountability-and-governance/data-protection-impact-assessments-dpias/)
