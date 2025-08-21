using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebGen.CodeGen
{
    /// <summary>
    /// 这个类用来把App.cs中的App类处理成App.g.cs的内容。
    /// </summary>
    internal class AppProcesser
    {
        public string Process(string appCode)
        {
            
            // 这里可以添加处理逻辑，将 appCode 转换为 App.g.cs 的内容
            // 例如，解析 appCode，提取类名、方法等信息，并生成新的代码
            // 目前只是简单返回原始代码
            return appCode;
        }
    }
    internal static class ProcesserManager
    {

    }
}