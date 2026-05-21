# UKHSA Service — Copilot Instructions

You are working on a UK Health Security Agency (UKHSA) digital service. Follow these project-wide standards in every interaction.

## Quality Expectations

**"Alpha" refers to the [GDS delivery phase](https://www.gov.uk/service-manual/agile-delivery/how-the-alpha-phase-works) — it does NOT mean lower-quality code, incomplete implementations, or relaxed standards.** All code must be production-quality: fully tested, secure, accessible, and maintainable. Do not skip validation, error handling, security controls, tests, or accessibility requirements. Every standard in this file applies in full — no exceptions based on project phase.

UKHSA standards use [RFC 2119](https://datatracker.ietf.org/doc/html/rfc2119) terminology (MUST / MUST NOT / SHOULD / SHOULD NOT / MAY) as contextualised in the [UKHSA Technology Radar](https://ukhsa-collaboration.github.io/standards-org/tech-radar/). Treat MUST rules as non-negotiable unless a formally approved exception is in place.

## Product Context


UKHSA is an executive agency of the Department of Health and Social Care (DHSC) responsible for health protection — infectious disease surveillance, outbreak response, chemical/radiation/nuclear hazards, and climate-and-health threats. Many UKHSA digital services handle UK GDPR special category (Art. 9) data, and some are computerised systems supporting medicinal product supply under MHRA Good Distribution Practice (GDP) — design and implementation decisions must take the applicable regulatory context into account.

The service follows:

- The [GDS Service Standard](https://www.gov.uk/service-manual/service-standard) (14 points)
- The [GOV.UK Design System](https://design-system.service.gov.uk/) with UKHSA header and footer branding applied via layout override — do not introduce Bootstrap, Tailwind, Material UI, or bespoke components when a GOV.UK Design System component exists
- The [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/) — organisational standards, API design guidelines, development standards, ways of working, technology radar, and QAT
- The Government Digital and Data (GDAD, formerly DDaT) Framework
- Where applicable to the service: [MHRA GDP](https://www.gov.uk/government/publications/good-distribution-practice/good-distribution-practice) and **Annex 11 equivalent** requirements for computerised systems

See `.github/instructions/tech-stack.instructions.md` for the full pinned versions of the technology stack on this service.

## General Standards

- Write clean, readable code with meaningful names — prefer clarity over cleverness
- Follow existing patterns in the codebase before introducing new ones
- Keep functions small and focused — one function, one job
- Write tests alongside implementation — aim for 80%+ coverage
- Use type hints on all function signatures
- Use async/await for I/O-bound operations (FastAPI is async-first)
- Handle errors explicitly — never swallow errors silently
- Follow [.NET runtime coding style](https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md) enforced via `.editorconfig`, `dotnet format`, and StyleCop / Roslyn analyzers in CI
- Handle errors explicitly — never swallow exceptions silently; never `catch (Exception) { }`. Log with structured properties via NLog → Application Insights, including a correlation identifier on every entry.
- **No silent fallback values** — never provide fallback/default values for required configuration (connection strings, URLs, API keys, secrets). Code must fail explicitly with a clear error when a dependency is missing. Use strongly-typed options bound with `ValidateDataAnnotations()` + `ValidateOnStart()`, or `configuration["X"] ?? throw new InvalidOperationException(...)`. **Never** use `?? "default-value"` or `GetValue<string>("X", "fallback")` for required settings.
- **No unauthorised mocking of services** — do not mock, stub, or fake Azure services, government APIs (e.g. GOV.UK Notify), UKHSA APIs, databases, or other external dependencies unless there is an explicit user story or assumption authorising it. Unit tests may mock external calls for isolation, but integration tests, E2E tests, and application code must use real services or formally agreed sandbox/mock endpoints. If a dependency is unavailable, the code must fail — not silently degrade to a local substitute. For services with no sandbox, an ADR authorising the integration is sufficient justification — see the Mocking Boundary in `testing.instructions.md`.

## UK-HSA Specific Rules

- **Never include real personal data, health protection data, or operational records** — use synthetic identifiers, synthetic names, synthetic account numbers, synthetic organisation codes, and placeholder dates. Apply to source, tests, fixtures, seed data, screenshots in PRs, and example payloads.
- **All user-facing pages MUST use `GovUk.Frontend.AspNetCore` GDS tag helpers and components**, with the UKHSA header/footer layout. Do not use Bootstrap, Tailwind, Material UI, or bespoke components when a GOV.UK Design System component exists.
- **Follow the [GOV.UK content style guide](https://www.gov.uk/guidance/style-guide)** — plain English, short sentences, active voice, sentence case. UKHSA services are public-facing government services and must read like them.
- **WCAG 2.2 Level AA is mandatory** — required by the [Public Sector Bodies (Websites and Mobile Applications) (No. 2) Accessibility Regulations 2018](https://www.gov.uk/guidance/accessibility-requirements-for-public-sector-websites-and-apps). Verify with automated and manual checks. Publish and maintain an accessibility statement.
- **Personal data is UK GDPR personal data**; health data is special category under Art. 9 — always consider data protection implications, lawful basis, minimisation, and retention. A DPIA must be signed off by the UKHSA DPO before user testing involving real data.
- **API design MUST follow the [UKHSA API Design Guidelines](https://ukhsa-collaboration.github.io/standards-api/)** — RESTful conventions, OpenAPI specifications, and consistent error formats. ASP.NET Core controllers must expose OpenAPI via Swashbuckle.
- **Correlation identifiers are required** — generate a `CorrelationId` at the start of every user journey or request, persist it on the relevant record, include it in every log entry, every outbound API call header, every GOV.UK Notify template personalisation, and every user-visible confirmation reference. Never log without one.
- **Tag all infrastructure and code with the owning UKHSA directorate / product** to support cost attribution and ownership tracking.

## Security

Follow `.github/instructions/ukhsa-security.instructions.md` (auto-applied to all files) for OWASP Top 10, secrets management, input validation, PII / special-category logging rules, and dependency pinning. Highlights:

## Organisational Standards

Follow `.github/instructions/org-standards.instructions.md` (auto-applied to all files) for organisational policies that apply across all services — deployment strategy, data durability, test coverage thresholds, secret scanning, coding standards, and security requirements. Standards defined in `.github/instructions/org-standards.instructions.md` take precedence over any values hardcoded in other files. These local standards must remain consistent with the published [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/) — if there is a conflict, the published organisational standards win and the local file must be updated.

## Infrastructure

Follow `.github/instructions/terraform-azure-ukhsa.instructions.md` (auto-applied to `infra/` and `.tf` files) for Terraform, Azure, Managed Identity, and Key Vault standards.
