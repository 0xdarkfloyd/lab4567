[CmdletBinding()]
param(
    [ValidateSet('exercise', 'fixed')][string]$Track = 'exercise',
    [ValidateSet('test', 'demo', 'lab1', 'lab2')][string]$Command = 'test',
    [switch]$Restore
)
$ErrorActionPreference = 'Stop'

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error 'A .NET 8 or newer SDK is required. Arrange installation with your administrator, then rerun. This script does not install software.' -ErrorAction Continue
    exit 2
}
$sdks = @(& dotnet --list-sdks)
if ($LASTEXITCODE -ne 0) { Write-Error 'Could not enumerate .NET SDKs.' -ErrorAction Continue; exit 2 }
$modern = @($sdks | Where-Object { $_ -match '^(\d+)\.' -and [int]$Matches[1] -ge 8 })
if ($modern.Count -eq 0) {
    Write-Error 'Installed SDKs cannot compile net8.0/C# 12. Install an approved .NET 8 or newer SDK yourself, then rerun. No SDK installation or NuGet restore was attempted.' -ErrorAction Continue
    exit 2
}

Push-Location $PSScriptRoot
try {
    $selected = & dotnet --version
    if ($LASTEXITCODE -ne 0 -or $selected -notmatch '^(\d+)\.' -or [int]$Matches[1] -lt 8) {
        Write-Error 'The SDK selected in this directory is older than 8 or unavailable. Check ancestor global.json SDK pins and select an installed .NET 8+ SDK.' -ErrorAction Continue
        exit 2
    }
    $project = Join-Path $PSScriptRoot "$Track\$Track.csproj"
    if ($Restore) {
        Write-Host 'Opt-in restore: NuGet HTTPS access may download Microsoft.Data.Sqlite 8.0.11 and its transitive/native dependencies.'
        & dotnet restore $project --configfile (Join-Path $PSScriptRoot 'NuGet.Config')
        if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
    } elseif (-not (Test-Path (Join-Path $PSScriptRoot "$Track\obj\project.assets.json"))) {
        Write-Error 'This track has not been restored. Rerun with -Restore to explicitly allow NuGet package restore. No implicit restore is performed.' -ErrorAction Continue
        exit 2
    }
    # No implicit package restore after the explicitly opted-in step.
    & dotnet run --project $project --no-restore -- $Track $Command
    exit $LASTEXITCODE
} finally { Pop-Location }