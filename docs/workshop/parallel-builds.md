# Parallel User Story Builds — Strategy

This guide describes how to build multiple user stories in parallel during Day 1 Phase 4 without merge conflicts. It applies when multiple developers or Copilot agent sessions work on different stories simultaneously.

## Problem

The default Day 1 workflow builds user stories sequentially — one batch at a time. This creates a bottleneck when multiple team members (or agent sessions) are available. Naïve parallelisation causes merge conflicts because stories often touch the same shared files:

- `app/main.py` — registering new routers
- `frontend/src/App.tsx` — adding new routes
- `tests/conftest.py` — adding shared fixtures
- `frontend/src/components/Layout.tsx` — modifying navigation links

## Strategy Overview

1. **Scaffold shared files once** before parallelising (Phase 3 — Scaffold & Deploy)
2. **Isolate each story** into its own files using naming conventions
3. **Use auto-discovery patterns** so shared files do not need editing when adding a new story
4. **Use feature branches** and merge stories one at a time
5. **Assign non-overlapping stories** to parallel streams

---

## 1. Auto-Discovery Patterns

The scaffold phase (Phase 3) sets up auto-discovery so that adding a new router or page does not require editing a shared file. This is the single most important technique for avoiding conflicts.

### Backend — Router Auto-Discovery

Instead of manually importing and registering each router in `app/main.py`, use a discovery pattern that finds all routers automatically:

```python
# app/main.py — scaffold this once in Phase 3
import importlib
import pkgutil
from fastapi import APIRouter

import app.routers

def discover_routers() -> list[APIRouter]:
    """Import all router modules and collect their router objects."""
    routers = []
    package = app.routers
    for importer, module_name, is_pkg in pkgutil.iter_modules(package.__path__):
        module = importlib.import_module(f"app.routers.{module_name}")
        if hasattr(module, "router"):
            routers.append(module.router)
    return routers

# In app setup:
for router in discover_routers():
    app.include_router(router)
```

With this pattern, each story creates a new file in `app/routers/` (e.g. `app/routers/appointments.py`) and exports a `router` object. No changes to `app/main.py` are required.

### Frontend — Route Auto-Discovery

Use a route registry pattern so that new pages register themselves without editing `App.tsx`:

```typescript
// frontend/src/routes.ts — scaffold this once in Phase 3
interface AppRoute {
  path: string;
  label: string;
  element: React.ReactNode;
}

// Each page module adds its route to this array
export const routes: AppRoute[] = [];

export function registerRoute(route: AppRoute): void {
  routes.push(route);
}
```

```typescript
// frontend/src/App.tsx — scaffold this once in Phase 3
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { routes } from './routes';

// Import all page modules so they register their routes
import './pages';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {routes.map((route) => (
          <Route key={route.path} path={route.path} element={route.element} />
        ))}
      </Routes>
    </BrowserRouter>
  );
}
```

```typescript
// frontend/src/pages/index.ts — auto-imports all page modules
// Scaffold this once; each story adds a new page file that self-registers

// Story-specific page modules are imported here.
// Each page module calls registerRoute() on import.
```

Each story creates a new page file (e.g. `frontend/src/pages/AppointmentsPage.tsx`) that calls `registerRoute()` on import, then adds its import to `frontend/src/pages/index.ts`. The only shared touchpoint is the single-line import in `pages/index.ts`, which is a trivial merge conflict if it occurs.

---

## 2. File Isolation Conventions

Each user story should create its own files and avoid modifying files owned by other stories.

### File Ownership

| File pattern | Owner | Parallel-safe? |
|---|---|---|
| `app/routers/<resource>.py` | Story that introduces the resource | ✅ Yes — one file per resource |
| `app/models/<resource>.py` | Story that introduces the resource | ✅ Yes — one file per resource |
| `frontend/src/pages/<Page>.tsx` | Story that introduces the page | ✅ Yes — one file per page |
| `frontend/src/components/<Component>.tsx` | Shared — first story to need it | ⚠️ Caution — assign to one stream |
| `tests/unit/test_<resource>.py` | Story that introduces the resource | ✅ Yes — one file per resource |
| `tests/e2e/test_<journey>.py` | Story that introduces the journey | ✅ Yes — one file per journey |
| `app/main.py` | Scaffold (Phase 3) | ✅ Yes — auto-discovery, no edits needed |
| `frontend/src/App.tsx` | Scaffold (Phase 3) | ✅ Yes — route registry, no edits needed |
| `frontend/src/routes.ts` | Scaffold (Phase 3) | ✅ Yes — no edits needed |
| `frontend/src/pages/index.ts` | All stories (append-only) | ⚠️ Low-risk — append-only imports |
| `tests/conftest.py` | Scaffold (Phase 3) | ⚠️ Low-risk — add fixtures rarely |
| `infra/*.tf` | Scaffold (Phase 3) | ❌ Sequential — only modify in scaffold |

### Naming Conventions

- **Routers**: `app/routers/<plural_resource>.py` — e.g. `appointments.py`, `patients.py`
- **Models**: `app/models/<plural_resource>.py` — e.g. `appointments.py`, `patients.py`
- **Pages**: `frontend/src/pages/<PascalCase>Page.tsx` — e.g. `AppointmentsPage.tsx`, `PatientSearchPage.tsx`
- **Tests**: `tests/unit/test_<resource>.py` — e.g. `test_appointments.py`, `test_patients.py`
- **E2E tests**: `tests/e2e/test_<journey_slug>.py` — e.g. `test_book_appointment.py`

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
| A | story-001, story-004 | `appointments` | AppointmentsPage, BookingPage |
| B | story-002, story-005 | `patients` | PatientSearchPage, PatientDetailPage |
| C | story-003 | `prescriptions` | PrescriptionsPage |

### Rules for Assigning Streams

1. **Stories that share a resource** (same router, same page) go in the **same stream** — build them sequentially within that stream
2. **Stories that touch different resources** go in **different streams** — build them in parallel
3. **Shared components** (e.g. a navigation component used by multiple pages) should be created by whichever story needs them first; other streams wait for that story to merge before using the component
4. **Infrastructure changes** are sequential — only modify `infra/` in the scaffold phase or in a dedicated infrastructure story

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

### Test Fixtures (`tests/conftest.py`)

Scaffold the shared test client fixture in Phase 3. Story-specific fixtures belong in the story's own test file, not in `conftest.py`:

```python
# tests/conftest.py — scaffold once in Phase 3
import pytest
from httpx import ASGITransport, AsyncClient

from app.main import app


@pytest.fixture
async def client():
    transport = ASGITransport(app=app)
    async with AsyncClient(transport=transport, base_url="http://test") as ac:
        yield ac
```

```python
# tests/unit/test_appointments.py — story-specific fixtures stay here
import pytest


@pytest.fixture
def sample_appointment():
    return {
        "date": "2026-04-01",
        "time": "09:30",
        "clinician": "Dr Patel",
    }
```

### Frontend Shared Components

If two stories need the same shared component (e.g. a PatientBanner), assign it to the story that is built first. The second story's branch rebases onto `main` after the first merges, then uses the component.

---

## 6. Checklist — Before Parallelising

Use this checklist during Phase 3 (Scaffold & Deploy) to ensure the codebase is ready for parallel story builds:

- [ ] `app/main.py` uses router auto-discovery — no manual `include_router()` calls needed per story
- [ ] `frontend/src/App.tsx` uses a route registry — no manual `<Route>` entries needed per story
- [ ] `frontend/src/routes.ts` exports the route registry and `registerRoute` function
- [ ] `frontend/src/pages/index.ts` exists as the barrel file for page imports
- [ ] `tests/conftest.py` has the shared test client fixture
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
