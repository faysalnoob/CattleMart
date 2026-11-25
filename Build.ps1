$ErrorActionPreference = 'Stop'
$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
if (-not (Test-Path -LiteralPath $vswhere)) { throw 'Install Visual Studio with .NET desktop development and .NET Framework 4.7.2 targeting pack.' }
$build = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $build) { throw 'MSBuild was not found.' }
& $build (Join-Path $PSScriptRoot 'CattleCart.sln') /t:Rebuild /p:Configuration=Release /v:minimal /nologo
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
New-Item -ItemType Directory -Force (Join-Path $PSScriptRoot 'Run') | Out-Null
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'CattleCart\bin\Release\CattleCart.exe') -Destination (Join-Path $PSScriptRoot 'Run') -Force
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'CattleCart\bin\Release\CattleCart.exe.config') -Destination (Join-Path $PSScriptRoot 'Run') -Force
