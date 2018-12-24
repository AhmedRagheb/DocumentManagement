Write-Host "['Building project in release mode']"

$baseHref = "/"
ng build --env=release --prod --base-href=$baseHref
