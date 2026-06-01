---
applyTo: "**"
---

# Required Component Availability

Agents must not produce inferior or partial results because a required component
could not run. When a tool an agent depends on is **unavailable** or **fails to
execute**, stop and ask the user how to proceed instead of silently degrading,
skipping the step, or fabricating output.

## What counts as a required component

- **MCP servers** — e.g. `context7` and `drawio` (`.vscode/mcp.json`), the Azure
  MCP server, the Playwright MCP server, or any other configured MCP tool.
- **Browser automation** — Playwright and its browsers (`playwright install --with-deps chromium`).
- **CLIs and services** — `terraform`, `az`, `gh`, `node`/`npm`, `python`/`pytest`,
  `ruff`, `k6`, and any cloud or NHS API the task depends on.

## Rule

1. **Check before you rely on it.** Before a step that needs a required component,
   confirm the component is present and can execute (for example, the MCP server is
   connected, the Playwright browser is installed, the CLI is on `PATH` and
   authenticated).
2. **Stop and prompt on failure.** If a required component is missing or fails, do
   **not** continue with a workaround that lowers quality. Pause and tell the user:
   - **which** component is unavailable or failing,
   - **why** it is needed for the current step,
   - the **error** observed (if any), and
   - the **options** to proceed — for example install/start the component, supply
     credentials or configuration, grant network/firewall access, or explicitly
     agree to skip the affected step.
3. **Never fake the output.** Do not invent results that the component would have
   produced — for example a draw.io diagram, Playwright screenshots or video, an
   `axe-core` accessibility report, a `terraform plan`, or test results. Missing
   evidence is a gap, not an assurance (see `review-agent-pattern.instructions.md`).
4. **Resume once resolved.** After the user installs the component, provides what is
   needed, or agrees how to proceed, continue from where you paused.

This rule applies to every agent and skill in this repository and complements the
"no silent fallback values" and "no unauthorised mocking of services" rules in
`.github/copilot-instructions.md`.
