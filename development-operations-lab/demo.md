# B1–B3 Copilot prompt-review demo — prompts, logs and results

## Run record and limits

- Review date: **12 October 2026**.
- Selected implementation: **Java exercise track** in this synthetic lab.
- Training source baseline: **f92ac6d143a9103fbe037caae9ca3d5e5edc9c76**. Student repository baseline: **d64cda0776f7f407d47f1ec579597ed889084a95**.
- Method: the three suggested prompts in [the beginner exercise sheet](docs/BEGINNER-EXERCISES.md) were submitted to three independent read-only review agents. Their answers were then manually checked against the selected source and shared harness.
- **Prompt reviews ran; the Java application and test suite did not run.** No compilation, launcher invocation, dependency installation, live-model application test or production access occurred. There is no new PASS/FAIL run to report. C# was not reviewed or executed in this demo.
- During the original reviews, no workspace files were modified. The clean working tree and six source hashes were checked before and after. This document is a later, explicitly requested capture of that review.
- Results below are the complete consolidated B1–B3 review response, with the draft-answer corrections recorded separately. They are not a verbatim internal agent trace or runtime console transcript. Internal reasoning and temporary tool-resource files are not part of the lab evidence.
- All suggested fixes and tests remain **proposals/expected checks**, not implemented fixes or observed test results.

## 1. Captured terminal verification logs

The following commands were run before the prompt reviews, from the training workspace:

```powershell
git status --short
git diff --no-ext-diff --stat
Get-FileHash development-operations-lab\exercise\src\training\operations\Search.java,development-operations-lab\exercise\src\training\operations\Broker.java,development-operations-lab\exercise\src\training\operations\Surfaces.java,development-operations-lab\common\src\training\operations\Budget.java,development-operations-lab\common\src\training\operations\Fixture.java,development-operations-lab\common\src\training\operations\Harness.java -Algorithm SHA256 | Format-Table -AutoSize | Out-String -Width 220
```

**Observed output:** both Git commands produced no entries: the tracked working tree was clean and no diff was reported. The hash output is reproduced below using repository-relative paths instead of the machine's absolute user-directory prefix:

```text
SHA256 0390F6D635EF82F47B8142B3BEF3118771EFC1AD978C2258E950E35C6E629B09 exercise/src/training/operations/Search.java
SHA256 C7CFB2DC8C9C524E2A07FD25C8F055DF52BC766BD4DEF2F439EE395A73308937 exercise/src/training/operations/Broker.java
SHA256 00C50A6EF8DE764DF5E416109C3BB7DFA2691B3C7040099C17874C4B647E2C28 exercise/src/training/operations/Surfaces.java
SHA256 B13D8185F4BE5EC8B14BB4296B575949373820089AF947089E8A16850D4349C0 common/src/training/operations/Budget.java
SHA256 8A547D0B9B1A88CFD75E59B5CD15E20BCAEE87E650A071BBC501F98940EC43D4 common/src/training/operations/Fixture.java
SHA256 9F8F2D2C1E252347C5BBB5F62B6D6935088A324162D43F359910DBBE055E04E2 common/src/training/operations/Harness.java
```

After the reviews and source checks, these commands were run:

```powershell
git status --short
Get-FileHash development-operations-lab\exercise\src\training\operations\Search.java,development-operations-lab\exercise\src\training\operations\Broker.java,development-operations-lab\exercise\src\training\operations\Surfaces.java,development-operations-lab\common\src\training\operations\Budget.java,development-operations-lab\common\src\training\operations\Fixture.java,development-operations-lab\common\src\training\operations\Harness.java -Algorithm SHA256 | Select-Object Hash | Format-Table -HideTableHeaders
```

**Observed output:** Git again produced no entries. The six hash values were:

```text
0390F6D635EF82F47B8142B3BEF3118771EFC1AD978C2258E950E35C6E629B09
C7CFB2DC8C9C524E2A07FD25C8F055DF52BC766BD4DEF2F439EE395A73308937
00C50A6EF8DE764DF5E416109C3BB7DFA2691B3C7040099C17874C4B647E2C28
B13D8185F4BE5EC8B14BB4296B575949373820089AF947089E8A16850D4349C0
8A547D0B9B1A88CFD75E59B5CD15E20BCAEE87E650A071BBC501F98940EC43D4
9F8F2D2C1E252347C5BBB5F62B6D6935088A324162D43F359910DBBE055E04E2
```

**Verification result:** all six hashes matched; no tracked changes were introduced by the prompt-review activity. This is file-integrity evidence, not proof of application correctness.

## 2. Prompt-review activity log

1. Read the B1–B3 instructions and exact suggested prompts.
2. Recorded the terminal baseline above.
3. Submitted B1, B2 and B3 concurrently to separate read-only review agents, selecting Java and explicitly prohibiting edits, builds, test execution, file writes and memory writes.
4. Received one B1 findings table, one B2 flow/boundary report and one B3 race/design report. The agents did not produce runtime test output.
5. Read exercise Search, Broker and Surfaces directly; read the shared Harness, Fixture and Budget to verify the claims, policy semantics and expected counters.
6. Corrected misleading draft recommendations and inaccurate characterizations, as recorded in section 6.
7. Reported the consolidated findings below and repeated the terminal verification.

Shared sources used for verification: [Harness](common/src/training/operations/Harness.java), [Fixture](common/src/training/operations/Fixture.java), [Budget](common/src/training/operations/Budget.java). B2 also used the lab's identity/proposal types and scripted mock/demo context. Source inspection does not establish JDBC execution or live-model behavior.

## 3. B1 — security weaknesses

### Exact suggested prompt

```text
Review ONLY the selected exercise Search and Broker files and the shared test expectations in this synthetic lab. Do not edit files yet. Explain up to five security weaknesses in simple language. For each, cite the file/method, show the root cause, suggest the smallest fix, and propose one permitted and one denied test. Treat SQL binding and supplier authorization as separate controls. Do not create attack payloads, change fixtures/tests, use real data, or claim execution. Mark uncertain claims as unknown.
```

### Consolidated, source-checked result

| Finding and source | Cause and proposed minimal fix | Expected checks — not executed |
|---|---|---|
| SQL input changes query structure — [Search](exercise/src/training/operations/Search.java#L17-L20) | `name` is concatenated into SQL. Use a prepared statement with bound parameters. | Permitted: Alice's exact `O'Reilly Guide` search returns 103. Adversarial/invalid: the existing harness's SQL-control input remains literal data and returns no matching rows. No new payload is generated here. |
| Missing supplier authorization — [Search](exercise/src/training/operations/Search.java#L14-L20), [Broker](exercise/src/training/operations/Broker.java#L15-L24) | Search authenticates but has no current-scope check or supplier predicate. Broker checks only that the requester is non-null. Bind the trusted supplier in search and check current resource permission before broker content/effects. SQL binding alone does not fix authorization. | Permitted: Alice finds 101; Bob finds 102; Alice may read 101. Denied: NoScope search gets 403 before DB access; Alice's read of 102 is denied with zero content reads/effects. |
| Excessive response data and token logging — [Search](exercise/src/training/operations/Search.java#L16-L20), [entity mapping](common/src/training/operations/Fixture.java#L88-L98) | Raw authorization is logged; `SELECT *` and entity mapping expose supplier, sensitive fields and content. Return only `id/name`; log only a minimal safe event, not raw or hashed tokens. | Permitted: successful search returns exactly `id/name`. Forbidden disclosure: no token, sensitive content or raw arguments appear in captured sinks. |
| Missing search validation — [Search](exercise/src/training/operations/Search.java#L13-L20) | Null, blank and overlength names reach SQL. Reject invalid names before DB work. Preserve exact-match semantics; do not trim valid input into a different search. | Permitted: length 100 is accepted. Invalid: null, blank or length 101 gets 400 with zero queries. |
| Broker trusts malformed or unauthorized proposals — [Broker](exercise/src/training/operations/Broker.java#L18-L24) | Missing/wrong-type IDs default to 101; extra fields are ignored; unknown tools fall through to a mock effect. Enforce exact schema, registered-tool allowlist and independent tool permission before spending quota or performing effects. | Permitted: valid authorized `readDocument` succeeds. Denied/rejected: `approveRisk` is denied; unknown tools, string IDs and extra identity fields are rejected with zero reads/effects. |

The [shared harness](common/src/training/operations/Harness.java#L57-L143) defines these expected outcomes independently of the exercise implementation. An AI finding is a review hypothesis; source evidence supports the causes, while genuine execution would be needed for actual-result fields.

## 4. B2 — untrusted data and boundaries

### Exact suggested prompt

```text
Using the selected lab files, draw a simple data flow for search and the mock broker. Label requester-controlled data, document text, model proposals, trusted fixture identity and current resource permission. Explain each trust boundary. List checks before SQL, before content retrieval and before output delivery. Do not treat delimiters, model instructions, a token string or a supplied supplierId as permission. Do not edit code or claim a live-model test.
```

### Consolidated, source-checked result

**Required search flow:** requester token/name → trusted fixture authentication → current supplier scope + input validation → parameterized supplier-scoped SQL → minimal response.

**Required mock-broker flow:** document text/model proposal → untrusted tool name/arguments → strict schema/tool policy → trusted requester's current resource permission → atomic quota reservation → guarded permission recheck/content access → minimal outcome/audit.

These are required controls, not an assertion that the exercise already performs them. The mock outputs are scripted; no live AI model was tested.

| Input/output | Who supplies it? | Can it grant authority? | Required check |
|---|---|---|---|
| Search name | Requester | No | Validate bounds; bind as SQL data. |
| Token text / identity | Token text is requester-controlled; identity comes from [trusted fixture lookup](common/src/training/operations/Fixture.java#L55-L59) | Token text alone: no. Recognized identity still needs authorization. | Authenticate, then check current scope. The fixture is not JWT middleware. |
| Tool name / `documentId` | Requester or mock model proposal | No | Exact schema, allowed tool, resource permission and quota. |
| Document text / model proposal | Retrieved content / scripted mock output | No | Instructions inside content are data, not permission. Delimiters do not enforce access control. |
| Cached or queued result | Application storage or earlier decision | Not by itself | Recheck current permission at cache access, job execution and delivery. |

**Trust boundaries:**

1. Request strings enter the application as untrusted input. A plausible token or supplied supplier field is not an authority decision.
2. Fixture authentication maps an exact synthetic token to an application-held identity. It does not validate real JWTs or demonstrate production authentication integration.
3. Tool/schema validation separates an untrusted proposal from an operation the application may consider. Even a valid proposal can be unauthorized.
4. Current scope/ownership policy separates identification from permission to access a specific resource.
5. Database/content and output boundaries must prevent protected data from being read or delivered before authorization, including alternate paths.

**Concrete exercise gaps** in [Surfaces](exercise/src/training/operations/Surfaces.java#L15-L44):

- `read()` checks only for a non-null identity.
- `cached()` returns a name-keyed hit without checking the caller or current permission, allowing same-name cross-supplier confusion and stale access after revocation.
- `run()` trusts the earlier enqueue decision instead of checking current policy.
- `deliver()` returns completed content without a current-policy check.
- `export()` begins reading items before establishing that all requested items are permitted. A denied mixed-supplier export must not first read the permitted subset.

**Metadata versus content:** [ownership/scope metadata checks](common/src/training/operations/Fixture.java#L60-L70) can legitimately precede the authorization decision; they do not disclose protected content. [Content retrieval](common/src/training/operations/Fixture.java#L74-L82) is separate and requires permission. Metadata participates in authorization, but does not itself replace the current policy decision.

## 5. B3 — races and reliability

### Exact suggested prompt

```text
Review the lab quota and guarded read/broker functions. Explain the one-slot race and revoke-before-use race with a two-request timeline. Classify their consequences, propose atomic reservation and a shared guarded permission-check/use fix, and design deterministic tests using barriers/latches with bounded waits rather than sleeps. Separately design (do not implement or claim execution of) idempotent processing for a hypothetical harmless retried progress update. Preserve fixtures and secure oracles; distinguish existing code from new design assumptions.
```

### B3a — one remaining quota slot

The unsafe reservation uses a separate read and write in [Budget](common/src/training/operations/Budget.java#L23-L27):

| Order | Request A | Request B |
|---|---|---|
| 1 | Reads remaining = 1 | |
| 2 | | Reads remaining = 1 |
| 3 | Writes 0; succeeds | |
| 4 | | Writes 0; succeeds |

**Possible result from this interleaving:** two requests succeed using one slot. This is a source-derived scenario, not an observed run.

**Classification:** reliability/quota-integrity failure, potentially also a security or resource-control problem; not automatically non-security. The fixture is a per-run quota, not durable monetary accounting.

**Proposed fix:** use the existing atomic [reserve operation](common/src/training/operations/Budget.java#L14-L22). Refund only a reserved-but-unused slot. A started-but-failed content read remains charged under the lab contract. The safe and unsafe counters in this teaching class are separate; do not interpret the safe `remaining()` accessor as a measurement of unsafe consumption.

**Deterministic expected test:** barriers coordinate two callers; bounded waits avoid hanging. Require one `ALLOWED`, one `STOPPED`, one content read and one effect. Existing [case 35 / budget race](common/src/training/operations/Harness.java#L229-L243) supplies that oracle.

### B3b — permission removed before use

For [guardedRead()](exercise/src/training/operations/Surfaces.java#L41-L44):

1. Read request checks Alice's permission: allowed.
2. Request pauses at `afterInitialCheck`.
3. Another operation revokes Alice.
4. Request resumes and retrieves content without a guarded recheck.

**Classification:** security and concurrency correctness failure: stale authorization can disclose content after revocation.

**Proposed fix:** the final permission check and protected read/effect must occur under the same guard used by revocation. Another unlocked check alone still leaves a race. This is local-process coordination, not a distributed transaction.

**Broker distinction:** the exercise broker does not perform an initial resource-permission check at all. Its hook follows requester/quota checks only. It therefore has a broader authorization defect as well as missing revocation coordination; do not describe its hook as proof of an earlier resource authorization.

| Existing case | Expected outcome — not executed in this demo |
|---|---|
| 33: guarded-read revocation | Pause → revoke → resume: empty 403 and zero content reads. |
| 34: broker revocation/reservation release | `DENIED`, empty body, zero reads/effects, unused quota refunded, minimal denial audit. |
| 35: one-slot concurrency | Exactly one allowed call and one stopped call; exactly one read/effect. |
| 36: shared policy lock | Protected content access occurs while holding the guard used by revocation. |
| 37: started database failure | Safe `FAILED`, empty body, one attempted content read, no effect, minimal failure audit; no refund after the read starts, so the next call stops. |

See [case definitions 33–37](common/src/training/operations/Harness.java#L177-L198) and [race coordination](common/src/training/operations/Harness.java#L203-L243). Latches/barriers use bounded waits, not sleeps. Revocation after a guarded read completes cannot retract bytes already delivered.

### B3c — retried progress update: hypothetical design only

**Problem:** a harmless progress increment succeeds, its reply is lost, and the sender retries. Processing the retry as new work increments twice.

**Design assumptions:** this new example involves no protected content or permission decision. It is not an implemented feature of the lab.

1. Use a stable operation ID scoped to the job/action; persist canonical arguments or their fingerprint alongside that ID.
2. Atomically coordinate deduplication, the increment and the stored result. Merely checking a ledger and later incrementing leaves another race/crash window.
3. Same ID + same arguments: return the saved result without another increment.
4. Same ID + different arguments: reject the conflict. Do not put the payload hash into the deduplication key in a way that turns conflicting retries into new operations.
5. Define bounded handling/recovery for an interrupted or in-progress operation. If the effect and ledger are in separate systems, additional coordination is needed; a local ledger alone is insufficient.

**Expected design checks:** sequential and concurrent duplicates increment once; conflicting arguments are rejected; a retry after a lost response returns the original result; crash/recovery does not silently reapply a completed increment.

This is not evidence of durable retries or exactly-once delivery. No code, storage or test for this hypothetical was created or run.

## 6. Draft-answer corrections and review lessons

The independent prompt answers found the main weaknesses, but source checking was necessary:

| Draft answer issue | Correction used in the final result |
|---|---|
| B1 suggested redacting/hashing tokens or logging a user identifier. | Default to a minimal safe event. Do not retain raw or hashed tokens unnecessarily; captured sinks must satisfy the explicit contract. |
| B1 suggested canonicalizing/trimming search input. | Reject invalid input but preserve exact matching. The harness explicitly distinguishes `Contract` from `Contract ` and lowercase `contract`. |
| B1 omitted search supplier authorization and response minimization from its five-row draft. | Include both important findings; group disclosure/logging and related proposal deficiencies to retain the prompt's five-finding limit. |
| B1 described the existing SQL-control literal as not legitimate filter data. | It must be handled as ordinary literal data, returning 200 with no matching rows under this fixture, not automatically rejected because it resembles SQL. |
| B2 blurred metadata and authority in a few statements. | Trusted metadata supports the current policy decision; non-disclosing ownership lookup is legitimate before protected content access. A token string or model field still cannot grant authority. |
| B3 described the exercise broker as checking resource permission before its pause hook. | It does not. Only the guarded surface has that initial resource check; broker has an additional baseline authorization gap. |
| B3 proposed an idempotency key containing the payload hash. | Stable operation identity is the key; payload fingerprint is a separately checked value so same-ID/different-argument conflicts are rejected. |
| B3's helper report compared fixed code and offered additional expected cases. | Reference implementation inspection is not execution evidence; this consolidated result grounds defects in exercise source and expectations in the shared harness. |

**Lesson:** suggested prompts can accelerate review, but their outputs require manual source checking and independent expected behavior. A plausible AI answer is not an oracle and is not proof that a fix works.

## 7. Final results and evidence status

| Activity | Actual status |
|---|---|
| B1 suggested prompt review | Completed; five consolidated security findings reported. |
| B2 suggested prompt review | Completed; flows, boundary table and alternate-path gaps reported. |
| B3 suggested prompt review | Completed; two existing race scenarios and a separate hypothetical retry design reported. |
| Manual verification | Completed against exercise source and shared expectations; draft corrections recorded. |
| Before/after working-tree checks | Clean during the original reviews. |
| Before/after six source hashes | Matched. |
| Java compilation/demo/test suite | **Not run.** No new PASS/FAIL evidence. |
| C# compilation/test suite | **Not run.** |
| Live-model, production or distributed-system tests | **Not run.** |
| Source fixes / hypothetical retry implementation | **Not performed.** |

Historical validation elsewhere in the pack is separate from this demo. Preserve fixtures and independent tests when later practicing fixes. Attempt the exercises before using this answer-bearing capture; it is a debrief reference, not additional required class work.