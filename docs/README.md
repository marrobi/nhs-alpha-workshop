# NHS Delivery Kit for GitHub Copilot

This kit helps a team to design and build an NHS alpha-phase digital service with GitHub Copilot. It gives role agents, task skills, instructions and discovery prompt files. It contains **no application code**.

## What is in this repository

```
.github/
  agents/             11 role-based custom Copilot agents (1 hidden worker)
  instructions/       13 auto-applied instruction files
  skills/             37 task skills (SKILL.md folders)
  workflows/          GitHub Actions
discovery/
  prompts/            Prompt files for Microsoft 365 Copilot Chat (same methods as the skills)
  templates/          Blank templates for discovery logs and registers
docs/
  README.md           This overview
  playbooks/          The phases, and one playbook for each delivery mode
AGENTS.md             Copilot coding agent context file
```

## Two delivery modes

The kit has one method and two schedules. The agents and skills are the method. The playbooks are the schedules. Use the same agents and skills in both modes.

| Mode | Use it when | Playbook |
|---|---|---|
| 2-day workshop | A team builds an alpha in a 2-day workshop, after a short discovery | [2-day workshop playbook](playbooks/workshop-2-day/README.md) |
| Multi-week | A team runs discovery over several weeks, then builds in sprints | Multi-week playbook (to be added) |

The phases are the same in both modes: discovery, design, build, harden and assess. See [the phases](playbooks/README.md).

**Prerequisites**: a GitHub account with a Copilot seat, an Azure subscription with Contributor access to UK South, and access to [Microsoft 365 Copilot](https://m365.cloud.microsoft/chat/) with the Researcher agent (for discovery). The Dev Container has all other tools.

## Phases

| Phase | Goal | 2-day workshop | Multi-week |
|---|---|---|---|
| Discovery | Understand the problem, the users and the journeys | Pre-work | Discovery weeks |
| Design | Agree the architecture, the stories and the backlog | Day 1 | Sprint 0 |
| Build | Build the backlog, one journey at a time | Day 1 | Sprints |
| Harden | Add tests, security, operations and compliance evidence | Day 2 | Sprints |
| Assess | Collect the evidence for the GDS service assessment | Day 2 | Before the assessment |

The [phases page](playbooks/README.md) gives the agents, skills and outputs for each phase. The [discovery guide](../discovery/README.md) gives the discovery steps.

## Tech stack

See [`.github/instructions/tech-stack.instructions.md`](../.github/instructions/tech-stack.instructions.md) for the current stack, modify this as required. Summary:

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

Review [`.github/instructions/org-standards.instructions.md`](../.github/instructions/org-standards.instructions.md) and update it with your organisation's policies — deployment strategy, backup requirements, coverage thresholds, secret scanning, coding standards, and security requirements. The defaults are based on NHS Architecture Principles and the NHS Cloud Security Good Practice Guide, but teams should tailor them to their organisation. If your team has no specific standards to enforce, you can delete the content body (keeping the frontmatter) and the toolkit will not apply any organisational standards.

## Agents

| Agent | Purpose |
|---|---|
| **NHS Discovery Analyst** | Scenario and problem definition, M365 Researcher persona handoff, and user journeys |
| **NHS Architect** | Architecture options, diagrams, and proposed ADRs. Leads multi-model Decide mode during architecture work |
| **NHS Product Owner** | User stories, acceptance criteria, end-to-end journey coverage, and prioritised backlog |
| **NHS Service Builder** | Scaffolds, builds, tests, and deploys the service using the selected stack, including alerting and the Azure SRE Agent. The only agent that implements stories. Leads Decide mode for hard design questions during the build |
| **NHS Delivery Lead** | Day 2 issue planning and delivery sequencing |
| **NHS Quality Engineer** | Selects the relevant testing, review, accessibility, performance, security, and GDS skills. Leads multi-model Review mode after each batch and at the end of each day |
| **NHS Clinical Safety** | DCB0129 hazards and safety case; drafts only |
| **NHS DPIA Advisor** | DPIA and data protection risks; the DPO or IG lead approves |
| **NHS Content Designer** | Reviews and writes user-facing copy against NHS content guidance |
| **NHS Documentation** | Builds and maintains service documentation |
| **NHS Engineer** _(hidden)_ | Read-only engineer that a lead starts as a subagent for the `engineering-review` skill, with a different model for each engineer. You do not select it |

## Models

The agents and skills do not pin a model. Each agent uses the model that you select in VS Code, or the default model for the Copilot coding agent on GitHub. This means that you do not need to change the repository when models are added or retired.

- Use a high-capability model for the NHS Service Builder and the NHS Architect. These roles do long, multi-step work.
- Before you start, check which models your organisation's Copilot policy allows, and their premium request cost.
- Do not add a `model` field to an agent unless the team agrees it. On GitHub, the field accepts only one model, with no fallback.
- For the `engineering-review` skill, name 2 or 3 models from different providers in your prompt. If you do not, the lead asks you before it starts the engineers. Run the lead on a high-tier model, because VS Code does not run a subagent on a model in a higher cost tier than the main chat.

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
| `engineering-review` | Multi-model Decide mode for hard design questions, and Review mode for built batches |
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
| `start-project` | Setting the delivery mode, discovery length, sprint length and owners in `delivery-mode.instructions.md` |
| `problem-statement` | Defining the scenario and problem statement in `discovery/scenarios/scenario.md` |
| `personas` | Drafting the persona report when the team cannot use M365 Copilot Researcher |
| `persona-slides` | Making the persona JSON files and Marp deck from the persona report |
| `user-journeys` | Suggesting and writing user journeys in `discovery/user_journeys/data/` |
| `workshop-prep` | Preparing a discovery workshop pack |
| `workshop-to-actions` | Changing workshop notes into decisions, actions, RAID items and open questions |
| `decision` | Recording a decision or an open decision in `discovery/decision-log.md` |
| `raid-and-traceability` | Keeping the RAID log and the traceability table |
| `interface-register` | Recording external systems and access lead times |
| `access-tracker` | Tracking access requests and finding the ones to escalate |
| `weekly-status` | Writing the weekly discovery status report |
| `playback-pack` | Building the end-of-discovery playback and sign-off pack |
| `marp-slides` | Making an NHS-style Marp deck from any Markdown document |

The discovery skills have a matching prompt file in [`discovery/prompts/`](../discovery/prompts/) or a blank template in [`discovery/templates/`](../discovery/templates/). People with only Microsoft 365 Copilot can use these. See the [discovery guide](../discovery/README.md).

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
- [Discovery toolkit](../discovery/README.md) — discovery skills and Microsoft 365 Copilot prompt files for scenarios, personas, user journeys and workshops
- [NHS Design System](https://service-manual.nhs.uk/design-system)
- [NHS Content Style Guide](https://service-manual.nhs.uk/content)
- [NHS Accessibility Guidance](https://service-manual.nhs.uk/accessibility)
- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [DCB0129 Clinical Safety](https://digital.nhs.uk/data-and-information/information-standards/information-standards-and-data-collections-including-extractions/publications-and-notifications/standards-and-collections/dcb0129-clinical-risk-management-its-application-in-the-manufacture-of-health-it-systems)
- [ICO DPIA Guidance](https://ico.org.uk/for-organisations/uk-gdpr-guidance-and-resources/accountability-and-governance/data-protection-impact-assessments-dpias/)
