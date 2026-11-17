# Prompt: personas

The method for personas is in one file: [discovery/personas/persona-generation-prompt.md](../personas/persona-generation-prompt.md). This file only points to it, so that there is one method.

## How to use

1. Open Microsoft 365 Copilot Chat at <https://m365.cloud.microsoft/chat/>.
1. Select the **Researcher** agent under **Agents**. Researcher is the preferred tool, because it can do web research and give citations.
1. Copy all of [discovery/personas/persona-generation-prompt.md](../personas/persona-generation-prompt.md), and paste it into the chat.
1. Paste the content of `discovery/scenarios/scenario.md` where the prompt says "PASTE SCENARIO HERE".
1. Save the output to `discovery/personas/persona-report.md` in the repository. If you do not have access to the repository, give the file to a team member who has access.

GitHub Copilot users who do not have access to Researcher run the `personas` skill instead. The skill uses the same method. It makes an AI draft that the team must check with real users.
