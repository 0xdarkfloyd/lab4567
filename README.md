# lab4567 — student lab exercises and reference solutions

Two standalone, synthetic training packs copied from [securecodingtraining](https://github.com/0xdarkfloyd/securecodingtraining/tree/626b77a2d04543e1543c25d8a708d09a23fcaa2d). This repository contains exercises **and answers**; attempt the student tasks before opening reference keys or fixed source.

| Pack | Start here | Reference solutions |
|---|---|---|
| Development and security operations — Java and C# | [Lab guide](development-operations-lab/README.md), [Java Lab 1](development-operations-lab/docs/lab1/README.md), [Java Lab 2](development-operations-lab/docs/lab2/README.md), [C# guide](development-operations-lab/dotnet/GUIDE.md) | [Java key](development-operations-lab/solutions/README.md), [C# key](development-operations-lab/dotnet/SOLUTIONS.md); separate exercise/fixed code in each language |
| Project management — paper-based R1 case | [Introduction and CVSS primer](project-management-lab/README.md), [sample report](project-management-lab/student/SAMPLE-ASSESSMENT-REPORT.md), [worksheets](project-management-lab/student/WORKSHEETS.md) | [Model answer](project-management-lab/instructor/MODEL-ANSWER.md) |

Choose **one language and one implementation track** for operations. Follow each pack's prerequisites and explicit dependency-download/restore options. Never deploy vulnerable exercise code or test production systems. Preserve shared fixtures and independent expectations.

**Evidence limits:** the recorded Java fixed suite passed 37/37 on JDK 21 targeting Java 17; this transfer does not rerun it. C# targets net8.0/C# 12 and requires an approved SDK 8+ and appropriate runtime; its recorded environment had SDK 2.1.526 only, so compilation/execution remains unverified. PM findings, CVSS/KEV/EPSS inputs and decisions are synthetic, not an actual assessment or organisational policy.

Slides, the full instructor workbook and [PDF exports](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/build/labs/README.md) remain in the original training repository. Original teaching attachments are not redistributed; see [availability guidance](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/appendix/README.md). The two lab folders contain all source exercises and reference solutions; code/runtime files are copied unchanged. Only documentation links are adapted for this repository.