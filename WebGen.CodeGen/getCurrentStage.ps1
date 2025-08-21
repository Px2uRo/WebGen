param (
    [string]$InputValue = "",
    [string]$Configuration = "Debug",
    [string]$OfflinePackagesDir = "D:\MyNupkgs"
)
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$buildFile = "Packing.txt"

# ����ļ������ڣ���ʼ��Ϊ false
if (-Not (Test-Path $buildFile)) {
    "false" | Out-File $buildFile 
}

# ��ȡ��ǰֵ
$MyBool = (Get-Content $buildFile).Trim()

# �ж��Ƿ���Ҫ����д��
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