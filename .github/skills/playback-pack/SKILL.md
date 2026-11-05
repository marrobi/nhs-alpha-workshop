---
name: playback-pack
description: 'Use when you need to build the end-of-discovery playback and sign-off pack from the discovery documents. Triggers: playback, sign-off, show and tell, end of discovery, /playback-pack. Writes discovery/playback/playback-pack.md.'
---

# Playback and Sign-off Pack

Follow the method, the rules and the output template in [discovery/prompts/playback-pack.md](../../../discovery/prompts/playback-pack.md). Read that file first.

The steps below add only the repository steps.

## Inputs

Read all these files and folders that exist:

- `discovery/scenarios/scenario.md`
- `discovery/personas/persona-report.md` and `discovery/personas/generated/personas-deck.md`
- `discovery/user_journeys/data/`
- `discovery/decision-log.md`, `discovery/raid-log.md`, `discovery/traceability.md`
- `discovery/interface-register.md`, `discovery/access-tracker.md`
- `discovery/workshops/` and the last report in `discovery/status/`
- `docs/adr/`, `docs/clinical-safety/` and `docs/dpia/`, if they exist

If the persona slides are older than the persona report, tell the user to run the `persona-slides` skill again.

## Questions

Ask for the date and the parties of the playback with the ask questions tool, if it is available. Otherwise, ask in the chat.

## Output

1. Write `discovery/playback/playback-pack.md`. Link each section to the full document with a relative path.
1. Give the gaps that can stop sign-off in the chat.

To make slides from the pack, use the `marp-slides` skill on the output.

## Validation

- Make sure that each ID in the pack is in the document that it comes from.
- Make sure that each link goes to a file that exists.
- Make sure that the file has no real patient data.
