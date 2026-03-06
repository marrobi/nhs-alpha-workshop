---
applyTo: ""
---

# Review Agent Pattern

Shared workflow, severity levels, and report template for all review/audit agents. Individual agents define their **checklist** and **report path** — this file defines **how** to run the review.

---

## Review Workflow

1. **Read the full codebase first** — understand structure, patterns, tech stack, and existing code before writing any findings.
2. **Initial review** — check every item in the agent's checklist against actual code. Run automated checks (linter, tests, `terraform validate`, etc.) where applicable.
3. **Fix issues** — fix all critical and high issues directly. Fix medium issues where the fix is clear and safe.
4. **Re-review** — re-run checks after fixes. Verify correctness, check for regressions.
5. **Repeat** — continue fix → re-review until no critical/high issues remain. Minimum **two full passes** (initial + one re-review).
6. **Save report** — only after the final pass. Report must reflect the **final** state, not initial findings. Include a "Resolved Issues" section.

## Severity Levels

| Level | Meaning | Action |
|---|---|---|
| **Critical** | Security violations, data integrity, patient safety | Fix immediately |
| **High** | Missing validation, broken auth, failing tests | Fix before deployment |
| **Medium** | Naming, duplication, missing edge-case tests | Fix in review |
| **Low** | Style, readability, minor improvements | Document only |

## Report Template

Each agent specifies its own report path and category table. Use this structure:

```markdown
# [Review Type] Report

**Service**: [name]
**Date**: [date]
**Tech Stack**: [from tech-stack.instructions.md]

## Summary

| Category | Issues Found | Fixed | Remaining |
|---|---|---|---|
| [Category 1] | [n] | [n] | [n] |

## Review Passes

| Pass | Date | Critical | High | Medium | Low |
|---|---|---|---|---|---|
| 1 (initial) | [date] | [n] | [n] | [n] | [n] |
| 2 (after fixes) | [date] | [n] | [n] | [n] | [n] |

## Resolved Issues

### [Finding title]
- **Severity**: [level]
- **Category**: [checklist section]
- **File**: [path]
- **Fix applied**: [what changed]

## Remaining Issues

### [Finding title]
- **Severity**: [level]
- **Category**: [checklist section]
- **File**: [path]
- **Reason not fixed**: [justification]
```

## Verification Rules

- **Fix issues, don't just report them** — review agents fix, not just audit
- **Never mark a control as implemented** without searching the codebase for evidence — ADRs and instruction files describe intent, not reality
- **Distinguish "designed" from "implemented"** — unimplemented controls are gaps, not assurances
- Save the report only after the final review pass
