## Accessibility Audit

### Goal
Conduct a thorough accessibility audit ensuring WCAG 2.2 Level AA compliance across all pages.

### Acceptance Criteria
- [ ] axe-core accessibility scan passes on every page with zero violations
- [ ] Skip link is the first focusable element on every page
- [ ] All form inputs have associated `<label>` elements
- [ ] Error summary appears at top of page on validation failure
- [ ] Focus management: focus moves to error summary when displayed
- [ ] Keyboard navigation works for all interactive elements (Tab, Enter, Escape)
- [ ] Heading hierarchy is correct (no skipped levels)
- [ ] All images have meaningful `alt` text (or `alt=""` for decorative)
- [ ] Colour contrast meets 4.5:1 ratio (handled by NHS Design System tokens)
- [ ] Report generated at `docs/accessibility-audit.md`
