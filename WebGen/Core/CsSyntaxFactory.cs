using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Converters;
using WebGen.Converters.CSharp;

namespace WebGen.Core
{

    public class CSSyntaxConverterFactory
    {
        public readonly Dictionary<Type, CSSyntaxConverter> Converters = new();
        public void Register(Type syntaxType, CSSyntaxConverter converter)
        {
            Converters[syntaxType] = converter;
        }
        public string ConvertSyntaxToJSString(SyntaxNode syntax)
        {
            if (Converters.TryGetValue(syntax.GetType(), out var converter))
            {
                return converter.ConvertToJSString(syntax);
            }
            else
            {
                throw new InvalidOperationException($"不支持{syntax.Kind()}");
            }
        }
        /// <summary>
        /// 转换入口，将 C# 代码转换为 JavaScript 代码。
        /// </summary>
        /// <param name="csharpCode">C# 代码</param>
        /// <returns>JS 代码</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal string Convert(string csharpCode)
        {
            var tree = CSharpSyntaxTree.ParseText(csharpCode);
            var root = tree.GetRoot() as CompilationUnitSyntax;

            if (root == null)
            {
                throw new InvalidOperationException("无法解析代码：输入的 C# 代码不完整或无效。");
            }

            return ProcessClasses(root);
        }
        private string ProcessClasses(CompilationUnitSyntax root)
        {
            // 遍历所有类，找到方法。
            var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
            return string.Join("\n", classes.SelectMany(c => ProcessMethods(c)));
        }
        private IEnumerable<string> ProcessMethods(ClassDeclarationSyntax classNode)
        {
            var methods = classNode.DescendantNodes().OfType<MethodDeclarationSyntax>();
            return methods.Select(ConvertMethod);
        }
        /// <summary>
        /// 转换所有的方法为 JavaScript 函数。
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private string ConvertMethod(MethodDeclarationSyntax method)
        {
            var methodName = method.Identifier.Text;
            var parameters = string.Join(", ", method.ParameterList.Parameters.Select(p => p.Identifier.Text));
            var body = ConvertBody(method.Body);
            return $"function {methodName}({parameters}) {{ {body} }}";
        }
        private string ConvertBody(BlockSyntax body)
        {
            return body == null
                ? "/* 方法体为空 */\n"
                : string.Join(" ", body.Statements.Select(ConvertStatement));
        }
        private string ConvertStatement(SyntaxNode statement)
        {
            var a = statement.GetType();
            if (Converters.ContainsKey(statement.GetType()))
            {
                try
                {
                    return this.Converters[statement.GetType()].ConvertToJSString(statement);
                }
                catch (Exception op)
                {
                    return $"/*转换错误:{statement.ToString()}，发生了{op.GetType().Name}（{op}）*/\n";
                }
            }
            else
            {
                return $"/*转换未实现:{statement.ToString()}({statement.GetType()})*/\n";
            }
        }

    }
}
