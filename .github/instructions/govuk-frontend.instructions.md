---
applyTo: "**/Views/**,**/*.cshtml,**/wwwroot/**"
---

# GOV.UK Design System (with UKHSA Branding)

Frontend standards for UKHSA citizen- and staff-facing services. UKHSA services use the **GOV.UK Design System** with UKHSA-branded header and footer.

For .NET / Razor specifics (tag helper registration, partial structure), see `tech-stack.instructions.md`.

Authoritative references:

- [GOV.UK Design System](https://design-system.service.gov.uk/)
- [GOV.UK Service Manual](https://www.gov.uk/service-manual)
- [GOV.UK content style guide](https://www.gov.uk/guidance/style-guide/a-to-z-of-gov-uk-style)
- [GovUk.Frontend.AspNetCore](https://github.com/gunndabad/govuk-frontend-aspnetcore)
- WCAG 2.2 Level AA (legal minimum under the Public Sector Bodies Accessibility Regulations 2018, as amended)

---

## Setup & Branding

- Install `GovUk.Frontend.AspNetCore` and register via `builder.Services.AddGovUkFrontend()` / `app.UseGovUkFrontend()`
- Tag helpers MUST be registered globally in `_ViewImports.cshtml`:
  ```cshtml
  @addTagHelper *, GovUk.Frontend.AspNetCore
  ```
- UKHSA branding is applied through:
  - A `_UkhsaHeader.cshtml` partial that renders a `<govuk-header>` with UKHSA logo and service name
  - A `_UkhsaFooter.cshtml` partial that renders a `<govuk-footer>` with UKHSA links (Privacy, Accessibility, Cookies, Contact)
  - A small CSS overlay for UKHSA colours where the design system permits brand variation (keep changes minimal — do not redesign GOV.UK components)
- Service name MUST be set consistently in the header partial and the `<title>` element

## Component Usage

- ALWAYS use GOV.UK Design System components via tag helpers — never hand-code a button, input, or layout that already exists in the design system
- See the [components index](https://design-system.service.gov.uk/components/) for the canonical list
- Follow the design system's content and accessibility guidance for each component — not just the markup

## Page Structure

- Every page MUST:
  - Have a unique, descriptive `<title>` in the pattern `Page name – Service name – GOV.UK`
  - Begin with a skip link to `#main-content` as the first focusable element
  - Wrap main content in `<main id="main-content" role="main">`
  - Use a `<govuk-back-link>` where the user has a meaningful previous step
- Use the GOV.UK grid: `govuk-grid-row`, `govuk-grid-column-two-thirds` for prose pages
- Headings MUST follow the GOV.UK type scale (`govuk-heading-xl`, `govuk-heading-l`, etc.) and form a single coherent outline

## Forms

- Use `<govuk-input>`, `<govuk-radios>`, `<govuk-checkboxes>`, `<govuk-date-input>`, `<govuk-select>`, `<govuk-textarea>`, `<govuk-fieldset>`, `<govuk-error-summary>`
- Bind to view models with `asp-for` — let the tag helpers manage `id`, `name`, `aria-describedby`, and error states
- One question per page is the default unless the questions are tightly related (e.g. date components)
- Use `<govuk-error-summary>` at the top of the page when validation fails, with anchor links to each erroneous field
- Use the `error-message` slot on form components for inline errors; error text MUST follow GOV.UK error message guidance ("Enter your date of birth", not "Invalid input")
- All form controls MUST have a visible label — placeholder text is not a label

## Content & Tone

- Follow the [GOV.UK content style guide](https://www.gov.uk/guidance/style-guide/a-to-z-of-gov-uk-style):
  - Plain English (aim for reading age 9)
  - Active voice
  - Short sentences (max ~25 words)
  - Sentence case for headings and buttons
  - Use "you", "we", "us" — speak directly to the user
- Buttons MUST describe the action (`Continue`, `Save and continue`, `Send your registration`) — not "Submit" or "OK"
- Error messages MUST tell the user what to do to fix the problem

## Accessibility (WCAG 2.2 AA)

- Components from the design system are accessible by default — do not modify their semantics or behaviour
- All interactive elements MUST be reachable and operable by keyboard alone
- Focus indicators MUST be visible — do not suppress the default GOV.UK focus styling
- Colour MUST NOT be the sole means of conveying meaning
- Provide a text alternative for all non-decorative images (`alt` attribute)
- Live regions (`aria-live`) MUST be used for dynamically inserted error messages and progress updates
- Run `axe-core` (via `@axe-core/playwright` in E2E tests) against every page; the build MUST fail on any new violation at Level A or AA
- A published accessibility statement MUST exist for the service and MUST be linked from the footer

## JavaScript

- Progressive enhancement is the default — pages MUST work without JavaScript
- Where JavaScript is required (e.g. session timeout warning, file upload progress), the experience without it MUST still be usable
- Do not introduce SPA frameworks (React, Vue, Angular) into a Razor service without an explicit architectural decision — the GOV.UK Design System assumes server-rendered HTML

## Cookies & Tracking

- A cookie banner MUST be present where any non-essential cookies are set
- Analytics MUST default to off until the user consents
- A cookies page MUST list every cookie the service sets, with purpose, duration, and provider

## Testing the Frontend

- View tests use `WebApplicationFactory` to render Razor pages and assert content
- Visual / behavioural tests use Playwright for .NET against a running test instance
- Accessibility checks: every page tested in E2E MUST include an `axe` scan
- Content assertions MUST check the rendered values, not just element visibility — see `testing.instructions.md`
