# C# beginner solutions — B1–B3 results, fix diffs and explanations

**Instructor/debrief reference.** Attempt the shared [B1–B3 exercises](../docs/BEGINNER-EXERCISES.md) first. These are the C# equivalents of the [Java review and fixes](../solutions/BEGINNER-SOLUTIONS.md). See [C# review capture](demo.md) for prompts, results and integrity logs.

The diffs compare existing exercise/fixed code; **no source changes are applied**. They are focused review excerpts, not standalone patches; linked fixed files provide complete context/imports. **No build, restore or test run occurred.** All checks below are expected, not observed PASS results. C# targets net8.0/C# 12 and needs approved SDK 8+ and appropriate runtime; the recorded SDK 2.1.526 cannot compile it. Java's historical results do not validate C#.

## B1: Search — parameters, scope, validation, DTO and logging

Compare [exercise Search](exercise/src/Search.cs) with [fixed Search](fixed/src/Search.cs). `Authenticate()` and anonymous 401 remain. The complete fixed method wraps scope validation and database work in `lock (fixture.PolicyLock)` and retains safe 500 handling.

```diff
-        fixture.Logs.Enqueue("Auth=" + authorization);
-        // Preserve Java's null-as-literal behavior and actual quote/control syntax.
-        string sql = "SELECT * FROM Documents WHERE Name = '" + (name ?? "null") + "' ORDER BY Id";
-        Interlocked.Increment(ref fixture.SearchQueries);
+        lock (fixture.PolicyLock)
+        {
+            if (!fixture.HasScope(identity)) return Response.Empty(403);
+            if (string.IsNullOrWhiteSpace(name) || name.Length > 100) return Response.Empty(400);
+            fixture.Logs.Enqueue("search:accepted");
```

Inside `WithDatabase()` the query/response changes are:

```diff
                 using var command = connection.CreateCommand();
-                command.CommandText = sql;
+                command.CommandTimeout = 2;
+                command.CommandText = "SELECT Id, Name FROM Documents WHERE SupplierId = $supplier AND Name = $name ORDER BY Id LIMIT 100";
+                command.Parameters.AddWithValue("$supplier", identity.Supplier!);
+                command.Parameters.AddWithValue("$name", name);
+                Interlocked.Increment(ref fixture.SearchQueries);
                 using var reader = command.ExecuteReader();
-                return new Response(200, Fixture.Rows(reader, true));
+                return new Response(200, Fixture.Rows(reader, false));
```

**Explanation:** named SQLite parameters keep values separate from SQL structure. Supplier filtering is a separate authorization control using trusted identity, not supplied `supplierId`. Scope and invalid input deny before DB work. Do not trim/case-normalize valid names: fixture matching is exact. `Rows(reader, false)` plus minimal SQL returns only id/name; the event contains no credential. `LIMIT 100` and timeout bound work. Null-forgiving `!` does not authorize anything; the earlier scope check establishes the supplier invariant.

**Expected cases 01–12:** Alice Contract →101 only; Bob →102 only; apostrophe name →103; anonymous/unknown token →401 without query; NoScope/revoked scope →403 before DB; blank/null/length101 →400; length100 accepted; SQL-control input treated literally; only id/name; no token/content/raw arguments in logs. Search `Contract ` and lowercase `contract` must not become `Contract` through canonicalization.

## B1/B2/B3: Broker — strict schema, independent policy, quota and guard

Compare [exercise Broker](exercise/src/Broker.cs) with [fixed Broker](fixed/src/Broker.cs). Entire `Call()` body transition:

```diff
-        var decision = Decision.FAILED;
+        var decision = Decision.REJECTED;
+        bool reserved = false, consumed = false;
         try
         {
-            if (requester is null) { decision = Decision.DENIED; return new(decision, ""); }
-            if (!budget.ReserveUnsafe()) { decision = Decision.STOPPED; return new(decision, ""); }
+            if (proposal?.Tool is null || proposal.Arguments is null) return new(decision, "");
+            if (proposal.Tool != "readDocument" && proposal.Tool != "approveRisk") return new(decision, "");
+            // Enumerate exact key spelling too: do not trust a caller's dictionary comparer.
+            if (proposal.Arguments.Count != 1 || !proposal.Arguments.Keys.Single().Equals("documentId", StringComparison.Ordinal))
+                return new(decision, "");
+            if (proposal.Arguments["documentId"] is not int id || id < 1 || id > 999999) return new(decision, "");
+            decision = Decision.DENIED;
+            if (requester is null || proposal.Tool == "approveRisk" || !fixture.Allowed(requester, id)) return new(decision, "");
+            if (!budget.Reserve()) { decision = Decision.STOPPED; return new(decision, ""); }
+            reserved = true;
             AfterInitialCheck();
-            object raw = proposal!.Arguments!.GetValueOrDefault("documentId", 101);
-            int id = raw is int number ? number : 101;
-            string body = "";
-            // Match Java null tool failure, rather than silently accepting null.
-            if (proposal.Tool!.Equals("readDocument", StringComparison.Ordinal)) body = fixture.Materialize(id);
-            Interlocked.Increment(ref fixture.ToolEffects);
-            decision = Decision.ALLOWED;
-            return new(decision, body);
+            lock (fixture.PolicyLock)
+            {
+                if (!fixture.Allowed(requester, id)) return new(decision, "");
+                consumed = true; // A started failed read still consumes its slot.
+                string body = fixture.Materialize(id);
+                Interlocked.Increment(ref fixture.ToolEffects);
+                decision = Decision.ALLOWED;
+                return new(decision, body);
+            }
         }
-        catch (Exception) { return new(decision, ""); }
-        finally { fixture.RecordAudit(decision); }
+        catch (Exception) { decision = Decision.FAILED; return new(decision, ""); }
+        finally
+        {
+            if (reserved && !consumed) budget.Release();
+            fixture.RecordAudit(decision); // Enum only, never tokens, raw args or content.
+        }
```

**Explanation:**

- No default ID for malformed arguments. Require only `documentId`, exactly spelled ordinally, boxed `int` in 1..999999. A caller's case-insensitive dictionary comparer must not weaken the schema; strings/longs/extra identity claims reject.
- Unknown tools/schema →`REJECTED`. Valid but forbidden approval/foreign resource →`DENIED`. `approveRisk` is known but never permitted for this supplier role. Model proposals and human confirmation cannot grant permission.
- `Allowed()` checks trusted identity/current scope/private ownership metadata. It is a non-disclosing policy lookup, not content retrieval.
- Use atomic `Reserve()`, then recheck permission under `PolicyLock` through content/effect. The unsafe broker never had a resource check; `AfterInitialCheck` followed only requester/quota checks.
- Refund only reserved-but-unused quota. Mark consumed immediately before starting content read; failures after entry remain charged. Decision-only audit avoids raw exception/data leakage. The broad catch is fixture behavior, not production exception-handling certification.

**Expected cases 13–22, 34–37:** own read allows; foreign/unknown resource/NoScope deny; approval denies; unknown tools/bad types/extra fields/malformed schema reject; exhausted quota stops. No forbidden reads/effects. Case20 includes exact key spelling with a case-insensitive dictionary. Revoke-before-use denies/refunds; started DB failure is safe, charged and audited.

## B2: Surfaces — same policy on every disclosure path

Compare [exercise Surfaces](exercise/src/Surfaces.cs) with [fixed Surfaces](fixed/src/Surfaces.cs). `Preview`, `Download` and `Storage` delegate to `Read`.

### Read

```diff
-    public Content Read(Identity? who, int id) => who is null ? Content.Denied(401) : Retrieve(id);
+    public Content Read(Identity? who, int id)
+    {
+        lock (fixture.PolicyLock)
+        {
+            if (who is null) return Content.Denied(401);
+            if (!fixture.Allowed(who, id)) return Content.Denied(403);
+            return Retrieve(id);
+        }
+    }
```

**Why/check:** a non-null identity is not permission. Check/use shares revocation's guard. Cases23–26 deny wrong-supplier paths with no read; case32 preserves own/anonymous behavior.

### Export: all-before-any

```diff
     public Content Export(Identity? who, IReadOnlyList<int>? ids)
     {
-        var output = new StringBuilder();
-        foreach (int id in ids!) output.Append(Read(who, id).Body);
-        return new(200, output.ToString());
+        lock (fixture.PolicyLock)
+        {
+            if (who is null) return Content.Denied(401);
+            if (ids is null || ids.Count == 0 || ids.Count > 10) return Content.Denied(400);
+            foreach (int id in ids) if (!fixture.Allowed(who, id)) return Content.Denied(403);
+            var output = new StringBuilder();
+            foreach (int id in ids)
+            {
+                var result = Retrieve(id);
+                if (result.Status != 200) return Content.Denied(result.Status);
+                output.Append(result.Body);
+            }
+            return new(200, output.ToString());
+        }
     }
```

**Why/check:** batch shape and every item's policy precede any content, under one guard. Case27 mixed101/102 →403 with zero reads; own101/103 works in case32. Infrastructure failure returns no partial response, but cannot erase reads already attempted. Typed int elements differ from Java's nullable boxed list elements.

### Cache: trusted supplier/resource key and current-policy hits

```diff
     public Content Cached(Identity? who, int id, string name)
     {
-        if (cache.TryGetValue(name, out var body)) return new(200, body);
-        var result = Read(who, id);
-        cache[name] = result.Body;
-        return result;
+        lock (fixture.PolicyLock)
+        {
+            if (who is null) return Content.Denied(401);
+            if (!fixture.Allowed(who, id)) return Content.Denied(403);
+            string key = who.Supplier + ":" + id;
+            if (cache.TryGetValue(key, out var body)) return new(200, body);
+            var result = Retrieve(id);
+            if (result.Status == 200) cache[key] = result.Body;
+            return result;
+        }
     }
```

**Why/check:** two suppliers can use the same display name; it is not an authority/cache key. Check policy before hits; store only successful results. Cases28/29 test same-name tenant separation and revocation. This is a local cache, not distributed invalidation.

### Jobs: execution and delivery are fresh decisions

```diff
-    public Job? Enqueue(Identity? who, int id) => fixture.Allowed(who, id) ? new(who!, id) : null;
-    public Content Run(Job? job) => job is null ? Content.Denied(403) : Retrieve(job.DocumentId);
-    public Content Deliver(Job? job, Content completed) => completed;
+    public Job? Enqueue(Identity? who, int id)
+    {
+        lock (fixture.PolicyLock) return fixture.Allowed(who, id) ? new(who!, id) : null;
+    }
+    public Content Run(Job? job) => job is null ? Content.Denied(403) : Read(job.Requester, job.DocumentId);
+    public Content Deliver(Job? job, Content completed)
+    {
+        lock (fixture.PolicyLock)
+            return job is null || !fixture.Allowed(job.Requester, job.DocumentId) ? Content.Denied(403) : completed;
+    }
```

**Why/check:** trusted local job metadata is not perpetual authorization. Case30 revoke-before-execution reads nothing; case31 revoke-before-delivery returns no completed content and adds no read. This does not implement durable worker payload validation or real network delivery.

## B3a: Atomic last-slot reservation

The broker diff switches `ReserveUnsafe()` to `Reserve()`. Preserve both functions in [shared Budget](Common/Budget.cs) for teaching/testing. This conceptual body diff compares existing algorithms, **not a patch to shared code**:

```diff
-        int snapshot = unsafeRemaining;
-        UnsafeAfterSnapshot();
-        if (snapshot <= 0) return false;
-        unsafeRemaining = snapshot - 1;
-        return true;
+        int current;
+        do
+        {
+            current = Volatile.Read(ref remaining);
+            if (current <= 0) return false;
+        } while (Interlocked.CompareExchange(ref remaining, current - 1, current) != current);
+        return true;
```

**Timeline:** A snapshots1 → B snapshots1 → A writes0/true → B writes0/true. Both may proceed. CAS checks/decrements atomically and retries competing updates. Safe/unsafe counters are separate; `Remaining` is the safe counter. `Release()` uses `Interlocked.Increment`, called once only for unused reservation. Reliability/resource control can also be security relevant; this is no monetary/distributed guarantee. Case35 uses `Barrier` with bounded waits: one `ALLOWED`, one `STOPPED`, one read/effect.

## B3b: Revoke-before-use guard

```diff
     public Content GuardedRead(Identity? who, int id)
     {
         if (!fixture.Allowed(who, id)) return Content.Denied(403);
-        AfterInitialCheck();
-        return Retrieve(id);
+        AfterInitialCheck(); // Outside lock so deterministic revocation can win.
+        lock (fixture.PolicyLock)
+        {
+            if (!fixture.Allowed(who, id)) return Content.Denied(403);
+            return Retrieve(id);
+        }
     }
```

**Timeline:** allow → pause → revoke → resume. The final check and read share revocation's guard; a second unlocked check is insufficient. Cases33/34 use `ManualResetEventSlim` to pause/resume with bounded waits; expect empty denial, zero content/effects, unused broker quota refund. Case36 uses `Monitor.IsEntered(PolicyLock)` at content entry. Case37 checks failure charging. Revocation after a completed guarded read cannot retract delivered bytes.

**C# lock distinction:** `PolicyLock` protects current authorization/revocation; `WithDatabase()`'s private `dbLock` serializes the non-thread-safe SQLite connection. `dbLock` is not authorization. Fixed callers acquire policy → database; do not introduce inverse lock order. These are local-process guarantees only.

## B3c: Harmless progress retry — design-only C#-oriented pseudocode

No progress-update implementation exists. This is illustrative pseudocode, **not compilable C#**, and `store.Transaction`/ledger operations are not repository APIs.

```diff
-HandleProgress(request):
-    counts[request.JobId] += 1
-    return counts[request.JobId]
+HandleProgress(request):
+    validate request and resolve intended job/action
+    key = (jobId, action, stableOperationId)
+    fingerprint = canonicalArguments(request)
+    return store.Transaction(() => {
+        existing = ledger.LookupForUpdate(key)
+        if existing exists:
+            if existing.Fingerprint != fingerprint: reject conflict
+            return existing.CompletedResult
+        ledger.ReserveUnique(key)
+        counts[jobId] += 1
+        result = counts[jobId]
+        ledger.StoreCompleted(key, fingerprint, result)
+        return result
+    }) after successful commit
```

Stable ID and arguments fingerprint are separate. Same ID/same arguments replays result; same ID/different arguments conflicts. Increment and ledger commit atomically in one transactional store; define uniqueness-conflict retry, in-progress/crash recovery and retention expiry. An in-memory dictionary or `ConcurrentDictionary` alone does not establish durable transactional effect/ledger coordination. External effects need further coordination/reconciliation. Protected adaptations need trusted principal/resource/action scope and replay authorization. Expected design checks: sequential/concurrent duplicate increments once, conflicts reject, lost-reply retry returns saved result, crash/recovery avoids duplicate committed effect. **Not implemented/executed; no exactly-once delivery claim.**

## Expected checks and evidence

The [C# Harness](Common/Harness.cs) is the independent oracle: cases01–12 search; 13–22 broker; 23–32 surfaces/cache/jobs; 33–37 races/guard/failure. Do not weaken assertions or replace trusted fixture identity with model/request identity. Documentation/source review is complete; compilation, restore, 37-case execution and real ASP.NET/JWT integration remain **not run/unverified**. Use [validation status](VALIDATION.md) for tooling limitations and the approved launcher only when prerequisites are available.