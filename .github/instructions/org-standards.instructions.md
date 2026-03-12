---
applyTo: "**"
---

# Organisational Standards

This file defines the **non-negotiable organisational standards** that apply across all services, regardless of tech stack. It is the single source of truth for policies that every agent and instruction must respect.

> **This file is optional.** If your team has no specific organisational standards to enforce, delete the content below (keeping only the frontmatter). Agents will then apply only the baseline NHS and GDS standards already defined in the other instruction files.

The default standards below are drawn from the [NHS Architecture Manual](https://architecture.digital.nhs.uk/) and the [NHS Cloud Centre of Excellence Cloud Security Good Practice Guide](https://digital.nhs.uk/services/cloud-centre-of-excellence/cloud-security-good-practice-guide). Replace or extend them with your own organisation's policies.

---

## Architecture Principles

*Source: [NHS Architecture Manual](https://architecture.digital.nhs.uk/)*

- **Cloud First** — all services must be hosted on public cloud unless a clear, documented justification exists for an alternative; align with the [UK Government Cloud First policy](https://www.gov.uk/guidance/government-cloud-first-policy)
- **Open Standards** — use open web standards, open APIs, and open data formats by default; prefer FHIR R4 UK Core when exchanging clinical data
- **Reuse Before Buy Before Build** — demonstrate that existing NHS or government platforms (e.g. NHS Login, NHS Notify, NHS FHIR API) were evaluated before procuring or building new capability
- **Interoperability by Default** — expose data via versioned, documented APIs; avoid proprietary formats or protocols that prevent integration
- **Sustainable Services** — minimise resource consumption (compute, storage, data retention); consider environmental impact at design time
- **User-Centred Design** — all design decisions must be grounded in user research; services must meet the [GDS Service Standard](https://www.gov.uk/service-manual/service-standard) 14 points
- **Design for Accessibility** — WCAG 2.2 Level AA is the minimum; follow [NHS accessibility guidance](https://service-manual.nhs.uk/accessibility) and the [NHS Design System](https://service-manual.nhs.uk/design-system)

---

## Cloud & Deployment

*Source: [NHS Cloud Principles](https://digital.nhs.uk/services/cloud-centre-of-excellence/strategy/nhs-cloud-principles)*

- **Zero-downtime deployments required** — use blue/green or canary/staged rollout strategies; no maintenance windows that make a service unavailable to users
- **Rollback procedure must be documented** — every service must have a tested, documented rollback procedure before deployment to production
- **Infrastructure as Code** — all cloud infrastructure must be defined in code (Terraform for this stack); no manual changes to production environments
- **Immutable infrastructure** — prefer replacing infrastructure components over modifying them in place
- **Automated CI/CD** — all deployments must go through a CI/CD pipeline; no direct deployments from developer machines to production
- **Environment parity** — dev, staging, and production environments must use the same IaC definitions; environment-specific differences are limited to variables only

---

## Security

*Source: [NHS Cloud Security Good Practice Guide](https://digital.nhs.uk/services/cloud-centre-of-excellence/cloud-security-good-practice-guide) and [NCSC 14 Cloud Security Principles](https://www.ncsc.gov.uk/collection/cloud/the-cloud-security-principles)*

- **Secure by Design and by Default** — security controls must be built in from the start, not added after; default configuration must be the most secure option
- **Defence in Depth** — apply multiple independent layers of security controls; do not rely on a single control
- **Zero Trust Network Architecture** — assume breach; verify every request regardless of network origin; use identity-based access controls, not network-perimeter trust
- **Secret scanning in CI** — secret scanning (e.g. GitHub Advanced Security, `detect-secrets`, `truffleHog`) must run on every pull request; PRs with detected secrets must be blocked from merging
- **Dependency vulnerability scanning in CI** — automated dependency audit (e.g. `pip audit`, `npm audit`, Dependabot) must run on every PR; critical and high CVEs must be resolved before merge
- **Penetration testing** — services handling patient data must undergo penetration testing by a [NCSC CHECK](https://www.ncsc.gov.uk/information/check-penetration-testing) or [CREST](https://www.crest-approved.org/)-certified provider before go-live; findings must be tracked to resolution
- **Incident response** — a documented incident response plan must exist before go-live; significant breaches must be reported to the ICO within 72 hours per UK GDPR Art. 33
- **Audit logging** — all access to patient data and all administrative actions must be logged to a tamper-evident, centralised log store; logs must be retained for a minimum of 12 months
- **Least privilege** — all service accounts, managed identities, and human users must be granted only the minimum permissions required; review access rights regularly

---

## Data Durability & Backup

*Source: [NHS Cloud Security Good Practice Guide](https://digital.nhs.uk/services/cloud-centre-of-excellence/cloud-security-good-practice-guide)*

- **All persistent data must be backed up** to a geographically separate, immutable backup service; backups must be protected against ransomware (immutable/WORM storage)
- **Recovery Point Objective (RPO)** must be defined, documented, and tested for each service; default target: ≤ 1 hour for services handling patient data
- **Recovery Time Objective (RTO)** must be defined, documented, and tested for each service; default target: ≤ 4 hours for services handling patient data
- **Backup retention** — backups must be retained for a minimum of 90 days; retention periods must comply with NHS Records Management Code of Practice
- **Backup and recovery must be tested** — restoration must be tested at least quarterly; test results must be documented and reviewed
- **Data residency** — all data must remain within the UK; use Azure UK South (primary) and UK West (secondary) regions only

---

## Test Coverage

*Source: [NHS service standard and GDS quality requirements](https://www.gov.uk/service-manual/service-standard)*

- **Minimum 80% line coverage** enforced in CI — builds must fail if coverage drops below 80%
- **Minimum 80% branch coverage** enforced in CI
- **All API routes must have a happy-path test and at least one error-path test**
- **E2E tests must cover all critical user journeys** — journeys that affect patient safety or clinical decision-making must have automated E2E test coverage
- **Performance tests must run against realistic load targets** — see `performance.instructions.md` for p95/p99 thresholds

---

## Secret Scanning

*Source: [NHS Cloud Security Good Practice Guide](https://digital.nhs.uk/services/cloud-centre-of-excellence/cloud-security-good-practice-guide)*

- **Secret scanning must be enabled on all repositories** — use GitHub Advanced Security secret scanning or an equivalent tool
- **Secret scanning must run in CI as part of the PR workflow** — PRs with detected secrets must be blocked from merging automatically
- **Hardcoded credentials and keys are never permitted** — see `nhs-security.instructions.md` for the full secrets management policy
- **Historical secret exposure must be remediated** — if a secret is found in git history, it must be treated as compromised: rotate the secret immediately, then remove it from history

---

## Coding Standards

*Source: [NHS Architecture Manual](https://architecture.digital.nhs.uk/) and [GDS Service Standard Point 11](https://www.gov.uk/service-manual/service-standard/point-11-choose-the-right-tools-and-technology)*

- **Linter and formatter must be enforced in CI** — the tools are specified in `tech-stack.instructions.md`; CI must fail on lint errors
- **All function signatures must have type annotations** — in Python this means full type hints; in TypeScript this means explicit types on all function parameters and return values
- **No `TODO` or `FIXME` comments in merged code** — raise a GitHub Issue instead; CI should warn on (or block) `TODO`/`FIXME` in merged branches
- **PR review required before merge** — at least one approving review from a team member is required; branch protection must enforce this
- **No force-push to the default branch** — branch protection must prevent force-pushes and branch deletion on `main`/`master`
- **Dependency versions must be pinned exactly** — no loose version ranges (`>=`, `~=`, `^`) in production dependency files; see `nhs-security.instructions.md`

---

## Dependency Management

*Source: [NHS Cloud Security Good Practice Guide](https://digital.nhs.uk/services/cloud-centre-of-excellence/cloud-security-good-practice-guide)*

- **Pin exact versions for all production dependencies** — no loose ranges (`>=`, `~=`, `^`) in production dependency files
- **Automated dependency update tooling configured** — use Dependabot or Renovate to detect outdated and vulnerable dependencies
- **Dependency audit in CI fails on critical/high vulnerabilities** — `pip audit`, `npm audit`, or equivalent must run on every PR; critical and high CVEs must be resolved before merge
- **Docker images must use specific version tags** — never use `latest`; pin to a specific digest or version

---

## Observability

*Source: [NHS Architecture Manual](https://architecture.digital.nhs.uk/) and [GDS Service Standard Point 14](https://www.gov.uk/service-manual/service-standard/point-14-operate-a-reliable-service)*

- **Structured JSON logging required** — all application logs must be structured JSON, not plaintext
- **Correlation IDs (`X-Request-ID`) in every log entry** — all requests must include a correlation ID for distributed tracing
- **Health check endpoint required at `/health`** — every service must expose a health check endpoint that returns `{"status": "ok"}` and a 200 status code
- **Application performance monitoring configured** — use Azure Application Insights or equivalent; monitor response times, error rates, and availability
- **Alerting on error rate spikes** — configure alerts for sustained error rate increases or availability drops

---

## Change Management

*Source: [NHS Cloud Security Good Practice Guide](https://digital.nhs.uk/services/cloud-centre-of-excellence/cloud-security-good-practice-guide)*

- **All changes to production must go through a PR workflow** — no direct commits to the default branch
- **PR review required before merge** — at least one approving review from a team member
- **CI must pass before merge** — all quality gates (lint, test, coverage, security scan) must be green
- **Rollback procedure must be documented and tested** — every service must have a documented rollback procedure before deployment to production
- **Change history must be auditable** — all changes tracked in version control with meaningful commit messages
