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

function DumpQbx([string]$qboxSerial, [string]$counterId)
{
	$exePath = Join-Path $PSScriptRoot ..\QboxNext.DumpQbx\bin\Debug\netcoreapp2.1\QboxNext.DumpQbx.dll
	$qbxPath = "D:\QboxNextData\Qbox_$qboxserial\${qboxserial}_${counterId}.qbx"
	if (Test-Path $qbxPath)
	{
		Write-Output "Dumping $qbxPath..."
		dotnet $exePath "--qbx=$qbxPath" "--values" > "$qbxPath.txt"
	}
}

DumpQbx $qboxSerial "00000181"
DumpQbx $qboxSerial "00000181"
DumpQbx $qboxSerial "00000281"
DumpQbx $qboxSerial "00000282"
DumpQbx $qboxSerial "00002421"
DumpQbx $qboxSerial "00000001_Client0_secondary"
