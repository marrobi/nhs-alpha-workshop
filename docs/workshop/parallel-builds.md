# Parallel User Story Builds — Strategy

This guide describes how to build multiple user stories in parallel during Day 1 Phase 4 without merge conflicts. It applies when multiple developers or Copilot agent sessions work on different stories simultaneously.

> **Tech-stack agnostic**: The principles below apply regardless of which backend framework, frontend framework, or language the team selects in `tech-stack.instructions.md`. The patterns are described generically — the NHS Service Builder agent implements them using whatever stack is configured.

## Problem

The default Day 1 workflow builds user stories sequentially — one batch at a time. This creates a bottleneck when multiple team members (or agent sessions) are available. Naïve parallelisation causes merge conflicts because stories often touch the same shared files:

- **Application entrypoint** — registering new route handlers or controllers
- **Frontend root component** — adding new page routes
- **Shared test configuration** — adding test fixtures or helpers
- **Layout or navigation components** — modifying navigation links

These files act as bottlenecks: every story needs to edit them, so parallel branches always conflict.

## Strategy Overview

1. **Scaffold shared files once** before parallelising (Phase 3 — Scaffold & Deploy)
2. **Isolate each story** into its own files using naming conventions
3. **Use auto-discovery patterns** so shared files do not need editing when adding a new story
4. **Use feature branches** and merge stories one at a time
5. **Assign non-overlapping stories** to parallel streams

---

## 1. Auto-Discovery Patterns

The scaffold phase (Phase 3) sets up auto-discovery so that adding a new route handler or page does not require editing a shared file. This is the single most important technique for avoiding conflicts.

### Backend — Route Handler Auto-Discovery

Instead of manually importing and registering each route handler in the application entrypoint, the scaffold should use a discovery pattern that automatically finds and registers all handlers at startup:

- **Convention**: each story creates a new route handler module in a designated directory (e.g. `routes/`, `routers/`, `controllers/`), exporting a well-known object (e.g. a router, blueprint, or controller)
- **Discovery**: the application entrypoint scans that directory at startup, imports every module it finds, and registers any exported handler objects
- **Result**: adding a new story means creating a new file — the entrypoint does not need editing

Most backend frameworks have an idiomatic way to achieve this:

| Framework | Auto-discovery approach |
|---|---|
| FastAPI (Python) | Scan `app/routers/` with `pkgutil.iter_modules`, import each module, register any `router` attribute |
| Django (Python) | Use `django.urls.path` with `include()` — each app registers its own `urls.py` |
| Express (Node.js) | Scan `routes/` with `fs.readdirSync`, `require()` each file, mount with `app.use()` |
| Spring Boot (Java) | Component scanning — annotated `@RestController` classes are auto-discovered |
| ASP.NET (C#) | Convention-based controller discovery via `AddControllers()` |

The NHS Service Builder agent should implement the appropriate pattern for the configured stack during Phase 3 scaffolding.

### Frontend — Route Auto-Discovery

Use a route registry pattern so that new pages register themselves without editing the root application component:

- **Convention**: each story creates a new page module in a designated directory (e.g. `pages/`), which self-registers its route on import
- **Registry**: a central route registry module exports a list of routes and a registration function; the root component reads from this list to render routes
- **Barrel file**: a barrel/index file in the pages directory imports all page modules, triggering their self-registration

The only shared touchpoint is the single-line import added to the barrel file, which is a trivial merge conflict if it occurs.

If the frontend framework supports file-based routing (e.g. Next.js, Nuxt, SvelteKit, Remix), auto-discovery is built in — each story simply creates a new file in the pages/routes directory and no shared files need editing.

### Navigation Links

Navigation menus that list pages should also be driven by the route registry or a similar data structure, so that adding a new page does not require editing a shared navigation component. The scaffold should wire the navigation to read from the same registry that powers routing.

---

## 2. File Isolation Conventions

Each user story should create its own files and avoid modifying files owned by other stories.

### File Ownership

| Layer | File pattern | Owner | Parallel-safe? |
|---|---|---|---|
| **Backend routes** | One file per resource in the routes directory | Story that introduces the resource | ✅ Yes |
| **Data models** | One file per resource in the models directory | Story that introduces the resource | ✅ Yes |
| **Frontend pages** | One file per page in the pages directory | Story that introduces the page | ✅ Yes |
| **Shared UI components** | One file per component in the components directory | First story to need it | ⚠️ Caution — assign to one stream |
| **Unit tests** | One test file per resource | Story that introduces the resource | ✅ Yes |
| **E2E tests** | One test file per user journey | Story that introduces the journey | ✅ Yes |
| **App entrypoint** | Single file — uses auto-discovery | Scaffold (Phase 3) | ✅ No edits needed |
| **Frontend root** | Single file — reads from route registry | Scaffold (Phase 3) | ✅ No edits needed |
| **Route registry** | Single file | Scaffold (Phase 3) | ✅ No edits needed |
| **Page barrel file** | Single file — append-only imports | All stories | ⚠️ Low-risk |
| **Shared test config** | Single file (e.g. conftest, setup) | Scaffold (Phase 3) | ⚠️ Low-risk |
| **Infrastructure** | IaC files | Scaffold (Phase 3) | ❌ Sequential only |

### Naming Conventions

Agree naming conventions during Phase 3 so that parallel streams do not create conflicting file names. General principles:

- **Route handlers**: one file per resource, named after the resource (plural) — e.g. `appointments`, `patients`
- **Data models**: one file per resource, named after the resource (plural)
- **Pages**: one file per page, named descriptively — e.g. `AppointmentsPage`, `PatientSearchPage`
- **Tests**: one file per resource or journey, prefixed with `test_` or suffixed with `.test` per framework convention
- **E2E tests**: one file per user journey, named after the journey slug

---

## 3. Branching Workflow

### Branch Strategy

Use a feature branch per story (or per small batch of tightly coupled stories):

```
main
 ├── story/001-view-appointments      (developer A or agent session 1)
 ├── story/002-book-appointment        (developer B or agent session 2)
 ├── story/003-patient-search          (developer C or agent session 3)
 └── story/004-cancel-appointment      (queued — depends on 001)
```

### Rules

1. **Branch from `main`** (or from the latest merged state) — never branch from another story branch
2. **Name branches** `story/<NNN>-<slug>` matching the user story file name
3. **Merge one at a time** — use pull requests, merge to `main`, then rebase remaining branches
4. **Resolve conflicts immediately** — if a rebase surfaces conflicts, resolve them before continuing work on the branch
5. **Do not force-push `main`** — branch protection should prevent this

### Merge Order

Merge stories in priority order (riskiest assumption first) to keep the highest-value stories integrated earliest. If two stories have no dependency relationship, merge whichever is ready first.

---

## 4. Assigning Stories to Parallel Streams

Not all stories can be built in parallel. Analyse dependencies before assigning work.

### Dependency Analysis

Before parallelising, group stories by the resources and pages they touch:

| Stream | Stories | Resources | Pages |
|---|---|---|---|
| A | story-001, story-004 | `appointments` | Appointments list, Booking form |
| B | story-002, story-005 | `patients` | Patient search, Patient detail |
| C | story-003 | `prescriptions` | Prescriptions list |

### Rules for Assigning Streams

1. **Stories that share a resource** (same route handler, same page) go in the **same stream** — build them sequentially within that stream
2. **Stories that touch different resources** go in **different streams** — build them in parallel
3. **Shared components** (e.g. a navigation component or patient banner used by multiple pages) should be created by whichever story needs them first; other streams wait for that story to merge before using the component
4. **Infrastructure changes** are sequential — only modify IaC files in the scaffold phase or in a dedicated infrastructure story

### Example Timeline

```
Time →

Stream A: [scaffold] → [story-001] → [merge] → [story-004] → [merge]
Stream B: [scaffold] → [story-002] ———————————→ [merge] → [story-005] → [merge]
Stream C: [scaffold] ——→ [story-003] ——→ [merge]

Scaffold is shared (Phase 3, sequential).
Stories in different streams run in parallel.
Merges happen one at a time to main.
```

---

## 5. Handling Shared Fixtures and Configuration

### Shared Test Configuration

Scaffold the shared test client or setup fixture in Phase 3. Story-specific fixtures and test data belong in the story's own test file, not in the shared configuration:

- **Shared config** (scaffolded once): test client setup, base URL, common helpers
- **Story-specific** (per story file): sample data, resource-specific fixtures, mock responses

This prevents the shared test configuration file from becoming a merge conflict hotspot.

### Shared UI Components

If two stories need the same shared component (e.g. a PatientBanner), assign it to the story that is built first. The second story's branch rebases onto `main` after the first merges, then uses the component.

---

## 6. Checklist — Before Parallelising

Use this checklist during Phase 3 (Scaffold & Deploy) to ensure the codebase is ready for parallel story builds:

- [ ] Application entrypoint uses route handler auto-discovery — no manual registration needed per story
- [ ] Frontend root component reads routes from a registry or uses file-based routing — no manual route entries needed per story
- [ ] Page barrel/index file exists for importing new page modules (if not using file-based routing)
- [ ] Shared test configuration has the test client fixture scaffolded
- [ ] Navigation component is driven by the route registry — no manual link additions needed per story
- [ ] Stories are grouped into streams by resource — no two parallel streams touch the same resource
- [ ] Feature branch naming convention agreed: `story/<NNN>-<slug>`
- [ ] Branch protection enabled on `main` (no force-push, PR required)

---

## When Not to Parallelise

Sequential builds are safer and simpler. Parallelise only when:

- Multiple team members or agent sessions are available simultaneously
- There are enough independent stories (touching different resources) to fill parallel streams
- The scaffold phase is complete and auto-discovery is in place

If the team has a single Copilot agent session or a single developer, follow the standard sequential workflow in the [Day 1 guide](day1-guide.md).
