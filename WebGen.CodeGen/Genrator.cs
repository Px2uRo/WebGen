using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using WebGen.Converters;

namespace WebGen.CodeGen
{
    public static class InvocationExpressionSyntaxUtil
    {
        public static bool Invocated(this BlockSyntax body, string methodName)
        {
            foreach (var stmt in body.Statements.OfType<ExpressionStatementSyntax>())
            {
                if (stmt.Expression is InvocationExpressionSyntax invo)
                {
                    return invo.Invocated(methodName);
                }
            }
            return false;
        }

        public static bool Invocated(this InvocationExpressionSyntax invocation, string methodName)
        {
            ExpressionSyntax expr = invocation.Expression;

            while (expr is MemberAccessExpressionSyntax member)
            {
                if (member.Name.Identifier.Text == methodName)
                {
                    return true;
                }
                expr = member.Expression; // 继续往左走
            }

            if (expr is IdentifierNameSyntax idn)
            {
                return idn.Identifier.Text == methodName;
            }

            return false;
        }

    }
    [Generator]
    public class WXAMLGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // 这里可以添加初始化代码，比如注册源生成器
            // 目前没有需要初始化的内容，所以留空
        }


        public void Execute(GeneratorExecutionContext context)
        {
            //https://github.com/Px2uRo/WebGen 就是 clr的namespace的"WebGen.Controls"
            //http://schemas.microsoft.com/winfx/2006/xaml 是 XAML 标准文档（应该是）。
            //在这里，HTML 里面没有说把 html 转换成对象的说法，
            //所以我们要把 wxaml 直接转换成 html，
            //并且在相应的 JS 用 DOM API 来操作。

            //并且我会 编译时 按需提供 IPageConverter 的实现，以及那些工厂的实现。也就是说这些东西在 .g.cs 里面。

            List<ExpressionSyntax> MinimalAPIExpressions = new List<ExpressionSyntax>();
            var useASP = false; foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var root = syntaxTree.GetRoot();

                // 查找所有类声明
                var classDeclarations = root.DescendantNodes()
                                            .OfType<ClassDeclarationSyntax>();

                foreach (var classDecl in classDeclarations)
                {
                    if (classDecl.Identifier.Text == "Program")
                    {
                        var Main = classDecl.Members.OfType<MethodDeclarationSyntax>().FirstOrDefault(m => m.Identifier.Text == "Main" && m.Modifiers.Any(SyntaxKind.StaticKeyword));
                        useASP = Main.Body.Invocated("UseASPNET");
                    }
                }
            }
            //foreach (var a in context.Compilation.SyntaxTrees.Where())
            //{
            //}
            foreach (var a in context.AdditionalFiles.Where(x => x.Path.EndsWith(".wxaml", StringComparison.OrdinalIgnoreCase)))
            {
                string wxaml = a.GetText(context.CancellationToken)?.ToString();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(wxaml);

                XmlElement root = doc.DocumentElement;
                string baseClass = null;
                if (root.NamespaceURI == "https://github.com/Px2uRo/WebGen")
                {
                    baseClass = "WebGen.Controls." + root.Name;
                }

                string classFullName = root.GetAttribute("Class", "http://schemas.microsoft.com/winfx/2006/xaml");
                int lastDot = classFullName.LastIndexOf('.');
                string namespaceName = lastDot > 0 ? classFullName.Substring(0, lastDot) : "";
                string className = lastDot >= 0 ? classFullName.Substring(lastDot + 1) : classFullName;

                NamespaceDeclarationSyntax @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName)).NormalizeWhitespace();
                ClassDeclarationSyntax myClass =
                SyntaxFactory.ClassDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseClass)))
                .NormalizeWhitespace();

                @namespace = @namespace.AddMembers(myClass);
                var code = @namespace.NormalizeWhitespace().ToFullString();
                context.AddSource(Path.GetFileNameWithoutExtension(a.Path) + ".g.cs", SourceText.From(code));
            }

            //wwwrootResources
        }
    }
}
