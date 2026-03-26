---
agent: agent
---

Take the scenario description I provide and formalize it into a clear, structured format. If no scenario is provided, request one.

**Before writing anything, interrogate the description.** If I have described a solution (e.g. "build a portal", "create an app"), reframe it as the underlying problem to be solved — following [GDS guidance: Define the problem](https://www.gov.uk/service-manual/agile-delivery/how-the-discovery-phase-works#define-the-problem). Ask clarifying questions that challenge assumptions and probe for the real user need behind any proposed solution. For example, "We need to build an interactive map" should become "How can we make it easier for people to find their nearest contact centre?"

Create:

## Scenario Overview
Write 2-3 paragraphs describing:
- Who is affected (patients, clinicians, admin staff, carers, etc.) and what they are trying to achieve
- The current state — how things work today and what pain points exist
- The context — what wider journey or process this sits within
- Key workflow stages involved

## Problem Statement
Write the problem as a user need, not a solution. Frame it as "How might we…" or "People need a way to…" rather than "Build a system that…".

Include:
- The core problem being solved, expressed from the user's perspective
- Why it matters (impact on patients, staff, safety, efficiency)
- The current cost of the problem — time wasted, risks, harm, or inefficiency caused by the status quo
- What success looks like, expressed as measurable outcomes where possible

## Assumptions
List the key assumptions that must be true for the problem statement to hold. Flag which are riskiest and should be tested first in alpha.

## Out of Scope
Explicitly state what is NOT part of this problem. Agree boundaries early to keep the alpha focused.

Use proper NHS terminology and context. Where details are missing, state assumptions clearly rather than silently filling gaps.

Save the output in markdown format with appropriate headings at `discovery/scenarios/scenario.md`
