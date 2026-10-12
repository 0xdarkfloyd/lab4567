# Copilot prompts for this C# lab

Use these only inside this subtree. Keep Common security oracles unchanged. Review diffs, dependencies and observed output rather than trusting generated claims.

## Search review

“Review exercise/src/Search.cs against Common/Harness.cs cases 01–12. Identify trust boundaries, SQL grammar changes, scope, DTO and logging failures. Propose a minimal supplier/name-parameterized fix preserving exact matching and all numeric statuses. Do not edit other subtrees or change test expectations.”

## Disclosure-path review

“Trace every Surfaces content path, including export, cache hits, job execution and delivery. Before editing, list the current policy check and materialization points. Repair exercise only using the shared revocation lock. Explain why database connection locking is a separate concern.”

## Broker review

“Treat Proposal as untrusted structured mock output, not authority. Enforce an exact dictionary schema, boxed int/range, named-tool policy and trusted requester metadata. Deny approveRisk, reject unknown tools, authorize before content, atomically reserve quota and release only unused reservations. Keep audit enum-only. Never add an external tool or live model call.”

## Race and evidence review

“Explain cases 33–37 from actual code. Preserve bounded event/barrier coordination, the revocation linearization point and charged started failures. Do not replace races with sleeps or claim tests passed without executing them. Check the selected SDK first; do not install one or restore NuGet without explicit opt-in.”

## Report

“Summarize observed test output separately from expected results. Report SDK, dependency pin, command/track, total cases, failures, counters and remaining limitations. Keep raw tokens, SQL, content and exceptions out of captured logs. Restrict writes to development-operations-lab/dotnet.”