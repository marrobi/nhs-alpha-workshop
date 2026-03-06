# NHS Alpha — Copilot Instructions

You are working on an NHS Alpha-phase digital service. Follow these project-wide standards in every interaction.

## Product Context

This is a UK National Health Service (NHS) digital service in the Alpha assessment phase, following the [GDS Service Standard](https://www.gov.uk/service-manual/service-standard) (14 points) and [NHS Design System](https://service-manual.nhs.uk/design-system). See `.github/instructions/tech-stack.instructions.md` for the current technology stack.

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
- **No unauthorised mocking of services** — do not mock, stub, or fake Azure services, NHS APIs, databases, or other external dependencies unless there is an explicit user story requesting that mock. Unit tests may mock external calls for isolation, but integration tests, E2E tests, and application code must use real services or real sandbox environments. If a dependency is unavailable, the code must fail — not silently degrade to a local substitute.

## NHS-Specific Rules

- **Never include real patient data** — use synthetic NHS numbers (e.g. `943 476 5919`), synthetic names, and placeholder dates
- **All user-facing pages must use `nhsuk-frontend` components** — see the [NHS Design System](https://service-manual.nhs.uk/design-system)
- **Follow the [NHS content style guide](https://service-manual.nhs.uk/content)** — plain English, short sentences, active voice
- **WCAG 2.2 Level AA is mandatory** — see [NHS accessibility guidance](https://service-manual.nhs.uk/accessibility)
- **Health data is UK GDPR special category (Art. 9)** — always consider data protection implications

## Security

Follow `.github/instructions/nhs-security.instructions.md` (auto-applied to all files) for OWASP Top 10, secrets management, input validation, PII logging rules, and dependency pinning.

## Infrastructure

Follow `.github/instructions/terraform-azure-nhs.instructions.md` (auto-applied to `infra/` and `.tf` files) for Terraform, Azure UK South, Managed Identity, and Key Vault standards.
