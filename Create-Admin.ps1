$ErrorActionPreference = 'Stop'
try {
    Add-Type -AssemblyName System.Data
    Add-Type -AssemblyName System.Configuration
    $exe = Join-Path $PSScriptRoot 'Run\CattleCart.exe'
    if (-not (Test-Path -LiteralPath $exe)) { throw 'Run executable missing. Extract the complete ZIP first.' }
    if (-not $env:CATTLECART_CONNECTION_STRING) {
        [xml]$config = Get-Content -LiteralPath ($exe + '.config')
        $env:CATTLECART_CONNECTION_STRING = [string]$config.configuration.connectionStrings.add.connectionString
    }
    [void][Reflection.Assembly]::LoadFrom($exe)
    [CattleCart.Class1]::InitializeDatabase()
    $adminName = Read-Host 'Admin name'
    $adminPhone = Read-Host 'Phone (11 digits, starts with 01)'
    $adminSecret = Read-Host 'Password (8-128 characters, letter, digit, special character)' -AsSecureString
    $credential = New-Object System.Net.NetworkCredential('', $adminSecret)
    $adminPassword = $credential.Password
    if ([string]::IsNullOrWhiteSpace($adminName) -or $adminName.Length -gt 100) { throw 'Enter a name of 1-100 characters.' }
    if (-not [CattleCart.AppSupport]::IsPhone($adminPhone)) { throw 'Invalid phone number.' }
    if ($adminPassword.Length -lt 8 -or $adminPassword.Length -gt 128 -or $adminPassword -notmatch '^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).+$') { throw 'Password does not meet the requirements.' }
    $connection = [CattleCart.Class1]::GetConnection()
    try {
        $connection.Open()
        $command = $connection.CreateCommand()
        $command.CommandText = 'INSERT INTO Admin(Name,Contact,Password) VALUES(@name,@phone,@password)'
        [void]$command.Parameters.AddWithValue('@name',$adminName.Trim())
        [void]$command.Parameters.AddWithValue('@phone',$adminPhone)
        [void]$command.Parameters.AddWithValue('@password',[CattleCart.Passwords]::Hash($adminPassword))
        [void]$command.ExecuteNonQuery()
    } finally { if ($command) { $command.Dispose() }; $connection.Dispose(); $adminPassword = $null }
    Write-Host 'Admin created. Select Admin on the login screen.' -ForegroundColor Green
} catch { Write-Host ('Setup failed: ' + $_.Exception.Message) -ForegroundColor Red; exit 1 }
