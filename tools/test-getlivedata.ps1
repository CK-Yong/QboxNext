# Make sure we don't fail silently.
$ErrorActionPreference = 'Stop'
# Fail on uninitialized variables and non-existing properties.
Set-StrictMode -Version Latest

(invoke-webrequest "http://localhost:5002/api/getlivedata?sn=00-00-000-000").content | convertfrom-json | convertto-json
