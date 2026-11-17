---
name: persona-slides
description: 'Use when you need a Marp slide deck of the personas in discovery/personas/persona-report.md, for persona checks with users and for the playback. Triggers: persona slides, persona deck, persona JSON, /persona-slides. Writes discovery/personas/data/*.json and discovery/personas/generated/personas-deck.md.'
---

# Persona Slides

Make a slide deck with one slide for each persona. The flow is: persona report, then one JSON file for each persona, then the script makes the deck.

This skill needs the repository scripts, so it has no Microsoft 365 Copilot prompt file. People with only Microsoft 365 Copilot can ask Copilot in PowerPoint to make a deck from `discovery/personas/persona-report.md`.

`discovery/personas/persona-report.md` is the source of truth. The deck is a summary of it. Do not add facts that are not in the report. If the report does not exist, make it first. See [discovery/prompts/personas.md](../../../discovery/prompts/personas.md), or run the `personas` skill.

## Files

| File | Contents |
|---|---|
| `discovery/personas/persona-report.md` | Input: the persona report. The user can give a different path or a URL |
| `discovery/personas/data/<name>.json` | Slide data that you write, one file for each persona |
| `discovery/personas/templates/persona-template.md` | The Marp template with the NHS style |
| `discovery/personas/scripts/generate-slides.js` | The script that makes the slides |
| `discovery/personas/generated/personas-deck.md` | The deck that the script makes. Do not edit it |

## Step 1: Get the personas from the report

Read the report. Find each persona and its workflow stage. For each persona, find:

- The name and the role.
- The background and the experience.
- The goals and the outcomes.
- The wants, the needs and the expectations.
- The pain points and the unmet needs.
- The other context: a typical day, technical skill, relationships.

## Step 2: Write one JSON file for each persona

Write each file in `discovery/personas/data/`. Use this format:

```json
{
  "name": "Fictional first name and last name",
  "jobTitle": "Job title - specialty or band",
  "slideTitle": "ROLE CATEGORY IN CAPITALS",
  "experience": "X years in the role, with the relevant background",
  "location": "NHS organisation or region",
  "department": "Department or service name",
  "photo": "../images/placeholder.jpg",
  "photoPrompt": "A prompt for an AI-generated headshot of this fictional person",
  "background": ["4 to 6 items"],
  "goals": ["4 to 6 items"],
  "wants": ["4 to 6 items"],
  "painPoints": ["4 to 6 items"]
}
```

File names:

- NHS staff: `firstname-job-role.json`. Example: `amara-clinic-administrator.json`.
- Patients: `firstname-patient-context.json`. Example: `tom-patient-diabetes.json`.
- Carers: `firstname-carer-context.json`.

Rules for the slide data:

- Copy the facts from the report. Shorten the text. Do not change the meaning.
- Write each item so that it is specific to this person, not a general role.
- Write goals, wants and pain points from the point of view of the person.
- Write 4 to 6 items in each list.
- Write the `slideTitle` as the role category in capitals. Example: "CLINIC ADMINISTRATOR".
- Keep `Assumption:` on each item that the report marks as an assumption.
- If the report has the banner "AI draft — not user research", keep the word "Draft" in the deck title or footer.
- Use fictional names only. Never add real patient data.

Rules for images:

- Never use a photo of a real person.
- Write `photoPrompt` for an AI-generated, photorealistic headshot of a fictional person. Give the age range, appearance, clothes for the role, expression, lighting and background. Write "no text". Do not copy appearance details that the report does not support.
- Until you have an AI-generated image, keep `photo` as the placeholder path. Put generated images in `discovery/personas/images/`.

## Step 3: Make the deck

Run this command:

```bash
cd discovery/personas && node scripts/generate-slides.js combined
```

The script reads all JSON files in `data/`, uses `templates/persona-template.md`, and writes `generated/personas-deck.md`. The script needs Node.js.

If the script gives errors, correct the JSON files. Then run the script again.

## Step 4: Check the output

1. Make sure that there is one JSON file for each persona in the report.
1. Make sure that `discovery/personas/generated/personas-deck.md` exists.
1. Report the personas that you processed and any data that was missing in the report.

## Preview and export

- Preview: open the deck in VS Code with the Marp extension. Press Ctrl+Shift+V on Windows or Cmd+Shift+V on macOS.
- Export with Marp CLI. Use the pinned version. The persona template uses HTML, so add `--html`:

  ```bash
  npx @marp-team/marp-cli@4.5.1 discovery/personas/generated/personas-deck.md --html --pdf
  npx @marp-team/marp-cli@4.5.1 discovery/personas/generated/personas-deck.md --html --pptx
  ```

Tell the user to check each slide before they use it.

## When the personas change

1. Update the JSON files from the report.
1. Run the script again.
