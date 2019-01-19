# Make sure we don't fail silently.
$ErrorActionPreference = 'Stop'
# Fail on uninitialized variables and non-existing properties.
Set-StrictMode -Version Latest

Push-Location
Try
{
	$projectRoot = Join-Path $PSScriptRoot ..\QboxNext.SimulateQbox
	cd $projectRoot
	dotnet build
}
Finally
{
	Pop-Location
}

$exePath = Join-Path $PSScriptRoot ..\QboxNext.SimulateQbox\bin\Debug\netcoreapp2.1\QboxNext.SimulateQbox.dll
dotnet $exePath "--qserver=http://localhost:5000" "--qboxserial=00-00-000-000" "--metertype=smart" "--pattern=181:flat(2);182:zero;281:zero;282:zero;2421:zero" --isduo
