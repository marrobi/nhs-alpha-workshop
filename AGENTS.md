# UKHSA Service — Copilot Coding Agent Context

## Quality Expectations

**All code must be production-quality.** The GDS delivery phase does not affect code standards. Write fully tested, secure, accessible, and maintainable code. Never skip validation, error handling, security controls, tests, or accessibility. Do not take shortcuts.

## Rules

- **Do not create or update documentation files** (README, markdown docs, MKdocs, ADRs, etc.) unless the user explicitly requests it. Focus on code, tests, and infrastructure.

## Project Description

This is a UKHSA digital service built with GitHub Copilot agents. The service follows the GOV.UK Design System with UKHSA branding and GDS Service Standard, deployed to Azure using Terraform.

## Tech Stack

See `.github/instructions/tech-stack.instructions.md` for current technology choices. Security rules in `.github/instructions/ukhsa-security.instructions.md`; organisational standards in `.github/instructions/org-standards.instructions.md`; coding standards in `.github/copilot-instructions.md`.

## Repository Structure

```
├── src/                    # .NET solution (see tech-stack.instructions.md)
│   ├── <ServiceName>.Web/           # ASP.NET Core MVC entry point
│   │   ├── Controllers/             # MVC controllers
│   │   ├── Views/                   # Razor views (.cshtml)
│   │   └── Program.cs               # App entrypoint
│   ├── <ServiceName>.Application/   # Use cases, DTOs, validation
│   ├── <ServiceName>.Domain/        # Domain model
│   └── <ServiceName>.Infrastructure/# EF Core, repositories, integrations
├── user_stories/           # User stories generated from journeys (Day 1)
│   └── story-NNN-slug.md   # One file per story with acceptance criteria
├── tests/
│   ├── <ServiceName>.UnitTests/      # xUnit unit tests
│   ├── <ServiceName>.IntegrationTests/# WebApplicationFactory tests
│   ├── <ServiceName>.E2ETests/       # Playwright for .NET tests
│   └── <ServiceName>.PerformanceTests/# k6 load tests (.k6.js)
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
├── <ServiceName>.sln       # .NET solution file
└── .gitignore
```

## Build, Test, and Lint Commands

```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run the application locally
dotnet run --project src/<ServiceName>.Web

# Run all tests
dotnet test

# Run unit tests only
dotnet test tests/<ServiceName>.UnitTests

# Run integration tests only
dotnet test tests/<ServiceName>.IntegrationTests

# Run E2E tests (Playwright for .NET)
dotnet test tests/<ServiceName>.E2ETests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Linter and formatter
dotnet format --verify-no-changes

# Terraform
cd infra && terraform init && terraform plan
```


