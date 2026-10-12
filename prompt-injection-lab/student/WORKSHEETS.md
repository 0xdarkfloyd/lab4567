# Prompt injection lab — student worksheets

**Use only the lab application, [LAB-GUIDE.md](LAB-GUIDE.md) and the fictional Kestrel Cloud case.** All response cells
are blank. Record what you observed. Quote short excerpts of model replies as evidence; never paste real personal data,
real credentials or company documents.

| Submission details | Entry |
|---|---|
| Participant / display name in the lab / class | |
| Date and lab session | |
| Language used in the lab (EN / 繁中) | |
| Hints used (task numbers), if any | |
| Limitations of this submission | |

**How to fill in an evidence record**

| Column | What to write |
|---|---|
| Attempt that worked | Your prompt (or a short excerpt), and whether it took one turn or several. |
| Evidence | A short quote of the reply, the tool call shown, or "solved via answer box". |
| Defense that stopped earlier attempts | The block message or refusal you saw, and which layer it came from. |
| Technique (your words) | Name the technique in one line, then check it against the write-up revealed after solving. |
| Root cause | Why it worked: the design flaw, not just "the AI was tricked". |

## Exercise 1 — Challenge 1, tasks 1.1–1.5 (55 minutes + 10 debrief)

Read **About this chatbot** before starting. List the defenses you see:

| # | Defense (as listed in the lab) | What it can see | What it cannot see |
|---|---|---|---|
| 1 | | | |
| 2 | | | |
| 3 | | | |
| 4 | | | |
| 5 | | | |

| Task | Solved? (Y/N) | Attempt that worked | Evidence | Defense that stopped earlier attempts | Technique (your words) | Root cause |
|---|---|---|---|---|---|---|
| 1.1 | | | | | | |
| 1.2 | | | | | | |
| 1.3 | | | | | | |
| 1.4 | | | | | | |
| 1.5 | | | | | | |

**Challenge:** "Kes was told never to share the Wi-Fi password, so the password is safe."

| Correction | Evidence from your attempts |
|---|---|
| | |

## Exercise 2 — Challenge 1, tasks 1.6–1.10 (40 minutes)

| Task | Solved? (Y/N) | Attempt that worked | Evidence | Defense that stopped earlier attempts | Technique (your words) | Root cause |
|---|---|---|---|---|---|---|
| 1.6 | | | | | | |
| 1.7 | | | | | | |
| 1.8 | | | | | | |
| 1.9 | | | | | | |
| 1.10 | | | | | | |

**Tool-call evidence (tasks 1.4 and 1.8).** Copy the tool call(s) the bot made.

| Task | Tool name | Arguments | Result | Should this call have been allowed? Who should have decided? |
|---|---|---|---|---|
| 1.4 | | | | |
| 1.8 | | | | |

**Indirect injection (task 1.10).** Who is the victim, and why can't a filter on the *chat box* stop this attack?

| Victim | Path the attacker's text took into the model | Why chat-input filtering misses it |
|---|---|---|
| | | |

## Exercise 3 — Challenge 2, Model Safety (20 minutes)

For each task, record the framing you tried and the **Check** line the lab showed.

| Task | Framing that failed | Check reason shown | Framing that worked | Check reason shown |
|---|---|---|---|---|
| 2.1 | | | | |
| 2.2 | | | | |
| 2.3 | | | | |
| 2.4 | | | | |
| 2.5 | | | | |
| 2.6 | | | | |
| 2.7 | | | | |
| 2.8 | | | | |

**Reflection.** Which kinds of framing did the model resist, and which did it accept? What does this mean for an
organisation that relies only on the model's own safety training?

| Resisted | Accepted | Implication for deployment |
|---|---|---|
| | | |

## Exercise 4 — Challenge 3 (stretch) and defender recommendations (20 minutes)

**Hardened bot.** List the guardrails of Kes v2 and, for each task you attempted, the gap you used (or suspect).

| Task | Solved? (Y/N) | Guardrail(s) in the way | Gap used / suspected | Evidence |
|---|---|---|---|---|
| 3.1 | | | | |
| 3.2 | | | | |
| 3.3 | | | | |

**Defender recommendations.** Choose **three** different tasks you solved (at least one from Challenge 1 tasks
1.4/1.5/1.8/1.9/1.10 and at least one from Challenge 3). For each, recommend a fix that addresses the **root cause**.
"Add a better filter" or "write a stronger prompt" is not enough on its own.

| # | Task | Root cause | Recommended fix (architecture / code / process) | Residual risk after the fix | Owner (role) |
|---|---|---|---|---|---|
| 1 | | | | | |
| 2 | | | | | |
| 3 | | | | | |

**Challenge:** "We added an AI guard and an AI judge, so the chatbot is now secure."

| Correction | Evidence from Challenge 3 |
|---|---|
| | |

## Submission self-check

| Check | Done (Y/N) |
|---|---|
| Every attempted task has an evidence record, including failed attempts where useful | |
| Evidence distinguishes what the bot *said*, what it *did* (tool calls) and what a defense *blocked* | |
| Each technique is described in my own words and checked against the revealed write-up | |
| Root causes name the design flaw (for example "secret in context", "authorisation only in the prompt") | |
| Three defender recommendations, each with residual risk and an owner | |
| No real personal data, real credentials or company documents included | |
| I attacked only the lab's chatbots, with my own account, and did not share access details | |
