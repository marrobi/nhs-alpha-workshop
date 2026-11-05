---
name: engineering-review
description: 'Use when a hard design question needs a decision (Decide mode), or when a built batch or a day of commits needs an independent review (Review mode). Runs 2 or 3 read-only NHS Engineer subagents on different models that the user chooses (asks if none are given), then combines their results.'
---

# Engineering Review

A multi-model engineering team makes hard design decisions and reviews built work. Engineers on different models find different risks, so the team catches more than one agent. It is slower and costs more than one agent, so use it where judgement matters, not for routine implementation.

The team does **not** implement user stories. The NHS Service Builder does that, and it is the only agent that writes to its checkout.

## When to use it

| When | Mode | Who leads | Engineers | How it starts |
|---|---|---|---|---|
| A hard design question comes up during the build | Decide | **NHS Service Builder**. It stops editing until the decision is made. | 3 | The builder loads this skill and starts the engineers itself |
| A hard design question comes up during architecture or ADR work | Decide | **NHS Architect** | 3 | The team asks the Architect: "Decide mode: …" |
| A journey-aligned batch is built, tested and committed | Review | **NHS Quality Engineer** | 2 | The team asks the Quality Engineer: "Review mode: commits …" |
| End of each day (workshop) or sprint (multi-week), or a batch that changed something many stories depend on | Review | **NHS Quality Engineer** | 3 | Same as above, with the commits of the day or sprint |

Read the review cadence in `.github/instructions/delivery-mode.instructions.md`.

A hard design question is one of these:

- The acceptance criteria of a story are open, or they conflict with an ADR.
- A change affects something that many stories depend on.
- There is a security, privacy or clinical-safety trade-off.
- You must choose between data-modelling or integration options.
- The decision needs a new ADR.

Do not use the team for routine implementation choices.

## Choose the engineers' models

The repository does not pin models. Models change, and organisations allow different models. A person chooses the models each time the team runs:

1. If the user names models in the request, use those models.
2. If the user does not name models, **stop and ask** before you start any engineer. Ask for 2 models for a batch review, or 3 models for Decide mode or an end-of-day review. Suggest models that are available in this session, from **different providers** where possible, with strong code and review capability. Do not choose the models yourself, and do not start the engineers until the user answers.
3. If the user names fewer models than the mode needs, ask for the others. If the user agrees, you can use the same model for more than one engineer. Record this in the report.
4. Start each engineer as an `NHS Engineer` subagent and pass its model as the model parameter of the subagent call. In VS Code, the lead can also ask in its prompt: "Use a subagent with <model name> …".
5. The lead's own model must be in the same or a higher cost tier than the engineers' models. VS Code does not run a subagent whose model is in a higher tier than the main chat's model. If a model that the user names is in a higher tier, tell the user and ask for another model.
6. If only one model is available, tell the user and ask whether to continue with one model. Do not report one model as a multi-model review.
7. Record each engineer's model in the **Team used** table of the report.

## Rules for every task

- Follow `AGENTS.md`, `.github/instructions/` and the accepted ADRs in `docs/adr/`.
- Decide on evidence, not votes. Prefer repository evidence, executable tests, security and operational safety, and the smallest complete solution.
- The lead and the engineers never edit repository files and never commit. Fixes go back to the NHS Service Builder.
- If a required tool, container or service is unavailable, stop and report it. Never invent results.
- Make every engineer prompt self-contained. Include the mode, the question or the exact commit SHAs, the relevant files, acceptance criteria, ADRs and repository rules, the validation commands, and the progress-file path.
- To run engineers as separate chats, to monitor them, and for progress logs and shared-machine limits, read [operations.md](./references/operations.md).

## Review isolation

- Review exact commit SHAs or another fixed snapshot, never a working tree that is changing.
- The NHS Service Builder is the only writer in its checkout. Engineers can run builds and tests, so a terminal can still write files. Do not run engineers in the builder's checkout while the builder is working.
- If the builder must continue while a review runs, ask the delivery team to create a separate checkout or detached worktree at the commits under review. Run every engineer in that checkout.
- If the commits are not available, the checkout has uncommitted changes that belong to the review, or the scope changes while you work, stop and ask the delivery team for a fixed scope.

## Decide mode

1. **Investigate.** Start all three engineers in Investigate mode with the same question. They edit nothing.
2. **Challenge.** Show each engineer the other proposals and ask it to challenge them on evidence.
3. **Decide.** Recommend one option. Record the decision, the alternatives that you rejected and why, the consequences, and what the builder must do.
4. **Draft an ADR if needed.** Use the `nhs-adr-writer` skill and put the draft in your report. The NHS Architect or the delivery team accepts it. Do not commit it.

People make the final decision. Aim to finish within 30 minutes.

## Review mode

1. **Fix the scope.** Record the base commit and every commit SHA in the batch. Make sure that the review checkout is at those commits and has no later builder changes.
2. **Start the engineers.** Use 2 engineers for a batch review and 3 for an end-of-day review. Each engineer reviews the same commits independently.
3. **What to check.**
   - The acceptance criteria of the stories.
   - `AGENTS.md`, `.github/instructions/` and the ADRs.
   - Authorisation, input validation and error responses.
   - Secrets and personal data in logs.
   - NHS Design System use and WCAG 2.2 AA.
   - Test coverage, including E2E and accessibility checks.
   - No mocks of external services in integration tests.
4. **Combine the findings.** Remove duplicates. Check each disputed claim, and each claim from only one engineer, against the code yourself. Record disagreements and how you resolved them. Use the severity levels in `.github/instructions/review-agent-pattern.instructions.md`.

Aim to finish a batch review within 30 minutes. The delivery team gives Critical and High findings to the NHS Service Builder, which fixes them before the next batch.

## Report

Start every report with a **Team used** table, with one row for each engineer: the model, how it ran (subagent or chat link), whether it returned a result, whether it had shell access, and what it found.

Then give:

- **Scope.** The base commit, the reviewed commit SHAs, the stories and the acceptance criteria.
- **Decide mode.** The recommendation, the options considered, the consequences, what the builder must do, and an ADR draft if needed.
- **Review mode.** The findings, each with severity, `file:line` and the fix. Then a verdict: **MERGE** or **FIX-THEN-MERGE**, with the list of blocking findings.
- **Validation.** The commands and results that you relied on, and what was not run.
- **Remaining risks** and disagreements.

Keep it concise and based on evidence.
