param (
    [string]$InputValue = "",
    [string]$Configuration = "Debug",
    [string]$OfflinePackagesDir = "D:\MyNupkgs"
)
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$buildFile = "Packing.txt"

# 如果文件不存在，初始化为 false
if (-Not (Test-Path $buildFile)) {
    "false" | Out-File $buildFile 
}

# 读取当前值
$MyBool = (Get-Content $buildFile).Trim()

# 判断是否需要覆盖写入
if ($InputValue -ne "") {
    $InputValue | Out-File $buildFile
    Write-Output $InputValue

    if ($InputValue -eq "false") {
        dotnet pack -c $Configuration -o $OfflinePackagesDir
    }
} 
else {
    Write-Output $MyBool

    if ($MyBool -eq "false") {
        $InputValue | Out-File $buildFile
        dotnet pack -c $Configuration -o $OfflinePackagesDir
    }
}