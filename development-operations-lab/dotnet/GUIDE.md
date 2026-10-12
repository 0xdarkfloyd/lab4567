# Student guide

1. Read [README.md](README.md); arrange an approved .NET 8+ SDK/runtime separately. Never deploy exercise code. Use only synthetic evidence.
2. Run the exercise demo, then `lab1` with the documented launcher. First runs need explicit `-Restore`. Preserve the FAIL/nonzero findings.
3. Trace [exercise/src/Search.cs](exercise/src/Search.cs): authentication is not supplier authorization; concatenation changes SQL grammar; entity serialization and header logs leak unnecessary data. Predict cases 01–12 before editing.
4. Fix only the exercise source if practicing. Bind both trusted supplier and exact name, validate before database work, project id/name, and capture only an accepted-event label. Keep the shared harness unchanged.
5. Run `lab2`. Trace [exercise/src/Surfaces.cs](exercise/src/Surfaces.cs) from each disclosure adapter to `Materialize`. Do not fix only the visible search path. Explain why a cache hit and a completed job still need current policy checks.
6. Review [exercise/src/Broker.cs](exercise/src/Broker.cs): outputs are proposals, not authority. Check exact dictionary schema, int type/range, tool policy and metadata before spending quota or reading content. Unknown tools must never fall through.
7. Inspect races 33–36 in [Common/Harness.cs](Common/Harness.cs). Explain where revocation linearizes and why a check followed by unlocked use is insufficient. Distinguish reserved-but-unused quota (refund) from started-but-failed reads (charge).
8. Rerun all 37 tests with both projects. Compare observed counters and minimal audit, not just status labels. Consult [SOLUTIONS.md](SOLUTIONS.md) only after recording findings.

The demo's clean/injected/delimited outputs are scripted mocks. Delimiters alone do not establish model reliability or authorization. There is no live model/network tool execution, storage backend, worker service or transport in this lab.