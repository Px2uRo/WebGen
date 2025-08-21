using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WebGen.CodeGen
{
    [Generator]
    public class Genrator: ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // 这里可以添加初始化代码，比如注册源生成器
            // 目前没有需要初始化的内容，所以留空
        }
        public void Execute(GeneratorExecutionContext context)
        {
            foreach (var item in context.AdditionalFiles)
            {
                var a = item.GetText();
            }
            foreach (var item in context.Compilation.SyntaxTrees)
            {

            }
        }
    }
}
