# Fixed explanation, independent oracles and limits

## Lab 1 solution

The intended contract is supplier-scoped exact-name search. Authentication alone is insufficient. The fixture maps tokens to trusted identities outside the request payload; missing identity returns 401. The fixed scope guard returns 403 before a query, then validates null/blank/>100 names for 400. The actual JDBC statement selects ID/name and binds **supplier and name**, rather than concatenating a string. The apostrophe fixture succeeds and the exact control input `' OR '1'='1` remains an unmatched literal.

The exercise's appended closing quote completes an always-true comparison, so the real H2 query broadens to all three rows. Its unscoped `Contract` query yields both 101 and 102, its legitimate apostrophe name fails SQL parsing, and its output includes extra sensitive/entity fields. Those are observed failures of independent secure expectations, not successful security tests.

The fixed log is a constant event label; the captured sink test checks fixture-token/header/content/raw-argument markers. This proves only the tested local sinks, not all production telemetry.

## Lab 2 solution

`Broker.call()` validates a local structured envelope; it never evaluates a generated command. Tool names must be exactly `readDocument` or `approveRisk`, with exactly one integer `documentId` in bounds. No identity/owner field is accepted. The supplier role is always denied approval, without any confirmation override. A trusted internal ownership check does not disclose existence/metadata; both foreign and unknown IDs produce the same empty denial.

Permission precedes quota/content. The fixed quota uses CAS, allowing at most one caller to consume the last slot. Unused reservations are refunded after a recheck denial/failure before content; a started content read consumes one unit even if the database fails. The execution guard rechecks current ACL while holding the revocation lock through the SQL read and mock effect. Minimal enum audit runs for all broker outcomes.

The sweep fixes the same invariant on all local adapters. Export authorises the entire bounded list before any intermediate retrieval. Cache keys use tenant/resource, not display name, and every hit rechecks current permission. Jobs carry trusted requester/resource context and recheck at execution and delivery. Storage is a mediated local read adapter, not a signed URL implementation.

## Independent oracle inventory

The shared [Harness.java](../common/src/training/operations/Harness.java) never reads the track to alter an assertion. It uses explicit expected IDs/statuses/fields/counters rather than the production predicate. Track labels affect only reporting. Each test receives a new database/policy/cache/quota context; concurrency tests use deterministic barriers/latches.

| Tests | Contract |
|---|---|
| 01–08 | Eight original search cases: Alice/Bob IDs, apostrophe, Anonymous, NoScope, unmatched SQL literal, oversize, token-free captured sinks |
| 09–12 | Blank/null input, DTO fields, exact-case/100-character boundary, unknown-token denial |
| 13–18 | Six original broker cases, including reads/effects and minimal audit checks |
| 19–22 | Model identity/extra args, malformed schema matrix, unknown metadata, Anonymous/NoScope |
| 23–27 | Read/preview/download/storage cross-supplier denials; all-before-any export ACL |
| 28–31 | Name-cache isolation, revoked cache hit, job execution/delivery revocation |
| 32 | Positive surface controls and anonymous status |
| 33–36 | Surface/broker revoke-before-use, unused reservation refund, concurrent last-slot race, policy-lock materialisation instrumentation |
| 37 | Real database unavailable after fixture close: safe failure, zero effect, charged started read, minimal audit |

## Limits — do not overclaim

- This is Java 17-targeted local JDBC plus console response objects, **not** a Spring Boot API, HTTP routing/middleware, JWT verification or real authentication integration. No server is installed/listening.
- Proposal records are structured mocks, not a JSON parser; duplicate/unknown JSON keys, deserialisation limits and network input need their own real adapter/tests.
- Scripted model examples are explicitly mocked, not live Copilot or model observations. Delimiters alone are not a security boundary.
- Tools, storage and job delivery are local effects. No network tool, object store, signed URL, durable queue, file download, risk record mutation or external target is exercised.
- Ownership is synthetic trusted metadata. There is no granular per-action ACL service; all permitted local read surfaces share supplier/current-scope policy. Trusted internal identities are not accepted from public payloads.
- The shared lock coordinates **this process only**. Guarded operations that already linearised can finish before revocation acquires the lock. A distributed service needs transactional/versioned policy coordination. Cached content may remain in memory after revocation but is not served while current policy denies; full invalidation/retention and policy versions are not implemented.
- Job delivery rechecks permission but does not prove revocable delivery after returned bytes leave the process. Job provenance is an internal fixture assumption, not durable signed worker context.
- Quota is a per-broker/run in-memory unit budget, not a durable per-principal global budget, concurrency limiter or monetary/cost ledger. No process-crash recovery is provided.
- Durable replay/idempotency, conflict detection and coordination with external effect commits are **design only**. No exactly-once, distributed rollback or lost-response retry guarantee is claimed.
- JDBC timeout/max rows are configured on fixed search; no disconnect/cancellation/timeout execution test, export byte ceiling, hostile runtime starvation or broader provider certification is supplied. H2 collation is not a production-provider guarantee.
- Audit is an in-memory enum list with no external sink/failure injection, persistence, access control or retention. Mandatory production audit outages must be designed to fail closed; this harness does not certify them.
- SHA-256 pins known bytes, not vulnerability status or a publisher signature. Runtime loading of a trusted dependency is distinct from the download script, which never executes a JAR.

Submit command/result/runtime evidence and remaining assumptions; do not call expected exercise FAILs green.