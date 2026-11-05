# Alpha readiness issues

The **NHS Delivery Lead** fills this directory with the `alpha-readiness-issues` skill. Run the skill after the build phase, before the harden and assess phases.

The skill reads the actual tech stack, architecture, organisational standards and code. It prepares one issue file for each piece of hardening and assessment work, with precise acceptance criteria.

## How to use

1. Switch to the **NHS Delivery Lead** agent in VS Code.
1. Ask it to use `alpha-readiness-issues` to prepare the issues.
1. Review the files in this directory. In multi-week mode, review the proposed sprint for each issue.
1. Confirm the list. The agent then creates the issues on GitHub. It does not assign them.
1. Assign Copilot with the role agent and skill that each issue gives.

## Schedule

- 2-day workshop: do the issues on Day 2. See the [Day 2 guide](../playbooks/workshop-2-day/day2-guide.md).
- Multi-week: spread the issues over the hardening sprints. See the [multi-week playbook](../playbooks/multi-week/README.md#hardening-sprints).
