# Advanced route — Lab 1: independent review and relational proof

**Advanced A1.** For guided introductory questions use [beginner exercises B1](../BEGINNER-EXERCISES.md). Here, complete the full contract and evidence route below. See [route selection](../../README.md#choose-a-route-beginner-or-advanced). Execution requires the documented prerequisites; blocked source review is not a test pass.

## Contract first

Before reading implementation code, write expectations:

1. Alice's exact `Contract` search yields **101 only**; Bob's yields **102 only**.
2. Alice's `O'Reilly Guide` search yields **103**.
3. Anonymous/unrecognised fixture identity yields **401**, no database query.
4. Authenticated NoScope yields **403 before the database**.
5. The exact input `' OR '1'='1` is an **unmatched literal**, returning **200 with no rows**. The harness supplies exactly those characters, with no final closing quote/comment appended in the input. In the exercise SQL, the concatenated closing quote completes the injected comparison.
6. Null/blank names or names longer than 100 characters yield **400 before the database**; length 100 is permitted.
7. Search responses contain only **id/name**.
8. Tokens, raw arguments and content do not enter captured logs/audit.

These are business-contract expectations, not implementation-derived result filters. There are eight mandatory original cases plus four additional search tests. Each test has its own newly seeded H2 database and counters. Expected IDs and fields are literal assertions in the shared harness.

## Route

1. Run `-Track exercise -Command demo`; record **observed** IDs, fields, status and leakage boolean. The demo avoids printing captured token-bearing log lines or the full sensitive response.
2. Run `-Track exercise -Command lab1`. Record failing test names and exit code. Do not change assertions to make the exercise green.
3. Review [../WALKTHROUGH.md](../WALKTHROUGH.md) and perform a manual source/sink review. Find query construction, trusted identity, missing supplier predicate, full-entity mapping, header logging and bounds.
4. Use [../COPILOT-PROMPTS.md](../COPILOT-PROMPTS.md) to compare Copilot suggestions against your manual baseline. Do not send real tokens/data to AI.
5. Propose or locally implement a bounded fix in the exercise folder. Keep the oracle unchanged and preserve the fixture. The fixed folder is the instructor comparison, not a source to copy blindly.
6. Run the same lab1 tests again. Inspect actual prepared SQL and supplier binding; a zero-row result alone is not parameterisation proof.
7. Compare `-Track fixed -Command lab1` and submit findings/evidence/limits.

Do not bind an entity, add a request-supplied supplier, remove scope checks or treat response minimisation as authorisation. Names are exact and case-sensitive in this H2 fixture; confirm deployed provider collation separately.

## Evidence packet

Record command, track, Java runtime, H2 pin, test summary/exit code, confirmed finding, minimal control and remaining assumption. Distinguish executed JDBC evidence from designed-only HTTP/authentication integration. No live system, real JWT or Spring middleware is involved.