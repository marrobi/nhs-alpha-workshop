# Day 1 — Design and Build with Copilot

## Overview

Day 1 assumes **discovery has already been completed** using the [product-dev-copilot](https://github.com/marrobi/product-dev-copilot) toolkit. You should arrive with a scenario, personas, and user journeys already produced. Day 1 is about designing the architecture and then building as many user stories as possible.

> **Prerequisites**: Complete discovery before the workshop using the [product-dev-copilot](https://github.com/marrobi/product-dev-copilot) toolkit. You need:
> - `scenarios/scenario.md` — scenario overview and problem statement
> - `personas/persona-report.md` — researched NHS personas
> - `user_journeys/data/journey-*.md` — detailed user journeys with Mermaid diagrams
>
> See the [discovery guide](discovery-guide.md) for how to produce these artefacts.

## Setup (30 minutes)

1. Create a new repo from this template
2. Clone and open in VS Code Insiders
3. Copy your discovery artefacts (`scenarios/`, `personas/`, `user_journeys/`) into the repo
4. Verify prerequisites:
   ```bash
   python --version    # 3.12+
   node --version      # 20+
   terraform --version
   az --version
   az login
   ```
5. Set your Azure subscription: `az account set --subscription <id>`

---

## Phase 1 — Architecture Design (1 hour)

Before writing any code, design the technical architecture informed by your discovery artefacts. This phase uses the **NHS Architect** agent, which will ask you questions and present options interactively.

### Start the Architecture Session

**Agent**: NHS Architect

> Read the discovery artefacts in `scenarios/`, `personas/`, and `user_journeys/data/`. Design the architecture for this service.

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

Agree the architecture before moving to build.

---

## Phase 2 — Scaffold & Deploy (1.5 hours)

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

## Phase 3 — Build User Stories (4+ hours)

This is the core of Day 1. The agent will build all user stories from your discovery journeys in a single session.

**Agent**: NHS Service Builder

> Build all the user stories from the user journeys in `user_journeys/data/`. Implement every story: API endpoints, frontend pages using NHS Design System components, tests, and Playwright E2E tests. Deploy when complete.

The agent will work through all stories, creating API endpoints, frontend pages, tests, and E2E tests for each. Monitor progress and provide guidance if it asks questions.

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
