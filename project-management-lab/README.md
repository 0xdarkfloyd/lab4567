# Project management lab — from R1 findings to accountable decisions

**Standalone participant pack and instructor key · original synthetic case · paper-based assessment.**

This pack supplies the sample report needed for the project-management exercises in [04-project-management-lab.md](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/04-project-management-lab.md). It follows the actual exercise keys in section 4 of [06-instructor-workbook-and-solutions.md](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/06-instructor-workbook-and-solutions.md), including the expanded workshops. Those teaching documents remain in the original training repository.

**Print copies:** [complete PDF index in the training repository](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/build/labs/README.md) includes this introduction, the student report, blank worksheets and the **separate instructor-only model answer**. Do not distribute the answer key with student copies.

## Start here

**New to security or project-management triage?** Start with [beginner questions and Copilot prompts](student/BEGINNER-EXERCISES.md): explain three findings, identify boundaries, and design simple controls for overlapping edits, duplicate work and stale decisions. The separate [beginner solutions](instructor/BEGINNER-SOLUTIONS.md) provide plain-language answers. These are an easier entry route, not extra mandatory class time; reliability examples are labelled hypotheticals, not new R1 findings.

| Audience | Document | Purpose |
|---|---|---|
| Students | [student/SAMPLE-ASSESSMENT-REPORT.md](student/SAMPLE-ASSESSMENT-REPORT.md) | Formal R1 report, six finding records and short synthetic source pack; no model priorities or board answer. |
| Students | [student/WORKSHEETS.md](student/WORKSHEETS.md) | Four exercises, automatic-ranking challenge and blank workshop records. |
| Instructors only | [instructor/MODEL-ANSWER.md](instructor/MODEL-ANSWER.md) | Complete answers, alternatives, expected verification evidence and marking guidance. Reveal after submissions/debrief. |

Give students the two student documents. Keep the instructor key separate from participant copies and approved AI context until the exercise is complete. No application, account, installation or live testing is required. Designed tests receive design credit, not execution credit.

## Short introduction: CVSS and its meaning in this lab

**Common Vulnerability Scoring System (CVSS)** is FIRST's open standard for describing a vulnerability's technical characteristics and **severity**, on a **0–10** scale. A **vector** records the metric choices behind a score. CVSS is not a probability of compromise, proof that an asset is affected, a complete business-risk assessment or an automatic remediation deadline.

Use the official **[CVSS 3.1 online calculator](https://www.first.org/cvss/calculator/3.1)** for the version named in R1. The **[CVSS 4.0 online calculator](https://www.first.org/cvss/calculator/4.0)** supports the newer standard. Choose metrics from documented evidence and assumptions, not by adjusting inputs until a desired number appears. In a real assessment, retain the version, score type/metric groups, vector, rationale, source and assessment date.

### Versions at a glance

| Version | Main distinction | How to use it here |
|---|---|---|
| 1.0 / 2.0 — legacy | Earlier scoring models; v2 uses Base, Temporal and Environmental groups, with an Authentication metric. | Recognise legacy reports; do not relabel their scores as v3/v4. [FIRST archives](https://www.first.org/cvss/) provide historical specifications. |
| 3.0 | Introduced Privileges Required, User Interaction and Scope; uses Base, Temporal and Environmental groups. | Understand older v3 reports; identify the precise version. |
| **3.1 — R1's version** | Clarifies v3.0 definitions/scoring guidance and severity versus risk; refines rounding and Environmental calculation without adding metrics. | Keep supplied R1 scores as **3.1**. See the [official user guide](https://www.first.org/cvss/v3.1/user-guide). |
| 4.0 | Uses **Base, Threat, Environmental and Supplemental** groups. Adds Attack Requirements and more detailed User Interaction; separates vulnerable-system and subsequent-system impacts instead of the v3 Scope metric. Supplemental metrics do **not** change the numeric score. | Compare methodology only; a v3.1 number cannot simply be converted or relabelled. See the [official specification](https://www.first.org/cvss/v4.0/specification-document). |

**Metric-group definitions:** Base captures intrinsic exploit conditions and confidentiality/integrity/availability impact. In v3.1, Temporal captures changing exploit-code maturity, remediation availability and report confidence; Environmental adjusts for local security requirements and conditions. In v4.0, Threat captures exploit maturity, Environmental adjusts for the deployment, and Supplemental communicates additional context without changing the score. Labels such as **CVSS-B**, **CVSS-BT**, **CVSS-BE** and **CVSS-BTE** identify the explicitly assessed v4.0 groups; unselected optional metrics use specification defaults, not evidence that a threat is absent. In v3.x, **Scope** concerns security authorities, not merely crossing supplier IDs or the report's assessment scope.

### Severity, evidence and priority in R1

The **v3.x/v4.0 qualitative bands** are None **0.0**, Low **0.1–3.9**, Medium **4.0–6.9**, High **7.0–8.9**, Critical **9.0–10.0**. Do not apply this table indiscriminately to legacy v2 ratings.

- **F1 8.1 = High; F4 9.8 = Critical; F5 5.3 = Medium**, interpreting the supplied **synthetic CVSS 3.1 numbers**. R1 supplies no vectors or score-group detail, so their derivation cannot be independently reproduced. Do not invent vectors or assume these are verified Base scores.
- **F2/F3/F6: score not supplied**, not zero or Low. F6 is an assurance gap, not a reproduced disclosure. The calculator does not fill evidence gaps or determine production applicability.
- **Severity** describes technical seriousness; **evidence confidence** describes support for a claim; **risk** also considers the affected business/environment and threat; **priority** is an accountable treatment decision considering exposure, applicability, impact, urgency and dependencies. A scanner's Critical label alone does not settle the queue.
- F1's **KEV Yes / EPSS 0.72** are fictional, separate threat inputs, not CVSS metrics or evidence of local compromise. In real use, KEV records known exploitation of listed vulnerabilities; EPSS estimates exploitation probability over the next 30 days, not the probability that this particular asset is compromised. Do not query real feeds for these advisory placeholders.
- **P1/P2/P3** are illustrative lab treatment categories, not CVSS severity bands, MTR policy or mandatory SLAs. Preserve R1's values and unknowns; justify decisions instead of sorting scores alone. This primer fits the existing briefing, not an added scoring exercise.

## What is original and what comes from the PDFs?

**The PDFs are teaching references, not an actual assessment report.** R1 is original fictional material based on their control themes and the supplied workshop case. No PDF is said to contain the R1 findings, scores, supplier identities, gateway advisory or test results. No assessment, exploitation, credential use, remediation or retest was performed to create this pack.

The report date is **1 October 2026**. Its scope is staging web application, API and gateway using **SUP-A / SUP-B**; production, mobile and subcontractors are excluded. The fictional build **stg-2026.10.01-r1** is an **additional fixture identifier created for this pack**, not a build mentioned in the PDFs or original workshop. Illustrative routes, component labels, evidence identifiers and document markers are additional fixtures too.

### Local teaching-source map

Page numbers below are **1-based PDF page positions**, not printed slide numbers. These are paraphrased themes, not bulk copied content.

| Local source | Pages | Theme used in R1 and the exercises |
|---|---|---|
| AppSec Reference (original attachment) | 2 | Source disclaimer; training material must not be presented as assessment assurance. |
| AppSec Reference (original attachment) | 3 | OWASP/API, security headers and secure implementation patterns; relevant to resource access, gateway and transport review. |
| SDLC (original attachment) | 2 | Source disclaimer and interpretation limits. |
| SDLC (original attachment) | 3 | Security touchpoints: threat modelling, static/dynamic checks, middleware and dependencies, vulnerability management and BDD-style acceptance evidence. |
| Development Security Concerns for .NET Core & Spring Boot APIs (original attachment) | 2 | Agenda themes: authorisation, rate limiting, validation, CORS, sensitive data/errors, injection and tests. An agenda is not proof these controls were tested. |
| Development Security Concerns for .NET Core & Spring Boot APIs (original attachment) | 4 | OAuth, JWT and roles; authentication/token trust must be distinguished from exact-resource permission. |

Original PDFs are **not included in this repository**; they were local, intentionally Git-ignored attachments in the training workspace. See [attachment availability guidance](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/appendix/README.md) for availability and distribution limitations. Obtain originals through the material owner; this pack does not redistribute their contents or override their disclaimers. The source map uses the supplied known pages; it is not a claim of comprehensive PDF or visual review. Students can complete the synthetic report and worksheets without these originals.

## Delivery and outputs

Use the existing three-hour route, rather than adding this pack as extra teaching time:

| Elapsed minutes | Activity / pack output |
|---|---|
| 0–15 | Briefing: read scope, synthetic-status notice and source pack. |
| 15–50 | Exercise 1; production-equivalence and evidence-wording worksheets. |
| 50–80 | Exercise 2; identify at least five queue errors; vendor negotiation. |
| 80–90 | Break. |
| 90–133 | Exercise 3; change, rollback and retest procurement agreement. |
| 133–154 | Acceptance, segregation and incident tabletop. |
| 154–180 | Exercise 4; separate hypothetical update, minutes rewrite and assessment. |

Submit a classification/evidence matrix, reasoned triage, six owned remediation records and a one-page board recommendation. Include scoped citations such as **R1 §5 F2 / S2**. Keep actual results blank or marked **not performed** unless a separately authorised activity supplies genuine evidence.

## Evidence and decision rules

- F1 staging applicability is supplied; production applicability remains unknown.
- F2/F3/F5 are confirmed **within the fictional R1 narrative**, not real observations.
- F4 is vendor scanner evidence with an unverified build exploitation path; build-only does not prove harmlessness.
- F6 is an untested design/assurance gap, not reproduced AI disclosure.
- F1/F4 advisories, CVSS 3.1 scores and F1 KEV/EPSS fields are fictional exercise inputs. There are no real CVE identifiers or supplied vectors; do not manufacture them or fetch live intelligence for these placeholders.
- All six findings are open; there is no remediation or retest evidence in baseline R1. Later workshop injects are separate hypotheticals, not report revisions.
- P1/P2/P3 are illustrative exercise categories, not MTR policy, SLAs or contractual deadlines.
- AI may organise approved synthetic evidence, not accept risk, authorise release or turn an unknown into a finding. Do not upload real reports, tokens, logs or supplier documents to unapproved tools.

The overall illustrative rubric is **evidence 25, triage 25, remediation 25, governance 15, responsible AI 10**. No pass threshold or organisational authority is supplied. Detailed reasoning, acceptable alternatives and critical errors are in the instructor key.