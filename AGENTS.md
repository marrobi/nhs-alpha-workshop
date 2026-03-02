# NHS Alpha — Copilot Coding Agent Context

## Rules

- **Do not create or update documentation files** (README, markdown docs, MKdocs, ADRs, etc.) unless the user explicitly requests it. Focus on code, tests, and infrastructure.

## Project Description

This is an NHS Alpha-phase digital service built during a 2-day workshop using GitHub Copilot agents. The service follows the NHS Design System and GDS Service Standard, deployed to Azure using Terraform.

## Tech Stack

See `.github/instructions/tech-stack.instructions.md` for current technology choices. Security rules in `.github/instructions/nhs-security.instructions.md`; coding standards in `.github/copilot-instructions.md`.

## Repository Structure

```
├── app/                    # Backend application (see tech-stack.instructions.md)
│   ├── routers/            # API route modules
│   ├── middleware/          # Application middleware
│   └── main.py             # App entrypoint
├── frontend/               # Frontend application (see tech-stack.instructions.md)
│   ├── src/
│   │   ├── components/     # UI components (NHS Design System)
│   │   ├── pages/          # Page components
│   │   └── App.tsx         # Root component
│   ├── package.json
│   └── vite.config.ts
├── tests/
│   ├── unit/               # Backend unit tests
│   ├── integration/        # API integration tests
│   ├── e2e/                # Browser tests
│   └── performance/        # Load tests
├── infra/                  # Terraform configuration
│   ├── main.tf
│   ├── variables.tf
│   ├── outputs.tf
│   └── terraform.tfvars
├── docs/
│   ├── adr/                # Architectural Decision Records
│   ├── clinical-safety/    # DCB0129 hazard log, safety case
│   └── dpia/               # Data Protection Impact Assessment
├── .github/
│   ├── agents/             # Custom Copilot agents
│   ├── instructions/       # Auto-applied coding instructions
│   ├── skills/             # Agent skills (SKILL.md folders)
│   └── workflows/          # GitHub Actions
├── AGENTS.md               # This file — Copilot Coding Agent context
├── requirements.txt        # Backend dependencies
└── .gitignore
```

## Build, Test, and Lint Commands

```bash
# Install backend dependencies
pip install -r requirements.txt

# Install frontend dependencies
cd frontend && npm ci && cd ..

# Run the backend locally
uvicorn app.main:app --reload --port 8000

# Run the frontend dev server
cd frontend && npm run dev

# Run backend tests
pytest

# Run unit tests only
pytest tests/unit/

# Run integration tests only
pytest tests/integration/

# Run E2E tests
pytest tests/e2e/ --browser chromium

# Run backend linter
ruff check . && ruff format --check .

# Run frontend linter
cd frontend && npm run lint

# Terraform
cd infra && terraform init && terraform plan
```


