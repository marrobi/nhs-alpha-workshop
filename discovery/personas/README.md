# NHS Persona Slides Generator

This directory contains everything needed to generate NHS persona slides with separated content and styling.

## Directory Structure

```
personas/
├── data/                    # JSON files with persona content
│   └── (place your JSON files here)
├── templates/               # Marp template with styling
│   └── persona-template.md
├── scripts/                 # Generation script
│   └── generate-slides.js
├── images/                  # Logo and image assets
│   ├── nhs-logo.jpg
│   └── microsoft-logo.png
├── generated/              # Output directory for final slides
│   └── (generated slides appear here)
└── README.md              # This file
```

## Quick Start

### 1. Add Persona Data
Place your JSON files generated from the Copilot prompt into the `data/` directory.

### 2. Add Images (Optional)
- Add NHS and Microsoft logos to `images/` directory
- Add persona photos as referenced in your JSON files

### 3. Generate All Slides
```bash
cd discovery/personas
node scripts/generate-slides.js
```

### 4. Generate Combined Deck
```bash
cd discovery/personas
node scripts/generate-slides.js combined
```

### 5. Generate Specific Slide
```bash
cd discovery/personas
node scripts/generate-slides.js sarah-midwife.json
```

### 6. List Available Personas
```bash
cd discovery/personas
node scripts/generate-slides.js list
```

## Commands

| Command | Description |
|---------|-------------|
| `node scripts/generate-slides.js` | Generate individual slides for all personas |
| `node scripts/generate-slides.js combined` | Generate single combined deck |
| `node scripts/generate-slides.js combined my-deck.md` | Generate combined deck with custom name |
| `node scripts/generate-slides.js list` | List all available persona files |
| `node scripts/generate-slides.js filename.json` | Generate slide for specific persona |
| `node scripts/generate-slides.js help` | Show help information |

## JSON File Format

Each persona should be a JSON file in `data/` following this structure:

```json
{
  "name": "Full Name",
  "jobTitle": "Job Title - Specialization",
  "slideTitle": "ROLE CATEGORY IN CAPS",
  "experience": "X years experience",
  "location": "NHS Trust/Region",
  "department": "Department Name",
  "photo": "../images/photo.jpg",
  "photoPrompt": "AI image generation prompt",
  "background": [
    "Background item 1",
    "Background item 2"
  ],
  "goals": [
    "Goal 1",
    "Goal 2"
  ],
  "wants": [
    "Want/Need 1",
    "Want/Need 2"
  ],
  "painPoints": [
    "Pain point 1",
    "Pain point 2"
  ]
}
```

## Workflow

1. **Generate Scenario** - Use `.github/prompts/generate_scenario_and_problem_statement.prompt.md`
2. **Generate Personas** - Use `persona-generation-prompt.md` with M365 Copilot Researcher
3. **Generate JSON** - Use `.github/prompts/generate_persona_slides_from_report.prompt.md`
4. **Place JSON Files** - Copy generated JSON files to `discovery/personas/data/`
5. **Generate Slides** - Run `node scripts/generate-slides.js combined`
6. **Present** - Open generated deck in Marp or VS Code with Marp extension

## Viewing Slides

### VS Code with Marp Extension
1. Install the Marp for VS Code extension
2. Open any generated `.md` file in `generated/`
3. Click the Marp preview button

### Export to PDF/PowerPoint
Use Marp CLI:
```bash
npm install -g @marp-team/marp-cli
marp generated/personas-deck.md --pdf
marp generated/personas-deck.md --pptx
```

## Customization

### Modify Template Styling
Edit `templates/persona-template.md` to change:
- Colors (NHS blue: #005eb8)
- Layout and spacing
- Font sizes
- Section arrangement

### Modify Content Structure
Update both:
- `templates/persona-template.md` (add new placeholders)
- `scripts/generate-slides.js` (handle new fields)
- `.github/prompts/generate_persona_slides_from_report.prompt.md` (update JSON structure)

## Troubleshooting

**No slides generated**
- Check that JSON files are in `data/` directory
- Verify JSON files are valid (use a JSON validator)

**Missing images**
- Ensure image paths in JSON match actual file locations
- Use relative paths from the generated slide location

**Formatting issues**
- Check template syntax in `persona-template.md`
- Verify all placeholders are properly closed with `}}`

## Requirements

- Node.js (v14 or higher)
- Marp for VS Code extension (for viewing)
- Optional: @marp-team/marp-cli (for exports)
