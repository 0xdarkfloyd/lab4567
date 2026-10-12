# lab4567 — student lab exercises and reference solutions

Three standalone, synthetic training packs. The operations and project-management packs are copied from [securecodingtraining](https://github.com/0xdarkfloyd/securecodingtraining/tree/626b77a2d04543e1543c25d8a708d09a23fcaa2d); the prompt-injection pack is a participant companion to a separately hosted, instructor-provided lab. This repository contains exercises **and answers** for the first two packs; attempt the student tasks before opening reference keys or fixed source.

## Beginner route — start here

- Operations: [simple exercises and example Copilot prompts](development-operations-lab/docs/BEGINNER-EXERCISES.md), then [expanded solutions](development-operations-lab/solutions/BEGINNER-SOLUTIONS.md).
- Project management: [beginner questions and example Copilot prompts](project-management-lab/student/BEGINNER-EXERCISES.md), then [expanded solutions](project-management-lab/instructor/BEGINNER-SOLUTIONS.md).

Each route covers security weaknesses, untrusted data/boundaries and reliability/concurrency problems. New paper examples are clearly separate from existing findings/code. These routes simplify equivalent initial tasks rather than adding mandatory teaching time. Beginner documents are synchronized from the training repository; its PDF index below includes printable copies.

## Advanced routes — existing full lab tasks

- **Operations A1–A4:** [route map](development-operations-lab/README.md#choose-a-route-beginner-or-advanced), [advanced Lab 1](development-operations-lab/docs/lab1/README.md), [advanced Lab 2](development-operations-lab/docs/lab2/README.md), or [C# advanced guide](development-operations-lab/dotnet/GUIDE.md). Full source review/repair, 37 independent cases, alternate-path sweep and deterministic race evidence. Durable idempotency is an optional design-only extension. [Java key](development-operations-lab/solutions/README.md) / [C# key](development-operations-lab/dotnet/SOLUTIONS.md).
- **PM A1–A4:** [route map](project-management-lab/README.md#choose-a-route-beginner-or-advanced), [advanced worksheets](project-management-lab/student/WORKSHEETS.md) and [advanced model answer](project-management-lab/instructor/MODEL-ANSWER.md). All six findings, evidence/triage/remediation records, supporting workshops and board recommendation. Still paper-based, not code execution.

Choose beginner or advanced within the existing allocation. Shared scenarios/prerequisites/evidence limits are unchanged. Readiness guidance and exact required outputs are in each route map; answers are for debrief. This route classification concerns operations and PM only; the separate prompt-injection handbook is unchanged.

| Pack | Start here | Reference solutions |
|---|---|---|
| Development and security operations — Java and C# | [Lab guide](development-operations-lab/README.md), [Java Lab 1](development-operations-lab/docs/lab1/README.md), [Java Lab 2](development-operations-lab/docs/lab2/README.md), [C# guide](development-operations-lab/dotnet/GUIDE.md) | [Java key](development-operations-lab/solutions/README.md), [C# key](development-operations-lab/dotnet/SOLUTIONS.md); separate exercise/fixed code in each language |
| Project management — paper-based R1 case | [Introduction and CVSS primer](project-management-lab/README.md), [sample report](project-management-lab/student/SAMPLE-ASSESSMENT-REPORT.md), [worksheets](project-management-lab/student/WORKSHEETS.md) | [Model answer](project-management-lab/instructor/MODEL-ANSWER.md) |
| Prompt injection — LLM application attacks, browser-based (90 min) | [Student handbook](prompt-injection-lab/README.md) ([繁中](prompt-injection-lab/README.zh-HK.md)): primer, lab guide, tasks, worksheets | Held by the instructor (not distributed); lab access is provided on the day |

Choose **one language and one implementation track** for operations. Follow each pack's prerequisites and explicit dependency-download/restore options. Never deploy vulnerable exercise code or test production systems. The prompt-injection pack contains no application code, access details or answer key; attack only the instructor-provided lab. Preserve shared fixtures and independent expectations.

**Evidence limits:** the recorded Java fixed suite passed 37/37 on JDK 21 targeting Java 17; this transfer does not rerun it. C# targets net8.0/C# 12 and requires an approved SDK 8+ and appropriate runtime; its recorded environment had SDK 2.1.526 only, so compilation/execution remains unverified. PM findings, CVSS/KEV/EPSS inputs and decisions are synthetic, not an actual assessment or organisational policy.

Slides, the full instructor workbook and [PDF exports](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/build/labs/README.md) remain in the original training repository. Original teaching attachments are not redistributed; see [availability guidance](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/appendix/README.md). The two lab folders contain all source exercises and reference solutions; code/runtime files are copied unchanged. Only documentation links are adapted for this repository.