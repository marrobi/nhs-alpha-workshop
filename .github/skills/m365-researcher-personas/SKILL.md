---
name: m365-researcher-personas
description: 'Use when preparing NHS personas with the M365 Copilot Researcher agent. Guides the user through the external Researcher workflow and preserves its report handoff.'
---

# M365 Researcher Personas

Use the M365 Copilot Researcher to produce a draft persona report for an NHS service. This is a manual handoff to Microsoft 365 Copilot; this skill does not call or control Researcher.

## Steps

1. Confirm that `discovery/scenarios/scenario.md` is ready to use.
2. Open [M365 Copilot Chat](https://m365.cloud.microsoft/chat/) and select the **Researcher** agent. See the [Microsoft Researcher documentation](https://learn.microsoft.com/en-us/copilot/microsoft-365/researcher-agent).
3. Open `discovery/personas/persona-generation-prompt.md`. Paste the scenario content at the end of the prompt and run it in Researcher.
4. Save the Researcher output to `discovery/personas/persona-report.md`.
5. Ask the team to review the personas and check them with real users before using them to shape design.

## Rules

- Do not put real patient data, identifiers, clinical records, secrets, or confidential information into the prompt.
- Treat the report as a draft, not as evidence that user research has taken place.
- If M365 Copilot Researcher is unavailable, stop and ask how to proceed. Do not silently substitute another tool or generate a Researcher report yourself.
- Keep the report path and downstream slide-generation step unchanged.
