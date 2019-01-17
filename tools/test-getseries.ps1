# Make sure we don't fail silently.
$ErrorActionPreference = 'Stop'
# Fail on uninitialized variables and non-existing properties.
Set-StrictMode -Version Latest

$today = get-date -format "yyyy-MM-dd"
$tomorrow = get-date (get-date).AddDays(1) -format "yyyy-MM-dd"
(invoke-webrequest "http://localhost:5002/api/getseries?sn=00-00-000-000&from=$today&to=$tomorrow").content | convertfrom-json | convertto-json
