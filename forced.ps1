param (
    [string]$GeneratorProj,
    [string]$TargetProj
)

# 1. 找 .sln 文件
$sln = Get-ChildItem *.sln | Select-Object -First 1
if (-not $sln) {
    Write-Error "❌ 未找到解决方案文件 (.sln)"
    exit 1
}

# 2. 获取所有项目定义
$slnContent = Get-Content $sln.FullName
$projectLines = $slnContent | Where-Object { $_ -match '^Project\(.*?\)\s=\s".+?",\s*".+?",\s*"\{.+?\}"' }

# 3. 构造项目名 => GUID 映射
$projects = @{}
foreach ($line in $projectLines) {
    if ($line -match '^Project\(.*?\)\s=\s"(.+?)",\s*".+?",\s*"\{(.+?)\}"') {
        $name = $matches[1]
        $guid = "{$($matches[2])}"
        $projects[$name] = $guid
    }
}

# 4. 目标项目模糊匹配
$matchedProj = $projects.Keys | Where-Object { $_ -like "*$TargetProj*" } | Select-Object -First 1
if (-not $matchedProj) {
    Write-Error "❌ 找不到目标项目 '$TargetProj'，可用名称如下："
    $projects.Keys | ForEach-Object { "  - $_" }
    exit 2
}

$projectGuid = $projects[$matchedProj]
Write-Host "✅ 匹配到目标项目：$matchedProj (GUID: $projectGuid)"

# 5. 查找 Generator 项目路径
$generatorProjPath = Get-ChildItem -Recurse -Filter "$GeneratorProj.csproj" | Select-Object -First 1
if (-not $generatorProjPath) {
    Write-Error "❌ 找不到生成器项目 '$GeneratorProj.csproj'"
    exit 3
}

$userFile = "$($generatorProjPath.FullName).user"

# 6. 写入 .csproj.user 文件
$xml = @"
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <TargetProject>$projectGuid</TargetProject>
  </PropertyGroup>
</Project>
"@

$xml | Set-Content $userFile -Encoding UTF8
Write-Host "`n🚀 已设置调试目标项目为：$matchedProj"
Write-Host "📝 写入文件：$userFile"
