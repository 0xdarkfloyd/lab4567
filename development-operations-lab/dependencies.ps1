[CmdletBinding()]
param([switch]$DownloadDependencies)
$ErrorActionPreference = 'Stop'
$url = 'https://repo.maven.apache.org/maven2/com/h2database/h2/2.3.232/h2-2.3.232.jar'
$sha256 = '8dae62d22db8982c3dcb3826edb9c727c5d302063a67eef7d63d82de401f07d3'
$destination = Join-Path $PSScriptRoot 'lib\h2-2.3.232.jar'
if (Test-Path $destination) {
    if ((Get-FileHash -Algorithm SHA256 $destination).Hash.ToLowerInvariant() -ne $sha256) {
        throw 'Existing H2 artifact failed SHA-256 verification. Remove it manually and retry with explicit download consent.'
    }
    return $destination
}
if (-not $DownloadDependencies) {
    throw 'H2 is absent. Review dependencies.ps1, then explicitly supply -DownloadDependencies. No network request was made.'
}
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$null = New-Item -ItemType Directory -Force -Path (Split-Path $destination)
$temporary = "$destination.$([Guid]::NewGuid().ToString('N')).partial"
try {
    # Fixed HTTPS origin/version; redirects forbidden. This downloads data, never executes the JAR.
    Invoke-WebRequest -UseBasicParsing -MaximumRedirection 0 -Uri $url -OutFile $temporary
    if ((Get-FileHash -Algorithm SHA256 $temporary).Hash.ToLowerInvariant() -ne $sha256) {
        throw 'Downloaded H2 artifact failed SHA-256 verification.'
    }
    Move-Item -LiteralPath $temporary -Destination $destination
} finally {
    if (Test-Path $temporary) { Remove-Item -LiteralPath $temporary -Force }
}
return $destination