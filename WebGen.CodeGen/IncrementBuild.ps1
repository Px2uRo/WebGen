
# �� UTF-8 ���
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

$buildFile = "buildnumber.txt"

if (-Not (Test-Path $buildFile)) {
    0 | Out-File $buildFile -Encoding UTF8
}

$num = [int](Get-Content $buildFile)
$num++
$num | Out-File $buildFile -Encoding UTF8

Write-Output $num