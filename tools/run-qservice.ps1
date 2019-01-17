# Make sure we don't fail silently.
$ErrorActionPreference = 'Stop'
# Fail on uninitialized variables and non-existing properties.
Set-StrictMode -Version Latest

Push-Location
Try
{
	$projectRoot = Join-Path $PSScriptRoot ..\QboxNext.Qservice
	cd $projectRoot
	dotnet build
}
Finally
{
	Pop-Location
}

$exePath = Join-Path $PSScriptRoot ..\QboxNext.Qservice\bin\Debug\netcoreapp2.1\QboxNext.Qservice.dll
dotnet $exePath
