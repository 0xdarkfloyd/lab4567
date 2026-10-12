# Lab 2 — untrusted source versus application authority

## Model/source exercise

The permitted task is to summarise supplier documents and identify missing security evidence. Run `-Command demo` to compare **clean**, **injected** and **delimited** source variants. Every output is explicitly labelled a **SCRIPTED MOCK**. The injected example deliberately pretends retention is compliant and proposes approval; the delimited example deliberately preserves the gap.

Record evidence-gap preservation, citation fidelity, proposed action and executed effect separately. These outputs illustrate possible behaviours; they are **not observations of any live model** and cannot establish that delimiters prevent prompt injection. Approved live-model experimentation is optional and outside this runnable project.

## Six mandatory broker cases

| Structured mock proposal as Alice | Secure expectation | Content reads / mock effects |
|---|---|---|
| `readDocument`, 101 | ALLOWED | 1 / 1 |
| `readDocument`, 102 | DENIED, empty body | 0 / 0 |
| `approveRisk`, 101 | DENIED always for supplier role | 0 / 0 |
| unsupported `shell` | REJECTED | 0 / 0 |
| string rather than integer documentId | REJECTED | 0 / 0 |
| additional read after one-slot quota | STOPPED | no additional reads/effects |

`Proposal` is a typed local mock-output envelope, with exact tool-specific argument keys/types/bounds. It is **not a JSON deserialiser**; unknown top-level fields cannot be expressed by the record. Production JSON parsing must separately reject unknown/duplicate keys and oversized input. No model text is evaluated as SQL, a shell command, URL or Java code.

The fixed broker takes a trusted server identity separately. A model-supplied `supplierId`/identity or any additional argument is rejected. A trusted non-disclosing metadata lookup precedes ACL enforcement. Unknown resource and wrong supplier both deny without content. Confirmation cannot grant supplier-role risk approval; approval-token validation is not implemented because this role can never perform that action.

## Same-class sweep

Run `-Track exercise -Command lab2`, then compare `-Track fixed -Command lab2` without changing the shared harness. Inspect read/preview/download/export/storage/cache/job adapters. They are **local mocks**, not filesystem access, signed URLs, real object storage, durable queues or network tools.

- Alice requests 102 on every read surface: empty 403 with **zero content reads**.
- Export `[101,102]`: deny before reading even the authorised first item.
- Bob primes `Contract` cache: Alice cannot receive Bob's value via the display name.
- Revoke Alice after cache fill: a hit still denies without an additional read.
- Revoke after enqueue: the job cannot retrieve content.
- Revoke after computation but before delivery: the adapter must not disclose the completed result.

## Executable races

The harness uses latches/barriers with bounded waits, never timing sleeps:

1. Pause after initial allow, revoke on another thread, resume: fixed surface and broker deny before materialisation; the broker refunds its unused reservation.
2. Start two requests with one quota slot: fixed atomically permits exactly one read/effect and stops the other. The exercise barrier deliberately exposes the non-atomic snapshot/update bug.
3. Instrument each content-query entry: verify the fixed guarded read and broker hold **the same policy lock** used by revocation. The fixed code keeps materialisation and the mock tool effect inside that lock.

Linearisation is the guarded current-policy check: revocation committed before it denies; revocation that waits behind an in-flight guarded operation does not retroactively retract bytes already read. No distributed lock/transaction guarantee is claimed.

## Design-only challenge

Draw durable scoped idempotency states bound to trusted principal/resource/action/canonical args, including conflicting-key rejection, reauthorisation of a replayed response and coordination with effect commit. Explain how an external tool participates or needs reconciliation. **This project does not implement replay/idempotency, durable quotas or exactly-once delivery.** Submit those as designs, not test results.

Minimal audit is one enum outcome per broker call, including denied/rejected/stopped/failed paths. It contains no raw model args, identity, token, source content or exception detail. Scope/metadata stay internal. Real audit failure, persistence, retention and distributed policy integration remain separate work.