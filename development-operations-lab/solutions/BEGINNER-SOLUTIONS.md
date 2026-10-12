# Beginner solutions — development and security operations

**Reference key: reveal after the [beginner exercises](../docs/BEGINNER-EXERCISES.md).** These are source-grounded explanations and expected checks, not a new test run. Keep common fixtures and independent tests unchanged. Java/C# names differ but the control goals match.

## B1 — security weaknesses and fixes

Any three well-explained issues below meet the beginner task. Do not demand a particular framework or coding style if the invariant is preserved.

| Weakness in exercise source | Simple explanation | Reference solution | Expected permitted / denied checks |
|---|---|---|---|
| Search joins name into SQL | Input can change query structure instead of remaining a value. | Java `PreparedStatement` / C# SQLite parameters. Bind both name and trusted supplier. | Alice finds her own Contract; quote-containing names stay literal and never expand access. |
| Search lacks supplier predicate | Login does not give access to every supplier's rows. | Require current server-derived supplier scope, then filter by supplier and name. | Alice gets 101, Bob gets 102; absent/revoked scope denies before query. |
| Search returns full rows | A search list unnecessarily reveals content and extra fields. | Select/project only ID and name. | Own result has only agreed summary fields; no supplier/content/extra sensitive field leaks. |
| Search logs raw authorization | Credentials in logs can become a second disclosure route. | Remove raw header logging; retain only safe necessary events. | Search still works; logs contain no submitted auth header. |
| Broker lacks strict tool/schema checks | The model can propose an action the application must not accept. | Explicit tool allowlist; exactly integer documentId in 1..999999; reject extra keys. Supplier `approveRisk` always denies. | Alice `readDocument` 101 allows; 102 denies, unknown tool or extra/string argument rejects, approval denies. |
| Surfaces read/cache/jobs lack current permission | Another path or stale result can bypass a fixed search. | Check resource permission on every surface; scope cache keys; recheck hits and enqueue/run/delivery. Export authorizes all items before any content. | Own item succeeds; foreign/revoked item returns no content; mixed export reads nothing. |

For the reference implementations see [Java Search](../fixed/src/training/operations/Search.java), [Broker](../fixed/src/training/operations/Broker.java), [Surfaces](../fixed/src/training/operations/Surfaces.java), or [C# Search](../dotnet/fixed/src/Search.cs), [Broker](../dotnet/fixed/src/Broker.cs), [Surfaces](../dotnet/fixed/src/Surfaces.cs).

**Example explanation:** “Search uses the document name inside SQL text. I would use parameters so the name stays data. I would separately add a supplier filter from the trusted identity. A parameter alone would not stop Alice reading Bob's Contract.”

**Useful follow-up Copilot prompt:**

```text
Compare my three findings with the selected fixed implementations. For each, explain which invariant is satisfied, what my proposed fix still misses, and what independent test would disprove it. Cite methods. Do not rewrite the harness, invent results, or treat a successful build as authorization evidence.
```

## B2 — boundaries and data

| Item | Trust decision and control |
|---|---|
| Search name | Requester-controlled data. Validate length/nonblank and bind into SQL; it cannot set supplier scope. |
| Token text | Requester-supplied candidate credential. The exact local fixture map resolves it to an internal identity or rejects it. This is not production JWT validation. |
| Internal fixture identity / owner metadata | Trusted for this local scenario when obtained through server code, not deserialized from model input. Check current scope and resource ownership; do not expose private metadata. |
| Tool name/documentId/arguments | Mock model output is untrusted input to the deterministic broker. Validate schema, action permission and object permission before content/effects. |
| Document text | Data to analyse, not authority. Embedded “approve” instructions must not change permission or erase evidence gaps. Delimiters may aid interpretation, not enforce access control. |
| Cache/job output | Earlier authorization is not permanent. Scope keys to trusted tenant/resource and recheck current permission before returning bytes, including job delivery. |

**Simple flow:** requester input → trusted fixture identity → current supplier/resource check → bounded database use → minimal response. For tools: document/mock model proposal → schema gate → trusted caller/action/resource checks → atomic quota → guarded final recheck/use → outcome and minimal audit. Read internal ownership metadata to decide permission; protected content must wait.

Alice cannot become Bob by adding `supplierId: SUP-B` to arguments; the fixed broker rejects the extra field and takes requester identity separately. Neither a valid login nor a human confirmation can grant supplier-role risk approval.

**Useful follow-up Copilot prompt:**

```text
Check my boundary table for any place where input is accidentally promoted into authority. Explain why token parsing, supplier scoping, tool schema and resource authorization are separate. Give one counterexample per mistaken assumption without producing exploit instructions or executing code.
```

## B3 — race and reliability solutions

### B3a: two requests, one slot

Unsafe timeline: A sees 1; B sees 1; A takes a slot; B also takes a slot. The required outcome is **one reservation succeeds**, the other stops; only one content read/mock effect occurs. The fixed quota uses an atomic reservation, not a separate check followed by update. This is a correctness/resource-control issue and can also affect security, availability and cost limits.

Refund a reservation if permission is revoked **before consumption starts**. Once a content read starts, the reservation is consumed even if the read fails. Do not double-refund. Test concurrent starts with a barrier and bounded waits, not lucky scheduling or sleeps. See [Java Budget](../common/src/training/operations/Budget.java) / [C# Budget](../dotnet/Common/Budget.cs).

### B3b: revoke-before-use

Unsafe timeline: A passes permission check → pause → another thread revokes A → A retrieves content without a guarded recheck. This is **both a race and a security vulnerability**, not merely a performance bug. The fixed surface and broker recheck under the same policy lock used for revocation and keep content retrieval/mock effect within that guarded operation.

Expected deterministic check: pause after initial allow, commit revocation, resume; deny with **zero content reads/effects**, and refund an unused broker reservation. Also check that query entry holds the shared guard. If the guarded read completes before revocation can acquire the guard, the earlier operation was allowed at its linearisation point; later revocation cannot undo bytes already read. A local lock is not a distributed-system guarantee.

### B3c: retried progress update — paper-only design

First operation `op-17` adds one to the harmless progress count and stores its completed result. Retry of `op-17` with identical arguments returns that result without another increment. Different arguments with the same ID reject as a conflict. Coordinate recording the operation and changing the count in one transaction/atomic commit; otherwise a crash between them can still duplicate or lose the update. Bound retention and define what happens after records expire.

Expected design checks: same request twice gives one increment; simultaneous duplicates give one increment; changed arguments reject; crash/retry does not apply the increment twice within the stated persistence contract. **Not implemented by this lab.** External effects may need provider idempotency or reconciliation, not an unsupported “exactly once” claim. If adapted to protected actions, add trusted principal/resource/action scoping and replay authorization.

**Useful follow-up Copilot prompt:**

```text
Challenge my concurrency fix. Can revocation still happen between check and use? Can both requests reserve the last slot? Can retries or crashes duplicate the hypothetical progress update? Propose a deterministic negative test and a good-path test for each. Clearly label which behavior the source implements and which is a design only. Report no actual results.
```

## Simple debrief / marking guide

Accept plain language. Ask the learner to explain **cause → consequence → fix → check**, not recite a label. Each of B1/B2/B3 is satisfactory when at least one clear example is correct and the submission completes the requested tables; stronger answers cover all cases and alternatives. This is formative feedback, not a new organisational pass threshold. Important corrections: SQL binding alone is not authorization; model output is not permission; revocation races are security relevant; expected checks are not executed results.