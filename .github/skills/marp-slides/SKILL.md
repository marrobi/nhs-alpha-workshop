---
name: marp-slides
description: 'Use when you need to turn a Markdown document into a Marp slide deck with the NHS style, for example a weekly status report, a playback pack, a problem statement, a decision log or workshop notes. Triggers: slides, deck, presentation, Marp, PowerPoint, PDF, /marp-slides.'
---

# Marp Slides

Make a Marp slide deck from one Markdown document. Examples of source documents:

- `discovery/status/YYYY-MM-DD.md`
- `discovery/playback/playback-pack.md`
- `discovery/scenarios/scenario.md`
- `discovery/decision-log.md`
- `discovery/workshops/YYYY-MM-DD-<topic>.md`

For persona slides, use the `persona-slides` skill. It has its own template and script.

This skill needs Marp, so it has no Microsoft 365 Copilot prompt file. People with only Microsoft 365 Copilot can ask Copilot in PowerPoint to make a deck from the same source document. They do not need Marp.

## Rules

- The source document is the source of truth. The deck is a summary of it.
- Never add facts that are not in the source document. Keep `Assumption:` on each assumption.
- Write one idea on each slide.
- Write short bullets. Write a maximum of 6 bullets on a slide. Write a maximum of 12 words in a bullet.
- Keep the identifiers from the source. Examples: `DEC-001`, `R-003`, `UC-01`.
- Include a slide with the source path, the date and the status of the source document.
- Link to the source document with a relative path from the deck.
- Never add real patient data.
- Never add photos of real people.
- Write in Simplified Technical English.

## Steps

1. Read the source document that the user gives. If the user gives no document, ask for one with the ask questions tool, if it is available. Otherwise, ask in the chat.
1. Find the date and the status of the source. If the source has no status, use "Draft".
1. Plan the slides. Use the headings of the source as the order. Show the plan to the user if the source has more than 15 sections.
1. Write the deck. Use the structure below.
1. Save the deck to `<source-dir>/slides/<source-name>-deck.md`, unless the user gives a different path. Example: the deck for `discovery/status/2026-11-02.md` is `discovery/status/slides/2026-11-02-deck.md`.

## Deck structure

Copy all the rules in [templates/nhs-theme.css](templates/nhs-theme.css) into the `style: |` block. Indent them by 2 spaces.

```markdown
---
marp: true
theme: default
paginate: true
size: 16:9
header: 'Draft'
footer: '[Service name] | [Source title] | [YYYY-MM-DD]'
style: |
  [the rules from templates/nhs-theme.css]
---

<!-- _class: title -->
<!-- _paginate: false -->

# [Source title]

[Service name]
[YYYY-MM-DD]

---

## [One idea]

- [Short bullet]
- [Short bullet]

---

<!-- _class: source -->

## Source

| Item | Value |
|---|---|
| Source | [relative link to the source document] |
| Date of source | [YYYY-MM-DD] |
| Status | [Draft, Agreed or the status in the source] |
| Made with | `marp-slides` skill |

Check each slide against the source before you use the deck.
```

Use the `header` for the status of the source. Remove the `header` line if the status is "Agreed".

## Preview and export

- Preview: open the deck in VS Code with the Marp extension. Press Ctrl+Shift+V on Windows or Cmd+Shift+V on macOS.
- Export to PDF with Marp CLI. Use the pinned version:

  ```bash
  npx @marp-team/marp-cli@4.5.1 <deck> --pdf
  ```

- Export to PowerPoint:

  ```bash
  npx @marp-team/marp-cli@4.5.1 <deck> --pptx
  ```

## Validation

- Make sure that each fact on a slide is in the source document.
- Make sure that the deck has a title slide and a source slide.
- Make sure that the link to the source goes to a file that exists.
- Tell the user to check each slide before they use the deck.
