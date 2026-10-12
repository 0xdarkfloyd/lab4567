# Beginner solutions — project management lab

**Reference key: reveal after the [beginner exercises](../student/BEGINNER-EXERCISES.md).** Plain language is sufficient. These are proposed actions and expected verification, not evidence that actions or tests occurred. Baseline R1 remains unchanged: six open findings, production excluded, no remediation/retest results.

## P-B1 — security problems, action and proof

| Finding | Beginner explanation and evidence limit | Proposed response / accountable roles | Evidence before closure |
|---|---|---|---|
| F2 | In the fictional staging case, A can read B's document. Authentication worked but document permission failed. Other paths and production remain unknown. | Application owner contains affected access; developers enforce trusted supplier/resource permission before content; security reviewer checks all equivalent routes. | Identified deployed revision; own-document success and cross-supplier/anonymous/missing-scope denial; no foreign bytes/links; relevant search/preview/export/cache/jobs coverage. |
| F3 | An active synthetic credential is visible to a broad support group. It may be misused; actual misuse is not established. | Credential/service owner revokes or rotates safely, restricts log access, preserves protected investigation evidence and fixes redaction; operations checks consumers still work. | Old credential rejected, replacement works for authorised use, new logs contain no credential, scope of old logs/use investigated; unresolved history remains visible. |
| F1 | An applicable fictional advisory affects the public staging gateway. Production applicability is unknown. | Platform/vendor owner contains affected staging exposure and applies an effective supported mitigation/fix; collect authorised production inventory separately. | Deployed version/configuration and approved mitigation checks, bypass/good paths, independent retest; separate production disposition. No invented CVE/vector. |
| F4 | A vendor scanner flags a build dependency. Its exploitation path has not been shown; build-only does not mean harmless. | Build owner/vendor supplies dependency graph, triggering inputs, affected build steps and artifact impact; plan supported update/mitigation and verify applicability. | Scoped provenance/path evidence and reviewed build-change checks; do not discard solely because it is build-only or rank solely on 9.8. |
| F5 | Fictional staging accepts deprecated TLS. Exact protocol/cipher is not given. | Platform owner verifies configuration and removes deprecated negotiation under an approved target; plan legitimate client compatibility. | Deployed negotiation tests reject the specified deprecated options and allow approved clients. Do not invent a protocol or claim production coverage. |
| F6 | Retrieval permission assurance is missing from the AI design, and isolation has not been tested. This is not a demonstrated AI leak. | Keep affected pilot off/out of release scope until the application enforces ACL filtering before model context, including equivalent caches/tool paths; solution owner supplies independent tests. | Design/code/configuration and own/foreign/missing/revoked-scope tests show filtering before context/output. Prompt instructions alone are not sufficient. |

F1/F2/F3 can need **parallel urgent work**. Exact categories depend on justified context and authorised policy; the lab does not supply mandatory deadlines. F4's Critical score does not automatically outrank a confirmed fictional cross-supplier disclosure or active credential exposure. Missing F2/F3 scores do not establish low risk. Proposed containment does not itself prove closure.

**Example learner sentence:** “F2 lets the wrong supplier see a document. I would restrict that path now, ask the application owner to enforce permission for each document, and request a deployed test showing own documents work but other suppliers' documents never appear.”

**Follow-up Copilot prompt:**

```text
Review my proposed R1 actions. Flag unsupported claims, missing owners, score-only ranking and closure without independent deployed retest. Keep the baseline open and production applicability unknown. Offer acceptable alternatives only if they preserve containment and evidence requirements. Explain corrections in beginner language; do not decide release or risk acceptance.
```

## P-B2 — boundaries and information

| Boundary | Simple answer / required check |
|---|---|
| Supplier A versus B | A's login identifies A, not permission to B's resource. Application must derive supplier identity from trusted server state and check exact-resource permission before content. |
| Service credential versus support logs | A secret copied into a wider-access place gains unintended readers. Minimise/redact logging, restrict access, rotate/revoke exposed credentials safely and assess history. Do not copy the secret into an AI prompt. |
| Vendor/scanner versus project decision | A scanner statement supports that a flag was reported, not every exploit/application/production claim. Request scoped technical evidence, verify independently and keep applicability unknown where missing. |
| Document/model versus application/tool authority | Retrieved text and AI proposals are data, not permission or board approval. Enforce ACL before retrieval enters model context; validate tool schema, action/resource authorization and budgets outside the model. Authorised humans govern release/risk. |
| Staging versus production | Different environments/configurations may change applicability. Obtain authorised version/configuration/exposure evidence; no production testing without scope/authority. Neither safe nor affected can be concluded merely from exclusion. |

R1's F2/F3/F5 observations are **stipulated fictional facts**, not classroom measurements. F1 staging applicability is supplied. F4 is a scanner assertion with an unverified path; F6 is untested assurance. Copilot must preserve this distinction, not upgrade an assertion or an unknown into observed evidence.

**Follow-up Copilot prompt:**

```text
Check my boundary table. Identify any sentence that treats login as authorization, a vendor statement as independent proof, model output as permission, or staging as production. Suggest corrected wording with R1 citations. Keep original-attachment references separate from synthetic finding evidence. No testing or new findings.
```

## P-B3 — reliability/coordination designs (not R1 findings)

### A: overlapping task edits

**Lost update:** the due-date save replaces the newer owner field with an old value. Proposed control: send the revision read with the update, and commit only if the current revision still matches. One update advances revision 4 to 5; the stale update receives a conflict rather than silently overwriting. User reloads, compares changes and explicitly merges/retries. A transaction or conditional update must make comparison and write atomic; “check then save” separately still races.

Owner: workflow/product owner with application maintainer. Expected check: two clients start from revision 4; one save succeeds, the other conflicts; approved merged save retains both changes and history. This example is task-data correctness; if such fields control access/release, it also has security/governance consequences. No implementation is supplied.

### B: duplicate harmless progress-report request

Proposed control: stable request ID, canonical request contents and stored operation state/result. Same ID + same contents resumes/returns the original work, not another job; same ID + different contents rejects. Atomically coordinate deduplication with job creation; otherwise overlapping arrivals still create duplicates. Define pending/failed/completed states, recovery, retention and retries. Do not claim universal exactly-once delivery, especially with external effects.

Owner: operations/workflow owner and maintainer. Expected checks: repeated and simultaneous identical requests create one logical job; changed-content reuse conflicts; crash/retry follows the stated recovery design. The harmless report example is reliability-only as described. Protected jobs would additionally need scoped identity/resource/action and current permission.

### C: stale board pack / scope

An approval for revision A does **not** automatically approve B. Bind the decision/minutes to report revision, proposed scope/build, evidence date, conditions and authorised role. If the scope changes before application, block the stale transition and obtain the required re-review. Preserve the prior decision rather than rewriting history. A real workflow needs an atomic version check when recording/applying the decision, not just a notice in an email.

Owner: PM/change coordinator maintains the versioned pack; release authority makes the decision; independent reviewer verifies its evidence. Expected check: approval for A cannot release B; changed scope triggers the agreed review; unchanged authorised scope follows the normal good path. This is a coordination problem with possible security/governance consequences, not a new confirmed vulnerability. Hypothetical B never becomes baseline R1.

**Follow-up Copilot prompt:**

```text
Challenge these paper-only designs: concurrent revision checks, duplicate-job tracking and applying a decision to a changed scope. Show one failing timeline and one atomic/controlled solution per case. Give expected checks, not actual results. Identify which examples are reliability-only and which could affect security/governance. Do not revise baseline R1 or claim that an implementation exists.
```

## Five quick answers

1. **No:** login identifies a user; exact-resource permission is separate.
2. **No:** no supplied score means unknown score, not low impact/priority.
3. **No:** an email is a claim; closure needs suitable deployed evidence and independent retest, with residual work tracked.
4. **No:** Copilot can organise approved synthetic information; authorised humans accept risk and approve release.
5. **No:** unauthorized disclosure during a revocation race is a security problem as well as a concurrency problem.

## Beginner feedback

Ask “What do you know? What could happen? Who acts? What proof is needed?” Accept conditional alternatives that preserve the supplied facts and control goals. Do not require advanced terminology, code or a new CVSS calculation. Give formative feedback for evidence discipline, clear actions/roles, boundaries and correct expected-versus-actual labels. No new pass threshold, SLA or organisational authority is introduced.