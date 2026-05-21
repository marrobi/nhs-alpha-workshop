---
applyTo: "**/*.cshtml,**/Views/**,**/*.tsx,**/*.jsx"
---

# UKHSA Frontend Standards

Use the [GOV.UK Design System](https://design-system.service.gov.uk) for all user-facing pages. Follow the [GOV.UK content design guidance](https://www.gov.uk/guidance/content-design) for content standards. See `tech-stack.instructions.md` for the current frontend framework, component library setup, and implementation details.

## Design System

- Always use the GOV.UK Design System component library — never hand-code components that exist in the design system
- Follow the component patterns from the [GOV.UK Design System components](https://design-system.service.gov.uk/components)

## Layout

- Pages must include GOV.UK header and footer
- Set service name in the header
- Use the GOV.UK grid system for layout
- Include a skip link as the first element

## Typography

- Use GOV.UK heading classes: `govuk-heading-xl`, `govuk-heading-l`, `govuk-heading-m`, `govuk-heading-s`
- Use `govuk-body` for paragraph text, `govuk-body-l` for lead paragraphs

## Forms

- One question per page ([GDS question protocol](https://design-system.service.gov.uk/patterns/question-pages/))
- Use GOV.UK Design System form components (input, radios, date input, select)
- Show error summary at the top of the page on validation failure
- Show inline error messages on individual form fields

## Navigation

- GOV.UK header with service name
- Breadcrumbs for page hierarchy (except on the start page)
- Back link on question pages

## API Data Consumption

- When consuming API data, frontend interface field names must match the backend response model exactly
- If unsure of field names, check the backend schema — do not guess field names

## Accessibility

- All pages must meet [WCAG 2.2 Level AA](https://www.gov.uk/service-manual/helping-people-to-use-your-service/making-your-service-accessible-an-introduction) — mandatory for UKHSA services
- Follow the [GOV.UK accessibility guidance](https://www.gov.uk/service-manual/helping-people-to-use-your-service/making-your-service-accessible-an-introduction)
