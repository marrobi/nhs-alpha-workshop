---
applyTo: "**"
---

# Required Component Availability

If a component an agent depends on is **unavailable** or **fails to execute**, stop
and ask the user how to proceed. Do not silently degrade, skip the step, or fabricate
the output the component would have produced (e.g. a draw.io diagram, Playwright
screenshots, an `axe-core` report, a `terraform plan`, or test results) — missing
evidence is a gap, not an assurance.

This covers MCP servers (e.g. `context7`, `drawio` in `.vscode/mcp.json`, the Azure
or Playwright MCP servers), Playwright browsers, and CLIs or services such as
`terraform`, `az`, `gh`, `npm`, `pytest`, `ruff`, `k6`, and any cloud or NHS API.

When this happens, tell the user **which** component failed, **why** it is needed, and
the **error** observed, then wait for them to resolve it or agree how to continue.
