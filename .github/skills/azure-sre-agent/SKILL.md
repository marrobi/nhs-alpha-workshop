---
name: azure-sre-agent
description: 'Use when adding Azure Monitor alerting and an Azure SRE Agent to an NHS service — Terraform for alert rules, action group and the Microsoft.App/agents resource, read-only RBAC, private telemetry access, agent skills/hooks/incident filters as code, an apply-and-verify script, and a controlled fault-injection rehearsal.'
---

# Azure SRE Agent for NHS Services

Patterns for giving an NHS service **AI-assisted incident diagnosis** with [Azure SRE Agent](https://learn.microsoft.com/azure/sre-agent/), deployed **with the service** from the same repository. The agent investigates Azure Monitor alerts, queries logs and metrics, correlates them with recent changes, and recommends a fix. It never changes the service itself.

Read `tech-stack.instructions.md` for the hosting platform, database and monitoring choices, and `terraform-azure-nhs.instructions.md` for IaC rules. Every resource name, signal and table below must be mapped to what the repository **actually** deploys.

## When to Use

- Adding alert rules and an action group so the team is told when the service fails
- Provisioning an Azure SRE Agent with Terraform
- Writing agent skills, guardrail hooks, incident filters or scheduled tasks as code
- Rehearsing an incident (fault injection) and recording the evidence for GDS point 14

## Always check current facts first

Azure SRE Agent changes quickly. Before writing code, look these up in the [Microsoft Learn SRE Agent docs](https://learn.microsoft.com/azure/sre-agent/) (or the Microsoft Learn MCP server) and record what you found, with the date, in the ADR:

| Fact | Where to check |
|---|---|
| Newest **non-preview** API version of `Microsoft.App/agents` | [ARM template reference](https://learn.microsoft.com/azure/templates/microsoft.app/agents) or `az provider show -n Microsoft.App --query "resourceTypes[?resourceType=='agents'].apiVersions"` |
| Supported regions (UK South must be supported) | [Supported regions](https://learn.microsoft.com/azure/sre-agent/supported-regions) |
| Where inference and conversation data are processed and stored | [Data residency and privacy](https://learn.microsoft.com/azure/sre-agent/data-privacy) |
| Model providers and which are opt-in | [Model provider selection](https://learn.microsoft.com/azure/sre-agent/) |
| Access levels, run modes, billing (Agent Units) and monthly cap | SRE Agent docs and the pricing page |
| Data-plane routes for skills, hooks, incident filters, scheduled tasks and knowledge | [microsoft/sre-agent templates](https://github.com/microsoft/sre-agent/tree/main/sreagent-templates) — pin the commit you read |

If web or MCP access is unavailable, say so and stop — do not guess an API version, role ID or route.

## Non-negotiable guardrails

Write these into an ADR and enforce them with Terraform `validation` blocks and tests:

1. **UK only** — `location = "uksouth"` (or `ukwest`); any other region fails validation.
2. **Approved model provider only** — default to Azure OpenAI (`MicrosoftFoundry`). Reject providers whose inference may run outside the UK unless Information Governance has accepted it in the DPIA.
3. **Read-only and human-approved** — `actionConfiguration.accessLevel = "Low"` and `mode = "Review"` in every environment. Never `High` or `Automatic`. Raising autonomy needs a new ADR and a clinical safety review.
4. **Stable channel, no experiments** — `upgradeChannel = "Stable"`; do not set `experimentalSettings`.
5. **Least privilege** — the agent's identities get only `Reader`, `Monitoring Reader` and `Log Analytics Reader` on the workload resource group. No `Owner`, `Contributor`, database login, FHIR data role, Key Vault secrets/keys role or Storage data role. `SRE Agent Administrator` goes to an Entra **group**, never an individual.
6. **Private telemetry stays private** — if Log Analytics / App Insights are private (Azure Monitor Private Link Scope), VNet-inject the agent into a dedicated subnet delegated to `Microsoft.App/environments` and prove it can query privately. **Never open a workspace to public query for the agent.** If private query cannot be proved, stop and report it.
7. **No personal data** — alert queries, notifications, prompts, knowledge and agent output must contain no NHS numbers, names, contact details, tokens or request bodies. The agent has no route to the data stores.
8. **Cost cap** — `monthlyAgentUnitLimit` is a required variable with no default.
9. **Feature flag** — `sre_agent_enabled` per environment, so the agent can be removed without touching alerts.

> The upstream `microsoft/sre-agent` templates are **reference only**. Their defaults (an opt-in third-party model provider, `Preview` channel, experimental settings, `Autonomous` incident handling, their own resource group and workspace, floating provider versions) are not acceptable for an NHS service. Do not run or vendor them.

## Phase 1 — Alerts first (useful with or without the agent)

Alerts are the agent's incident source and an organisational standard in their own right. Put them in `infra/modules/alerting` (or the repository's module layout), used by every environment, with thresholds, windows and severities as variables.

Map signals to what the service actually runs:

| Alert | App Service | Container Apps | Severity |
|---|---|---|---|
| Server error rate | `Http5xx` metric, or App Insights `requests` with `resultCode >= 500` | `AppRequests` 5xx in the workspace | Sev1 |
| Dependency unavailable | App Insights failed `dependencies` to the database / FHIR | same, from `AppDependencies` | Sev1 |
| Health check failing | `HealthCheckStatus` metric below 100 | revision/readiness probe failures | Sev1 |
| Latency | `HttpResponseTime` / p95 request duration | `AppRequests` p95 duration | Sev2 |
| Restarts | App Service restarts in Activity/AppServicePlatformLogs | `RestartCount` metric | Sev2 |
| Database down | PostgreSQL `is_db_alive` metric | same | Sev1 |

Rules:

- Use `azurerm_monitor_metric_alert` for metrics and `azurerm_monitor_scheduled_query_rules_alert_v2` for KQL; every rule `auto_mitigation_enabled = true`.
- Add **minimum-request thresholds** (for example ≥ 20 requests in 5 minutes) so low alpha traffic does not alert on one failed request.
- One action group with the common alert schema; the email distribution list is a **required variable with no default** — never commit a personal address.
- Each alert description names the runbook section to follow.
- KQL projects counts, rates, status codes, problem types and correlation IDs only — never `customDimensions`, URLs with query strings or request bodies.
- If the environment is stopped out of hours, silence it with an alert processing rule or an `alerts_enabled` variable — never delete rules.
- Add a `terraform test` (`command = plan`) asserting each rule exists, is enabled, targets this environment's workspace and uses the action group.

## Phase 2 — The agent in Terraform

Create `infra/modules/sre-agent` with the `azapi` provider (pin the exact version already used by the repository, or the latest release you looked up). Abridged body shape — confirm every property against the API version you pinned:

```hcl
resource "azapi_resource" "agent" {
  type      = "Microsoft.App/agents@<non-preview api version>"
  name      = "sre-${var.app_name}-${var.environment}"
  parent_id = var.resource_group_id
  location  = var.location
  identity {
    type         = "SystemAssigned, UserAssigned"
    identity_ids = [var.user_assigned_identity_id]
  }
  body = {
    properties = {
      actionConfiguration         = { accessLevel = "Low", mode = "Review", identity = var.user_assigned_identity_id }
      knowledgeGraphConfiguration = { identity = var.user_assigned_identity_id, managedResources = [var.resource_group_id] }
      upgradeChannel              = "Stable"
      monthlyAgentUnitLimit       = var.monthly_agent_unit_limit
      defaultModel                = { provider = "MicrosoftFoundry", name = "Automatic" }
      # Only when telemetry is private:
      vnetConfiguration    = { subnetResourceId = var.subnet_id }
      sandboxConfiguration = { egress = { mode = "AzureVNet", vnetConfiguration = { usePrivateDnsResolution = true } } }
    }
  }
}
```

- Add `Microsoft.App/agents/connectors` for App Insights and Log Analytics pointing at the **existing** monitoring resources; set `timeouts` (connector PUTs can take 10–30 minutes).
- `logConfiguration` (the agent's own telemetry) needs an App Insights connection string; omit it if the repository disables local auth on App Insights, and record why.
- `schema_validation_enabled = false` only if the pinned azapi provider has no schema for the version — comment the reason.
- Guard with `count = var.sre_agent_enabled ? 1 : 0` in every environment; enable in the alpha/dev environment only until production ownership and the DPIA are agreed.
- Validation blocks for `location`, `default_model_provider`, `access_level`, `action_mode`, `upgrade_channel` and required `monthly_agent_unit_limit` / `admin_group_object_id`, each error citing the ADR.
- Outputs: agent ID, name, data-plane endpoint, principal IDs.
- A `terraform test` asserts the guardrails above and that forbidden roles are absent; a negative test (`expect_failures`) proves a non-UK region and a forbidden provider are rejected.
- Networking (when VNet-injected): a dedicated `/27` delegated to `Microsoft.App/environments`, its own NSG allowing outbound 443 only to the private-endpoint subnet and required service tags (`AzureActiveDirectory`, `AzureMonitor`, the regional `AzureCloud` tag), reusing the existing `privatelink.*` DNS zones.

## Phase 3 — Agent configuration as code

ARM does not cover skills, subagents, hooks, incident filters, scheduled tasks or knowledge. Keep them as declarative files and apply them with **one reviewed, idempotent script** — not `local-exec`, not the portal.

```
infra/sre-agent/
├── README.md
├── config/
│   ├── common-prompts/   service-context.yaml, safety-rules.yaml
│   ├── skills/           one per alert: <alert>.yaml + <alert>.md (KQL, change checks, runbook link)
│   ├── subagents/        alert-investigator, remediation-advisor
│   └── hooks/            deny-all-write-tools.yaml, block-personal-data-in-output.yaml
├── automations/
│   ├── incident-filters/ sev0-sev1.yaml   (agentMode: Review)
│   └── scheduled-tasks/  daily-health-summary.yaml (mode: Review)
└── knowledge/manifest.yaml               (paths under docs/ only: runbook, ADRs, architecture)
```

- **Safety-rules prompt**: never output personal data; summarise counts and correlation IDs; never propose portal or CLI changes as the fix — propose the IaC change or release; always name the runbook section.
- **Skills** restrict tools to read-only ones and link to the runbook rather than copying it.
- **`deny-all-write-tools` hook**: `PreToolUse`, matcher covering at least `^(restart_|scale_|delete_|update_|create_|set_|RunAzCliWriteCommands)`, `permissionDecision: deny`.
- No environment-specific values committed — use placeholders filled from `terraform output -json`.
- **`scripts/sre-agent-apply-config.sh`**: reads Terraform outputs; gets a data-plane token with `az account get-access-token`; passes it to `curl` via stdin or a mode-0600 temp file — **never on the command line and never printed**; PUTs each item; deletes only items carrying the repository's managed marker; fails on any non-2xx with kind, name and status (not the body); is idempotent; supports `--dry-run` that prints kinds and names only.
- **`scripts/sre-agent-verify-config.sh`**: compares the live agent with the rendered files and Terraform settings and fails with a readable diff. Run it after every release to catch portal drift.
- **Offline CI test** (no Azure): parses every YAML file; checks every skill uses only allow-listed read-only tools; checks the deny hook exists and is enabled; checks every incident filter and scheduled task uses `Review`; checks knowledge paths exist under `docs/`; scans for NHS-number-shaped values (10 digits passing Modulus 11), emails, `Bearer` tokens, connection strings and GUID-shaped secrets; runs both scripts with `--help` and `--dry-run`; runs `shellcheck`.

## Phase 4 — Rehearse an incident (operator, not the coding agent)

Evidence that the agent works comes from a live, controlled fault in the **alpha environment only**:

- `scripts/sre-agent-fault.sh <scenario> inject|restore` — **fails closed**: refuses unless a Terraform output says the environment is the alpha/dev one, checks every resource ID is inside that subscription and resource group, needs typed confirmation (or `--yes` plus the exact target name), allows one active fault at a time, never prints tokens, and has `--dry-run`.
- Good scenarios: remove one data-plane role assignment from the app identity (restore by `terraform plan` showing exactly that one change, then apply — proving IaC is the fix route); stop the database (restore by starting it); deploy a revision/slot with an invalid required setting (restore by redeploying the last good release).
- `scripts/sre-agent-load.sh` sends low, steady synthetic traffic with synthetic personas so minimum-request thresholds are met.
- Expect delays: role changes can take ~10 minutes to take effect, plus 5–15 minutes alert evaluation. Rehearse once 30–60 minutes before any demonstration so a finished investigation is ready.
- Check the error mapping before predicting the alert: for example a data store returning 403 often surfaces as a 500, not a 503.
- Record in the runbook: date, commit, scenario, time to alert, time to agent finding, time to restore, thread ID, whether findings were correct. Ask the agent to fix the fault itself and record that it refuses. Delete demonstration threads afterwards. Fix wrong findings by improving a skill in the repository, never in the portal.

## Compliance hand-offs

- **DPIA** (NHS DPIA Advisor): Microsoft as processor for agent conversations; storage region; inference location; retention until deletion; no Customer Lockbox.
- **DCB0129** (NHS Clinical Safety): hazard "agent proposes an incorrect cause or remediation" with controls read-only access, `Review` mode, changes only through reviewed IaC, human incident owner. Alerts are a detection control for "service unavailable" hazards.
- **GDS point 14**: alerts, runbook, rehearsal record.
- **Architecture** (NHS Architect): add the agent, its subnet and flows to the draw.io diagram.

## Checklist

- [ ] Facts checked and dated in an ADR (API version, region, provider, residency, roles, routes)
- [ ] Alert rules + action group in Terraform, all environments, with plan-time tests
- [ ] Agent module with validation guardrails, read-only roles, feature flag and tests
- [ ] Private telemetry proved, or the agent not deployed
- [ ] Skills, hooks, filters and tasks as code with offline CI tests
- [ ] Apply and verify scripts, idempotent, token-safe, with `--dry-run`
- [ ] Runbook section: open the agent, guardrails, delete threads, disable it
- [ ] Rehearsal recorded by an operator
