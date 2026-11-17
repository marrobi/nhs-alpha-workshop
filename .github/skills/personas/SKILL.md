---
name: personas
description: 'Use when the team has no access to Microsoft 365 Copilot Researcher and needs a draft persona report from the scenario. Triggers: personas, persona report, users and their needs, /personas. Writes discovery/personas/persona-report.md in the same structure as the Researcher output.'
---

# Personas

Microsoft 365 Copilot Researcher is the preferred way to make personas, because it does web research and gives citations. See [discovery/prompts/personas.md](../../../discovery/prompts/personas.md). Use this skill only if the team cannot use Researcher.

Follow the method in [discovery/personas/persona-generation-prompt.md](../../../discovery/personas/persona-generation-prompt.md). Read that file first. It is the only method for personas. Do not change it.

The steps below add only the repository steps and the differences for an AI draft without web research.

## Inputs

1. Read `discovery/scenarios/scenario.md`. Use it as the "Scenario Details" of the method.
1. If the scenario does not exist, tell the user to run the `problem-statement` skill first. Stop.
1. Read `discovery/personas/persona-report.md` if it exists. If it exists, ask the user if you must replace it or add to it.

## Questions

Before you write, ask the user these questions with the ask questions tool, if it is available. Otherwise, ask them in the chat:

1. Did the team talk to any users? If yes, ask for a short summary without real names or patient data.
1. Which roles must the report include, or must not include?
1. How many personas does the team want? The method says "enough to cover every distinct user type, but no more".

Stop and wait for the answers.

## Differences from the Researcher output

- You do not do web research. Do not claim research that you did not do.
- Do not invent citations, statistics, studies or links. Only give a source if the user gave it, or if you read it in this session. Give the link.
- Write `Assumption:` before each detail that is not from the scenario or the user.
- Write each gap as a question in the "Gaps and questions" section. Do not fill a gap with a guess.
- Use fictional names only. Never add real patient data.

## Output

Write `discovery/personas/persona-report.md` in the same structure as the Researcher output, so that the `persona-slides` skill can read it:

```markdown
# Personas: [service name]

> **AI draft — not user research. Check with real users.**
> GitHub Copilot made this draft from `discovery/scenarios/scenario.md` without web research.
> Microsoft 365 Copilot Researcher is the preferred tool. The team must check each persona with real users before design.

## Summary

| ID | Name | Role | Level | Workflow stage | Use cases |
|---|---|---|---|---|---|
| P-01 | | | Strategic, Tactical or Operational | | UC-01 |

## [Workflow stage name]

### P-01: [Fictional name], [role]

#### 1. Persona Name & Role
#### 2. Goals & Outcomes
#### 3. Wants, Needs & Expectations
#### 4. Biggest Pain Points & Unmet Needs
#### 5. Wider Journey & Touchpoints
#### 6. Additional Context

**Check with:** [the real user group to check this persona with], owner [name or "Owner needed"], by [date or "Date needed"].

## Gaps and questions

| Question | Persona | Owner | Date |
|---|---|---|---|

## Sources

[Only the sources that the user gave or that you read in this session. If there are none, write "None. AI draft from the scenario only."]
```

## Validation

- Make sure that the banner is at the top of the file.
- Make sure that the personas cover the strategic, tactical and operational levels, and the non-NHS roles in the method.
- Make sure that each persona has all 6 sections and a "Check with" line.
- Make sure that the file has no invented citations and no real patient data.
- Tell the user to make slides with the `persona-slides` skill.
