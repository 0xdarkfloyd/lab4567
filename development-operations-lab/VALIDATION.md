# Observed execution — 2026-10-12

Environment: Windows PowerShell; Oracle JDK **21.0.2**, compiled using `javac --release 17`. Java 17 bytecode/API targeting was exercised; execution on an actual JDK 17 installation was **not run**. H2 **2.3.232**, private in-memory relational fixture, no Maven/JUnit/Spring.

## Executed from parent workspace

- `& .\development-operations-lab\lab.ps1 -Track fixed -Command test -DownloadDependencies`: initial consented artifact download, pin verification and compilation. Initial test run exposed a fixed-export null-list check bug, which was repaired without changing oracles.
- `& .\development-operations-lab\lab.ps1 -Track fixed -Command test`: final **37/37 PASS, 0 FAIL, exit 0**, without download consent/network.
- `& .\development-operations-lab\lab.ps1 -Track exercise -Command test`: final **7 PASS, 30 FAIL, exit 1**. These are expected insecure-track findings; no failures were inverted.
- Both `-Track fixed -Command demo` and `-Track exercise -Command demo`: **exit 0**, observations only.
- Fixed `-Command lab1`: **12/12 PASS, exit 0**; fixed `-Command lab2`: **25/25 PASS, exit 0**. `-List` printed the supported tracks/commands without a build.

Final search observations: fixed `Contract` -> 101, apostrophe -> 103, exact control literal -> empty result; token-leakage boolean false. Exercise `Contract` -> 101/102, apostrophe -> safe 500, control literal -> 101/102/103; token-leakage boolean true. Fixed search fields only ID/name; exercise adds supplier, sensitive field and content.

Fixed six-case mock broker observations: ALLOWED / DENIED / DENIED / REJECTED / REJECTED / STOPPED. Each denial/rejection/stop caused zero content reads and zero mock effects. Exercise allowed cross-supplier, risky/unknown-tool and malformed proposals. No external tool ran. Clean/injected/delimited outputs were explicitly scripted mocks.

Executable tests passed for fixed export all-before-any checks, tenant/resource cache isolation, cache/job revocation, latch-controlled revoke-before-use, reservation refund, two-thread one-slot quota and shared-lock content materialisation. Real JDBC failure after closing the fixture was safely mapped and audited, with no mock effect.

Artifact SHA-256 rechecked after final runs:

**8dae62d22db8982c3dcb3826edb9c727c5d302063a67eef7d63d82de401f07d3**

Editor diagnostic collection in the parent workspace reported unresolved symbols on portions of the exercise source roots (automatic discovery of intentionally overlapping track classes). Both selected-track compiler invocations succeeded. Separate supplied workspace configurations select the proper roots; launching/reindexing those editor workspaces was not tested.

See [solutions/README.md](solutions/README.md) for the full limitations. HTTP middleware/JWT, production JSON parsing, actual JDK 17 runtime, external tools/storage, distributed policy/quota, audit outage and durable idempotency remain untested or designed-only.