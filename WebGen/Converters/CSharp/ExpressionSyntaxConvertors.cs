using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebGen.Core;
using WebGen.WorlWideWeb.JS.Rules;

namespace WebGen.Converters.CSharp
{
    internal class MemberAccessExpressionSyntaxConvertor: CSSyntaxConverter
    {
        public MemberAccessExpressionSyntaxConvertor(CSSyntaxConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToJSString(SyntaxNode syntax)
        {
            var ma = syntax as MemberAccessExpressionSyntax;
            if (ma == null)
                return "";

            if (ma.Parent is InvocationExpressionSyntax invo)
            {
                var references = new List<MetadataReference>();
                var files = new List<string>()
                {
                    typeof(object).Assembly.Location,
                    typeof(Enumerable).Assembly.Location,
                    typeof(Console).Assembly.Location,
                    typeof(JSGlobalFunctionsConvertRule).Assembly.Location,
                    //TODO 项目引用时候直接用反射吧以后
                    @"I:\Xiong's\MyStudio\WebGen\WebGen\WebGen.Proj\bin\Debug\net6.0\WebGen.Proj.dll"
                };
                Parallel.For(0, files.Count, i =>
                {
                    var file = files[i];
                    if (System.IO.File.Exists(file))
                    {
                        references.Add(MetadataReference.CreateFromFile(file));
                    }
                });
                var compilation = CSharpCompilation.Create("MyCompilation")
                    .AddReferences(references)
                    .AddSyntaxTrees(ma.SyntaxTree);

                var semanticModel = compilation.GetSemanticModel(ma.SyntaxTree);

                var symbolInfo = semanticModel.GetSymbolInfo(ma);
                var symbol = symbolInfo.Symbol;

                if (symbol is IMethodSymbol me)
                {
                    if (me.ContainingType.ToDisplayString() == "WebGen.WorlWideWeb.JS.JSGlobalFunctions")
                    {
                        var converter = new WebGen.WorlWideWeb.JS.Rules.JSGlobalFunctionsConvertRule();
                        var convertMethod = typeof(WebGen.WorlWideWeb.JS.Rules.JSGlobalFunctionsConvertRule)
                            .GetMethod(me.Name, BindingFlags.Public | BindingFlags.Instance);

                        if (convertMethod == null)
                            return "/*请反馈*/";

                        // 提取参数（这里只处理 string 或常量）
                        var args = invo.ArgumentList.Arguments
                            .Select(arg =>
                            {
                                var constVal = semanticModel.GetConstantValue(arg.Expression);
                                return constVal.HasValue ? constVal.Value : null;
                            })
                            .ToArray();

                        if (args.Contains(null)) 
                            return "/*请反馈*/";

                        return convertMethod.Invoke(converter, args) as String;
                    }
                }
                    
                else
                    return "/*请反馈*/";
            }
            else
            {
                return "/*请反馈*/";
            }
            return "/*请反馈*/";
            //var compilation = CSharpCompilation.Create("MyCompilation")
            //    .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
            //    .AddSyntaxTrees(_factory.Tree);
            //var semanticModel = compilation.GetSemanticModel(_factory.Tree);

            //var ma = syntax as MemberAccessExpressionSyntax;
            ////if syntax is Method
            //var symbolInfo = semanticModel.GetSymbolInfo(ma.Parent);
            //var symbol = symbolInfo.Symbol;

            //return "";
        }
    }

    internal class ConditionalAccessExpressionSyntax : CSSyntaxConverter
    {
        public ConditionalAccessExpressionSyntax(CSSyntaxConverterFactory factory):base(factory)
        {
                
        }
        public override string ConvertToJSString(SyntaxNode syntax)
        {
            throw new NotImplementedException();
        }
    }

    internal class IdentifierNameSyntaxConvertor : CSSyntaxConverter
    {
        public IdentifierNameSyntaxConvertor(CSSyntaxConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToJSString(SyntaxNode syntax)
        {
            //这种东西的转换一般需要根据上下文来决定，所以有语义分析。
            var ma = syntax as IdentifierNameSyntax;
            if (ma == null)
                return "";

            if (ma.Parent is InvocationExpressionSyntax invo)
            {
                var references = new List<MetadataReference>();
                var files = new List<string>()
                {
                    typeof(object).Assembly.Location,
                    typeof(Enumerable).Assembly.Location,
                    typeof(Console).Assembly.Location,
                    typeof(JSGlobalFunctionsConvertRule).Assembly.Location,
                    //TODO 项目引用时候直接用反射吧以后
                    @"I:\Xiong's\MyStudio\WebGen\WebGen\WebGen.Proj\bin\Debug\net6.0\WebGen.Proj.dll"
                };
                Parallel.For(0, files.Count, i =>
                {
                    var file = files[i];
                    if (System.IO.File.Exists(file))
                    {
                        references.Add(MetadataReference.CreateFromFile(file));
                    }
                });
                var compilation = CSharpCompilation.Create("MyCompilation")
                    .AddReferences(references)
                    .AddSyntaxTrees(ma.SyntaxTree);

                var semanticModel = compilation.GetSemanticModel(ma.SyntaxTree);

                var symbolInfo = semanticModel.GetSymbolInfo(ma);
                var symbol = symbolInfo.Symbol;

                if (symbol is IMethodSymbol me)
                {
                    if (me.ContainingType.ToDisplayString() == "WebGen.WorlWideWeb.JS.JSGlobalFunctions")
                    {
                        var converter = new WebGen.WorlWideWeb.JS.Rules.JSGlobalFunctionsConvertRule();
                        var convertMethod = typeof(WebGen.WorlWideWeb.JS.Rules.JSGlobalFunctionsConvertRule)
                            .GetMethod(me.Name, BindingFlags.Public | BindingFlags.Instance);

                        if (convertMethod == null)
                            return "/*请反馈*/";

                        // 提取参数（这里只处理 string 或常量）
                        var args = invo.ArgumentList.Arguments
                            .Select(arg =>
                            {
                                var constVal = semanticModel.GetConstantValue(arg.Expression);
                                return constVal.HasValue ? constVal.Value : null;
                            })
                            .ToArray();

                        if (args.Contains(null))
                            return "/*请反馈*/";

                        return convertMethod.Invoke(converter, args) as String;
                    }
                }

                else
                    return "/*请反馈*/";
            }
            else
            {
                return "/*请反馈*/";
            }
            return "/*请反馈*/";
            //var compilation = CSharpCompilation.Create("MyCompilation")
            //    .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
            //    .AddSyntaxTrees(_factory.Tree);
            //var semanticModel = compilation.GetSemanticModel(_factory.Tree);

            //var ma = syntax as MemberAccessExpressionSyntax;
            ////if syntax is Method
            //var symbolInfo = semanticModel.GetSymbolInfo(ma.Parent);
            //var symbol = symbolInfo.Symbol;

            //return "";

        }
    }
}
