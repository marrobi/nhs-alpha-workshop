# Prompt: discovery plan

## How to use

1. Open Microsoft 365 Copilot Chat at <https://m365.cloud.microsoft/chat/>.
1. Copy all of this file below the line, and paste it into the chat.
1. Paste the inputs that are listed in the "Inputs" section.
1. Answer the questions that Copilot asks.
1. Save the output to `discovery/plan.md` in the repository. If you do not have access to the repository, give the file to a team member who has access.
1. Copy the blank logs from `discovery/templates/` to `discovery/`, if they are not there.

GitHub Copilot users run the `discovery-plan` skill instead. The skill uses this method.

---

You help an NHS discovery team to write a plan for discovery. The plan covers a number of weeks. It ends with a playback and a sign-off. Follow the method, the rules and the output template below. Write in Simplified Technical English: short sentences, active voice, one instruction in each sentence. Write each acronym in full the first time.

## Inputs

Ask me for these inputs if I did not give them:

1. A short description of the service, or the scenario and problem statement from `discovery/scenarios/scenario.md`.
1. The number of weeks for discovery. The default is 4 weeks.
1. The start date of discovery, and the planned start date of the first sprint.
1. The parties. Give the role of each party, and a named contact if the team chooses.
1. The owners: product owner, Clinical Safety Officer, data protection officer (DPO) or information governance (IG) lead, and delivery lead. Give roles. Give names only if the team chooses.
1. Optional: the delivery mode settings from `.github/instructions/delivery-mode.instructions.md`.
1. Optional: the known external systems, constraints and deadlines.

## Questions to ask

Ask these questions before you write the plan. Stop and wait for my answers.

1. Who decides? Who is the product owner with the authority to say yes or no?
1. Who signs off clinical safety, data protection and security?
1. Which external parties take part? Who is the named contact at each party?
1. Which external systems or data do we need access to? Which request is the slowest?
1. What does "done" mean for the first release?
1. When is the playback? Who must sign off?
1. Which days do the team meet? Is there a weekly decision meeting?

If I do not know an answer, write "Owner needed" or "Date needed". Do not guess.

## Rules

- Never use real patient data. If the inputs contain names, NHS numbers, dates of birth, addresses or clinical details of a real person, stop and tell me. Do not copy them.
- Never put secrets, tenant IDs or credentials in the plan.
- Define the problem before you define a solution. Do not choose a technology in the plan.
- Give each item an owner and a date. If you do not know them, write "Owner needed" or "Date needed".
- Never write a decision as agreed unless a named person agreed it.
- Bring the Clinical Safety Officer and the DPO or IG lead into the working sessions from the first week. Do not leave their review to the end.
- Write the steps for the agents and skills that the template names. Do not invent other tools.

## Method

1. Ask for the missing inputs and the questions above. Stop and wait for my answers.
1. Use the default of 4 weeks if I do not give a number.
1. Fit the workstreams to the number of weeks:
   - **2 or 3 weeks**: join week 2 and week 3 of the template. Keep the playback in the last week.
   - **4 weeks**: use the template as it is.
   - **5 weeks or more**: give the extra weeks to requirements, interfaces and the first slice. Keep the playback in the last week.
1. Put the slowest access requests in week 1.
1. Write the dates of each week. Use the start date that I give.
1. Write each week with a goal, the questions to ask, the Copilot steps and a checklist.
1. End the plan with the playback and sign-off gate. Sprint 0 starts only after the sign-off.
1. Write the plan with the template below.

## Output template

```markdown
# Discovery plan

This plan covers [N] weeks: [start date] to [end date]. At the end, all parties sign off the scope and the plan. Sprint 0 starts on [date].

**Service**: [service name]
**Owner**: [delivery lead role or name]
**Last updated**: [YYYY-MM-DD]

## How we work

- We define the problem before we define a solution.
- We aim to start delivery quickly and safely. For each topic, we ask: "What do we need to decide, by when, and who decides?"
- Each item has an owner and a date.
- The Clinical Safety Officer and the DPO or IG lead attend the working sessions. They do not only review at the end.
- Agents propose. People decide. Copilot drafts. A person reviews each output before we use it.

## Roles and owners

| Role | Owner | Decides |
|---|---|---|
| Product owner | [role or name] | Scope and priority |
| Clinical Safety Officer | [role or name] | Clinical safety sign-off |
| DPO or IG lead | [role or name] | Data protection sign-off |
| Delivery lead | [role or name] | Plan and status |

## How to use Copilot in discovery

| Agent | Use it to |
|---|---|
| NHS Discovery Analyst | Write the problem statement, personas and journeys. Prepare workshops. Record workshop results. Find gaps |
| NHS Delivery Lead | Keep the plan, the RAID log, the decision log and the access tracker current. Write the weekly status. Build the playback pack |
| NHS Product Owner | Propose the first slice. Write the first stories after the sign-off |
| NHS Architect | Give options for a decision. Challenge costly, risky or disputed designs. Write proposed ADRs |
| NHS Clinical Safety | Find possible hazards. Start the hazard log |
| NHS DPIA Advisor | Record data flows. Start the DPIA |

### Rules for Copilot

- Never put real patient data in Copilot. This includes notes, sample data and screenshots. Use synthetic data only.
- Never put secrets or credentials in Copilot.
- Check each output. The person who runs the step owns the output.
- Copilot records a decision only when a named person made it.
- Keep the formats. The agents read the documents in `discovery/`. Do not change the table columns.

## Weekly rhythm

| When | Who | Copilot step | Output |
|---|---|---|---|
| [First day of the week] | Delivery lead | `access-tracker` | Access requests to send and to escalate |
| 1 day before each workshop | Workshop lead | `workshop-prep` | Workshop pack in `discovery/workshops/` |
| Same day as each workshop | Note taker | `workshop-to-actions` with the notes | Decisions, actions, RAID items and document updates |
| When a decision is made or needed | Any team member | `decision` | Decision log entry. Draft ADR if the decision is large |
| After document changes | Clinical safety and IG leads | NHS Clinical Safety and NHS DPIA Advisor: "Review the changes this week" | Hazard log and DPIA updates |
| [Fourth day of the week] | Delivery lead | NHS Discovery Analyst: "Find gaps against the checklist for this week" | List of gaps, in priority order |
| [Last day of the week] | Delivery lead | `weekly-status` | Status report in `discovery/status/` |

## Workstreams

| Workstream | Week 1 | Week 2 | Week 3 | Week 4 |
|---|---|---|---|---|
| Mobilise and govern | Kick-off, roles and governance | Ways of working | | |
| Understand the problem and users | Current state, users and outcomes | Journeys and priority use cases | | |
| Requirements and constraints | | Functional requirements. Start non-functional requirements and interfaces | Finish non-functional requirements, interfaces and dependencies | |
| Plan, RAID and acceptance | Access requests | Start the RAID log | First slice and delivery plan. Start traceability | Finish traceability |
| Review and approve | | | | Playback and sign-off |

---

## Week 1: [dates]

**Goal:** start the people and the access requests. Agree the problem.

### Questions

- Who decides? Who signs off clinical safety, data protection and security?
- What problem do we solve, and for whom? What goes wrong now?
- Who are the users? How do we know that the service works?
- What is out of scope for the first release?

### Copilot steps

1. NHS Discovery Analyst: `problem-statement` with the description of the service. Review the output at the kick-off.
1. NHS Delivery Lead: `decision` and `interface-register` for the roles, decision-makers and named contacts.
1. NHS Delivery Lead: `access-tracker`. Send each access request on the list the same day.
1. NHS Discovery Analyst: `m365-researcher-personas`, or `personas` if the team cannot use Researcher. Check the draft personas with real users.
1. NHS Discovery Analyst: `user-journeys`. Select 3 to 5 journeys for the priority use cases.
1. NHS Discovery Analyst: `workshop-prep` for each week 2 workshop.
1. NHS Delivery Lead: `weekly-status`.

### Checklist

- [ ] Roles and decision-makers recorded (`discovery/decision-log.md`)
- [ ] Named contact for each party (`discovery/interface-register.md`)
- [ ] Problem statement, priority use cases, assumptions and out of scope drafted (`discovery/scenarios/scenario.md`)
- [ ] Draft personas (`discovery/personas/persona-report.md`)
- [ ] Access requests sent (`discovery/access-tracker.md`)

---

## Week 2: [dates]

**Goal:** agree how we work. Agree the journeys. Draft the requirements.

### Questions

- Where are the hand-offs between parties? How do we stop them becoming queues?
- How do we use AI coding tools? What is permitted? Where must a person review?
- Which journeys are most important? Which assumption is the riskiest?
- What happens when something goes wrong? Who sees it? Who fixes it?
- Can we reuse an existing NHS or national platform?

### Copilot steps

1. NHS Discovery Analyst: `workshop-to-actions` after each workshop.
1. NHS Discovery Analyst: "Draft the functional requirements from the use cases and journeys." Record them in `discovery/traceability.md`.
1. NHS Clinical Safety: "Start the hazard log from the journeys."
1. NHS DPIA Advisor: "Start the DPIA from the problem statement, the journeys and the interface register."
1. NHS Delivery Lead: `raid-and-traceability`. Start the RAID log from the assumptions, the open decisions and the access tracker.
1. NHS Discovery Analyst: `workshop-prep` for each week 3 workshop.
1. NHS Delivery Lead: `weekly-status`.

### Checklist

- [ ] Ways of working agreed, including AI-assisted delivery (`discovery/decision-log.md`)
- [ ] Journeys agreed for the first release (`discovery/user_journeys/data/`)
- [ ] Functional requirements drafted (`discovery/traceability.md`)
- [ ] Hazard log started (`docs/clinical-safety/hazard-log.md`)
- [ ] DPIA started (`docs/dpia/dpia.md`)
- [ ] RAID log started (`discovery/raid-log.md`)

---

## Week 3: [dates]

**Goal:** finish the targets and interfaces. Agree the first slice and the plan.

### Questions

- How much downtime is acceptable? How much data can we lose? How fast must the service respond?
- Which security and network rules apply?
- Who supports the service, and when? How do we detect a failure? Who receives each alert?
- For each external system: how do we connect? Is there a test environment? How long does access take?
- What is the smallest end-to-end slice that tests the riskiest assumption?
- What can we do at the same time? What waits for access or decisions?

### Copilot steps

1. NHS Discovery Analyst: "Draft the non-functional requirements with measurable targets." Agree each target with its owner.
1. NHS Discovery Analyst: `interface-register`. Mark each unknown lead time as an action.
1. NHS Architect: "List the decisions that the first slice needs, with options." Then `decision` for each decision. Draft an ADR for each large decision.
1. NHS Product Owner: "Propose the first slice. Give the riskiest assumption that it tests."
1. NHS Delivery Lead: "Draft the delivery plan from the first slice, the access tracker and the RAID log."
1. NHS Delivery Lead: `raid-and-traceability`. Start the traceability table from the requirements.
1. NHS Discovery Analyst: "Find gaps against the week 3 checklist."
1. NHS Delivery Lead: `weekly-status`.

### Checklist

- [ ] Non-functional requirements with measurable targets (`discovery/traceability.md`)
- [ ] Interface register complete with lead times (`discovery/interface-register.md`)
- [ ] First slice proposed
- [ ] Delivery plan drafted
- [ ] Each open decision has an owner and a date (`discovery/decision-log.md`)
- [ ] Each risk has an owner and a mitigation (`discovery/raid-log.md`)

---

## Week 4: [dates]

**Goal:** finish traceability. Play back. Get sign-off.

### Questions

- Can we trace each requirement from the use case to the test approach?
- Does each party agree what it must do, and by when?
- Is the scope of the first release agreed?
- Is the access for sprint 0 available, or does it have a firm date?

### Copilot steps

1. NHS Delivery Lead: `raid-and-traceability`. Give each requirement that has no test approach.
1. NHS Clinical Safety and NHS DPIA Advisor: "Give the status and the decisions needed for the playback."
1. NHS Delivery Lead: `playback-pack`. Review it with each party before the playback.
1. NHS Delivery Lead: `marp-slides` on the playback pack.
1. NHS Delivery Lead: after the playback, `workshop-to-actions` with the sign-off notes.

### Checklist

- [ ] Traceability complete (`discovery/traceability.md`)
- [ ] Playback pack ready (`discovery/playback/playback-pack.md`)
- [ ] Sign-off recorded, with names and the date (`discovery/decision-log.md`)

---

## Gate: playback and sign-off

Sprint 0 starts only when these are true:

- [ ] Each party signed off the scope of the first release.
- [ ] The first slice is agreed.
- [ ] The access for sprint 0 is available, or has a firm date.
- [ ] Each open decision has an owner and a date.
- [ ] The Clinical Safety Officer and the DPO or IG lead agreed the next steps.

## What makes us faster

1. Send access requests in week 1.
1. Have one product owner with authority to decide, and a decision meeting each week.
1. Bring clinical safety and IG into the working sessions.
1. Agree AI-assisted delivery and its controls in week 2.
1. Reuse existing NHS and national platforms and patterns.
1. Have the first slice ready to build at the start of sprint 0.
```
