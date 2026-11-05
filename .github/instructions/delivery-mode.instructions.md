---
applyTo: "**"
---

# Delivery Mode

This file is the **single source of truth** for how the project runs. Agents and skills read this file. They do not assume a schedule. Change this file with the `start-project` skill.

| Setting | Value |
|---|---|
| Mode | `workshop-2-day` |
| Discovery length | Pre-work before the workshop |
| Sprint length | Not used in workshop mode |
| Engineering review cadence | After each batch (2 engineers), and at the end of each day (3 engineers) |
| Governance records required | RAID (risks, assumptions, issues and dependencies) log, decision log, weekly status and playback sign-off: optional |
| Owners | Product owner: not recorded. Clinical Safety Officer: not recorded. Data protection officer (DPO) or information governance (IG) lead: not recorded. Delivery lead: not recorded |
| Playbook | [2-day workshop playbook](../../docs/playbooks/workshop-2-day/README.md) |

## Values

- **Mode**: `workshop-2-day` or `multi-week`. The default is `workshop-2-day`.
- **Governance records required**: optional in `workshop-2-day` mode. Required in `multi-week` mode.
- **Owners**: record the roles. Record names only if the team chooses.
- **Playbook**: [2-day workshop playbook](../../docs/playbooks/workshop-2-day/README.md) or [multi-week playbook](../../docs/playbooks/multi-week/README.md).
