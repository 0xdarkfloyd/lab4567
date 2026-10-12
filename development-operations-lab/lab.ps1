[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][ValidateSet('exercise','fixed')][string]$Track,
    [ValidateSet('build','demo','test','lab1','lab2')][string]$Command = 'demo',
    [switch]$DownloadDependencies,
    [switch]$List
)
$ErrorActionPreference = 'Stop'
if ($List) {
    Write-Output 'Tracks: exercise | fixed. Commands: build | demo | test | lab1 | lab2. lab1/lab2 run independent test subsets.'
    exit 0
}
$jar = & (Join-Path $PSScriptRoot 'dependencies.ps1') -DownloadDependencies:$DownloadDependencies
$null = Get-Command javac -ErrorAction Stop
$null = Get-Command java -ErrorAction Stop
$output = Join-Path $PSScriptRoot "build\$Track"
if (Test-Path $output) { Remove-Item -LiteralPath $output -Recurse -Force }
$null = New-Item -ItemType Directory -Force -Path $output
# Select exactly one implementation track. Both tracks intentionally define identical class names.
$sources = @(Get-ChildItem (Join-Path $PSScriptRoot 'common\src') -Recurse -Filter '*.java' | ForEach-Object FullName)
$sources += @(Get-ChildItem (Join-Path $PSScriptRoot "$Track\src") -Recurse -Filter '*.java' | ForEach-Object FullName)
& javac --release 17 -encoding UTF-8 -cp $jar -d $output @sources
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
if ($Command -eq 'build') { Write-Output "Compiled Java 17 target: $Track"; exit 0 }
& java -cp "$output;$jar" training.operations.Main $Track $Command
exit $LASTEXITCODE