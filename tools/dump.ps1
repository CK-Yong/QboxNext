[CmdletBinding()]
Param(
	[Parameter(Mandatory=$true)]
	[string]$qboxserial
)	

# Make sure we don't fail silently.
$ErrorActionPreference = 'Stop'
# Fail on uninitialized variables and non-existing properties.
Set-StrictMode -Version Latest

Push-Location
Try
{
	$projectRoot = Join-Path $PSScriptRoot ..\QboxNext.DumpQbx
	cd $projectRoot
	dotnet build
}
Finally
{
	Pop-Location
}

function DumpQbx([string]$qboxSerial, [int]$counter)
{
	$exePath = Join-Path $PSScriptRoot ..\QboxNext.DumpQbx\bin\Debug\netcoreapp2.1\QboxNext.DumpQbx.dll
	dotnet $exePath "--qbx=D:\QboxNextData\Qbox_$qboxserial\$qboxserial_" "--qboxserial=00-00-000-000" "--metertype=smart" "--pattern=181:flat(2);182:zero;281:zero;282:zero;2421:zero"
}
