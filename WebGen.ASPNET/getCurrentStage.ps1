param (
    [string]$InputValue = "",
    [string]$Configuration = "Debug",
    [string]$OfflinePackagesDir = "D:\MyNupkgs",
    [string]$DirectSet = ""
)

chcp 936
$cD = (Get-Location).Path

$parentDir = $cD | Split-Path -Parent

$Pa = Join-Path $parentDir "AskedToPack.txt"
$buildFile    = "Packing.txt"
$buildingFile = "buildnumber.txt"

# 确保文件存在
if (-not (Test-Path $buildFile))    { "false" | Out-File $buildFile }
if (-not (Test-Path $buildingFile)) { "0"     | Out-File $buildingFile }
if (-not (Test-Path $Pa)) { "false"     | Out-File $Pa }

function Increment-Build {
    $ft = [string](Get-Content $Pa)
    if($ft -ne "true")
    {
        exit 0;
    }
    $num = [int](Get-Content $buildingFile)
    dotnet pack -c $Configuration -o $OfflinePackagesDir -p:PackageVersion=0.0.0.$num
    if ($LASTEXITCODE -ne 0) {
        Write-Error "dotnet pack failed with exit code $LASTEXITCODE"
        # 在这里放你需要执行的代码，比如发通知/回滚/退出等

        "false" | Out-File $buildFile
        exit $LASTEXITCODE
    } 
    $num++
    $num | Out-File $buildingFile -Encoding UTF8
}

# 直接指定
if ($DirectSet) {
    $DirectSet | Out-File $buildFile
    Write-Output $DirectSet
    exit 0
}

# 主逻辑
$current = (Get-Content $buildFile).Trim()

if ($InputValue) {
    $InputValue | Out-File $buildFile
    Write-Output $InputValue
    if ($InputValue -eq "false") { Increment-Build }
}
else {
    Write-Output $current
    if ($current -eq "false") {
        "true" | Out-File $buildFile   # 默认改成 true，否则会陷入无限 false
        Increment-Build
    }
}
