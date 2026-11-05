# 2-day workshop playbook

Use this playbook when a team builds an alpha in a 2-day workshop. The team does a short discovery before the workshop. The [phases page](../README.md) gives the method for each phase.

## Schedule

| When | Phases | Tool | Guide |
|---|---|---|---|
| Before the workshop | Discovery | GitHub Copilot or Microsoft 365 Copilot | [Discovery guide](../../../discovery/README.md) |
| Day 1 | Design and build | Copilot agent mode in VS Code | [Day 1 guide](day1-guide.md) |
| Day 2 | Harden and assess | Copilot coding agent on GitHub, and VS Code for local activities | [Day 2 guide](day2-guide.md) |

## Prerequisites

- A GitHub account with a Copilot seat.
- An Azure subscription with Contributor access to UK South.
- Access to [Microsoft 365 Copilot](https://m365.cloud.microsoft/chat/) with the Researcher agent, for discovery.

Use this template to make a new repository for your team. Open it in GitHub Codespaces or in VS Code with the Dev Container. The Dev Container has all other tools.

## Before the workshop

1. Run the `start-project` skill. Choose the `workshop-2-day` mode. This mode is the default.
1. Complete the discovery steps in the [discovery guide](../../../discovery/README.md).
1. Merge the discovery outputs to `main`.
1. Check which models your organisation's Copilot policy allows. See [Models](../../README.md#models).
