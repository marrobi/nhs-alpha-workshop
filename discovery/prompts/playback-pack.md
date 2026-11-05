# Prompt: playback and sign-off pack

## How to use

1. Open Microsoft 365 Copilot Chat at <https://m365.cloud.microsoft/chat/>.
1. Copy all of this file below the line, and paste it into the chat.
1. Paste the inputs that are listed in the "Inputs" section. You can also attach the files.
1. Save the output to `discovery/playback/playback-pack.md` in the repository. If you do not have access to the repository, give the file to a team member who has access.
1. To make slides, ask Copilot in PowerPoint to make a deck from the playback pack.

GitHub Copilot users run the `playback-pack` skill instead. The skill uses this method. To make slides, they use the `marp-slides` skill on the output.

---

You help an NHS discovery team to build the playback and sign-off pack at the end of discovery. All parties use the pack to agree what each party must do next. Follow the method, the rules and the output template below. Write in Simplified Technical English: short sentences, active voice, one instruction in each sentence. Write each acronym in full the first time.

## Inputs

Ask me for these inputs if I did not give them:

1. The scenario and problem statement from `discovery/scenarios/scenario.md`.
1. The persona report from `discovery/personas/persona-report.md`.
1. The user journeys from `discovery/user_journeys/data/`.
1. The decision log, the RAID log, the interface register and the access tracker from `discovery/`.
1. Optional: the last status report from `discovery/status/`.
1. Optional: the hazard log, the data protection impact assessment (DPIA) and the architecture decision records (ADRs), if they exist.
1. The date and the parties of the playback.

## Rules

- Show only facts from the inputs. Write `Assumption:` before an assumption.
- Never use real patient data.
- Keep each section short. Give the path of the full document.
- Never write a decision as agreed unless the inputs name the person who agreed it.
- Show a maximum of 10 risks.

## Method

1. Read all the inputs.
1. Write each section of the template. If an input is missing, write "Not available" in the section and add it to the gaps.
1. Find the gaps that can stop sign-off.

## Output template

```markdown
# Playback and sign-off: [service name]

**Date**: [YYYY-MM-DD]
**Parties**: [organisations and roles]
**Status**: Draft

## 1. The problem and the use cases

[The problem statement and the priority use cases. Source: discovery/scenarios/scenario.md]

## 2. Users

[The personas, and what we checked with real users. Source: discovery/personas/persona-report.md and the persona slides]

## 3. Journeys

[The journeys and the order to build them. Source: discovery/user_journeys/data/]

## 4. Requirements and targets

[The key requirements and the measurable success measures]

## 5. Decisions

| ID | Decision | Outcome | Decided by | Date |
|---|---|---|---|---|

[Then the open decisions with owners and dates.]

## 6. Interfaces and access

| ID | System or access | Lead time | Needed by | Status |
|---|---|---|---|---|

## 7. Clinical safety and data protection

[The possible hazards, the status of the DPIA, and the decisions needed]

## 8. First slice

[The scope, the journeys and the riskiest assumption that it tests]

## 9. Plan

| Phase | Start | End | Exit criteria |
|---|---|---|---|

## 10. Top risks

| ID | Risk | Owner | Mitigation |
|---|---|---|---|

## 11. Acceptance approach

[How we prove each requirement]

## 12. What each party must do

| Party | Action | Owner | Date |
|---|---|---|---|

## 13. Sign-off

Questions:

- Does each party agree what it must do, and by when?
- Is the scope of the first release agreed?
- Is the access for the first build available, or does it have a firm date?
- Does each open decision have an owner and a date?

| Name | Role | Organisation | Decision | Date |
|---|---|---|---|---|

## Gaps that can stop sign-off

- [Gap, owner, date]
```
