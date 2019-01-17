# Make sure we don't fail silently.
$ErrorActionPreference = 'Stop'
# Fail on uninitialized variables and non-existing properties.
Set-StrictMode -Version Latest

(invoke-webrequest "http://localhost:5001/api/getlivedata?sn=15-49-001-047").content | convertfrom-json | convertto-json
