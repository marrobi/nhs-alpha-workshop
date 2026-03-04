# User Journeys

This directory contains user journey documentation and diagrams that illustrate key workflows and functionality in the system.

## Directory Structure

```
user_journeys/
├── data/                    # Journey documentation files (markdown with embedded diagrams)
│   └── journey-[name].md    # Each journey as a single markdown file
├── templates/               # Templates for documentation
│   └── journey-template.md  # Template with sections and Mermaid diagram examples
└── README.md               # This file
```

### About the Directories

- **`data/`** - Stores your journey documentation files. Each file is a complete journey with embedded Mermaid diagrams in markdown format. Files are named `journey-[short-name].md` (e.g., `journey-patient-registration.md`)
- **`templates/`** - Contains the journey template markdown file showing the structure and where diagrams go
- **Diagrams** - Mermaid diagrams are embedded directly in the markdown files using code blocks, so they render alongside the journey documentation

## How to Use

### Step 1: Generate Journey Suggestions

1. Gather your **Scenario Overview**, **Problem Statement**, and **Personas**
2. Open `.github/prompts/generate_user_journeys.prompt.md` in VS Code
3. Invoke the prompt (using Copilot chat or prompt picker)
4. Provide your scenario context
5. Copilot will suggest 5-8 user journeys

### Step 2: Select Journeys to Develop

Review the suggestions and identify which journeys best demonstrate your system's key functionality.

### Step 3: Generate Detailed Journeys

Ask Copilot to create detailed documentation for your selected journeys, including:
- Step-by-step interactions
- Decision points and variations
- Touchpoints and channels
- Mermaid diagrams

### Step 4: Save Journey Documentation

Save the generated journey documentation to the `data/` directory as a single markdown file that includes embedded diagrams.

Naming convention:
```
journey-[short-name].md
```

Example files:
- `journey-patient-registration.md`
- `journey-appointment-booking.md`
- `journey-clinical-assessment.md`

Each file contains:
- Journey narrative and tables
- Embedded Mermaid diagrams in markdown code blocks
- All relevant context in one place

## Journey Documentation Template

Each journey file should follow this structure:

```markdown
# Journey: [Title]

**Primary Actor:** [Persona name]  
**Duration:** [Estimated timeframe]  
**Preconditions:** [What must be true before journey starts]  
**Success Criteria:** [How we know the journey succeeded]

## Overview
[2-3 paragraphs describing the journey context and importance]

## Main Flow
[Step-by-step table with Actor, Action, System Response, Notes]

## Decision Points
[Variations and alternative paths]

## Touchpoints
- Digital: [Apps, web interfaces, SMS, etc.]
- Physical: [In-person interactions, forms, etc.]
- People: [Staff/role interactions]

## Pain Points & Opportunities
[Key challenges and improvement areas]

## Related Personas
[Who else is involved in this journey]

## Related Journeys
[Other journeys that intersect with this one]
```

## Viewing Journey Diagrams

Diagrams are embedded directly in the journey markdown files, so they render automatically when you open the file in VS Code.

### VS Code with Mermaid Extension (Recommended)
1. Install the **Mermaid Markdown Syntax Highlighting** extension
   - or **Markdown Preview Mermaid Support**
2. Open any journey file from `data/` directory
3. Click "Open Preview to the Side" (Ctrl+K V / Cmd+K V)
4. Mermaid diagrams render in the preview panel alongside the journey documentation

### Exporting Diagrams

To export individual diagrams as PNG/SVG:

1. Copy the Mermaid code block from the markdown file
2. Paste into https://mermaid.live
3. Export as PNG/SVG from there

Or install Mermaid CLI for batch exports:
```bash
npm install -g @mermaid-js/mermaid-cli
mmdc -i data/journey-name.md -o journey-name.png
```

## Example Journey Structure

A complete user journey typically includes:

1. **Context Setup** - Who, when, where, why
2. **Entry Point** - How the journey begins
3. **Main Activities** - Primary steps toward the goal
4. **Decision Gates** - Choice points and variations
5. **Supporting Activities** - Help desk, escalation, etc.
6. **Resolution** - How the journey ends successfully
7. **Exception Handling** - What happens when things go wrong

## Best Practices

- **One actor per journey** - Focus on a primary persona's perspective
- **Define scope clearly** - Start/end points are unambiguous
- **Include alternatives** - Happy path AND edge cases
- **Show interactions** - How multiple personas and systems interact
- **Be specific** - Name screens, forms, documents
- **Make it testable** - Success criteria should be measurable
- **Accessibility first** - Consider WCAG 2.1 AA requirements throughout

## Workflow Integration

These user journeys complement the personas and scenarios:

```
Scenario & Problem Statement
         ↓
      Personas
         ↓
    User Journeys ← You are here
         ↓
  Process Flows / Wireframes
         ↓
   Implementation & Testing
```

## Mermaid Diagram Types

When asking Copilot to generate diagrams for your journeys, request:
- **`sequenceDiagram`** - Show actor interactions over time (best for multi-actor workflows)
- **`flowchart`** - Show decision points and process flows (best for complex paths)
- **`timeline`** - Show journeys spanning days/weeks/months (best for long-duration journeys)
- **`graph`** - Show relationships between systems/actors (best for architecture)

Each diagram is embedded as a Mermaid code block in the markdown file:
```markdown
## Actor Interactions

` ` `mermaid
sequenceDiagram
    Patient->>System: Registers
    System->>GP: Sends referral
    GP->>System: Confirms receipt
` ` `
```

## File Examples

- `journey-registration.md` - Patient registration process
- `journey-appointment.md` - Booking and attending appointment
- `journey-referral.md` - Specialist referral workflow
- `journey-data-entry.md` - Clinical data entry process
