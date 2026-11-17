# Day 1 — Design and Build with Copilot

## Overview

Day 1 assumes **discovery has already been completed** using the discovery toolkit in this repository. You should arrive with a scenario, personas, and user journeys already produced. Day 1 is about designing the architecture, generating user stories from the journeys, recommending slices, recording architectural decisions as ADRs, building slice 0 (scaffold, CI and deployment), and then building the first slice. A 2-day workshop may build only 1 slice — choose a thin slice if you want to test the riskiest assumption early.

> **Prerequisites**: Complete discovery before the workshop using the [discovery guide](../../../discovery/README.md). You need:
> - `discovery/scenarios/scenario.md` — scenario overview and problem statement
> - `discovery/personas/persona-report.md` — researched NHS personas
> - `discovery/user_journeys/data/journey-*.md` — detailed user journeys with Mermaid diagrams
> - `discovery/open-questions.md` — open questions, with a direction for each question that blocks the first slice
> - `discovery/interface-register.md` — external systems, with a draft alpha approach (real, sandbox, mock or not used)
> - `discovery/discovery-notes.md`, reviewed by the team, with an `Agreed` "Steer" section: the priority journey, the riskiest assumption and a first slice candidate
>
> See the [discovery guide](../../../discovery/README.md) for how to produce these artefacts.

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

**Skills**: `nhs-architecture-design`, followed by `nhs-adr-writer` when creating ADRs.

> Read the discovery artefacts in `discovery/scenarios/`, `discovery/personas/`, and `discovery/user_journeys/data/`. Design the architecture for this service.

The agent will:

1. **Review the default tech stack** and ask if you want to change anything (e.g. add a database, change the frontend framework, add authentication)
2. **Update `tech-stack.instructions.md`** if you make any changes — this ensures all other agents use the correct stack
3. **Analyse your discovery artefacts** and confirm understanding
4. **Present architecture decision points** with 2–3 options each — you choose
5. **Compare candidate architectures** when decisions depend on each other — 2 or 3 options, each with a diagram and a comparison table — you choose
6. **Map user journeys to API endpoints, data models, and frontend pages**
7. **Prioritise journeys** — from your steer (priority journey and riskiest assumption) and the technical dependencies
8. **Write an ADR** at `docs/adr/001-architecture.md`
9. **Write the dependency strategy** at `docs/adr/002-dependency-strategy.md` — for each dependency, whether the alpha uses the real system, an NHS sandbox or an external mock service
10. **Generate a draw.io architecture diagram** at `docs/adr/architecture.drawio`, and export it to `docs/adr/architecture.png` with `scripts/export-diagrams.sh`

> **Diagrams are living artefacts.** The architecture diagram you generate here is a first draft, not a one-off deliverable. You will revisit it after the ADR review (Phase 4), as you build (Phase 6), and again on Day 2. Export each diagram to PNG with `scripts/export-diagrams.sh docs/adr` after each change so it always reflects the current design. You can also ask the **NHS Architect** agent for additional diagrams when they would help the team — for example a data model (entity-relationship) diagram, a sequence/data-flow diagram for a key journey, or a deployment diagram. Keep them alongside the architecture diagram in `docs/adr/`.

### Review the Architecture (15 minutes)

Walk through as a team:
- **Scenario & problem statement** — is the scope clear?
- **Personas** — who are the primary users?
- **User journeys** — which journeys will you build? Are they prioritised by riskiest assumption? (see [focus on testing your riskiest assumptions](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works#focus-on-testing-your-riskiest-assumptions))
- **ADR** — does the architecture make sense given the constraints?
- **Dependency strategy** — are you clear what is real and what is mocked? Is each mock a national system that you cannot reach in the alpha?
- **Diagram** — open `docs/adr/architecture.drawio` in VS Code (draw.io extension) or `docs/adr/architecture.png` to review visually. Is anything missing that a further diagram would make clearer (e.g. data model or a key journey's data flow)?

Agree the architecture before moving to user stories. Expect the diagram(s) to change as the design firms up — that is the point.

---

## Phase 2 — Generate User Stories (30 minutes)

Before building, decompose the user journeys into implementable user stories with acceptance criteria. This phase uses the **NHS Product Owner** agent.

**Agent**: NHS Product Owner

**Skills**: `nhs-product-backlog` and `nhs-user-stories`.

> Read the discovery artefacts in `discovery/scenarios/`, `discovery/personas/`, and `discovery/user_journeys/data/`, and the architecture in `docs/adr/001-architecture.md`. Decompose all user journeys into user stories with acceptance criteria.

The agent will:

1. **Read all discovery artefacts and the ADR** — understands the journeys, personas, and technical design
2. **Decompose each journey into discrete user stories** — typically 3–8 stories per journey
3. **Write acceptance criteria** in four categories: Functional (Given/When/Then), Accessibility, Clinical Safety, Data Protection
4. **Present stories for review** — you approve, adjust, or add stories before they are saved
5. **Save each story** as a separate file in `user_stories/story-NNN-short-slug.md`, with the first fields of its **Build context** section (depends on, answered questions, out of scope)

### Review the Stories (10 minutes)

Walk through as a team:
- Are any stories missing from the journeys?
- Should any be split further or merged?
- Are the acceptance criteria testable?

Approve the stories before moving to slices.

---

## Phase 3 — Recommend Slices (20 minutes)

Turn the flat list of stories into an ordered list of slices. This phase uses the **NHS Product Owner** agent.

**Agent**: NHS Product Owner

**Skill**: `nhs-product-backlog`.

> Recommend slices from the user stories in `user_stories/`, with their dependencies. Save the backlog to `user_stories/backlog.md`, then create one GitHub issue for each slice.

A **slice** is a set of stories that you build, review and show together. You choose the type of each slice:

- **Thin slice** — the shortest end-to-end path that proves one assumption. It can be part of a journey.
- **Journey slice** — one complete journey, including its failure paths.

In a 2-day workshop you may build only 1 slice after slice 0. A thin slice tests the riskiest assumption sooner.

The agent will:

1. **Ask which slice type you want** for the first slice, with a recommendation
2. **Recommend slices** — each with the assumption it tests, its stories, the slices it depends on, its external dependencies (real, sandbox or mock), and whether it can run in parallel with another slice
3. **Order the slices** — your steer and the riskiest assumption first, then the dependencies
4. **Save the backlog** to `user_stories/backlog.md`, with a dependency diagram. Slice 0 is the walking skeleton: scaffold, CI and deployment
5. **Create one GitHub issue for each slice** after you confirm the list. The issue links to the story files. The stories stay in Markdown

### Review the Slices (10 minutes)

Walk through as a team:

- Does the first slice test the riskiest assumption?
- Can each slice be shown end to end on the deployed service?
- Are the dependencies in the right order? Are the slices marked as parallel truly independent?

Approve the backlog before moving to ADR review.

---

## Phase 4 — Architecture ADR Review (30 minutes)

Now that user stories exist, run the **NHS Architect** agent a second time. The stories reveal detailed technical decisions (data models, integrations, error handling, auth flows) that were not visible during the initial architecture phase. This pass identifies the ADRs needed and creates them.

### Start the ADR Review Session

**Agent**: NHS Architect

**Skills**: `nhs-architecture-design` and `nhs-adr-writer`.

> Read the user stories in `user_stories/`, the architecture in `docs/adr/001-architecture.md`, and the discovery artefacts. Identify the architectural decisions that need to be recorded as ADRs based on the stories, then create them.

The agent will:

1. **Review user stories alongside the architecture** — identify decisions implied by the stories (e.g. data storage for specific entities, auth strategy for role-based access, API versioning, error handling patterns)
2. **Identify required ADRs** — present a list of ADR topics with rationale and confirm with the team
3. **Create the ADRs** — write each ADR to `docs/adr/` following the MADR format from the `nhs-adr-writer` skill
4. **Update the ADR index** — update `docs/adr/README.md` with the new records
5. **Update the architecture diagram** — revise `docs/adr/architecture.drawio` so it reflects the decisions captured in the new ADRs, and add any further diagrams that now help (e.g. a data model diagram for the chosen storage, or a sequence/data-flow diagram for an auth or integration flow). Export them all to PNG with `scripts/export-diagrams.sh docs/adr`
6. **Check the slice order** against the ADRs and the dependency strategy
7. **Complete the build context in each story** — ADRs, API contract, data model, pages and components, files to change, external dependencies, synthetic data and validation commands. A story with an open blocking question is marked `Not ready`

> **Why the build context matters**: a complete build context answers the questions an engineer would ask. Then the build agent does not need to reason about design decisions, so it is faster, uses fewer tokens, and can run on a lower-cost model.

### Review the ADRs (10 minutes)

Walk through as a team:
- Do the ADRs cover all significant decisions from the stories?
- Are any decisions missing (database choice, auth strategy, external integrations)?
- Are the alternatives and trade-offs realistic?
- Does the architecture diagram still match the design after the new ADRs? Would another diagram make a key flow clearer?
- Is each story in the first slice `Ready`? If not, get a direction for the blocking question now.

Approve the ADRs before moving to scaffold.

---

## Phase 5 — Slice 0: Scaffold, CI and Deploy (1.5 hours)

Slice 0 is the walking skeleton. It has two pull requests. CI comes first, so that every later pull request is checked.

### Slice 0a — Scaffold and CI

**Agent**: NHS Service Builder

**Skills**: `fastapi-react-azure`, `automated-testing`, `playwright-nhs-e2e`, and `github-actions-pipeline` (CI mode).

> Build slice 0a: scaffold the service from `docs/adr/001-architecture.md`, `docs/adr/002-dependency-strategy.md` and the ADRs in `docs/adr/`. Create the backend with a health endpoint, the frontend with NHS Design System components and a start page, the local data store containers, the Terraform (validate and plan only), and the CI pipeline. Open a pull request.

The agent will:

- Create the backend app structure with middleware, and the frontend with NHS Design System components
- Add the local data store containers from the dependency strategy
- Write the Terraform, including any external mock services, and run `terraform validate` and `terraform plan`
- Create `.github/workflows/ci.yml` and `.github/dependabot.yml`
- Open a pull request and stop

Then ask the **NHS Quality Engineer** to run `engineering-review` in Review mode on the pull request, with 2 engineers. Fix Critical and High findings. Merge after CI passes.

### Slice 0b — Azure Deployment

**Agent**: NHS Service Builder

**Skills**: `azure-nhs-deploy` and `github-actions-pipeline` (deploy mode).

> Build slice 0b: apply the Terraform, set up OIDC for GitHub Actions, and create the deploy pipeline. Open a pull request.

The agent will:

- Review the Terraform plan with you, then apply it after your `az login`
- Set up an OIDC federated credential and a GitHub environment. If you cannot create these, it stops and asks. With your agreement, it deploys from VS Code until OIDC is available
- Create `.github/workflows/deploy.yml`, which deploys on each merge to `main`
- Add "What is mocked in this alpha" to `docs/runbook.md`
- Open a pull request. After you merge it, verify that the health endpoint returns 200 on the live URL

---

## Phase 6 — Build Slices (3.5+ hours)

This is the core of Day 1. Build the slices in the order in `user_stories/backlog.md`, one GitHub issue and one pull request for each slice. Each slice can be shown end to end on the deployed service.

### Build → Review → Merge → QA → Demo → Repeat

For each slice:

1. **Build** — use the **NHS Service Builder** agent to build the stories in the slice. The agent reads the **Build context** in each story, builds API endpoints, frontend pages, tests and E2E tests, commits each verified story, and opens a pull request that links the slice issue. It is the only agent that writes to the checkout during the build.

   **Agent**: NHS Service Builder

   **Skills**: `fastapi-react-azure`, `automated-testing`, `playwright-nhs-e2e`, as needed.

   > Build slice [N] from its GitHub issue #[number]. Follow the Build context in each story. Open a pull request when CI passes locally.

   Because each story has its build context, you can run the build on a lower-cost model. Use a high-tier model for the review.

2. **Engineering review** — after the Service Builder reports the pull request, the exact commit SHAs and the validation results, ask the **NHS Quality Engineer** to run the `engineering-review` skill in Review mode. Two read-only engineers on different models review the same commits independently. The Quality Engineer combines their findings and gives a verdict: MERGE or FIX-THEN-MERGE. Give any Critical or High findings to the NHS Service Builder to fix before you merge.

   **Agent**: NHS Quality Engineer

   **Skill**: `engineering-review`.

   > Review mode: review commits [base SHA..final SHA, listing each commit SHA] for slice [N] (pull request #[number]) against the acceptance criteria of its stories, the ADRs and the repository rules. The Service Builder reported these validation results: [commands and results]. Use [model A] and [model B] for the engineers.

   If you leave out the models, the Quality Engineer asks you for them before it starts. Choose models from different providers. Run the Quality Engineer on a high-tier model.

   Do not let the Service Builder change the reviewed checkout while the review runs. If work must continue at the same time, create a separate checkout or detached worktree at the reviewed commits and run the review there.

   If the Service Builder meets a hard design question during the build, such as conflicting acceptance criteria or a security or clinical-safety trade-off, it stops and runs Decide mode itself with three engineers. It shows you the recommendation and continues after you agree. During architecture work, ask the **NHS Architect** to run Decide mode.

3. **Merge and deploy** — merge the pull request after CI passes and the review findings are fixed. The deploy pipeline deploys the slice.

4. **Visual QA** — use the **NHS Quality Engineer** with the `visual-qa` skill to review each page in the slice.

   Capture each page at desktop and mobile sizes. Walk through the slice's path. Check that the rendered content matches the API data. Run the review in VS Code. Start the app locally first, then check the deployed URL.

   **Agent**: NHS Quality Engineer

   **Skill**: `visual-qa`.

   > Review the pages and the path that slice [N] delivers (stories [story-001, story-002]). Check layouts, NHS Design System components, form validation, navigation, and data correctness at both desktop and mobile viewports.

5. **Fix** — the NHS Quality Engineer fixes issues found in layout, data, and navigation, in a new pull request. Let it iterate until clean.

6. **Demo** — walk through the slice end-to-end on the deployed service to confirm it works and can be demonstrated.

7. **Readiness loop** — the hardening and evidence for the slice. On Day 2, run it for each slice built on Day 1. See the [Day 2 guide](day2-guide.md).

8. **Repeat** — move to the next slice in the backlog.

### Parallel Work

Build two slices at the same time only if `user_stories/backlog.md` marks them as parallel. Parallel work needs more memory and more review time, so in a 2-day workshop build one slice at a time unless the team has spare capacity.

For each parallel slice:

- Create a separate worktree: `git worktree add ../<repository>-slice-N slice-N-short-name`. Run one NHS Service Builder in each worktree.
- Use a separate database in the **shared** local data store container, as `docs/adr/002-dependency-strategy.md` says. Do not start one container for each worktree unless the ADR records that fallback. Many containers use too much memory.
- Use different local ports for each worktree.
- Open one pull request for each slice. Merge them one at a time, and rebase the other slice after each merge.

### While the Agents Work

- Watch for questions — the agents may need clarification on specific stories
- Review the code as it's created — catch design issues early
- If you spot problems, steer the agent in the chat
- Follow the build backlog — work through the slices in `user_stories/backlog.md` in order; each slice can be shown end to end once built
- If a story is `Not ready`, get a direction for its open question, then ask the NHS Architect to update the build context

### End of Day — Review & Commit (15 minutes)

1. Ensure all code is committed and pushed
2. Ask the **NHS Quality Engineer** to run the `engineering-review` skill in Review mode on the exact commits completed that day; three engineers on different models review this wider scope
3. Hand any Critical or High findings to the **NHS Service Builder**, then re-run the affected validation
4. Verify the service works end-to-end on the live URL
5. If building revealed architecture changes (new components, services, or data flows), ask the **NHS Architect** agent to update `docs/adr/architecture.drawio` so it matches what was actually built, and export it to PNG with `scripts/export-diagrams.sh docs/adr`
6. Note which slices are merged and which remain — Day 2 runs the readiness loop for each merged slice

---

## Tips

- **Arrive with discovery done** — the workshop is for building, not researching
- **Architecture first** — do not scaffold until the architecture is agreed
- **Build in slices** — build one slice at a time as a pull request, then review, merge, Visual QA and demo, then repeat
- **Prioritise the riskiest assumption first** — build the backlog in priority order so the riskiest assumptions are tested earliest
- **Choose the slice type** — a thin slice proves one assumption sooner; a journey slice completes a whole journey
- **Use skills for procedures** — keep the role agent and select the skill that matches the task
- **Verify on the live URL** — always check the deployed service after deployment
- **Keep diagrams alive** — the architecture diagram is a living artefact; update it (and add new diagrams) whenever the design changes, and export the PNGs again
- **Context in the stories** — the more questions the build context answers, the less the build agent has to reason
- **Commit frequently** — the agent can run `git commit` for you
- **Save discovery artefacts** — scenarios, personas, and journeys are evidence for GDS assessment on Day 2
