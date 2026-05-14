# UKHSA Service — Copilot Instructions

You are working on a UKHSA digital service. Follow these project-wide standards in every interaction.

## Quality Expectations

**"Alpha" refers to the [GDS delivery phase](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works) — it does NOT mean lower-quality code, incomplete implementations, or relaxed standards.** All code must be production-quality: fully tested, secure, accessible, and maintainable. Do not skip validation, error handling, security controls, tests, or accessibility requirements. Every standard in this file applies in full — no exceptions based on project phase.

## Product Context

This is a UK Health Security Agency (UKHSA) digital service following the [GDS Service Standard](https://www.gov.uk/service-manual/service-standard) (14 points) and [GOV.UK Design System](https://design-system.service.gov.uk). See `.github/instructions/tech-stack.instructions.md` for the current technology stack.

## General Standards

- Write clean, readable code with meaningful names — prefer clarity over cleverness
- Follow existing patterns in the codebase before introducing new ones
- Keep functions small and focused — one function, one job
- Write tests alongside implementation — aim for 80%+ coverage
- Use type hints on all function signatures
- Use async/await for I/O-bound operations (FastAPI is async-first)
- Handle errors explicitly — never swallow errors silently
- Follow [PEP 8](https://peps.python.org/pep-0008/) via `ruff` linter/formatter
- **No silent fallback values** — never provide fallback/default values for required configuration (env vars, URLs, secrets). Code must fail explicitly with a clear error when a dependency is missing. Use `os.environ["VAR"]` (raises `KeyError`) instead of `os.environ.get("VAR", "default")`. In JavaScript/k6, validate and throw instead of using `||` fallbacks.
- **No unauthorised mocking of services** — do not mock, stub, or fake cloud services, NHS APIs, databases, or other external dependencies unless there is an explicit user story requesting that mock. Unit tests may mock external calls for isolation, but integration tests, E2E tests, and application code must use real services or real sandbox environments. If a dependency is unavailable, the code must fail — not silently degrade to a local substitute. For cloud services with no local emulator (e.g. hosted AI/LLM APIs), see the Mocking Boundary in `testing.instructions.md` — an ADR authorising the integration is sufficient justification.

## UKHSA-Specific Rules

- **Never include real patient data** — use synthetic NHS numbers (e.g. `943 476 5919`), synthetic names, and placeholder dates
- **All user-facing pages must use `govuk-frontend` components** — see the [GOV.UK Design System](https://design-system.service.gov.uk)
- **Follow the [GOV.UK content design guidance](https://www.gov.uk/guidance/content-design)** — plain English, short sentences, active voice
- **WCAG 2.2 Level AA is mandatory** — see [GOV.UK accessibility guidance](https://www.gov.uk/service-manual/helping-people-to-use-your-service/making-your-service-accessible-an-introduction)
- **Health data is UK GDPR special category (Art. 9)** — always consider data protection implications

## Security

Follow `.github/instructions/ukhsa-security.instructions.md` (auto-applied to all files) for OWASP Top 10, secrets management, input validation, PII logging rules, and dependency pinning.

## Organisational Standards

Follow `.github/instructions/org-standards.instructions.md` (auto-applied to all files) for organisational policies that apply across all services — deployment strategy, data durability, test coverage thresholds, secret scanning, coding standards, and security requirements. Standards defined in `.github/instructions/org-standards.instructions.md` take precedence over any values hardcoded in other files.

## Infrastructure

Follow `.github/instructions/terraform-azure-ukhsa.instructions.md` (auto-applied to `infra/` and `.tf` files) for Terraform, Azure UK South, Managed Identity, and Key Vault standards.
