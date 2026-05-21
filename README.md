# UKHSA Alpha Workshop Template

A pre-configured GitHub repository template for building UKHSA Alpha-phase digital services using GitHub Copilot agents. This template provides a complete AI-assisted development environment for 2-day workshops.

## What is this?

This is a **workshop template** — it contains **no application code**. Instead, it provides:

- **19 custom Copilot agents** for UKHSA service design, development, testing, and compliance
- **10 instruction files** that auto-apply UKHSA, GDS, and security standards to all code
- **9 specialized skills** covering UKHSA-specific workflows (DPIA, clinical safety, ADRs, synthetic data, etc.)
- **Workshop guides** for discovery, Day 1 (design & build), and Day 2 (harden & assess)
- **Discovery toolkit** with personas, user journeys, and scenario templates

## Quick Start

1. **Use this template** to create a new repository for your team
2. Open in GitHub Codespaces or clone locally with the Dev Container
3. Complete the [discovery phase](discovery/README.md) before the workshop

== Before 2 day workshops please review and customise all the files in .github folder
4. Follow [Day 1 guide](docs/workshop/day1-guide.md) to design and build with Copilot Agent Mode
5. Follow [Day 2 guide](docs/workshop/day2-guide.md) to complete the Alpha with Copilot Coding Agent

## Prerequisites

- GitHub account with Copilot seat (Business or Enterprise)
- Azure subscription with Contributor access to UK South region
- Access to [M365 Copilot](https://m365.cloud.microsoft/chat/) with Researcher agent (for discovery)

All development tools are pre-installed in the Dev Container.

## Workshop Structure

### Pre-workshop: Discovery (1-2 hours)

Complete user research before the workshop using the built-in discovery toolkit:

- Define the **scenario** (problem statement, users, context)
- Create **personas** (19 example UKHSA personas included)
- Map **user journeys** (template-based approach)

See [discovery/README.md](discovery/README.md) for detailed instructions.

### Day 1: Design and Build (8 hours)

Build the service from scratch using Copilot Agent Mode in VS Code:

1. **Architecture design** — UKHSA Architect agent analyses discovery and generates ADRs
2. **User stories** — UKHSA Product Owner decomposes journeys into stories with acceptance criteria
3. **Service scaffolding** — UKHSA Service Builder generates full-stack application
4. **Feature implementation** — Build as many stories as possible with Copilot assistance

See [docs/workshop/day1-guide.md](docs/workshop/day1-guide.md) for the full guide.

### Day 2: Complete the Alpha (8 hours)

Harden the service and demonstrate GDS Service Standard readiness using Copilot Coding Agent on GitHub:

- Testing (unit, integration, E2E, performance)
- Code quality and security review
- Accessibility audit (WCAG 2.2 Level AA)
- CI/CD pipeline
- Infrastructure review
- Clinical safety (UKHSA safety hazard log)
- DPIA
- GDS assessment evidence
- Documentation and runbook

See [docs/workshop/day2-guide.md](docs/workshop/day2-guide.md) for the full guide.

## Tech Stack

The stack is .NET/ASP.NET Core with Razor views and GOV.UK Design System with UKHSA branding. This can be customized before the workshop by editing `.github/instructions/tech-stack.instructions.md`.

| Layer | Choice |
|---|---|
| **Backend** | .NET 10 LTS / ASP.NET Core MVC / Kestrel |
| **Frontend** | ASP.NET Core MVC Razor views (`.cshtml`) + GovUk.Frontend.AspNetCore |
| **Design System** | GOV.UK Design System with UKHSA branding overrides |
| **Database** | Azure SQL Database + Entity Framework Core 10 |
| **Testing** | xUnit + FluentAssertions + Moq + WebApplicationFactory + Playwright for .NET + k6 |
| **Infrastructure** | Terraform (`azurerm`), Azure App Service / Container Apps (UK South/UK West) |
| **Secrets** | Azure Key Vault via User-Assigned Managed Identity |
| **CI/CD** | GitHub Actions with OIDC-based Azure auth |

See [tech-stack.instructions.md](.github/instructions/tech-stack.instructions.md) for full implementation details and instructions for changing the stack.

## Custom Agents

This template includes 19 specialized Copilot agents in `.github/agents/`:

### Engineering Agents (7)
- **UKHSA Service Builder** — Scaffolds and deploys full-stack UKHSA services from architecture and user stories
- **Testing** — Writes unit and integration tests with xUnit + FluentAssertions (80% coverage target)
- **Playwright E2E** — Adds E2E test coverage with Playwright for .NET and accessibility audits
- **Performance** — Creates k6 load tests and checks Core Web Vitals
- **Code Quality Reviewer** — Reviews patterns, type safety, error handling, test coverage, API quality
- **Security Reviewer** — Audits against OWASP Top 10, checks headers, secrets, dependencies
- **CI/CD Pipeline Builder** — Creates GitHub Actions workflows for linting, testing, building, deploying

### UKHSA Domain Agents (6)
- **UKHSA Architect** — Analyses discovery, designs technical architecture, writes ADRs, produces diagrams
- **UKHSA Product Owner** — Decomposes user journeys into user stories with UKHSA acceptance criteria
- **UKHSA Clinical Safety** — Generates safety hazard logs, risk matrices, Clinical Safety Case Reports
- **UKHSA DPIA Advisor** — Drafts Data Protection Impact Assessments for UKHSA services processing health data
- **UKHSA GDS Assessor** — Maps repository evidence to GDS Service Standard 14 points and UKHSA Technology Standards
- **UKHSA Content Designer** — Reviews and writes user-facing copy following GOV.UK content style guide

### Quality & Operations Agents (6)
- **Accessibility Auditor** — Audits against WCAG 2.2 Level AA, runs axe-core scans, validates keyboard navigation
- **Azure Infra Security Reviewer** — Audits Terraform and Azure configuration against UKHSA security standards
- **Security Reviewer** — Audits code against OWASP Top 10, identifies vulnerabilities
- **Visual QA** — Screenshots every page at desktop and mobile, verifies API data matches rendered content
- **Demo Recorder** — Generates demo narrative and Playwright script with video recording
- **UKHSA Documentation** — Creates and updates MKdocs site for the service

## Instructions & Standards

All code generated in the workshop automatically follows UKHSA, GDS, and security standards through auto-applied instruction files in `.github/instructions/`:

- **ukhsa-api.instructions.md** — RESTful API patterns for UKHSA services
- **health-identifiers.instructions.md** — Health identifier handling standards
- **ukhsa-security.instructions.md** — OWASP Top 10, secrets management, input validation, PII logging
- **govuk-frontend.instructions.md** — GOV.UK Design System component usage with UKHSA branding
- **org-standards.instructions.md** — Organisational policies (deployment, testing, security)
- **tech-stack.instructions.md** — Technology implementation details
- **testing.instructions.md** — Test coverage, patterns, and quality thresholds
- **performance.instructions.md** — Performance testing, targets, Core Web Vitals
- **terraform-azure-nhs.instructions.md** — Azure infrastructure patterns for UKHSA services

## Skills

Specialized workflows in `.github/skills/`:

- **azure-ukhsa-deploy** — Deploy UKHSA .NET/ASP.NET Core service to Azure using Terraform
- **ukhsa-safety-hazard-log** — Generate clinical safety hazard log and safety case
- **dotnet-aspnet-azure** — Scaffold UKHSA service with .NET + ASP.NET Core + Azure stack
- **gds-service-standard** — Assess against GDS Service Standard 14 points
- **ukhsa-adr-writer** — Document architectural decisions using ADR format
- **ukhsa-dpia** — Draft Data Protection Impact Assessment for UKHSA services
- **ukhsa-synthetic-data** — Generate synthetic test data for UKHSA services
- **ukhsa-user-stories** — Write user stories and acceptance criteria for UKHSA digital services
- **playwright-dotnet-e2e** — Playwright for .NET E2E patterns with Page Object Model

## Repository Structure

```
.github/
  agents/             19 custom Copilot agents
  instructions/       10 auto-applied coding instruction files
  skills/             8 agent skills (SKILL.md folders)
  workflows/          GitHub Actions
discovery/
  personas/           Persona generation and templates
  scenarios/          Scenario templates
  user_journeys/      User journey templates
docs/
  workshop/           Workshop guides and Day 2 issue templates
AGENTS.md             Copilot Coding Agent context file
```

## Documentation

- **[Workshop README](docs/workshop/README.md)** — Overview of workshop structure and content
- **[Discovery Guide](discovery/README.md)** — Pre-workshop discovery toolkit
- **[Day 1 Guide](docs/workshop/day1-guide.md)** — Design and build with Copilot Agent Mode
- **[Day 2 Guide](docs/workshop/day2-guide.md)** — Complete the Alpha with Copilot Coding Agent
- **[Day 2 Issues](docs/workshop/day2-issues/)** — Generated by Day 2 Issue Generator agent

## Customizing This Template

### Change the Tech Stack

1. Edit `.github/instructions/tech-stack.instructions.md`
2. Swap the corresponding agents in `.github/agents/`
3. Update skills in `.github/skills/` with new implementation patterns

See the "Key Files When Swapping" section in [tech-stack.instructions.md](.github/instructions/tech-stack.instructions.md) for details.

### Adapt for Another UK Health Organization

This template can be adapted for other UK health organizations (NHS, NICE, DHSC, etc.):

1. Fork this repository
2. Rename agents, instructions, and skills to match your organization
3. Update branding in instructions (design system, logo, color scheme)
4. Modify org-standards.instructions.md for your organization's policies
5. Update personas and scenarios for your service context

## Standards Compliance

Services built with this template comply with:

- **[GDS Service Standard](https://www.gov.uk/service-manual/service-standard)** (14 points)
- **[UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)**
- **[WCAG 2.2 Level AA](https://www.w3.org/WAI/WCAG22/quickref/)** accessibility
- **[OWASP Top 10](https://owasp.org/www-project-top-ten/)** security
- **Clinical safety risk management** (SIREN methodology)
- **UK GDPR** and **[Data Protection Act 2018](https://www.legislation.gov.uk/ukpga/2018/12/contents/enacted)**
- **[MHRA Good Distribution Practice](https://www.gov.uk/government/publications/good-distribution-practice/good-distribution-practice)** (where applicable)

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on contributing to this template.

## Code of Conduct

See [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) for community guidelines.

## Licence

MIT — See [LICENSE](LICENSE) for details.
