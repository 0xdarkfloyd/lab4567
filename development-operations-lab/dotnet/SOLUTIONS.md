# Instructor reference and fix rationale

The fixed implementation is a reference, not production certification. Expected 37/37 is **unverified until compiled and run with suitable tooling**. Do not weaken, delete or invert tests to hide exercise findings.

## Lab 1: exact supplier-scoped search

[fixed/src/Search.cs](fixed/src/Search.cs) authenticates from the trusted fixture lookup, then checks current supplier scope under the policy lock. Anonymous is 401; recognized no-scope is 403. Null/whitespace or length >100 is 400 before submitting SQL. The SQL binds supplier and exact name separately and returns only id/name, capped by LIMIT 100. Do not “fix” apostrophes with manual replacements or switch to LIKE/case-insensitive matching. The unmatched SQL-control literal must be 200 with no rows, not 400 or 500. Safe logs contain only `search:accepted`.

## Lab 2: all disclosure surfaces

[fixed/src/Surfaces.cs](fixed/src/Surfaces.cs) rechecks policy before read/preview/download/storage and cache hits. Cache keys include supplier and resource ID, not a display name. Export authorizes **every** resource before **any** content query. Job enqueue is not lasting permission: execution and delivery each check current authorization. Revoked/foreign/missing resources produce empty 403 without extra materialization.

`GuardedRead` deliberately exposes a seam after the initial check. The final policy recheck and content query share `PolicyLock` with revocation. Database serialization alone protects SQLite connection safety, not policy correctness. The low-level fixture deliberately lacks ACL enforcement so unsafe adapters are observable; it must not be exposed as a production repository.

## Broker and budget

[fixed/src/Broker.cs](fixed/src/Broker.cs) validates nonnull proposal/tool/arguments, exact two-tool allowlist, exactly the ordinal key `documentId`, boxed `int` only, and range 1–999999. Long/string IDs and extra model-supplied identity fields are rejected. The requester comes from trusted authentication; private ownership metadata makes cross-supplier and nonexistent IDs indistinguishable before any content read. `approveRisk` is always denied for this supplier role, not granted by model output or human approval alone.

[Common/Budget.cs](Common/Budget.cs) atomically reserves with CompareExchange. After reservation, the broker rechecks under the policy lock and holds it through query and local mock-effect increment. A revoked, unused reservation is released; once materialization starts, even a database failure costs a unit. `finally` emits exactly an enum audit event. FAILED responses never print exceptions, SQL, bindings, tokens, contents or connection strings. Exercise can report safe failures while still being insecure: error handling is not authorization.

## Counters and races

- `SearchQueries`: submitted searches, including exercise syntax failure.
- `ContentReads`: started materializations, including the deliberately disposed-database failure.
- `ToolEffects`: successful local mock completions, never external side effects.
- Cases 33/34 use bounded ManualResetEventSlim seams to revoke deterministically.
- Case 35 starts two Tasks together and exposes exercise's unsafe snapshot/update with a bounded Barrier. Fixed calls do not invoke that unsafe hook.
- Case 36 observes Monitor ownership at the materialization seam; source review verifies mock completion stays inside the same guard.
- Case 37 closes the actual in-memory anchor; the first failed read is charged and audited, the second call stops before reading.

These locks/counters are local teaching constructs. Production requires transport authentication, durable/distributed policy and quota, transactional side-effect handling, audit retention, dependency review and appropriately scoped data access.