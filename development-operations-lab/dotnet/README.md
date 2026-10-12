# Development and security operations lab — .NET / C#

Two console applications target **net8.0 / C# 12**: intentionally vulnerable **exercise** and reference **fixed**. Both compile the same Common fixture and independent 37-case harness, plus exactly one implementation set. No ASP.NET server, HTTP requests, JWT validation, live model or external tool execution is involved. Numeric statuses are console response contracts.

## Prerequisites and commands

An approved **.NET SDK 8 or newer** must be selected in this directory. A newer SDK may also need the .NET 8 runtime to execute this target. This repository does not install SDKs, elevate privileges or change machine policy. The available SDK at authoring time was **2.1.526**, so compilation and runtime tests have **not** been performed. Do not interpret the fixed expectations below as observed results.

From this directory, run `powershell -File .\lab.ps1 -Track exercise -Command test -Restore` or `powershell -File .\lab.ps1 -Track fixed -Command test -Restore` for the first run of each track. `-Restore` explicitly permits HTTPS NuGet package restore, including transitive/native SQLite dependencies. The launcher checks the SDK before attempting restore and uses the local NuGet configuration (only nuget.org).

Subsequent examples: `powershell -File .\lab.ps1 -Track fixed -Command test`, `powershell -File .\lab.ps1 -Track exercise -Command lab1`, `powershell -File .\lab.ps1 -Track fixed -Command lab2`, and `powershell -File .\lab.ps1 -Track fixed -Command demo`. Add `-Restore` only when package assets need restoring. All runs use `dotnet run --no-restore`; no implicit package restore. A build may contact SDK infrastructure if required targeting packs are missing; provisioning an appropriate SDK/runtime is a prerequisite, not part of the lab.

Exit codes: **0** all selected tests pass (or demo completes); **1** security-oracle failure; **2** invalid command/prerequisite. Exercise findings remain FAIL/nonzero, not inverted successes. Infrastructure/compiler failures also propagate nonzero. Demo only reports observations and is not a test substitute.

Dependency: **Microsoft.Data.Sqlite 8.0.11**, pinned in both project files. This is an intentional reproducible training dependency, not a statement that this older pin is currently vulnerability-free. Evaluate its current advisory status before using elsewhere. Transitive versions are resolved by NuGet, not a committed lockfile; package-signature/native-library trust and offline reproducibility are not independently certified here.

## Fixture and parity

| ID | Supplier | Exact name | Synthetic content |
|---|---|---|---|
| 101 | SUP-A | Contract | SUP-A contract: retention evidence missing. |
| 102 | SUP-B | Contract | SUP-B contract: private supplier evidence. |
| 103 | SUP-A | O'Reilly Guide | SUP-A guide: synthetic reference. |

Exact tokens map to trusted fixture identities: ALICE-DEMO → alice/SUP-A; BOB-DEMO → bob/SUP-B; NOSCOPE-DEMO → noscope/no supplier. Authentication requires the exact `Bearer ` prefix. These are synthetic tokens, **not credentials or JWTs**. Internal `Identity` values are obtained from that lookup, never from proposal arguments; the in-process types are not a public authentication API.

The SQLite anchor uses a private `:memory:` database and remains open for the fixture lifetime. `IDisposable` closes it idempotently. All connection use/disposal is serialized under a separate database lock. The fixed path acquires policy → database, never the reverse, and holds the shared revocation lock across current authorization, content materialization and mock completion.

Search retains the Java vulnerable query syntax: concatenated `SELECT * ... Name = '... ' ORDER BY Id` (without adding escaping). The actual SQL-control input is `' OR '1'='1`, not a substituted attack string. In fixed, that input is a bound literal with an **empty result**, while O'Reilly Guide returns 103. SQLite BINARY text equality preserves case/trailing-space exact matching. Fixed projects tenant and name predicates, id/name DTO only, two-second command timeout and LIMIT 100. Timeouts are provider controls, not a hard cancellation or full availability guarantee.

SQLite replaces Java's H2; database dialect/provider internals are not identical. C# `Search.Find` corresponds to Java `search` (a method cannot share its class name in C#). C# uses UTF-16 length and `IsNullOrWhiteSpace`; obscure Unicode whitespace behavior may differ from Java `isBlank`. Export uses typed `IReadOnlyList<int>`: null list/empty/oversize are validated, but null elements cannot be represented, unlike Java's boxed list. The 37 existing oracles remain equivalent.

## Expected cases (not executed here)

The harness creates a new fixture for each test, shares literal expectations across tracks, and prints only safe assertions or generic infrastructure failures.

| Cases | Contract |
|---|---|
| 01–03 | Alice Contract → 101; Bob Contract → 102; apostrophe name → 103 |
| 04–07 | Anonymous 401; NoScope 403; SQL-control literal → zero rows; length 101 → 400; denied/invalid inputs do not query |
| 08–12 | No token/content/raw-argument sinks; blank/null 400; id/name DTO; exact case/space and length-100 boundary; unknown token 401 |
| 13–18 | Six original broker outcomes: ALLOWED, DENIED cross-supplier, DENIED approveRisk, REJECTED unknown tool, REJECTED string ID, STOPPED exhausted quota |
| 19–22 | Reject identity/extra keys and malformed envelope/type/range; unknown resource non-disclosing denial; anonymous/NoScope denied without effects |
| 23–27 | Read/preview/download/storage wrong supplier → empty 403 without read; export authorizes all before any read |
| 28–31 | Tenant/resource cache separation; cache revocation; job execution and delivery revocation checks |
| 32 | Authorized surface sweep and anonymous 401 |
| 33–34 | Deterministic revocation races deny before content; broker refunds unused reservation and audits DENIED |
| 35 | Concurrent one-slot quota permits exactly one ALLOWED, one STOPPED, one content read and one mock effect |
| 36 | Materialization occurs under the shared policy lock |
| 37 | Disposed database → empty FAILED, one started read, zero effects, minimal audit; failed started read stays charged |

Expected fixed: **37/37** (`lab1`: 12, `lab2`: 25). Exercise intentionally fails security cases; no observed pass/fail count is claimed. Race tests use bounded events/barriers and dedicated Tasks, not sleeps, and drain workers before fixture cleanup. Fixed quota uses CAS; exercise's snapshot hook deliberately exposes a lost update. The policy lock is in-process only: no distributed transactions, durable quota, cancellation guarantees or post-delivery revocation of already-disclosed content.

## Source and teaching material

- [Common/Fixture.cs](Common/Fixture.cs), [Common/Contract.cs](Common/Contract.cs), [Common/Budget.cs](Common/Budget.cs): trusted metadata, real SQL, counters and one-unit quota.
- [Common/Harness.cs](Common/Harness.cs): 37 independent oracles.
- [Common/Program.cs](Common/Program.cs), [Common/MockModel.cs](Common/MockModel.cs): CLI and explicitly scripted clean/injected/delimited examples plus six broker demos.
- [exercise/src/Search.cs](exercise/src/Search.cs), [exercise/src/Surfaces.cs](exercise/src/Surfaces.cs), [exercise/src/Broker.cs](exercise/src/Broker.cs): intentionally unsafe starting points.
- [fixed/src/Search.cs](fixed/src/Search.cs), [fixed/src/Surfaces.cs](fixed/src/Surfaces.cs), [fixed/src/Broker.cs](fixed/src/Broker.cs): reference implementations.
- [GUIDE.md](GUIDE.md), [SOLUTIONS.md](SOLUTIONS.md), [COPILOT-PROMPTS.md](COPILOT-PROMPTS.md), [VALIDATION.md](VALIDATION.md).