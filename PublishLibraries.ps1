# README：
# 如何在 Visual Studio (2022) 中 加入新版的 Powershell 上下文菜单
# 首先 工具 -> 外部工具 -> 添加
# 标题：使用 Powershell 7 运行
# 命令：C:\Program Files\PowerShell\7\pwsh.exe 
#（这要求你使用 github 上的 msi 安装包安装 Powershell，不要使用微软商店的，Visual Studio 会打不开）
# 参数：-NoProfile -ExecutionPolicy Bypass -File "$(ItemPath)"
# 初始目录：$(ItemDir)
# 工具 -> 自定义 -> 命令 -> 上下文菜单 -> 项目和解决方案上下文菜单|项 -> 
# 添加命令-> 外部工具 2（如果你有以前的外部工具记得自己调） -> 下移到 使用 Powershell ISE 打开 的下面
# 然后 你可以在 任何 .ps1 文件上 右键 -> 外部工具 -> Powershell 来运行当前脚本

# 但是我的建议是自己写一个 VS 插件，因为这样加入的上下文菜单会有在其他项目也显示这个上下文菜单的 Bug。