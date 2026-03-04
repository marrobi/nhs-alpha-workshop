---
agent: agent
---

# Generate Persona Slide Deck from Researcher Report

Generate a complete Marp slide deck from a user research report markdown file. This agent combines persona extraction, JSON conversion, and slide generation into a single workflow.

## Input Required

Provide a link or path to the researcher report markdown file (e.g., `discovery/personas/persona-report.md` or a URL to a markdown file).

## Process Overview

This agent will:

1. **Read the research report** and extract all persona descriptions
2. **Convert each persona to JSON** following the specified schema
3. **Generate individual Marp slides** for each persona
4. **Create a combined deck** with all personas organized by workflow stage
5. **Output files** to the `discovery/personas/` directory structure

## Step 1: Extract Personas from Report

Read the provided markdown file and identify all persona sections. Look for:
- Persona names and roles
- Background and experience details
- Goals and desired outcomes
- Wants, needs, and expectations
- Pain points and unmet needs
- Additional context (day-in-life, technical proficiency, relationships, etc.)

Extract personas organized by their workflow stage categories (e.g., Planning & Governance, Referral, Consultation, etc.)

## Step 2: Convert to JSON

For each persona, create a JSON file in `discovery/personas/data/` following this structure:

```json
{
  "name": "Fictional First Name + Last Name",
  "jobTitle": "Job Title - Specialization/Band",
  "slideTitle": "ROLE CATEGORY IN CAPS",
  "experience": "X years in role/field with relevant background",
  "location": "NHS Organization/Region",
  "department": "Department/Service Name",
  "photo": "../images/placeholder.jpg",
  "photoPrompt": "Detailed AI image generation prompt for a photorealistic professional headshot",
  "background": [
    "Specific background point 1 about this individual",
    "Specific background point 2 (qualifications, specializations)",
    "Specific background point 3 (areas of focus, experience)",
    "Additional relevant background (4-6 items total)"
  ],
  "goals": [
    "Specific goal 1 from their perspective",
    "Specific goal 2 with measurable outcome",
    "Specific goal 3 related to the system",
    "Additional goals (4-6 items total)"
  ],
  "wants": [
    "Specific want/need 1 with context",
    "Specific want/need 2 (technology, workflow)",
    "Specific want/need 3 (support, information)",
    "Additional wants/needs (4-6 items total)"
  ],
  "painPoints": [
    "Specific pain point 1 with impact",
    "Specific pain point 2 (workflow, system issue)",
    "Specific pain point 3 (time, stress, safety concern)",
    "Additional pain points (4-6 items total)"
  ]
}
```

**JSON File Naming Convention:**
- For NHS staff: `firstname-job-role.json` (e.g., `sanjay-programme-director.json`)
- For patients: `firstname-patient-context.json` (e.g., `ellie-young-patient-crohns.json`)
- For carers: `firstname-carer-context.json`

**Key Requirements:**
1. **Specific, not generic** - Each item should feel personal to this individual, not a role template
2. **Photo prompt** - Create detailed AI image generation prompt (age, ethnicity, professional appearance, context-appropriate setting)
3. **Slide title** - Should be the role category in CAPS (e.g., "PROGRAMME DIRECTOR", "IBD NURSE SPECIALIST", "PATIENT ADVOCATE")
4. **4-6 items per section** - Background, goals, wants, and pain points should each have enough detail
5. **Authentic context** - Use real job titles, organizations, qualifications from the research
6. **First person perspective** - Write as if this person is speaking (goals, wants, pain points)

**Photo Prompt Guidelines:**
Include: age range, ethnicity/diversity, professional attire, expression, lighting, background, image quality details. Example:
"Photorealistic headshot of a senior NHS programme director (mid-40s, South Asian British man), business professional attire (navy suit, open collar shirt), confident warm expression, soft studio lighting, modern office background slightly blurred, sharp focus, natural skin tones, no text"

## Step 3: Generate Slides

Once all JSON files are created in `discovery/personas/data/`, run the slide generation script:

```bash
cd discovery/personas
node scripts/generate-slides.js combined
```

This will:
1. Read all JSON files from `discovery/personas/data/`
2. Apply the Marp template from `discovery/personas/templates/persona-template.md`
3. Generate a single combined deck at `discovery/personas/generated/personas-deck.md`
4. The deck will contain one slide per persona, grouped by workflow stage

## Step 4: Verify Output

Ensure the following files are created:

**JSON Files:**
- `discovery/personas/data/[persona-name].json` (one per persona)

**Generated Deck:**
- `discovery/personas/generated/personas-deck.md` (combined Marp presentation)

**Optional Individual Slides:**
- `discovery/personas/generated/[persona-name]-slide.md` (if generated individually)

## Step 5: Next Steps (Optional)

After generation, you can:
- **View in VS Code** with the Marp extension
- **Export to PDF**: `marp discovery/personas/generated/personas-deck.md --pdf`
- **Export to PowerPoint**: `marp discovery/personas/generated/personas-deck.md --pptx`
- **Export to HTML**: `marp discovery/personas/generated/personas-deck.md --html`

## Example Usage

When invoked, provide the path to the research report:

```
User: Generate slides from discovery/personas/persona-report.md
```

or

```
User: Generate slides from https://example.com/research/personas.md
```

The agent will then:
1. Read the report
2. Extract all personas
3. Create JSON files in `discovery/personas/data/`
4. Run the slide generator
5. Output the combined deck to `discovery/personas/generated/personas-deck.md`

## Success Criteria

- All personas from the report are extracted and converted to JSON
- Each JSON file follows the specified schema with 4-6 items per section
- Photo prompts are detailed and context-appropriate
- Combined Marp deck is generated successfully
- Slides are organized by workflow stage
- Each slide is visually consistent using the template
- All files are saved in the correct directory structure

## Error Handling

If any step fails:
- Report which personas were successfully processed
- Identify any missing or malformed data
- Suggest corrections to the input report format
- Provide partial output if some personas succeeded

## Notes

- This agent replaces the need to run separate persona conversion and slide generation prompts
- The agent assumes the `discovery/personas/scripts/generate-slides.js` script exists and is functional
- The agent assumes the `discovery/personas/templates/persona-template.md` template exists
- Ensure Node.js is available in the environment to run the generation script
