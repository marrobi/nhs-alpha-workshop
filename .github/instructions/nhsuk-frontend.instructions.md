---
applyTo: "**/*.tsx,**/*.jsx,**/frontend/**"
---

# NHS.UK Frontend Standards

Use the [NHS Design System](https://service-manual.nhs.uk/design-system) for all user-facing pages. Follow the [NHS content style guide](https://service-manual.nhs.uk/content) for content standards. See `tech-stack.instructions.md` for the current frontend framework, component library setup, and implementation details.

## Design System

- Always use the NHS Design System component library — never hand-code components that exist in the design system
- Follow the component patterns from the [NHS Design System components](https://service-manual.nhs.uk/design-system/components)

## Layout

- Pages must include NHS header and footer
- Set service name in the header
- Use the NHS grid system for layout
- Include a skip link as the first element

## Typography

- Use NHS heading classes: `nhsuk-heading-xl`, `nhsuk-heading-l`, `nhsuk-heading-m`, `nhsuk-heading-s`
- Use `nhsuk-body` for paragraph text, `nhsuk-body-l` for lead paragraphs

## Forms

- One question per page ([GDS question protocol](https://design-system.service.gov.uk/patterns/question-pages/))
- Use NHS Design System form components (input, radios, date input, select)
- Show error summary at the top of the page on validation failure
- Show inline error messages on individual form fields

## Navigation

- NHS header with service name
- Breadcrumbs for page hierarchy (except on the start page)
- Back link on question pages

## API Data Consumption

- When consuming API data, frontend interface field names must match the backend response model exactly
- If unsure of field names, check the backend schema — do not guess field names

## Accessibility

- All pages must meet [WCAG 2.2 Level AA](https://service-manual.nhs.uk/accessibility) — mandatory for NHS services
- Follow the [NHS accessibility guidance](https://service-manual.nhs.uk/accessibility)
