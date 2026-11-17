---
name: nhs-architecture-design
description: 'Use when turning discovery evidence into architecture options, diagrams, and proposed ADRs for an NHS digital service.'
---

# NHS Architecture Design

Use this skill to design an NHS digital service architecture from discovery evidence. Work interactively: ask questions, present options, and wait for people to decide.

You can read and write architecture artefacts. Do **not** scaffold application code; the NHS Service Builder agent owns that work.

## Workflow

### Before Step 1 — Check Human Alignment

Read `discovery/discovery-notes.md` and `discovery/open-questions.md` before proposing technology or architecture options. Use the reviewed constraints, scope, riskiest assumptions and agreed steer throughout the design.

If the notes are missing, any active rows are still `Draft`, the steer is not `Agreed`, or a question blocking architecture has no agreed direction, stop and ask for human review and agreement before continuing. Ask the NHS Discovery Analyst to draft or update missing notes; do not edit the notes or approve them yourself. Recheck this gate on the second architecture pass if the discovery inputs have changed.

### Step 1 — Clarify the Tech Stack

Read `.github/instructions/tech-stack.instructions.md` — this is the **default** tech stack. Present it to the user and ask whether they want to change anything. Common questions:

> **Tech stack review — please confirm or change:**
>
> The default stack is:
> - **Backend**: Python 3.12 / FastAPI
> - **Frontend**: React 18 / Vite / TypeScript / nhsuk-react-components
> - **IaC**: Terraform (azurerm)
> - **Hosting**: Azure App Service (UK South)
> - **Testing**: pytest, Vitest, Playwright, k6
>
> 1. Are you happy with this stack, or do you want to change any layer?
> 2. Do you need a database? If so, which (PostgreSQL on Azure, CosmosDB, SQLite)?
> 3. Do you need any external NHS API integrations (PDS, Spine, MESH)?
> 4. Do you need authentication (NHS login/CIS2, Azure AD)?

**Wait for the user's response.** If they want changes, update `.github/instructions/tech-stack.instructions.md` to reflect their choices. This ensures all downstream agents and instructions use the correct stack.

### Step 2 — Analyse Discovery Artefacts

Read the discovery artefacts:
1. `discovery/scenarios/scenario.md` — identify the core problem and scope boundaries
2. `discovery/personas/persona-report.md` — identify primary and secondary users
3. `discovery/user_journeys/data/journey-*.md` — these drive the entire technical design
4. Identify **constraints**: data sovereignty, NHS network, UK GDPR Art. 9, DCB0129, NHS Design System

Summarise what you found and confirm with the user:
> I've read the discovery artefacts. Here's my understanding: [summary]. Is this correct?

### Step 3 — Present Architecture Options

Identify the key decision points and present 2–3 options for each. Do **not** produce a single design silently. Common decision points:

- **Data storage** — Choose based on data volume and query needs. **Never recommend in-memory or file-based storage** — even in Alpha, data must persist across restarts to test real user journeys.
- **API structure** — single router vs. domain-based routers. How many distinct resources exist?
- **User interface (UI) vs. API-only** — for each component or capability, decide whether it needs a user-facing UI (NHS Design System pages) or is API-only (e.g. a backend integration, a machine-to-machine endpoint, a background job). **If it is not clear whether a component requires a UI, stop and ask the user explicitly** — do not silently assume API-only. Omitting a UI here causes UI user stories to be missed downstream.
- **Frontend pattern** — multi-page with router vs. single interactive page. How complex are the user journeys?
- **External integrations** — which NHS APIs, national systems and Azure services are needed? For each one, propose the alpha approach (real, sandbox or external mock service) in Step 6b. Use `discovery/interface-register.md` for test environments and lead times.
- **Auth approach** — NHS login/CIS2 vs. Azure AD. If the service has multiple user roles, authentication is likely a riskiest assumption and should be included. Only omit auth if the team explicitly decides it is not a riskiest assumption.
- **Network & identity** — all service-to-service and service-to-data communication must use Managed Identity (no shared keys) and Private Endpoints (no public database/storage endpoints). Design the VNet topology, subnet layout, and RBAC role assignments. See `nhs-security.instructions.md` for the full rules.
- **Infrastructure extras** — baseline only vs. database, queue, or cache. What does the data model need?

Format each decision as:

> **Decision: [topic]**
> - **Option A**: [description] — *Trade-off: [pro/con]*
> - **Option B**: [description] — *Trade-off: [pro/con]*
> - **Recommended**: [which and why, given the constraints]
>
> Which do you prefer?

**Wait for the user's response before proceeding.** Collect all decisions.

### Step 3b — Compare Candidate Architectures (when useful)

Do this step when 2 or more decisions depend on each other (for example hosting and data store, a FHIR store or a relational database, one service or split services), or when the user asks. If each decision is independent, skip this step.

1. Make 2 or 3 candidate architectures. Each candidate is a consistent set of choices from Step 3.
2. Draw each candidate as `docs/adr/options/option-<letter>-<short-name>.drawio`, and export it to PNG (see Step 7).
3. Show a comparison table:

   | Criterion | Option A | Option B | Option C |
   |---|---|---|---|
   | Fit to the riskiest assumption | | | |
   | NHS constraints (UK hosting, Managed Identity, Private Endpoints) | | | |
   | Complexity to build and run | | | |
   | Build time in the current delivery mode | | | |
   | Cost signal | | | |
   | Dependencies that are real, sandbox or mock | | | |

4. Give a recommendation and the reason. If the trade-off is hard, offer to run the `engineering-review` skill in Decide mode.
5. **Wait for the user to choose.** The chosen option becomes `docs/adr/architecture.drawio`. Keep the other options in `docs/adr/options/`. List them as "Considered options" in `docs/adr/001-architecture.md`, with links to their PNG files.

### Step 4 — Map Journeys to Technical Design

Using the agreed decisions, map each user journey to:
- **API endpoints** — routes, HTTP methods, request/response shapes
- **Data models** — entities, fields, types, validation rules, relationships. Any entity storing patient data MUST include the NHS Number as a 10-digit string field with modulus 11 validation — see `.github/instructions/nhs-number.instructions.md` for the full ISB 0149 requirements (storage, display, search, validation, transmission)
- **Frontend pages** — NHS Design System pages/components for each journey step (per the UI vs. API-only decision from Step 3). Every screen showing patient-identifiable data MUST display the NHS Number in 3-3-4 format (ISB 0149 Req 07/09).
- **Infrastructure** — cloud resources required beyond the baseline

### Step 5 — Prioritise with the User's Steer and the Dependencies

Order the journeys so the **riskiest assumption** is tested first — the thing that must work for the service to be viable. See [GDS guidance on testing riskiest assumptions](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works#focus-on-testing-your-riskiest-assumptions).

1. Read the steer from discovery: the priority journey, the riskiest assumption and the first slice candidate (the "Steer" section of `discovery/discovery-notes.md`, and `discovery/open-questions.md`). If there is no steer, **ask the user which journey and assumption to prioritise**. Do not choose for them. If the steer status is not `Agreed`, or rows in the file are still `Draft`, tell the user and ask them to confirm before you use it. Do not edit `discovery/discovery-notes.md` — if the design finds a new constraint or changes the scope, tell the user and ask the NHS Discovery Analyst to update the file.
2. Show the technical dependencies that limit the order: authentication, the shared data model, the shared layout and navigation, and external systems with long lead times or mock services.
3. Present the proposed journey order and the reason for each position. Confirm it with the user.

The NHS Product Owner turns this order into slices.

### Step 6 — Write the Architecture ADR

Read the `nhs-adr-writer` skill (`.github/skills/nhs-adr-writer/SKILL.md`) for the ADR template and rules. The skill is a **reference only** — do **not** edit it. Create new files under `docs/adr/`.

Create `docs/adr/001-architecture.md` containing:
- Tech stack decisions (with rationale)
- API endpoint summary (routes, methods, purpose)
- Data model summary (entities, key fields)
- Frontend page structure mapped to user journeys
- Infrastructure components
- User journey priority order with riskiest assumption identified
- NHS constraints that shaped the design

Also create `docs/adr/README.md` as an ADR index.

### Step 6b — Write the Dependency Strategy ADR

Create `docs/adr/002-dependency-strategy.md`. It records, for each dependency, what is real and what is mocked in the alpha. Read `discovery/interface-register.md` for test environments, onboarding steps and lead times.

Apply these defaults. Present the table to the user and **wait for agreement**:

| Category | Examples | Alpha approach |
|---|---|---|
| Azure platform services | App Service, Key Vault, Application Insights, Managed Identity, Entra ID | **Real. Never mocked** |
| The service's own data store | Azure SQL, PostgreSQL, Cosmos DB, Azure Health Data Services FHIR | **Real in Azure.** Locally, the real engine in a container (see "Local data stores") |
| NHS national systems with an open sandbox | Check the [NHS API catalogue](https://digital.nhs.uk/developer/api-catalogue) for the current sandboxes. Do not assume from memory | **Use the NHS sandbox** |
| NHS national systems that need onboarding, an integration environment, HSCN or assurance before access | Decide for each system from the interface register | **External mock service** built from the published specification |
| Never mocked | Authorisation checks, NHS number validation, clinical logic under test, audit logging | **Real** |

**External mock services:**

- A mock is a **separate service**, never code in the application. Put it in `mocks/<system>/`.
- Build it from the published specification. For example, use a mock server generated from the OpenAPI specification, or a FHIR server with synthetic data.
- The application contains only the real client. It reads the base URL of the system from required configuration, with no default value. In the alpha, the URL points at the mock service.
- Deploy the mock services to Azure with Terraform, so that the deployed alpha works end to end. Apply the same network and identity rules as for the application.
- Make the mocks clear in the documentation, not in the user interface. List them in this ADR and in the "What is mocked in this alpha" section of the runbook.
- For each mock, add a risk to the RAID log, an access request to the access tracker (to move to the real system), and a note in the hazard log and the DPIA.

**Local data stores (for parallel work):**

- Default: run **one** container for each data store engine on the developer machine. Give each agent or worktree its own database in it: a separate database name for SQL, or a separate partition or tenant for FHIR. This uses less memory than one container for each agent.
- If this is not possible (the engine has no isolation, the engines need different versions, or the machine has too little memory), use one container for each agent, or run fewer agents in parallel. Record the choice and the reason.

Use this structure:

```markdown
# ADR-002: Dependency strategy for the alpha

## Dependencies

| ID | Dependency | Category | Alpha approach | Reason | Configuration | Move to real by | Links |
|---|---|---|---|---|---|---|---|
| IF-01 | [system] | [category] | Real / Sandbox / Mock / Not used | [why] | `[SYSTEM]_BASE_URL` | [beta, date or sprint] | R-, AR-, HAZ- |

## Local data stores

[Shared container with one database for each worktree, or the fallback and the reason]

## What is mocked in this alpha

[Plain-English list for the runbook and the documentation site]
```

If `discovery/interface-register.md` exists, set its **Alpha approach** column to match this ADR.

### Step 7 — Generate Architecture Diagram

Create an architecture diagram as a draw.io file at `docs/adr/architecture.drawio`. Use the **Draw.io MCP server** (configured in `.vscode/mcp.json`) to create and edit the diagram. The MCP server provides tools for generating draw.io XML from descriptions, converting Mermaid diagrams, and editing existing diagrams.

The diagram should show:

- Frontend (browser) connecting to the backend API — show the user-facing UI for every component that has one, distinct from API-only components
- Backend API with key routers/endpoints
- Data store (if any)
- External integrations (if any) — mark each external mock service as a mock, with a dashed border
- Cloud infrastructure (App Service, Key Vault, monitoring)
- User types (from personas) connecting to the frontend

Use draw.io XML format. Structure the diagram with:
- NHS blue (`#005eb8`) for NHS-specific components
- Standard cloud provider colours for infrastructure
- Clear arrows showing data flow direction
- Labels on all connections
- User Azure icons where appropriate - https://arch-center.azureedge.net/icons/Azure_Public_Service_Icons_V23.zip

Example draw.io XML structure:
```xml
<mxfile>
  <diagram name="Architecture">
    <mxGraphModel>
      <root>
        <mxCell id="0"/>
        <mxCell id="1" parent="0"/>
        <!-- Add nodes and edges here -->
      </root>
    </mxGraphModel>
  </diagram>
</mxfile>
```

#### Export the diagrams to PNG

Export every diagram to PNG, so that ADRs, slide decks and the documentation site can show it:

```bash
scripts/export-diagrams.sh docs/adr
```

The script writes a `.png` file next to each `.drawio` and `.mmd` file. Use draw.io for architecture and deployment diagrams. Use Mermaid (`.mmd`) for sequence, data-flow and entity-relationship diagrams. Embed the PNG in the ADR, for example `![Architecture](architecture.png)`. Commit the source file and the PNG together.

If the script fails because the draw.io CLI, `xvfb-run` or mermaid-cli is missing, stop and tell the user. These tools are installed by `.devcontainer/install-diagram-tools.sh`. Do not skip the export.

### Handoff

Once the ADR and diagram are complete, tell the user:
> Architecture is ready. Switch to the **NHS Product Owner** agent to decompose the user journeys into user stories and recommend slices. After the stories are created, switch back to the **NHS Architect** agent to review the stories, identify additional ADRs and complete the build context in each story. Then use the **NHS Service Builder** agent to build slice 0 (scaffold and CI). The architecture is in `docs/adr/001-architecture.md`, the dependency strategy is in `docs/adr/002-dependency-strategy.md`, and the diagram is at `docs/adr/architecture.drawio` and `docs/adr/architecture.png`.

---

## Second Pass — ADR Review (After User Stories)

This workflow runs **after the NHS Product Owner** has created user stories in `user_stories/`. The stories reveal detailed technical decisions that were not visible during the initial architecture phase — data models, integration patterns, auth flows, error handling strategies, and more. This pass identifies and creates the ADRs needed.

### Step 1 — Read User Stories and Architecture

Read all input artefacts:
1. `user_stories/story-*.md` — all user stories with acceptance criteria
2. `user_stories/backlog.md` — the slices, their order and their dependencies
3. `docs/adr/001-architecture.md` — the initial architecture ADR
4. `docs/adr/002-dependency-strategy.md` — what is real, sandbox or mock
5. `discovery/scenarios/scenario.md` — problem statement and scope
6. `discovery/user_journeys/data/journey-*.md` — original user journeys
7. `discovery/open-questions.md` — open questions and the directions given

### Step 2 — Identify Required ADRs

Analyse the stories for architectural decisions that should be recorded. Look for:

- **Technology choices** implied by stories (database type, auth provider, API gateway, caching strategy)
- **Design patterns** required (event-driven, CQRS, repository pattern, service layer)
- **Integration decisions** (NHS API choices, FHIR resource types, external service contracts)
- **Data model decisions** (entity relationships, storage format, retention, backup strategy)
- **Security decisions** (auth strategy for user roles, session management, PII handling)
- **Infrastructure decisions** (scaling approach, queue/cache needs, monitoring strategy)

Present the list of ADR topics to the user with a brief rationale for each:

> Based on the user stories, I recommend creating these ADRs:
>
> 1. **ADR-0002: [Topic]** — [why this decision matters, which stories drive it]
> 2. **ADR-0003: [Topic]** — [why this decision matters, which stories drive it]
> 3. ...
>
> Do you want to add, remove, or change any of these?

**Wait for the user's response before proceeding.**

### Step 3 — Create the ADRs

Read the `nhs-adr-writer` skill (`.github/skills/nhs-adr-writer/SKILL.md`) for the ADR template and rules. For each agreed ADR topic:

1. Create the ADR file in `docs/adr/` with sequential numbering (starting after the dependency strategy ADR, `002`)
2. Follow the MADR format from the skill
3. Include alternatives considered with trade-offs
4. Reference the user stories that drive the decision
5. Document NHS-specific constraints that influenced the choice

### Step 4 — Update the ADR Index and the Diagrams

1. Update `docs/adr/README.md` to include all new ADRs.
2. Update `docs/adr/architecture.drawio` to match the new ADRs. Add other diagrams if they help, for example a data model or the sequence of an auth or integration flow (as `.mmd` files).
3. Export all diagrams to PNG with `scripts/export-diagrams.sh docs/adr`.

### Step 5 — Check the Slice Order

Read the slices in `user_stories/backlog.md`. Check that each slice comes after the slices and ADR decisions that it depends on. Check that each external dependency of a slice is available in its alpha approach (real, sandbox or mock service). Tell the user about each problem, and suggest a new order. Do not change the order without agreement.

### Step 6 — Complete the Build Context in Each Story

Each story has a **Build context** section (see the `nhs-product-backlog` skill). The NHS Product Owner fills the slice fields. You complete the technical fields, so that an agent on a lower-cost model can build the story without design decisions:

- **ADRs**: each ADR that applies, with its decision in one sentence.
- **API contract**: method, path, and the request and response fields with their exact names and types.
- **Data model**: the entities and fields that the story reads or writes.
- **Pages and components**: routes, and the NHS Design System components for each page.
- **Files to create or change**: the paths, from the project structure in the implementation skill.
- **External dependencies**: each system, its alpha approach from ADR-002, and the configuration setting for its base URL.
- **Synthetic data**: the records that the story needs, made with the `nhs-synthetic-data` skill.
- **Validation commands**: the exact test and lint commands that prove the story is done.
- **Out of scope**: what the story does not do.

Collect each new question in `discovery/open-questions.md` with the `decision` skill. Then set the **Status** field in each story file:

- `Ready` — the build context is complete and no open question blocks the story.
- `Not ready (Q-NNN)` — an open question blocks the story. Tell the user which question.

Update the status of each story in `user_stories/backlog.md` and in the slice's GitHub issue to match.

### Handoff (Second Pass)

Once the ADRs and the build context are complete, tell the user:
> ADRs are ready in `docs/adr/`, and each ready story has its build context. Switch to the **NHS Service Builder** agent to build slice 0: the scaffold and the CI pipeline, in one pull request. Then build the slices in `user_stories/backlog.md` in order, one GitHub issue for each slice.

## MCP Servers

This agent has access to MCP servers configured in `.vscode/mcp.json` and via VS Code extensions:
- **Context7** — use to look up current documentation for libraries, frameworks, and Terraform providers/modules when advising on tech stack and infrastructure choices
- **Draw.io** — use to create and edit architecture diagrams in draw.io format. The MCP server does not export images. Use `scripts/export-diagrams.sh` to make the PNG files
- **Azure MCP Server** (provided by the `ms-azuretools.vscode-azure-mcp-server` extension) — use to query Azure resources, validate infrastructure decisions, and explore available Azure services

## Rules

- **Always ask, never assume** — present options and wait for the user to decide
- **Read organisational standards** — read `.github/instructions/org-standards.instructions.md` for organisational policies that apply to architecture decisions. Incorporate these into the ADR.
- **Update tech-stack.instructions.md** if the user changes any stack choices — this is the single source of truth
- **No Alpha shortcuts** — Alpha exists to test riskiest assumptions with a realistic service. Do not recommend shortcuts that undermine this:
  - **No in-memory data stores** — data must persist across restarts. Use at minimum SQLite, or a proper database if the journeys involve multi-user data.
  - **No skipping authentication** when user roles exist — if the service distinguishes between patients, clinicians, and admin, auth is a riskiest assumption.
  - **No hardcoded/mock data in production code** — use synthetic data via proper seed scripts, not inline dictionaries or JSON files served as APIs.
  - **No mocks inside the application** — design real integrations with Azure services (Entra ID, Monitor, Key Vault) and NHS API sandboxes. A national system that the team cannot reach in the alpha may use an **external mock service** only if `docs/adr/002-dependency-strategy.md` lists it. Never put mock or stub code in the application.
  - **No skipping error handling** — error states are part of the user journey and must be designed.
  - **No single-file applications** — follow the project structure in the implementation skill.
  - If the team explicitly decides to descope something, record it as a decision in the ADR with rationale.
- **Do not write application code** — your output is ADRs, diagrams, and configuration updates
- **Keep ADRs concise** — 1–2 pages maximum, plain English
- **NHS constraints are non-negotiable** — UK hosting, NHS Design System, WCAG 2.2 AA, no real patient data, DCB0129
