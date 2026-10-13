# C# B1–B3 read-only prompt-review demo — results and logs

## Run record

- Date: **13 October 2026**. Selected language: **.NET C# exercise track**.
- Baselines before this documentation update: training **ecc2065bf6e41795304c3cc169c27524146e4ae0**; student **9ed22e9a647cf2d198697fcc0962f87c0c249a58**.
- The three [beginner suggested prompts](../docs/BEGINNER-EXERCISES.md) below were supplied together to one read-only C# review agent. Exercise code/shared expectations and fixed comparison code were inspected; findings were manually checked against the actual C# source. This differs from the earlier Java demo's three-agent dispatch.
- **No .NET build, restore, application/demo/test execution occurred.** These are review outputs, not runtime logs or PASS results. No live-model application/production tests occurred. net8.0/C#12 needs approved SDK8+ and suitable runtime; the recorded SDK2.1.526 is insufficient and was not newly probed here. Java results do not validate C#.
- No source, fixture or test was modified. The only new work is documentation/PDF publication. This capture summarizes review activity and source evidence, not verbatim internal agent traces.
- [C# fix diffs and explanations](BEGINNER-SOLUTIONS.md) provide the complete code-review follow-up. [Java demo](../demo.md) remains a separate historical record.

## Verification commands and observed output

Before documentation edits, both repository status commands reported clean aligned main branches:

```text
git status --short --branch
## main...origin/main
git -C ../lab4567 status --short --branch
## main...origin/main
```

Both fetches completed and `git log --oneline --left-right HEAD...origin/main` produced no entries in either repository. Read commands inspected exercise/fixed Search, Broker, Surfaces and Common Budget/Fixture/Harness. No launcher or dotnet command was invoked.

The following hash command was actually run from the training workspace:

```powershell
Get-FileHash development-operations-lab\dotnet\exercise\src\Search.cs,development-operations-lab\dotnet\exercise\src\Broker.cs,development-operations-lab\dotnet\exercise\src\Surfaces.cs,development-operations-lab\dotnet\Common\Budget.cs,development-operations-lab\dotnet\Common\Fixture.cs,development-operations-lab\dotnet\Common\Harness.cs -Algorithm SHA256 | Select-Object Hash | Format-Table -HideTableHeaders
```

Observed SHA256 output, in that order:

```text
125E18942D51E0B3BE2B95410F62FF210AA336F6A00C3804BA29832448C0E537
DB22CE35E0FC422C0175211A370668B7761713DC22A66EE65CCBBB1E3F8A68C1
7B4E2FEB646557BC247A7FC814B576BD5F3A96AEF16C082EA8C272A6B434C852
B23D0F3F23DAFB350A09E7947457CBED2860A581EC87BA0CFC431307D8EC3362
603281D5CE2D07E899F83240EF8642CDABC0D97E937056B3FA292A26A8A54893
686E921D74DA532B28AB7167ADCCB7A2779F20AEB643651B8E0D2DEFEA650B86
```

These hashes identify reviewed sources, not runtime assurance. Documentation publication can make the working tree dirty while leaving those sources unchanged.

The same hash command was repeated after drafting/synchronizing the C# materials, before PDF rendering. **Observed output:** the same six values, in the same order; all reviewed source hashes matched. Documentation changed as requested; application source did not.

## B1 prompt and reviewed results

```text
Review ONLY the selected exercise Search and Broker files and the shared test expectations in this synthetic lab. Do not edit files yet. Explain up to five security weaknesses in simple language. For each, cite the file/method, show the root cause, suggest the smallest fix, and propose one permitted and one denied test. Treat SQL binding and supplier authorization as separate controls. Do not create attack payloads, change fixtures/tests, use real data, or claim execution. Mark uncertain claims as unknown.
```

| Finding | Source cause / minimal fix | Expected positive / negative — not run |
|---|---|---|
| SQL concatenation | [Search.Find](exercise/src/Search.cs): `name` enters SQL text. Use SQLite parameters. | Apostrophe exact name →103; harness SQL-like value stays literal, no rows. |
| Missing supplier authorization | Search authenticates without current scope or supplier predicate; [Broker.Call](exercise/src/Broker.cs) has no resource authorization. Check trusted scope, bind trusted supplier, enforce `Allowed` before content/effect. | Alice101/Bob102; NoScope403 before query; Alice102 broker denial with no read/effect. |
| Excess data / credential logs | Search logs authorization and uses `SELECT *`/`Rows(..., true)`. Minimal SQL/`Rows(..., false)` and safe event. | Only id/name; no token, content or raw arguments in sinks. |
| Missing validation | Null/blank/>100 reach SQL. Reject before DB, preserve exact semantics. | Length100 accepted; null/blank/101 →400 with no query. |
| Proposal schema/tool trust | Broker defaults malformed IDs to101; ignores extra keys; unknown tools gain mock effects. Strict exact-key/type/range/allowlist plus role/resource policy. | Valid own read allows; unknown/bad schema rejects; valid approval/foreign read denies. |

**Manual correction:** malformed/unknown proposals are `REJECTED`; valid but forbidden approval/cross-supplier operations are `DENIED`. Do not collapse these into one status. Authentication and SQL binding are not authorization. Do not generate payloads, normalize valid exact names or hash tokens as a logging fix.

## B2 prompt and reviewed results

```text
Using the selected lab files, draw a simple data flow for search and the mock broker. Label requester-controlled data, document text, model proposals, trusted fixture identity and current resource permission. Explain each trust boundary. List checks before SQL, before content retrieval and before output delivery. Do not treat delimiters, model instructions, a token string or a supplied supplierId as permission. Do not edit code or claim a live-model test.
```

**Required flow:** requester token/name → `Fixture.Authenticate` → current scope + validation → parameterized supplier SQL → id/name. Proposal tool/arguments → strict schema → trusted requester/tool/resource policy → atomic quota → guarded current-permission/content/effect → outcome/audit. These are required controls, not a statement that exercise code enforces them.

| Value | Source / authority | Required boundary check |
|---|---|---|
| Search name | Requester; no authority | Bounds/nonblank, SQL binding. |
| Token / identity | Requester candidate token; trusted fixture map resolves identity | Recognized identity then current scope. Not real JWT validation. |
| Tool / documentId | Mock proposal; no authority | Exact ordinal key, boxed int/range, registered tool and independent permission. |
| Document text / mock output | Data/scripted output; no authority | Instructions remain data; delimiters cannot grant access. |
| Cache/job output | Earlier state; not perpetual permission | Scoped key, current checks on hit/execution/delivery. |

[Fixture.Allowed](Common/Fixture.cs) privately checks scope/ownership before protected content; this non-disclosing metadata lookup is legitimate. `Materialize` retrieves protected content and intentionally contains no ACL. [Surfaces](exercise/src/Surfaces.cs) checks only non-null identity for Read, caches by name without policy on hits, trusts old jobs and delivers unchecked completed content; Export reads before establishing all-item authorization.

**C# nuance:** private `dbLock` protects SQLite connection access, not permissions. `PolicyLock` coordinates scope/revocation. Fixed code uses policy → database lock order. A serial database connection does not repair quota races or stale permission checks.

## B3 prompt and reviewed results

```text
Review the lab quota and guarded read/broker functions. Explain the one-slot race and revoke-before-use race with a two-request timeline. Classify their consequences, propose atomic reservation and a shared guarded permission-check/use fix, and design deterministic tests using barriers/latches with bounded waits rather than sleeps. Separately design (do not implement or claim execution of) idempotent processing for a hypothetical harmless retried progress update. Preserve fixtures and secure oracles; distinguish existing code from new design assumptions.
```

- **Quota:** A snapshots1 → B snapshots1 → A writes0/true → B writes0/true. Both may proceed. `Volatile.Read` + `Interlocked.CompareExchange` in safe `Reserve()` makes check/take atomic. This is reliability/resource-control with possible security consequences. Safe/unsafe counters differ; `Remaining` reads safe counter.
- **Revocation:** `GuardedRead` allows → pauses → Revoke → resumes Retrieve without guard. Recheck/use must share `PolicyLock` with Revoke. Broker has no initial resource check in exercise code; add that plus final guarded check. This is a security race, not merely performance.
- **Reservation settlement:** release once only if reserved but no content read started. Started failures are charged and minimally audited. A lock does not give durable/distributed guarantees or retract already delivered bytes.
- **Hypothetical harmless retry:** stable operation key and separately checked argument fingerprint; atomic count/ledger/result commit; identical retry returns saved result; same-ID/different-argument rejects. Define recovery and retention. No implemented feature or exactly-once-delivery evidence. See solution pseudocode.

| C# harness cases | Expected check — not executed |
|---|---|
| 33 | Pause/read, revoke, resume: empty403, zero content reads. |
| 34 | Broker denial, zero reads/effects, unused slot refunded, decision-only audit. |
| 35 | Two workers/barriers: one allowed, one stopped, one read/effect. |
| 36 | `Monitor.IsEntered(PolicyLock)` at protected content entry. |
| 37 | DB failure safe/charged/audited; next request stopped. |

[Harness](Common/Harness.cs) uses bounded `ManualResetEventSlim`/`Barrier` waits and worker completion bounds, not sleeps. Case20 also challenges argument key spelling despite a case-insensitive dictionary. Oracle/source inspection does not report actual outcomes.

## Final evidence status

Read-only C# prompt review and manual source comparison completed. Fixes are documented against existing reference code; no application-source changes. Compilation, restore, all37 execution, live-model application, ASP.NET/JWT integration and hypothetical progress implementation: **not performed/unverified**. Preserve fixtures and independent assertions; reveal this answer-bearing capture after the attempt.