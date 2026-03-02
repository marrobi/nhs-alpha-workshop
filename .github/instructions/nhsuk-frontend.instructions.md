---
applyTo: "**/*.tsx,**/*.jsx,**/frontend/**"
---

# NHS.UK Frontend — React Component Standards

Use [nhsuk-react-components](https://github.com/NHSDigital/nhsuk-react-components) for all NHS Design System components. Refer to the [NHS Design System](https://service-manual.nhs.uk/design-system) for patterns and the [NHS content style guide](https://service-manual.nhs.uk/content) for content standards.

## Setup

- Install: `npm install nhsuk-react-components nhsuk-frontend`
- Import CSS in entry point: `import 'nhsuk-frontend/dist/nhsuk.css'`
- Import components individually: `import { Header, Footer, Button } from 'nhsuk-react-components'`

## Component Usage

- Always use `nhsuk-react-components` — never hand-code components that exist in the design system
- See [component list](https://github.com/NHSDigital/nhsuk-react-components#components) for available components
- Follow the prop patterns from the nhsuk-react-components docs, which mirror the [NHS Design System components](https://service-manual.nhs.uk/design-system/components)

## Layout

- Wrap pages in `<Header>` and `<Footer>` from nhsuk-react-components
- Set service name in the Header component
- Use NHS grid: `<Container>`, `<Row>`, `<Col width="two-thirds">`
- Include a skip link as the first element

## Typography

- Use NHS heading classes: `nhsuk-heading-xl`, `nhsuk-heading-l`, `nhsuk-heading-m`, `nhsuk-heading-s`
- Use `nhsuk-body` for paragraph text, `nhsuk-body-l` for lead paragraphs

## Forms

- One question per page ([GDS question protocol](https://design-system.service.gov.uk/patterns/question-pages/))
- Use `<Input>`, `<Radios>`, `<DateInput>`, `<Select>` components from nhsuk-react-components
- Use `<ErrorSummary>` at the top of the page on validation failure
- Use `error` prop on form components for inline error messages

## Navigation

- NHS `<Header>` with service name
- `<Breadcrumb>` for page hierarchy (except on the start page)
- `<BackLink>` on question pages

## Accessibility

- All pages must meet [WCAG 2.2 Level AA](https://service-manual.nhs.uk/accessibility) — mandatory for NHS services
- Follow the [NHS accessibility guidance](https://service-manual.nhs.uk/accessibility)
