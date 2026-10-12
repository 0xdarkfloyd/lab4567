# Source walkthrough

The root build script compiles these common classes and **one** implementation set. Never compile exercise and fixed together.

| Shared source | Responsibility / evidence |
|---|---|
| [Contract.java](../common/src/training/operations/Contract.java) | Numeric response contract, internal trusted identity, jobs, structured mock proposals, safe outcomes |
| [Fixture.java](../common/src/training/operations/Fixture.java) | Exact fixture-token map outside payload; private H2 database; prepared seed inserts; trusted ownership metadata; shared revocation lock; JDBC content query and atomic read/effect counters |
| [Budget.java](../common/src/training/operations/Budget.java) | CAS reservation/refund for fixed; deliberately unsafe snapshot/update path for exercise; executable barrier hook |
| [Harness.java](../common/src/training/operations/Harness.java) | Independent hard-coded oracles, new fixture per test, security assertions and safe reporting; no track-specific expectation inversion |
| [MockModel.java](../common/src/training/operations/MockModel.java) | Explicitly scripted clean/injected/delimited sources and outputs |
| [Main.java](../common/src/training/operations/Main.java) | CLI, demo observations and six mock broker proposals; no server |

## Paired implementations

| Class | Exercise | Fixed |
|---|---|---|
| Search | [source](../exercise/src/training/operations/Search.java): `Statement`, concatenated `SELECT *`, auth-header log, unscoped/unbounded name | [source](../fixed/src/training/operations/Search.java): authentication then scope/bounds before query, `PreparedStatement`, tenant and name bindings, ID/name projection, safe log, query timeout/max rows |
| Surfaces | [source](../exercise/src/training/operations/Surfaces.java): authenticated-only retrieval, shared name cache, stale enqueue trust and unguarded check/use | [source](../fixed/src/training/operations/Surfaces.java): current ACL before reads/hits, all-resource export check, tenant/resource key, job execution/delivery recheck, lock enclosing current check and query |
| Broker | [source](../exercise/src/training/operations/Broker.java): permissive default arguments, unknown-tool fallback to mock effect, missing ACL, non-atomic quota | [source](../fixed/src/training/operations/Broker.java): exact envelope/argument constraints, two named tools with supplier-role approval denial, trusted identity/metadata, ACL before content, atomic quota, guarded recheck/effect, reservation settlement and enum audit |

Follow `Fixture.materialize()` from each adapter: the fixed caller's lock and current policy precede the actual JDBC content query. The low-level fixture method intentionally does not enforce ACL itself, so the exercise's missing enforcement is observable. It is not a production-safe general-purpose repository.

`searchQueries` counts submitted search queries, including the vulnerable syntax failure. `contentReads` counts started content materialisations (a failed database read still costs one). `toolEffects` counts successful local mock tool completions. A cache hit is still a disclosure path, so its denial is asserted by both empty response and unchanged read counter.

Unexpected SQL failures map to safe 500/FAILED outputs without stack traces, SQL, bindings or connection strings. Test failures print safe assertion messages, not raw exceptions. Dependency verification errors do not fall back to another source or an unverified artifact.