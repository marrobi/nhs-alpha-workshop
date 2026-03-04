# Day 1 — Design and Build with Copilot

## Overview

Day 1 assumes **discovery has already been completed** using the discovery toolkit in this repository. You should arrive with a scenario, personas, and user journeys already produced. Day 1 is about designing the architecture, generating user stories from the journeys, and then building as many stories as possible.

> **Prerequisites**: Complete discovery before the workshop using the [discovery guide](../../discovery/README.md). You need:
> - `discovery/scenarios/scenario.md` — scenario overview and problem statement
> - `discovery/personas/persona-report.md` — researched NHS personas
> - `discovery/user_journeys/data/journey-*.md` — detailed user journeys with Mermaid diagrams
>
> See the [discovery guide](../../discovery/README.md) for how to produce these artefacts.

## Setup (30 minutes)

1. Open your repo in GitHub Codespaces or VS Code with the provided Dev Container (all prerequisites are pre-installed)
2. Verify your discovery artefacts are in `discovery/` (`scenarios/`, `personas/`, `user_journeys/`)
3. Log in to Azure: `az login`
4. Set your Azure subscription: `az account set --subscription <id>`

---

## Phase 1 — Architecture Design (1 hour)

Before writing any code, design the technical architecture informed by your discovery artefacts. This phase uses the **NHS Architect** agent, which will ask you questions and present options interactively.

### Start the Architecture Session

**Agent**: NHS Architect

> Read the discovery artefacts in `discovery/scenarios/`, `discovery/personas/`, and `discovery/user_journeys/data/`. Design the architecture for this service.

The agent will:

1. **Review the default tech stack** and ask if you want to change anything (e.g. add a database, change the frontend framework, add authentication)
2. **Update `tech-stack.instructions.md`** if you make any changes — this ensures all other agents use the correct stack
3. **Analyse your discovery artefacts** and confirm understanding
4. **Present architecture decision points** with 2–3 options each — you choose
5. **Map user journeys to API endpoints, data models, and frontend pages**
6. **Prioritise journeys by riskiest assumption**
7. **Write an ADR** at `docs/adr/001-architecture.md`
8. **Generate a draw.io architecture diagram** at `docs/adr/architecture.drawio`

### Review the Architecture (15 minutes)

Walk through as a team:
- **Scenario & problem statement** — is the scope clear?
- **Personas** — who are the primary users?
- **User journeys** — which journeys will you build? Are they prioritised by riskiest assumption? (see [focus on testing your riskiest assumptions](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works#focus-on-testing-your-riskiest-assumptions))
- **ADR** — does the architecture make sense given the constraints?
- **Diagram** — open `docs/adr/architecture.drawio` in VS Code to review visually

Agree the architecture before moving to user stories.

---

## Phase 2 — Generate User Stories (30 minutes)

Before building, decompose the user journeys into implementable user stories with acceptance criteria. This phase uses the **NHS Product Owner** agent.

**Agent**: NHS Product Owner

> Read the discovery artefacts in `discovery/scenarios/`, `discovery/personas/`, and `discovery/user_journeys/data/`, and the architecture in `docs/adr/001-architecture.md`. Decompose all user journeys into user stories with acceptance criteria.

The agent will:

1. **Read all discovery artefacts and the ADR** — understands the journeys, personas, and technical design
2. **Decompose each journey into discrete user stories** — typically 3–8 stories per journey
3. **Write acceptance criteria** in four categories: Functional (Given/When/Then), Accessibility, Clinical Safety, Data Protection
4. **Present stories for review** — you approve, adjust, or add stories before they are saved
5. **Save each story** as a separate file in `user_stories/story-NNN-short-slug.md`

### Review the Stories (10 minutes)

Walk through as a team:
- Are any stories missing from the journeys?
- Should any be split further or merged?
- Is the priority order correct (riskiest assumption first)?
- Are the acceptance criteria testable?

Approve the stories before moving to scaffold.

---

## Phase 3 — Scaffold & Deploy (1.5 hours)

### Iteration 0 — Scaffold the Service

**Agent**: NHS Service Builder

> Scaffold the NHS Alpha service based on the architecture in `docs/adr/001-architecture.md`. Create the backend with a health endpoint, the frontend with NHS Design System components and a start page, and the infrastructure configuration. Deploy everything.

The agent will:
- Create the backend app structure with middleware
- Scaffold the frontend with NHS Design System components
- Write infrastructure-as-code configuration
- Deploy infrastructure and application
- Verify the health endpoint returns 200 on the live URL

---

## Phase 4 — Build User Stories (3.5+ hours)

This is the core of Day 1. The agent will build all user stories from `user_stories/` in a single session.

**Agent**: NHS Service Builder

> Build all the user stories from `user_stories/`. Implement every story: API endpoints, frontend pages using NHS Design System components, tests, and Playwright E2E tests. Deploy when complete.

The agent reads each story's acceptance criteria to drive implementation and testing. It uses `discovery/user_journeys/data/` for E2E test flow context. Monitor progress and provide guidance if it asks questions.

### While the Agent Works

- Watch for questions — the agent may need clarification on specific stories
- Review the code as it's created — catch design issues early
- If you spot problems, steer the agent in the chat
- Switch agents mid-session if needed (e.g. **Security Reviewer** for a quick audit)

### End of Day — Review & Commit (15 minutes)

1. Ensure all code is committed and pushed
2. Verify the service works end-to-end on the live URL
3. Note which user stories are complete and which remain — these inform Day 2 priorities

---

## Tips

- **Arrive with discovery done** — the workshop is for building, not researching
- **Architecture first** — do not scaffold until the architecture is agreed
- **Let the agent do the work** — it will build all stories in one session
- **Switch agents** — use the right agent for the right job
- **Verify on the live URL** — always check the deployed service after deployment
- **Commit frequently** — the agent can run `git commit` for you
- **Save discovery artefacts** — scenarios, personas, and journeys are evidence for GDS assessment on Day 2
