---
name: github-actions-pipeline
description: 'Use when creating or updating GitHub Actions CI and deployment workflows, Dependabot configuration, or pipeline validation for an NHS service.'
---

# GitHub Actions Pipeline

Use this skill to build GitHub Actions pipelines for NHS digital services. Pipelines enforce quality gates (linting, testing, coverage, security scanning) and automate deployment to the hosting platform selected for the service.

## Your Capabilities

You create GitHub Actions workflow files, Dependabot configuration, and branch protection recommendations. You can run and test pipeline steps locally to validate them before committing.

## Modes

Use the mode that the task needs. The NHS Service Builder uses CI mode in slice 0a and deploy mode in slice 0b.

| Mode | When | Creates | Needs Azure access |
|---|---|---|---|
| **CI mode** | Slice 0a, in the scaffold pull request | `ci.yml`, `dependabot.yml` | No |
| **Deploy mode** | Slice 0b, after the Terraform is applied and OIDC is set up | `deploy.yml` | Yes: a federated credential and a GitHub environment |
| **Hardening mode** | The "CI/CD hardening" alpha readiness issue (F01) | Workflow `environment:` and approval settings, Terraform for deployment slots or staged rollout, and the branch protection recommendation. An operator then applies the Terraform and sets the environment protection rules and branch protection | The pull request: no. The operator steps: yes, and repository admin rights |

## Pipeline Architecture

NHS Alpha services need two core pipelines:

```
.github/
├── workflows/
│   ├── ci.yml                    # Runs on every PR to main
│   └── deploy.yml                # Runs on push to main (after merge)
└── dependabot.yml                # Automated dependency updates
```

## CI Pipeline (`ci.yml`)

Runs on every pull request to `main`. Must pass before merge.

Read `tech-stack.instructions.md` to determine the backend language, frontend framework, linter, test runner, coverage tool, dependency audit command, and IaC tool. Read `.github/instructions/org-standards.instructions.md` for organisational policies that apply to CI/CD and pipelines. Standards defined in org-standards take precedence over values that may be defined anywhere else in the repository. Generate the workflow steps from the actual stack — do not hardcode language versions or tool names.

### Required Jobs

1. **Backend**: setup language runtime → install dependencies → lint → test with coverage (fail under 80%) → dependency audit (fail on critical/high)
2. **Frontend**: setup runtime → install dependencies → lint → production build → unit tests (if configured)
3. **IaC validation**: setup IaC tool → init (no backend) → validate → format check
4. **Secret scanning**: scan the pull request for secrets with a pinned scanner (for example `gitleaks`). Fail the job if it finds a secret

### Principles

- Use `permissions: contents: read` (minimal)
- Cache dependencies for both backend and frontend
- Set `timeout-minutes: 10` on all jobs
- Pin action versions (e.g. `actions/checkout@v4`)

## Deploy Pipeline (`deploy.yml`)

Runs on push to `main` (after PR merge). Deploys to Azure UK South.

Read `tech-stack.instructions.md` for the hosting platform and build commands. Generate language-appropriate setup, build, package, and deploy steps.

### Required Steps

1. Authenticate to Azure using OpenID Connect (OIDC) — never use service principal secrets
2. Setup language runtimes from tech stack
3. Install dependencies and build frontend for production
4. Package application for deployment, and each external mock service listed in `docs/adr/002-dependency-strategy.md`
5. Deploy using the platform CLI (e.g. `az webapp deploy`)
6. Verify health endpoint returns 200

### Principles

- `permissions: id-token: write` for OIDC, `contents: read`
- Use GitHub Environment secrets for Azure credentials (`AZURE_CLIENT_ID`, `AZURE_TENANT_ID`, `AZURE_SUBSCRIPTION_ID`)
- Use `concurrency` groups to cancel superseded deploys

## Dependabot Configuration

Generate `.github/dependabot.yml` with ecosystems matching `tech-stack.instructions.md`: backend package manager, frontend package manager, IaC tool, and `github-actions`. Weekly interval, 5 PR limit per ecosystem (3 for IaC and actions).

## Rules

- **Permissions**: Use minimal permissions — `contents: read` for CI, add `id-token: write` only for OIDC auth in deploy
- **Secrets**: Never echo or log secrets. Use GitHub Environment secrets for Azure OIDC credentials
- **OIDC**: Use `azure/login@v2` with federated credentials — never store service principal client secrets
- **Caching**: Cache pip and npm dependencies for faster CI runs
- **Timeouts**: Set `timeout-minutes: 10` on jobs to prevent runaway builds
- **Concurrency**: Use `concurrency` groups to cancel superseded deploy runs
- **Branch protection**: Recommend requiring CI to pass before merge on `main`

## Build Sequence

1. Read the mode (see Modes). In CI mode, do steps 2, 4, 5, 6 and 7. In deploy mode, do steps 3, 5 and 7. In hardening mode, do all steps
2. Check if `.github/workflows/ci.yml` exists — create or update it
3. Check if `.github/workflows/deploy.yml` exists — create or update it
4. Check if `.github/dependabot.yml` exists — create it
5. Validate workflow syntax: `actionlint` if available, or manual review of YAML
6. Run a dry-run of CI steps locally to verify (ruff, pytest, terraform validate)
7. Recommend branch protection rules to the team

## MCP Servers

This agent has access to MCP servers configured in `.vscode/mcp.json` and via VS Code extensions:
- **Context7** — use to look up current GitHub Actions and CI/CD tool documentation
- **Azure MCP Server** (provided by the `ms-azuretools.vscode-azure-mcp-server` extension) — use to verify Azure resource configuration when building deploy pipelines

## References

- [GitHub Actions Workflow Syntax](https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions)
- [Azure Login Action](https://github.com/azure/login)
- [actions/setup-python](https://github.com/actions/setup-python)
- [actions/setup-node](https://github.com/actions/setup-node)
- [hashicorp/setup-terraform](https://github.com/hashicorp/setup-terraform)
