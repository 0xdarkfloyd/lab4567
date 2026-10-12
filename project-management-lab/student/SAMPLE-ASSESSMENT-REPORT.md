# R1 — Supplier collaboration portal security assessment report

**ORIGINAL SYNTHETIC TRAINING REPORT — NOT AN ACTUAL PENTEST OR ASSURANCE CERTIFICATE**

| Report control | Value |
|---|---|
| Report identifier / revision | R1 / baseline training edition |
| Assessment date in the fictional case | 1 October 2026 |
| System | Fictional supplier collaboration portal |
| Environment | Staging web application, API and gateway |
| Build | **stg-2026.10.01-r1 — additional fictional fixture created for this pack** |
| Assessed identities | SUP-A and SUP-B synthetic supplier accounts |
| Intended audience | Project manager, solution owner, security reviewer and authorised release/risk roles |
| Handling | Training-only synthetic content; not evidence of a real organisation's security state |
| Remediation / retest | All six records open; no correction, containment completion or retest result supplied |

## 1. Executive summary

The fictional portal supports confidential supplier and commercial document collaboration and is approaching release. Supplier access is internet-facing; shared hosting and vendor support are part of the business context. A proposed AI summary pilot introduces another data-access path. There is **no railway-control or safety-system connection** in this case.

R1 records six issues of differing evidence quality. In the stated fictional staging scope, SUP-A can retrieve a SUP-B document (F2), an active synthetic credential is exposed in logs readable by a broad support group (F3), and a public endpoint accepts a deprecated TLS protocol (F5). F1's fictional advisory is applicable to the public staging gateway. F4 is a vendor-supplied dependency scanner observation; the build exploitation path is unverified. F6 is an assurance gap in the AI retrieval design, with no retrieval isolation test performed.

These statements are **stipulated case facts**, not tests performed by the author or findings extracted from the PDFs. The report provides evidence descriptions and proposed verification, not a release decision or remediation ranking. Production applicability, historical misuse and control effectiveness after a change remain unresolved. No finding is closed and no retest has occurred.

Supplied scores are **CVSS 3.1**: F1 8.1, F4 9.8, F5 5.3. Vectors are absent. F1 has **fictional KEV=Yes and EPSS=0.72** as exercise inputs. They are not live intelligence, proof of local compromise or a probability that this asset has been compromised. F2/F3/F6 have no supplied score. Missing scores do not establish low risk.

## 2. Scope and system description

### 2.1 Included scope

| Asset label — additional fictional fixture | Included boundary / purpose |
|---|---|
| STG-WEB | Supplier-facing staging web interface. |
| STG-API | Staging supplier-document API, including the illustrative document-read path in S2. |
| STG-GW | Public staging gateway terminating/routing portal traffic. No real hostname or address is supplied. |
| Associated supporting evidence | Vendor build dependency output, sanitised logging description and proposed AI design excerpt. These are supporting records, not a full CI/logging/AI assessment. |

The report associates the staging web/API/gateway fixture with build **stg-2026.10.01-r1**. Exact component versions, configuration hashes and production revision are not supplied. SUP-A and SUP-B are distinct fictional suppliers, not real users. The document marker in S2 is inert synthetic text, not confidential business data.

### 2.2 Explicit exclusions

- Production systems: no production access, tests, equivalence or applicability conclusion.
- Mobile clients and subcontractor systems.
- Full cloud, identity-provider, support-access, CI or incident audit; supporting excerpts do not imply complete assessment of these systems.
- AI retrieval execution/isolation: design excerpt reviewed in the narrative, but retrieval, cache and tool isolation were **not tested**.
- Remediation effectiveness and independent retest: none completed.

An exclusion means **no assessment conclusion**, not a safe/affected verdict. A common product name alone does not prove staging/production equivalence. Production evidence collection or testing needs separately approved scope and access.

## 3. Methodology, provenance and confidence

### 3.1 What this report does and does not claim

The fictional assessment narrative uses targeted authenticated authorisation review, sanitised log evidence review, gateway applicability/configuration review, supplied dependency scanner output, transport negotiation evidence and AI design review. **No such checks were actually run to prepare this report.** Reproduction descriptions below explain the case or propose a future authorised retest; they are not executable attack instructions or a test transcript.

The author created original fixtures and short excerpts to make the existing workshop facts assessable. There is no real assessment firm, assessor signature, exploit payload, credential, scanner product/version, CVE, advisory URL, request trace or production inventory. Do not fill these gaps with invented evidence.

### 3.2 Evidence-source types

| Source type | What it can support in this exercise | Limit |
|---|---|---|
| R1 stipulated observations, S2/S3/S5 | The described staging weaknesses are confirmed within the fictional narrative. | Not empirical classroom or real-system observations; no raw traces or comprehensive coverage. |
| R1 applicability statement, S1 | F1 applies to the supplied public staging fixture. | Production version/exposure and actual advisory mapping absent. |
| Vendor scanner statement, S4 | Scanner flagged a build-only dependency with a supplied score. | No verified exploit path, full graph, advisory prerequisites or artifact-impact evidence. |
| Design excerpt, S6 | ACL filtering is not described; isolation evidence missing. | Omission is not proof of runtime implementation or an AI disclosure. |
| Teaching PDFs | Background control themes only. | Not a source of R1 findings, scores, identities, fixtures or test results. |

Confidence labels are qualitative descriptions of case evidence, **not measured probabilities**: high for a narrowly stipulated observation, limited for assertions without technical corroboration, and unassessed for untested behaviour. Confidence does not replace impact or business prioritisation.

### 3.3 Teaching-source references

These original teaching PDFs are not distributed in this repository. See [attachment availability guidance](https://github.com/0xdarkfloyd/securecodingtraining/blob/main/appendix/README.md). Page references are **1-based PDF positions** in authorised originals. The following paraphrases do not reproduce bulk source text; the synthetic exercises can be completed without the originals:

| Reference | Control theme informing this original report |
|---|---|
| AppSec Reference, page 2 (original attachment) | Source disclaimer; respect the limits of training material. |
| AppSec Reference, page 3 (original attachment) | OWASP/API, headers and secure patterns; informs access/gateway/transport discussion, not specific findings. |
| SDLC, page 2 (original attachment) | Source disclaimer. |
| SDLC, page 3 (original attachment) | Threat modelling, static/dynamic checks, middleware/dependencies, vulnerability management and BDD-style security touchpoints; informs verification and closure evidence. |
| Development Security Concerns for .NET Core & Spring Boot APIs, page 2 (original attachment) | Agenda: authorisation, rate limiting, validation, CORS, sensitive data/errors, injection and testing. These agenda topics are not claimed test coverage. |
| Development Security Concerns for .NET Core & Spring Boot APIs, page 4 (original attachment) | OAuth/JWT/roles; identity validation and resource authorisation are distinct. |

F6 extends the workshop's AI pilot assurance theme using access-control principles; it is **not** represented as an AI finding in these source pages. Exact framework, algorithm, protocol and policy requirements need separately verified implementation evidence.

## 4. Finding register

Order is by identifier, **not a recommended queue or board decision**.

| ID | Subject | Supplied metric | R1 evidence status | Remediation / retest |
|---|---|---|---|---|
| F1 | Applicable fictional gateway advisory on public staging | CVSS 3.1 8.1; fictional KEV Yes / EPSS 0.72 | Staging applicability stated; production unknown | Open / none |
| F2 | Cross-supplier document retrieval | Not supplied | Confirmed in fictional staging narrative | Open / none |
| F3 | Active synthetic credential in broadly readable logs | Not supplied | Confirmed in fictional staging narrative | Open / none |
| F4 | Build-only dependency scanner flag | CVSS 3.1 9.8 | Scanner assertion; exploitation path unknown | Open / none |
| F5 | Deprecated TLS accepted | CVSS 3.1 5.3 | Confirmed in fictional staging narrative | Open / none |
| F6 | AI retrieval ACL assurance missing | Not supplied | Design gap; retrieval untested | Open / none |

F1/F4 are **fictional advisory placeholders, not identifiable CVEs**. No vectors are supplied for any score. There is no current threat-feed lookup, prescribed treatment deadline or organisational risk rating in R1.

## 5. Detailed finding records

### F1 — Applicable fictional gateway advisory on public staging

- **Asset and condition:** STG-GW is publicly reachable in the staging fixture. Applicability of the fictional gateway advisory to that fixture is supplied, not awaiting rediscovery. Supplied CVSS 3.1 score **8.1**, **fictional KEV Yes / EPSS 0.72**.
- **Evidence and source:** R1 scope/applicability statement and S1 fictional vendor inventory note. No real advisory identifier, affected-version range, vector or exploit trace exists in the pack.
- **Reproduction description / synthetic status:** the narrative matches the staging gateway to the fictional affected fixture and records public exposure. **No exploit was executed, no gateway was contacted and no local compromise was established.** A future retest would verify deployed version/configuration and the relevant approved mitigation/fix against a separately specified oracle.
- **Confidence:** staging applicability is a stipulated case fact; independent technical corroboration is limited by missing component detail. Production applicability is unassessed. Fictional exploitation signals cannot be validated against real feeds.
- **Potential impact:** unauthorised gateway effects or service disruption could affect supplier access and downstream confidentiality/integrity. The exact exploit mechanism and demonstrated local impact are unknown; do not invent them.
- **Known:** public staging exposure; applicability in staging; supplied synthetic score/signals. **Unknown:** production version/configuration/exposure, advisory prerequisites/vector, effectiveness of existing controls, access history and compromise indicators.
- **Recommended treatment:** contain the affected exposed staging path and arrange an approved effective mitigation or supported fix; preserve relevant evidence and assess indicators. Obtain authorised production inventory/exposure evidence and treat production **if applicable**. Do not infer either safety or compromise from the synthetic signals.
- **Recommended independent retest:** link reviewed change, deployed staging component/configuration and date; check original affected conditions, mitigation bypass coverage and legitimate gateway/authentication/health flows. Production disposition needs its own scope/evidence. An installed patch or single blocked path does not prove comprehensive closure.
- **Status:** **open**; containment, correction and deployed retest not evidenced. Production applicability work remains open separately.

### F2 — SUP-A retrieves a SUP-B document

- **Asset and condition:** STG-API document read returns supplier B's synthetic content to authenticated supplier A. Valid login does not confer permission to another supplier's resource.
- **Evidence and source:** S2 fictional request/outcome record; SUP-A retrieves DOC-B-TRAINING with marker **SUP-B-DOCUMENT-MARKER**. The route and marker are added fixtures, not PDF content or actual traces.
- **Reproduction description / synthetic status:** within the case, use the SUP-A identity to request the SUP-B fixture through the illustrative document-read route; foreign content is returned. This is a **stipulated confirmed failure**, not a request executed by the report author. No cookie/token/body trace is provided. The exact root-cause implementation is unknown.
- **Confidence:** high for this narrow case observation; limited for other endpoints, historical access and production. No CVE, numerical severity or vector is supplied.
- **Potential impact:** disclosure of confidential supplier/commercial documents, loss of supplier trust and possible contractual/data-handling consequences. Specific legal duties, number of documents exposed and actual financial loss are not supplied.
- **Known:** authenticated SUP-A can retrieve the stated SUP-B fixture in staging. **Unknown:** complete document/path population, search/preview/export/storage/jobs/cache coverage, ACL implementation, production equivalence and access history.
- **Recommended treatment:** restrict or disable affected access while assessing exposure. Enforce verified server-side supplier/resource permission before content or links leave the trusted boundary; review same-class paths. Adding login, hiding UI or changing identifiers alone does not repair this condition.
- **Recommended independent retest:** deployed revision identified; SUP-A own document and SUP-B own document succeed, both cross-supplier directions deny foreign bytes/links/metadata under the agreed non-disclosing contract. Include anonymous and absent/ambiguous scope cases, alternate paths and current-permission/cache checks as applicable. Record expected and actual content, not status alone.
- **Status:** **open**; no correction/deployment/retest. Historical disclosure review has no supplied result.

### F3 — Active synthetic credential exposed in broadly readable logs

- **Asset and condition:** a credential is active in the fictional fixture and appears in associated portal logs. The fictional support group **SUPPORT-WIDE** can read those logs; exact membership count is not supplied.
- **Evidence and source:** S3 sanitised source description. **LOG-CREDENTIAL-MARKER** is an inert label, not a credential, token, secret or usable authentication string. The excerpt stands for exposure; it does not expose anything real.
- **Reproduction description / synthetic status:** the narrative records an active credential in a log sink with broad support readability. **No credential was supplied, read, validated or used by the author.** Credential activity and group access are stipulated case facts; no real authentication attempt is claimed.
- **Confidence:** high for the narrow exposure/active-status facts in the case; authority, retention, readers, exports and historical use are not assessed. No score or CVE is supplied.
- **Potential impact:** unauthorised activity within whatever authority the credential carries; persistence of copied log material and exposure of accessed data. Credential permissions, lifetime and observed misuse remain unknown, so an actual compromise claim is unsupported.
- **Known:** active synthetic credential exposed; broad log readership. **Unknown:** privileges, dependent workloads, exposure window, copies/backups/exports, actual readers and use, other logging sinks and incident scope.
- **Recommended treatment:** revoke/rotate with controlled dependency recovery, restrict log access and exports, preserve authorised investigation evidence, investigate possible use and related exposure. Remove secret logging, minimise/redact telemetry and review least privilege, access and retention. Do not treat deletion alone as containment or incident closure.
- **Recommended independent retest:** old credential invalidation evidenced through approved safe verification, replacement workload functions, replacement synthetic marker absent across success/denial/error/application/proxy/trace sinks. Verify log access/retention controls and incident evidence/disposition. Use sanitised references, never include the secret in AI prompts or the packet.
- **Status:** **open**; no revocation, logging correction or investigation completion evidenced; no retest.

### F4 — Vendor scanner flags a build-only dependency

- **Asset and condition:** vendor build evidence flags the fictional **BUILD-LIB-FIXTURE** dependency, labelled build-only, with supplied **CVSS 3.1 9.8**. The build exploitation path is unverified.
- **Evidence and source:** S4 fictional vendor scanner summary. It is not a real scanner output, CVE/advisory record or full SBOM. Tool version, exact component version, lockfile and affected-version mapping are absent.
- **Reproduction description / synthetic status:** the case supplies a scanner flag, not a reproduced exploit. No scan/build/dependency execution was performed for this report. “Build-only” describes the supplied classification, not demonstrated non-reachability or absence from all artifacts.
- **Confidence:** limited to the supplied vendor assertion; applicability, prerequisites and reachable execution are unknown. Score vector is absent and cannot be independently reconstructed.
- **Potential impact:** if applicable and reachable in CI/build processing, the issue could affect build credentials or artifact integrity even without a deployed runtime library. The pack supplies **no evidence of malicious artifacts, compromised CI or exploited code**.
- **Known:** vendor flags a build-only dependency and supplies 9.8. **Unknown:** advisory/component/version provenance, build inputs and execution paths, CI privileges/secrets, trusted/untrusted job separation, artifact inclusion/integrity and advisory prerequisites.
- **Recommended treatment:** promptly validate the dependency/advisory and build-chain path; constrain risky inputs/privileges while resolving credible exposure. Upgrade/remove an applicable component and produce trusted artifacts, or document scoped non-applicability supported by evidence. Runtime absence alone is insufficient.
- **Recommended independent retest:** reviewed inventory/lock/SBOM and advisory mapping, relevant build execution/configuration evidence, CI identity/input/permission review and rebuilt artifact provenance. Verify prevention of the applicable path and legitimate build operation, or independently review an evidence-based non-applicability disposition. Do not invent an advisory-specific exploit test from the placeholder.
- **Status:** **open — validation required**; no applicability disposition, remediation or retest supplied.

### F5 — Public staging endpoint accepts deprecated TLS

- **Asset and condition:** STG-GW's public portal endpoint accepts a deprecated TLS protocol in the narrative. Supplied **CVSS 3.1 5.3**; the exact protocol/cipher and vector are deliberately not supplied.
- **Evidence and source:** S5 fictional transport-negotiation summary. No live handshake, scanner transcript or organisational baseline is supplied.
- **Reproduction description / synthetic status:** the fictional record states that deprecated-protocol negotiation succeeds at the stated endpoint. **No network handshake was performed to create the pack.** Do not assign a protocol version, cipher, policy breach or downgrade exploit without evidence.
- **Confidence:** high for the narrow stipulated acceptance condition; other termination points, clients, policy compliance and exploitability remain unknown.
- **Potential impact:** weaker transport assurance and compatibility/security trade-offs for supplier communication. Actual intercepted data, downgrade success and mandated compliance consequences are not demonstrated.
- **Known:** public staging endpoint accepts a deprecated protocol; supplied 5.3. **Unknown:** exact negotiated protocol/cipher, gateway/CDN/load-balancer/backend coverage, client dependencies, approved baseline, production configuration and deployment options.
- **Recommended treatment:** inventory termination points and dependent clients; agree the applicable baseline and disable deprecated support with compatibility planning. If a temporary exception is proposed, define verified restrictions, residual risk, accountable authority and expiry/review; R1 grants no exception.
- **Recommended independent retest:** identified deployed endpoint/configuration rejects the specified deprecated protocol and permits approved protocols; test supported client journeys and all relevant termination points. Supply dated handshake/configuration and compatibility evidence rather than a “TLS enabled” screenshot.
- **Status:** **open**; no configuration correction, authorised exception or retest supplied.

### F6 — AI retrieval ACL filtering omitted from design; isolation untested

- **Asset and condition:** proposed AI summary pilot design describes retrieval and summarisation but omits ACL filtering. Retrieval isolation has not been tested. This is an **assurance gap, not a confirmed cross-supplier AI disclosure**.
- **Evidence and source:** S6 original fictional design excerpt. An omitted control in a brief document does not prove the runtime implementation lacks it; the missing assurance is nevertheless material to evaluating the pilot.
- **Reproduction description / synthetic status:** design-only review in the case; **no retrieval/model/cache/tool test or AI data disclosure was performed or demonstrated**. Future cross-scope tests are proposed, not reported outcomes.
- **Confidence:** high that the supplied excerpt does not document ACL filtering; runtime isolation is unassessed. No score, CVE or exploitation signal supplied.
- **Potential impact:** if unfiltered retrieval/caching/tool access is enabled, supplier data could enter another supplier's context, response or processing chain. Correct final wording does not establish prevention of earlier context disclosure.
- **Known:** planned summary pilot, omitted filtering detail and absent retrieval isolation tests. **Unknown:** authoritative ACL source/enforcement, pre-context filtering, cache keys/revocation, connectors/tools, provider data handling, scopes and actual runtime behaviour.
- **Recommended treatment:** do not enable confidential-data pilot use without assurance; retain an enforced disabled gate while design/evidence is established. Implement/review current resource permission before retrieval enters model context, scoped caches, rechecks and external tool authorisation. Read-only capability and prompt instructions do not themselves prevent disclosure.
- **Recommended independent retest:** SUP-A/SUP-B own-scope successes and cross-scope denial at retrieval, context, cache and response; revoked/changed permission tests; tool/budget/output/log checks where those features exist. Review approved provider/data flow and exact deployed revision. Any synthetic-only trial needs enforced segregation and authorised scope.
- **Status:** **open — assurance gap**; no design assurance, runtime test, remediation verification or retest supplied.

## 6. Short synthetic source pack

**All excerpts below are original invented case text.** They are not quotations from the PDFs, vendor correspondence, genuine logs or executed tests. They contain no working secret, real endpoint, exploit payload or actionable threat intelligence. Added fixture names simply make citations unambiguous. Each source belongs to the baseline fictional case dated **1 October 2026**; no clock timestamps or later work outcomes are implied.

| ID / fictional source | Short inert excerpt | Evidential boundary |
|---|---|---|
| S1 / vendor inventory note | “STG-GW: public staging gateway; fictional advisory fixture applicable. Supplied CVSS 3.1: 8.1; synthetic KEV: Yes; synthetic EPSS: 0.72. Production version/exposure: not supplied.” | Staging applicability is given. No real CVE/version/vector or local-compromise record. |
| S2 / assessment request-outcome note | “Build stg-2026.10.01-r1; identity SUP-A; illustrative read path /api/documents/DOC-B-TRAINING; response contains SUP-B-DOCUMENT-MARKER. Document owner: SUP-B.” | Stipulated fictional foreign-content outcome; no actual request/status/body capture and no whole-route coverage. |
| S3 / redacted logging and access note | “Portal log contains [LOG-CREDENTIAL-MARKER — INERT, NOT A SECRET]. Underlying case credential: active. Log readers include SUPPORT-WIDE. Credential authority and use history: not supplied.” | Marker substitutes for the fictional exposure; not a token or evidence of actual misuse. |
| S4 / vendor dependency scanner summary | “BUILD-LIB-FIXTURE: flagged fictional advisory; supplied CVSS 3.1: 9.8; vendor classification: build-only. Exploitation path, CI privilege analysis and artifact impact: not verified.” | Scanner assertion only; no real advisory or independently validated component path. |
| S5 / assessment transport note | “Public staging portal endpoint accepts a deprecated TLS protocol. Supplied CVSS 3.1: 5.3. Protocol/cipher and other termination-point details: not supplied.” | Narrow stipulated acceptance condition; no live handshake or policy-compliance verdict. |
| S6 / proposed AI design excerpt | “Supplier asks for a document summary. Retrieval obtains matching material; model prepares a summary. ACL filtering and cache permission handling are not described. Retrieval isolation tests: not performed.” | Design assurance gap; no executed retrieval or demonstrated foreign-content disclosure. |

## 7. Coverage and limitations

| Area | Coverage supplied by R1 | Remaining limit |
|---|---|---|
| Resource authorisation | F2's narrow two-supplier staging failure | Not a complete route/role/data matrix; no production inference. |
| Credentials/logs | F3 exposure/activity/readership case facts | No full sink inventory, secret authority or investigation result. |
| Gateway/dependencies | F1 staging applicability; F4 vendor scanner flag | Real advisory mapping/vectors absent; production/CI uncertainty persists. |
| Transport | F5 single stated public staging condition | Exact protocol/cipher, client matrix and other terminations absent. |
| AI | F6 design excerpt and absent assurance | No retrieval/context/cache/tool isolation tests. |
| Other PDF agenda/control topics | Background only | No “passed” claim for injection, input validation, CORS, rate limits, JWT validation, headers or middleware effectiveness. |
| Remediation | Recommended actions only | No completed containment/change/deployment/independent retest. |

The assessment date does not establish freshness after code/configuration changes. The build label is not an artifact hash or proof of deployment equivalence. No actual raw evidence package, sign-off, contractual entitlement, organisational policy, response interval, risk-acceptance authority or SLA is provided. The synthetic source pack is suitable for evidence reasoning, not audit certification.

## 8. Remediation status and handover

**Baseline status: F1–F6 all open; retest not performed.** Recommendations do not constitute completed work. Owner assignments, priorities, negotiated targets and the board decision are learner deliverables, deliberately not supplied here.

Any later evidence should identify finding/source, environment, version/configuration, approved change, expected/actual check, coverage limits, date and independent reviewer. Keep correction, deployment, verification, incident disposition and authorised residual-risk acceptance distinct. Do not overwrite baseline R1 with a workshop hypothetical or close findings on a vendor assurance alone.