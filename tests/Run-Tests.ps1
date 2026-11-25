$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
& (Join-Path $root 'Build.ps1')
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
$csc = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\Roslyn\csc.exe' | Select-Object -First 1
if (-not $csc) { throw 'C# compiler was not found.' }
$out = Join-Path $root 'CattleCart\bin\Release\RegressionTests.exe'
$ref = '/reference:' + (Join-Path $root 'CattleCart\bin\Release\CattleCart.exe')
& $csc /nologo /target:exe ('/out:'+$out) $ref (Join-Path $PSScriptRoot 'RegressionTests.cs')
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
& $out
exit $LASTEXITCODE
