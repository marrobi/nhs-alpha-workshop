---
name: playwright-dotnet-e2e
description: 'Playwright for .NET E2E patterns for UKHSA user journeys — Page Object Model, accessibility via Deque.AxeCore.Playwright, GOV.UK Design System assertions.'
---

# Playwright .NET E2E Testing — UKHSA Skill

Patterns and rules for Playwright browser tests that verify UKHSA user journeys. All tests are written in C# using `Microsoft.Playwright` and `Deque.AxeCore.Playwright`, running against an ASP.NET Core dev server.

## Dependencies

Installed during scaffold:

```bash
dotnet add tests/e2e package Microsoft.Playwright
dotnet add tests/e2e package Microsoft.Playwright.NUnit   # or .Xunit
dotnet add tests/e2e package Deque.AxeCore.Playwright
```

After build, install browsers: `pwsh tests/e2e/bin/Debug/net10.0/playwright.ps1 install --with-deps chromium`.

## Directory Structure

```
tests/e2e/
├── Pages/             # Page Objects (one per route)
├── Journeys/          # One test file per user journey
├── Screenshots/       # Visual QA evidence (gitignored)
├── Accessibility/     # Full-site axe audit
├── PlaywrightSettings.cs
└── playwright.config.cs (or runsettings)
```

Set base URL to the local frontend dev server (e.g. `http://localhost:5000`).

## Page Object Model

One class per page/route in `tests/e2e/Pages/`. Tests call Page Object methods — never raw selectors. Each Page Object exposes navigation and interaction methods.

```csharp
public class StartPage(IPage page)
{
    public Task GotoAsync() => page.GotoAsync("/");
    public Task ClickStartNowAsync() => page.GetByRole(AriaRole.Button, new() { Name = "Start now" }).ClickAsync();
    public ILocator Heading => page.GetByRole(AriaRole.Heading, new() { Level = 1 });
}
```

## Accessibility

Run axe-core on **every page** the test visits — zero violations allowed. Also verify keyboard Tab order, screen reader landmarks (`<main>`, `<nav>`, `<header>`, `<footer>`), form labels, and GOV.UK error summary on validation failure.

```csharp
var axe = await new AxeBuilder(page).AnalyzeAsync();
axe.Violations.Should().BeEmpty();
```

## Selectors

Role-based only: `GetByRole`, `GetByLabel`, `GetByText`. Never CSS or XPath.

## GOV.UK Page Patterns

| Pattern | Key assertions |
|---|---|
| **Start page** | `<govuk-header>` present, service name in `<h1>`, "Start now" button |
| **Question pages** | One question per page, `<govuk-back-link>`, "Continue" button |
| **Check answers** | `<govuk-summary-list>` with "Change" links |
| **Confirmation** | `<govuk-panel>` with reference number |
| **Error pages** | `<govuk-error-summary>` at top, inline field errors, focus moves to summary |
| **Data display** | Assert expected data values appear in page text — do not rely on element visibility alone |
| **Success pages** | Assert `<govuk-error-summary>` is NOT present |
| **Summary list / data table** | Assert every value cell is non-empty |

## Consuming User Journeys

One test file per journey. Derive tests from:

1. **Journey Main Flow table** (`discovery/user_journeys/data/`) — digital touchpoint rows → test steps
2. **Decision Points** → separate test methods or `[Theory]` cases
3. **ADR** (`docs/adr/`) — concrete routes, endpoints, data models
4. **User stories** (`user_stories/story-*.md`) — acceptance criteria → assertions

The journey gives the **sequence**; ADR and stories give **implementation detail**.

## Screenshots

Save screenshots on failure and at key journey milestones. Store in `tests/e2e/Screenshots/`. Capture at minimum: start page, each form submission result, confirmation page, and error states.

```csharp
await page.ScreenshotAsync(new() { Path = $"Screenshots/{TestContext.CurrentContext.Test.Name}.png" });
```

## Rules

- One test file per journey, one Page Object per page
- axe check on every page navigation — no exceptions
- Role-based locators only (`GetByRole`, `GetByLabel`, `GetByText`)
- Must work headless in CI (Chromium)
- Synthetic UKHSA data only (default NHS number `943 476 5919` — see `ukhsa-synthetic-data` skill)
- Never mix E2E and unit tests in the same project
- Authentication uses test stubs for GOV.UK One Login / Entra ID — never real credentials

## References

- [Playwright for .NET](https://playwright.dev/dotnet/)
- [Deque.AxeCore.Playwright](https://github.com/dequelabs/axe-core-nuget)
- [GOV.UK Design System](https://design-system.service.gov.uk/)
- [UKHSA Engineering Standards](https://ukhsa-collaboration.github.io/standards-org/)
