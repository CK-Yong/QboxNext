# Make sure we don't fail silently.
$ErrorActionPreference = 'Stop'
# Fail on uninitialized variables and non-existing properties.
Set-StrictMode -Version Latest

Push-Location
Try
{
	$projectRoot = Join-Path $PSScriptRoot ..\QboxNext.Qserver
	cd $projectRoot
	dotnet build
}
Finally
{
	Pop-Location
}

$exePath = Join-Path $PSScriptRoot ..\QboxNext.Qserver\bin\Debug\netcoreapp2.1\QboxNext.Qserver.dll
dotnet $exePath
