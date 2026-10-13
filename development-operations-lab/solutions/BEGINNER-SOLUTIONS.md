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

## B1–B3 Java code-fix diffs and explanations

These diffs explain the fixes for the [Java prompt-review demo](../demo.md). They compare the existing **exercise** and **fixed** Java sources; they do not apply changes to either track. C# equivalents remain linked above, but the diffs here are Java only. Imports/class declarations are omitted where unchanged or irrelevant; each fenced diff is a **review excerpt, not a standalone patch**. The linked fixed files contain the complete implementation. Line wrapping in the excerpts is for readability.

**Evidence limit:** this documentation update does not compile or execute anything. Tests below are expected checks, not new PASS results. Keep shared fixtures and secure test expectations unchanged. Local locks/cache/jobs are teaching mechanisms, not production/distributed-system assurance.

### B1: Search — parameters, authorization, validation and minimal data

Compare [exercise Search](../exercise/src/training/operations/Search.java) with [fixed Search](../fixed/src/training/operations/Search.java). Replace the `Statement` import with `PreparedStatement`; the method's authenticated-identity lookup and anonymous 401 stay unchanged.

```diff
-        fixture.logs.add("Auth=" + authorization);
-        String sql = "SELECT * FROM Documents WHERE Name = '" + name + "' ORDER BY Id";
-        fixture.searchQueries.incrementAndGet();
-        try (Connection c = fixture.connection(); Statement s = c.createStatement()) {
-            try (ResultSet rs = s.executeQuery(sql)) { return new Response(200, Fixture.entities(rs)); }
-        } catch (SQLException failure) {
-            // Safe console error, but the vulnerable query still fails for legitimate apostrophes.
-            return Response.empty(500);
+        synchronized (fixture.policyLock) {
+            if (!fixture.hasScope(identity)) return Response.empty(403);
+            if (name == null || name.isBlank() || name.length() > 100) return Response.empty(400);
+            fixture.logs.add("search:accepted");
+            try (Connection c = fixture.connection(); PreparedStatement s = c.prepareStatement(
+                    "SELECT Id, Name FROM Documents WHERE SupplierId = ? AND Name = ? ORDER BY Id")) {
+                s.setQueryTimeout(2); s.setMaxRows(100);
+                s.setString(1, identity.supplier()); s.setString(2, name);
+                fixture.searchQueries.incrementAndGet();
+                try (ResultSet rs = s.executeQuery()) { return new Response(200, Fixture.summaries(rs)); }
+            } catch (SQLException failure) { return Response.empty(500); }
	    }
```

**Why each change matters:**

- `hasScope()` rejects missing/revoked scope before database work. The supplier comes from the trusted identity, never a request/model field.
- Parameters keep the name as data; the separate supplier predicate prevents cross-supplier matches. Neither control substitutes for the other.
- Validation rejects null/blank/>100 names before the query. Do not trim or change case: the fixture intentionally uses exact matching. Length 100 remains valid.
- `Id, Name` plus `Fixture.summaries()` minimizes returned fields. Safe event logging removes the credential sink; do not replace raw-token logging with unnecessary token hashes.
- Holding `policyLock` through the scope check and query coordinates local revocation. Timeout/row cap bound database work; they do not grant permission.

**Expected checks:** harness cases 01–12: Alice 101 only, Bob 102 only, apostrophe name 103, anonymous/unknown identity 401, NoScope 403 before DB, invalid names 400 before DB, SQL-like input treated literally, only id/name and no token/content/raw-argument logs. Preserve the exact-name and 100-character boundary tests.

### B1/B2/B3: Broker — full validation, authorization and reservation lifecycle

Compare [exercise Broker](../exercise/src/training/operations/Broker.java) with [fixed Broker](../fixed/src/training/operations/Broker.java). The complete reference adds `import java.util.Set;`. These changes belong together: schema validation alone does not repair authorization or races.

```diff
-        Decision decision = Decision.FAILED;
+        Decision decision = Decision.REJECTED;
+        boolean reserved = false;
+        boolean consumed = false;
	    try {
-            if (requester == null) { decision = Decision.DENIED; return new Outcome(decision, ""); }
-            if (!budget.reserveUnsafe()) { decision = Decision.STOPPED; return new Outcome(decision, ""); }
+            if (proposal == null || proposal.tool() == null || proposal.arguments() == null)
+                return new Outcome(decision, "");
+            if (!Set.of("readDocument", "approveRisk").contains(proposal.tool())) return new Outcome(decision, "");
+            if (!proposal.arguments().keySet().equals(Set.of("documentId"))) return new Outcome(decision, "");
+            Object rawId = proposal.arguments().get("documentId");
+            if (!(rawId instanceof Integer id) || id < 1 || id > 999999) return new Outcome(decision, "");
+            decision = Decision.DENIED;
+            // Approval is NOT a permission grant. This supplier role can never approve risk.
+            if (requester == null || proposal.tool().equals("approveRisk") || !fixture.allowed(requester, id))
+                return new Outcome(decision, "");
+            if (!budget.reserve()) { decision = Decision.STOPPED; return new Outcome(decision, ""); }
+            reserved = true;
		   afterInitialCheck.run();
-            Object rawId = proposal.arguments().getOrDefault("documentId", 101);
-            int id = rawId instanceof Integer number ? number : 101;
-            // Unknown tools fall through to a mock effect. Extra identity/arguments are ignored.
-            String body = "";
-            if (proposal.tool().equals("readDocument")) body = fixture.materialize(id);
-            fixture.toolEffects.incrementAndGet();
-            decision = Decision.ALLOWED; return new Outcome(decision, body);
+            synchronized (fixture.policyLock) {
+                if (!fixture.allowed(requester, id)) return new Outcome(decision, "");
+                // Same lock as revoke: current permission, content and mock effect are one guarded operation.
+                consumed = true; // A started content read costs one unit even if it fails.
+                String body = fixture.materialize(id);
+                fixture.toolEffects.incrementAndGet();
+                decision = Decision.ALLOWED; return new Outcome(decision, body);
+            }
	    } catch (SQLException | RuntimeException failure) {
-            return new Outcome(decision, "");
-        } finally { fixture.audit(decision); }
+            decision = Decision.FAILED; return new Outcome(decision, "");
+        } finally {
+            if (reserved && !consumed) budget.release();
+            fixture.audit(decision); // Enum only; never model args, tokens, contents or exception messages.
+        }
```

**Explanation:**

1. Null/unknown/malformed proposals stay `REJECTED`. Require exactly `documentId`, Java `Integer` and the fixture range; never default malformed input to 101 or accept extra identity claims.
2. `approveRisk` is a recognized operation but **not permitted** to this supplier role: valid proposals are `DENIED`, not executed. Unknown tools are `REJECTED`. Human confirmation cannot grant missing permission.
3. `fixture.allowed()` uses trusted identity/current scope/private ownership metadata without releasing protected content. Check it before quota and again under the revocation guard before use.
4. `reserve()` is atomic. Track reservation separately from consumption: denial before the read releases the unused unit in `finally`; a started read is charged even if it fails.
5. Content retrieval and mock effect share the guard with the final permission decision. Audit contains the decision enum, not model arguments, tokens, content or raw exception text.

**Expected checks:** cases 13–22 plus 34–37. Valid own read allows; foreign/unknown resource and NoScope deny; approval denies; unknown tool/string ID/extra key/malformed schema reject; exhausted quota stops. Denials/rejections produce no protected reads or effects. Revoke-before-use refunds unused quota; a started failed read maps safely to `FAILED` and is not refunded.

### B2: Alternate surfaces — protect every path

Compare [exercise Surfaces](../exercise/src/training/operations/Surfaces.java) with [fixed Surfaces](../fixed/src/training/operations/Surfaces.java). `preview`, `download` and `storage` already delegate to `read`, so repairing that common gate covers those adapters without duplicating policy.

**Direct read:**

```diff
	public Content read(Identity who, int id) {
-        if (who == null) return Content.denied(401);
-        return retrieve(id);
+        synchronized (fixture.policyLock) {
+            if (who == null) return Content.denied(401);
+            if (!fixture.allowed(who, id)) return Content.denied(403);
+            return retrieve(id);
+        }
	}
```

**Explanation/check:** anonymous returns 401; foreign/missing/revoked scope returns 403 before content. Keep current permission and retrieval in one guarded operation. Own read still succeeds; cases 23–26/32 cover wrong-supplier paths and good paths.

**Export — authorize all before reading any:**

```diff
	public Content export(Identity who, List<Integer> ids) {
-        StringBuilder out = new StringBuilder();
-        for (int id : ids) out.append(read(who, id).body());
-        return new Content(200, out.toString());
+        synchronized (fixture.policyLock) {
+            if (who == null) return Content.denied(401);
+            if (ids == null || ids.isEmpty() || ids.size() > 10 || ids.stream().anyMatch(java.util.Objects::isNull))
+                return Content.denied(400);
+            // Authorise ALL entries before any intermediate content read.
+            for (int id : ids) if (!fixture.allowed(who, id)) return Content.denied(403);
+            StringBuilder out = new StringBuilder();
+            for (int id : ids) {
+                Content result = retrieve(id);
+                if (result.status() != 200) return Content.denied(result.status());
+                out.append(result.body());
+            }
+            return new Content(200, out.toString());
+        }
	}
```

**Explanation/check:** validate the batch, authorize every ID under the same guard, then retrieve. Do not read an allowed subset and later discard it when a foreign item is found. Case 27 expects mixed 101/102 to deny with zero reads; case 32 expects own 101/103 to work. Failure returns no partial response; reads already attempted on infrastructure failure are not erased.

**Cache — scope keys and authorize hits:**

```diff
	public Content cached(Identity who, int id, String name) {
-        if (cache.containsKey(name)) return new Content(200, cache.get(name));
-        Content result = read(who, id); cache.put(name, result.body()); return result;
+        synchronized (fixture.policyLock) {
+            if (who == null) return Content.denied(401);
+            if (!fixture.allowed(who, id)) return Content.denied(403);
+            String key = who.supplier() + ":" + id; // Never keyed solely by display name.
+            if (cache.containsKey(key)) return new Content(200, cache.get(key));
+            Content result = retrieve(id);
+            if (result.status() == 200) cache.put(key, result.body());
+            return result;
+        }
	}
```

**Explanation/check:** display name is not a unique supplier/resource identifier. Use trusted supplier plus ID, and check current permission before even a cache hit. Cache only successful content. Case 28 checks Bob's same-name prime cannot reach Alice; case 29 checks revocation blocks a hit. This local cache has no distributed invalidation or production retention guarantee.

**Jobs — enqueue permission is not permanent:**

```diff
-    public Job enqueue(Identity who, int id) { return fixture.allowed(who, id) ? new Job(who, id) : null; }
+    public Job enqueue(Identity who, int id) {
+        synchronized (fixture.policyLock) { return fixture.allowed(who, id) ? new Job(who, id) : null; }
+    }
	public Content run(Job job) {
-        if (job == null) return Content.denied(403);
-        return retrieve(job.documentId()); // Trusts stale enqueue decision.
+        return job == null ? Content.denied(403) : read(job.requester(), job.documentId());
	}
-    public Content deliver(Job job, Content completed) { return completed; }
+    public Content deliver(Job job, Content completed) {
+        synchronized (fixture.policyLock) {
+            if (job == null || !fixture.allowed(job.requester(), job.documentId())) return Content.denied(403);
+            return completed;
+        }
+    }
```

**Explanation/check:** trusted jobs retain requester/resource context, but permission is checked again at execution and delivery. Case 30 expects revoke-before-run to read nothing; case 31 expects revoke-before-delivery to disclose no completed content and perform no additional read. This is a local typed fixture, not durable/signed worker payload validation or real network delivery.

### B3a: Atomic quota — choose the existing safe API

The [shared Budget](../common/src/training/operations/Budget.java) deliberately contains both unsafe and safe functions. **Do not replace or delete the shared unsafe function:** it is needed to expose the exercise race. The real application-track change is the broker switch from `reserveUnsafe()` to `reserve()` shown above.

For comparison, this conceptual method-body diff contrasts the two existing algorithms, not a patch to Budget:

```diff
-        int snapshot = unsafeRemaining;
-        unsafeAfterSnapshot.run();
-        if (snapshot <= 0) return false;
-        unsafeRemaining = snapshot - 1; return true;
+        int current;
+        do {
+            current = remaining.get();
+            if (current <= 0) return false;
+        } while (!remaining.compareAndSet(current, current - 1));
+        return true;
```

**Explanation/check:** compare-and-set makes “check positive and decrement” one atomic reservation. A competing update retries instead of allowing both callers to spend the same unit. The two teaching algorithms use separate counters; `remaining()` reports the safe counter. Case 35 coordinates two callers with barriers and bounded waits: one allows, one stops, one read/effect. `release()` is called once for an unused reservation by the broker's `finally`, not for every failed/denied outcome indiscriminately.

### B3b: Guarded read — recheck and use under revocation's lock

```diff
	public Content guardedRead(Identity who, int id) {
	    if (!fixture.allowed(who, id)) return Content.denied(403);
-        afterInitialCheck.run();
-        return retrieve(id); // Check/use race, no recheck or shared guard.
+        afterInitialCheck.run(); // Test seam outside lock: revoke can win before linearisation.
+        synchronized (fixture.policyLock) {
+            if (!fixture.allowed(who, id)) return Content.denied(403);
+            return retrieve(id); // Current policy + materialisation inside the same revocation lock.
+        }
	}
```

**Explanation/check:** the first check is not enough. The hook intentionally permits revocation to win before the final guarded operation; then the final check denies with zero reads. Hold the guard through retrieval, not just through the check. Cases 33/36 test the denial and guard; broker case 34 additionally tests refund. The unsafe broker has no initial resource check at all, so its full diff above adds one rather than assuming it existed. A read that linearizes before revocation can complete; later revocation cannot retract bytes already delivered.

### B3c: Retry example — illustrative design diff, not Java implementation

There is no existing progress-update feature to patch. The following **pseudocode** contrasts unsafe and proposed processing. Names such as `transaction` and `operations` are illustrative, not APIs supplied by this repository.

```diff
-onProgress(request):
-    progress[request.jobId] += 1
-    return progress[request.jobId]
+onProgress(request):
+    validate request and resolve the intended job/action
+    key = (jobId, action, stableOperationId)
+    fingerprint = canonicalArguments(request)
+    atomically in one transaction:
+        existing = operations.lookupForUpdate(key)
+        if existing exists:
+            if existing.fingerprint != fingerprint: reject conflict
+            return existing.completedResult
+        reserve key with a uniqueness constraint
+        progress[jobId] += 1
+        result = progress[jobId]
+        operations.storeCompleted(key, fingerprint, result)
+    return result after commit
```

**Explanation/check:** stable operation identity is separate from argument fingerprint. A duplicate with identical arguments returns the recorded result; different arguments with the same ID conflict. The effect and ledger commit together; otherwise crash/retry can reapply the increment. Define concurrency-conflict retry, in-progress recovery and retention/expiry policy. This assumes the count and ledger share a transactional store; external effects need additional coordination. For this harmless example no protected permission is involved; if adapted to protected work, add trusted principal/resource/action scope and replay authorization. Expected design checks: sequential/concurrent duplicates increment once, conflicting arguments reject, lost-response retry returns the original result, and interrupted transactions recover without double application. **No implementation or execution claim; no exactly-once delivery claim.**

## Simple debrief / marking guide

Accept plain language. Ask the learner to explain **cause → consequence → fix → check**, not recite a label. Each of B1/B2/B3 is satisfactory when at least one clear example is correct and the submission completes the requested tables; stronger answers cover all cases and alternatives. This is formative feedback, not a new organisational pass threshold. Important corrections: SQL binding alone is not authorization; model output is not permission; revocation races are security relevant; expected checks are not executed results.