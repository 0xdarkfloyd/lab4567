# Prompt injection lab — attacking and defending LLM applications

**Standalone participant pack · original synthetic case · browser-based hands-on lab.**

This pack supports a **90-minute**, hands-on session on **prompt injection** and the **OWASP Top 10 for LLM Applications
(2025)**. Participants attack three deliberately vulnerable AI chatbots run by a fictional company, record their
evidence, and turn each successful attack into a defensive recommendation. The lab application, its source code,
access details and the answer key are **not part of this pack**. Your instructor provides access on the day.

## Start here

| Audience | Document | Purpose |
|---|---|---|
| Students | [student/LAB-GUIDE.md](student/LAB-GUIDE.md) | How to sign in and use the lab, the fictional company brief, and every challenge and task with its objective. |
| Students | [student/WORKSHEETS.md](student/WORKSHEETS.md) | Blank evidence records for each task, the defender exercise and a submission self-check. |
| Instructors | *Separate instructor handbook (not distributed)* | Answer key, calibration notes, administration and marking guidance. Ask the lab owner. |

Give students both student documents. No installation is required: a modern browser (desktop or mobile) is enough.
Lab URL, site login and any classroom rules are supplied by the instructor and must not be shared outside the class.

## Short introduction: prompt injection and why it matters

An LLM application usually builds **one long text**: the developer's instructions (the *system prompt*), retrieved
data (documents, reviews, tool results), the conversation so far, and the user's message. The model reads all of it as
the same kind of token stream. Nothing in the model's architecture makes the developer's rules more authoritative than
text written by a user or an attacker. Training makes a model *likely* to follow its instructions, but it does not *enforce* them.

**Prompt injection** is when attacker-controlled text changes what the model does.

| Term | Meaning in this lab |
|---|---|
| **Direct injection** | You type the attack into the chat yourself. |
| **Indirect injection** | You plant text in content the bot reads later (here: product reviews). The victim is another user. |
| **Guardrail** | A defense around the model: keyword filters, output filters, AI classifiers ("guards"), AI reviewers ("judges"). |
| **Model safety training** | Behaviour learned by the model itself (refusals). It has no application guardrails, and different models have different blind spots. |
| **Excessive agency** | The model can *act* (look up accounts, issue refunds), so an injection becomes an unauthorised action, not just a leak. |
| **Insecure output handling** | Model output is passed to another component (a browser, a shell, SQL) without encoding, which leads to XSS and similar issues. |

### OWASP Top 10 for LLM Applications (2025) at a glance

| ID | Risk | Practised in this lab |
|---|---|---|
| LLM01 | Prompt Injection (direct and indirect) | Throughout |
| LLM02 | Sensitive Information Disclosure | Challenges 1 and 3 |
| LLM03 | Supply Chain | Discussion only |
| LLM04 | Data and Model Poisoning | Review-poisoning tasks |
| LLM05 | Improper Output Handling | Challenge 1 |
| LLM06 | Excessive Agency | Challenge 1 |
| LLM07 | System Prompt Leakage | Challenges 1–3 |
| LLM08 | Vector and Embedding Weaknesses | Review-poisoning tasks (untrusted retrieved content) |
| LLM09 | Misinformation | Challenge 2 |
| LLM10 | Unbounded Consumption | Discussion only (the lab rate-limits for this reason) |

Official reference: [OWASP Top 10 for LLM Applications](https://genai.owasp.org/llm-top-10/).

## What is synthetic and what is real?

**Everything inside the lab is fictional.** *Kestrel Cloud Ltd*, its staff, customers, orders, hosts, keys, passwords,
discount codes and products do not exist. Email addresses and domains use the reserved `.example` domain. Each
participant receives **different secret values**, so answers cannot be copied between participants.

The AI models are real, so their behaviour is genuine and **non-deterministic**: the same
prompt may succeed once and fail the next time. Treat a single refusal as weak evidence, and a single success as one
observation, not as proof of reliability.

## Delivery and outputs

Use the 90-minute route below. Hints are hidden by default; the instructor releases them when a class is stuck.
Ask participants to create their lab account before the session if possible, which saves about five minutes.

| Elapsed minutes | Activity / pack output |
|---|---|
| 0–10 | Briefing: introduction above, rules of engagement, sign-in, the Kestrel company brief. |
| 10–40 | Exercise 1: Challenge 1, tasks 1.1–1.5. Record evidence in the worksheets. |
| 40–45 | Quick debrief: why rules, filters and "never reveal" instructions fail. |
| 45–65 | Exercise 2: Challenge 1, **at least three** of tasks 1.6–1.10 (filters, semantic leaks, actions, output handling, indirect injection). |
| 65–80 | Exercise 3: Challenge 2 (Model Safety), **at least four** tasks. Compare which framings the model resists. |
| 80–90 | Exercise 4: two defender recommendations, then wrap-up on why architecture beats guardrails. |
| Stretch / after class | Challenge 3 (hardened bot) and any remaining tasks. The lab stays open while the instructor keeps it running. |

**Submit** the completed worksheets: an evidence record for every attempted task, the Model Safety reflection and two
defender recommendations. Quote only short excerpts of model replies, as evidence. The scoreboard in the lab
measures solved tasks; the worksheet shows *understanding*.

## Rules of engagement

- Attack **only the three chatbots** of this lab. Do not attack the lab platform itself: sign-in pages, other
  participants' accounts, the server or the network.
- Do not share the lab URL, site login, your account or your secret values outside the class.
- Use only your own account. Do not automate requests or flood the lab; the AI service is shared and rate-limited.
- The Model Safety objectives are deliberately low-harm. Do not try to obtain genuinely harmful content (weapons,
  malware, self-harm or similar), even if a technique might work.
- Do not paste real personal data, real credentials or company documents into the lab or into any AI tool.
- Using these techniques against systems you are not authorised to test is illegal and unethical. Report real
  vulnerabilities responsibly.

## Evidence and decision rules

- A task counts as **solved** when the lab marks it solved. Answer tasks are checked against your personal secret;
  action tasks (refund, script execution, poisoned review, Model Safety) are checked automatically by fixed rules.
- **Record what you observed, not what you assume.** Distinguish *the bot said X* from *the bot did X* (a tool call)
  and from *the filter blocked X*.
- A "Blocked" message is evidence too: note which layer stopped you (input filter, AI guard, output filter or AI judge).
- When a task needs several attempts, record the attempt that worked **and** at least one that failed, and explain the difference.
- Do not claim that a defense is "secure" because your attempts failed. Absence of a successful attack is not proof of safety.
- Defender recommendations must name the **root cause** (for example "secret in model context", "authorisation
  enforced only in the prompt"), not only "add a better filter".

The illustrative rubric is **attack evidence 30, technique explanation 25, defender recommendations 25, Model Safety
reflection 10, responsible conduct 10**. No pass threshold is supplied; the instructor holds the detailed marking guidance.
