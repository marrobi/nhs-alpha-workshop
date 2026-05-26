# UKHSA — Copilot Coding Agent Context

## Quality Expectations

**All code must be production-quality.** The GDS delivery phase does not affect code standards. Write fully tested, secure, accessible, and maintainable code. Never skip validation, error handling, security controls, tests, or accessibility. Do not take shortcuts.

## Rules

- **Do not create or update documentation files** (README, markdown docs, MKdocs, ADRs, etc.) unless the user explicitly requests it. Focus on code, tests, and infrastructure.

## Project Description

This is a UKHSA digital service built with GitHub Copilot agents. The service follows the GOV.UK Design System and GDS Service Standard, deployed to Azure using Terraform.

## Tech Stack

See `.github/instructions/tech-stack.instructions.md` for current technology choices. Security rules in `.github/instructions/ukhsa-security.instructions.md`; organisational standards in `.github/instructions/org-standards.instructions.md`; coding standards in `.github/copilot-instructions.md`.

## Repository Structure

```
├── src/
│   ├── ImmForm.Web/            # ASP.NET Core MVC — multi-step form UI (Razor views)
│   ├── ImmForm.Api/            # ASP.NET Core Web API — real service endpoints
│   └── ImmForm.Mocks/          # ASP.NET Core Minimal API — alpha stubs (not deployed to production)
├── user_stories/           # User stories generated from journeys (Day 1)
│   └── story-NNN-slug.md   # One file per story with acceptance criteria
├── tests/
│   ├── ImmForm.Tests.Unit/         # Unit tests — mirror src/ structure
│   ├── ImmForm.Tests.Integration/  # Integration tests via WebApplicationFactory
│   ├── ImmForm.Tests.E2E/          # Playwright browser tests
│   └── performance/                # k6 load tests
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
└── .gitignore
```

## Build, Test, and Lint Commands

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the web app locally
dotnet run --project src/ImmForm.Web

# Run the API locally
dotnet run --project src/ImmForm.Api

# Run all tests
dotnet test

# Run unit tests only
dotnet test tests/ImmForm.Tests.Unit/

# Run integration tests only
dotnet test tests/ImmForm.Tests.Integration/

# Run E2E tests
dotnet test tests/ImmForm.Tests.E2E/

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Format code
dotnet format

# Terraform
cd infra && terraform init && terraform plan
```


