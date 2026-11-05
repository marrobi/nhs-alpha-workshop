# Engineering Review — Operations

Read this with the [engineering-review skill](../SKILL.md). It explains how to run the engineers, how to monitor them, and how to share one machine.

## How to run the engineers

- **Separate chats where possible.** If you can create chats (`create_session`,
  for example from the VS Code Agents panel), run each engineer as its own
  chat. The user can then watch and steer it, and it keeps running if your own
  turn ends. Set:
  - the model to the model that the user chose for that engineer;
  - the workspace to the review checkout from the skill's "Review isolation"
    rules, without a new worktree;
  - the title `<task> · <model> · <mode>`.

  The prompt must start: "You are an NHS Engineer. Read and follow
  `.github/agents/nhs-engineer.agent.md`."
- **Otherwise, subagents.** Run each engineer as a subagent (`runSubagent` in
  VS Code, or the task tool) with the agent set to `NHS Engineer`, and pass the user's
  chosen model.
- **Prompts.** Make every prompt self-contained. Include:
  - the mode (Investigate or Review);
  - the question, or the exact commit SHAs or stable diff to review;
  - the relevant files, acceptance criteria, ADRs and repository rules;
  - the validation commands the repository defines;
  - the progress-file path.
- **Monitor.** Check each engineer at least every 5 minutes, using its chat
  status and progress file.
  - If its progress file hasn't changed for 15 minutes and it is not running a
    build or test, steer it once.
  - If it still hasn't changed 10 minutes later, start a new chat or subagent
    with the same prompt plus "Resume: read your progress file and
    `git status`, and continue".

## Progress log

Your caller cannot see your work until you finish, so keep a progress log.
- **Where.** Use the path given in your prompt. If none is given, use
  `$HOME/.copilot/agent-progress/<task>.log`.
- **When.** Append a timestamped line at every phase change and at least every
  5 minutes:
  `printf '%s %s\n' "$(date -Is)" "<message>" >> "$PROGRESS_FILE"`.
  - Each line says what finished, with its result, and what runs next.
  - Never write secrets or personal data.
- **Pass it on.** Give each engineer the same path with a suffix, such as
  `-engineer-1.log`.
- **Long work.** Never block on one command or engineer for more than
  5 minutes. Run long work asynchronously, poll it, and log after each poll.
- **Don't stop early.** Do not end your turn while anything you started is
  still running.
- **Resuming.** If the progress file already exists, you are resuming. Read it
  and `git status`, then continue from the last completed step.
- **Last line.** Finish with `DONE` or `BLOCKED: <reason>`.

## Shared machine

When several agents run on one machine, they share its CPU, memory and
container runtime.
- Run at most two engineers that build or test at the same time. Engineers
  that only read code do not count towards that limit.
- Let only one agent at a time run container-based test suites. Use a shared
  lock file, for example
  `flock -o /tmp/<repo>-integration-tests.lock <command>`.
- Clean up the processes and containers your run started.
- Never remove containers, images or volumes this project did not create.
