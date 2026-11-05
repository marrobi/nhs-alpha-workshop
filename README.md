# NHS Alpha Workshop

A template for designing and building an NHS digital service with GitHub Copilot.

```mermaid
flowchart LR
    Scenario --> Personas --> Journeys --> Design --> Build --> Validate
```

## Two ways to deliver

The kit has one set of agents and skills, and two playbooks:

- **2-day workshop**: discovery is pre-work, then a team designs and builds an alpha in two days. See the [2-day workshop playbook](docs/playbooks/workshop-2-day/README.md).
- **Multi-week**: a team runs discovery over several weeks, gets sign-off, then builds in sprints. See the [multi-week playbook](docs/playbooks/multi-week/README.md).

The [kit overview](docs/README.md) gives the phases, agents and skills.

## Start

1. Run the `/start-project` skill in GitHub Copilot Chat. Choose the delivery mode.
1. Run the `/problem-statement` skill. The skill creates the discovery scenario and replaces this file with a README for your service. If you have only Microsoft 365 Copilot, use the prompt file [discovery/prompts/problem-statement.md](discovery/prompts/problem-statement.md).
1. In multi-week mode, run the `/discovery-plan` skill.

See the [discovery guide](discovery/README.md) and the
[kit overview](docs/README.md) for the full process.

## Previous version

For the previous workshop-only version of this kit, use the
[`alpha-workshop-v1` release](https://github.com/marrobi/nhs-alpha-workshop/releases/tag/alpha-workshop-v1).

## Licence

[MIT](LICENSE)
