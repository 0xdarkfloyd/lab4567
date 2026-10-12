# Reviewed H2 dependency

- Coordinates: `com.h2database:h2:2.3.232` (MPL 2.0 / EPL 1.0 dual licence; review upstream licensing for redistribution).
- Fixed artifact: https://repo.maven.apache.org/maven2/com/h2database/h2/2.3.232/h2-2.3.232.jar
- Official SHA-256: https://repo.maven.apache.org/maven2/com/h2database/h2/2.3.232/h2-2.3.232.jar.sha256
- Pinned SHA-256: **8dae62d22db8982c3dcb3826edb9c727c5d302063a67eef7d63d82de401f07d3**
- Official SHA-1 (provenance cross-check, not the script's security pin): https://repo.maven.apache.org/maven2/com/h2database/h2/2.3.232/h2-2.3.232.jar.sha1
- Observed SHA-1: `4fcc05d966ccdb2812ae8b9a718f69226c0cf4e2`.

On 2026-10-12 the official SHA-256/SHA-1 sidecars were fetched and a separate artifact download was hashed with PowerShell `Get-FileHash`. The artifact hashes matched the published sidecars. This is provenance/integrity evidence, **not a publisher signature or proof of absence of vulnerabilities**. The established SHA-256 is committed in [dependencies.ps1](dependencies.ps1); execution does not trust a newly downloaded checksum.

The script refuses absent dependencies unless explicitly given `-DownloadDependencies`. It verifies existing JARs on every run, permits only the literal versioned HTTPS URL, requests TLS 1.2, rejects redirects, verifies a temporary file before moving it into place, and cleans up partial downloads. A corrupt existing artifact fails closed; there is no silent replacement, mirror fallback, Maven installation, dynamic version, JAR execution or checksum bypass.

After a verified download, all build/demo/test commands operate offline. Local classpath loading of the JDBC library during tests is expected; downloading it alone never executes it. Internet access to Maven Central is necessary only for an explicitly consented missing-dependency download. Generated dependency and build directories are ignored by [.gitignore](.gitignore).