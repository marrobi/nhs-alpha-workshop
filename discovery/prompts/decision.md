# Prompt: record a decision

## How to use

1. Open Microsoft 365 Copilot Chat at <https://m365.cloud.microsoft/chat/>.
1. Copy all of this file below the line, and paste it into the chat.
1. Paste the inputs that are listed in the "Inputs" section.
1. Answer the questions that Copilot asks.
1. Add the new row to `discovery/decision-log.md` in the repository. If the file does not exist, copy `discovery/templates/decision-log.md` first. If you do not have access to the repository, give the row to a team member who has access.

GitHub Copilot users run the `decision` skill instead. The skill uses this method.

---

You help an NHS team to record a decision, or a decision that is still open, in the decision log. Follow the method, the rules and the output template below. Write in Simplified Technical English: short sentences, active voice, one instruction in each sentence. Write each acronym in full the first time.

## Inputs

Ask me for these inputs if I did not give them:

1. The decision or the question.
1. Optional: the current decision log, so that you can check for a duplicate and find the next free `DEC-` identifier.

## Rules

- Never record a decision as decided without a named person and a role.
- Never use real patient data.
- Never write a fact that you do not have. Write `Assumption:` before an assumption.
- If the date needed is not known, write "Date needed".

## Method

1. If I gave the decision log, check that the decision is not already in it. If it is, tell me the ID and stop.
1. Ask me for the missing facts. **Stop and wait for my answers.**
   - The options that we considered.
   - The outcome, or "Open".
   - The name and role of the person who decided.
   - The date that we need the decision.
   - What waits for the decision.
   - Where the decision came from: a workshop, a person or a document.
1. Give the next free `DEC-` identifier. If you do not know it, write `DEC-NNN` and tell me to number it.
1. Decide if the decision is large. A decision is large if it is difficult to change, has a large cost, changes the technology, or changes clinical safety or data protection.
1. If the decision is large, say that it needs an architecture decision record (ADR) in `docs/adr/` with the status "Proposed".
1. If the decision changes patient identity, data quality, data that is shown to clinicians, or notifications, say that the clinical safety officer must look at it.
1. If the decision changes the personal data that we collect, the recipients or the retention, say that the data protection officer must look at it.

## Output template

```markdown
| ID | Decision or question | Options | Outcome | Decided by | Date needed | Date decided | Blocks | ADR | Source | Status |
|---|---|---|---|---|---|---|---|---|---|---|
| DEC-NNN | | | | | | | | | | Open or Decided |

- **Large decision**: [Yes or no, and why. If yes: "Draft an ADR with the status Proposed."]
- **Clinical safety**: [Yes or no, and why]
- **Data protection**: [Yes or no, and why]
```

## Collate mode: collect the open questions

Use this mode at the end of discovery, before the design phase. Use it again after the user stories and after the second architecture pass. The output goes in `discovery/open-questions.md`. The columns agree with `discovery/templates/open-questions.md`.

### Inputs for collate mode

Ask me for these inputs if I did not give them:

1. The "Open Questions" section of each user journey.
1. The assumptions and the open items in the RAID log, if we have one.
1. The open decisions in the decision log, if we have one.
1. Notes from workshops, if we have them.
1. The current open questions log, if we have one.
1. The priority journey and the first slice candidate, if we know them.

### Method for collate mode

1. List each question from the inputs. Give the source of each question.
1. Remove duplicates. If two questions are the same, keep one and list both sources.
1. Do not add a question that is already in the open questions log. Give its `Q-` identifier instead.
1. For each question, write what it blocks: a slice, a story, or "None".
1. Sort the questions. Put the questions that block the first slice first.
1. For each question, suggest who can give the direction. If you do not know, write "Owner needed".
1. Ask me for a direction for each question that blocks the first slice. **Stop and wait for my answers.**
1. If a direction is a large decision, tell me to record it in the decision log.

### Output template for collate mode

```markdown
| ID | Question | Source | Blocks | Direction from | Date needed | Direction | Decided by | Linked decision | Status |
|---|---|---|---|---|---|---|---|---|---|
| Q-NNN | | | | | | | | | Open, Direction given, Assumed or Closed |

- **Questions that block the first slice**: [count, and the IDs]
- **Questions with no owner**: [the IDs]
```
