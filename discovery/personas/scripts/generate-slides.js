#!/usr/bin/env node

const fs = require('fs');
const path = require('path');

class PersonaSlideGenerator {
  constructor() {
    this.templatePath = path.join(__dirname, '../templates/persona-template.md');
    this.personasDir = path.join(__dirname, '../data');
    this.outputDir = path.join(__dirname, '../generated');
  }

  /**
   * Load the template file
   */
  loadTemplate() {
    if (!fs.existsSync(this.templatePath)) {
      throw new Error(`Template file not found: ${this.templatePath}`);
    }
    return fs.readFileSync(this.templatePath, 'utf8');
  }

  /**
   * Load persona data from JSON file
   */
  loadPersonaData(filePath) {
    if (!fs.existsSync(filePath)) {
      throw new Error(`Persona file not found: ${filePath}`);
    }
    const content = fs.readFileSync(filePath, 'utf8');
    return JSON.parse(content);
  }

  /**
   * Generate HTML list items from array
   */
  generateListItems(items, indent = '          ') {
    if (!Array.isArray(items)) return '';
    return items.map(item => `${indent}<li>${item}</li>`).join('\n');
  }

  /**
   * Replace placeholders in template with persona data
   */
  generateSlide(template, personaData) {
    let result = template;

    // Replace simple string placeholders
    const simpleFields = ['name', 'jobTitle', 'experience', 'location', 'department', 'photo', 'sampleText', 'slideTitle'];
    simpleFields.forEach(field => {
      if (personaData[field]) {
        const placeholder = `{{${field}}}`;
        result = result.replace(new RegExp(placeholder, 'g'), personaData[field]);
      }
    });

    // Replace list placeholders
    const listFields = {
      'backgroundItems': personaData.background || [],
      'goalItems': personaData.goals || [],
      'wantItems': personaData.wants || [],
      'painPointItems': personaData.painPoints || []
    };

    Object.entries(listFields).forEach(([placeholder, items]) => {
      const listHtml = this.generateListItems(items);
      result = result.replace(`{{${placeholder}}}`, listHtml);
    });

    return result;
  }

  /**
   * Generate slide for a specific persona file
   */
  generatePersonaSlide(personaFileName) {
    const personaPath = path.join(this.personasDir, personaFileName);
    const template = this.loadTemplate();
    const personaData = this.loadPersonaData(personaPath);
    
    const slide = this.generateSlide(template, personaData);
    
    // Generate output filename
    const baseName = path.basename(personaFileName, '.json');
    const outputFileName = `${baseName}-slide.md`;
    const outputPath = path.join(this.outputDir, outputFileName);
    
    // Ensure output directory exists
    if (!fs.existsSync(this.outputDir)) {
      fs.mkdirSync(this.outputDir, { recursive: true });
    }
    
    // Write generated slide
    fs.writeFileSync(outputPath, slide, 'utf8');
    
    console.log(`✓ Generated slide: ${outputFileName}`);
    console.log(`  From: ${personaFileName}`);
    console.log(`  Output: ${outputPath}\n`);
    
    return outputPath;
  }

  /**
   * Generate slides for all persona files
   */
  generateAllSlides() {
    if (!fs.existsSync(this.personasDir)) {
      throw new Error(`Personas directory not found: ${this.personasDir}`);
    }

    const personaFiles = fs.readdirSync(this.personasDir)
      .filter(file => file.endsWith('.json'))
      .sort((a, b) => a.localeCompare(b));

    if (personaFiles.length === 0) {
      console.log('No persona files found in:', this.personasDir);
      return [];
    }

    console.log(`Found ${personaFiles.length} persona files. Generating slides...\n`);

    const generatedFiles = personaFiles.map(file => 
      this.generatePersonaSlide(file)
    );

    console.log(`Successfully generated ${generatedFiles.length} slides!`);
    return generatedFiles;
  }

  /**
   * Extract front matter (first two '---' fences) and body from a template-rendered slide
   */
  extractFrontMatter(rendered) {
    if (!rendered.startsWith('---')) {
      return { front: '', body: rendered };
    }
    // Find the second '---' that ends the front matter block
    const lines = rendered.split('\n');
    let secondIndex = -1;
    for (let i = 1; i < lines.length; i++) {
      if (lines[i].trim() === '---') { secondIndex = i; break; }
    }
    if (secondIndex === -1) {
      return { front: rendered, body: '' }; // malformed but return all as front
    }
    const front = lines.slice(0, secondIndex + 1).join('\n');
    const body = lines.slice(secondIndex + 1).join('\n');
    return { front, body };
  }

  /**
   * Generate a single combined Marp deck containing all personas.
   * Only the first slide contributes the front matter + style block.
   */
  generateCombinedDeck(outputFileName = 'personas-deck.md') {
    if (!fs.existsSync(this.personasDir)) {
      throw new Error(`Personas directory not found: ${this.personasDir}`);
    }
    const personaFiles = fs.readdirSync(this.personasDir)
      .filter(file => file.endsWith('.json'));
    if (personaFiles.length === 0) {
      console.log('No persona files found in:', this.personasDir);
      return null;
    }

    console.log(`Combining ${personaFiles.length} personas into single deck...`);
    const template = this.loadTemplate();
    let combinedFront = '';
    const bodies = [];

    const ordered = personaFiles
      .map(file => {
        const personaPath = path.join(this.personasDir, file);
        const personaData = this.loadPersonaData(personaPath);
        const workflowStageOrder = Number.isFinite(Number(personaData.workflowStageOrder))
          ? Number(personaData.workflowStageOrder)
          : Number.POSITIVE_INFINITY;
        const workflowStage = typeof personaData.workflowStage === 'string' ? personaData.workflowStage : '';
        const name = typeof personaData.name === 'string' ? personaData.name : '';
        return { file, personaPath, personaData, workflowStageOrder, workflowStage, name };
      })
      .sort((a, b) => {
        if (a.workflowStageOrder !== b.workflowStageOrder) {
          return a.workflowStageOrder - b.workflowStageOrder;
        }
        const stageCmp = a.workflowStage.localeCompare(b.workflowStage);
        if (stageCmp !== 0) return stageCmp;
        const nameCmp = a.name.localeCompare(b.name);
        if (nameCmp !== 0) return nameCmp;
        return a.file.localeCompare(b.file);
      });

    ordered.forEach((entry, idx) => {
      const rendered = this.generateSlide(template, entry.personaData);
      const { front, body } = this.extractFrontMatter(rendered);
      if (idx === 0) {
        combinedFront = front; // keep style + meta once
        bodies.push(body.trimStart());
      } else {
        bodies.push(body.trimStart());
      }
    });

    // Ensure Marp-compatible slide breaks: blank lines around --- separators
    const deckContent = [combinedFront, bodies[0].trimStart()] 
      .concat(bodies.slice(1).map(b => `\n\n---\n\n${b.trimStart()}`))
      .join('\n');

    if (!fs.existsSync(this.outputDir)) {
      fs.mkdirSync(this.outputDir, { recursive: true });
    }
    const outPath = path.join(this.outputDir, outputFileName);
    fs.writeFileSync(outPath, deckContent, 'utf8');
    console.log(`✓ Combined deck written: ${outPath}`);
    return outPath;
  }

  /**
   * List available persona files
   */
  listPersonas() {
    if (!fs.existsSync(this.personasDir)) {
      console.log('Personas directory not found:', this.personasDir);
      return [];
    }

    const personaFiles = fs.readdirSync(this.personasDir)
      .filter(file => file.endsWith('.json'))
      .sort((a, b) => a.localeCompare(b));

    console.log('Available persona files:');
    personaFiles.forEach((file, index) => {
      const personaPath = path.join(this.personasDir, file);
      const data = this.loadPersonaData(personaPath);
      console.log(`  ${index + 1}. ${file}`);
      console.log(`     ${data.name} - ${data.jobTitle}`);
    });
    
    return personaFiles;
  }
}

// CLI Usage
function main() {
  const generator = new PersonaSlideGenerator();
  const args = process.argv.slice(2);

  try {
    if (args.length === 0) {
      // Generate all slides
      generator.generateAllSlides();
    } else if (args[0] === 'combined') {
      // Generate single combined deck
      const outputName = args[1] && !args[1].startsWith('--') ? args[1] : 'personas-deck.md';
      generator.generateCombinedDeck(outputName);
    } else if (args[0] === 'list') {
      // List available personas
      generator.listPersonas();
    } else if (args[0] === 'help' || args[0] === '--help' || args[0] === '-h') {
      // Show help
      console.log(`
Persona Slide Generator

Usage:
  node generate-slides.js                    Generate slides for all persona files
  node generate-slides.js combined [name]    Generate single combined deck (default personas-deck.md)
  node generate-slides.js list               List available persona files
  node generate-slides.js <filename>         Generate slide for specific persona file
  node generate-slides.js help               Show this help message

Examples:
  node generate-slides.js
  node generate-slides.js combined
  node generate-slides.js combined all-personas.md
  node generate-slides.js sarah-midwife.json
  node generate-slides.js list

Files:
  Templates: personas/templates/persona-template.md
  Personas:  personas/data/*.json
  Output:    personas/generated/*-slide.md
      `);
    } else {
      // Generate specific persona slide
      const filename = args[0];
      generator.generatePersonaSlide(filename);
    }
  } catch (error) {
    console.error('Error:', error.message);
    process.exit(1);
  }
}

// Run if called directly
if (require.main === module) {
  main();
}

module.exports = PersonaSlideGenerator;
