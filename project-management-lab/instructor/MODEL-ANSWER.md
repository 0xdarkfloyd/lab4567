# Project management lab — instructor model answer

**Advanced route answer key:** covers A1–A4 and supporting workshops in the [advanced worksheets](../student/WORKSHEETS.md). For the beginner questions use [BEGINNER-SOLUTIONS.md](BEGINNER-SOLUTIONS.md). Shared R1 facts and evidence limits apply to both; reveal the selected key after the attempt.

**Instructor-only · illustrative decisions, not MTR policy · tests below are expected oracles, not performed results.**

Use [student/SAMPLE-ASSESSMENT-REPORT.md](../student/SAMPLE-ASSESSMENT-REPORT.md) and [student/WORKSHEETS.md](../student/WORKSHEETS.md). This key preserves exercises 4A–4D and expanded activities 4E–4J from section 4 of [06-instructor-workbook-and-solutions.md](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/06-instructor-workbook-and-solutions.md), grounded in the actual tasks/presenter answers of [04-project-management-lab.md](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/04-project-management-lab.md).

## Preparation and answer discipline

- PDFs supply teaching themes, **not pentest findings**. R1/S1–S6 are original synthetic fixtures. Refer to the report's local page map for AppSec pages 2–3, SDLC pages 2–3 and API-development pages 2 and 4.
- Date/scope: **1 October 2026**, staging web/API/gateway, SUP-A/SUP-B; production/mobile/subcontractors excluded. **stg-2026.10.01-r1** is an additional fixture identifier, not a source-PDF build or artifact hash.
- Baseline R1 has six **open** findings and **no retest**. Later claims, tabletop injects and the worked-decision update are independent hypotheticals. Never merge them into baseline or populate actual results with these expected checks.
- F1 staging applicability is supplied. F2/F3/F5 are confirmed only in the fictional case. F4 is an unverified build-path scanner flag. F6 is untested assurance, not demonstrated AI disclosure.
- F1/F4 have no identifiable real CVEs. CVSS **3.1** scores/vectors must not be invented or relabelled. F1 fictional KEV Yes / EPSS 0.72 are not live intelligence or proof of local compromise.
- Role names below are proposals to confirm against actual organisational delegations. AI/PM/vendor cannot acquire acceptance powers merely by writing minutes. Targets/expiry dates require agreement; no numeric SLA or pass threshold is supplied.

## 1. Exercise 1 — classify R1 before ranking

### Complete F1–F6 evidence key

| ID / citation | Classification and why | Evidence still needed / owner / decision consequence |
|---|---|---|
| F1 / R1 §5 F1, S1 | Applicable **public staging** gateway advisory is a supplied case fact; fictional signals support contextual urgency, not local-compromise attribution. Production remains unknown. | Gateway/platform/vendor owner supplies staging version/configuration and approved mitigation/fix evidence; production owner supplies actual version/exposure comparison. Independent reviewer checks scope. Missing production evidence prevents assurance for the affected production path, not a staging reclassification to “unknown everywhere”. |
| F2 / R1 §5 F2, S2 | Confirmed fictional cross-supplier content disclosure: SUP-A obtains the SUP-B marker. Authentication is present; exact-resource permission failed. | Application/vendor/data owners supply original route/build/root cause, same-class coverage and access history; reviewed deployed change and independent two-supplier retest required. Unknown production equivalence cannot inherit a passing staging claim. |
| F3 / R1 §5 F3, S3 | Confirmed fictional active credential exposure to broad log readers; not demonstrated misuse or compromise. Marker is inert, never a usable token. | Credential/incident/log owners identify authority, consumers, window/readers/copies and use history; supply invalidation, replacement, log-control and investigation evidence. Removing a sink does not close historical authority/exposure. |
| F4 / R1 §5 F4, S4 | Vendor scanner assertion; supplied 9.8 is not verified applicability or exploitation. Build-only is not an exemption. | Build/dependency/vendor owner supplies inventory/version/advisory mapping, build execution/inputs, privileges/secrets and artifact integrity. Reviewer verifies relevant path or scoped non-applicability. Unresolved artifact trust is a release dependency. |
| F5 / R1 §5 F5, S5 | Confirmed fictional deprecated-protocol acceptance at the stated public staging endpoint. Exact protocol/cipher/policy unspecified. | Network/platform/client owners supply negotiation evidence, termination inventory, client matrix and actual approved baseline; independent protocol/compatibility retest. Do not invent a specific protocol or policy violation. |
| F6 / R1 §5 F6, S6 | Design assurance gap: ACL filtering omitted, retrieval untested. No confirmed AI disclosure. | AI/application/data owners supply authoritative ACL design, pre-context filtering, cache/revocation/tool enforcement and approved processing flow; reviewer needs cross-scope tests. Keep confidential-data AI scope gated while assurance is absent. |

**Coverage:** staging fixture/date/accounts only; vendor dependency output is supplied evidence, not independently verified CI assurance. Production/mobile/subcontractors excluded; AI execution and retest absent. Other PDF agenda topics are not “passed”. Component hashes, exact versions, vectors and comprehensive route/role/sink coverage are missing. None of these gaps proves a control absent in every environment.

### Model assessor/vendor questions — at least five required

1. **Environment mapping (F1/F2):** What deployed artifact/version/configuration inventory and change history map assessed staging to intended production? Supply actual differences, exposure, identity/ACL and data paths; platform/application owners provide authorised records.
2. **F2:** What exact request/resource/build demonstrates the failure, which related read/search/preview/download/export/storage/job paths were covered, and who independently verifies the deployed correction with both suppliers?
3. **F3:** What authority, lifetime, consumers, log-reader population, exposure window, exports and use history belong to the exposed credential? Supply sanitised invalidation/replacement and incident/log-control evidence, **not the secret**.
4. **F4:** What component/version/lock/SBOM and advisory-prerequisite evidence supports the scanner flag? Which build inputs execute it, with what CI permissions, release credentials and artifact effects?
5. **F5:** Which deprecated protocol/cipher was accepted at which termination points? What baseline and client compatibility evidence governs removing support, and who owns deployed retest?
6. **F6:** Where are current supplier/resource ACLs enforced before retrieval/model context and cache/tool access? Supply design, provider/data-flow approval and two-supplier/revocation test coverage.
7. **Cross-cutting:** What original score/vector provenance and finding-level deployed retest scope/method/result/reviewer exist? In this placeholder case real advisory/vector evidence is unavailable; record that, rather than filling it in.

**Challenge answer:** “Production was not tested” means no production conclusion. Obtain authorised inventory/configuration/ACL/build evidence; separately approve any production tests. Same product or planned fix is not proof of equivalent deployed control.

**Why / alternatives:** accept alternative labels such as stipulated observation / supplier claim / unassessed if the evidence boundaries remain intact. Redacted records or witnessed synthetic checks can substitute for sensitive raw traces when they resolve the same uncertainty. Reject all-six-confirmed, production-safe/affected-without-evidence, invented CVEs, F1-unknown-everywhere and “high confidence” unsupported by source.

## 2. Exercise 2 — triage and automatic-queue challenge

### Reference treatment, not a universal sequence

| ID | Illustrative treatment / rationale | Evidence that changes treatment |
|---|---|---|
| F1 | **P1:** contain public applicable staging gateway and mitigate/patch; evaluate production separately. Supplied exploitation context and exposure matter beyond score. | Verified effective mitigation reduces a scoped path's exposure but does not erase applicability or permanent work. Actual production inventory changes production disposition, not the supplied staging fact. |
| F2 | **P1:** contain foreign-document access, assess history, correct resource ACL and independently retest. Direct confidential-data disclosure has business urgency without a score. | Verified deployed isolation/alternate-path coverage can support re-enabling scoped access; historical review and production transfer remain separate. |
| F3 | **P1:** revoke/rotate, restrict logs and preserve/investigate evidence. An active credential is an authority-removal concern, not merely a future code ticket. | Verified invalidation reduces active authority; privilege/window/use/copy evidence determines incident scope. Rotation alone does not close incident or sink controls. |
| F4 | **Prompt owned validation**, provisional treatment depending on build-chain exposure; no automatic 9.8-first or dismissal. | Untrusted build execution with release authority could elevate to urgent containment. Proven scoped non-applicability can support disposition. Neither situation is present in baseline. |
| F5 | **P2/P3 conditional on actual baseline, exposure, clients and verified controls.** Resolve or explicitly govern the transport issue. | Baseline/client/termination evidence and tested restrictions change urgency/exception feasibility; do not assume existing compensating controls. |
| F6 | **P2 assurance gate for the AI pilot:** keep disabled while isolation/processing evidence is established. | Verified pre-context ACL/cache/tool design and cross-scope tests support an authorised scoped trial. Clean summaries/read-only promises do not. |

**First three workstreams: F1, F2 and F3 in parallel.** Platform owner addresses gateway, application owner disables affected access, credential/incident/log owners invalidate authority and control logs. Build owner starts F4 investigation concurrently. If staffing forces sequencing, document capacity and dependencies, name owners for all immediate containment, and escalate service trade-offs through actual authority; a serial scoring queue must not leave F2/F3 exposed and unowned.

### Nine analytical errors — require at least five distinct errors with reasons

| Error | Why it fails / correction |
|---|---|
| 1. Base CVSS determines the entire queue | Technical severity is not exposure, business impact, urgency or evidence quality. Use the affected control/data/service context. |
| 2. F4 scanner flag treated as verified exploitability | Build path/advisory prerequisites unknown. Promptly investigate without calling it exploited or harmless. |
| 3. F1 applicability/public exposure/exploitation context underweighted | Staging applicability is given; supplied fictional signals strengthen containment rationale without proving local compromise. |
| 4. F2 postponed despite foreign-content evidence | Confirmed isolation failure directly threatens confidential supplier material. Missing score cannot justify delay. |
| 5. F3 postponed despite active authority in readable logs | Immediate invalidation/log control/investigation needed; a scanner or code patch does not revoke it. |
| 6. No CVSS equated to low risk | Missing metrics are missing evidence. Custom authorisation and logging weaknesses may lack CVEs. |
| 7. F6 ignored because no exploit demonstrated | Assurance gap blocks evidence-based AI rollout even without a reproduced disclosure. Keep observed failure distinct from launch gate. |
| 8. Urgent work made strictly serial | Distinct owners can contain F1–F3 in parallel; capacity constraints require explicit decisions, not blind scoring. |
| 9. Scope, uncertainty, business impact and authority omitted | Production unknowns, shared credentials/build paths, temporary controls and authorised release scope affect the recommendation. |

**Acceptable alternatives:** relative ordering can differ with evidence, explicit capacity and verified controls; unsupported hypothetical mitigations cannot down-rank baseline findings. P1/P2/P3 carry no invented hours/days or MTR obligation. EPSS 0.72 is not a 72% asset-compromise claim; fictional KEV is not observed local exploitation.

## 3. Exercise 3 — complete six-finding remediation records

Decision points below are event-based proposals to agree, not organisational deadlines. Each owner must maintain evidence links, actual/not-run status, dependencies and escalation. Independent verifier reviews the deployed state, not merely vendor intent.

### F1 action record

- **Contain:** restrict/isolate the affected public staging gateway path or deploy an approved effective mitigation; preserve relevant investigation evidence. Start authorised production inventory/applicability collection separately.
- **Permanent correction:** supported gateway fix/configuration addressing applicable conditions; production treatment **if evidence shows applicability**.
- **Accountability / decision point:** platform/gateway owner and vendor engineering deliver; security/incident reviewer investigates/verifies; actual change/release authority approves. Verify staging deployment/retest and resolve production applicability before exposing the relevant release path.
- **Change / rollback:** validate routing, authentication, health, capacity and configuration backup. If an old version is restored, retain isolation/mitigation or disable affected path; availability recovery must not silently restore vulnerable public exposure.
- **Independent retest / closure evidence:** original applicable conditions addressed on identified component/build/config/date; approved focused checks cover mitigation bypasses and legitimate traffic. Include deployment/change reference and production-specific applicability disposition. No real exploit/vector can be inferred from this placeholder.
- **Residual risk / expiry:** temporary mitigation coverage, drift, investigation and production unknowns stay owned. Any interim acceptance has actual approver, scope, monitoring and agreed expiry/review event, with suspension on control failure or new applicability evidence. Staging technical verification does not close an unknown production workstream.

### F2 action record

- **Contain:** disable/restrict affected document access and alternate paths; preserve minimal authorised evidence and inspect access history. Essential scope can remain only if actual separation is demonstrated.
- **Permanent correction:** trusted server-side supplier/resource ACL before content/links; apply consistently to search, preview, download, exports, storage and jobs/cache as relevant. Authentication or opaque IDs alone are insufficient.
- **Accountability / decision point:** application/solution owner accountable; vendor engineering delivers; data/incident owner assesses historical disclosure; independent assessor verifies. Re-enable affected functionality only after deployed isolation and related-path evidence supports it.
- **Change / rollback:** coordinate outage, ACL/data/cache migration and compatible clients. Rollback to vulnerable code keeps endpoints blocked; use a tested safe fallback, not “restore service” by restoring leakage.
- **Independent retest / closure evidence:** SUP-A own and SUP-B own success; cross-supplier denial both directions with no foreign bytes/metadata/link; anonymous and missing/invalid/ambiguous scope deny safely. Cover relevant alternate routes, cache/revocation/queued work and races. Attach reviewed change, deployed revision/configuration, expected/actual evidence, date and independent reviewer.
- **Residual risk / expiry:** historical access and excluded/uncovered paths remain investigated/owned. Any segregated exception names exact permitted resources/actions, enforced boundaries, authority and expiry/review; suspend on disclosure or contradictory tests. Passing one staging route does not close production assurance.

### F3 action record

- **Contain:** revoke/rotate exposed authority, restrict log access/exports and preserve controlled incident evidence; assess possible use and related copies/sinks. Removing a file is neither invalidation nor investigation.
- **Permanent correction:** remove secret logging across all sinks, minimise/redact telemetry, least-privileged replacement credential, access/retention controls and reviewed secret lifecycle.
- **Accountability / decision point:** credential/service owner handles replacement consumers, incident lead directs investigation, logging/support owner restricts sinks. Change/incident/release authorities make actual service decisions. Verify old invalidation and safe replacement before normal dependent access resumes.
- **Change / rollback:** coordinate consumers and availability. Do not restore the exposed credential; use another new least-privileged replacement or controlled outage. Any overlap needs specific authorised safeguards; do not invent an automatic overlap allowance. Preserve evidence under approved procedure, not indiscriminate deletion.
- **Independent retest / closure evidence:** old authority rejected via approved safe verification; replacement workload succeeds; synthetic marker absent in application/proxy/trace/exception/success/denial logs; support restrictions and retention verified. Incident record addresses exposure window/readers/exports/use, remaining limits and accountable disposition. Evidence packet contains IDs, not credentials.
- **Residual risk / expiry:** copied logs, possible misuse and historical data access do not disappear with rotation. Keep incident work open until evidence supports disposition. Expiring service exception must never be interpreted as authorising continued exposed-credential use; stop on suspected misuse or renewed logging.

### F4 action record

- **Contain / validate:** promptly map component/advisory/build path. Constrain untrusted inputs and CI authority, or suspend the affected path when credible exposure warrants it; baseline does not establish compromised CI.
- **Permanent correction:** upgrade/remove applicable dependency/plugin and rebuild trusted artifacts; alternatively document scoped non-applicability with actual prerequisites/reachability evidence. Runtime absence alone is not sufficient.
- **Accountability / decision point:** build/platform/dependency owner and vendor supply graph/path/provenance; security reviewer independently challenges; release authority decides before promoting affected artifacts or using the relevant pipeline.
- **Change / rollback:** compare build compatibility, artifact outputs and signing/publishing provenance. Roll back only to reviewed safe dependency/configuration/artifacts. Suspend the path if no trusted fallback exists; do not restore uncertain artifacts to meet a date.
- **Independent retest / closure evidence:** lock/SBOM/version/provenance, actual advisory mapping if a real assessment replaces the placeholder, input/job execution, CI permissions/secrets and artifact-impact analysis. For an applicable path, demonstrate correction and legitimate trusted build; for non-applicability, independently review the exact scope and preconditions. A green scan alone is insufficient.
- **Residual risk / expiry:** unknown paths, vendor scanner coverage and artifact trust stay explicit. Any interim pipeline/artifact disposition is scoped, authorised and reviewed on dependency/input/privilege changes or expiry; escalate suspected CI compromise under incident process. Do not supply a made-up CVE, vector or exploit result.

### F5 action record

- **Contain:** inventory clients/termination points and restrict deprecated transport where feasible under approved service handling. Confirm actual protocol/cipher and baseline before specifying precise configuration.
- **Permanent correction:** disable deprecated support and migrate dependent clients to the approved baseline.
- **Accountability / decision point:** network/platform owner accountable with client/service owners; independent reviewer verifies. Before public release, resolve relevant configuration/compatibility or obtain an authorised scoped exception.
- **Change / rollback:** staged/canary compatibility checks, configuration backup and service monitoring. Prefer upgraded clients or verified isolated constrained legacy access if justified; blanket public re-enable is not a secure fallback.
- **Independent retest / closure evidence:** deprecated negotiation rejected; approved negotiation and supported client journeys succeed at each relevant gateway/CDN/load-balancer/backend termination. Record endpoint/config/date/method/results and client matrix. Do not invent protocol details or claim “TLS on” is sufficient.
- **Residual risk / expiry:** any legacy client exception has bounded users/paths, restrictions, actual approver, monitoring, expiry/review and stop triggers; at expiry suspend the exception unless explicitly renewed through authority. Supplied 5.3 determines no automatic treatment deadline.

### F6 action record

- **Contain / gate:** keep AI pilot disabled through enforced connector/retrieval/tool configuration. A separately authorised synthetic-only trial requires proven separation from confidential data; read-only is not isolation.
- **Permanent correction:** establish authoritative ACLs before context retrieval, current-permission checks, scoped cache/invalidations and broker policy outside the model; review processing/provider/data permissions.
- **Accountability / decision point:** AI/application/data owner with vendor/retrieval engineering; independent security reviewer verifies; actual data/release/risk authority decides before enabling relevant data/access scope.
- **Change / rollback:** review connector/index/cache/data-flow dependencies; disable pilot/tools and stop ingestion safely, invalidate affected caches as authorised. Rollback must not restore unfiltered retrieval or confidential-data access.
- **Independent retest / closure evidence:** SUP-A/SUP-B own-scope success and cross-scope denial at retrieval, chunks/context, citations/cache and response; revoked/changed scope cannot retrieve stale material. For implemented tools, check allowed read, foreign read denial, unauthorised action denial, unknown tool, malformed arguments and budget denial; no forbidden content/effects. Review safe output/log/budget behaviour and approved provider flow. Record exact revision and independent expected/actual checks. A prompt or clean summary is insufficient.
- **Residual risk / expiry:** inaccurate summaries, injection influence, provider/model changes and omitted paths remain monitored and change-regressed. Pilot approval specifies data/actions/users, controls, actual authority and expiry/review; stop on isolation failure, unapproved flow, changed permissions or expired acceptance. These are proposed conditions, not proof of present implementation.

### Closure workflow and common rejection criteria

Track **reported → validated → contained → corrected → deployed → independently verified → authorised disposition** separately. Baseline R1 contains no completed transition evidence. Failed retest returns to correction; containment remains. Non-applicability needs scoped evidence; acceptance is not technical closure. Investigation and production assurance may remain open after staging correction.

Reject email-only closure, screenshot-only authorisation proof, scanner-only credential closure, “no CVE means no work”, F4 runtime-only dismissal and F6 prompt-only assurance. Equivalent policy services, database controls or vendor-managed enforcement can earn full design credit if complete mediation, deployment, independent checks and customer accountability are explicit.

## 4. Exercise 4 — sample one-page board recommendation

**Decision requested:** defer the affected internet-facing release scope pending relevant verification; keep the AI pilot disabled. This is a recommendation to authorised project/release/risk owners, **not an approval made by AI, vendor or this key**.

**Basis:** synthetic R1 dated 1 October 2026; staging build fixture stg-2026.10.01-r1; SUP-A/SUP-B. F2 confirms foreign supplier content; F3 active credential exposure; F5 deprecated transport acceptance. F1 applies to public staging; production unknown. F4 build-path applicability unknown; F6 design/isolation assurance absent. Production/mobile/subcontractors excluded; all findings open; no retest; scores CVSS 3.1 with vectors absent. Cite R1 §5 F1–F6 / S1–S6; do not represent fictional intelligence as current real evidence.

**Top three business risks:** confidential supplier disclosure (F2), credential-enabled unauthorised activity within unknown authority (F3), and gateway exploitation/service effects (F1). Initiate application, credential/incident/logging and platform containment in parallel. Prompt F4 analysis is necessary because build/artifact trust may affect this release.

**Mandatory decision conditions:** verify staging F1 deployed mitigation/fix and relevant paths; determine actual production version/exposure and control applicable paths; repair F2 server-side permission and independently verify deployed original/same-class cases; demonstrate F3 invalidation, functional safe replacement, restricted/redacted logging and accountable incident investigation/disposition; resolve F4 applicability and artifact/build treatment with evidence; correct F5 or obtain an evidenced scoped exception through actual authority. Supplier commitment and booked retest do not satisfy these gates.

**AI conditions:** retain enforced off-state until F6 ACL-before-context, cache/revocation and applicable tool controls are independently demonstrated, with approved processing/data flow. A later trial should be read-only with synthetic or specifically approved data and scoped monitoring. Read-only and a clean summary cannot justify launch today.

**Unknowns / owners:** platform/vendor owns production mapping; application/data/incident owns historical disclosure; credential/logging/incident owns exposure/use; build owner owns CI applicability/artifact trust; AI/data owner owns isolation/provider evidence; network/client owners own transport compatibility/baseline. Independent assessor validates scoped evidence. PM coordinates access, dependencies and agreed decision events; actual release/risk authority decides. No invented dates, names or delegation.

**Residual-risk record:** any proposed exception names exact assets/users/actions/data, evidence/unknowns, alternatives, verified controls, accountable owner and authorised approver; specify agreed expiry **or review event**, monitoring, revocation and safe recovery. No renewal by silence. At expiry or contradictory evidence, suspend the exception pending authorised reconsideration. Historical investigation cannot be disguised as technical closure.

### Defensible limited-scope alternative — conditional, not presently proven

A board may consider a genuinely segregated unaffected capability only with all relevant conditions evidenced:

1. Actual routes, identities, data/storage/cache/jobs and permissions show no path to affected features or foreign supplier material; F2 affected access and AI retrieval/tools remain enforced off.
2. Shared gateway exposure and actual production F1 applicability are resolved/controlled for that enabled scope; “same product” and “it's only staging” are not sufficient.
3. Exposed credential is invalidated; safe replacement/dependent services and log controls are verified; ongoing incident risk is explicitly owned/governed.
4. Build/artifact provenance and F4 applicability support the proposed release, not just the fact that a library is absent from runtime.
5. F5 treatment/exception and other residual risks are scoped, verified and authorised against the actual baseline.
6. Independent positive/negative checks identify the exact enabled revision, monitoring and tested containment/rollback; permissions/configuration demonstrate separation, not merely a feature flag or launch email.
7. Actual authority accepts bounded residual risk with expiry/review event, stop triggers and no automatic extension.

**Why / alternatives:** original R1 does not evidence those conditions, so unconditional proceed is unsupported. Conditional limited release can earn full marks if it makes gates real and retains uncertainties; “release now and test later” cannot. Proceed becomes defensible **after** relevant proof/authority, not by inventing passing tests. Reject/defer longer is reasonable where safe scope or evidence cannot be established. Do not teach that every weakness necessarily blocks every unrelated release.

## 5. Expanded workshop answers aligned to the actual deck

### 5A. Production-equivalence workshop (29–39; key 39–42)

Vendor's common-product and build-only claims do not establish equivalence or non-applicability. Model five-item evidence request:

| Request | Accountable supplier role / independent use |
|---|---|
| 1. Deployed artifact/component/version inventory for staging and production | Platform/vendor/application owners; reviewer identifies actual affected assets/revisions. |
| 2. Configuration/exposure comparison, including gateway routing/termination and relevant changes | Platform/network owners; reviewer checks effective deployment, not just template equality. |
| 3. Identity/ACL/data-flow comparison, including storage/cache/jobs | Application/data/identity owners; reviewer checks effective supplier/resource isolation. |
| 4. Dependency/build-path provenance, inputs, privileges and artifact linkage | Build/vendor owners; reviewer assesses CI reachability/secrets and release artifact trust. |
| 5. Dated environment-specific tests/change history and scope/limitations | Test/change/environment owners; reviewer identifies what assurance transfers and what needs authorised testing. |

| ID | Reusable staging evidence | Unproven aspect / consequence / expected checks |
|---|---|---|
| F1 | Supplied applicability and staging correction design guide work; staging applies already. | Production version/config/exposure remains unknown. Platform owns decision; applicable path must be controlled and legitimate traffic work. Without evidence, do not approve affected production scope as safe. |
| F2 | Failure and reviewed ACL correction inform test oracle. | Production artifact/identity/data paths not verified. Application owns mapping and environment-specific verification: A never gets B bytes; own A/B works. Staging pass cannot close production. |
| F4 | Scanner/build claim initiates investigation. | CI execution/permissions/artifact prerequisites unknown; build owner supplies evidence. Untrusted path cannot misuse release authority; trusted build works. Runtime absence alone does not close finding. |
| F6 | Design omission and assurance requirement reusable. | Retrieval/cache/tool behaviour remains untested; AI owner maintains gate. Cross-scope context/cache denies foreign content; permitted scoped read works only after real checks. No demonstrated AI exploit in R1. |

**Alternative / why:** controlled artifact/config comparison plus witnessed scoped synthetic verification may resolve uncertainties without live production probing. Tests still need authority. Missing evidence is a scoped decision condition, not a safe verdict or proof all production is affected. Suggested workshop rubric **9:** requests 5; four accurate owned reuse/unknown distinctions 4.

### 5B. Evidence-quality wording workshop (42–48; key 48–50)

| Claim | Model wording / classification | Next evidence / why closure unsupported |
|---|---|---|
| F2 fixed by email | “Vendor asserts correction; deployed control and independent verification unknown.” | Reviewed change/deployed revision and original/related A/B positives/negatives; no foreign bytes. Email is intent/assertion, not test proof. |
| F4 critical everywhere | “Supplied scanner CVSS 3.1 9.8; applicability/build path unverified.” | Component/advisory/preconditions/CI privileges/artifact mapping; cannot infer universal exposure or non-applicability. Trusted build success supplements relevant security analysis. |
| F6 exploited | “ACL design assurance absent; retrieval untested; no disclosure reproduced.” | Design plus retrieval/context/cache/tool cross-scope tests; approved own-scope path succeeds. Missing documentation is not exploit evidence. |
| F3 closed after rotation | “Rotation claimed or evidenced only within provided records; log controls and use investigation unresolved.” | Old rejected, replacement functional/not logged, controlled log access and incident evidence; rotation removes one authority but not history/copies. |

Capture source, environment/revision, date, method, expected/actual result, limitation and reviewer. Alternative labels are acceptable if claim versus verification stays distinct. **Rubric 8:** wording/classification 1 and next evidence/closure distinction 1 per row.

### 5C. Vendor negotiation (74–80)

1. **F2:** “Which reviewed change and deployed revision repairs the exact route and related paths? Who independently verifies both suppliers, and what access/fixtures/results are delivered?” Application/vendor delivers; assessor verifies; affected access stays gated until agreed criteria met.
2. **F3:** “Provide redacted old-credential invalidation, safe replacement, log restrictions and use/copy investigation coverage. Which service, log and incident owners account for remaining work?” Credential/log/incident roles deliver; independent security reviewer checks; no secret in the packet.
3. **F1:** “Who supplies production component/configuration/exposure comparison and authorised applicability verification? What affected release decision remains unsupported if this evidence is unavailable?” Platform/vendor provides; authorised environment owner controls access; release/risk authority decides.

Agree deliverables, independence, sanitised evidence, access, acceptance criteria, owner and actual decision event. Commercial roles agree costs/obligations; no guarantee of zero risk, unilateral deadlines or invented contract rights. **Alternative:** approved witnessed/redacted equivalent proof may replace sensitive raw records, never evidence-free trust. **Rubric 6:** concrete deliverable 1 and owner/criterion/consequence 1 for each question.

### 5D. Change and retest procurement workshop (120–130; key 130–133)

**Reject release on deletion/screenshot/future-environment testing alone.** Model role-play agreement:

| Workstream | Minimum package / accountability / why |
|---|---|
| F3 containment and incident | Credential owner invalidates/rotates and coordinates consumers; log owner restricts exports/access; incident lead preserves authorised records and investigates copies/use. Deletion alone may destroy evidence without removing authority. |
| F2 correction/deployment | Vendor/application owner supplies reviewed change, exact artifact/config/environment and deployment evidence; operations provides compatibility/health/recovery readiness. Screenshot is not authorisation proof. |
| Independent retest statement | Approved environments/versions/accounts/windows; original foreign read plus related search/download/preview/storage paths, positives and negatives; assessor independence; agreed no-content/no-effect oracles and limitations. No “pass” result until performed/evidenced. |
| Deliverables/handling | Finding-level redacted evidence with date/revision/method/expected/actual/reviewer; secure access to originals; secrets and unnecessary document contents excluded from AI/board packet. |
| Change and rollback | Actual change authority approves; operations executes and owns recovery. If rollback restores leakage, keep affected route disabled; failed health triggers safe fallback/escalation. |
| Commercial escalation | Authorised procurement/commercial role negotiates price/access/statement of work; PM records unresolved terms/dependencies. Commercial delay is not risk acceptance, and a purchase order is not a passing retest. |

**Expected checks:** old F3 denied; replacement works without logging; A cannot read B while own A/B succeeds; failed F2 retest remains open; unsafe rollback does not reopen access. Record not-run tests explicitly.

**Alternative / why:** defer affected functionality during procurement, or propose demonstrably segregated limited scope with actual expiring acceptance; no assumed right to free retest or release. Paper agreement is not a purchase, deployment or approval. **Rubric 8:** F3 handling 2; F2 independent evidence 2; deploy/rollback/roles 2; procurement/unresolved conditions 2.

### 5E. Incident tabletop (140–150; key 150–154)

These ordered injects are hypothetical; do not rewrite baseline R1.

| Inject | Immediate action / owner | Preserve and qualify evidence | Stop / safe restart conditions |
|---|---|---|---|
| Suspect F3 use outside workload; support logs exposed | Credential owner invalidates/rotates; logging owner restricts access/exports; incident lead invokes actual process and investigates related data/consumers. | Sanitised use/access event IDs, sequence, credential authority and affected-sink scope in authorised protected evidence. Suspicion is a signal, **not attacker attribution**. | Stop exposed authority/access. Approved safe replacement works without secret logging; old authority denied; investigation and residual risk owned under incident/release authority. No invented response interval. |
| F2 retest fails; rollback health fails | Stop affected document access; retain containment. Operations/recovery owner escalates to actual change/incident authority and uses tested safe fallback or outage. | Failing revision/config/test evidence and recovery/health sequence; preserve failure, not an overwritten green report. | No closure or restart on failed checks. Fallback must be healthy and deny foreign content while permitted scope works; independent evidence plus authorised scoped recovery decision required. |
| AI launch demanded; clean summary supplied | AI/application/data owner keeps pilot enforced off; security reviewer requests ACL-before-context, cache/tool design and cross-scope tests. Sponsor demand is not technical assurance. | Summary labelled limited observation; missing isolation/processing evidence retained. No claim model resistance proves filtering. | Block confidential-data launch until scoped controls/data flow verified and authorised. Own-scope synthetic/read-only operation may be checked within separately approved segregated trial; read-only alone is insufficient. |

**Alternative / why:** controlled outage or proven isolated essential service can be appropriate when containment/recovery/monitoring and actual authority support it. Do not restore leaked credentials, infer an attacker, launch on a clean summary or declare success after failed rollback. **Rubric 9:** action/owner 1, protected evidence/uncertainty 1, stop/restart oracle 1 per inject.

### 5F. Worked board decision and minutes (169–177)

**Separate hypothetical update:** scoped staging F2 retest passes; F3 rotation evidenced, use investigation open; production F1 equivalence, F4 CI analysis and F6 tests incomplete. This does not amend R1 or prove F3 log-control completion.

**Worked recommendation:** defer affected internet-facing scope and keep AI disabled. Record F2 verification as staging-only for the actual hypothetical tested paths/revision; historical review and production transfer are not established. Preserve F3 containment/investigation and obtain log-control coverage, F1 production and F4 build evidence. Limited scope requires the same proven segregation/recovery/authority conditions as §4, not optimism.

**Replacement minutes — model text:**

> The recommendation is to defer affected internet-facing release scope and retain the enforced AI off-state. In the separate hypothetical add-on, F2 passed a scoped staging retest; this is not production assurance or whole-project closure. F3 rotation is evidenced, but use investigation and log-control coverage remain open. F1 production applicability/equivalence, F4 CI/advisory applicability and F6 retrieval/cache isolation evidence remain unresolved. Platform, application/vendor, credential/logging/incident, build and AI/data owners will supply the finding-level evidence for independent review. Actual release/risk authorities, not AI or vendor advice, decide the scope and any residual-risk disposition. A limited-scope proposal requires verified segregation and healthy tested recovery, documented controls/unknowns and authorised acceptance with an agreed expiry or review event; there is no renewal by silence. Suspend affected access or exception on cross-supplier disclosure, credential misuse signal, failed security/health/recovery checks, changed assumptions or expiry. No treatment date or approval is invented in these minutes.

**Why:** staging pass does not cover excluded production; rotation does not conclude exposure/use; build-only does not establish F4 irrelevance; read-only does not prevent context disclosure; AI cannot accept risk indefinitely or otherwise. **Alternative:** proposed conditional limited scope earns credit if evidence, authority, review/expiry and stop triggers are equally explicit. **Rubric 6:** scope/facts 2; open F1/F3/F4/F6 work 2; authority/expiry/triggers 2.

## 6. Assessment rubric and answer-quality guidance

Overall weights match the deck. Workshop mini-rubrics above are formative aids, **not additional weights to add to 100**. No organisational pass mark or authority is supplied.

| Area | Weight | Full-credit answer / rationale |
|---|---:|---|
| Evidence validation and unknowns | 25 | Six correctly scoped classifications, source citations, coverage/exclusions, at least five targeted questions. Preserves F1 staging known / production unknown, F4 unverified and F6 assurance gap. |
| Business-risk prioritisation | 25 | Contextual treatment of all six; F1–F3 parallel owned actions; at least five distinct queue errors with consequence; evidence that changes priority. Does not use score arithmetic as policy. |
| Remediation and closure | 25 | Six records cover containment/correction, accountable roles, agreed decision events, safe rollback, independent negative/good-path evidence and residual-risk expiry/review. Separates technical, incident and scope disposition. |
| Governance recommendation | 15 | Plain-language scoped recommendation, top risks, actual authorities, mandatory/AI conditions, owned unknowns and explicit limits of alternative limited release; no silent/indefinite acceptance. |
| Responsible AI use | 10 | Approved synthetic context only, checked citations, no invented CVEs/vectors/signals/results/policy; AI drafts but does not approve; hypothetical evidence remains separate and actual/not-run status honest. |

**Critical errors requiring correction regardless of aggregate score:** inventing threat intelligence or tests; missing score treated as low risk; AI accepts risk/releases; F3 closed without invalidation; untested AI isolation called proven. Also challenge production safety from exclusion, real PDF findings attributed from this invented pack, erased incident evidence, unsafe rollback and fabricated authority/deadlines. Do not invent an automatic numeric penalty or pass threshold.

**Feedback prompts:** What source supports that claim? Which environment/revision? Is it observation, assertion, assumption or missing assurance? Which trusted control enforces permission? What negative result would falsify closure? Does the legitimate path still work? Who can actually decide, what expires, and what happens if rollback fails?

## 7. Coverage ledger and facilitator finish

| Student activity | Complete key |
|---|---|
| Exercise 1: F1–F6 classification, scope, at least five questions | §1 |
| Exercise 2: all six triage records and at least five automatic-queue errors | §2 |
| Exercise 3: six containment/fix/owner/decision/rollback/retest/residual records | §3 |
| Exercise 4: one-page board answer and conditional limited-scope alternative | §4 |
| Production equivalence / evidence wording / vendor negotiation | §5A / §5B / §5C |
| Change and retest procurement / ordered incident tabletop | §5D / §5E |
| Separate worked decision / minutes rewrite | §5F |
| Assessment rationale and responsible AI | §6 |

Four numbered exercises plus six expanded workshops are covered; the queue challenge belongs to Exercise 2, not an added eleventh activity. Keep the existing three-hour route and break. End by checking finding → source → scope/revision → change → independent retest → accountable disposition. Student submissions must still say **not performed** for real execution; neither this key nor the PDFs supplies runtime assurance.