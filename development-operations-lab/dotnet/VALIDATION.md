# Validation status

## Environment constraint

Authoring environment: Windows PowerShell 5.1; `dotnet --list-sdks` reports **2.1.526 only**. No standalone C# compiler was discovered. Both projects target **net8.0** and use C# 12 syntax. No SDK was installed, no NuGet restore was attempted, and **neither project has been compiled or executed**. No C# compiler/syntax-validator success or 37/37 runtime success is claimed.

## Static checks

Observed offline checks passed: both project XML files and NuGet XML parse; PowerShell launcher parses; all 37 harness case names/ordering exactly match current Java (12 lab1, 25 lab2); target, direct dependency pin and isolated explicit compile includes match requirements. These checks do not prove C# type correctness, SQLite native loading, runtime behavior, concurrency correctness or package security. The launcher rejects the installed old SDK before any restore/build operation. Validation caught PowerShell's terminating `Write-Error` overriding the intended exit code; prerequisite diagnostics now explicitly continue to `exit 2`.

Final launcher observations: exercise and fixed, each invoked with `-Command test -Restore`, both returned **exit 2** with no per-track obj directory created. Final PowerShell parsing reported **0 errors**. There are 22 authored files in this subtree. No package download or C# build occurred.

## Required follow-up with approved tooling

1. Select an approved .NET 8+ SDK and suitable net8.0 runtime; ensure no ancestor SDK pin selects 2.1.
2. Explicitly restore each project with the launcher's `-Restore` switch. Evaluate package advisories and the native dependency chain.
3. Run fixed `test`, `lab1`, `lab2`: expect 37, 12 and 25 tests respectively, all PASS/exit 0. Repeat race cases via repeated full lab2 runs.
4. Run exercise tests: failures must remain FAIL/nonzero. Record the actual counts rather than copying a prediction.
5. Run demo for each track: compare six mock broker cases and scripted model labels. Do not treat demo exit 0 as security validation.
6. Verify missing/stale assets without `-Restore` fail rather than silently downloading packages; inspect restore/build failures separately from security findings.

Only the .NET subtree is authored by this task; Java fixture/source, parent scripts, decks and root documentation are unchanged.