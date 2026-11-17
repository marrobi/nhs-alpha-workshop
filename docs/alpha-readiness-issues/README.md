# Alpha readiness issues

The **NHS Delivery Lead** fills this directory with the `alpha-readiness-issues` skill. The readiness work is a loop:

- **Foundation mode** runs once, after slice 0b or slice 1. It writes `foundation/F01`–`F04`: CI/CD hardening, alerting and the Azure SRE Agent (optional), the infrastructure security review and the runbook.
- **Slice mode** runs after each slice is merged. It writes `slice-N/S01`–`S09`: test gaps, code quality, security, accessibility, performance, content, clinical safety, the DPIA and the GDS evidence for that slice. Add S10 for changed infrastructure or deployed dependencies, and S11 for changed operational guidance; complete these before S07–S09. The foundation baseline remains once-only.

The skill reads the actual tech stack, architecture, organisational standards and code. In slice mode, it reads the slice's stories and the diff of its merged pull request. It prepares one issue file for each piece of work, with precise acceptance criteria.

## How to use

1. Switch to the **NHS Delivery Lead** agent in VS Code.
1. Ask it to use `alpha-readiness-issues` in foundation mode, or in slice mode for slice N.
1. Review the files in this directory. In multi-week mode, review the proposed sprint for each issue.
1. Confirm the list. The agent then creates the issues on GitHub. It does not assign them.
1. Assign Copilot with the role agent and skill that each issue gives, one issue at a time.

## Schedule

- 2-day workshop: on Day 2, run foundation mode, then slice mode for each slice built on Day 1. See the [Day 2 guide](../playbooks/workshop-2-day/day2-guide.md).
- Multi-week: run foundation mode in the first build sprint, and slice mode after each slice. See the [multi-week playbook](../playbooks/multi-week/README.md#hardening-sprints).
