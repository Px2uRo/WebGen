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
using System.Threading;
using System.Xml;
using WebGen.Converters;

namespace WebGen.CodeGen
{
    public class ClassDeclarationReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> Candidates { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // 收集所有类声明
            if (syntaxNode is ClassDeclarationSyntax cds)
            {
                Candidates.Add(cds);
            }
        }
    }
    public static class SyntaxTreeUtil
    {
        public static string GetFullName(this ClassDeclarationSyntax classDecl)
        {
            string name = classDecl.Identifier.Text;
            SyntaxNode? parent = classDecl.Parent;

            while (parent is not null)
            {
                if (parent is NamespaceDeclarationSyntax ns)
                {
                    name = $"{ns.Name}.{name}";
                }
                else if (parent is FileScopedNamespaceDeclarationSyntax fns)
                {
                    name = $"{fns.Name}.{name}";
                }
                else if (parent is ClassDeclarationSyntax parentClass)
                {
                    // 处理嵌套类
                    name = $"{parentClass.Identifier.Text}+{name}";
                }

                parent = parent.Parent;
            }

            return name;
        }

    }
    public static class InvocationExpressionSyntaxUtil
    {
        public static bool HasInvocated(this BlockSyntax body, string toDisplayString,SemanticModel semantic)
        {
            foreach (var stmt in body.Statements.OfType<ExpressionStatementSyntax>())
            {
                if (stmt.Expression is InvocationExpressionSyntax invo)
                {
                    if(!invo.HasInvocated(toDisplayString,semantic))continue;
                    return true;
                }
            }
            return false;
        }



        public static bool HasInvocated(this InvocationExpressionSyntax invocation, string toDisplayString, SemanticModel semantic)
        {

            ExpressionSyntax expr = invocation;

            while (expr is InvocationExpressionSyntax invo)
            {
                // 获取当前调用的方法符号
                if (invo.Expression is MemberAccessExpressionSyntax ma)
                {
                    var symbol = semantic.GetSymbolInfo(ma).Symbol as IMethodSymbol;
                    var a = symbol?.ToDisplayString();
                    if (symbol?.ToDisplayString() == toDisplayString)
                        return true;

                    expr = ma.Expression; // 往左钻
                }
                else
                {
                    break;
                }
            }
            return false;
        }

    }
    [Generator]
    public class WXAMLGenerator : ISourceGenerator
    {
        public bool UseASPNET { get; set; } = false;
        public void Initialize(GeneratorInitializationContext context)
        {
            // 这里可以添加初始化代码，比如注册源生成器
            // 目前没有需要初始化的内容，所以留空
        }
        public void HandleAppCode(GeneratorExecutionContext context)
        {
            if(!UseASPNET)return;

            foreach (var stree in context.Compilation.SyntaxTrees)
            {
                var classDeclarations = stree.GetRoot().DescendantNodes()
                                            .OfType<ClassDeclarationSyntax>();
                foreach (var classDecl in classDeclarations)
                {
                    if (classDecl.Identifier.Text == "App")
                    {
                        var sema = context.Compilation.GetSemanticModel(stree);
                        var namespaceNode = classDecl.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
                        string namespaceName = namespaceNode?.Name.ToString() ?? "";

                        NamespaceDeclarationSyntax @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName)).NormalizeWhitespace();

                        string overridedRun = "{";
                        foreach (var item in MinimalAPIExpressions)
                        {
                            overridedRun += item.ToFullString();
                        }
                        overridedRun += Environment.NewLine;
                        overridedRun += "base.Run();}";
                        var AppSyn = SyntaxFactory.ClassDeclaration("App")
                               .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                               .AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword))
                               .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("WebGen.ASPNET.WebGenASPApplication")))
                               .AddMembers(SyntaxFactory.MethodDeclaration(
                               SyntaxFactory.PredefinedType(
                                   SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                               SyntaxFactory.Identifier("Run"))
                               .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                               .AddModifiers(SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
                               .WithBody((BlockSyntax)SyntaxFactory.ParseStatement(overridedRun
           ))).NormalizeWhitespace();
                        @namespace = @namespace.AddMembers(AppSyn);
                        @namespace = @namespace.AddUsings(
                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Text.Json")),
                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Text.Json.Serialization")),
                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("WebGen.ASPNET"))
                            );
                        var code2 = @namespace.NormalizeWhitespace().ToFullString();
                        code2 =
            @"/*
<auto-generated>
     由 WebGen.CodeGen.AppGenertator 自动生成。
     请勿手动更改此文件，更改会在下次生成时被覆盖。
</auto-generated>
*/"
            + Environment.NewLine
            + code2;

                        context.AddSource("App.g.cs", SourceText.From(code2, Encoding.UTF8));

                    }
                }

            }
        }

        List<ExpressionSyntax> MinimalAPIExpressions { get; set; } = new List<ExpressionSyntax>();
        public void HandleWXAMLs(GeneratorExecutionContext context)
        {
            //https://github.com/Px2uRo/WebGen 就是 clr的namespace的"WebGen.Controls"
            //http://schemas.microsoft.com/winfx/2006/xaml 是 XAML 标准文档（应该是）。
            //在这里，HTML 里面没有说把 html 转换成对象的说法，
            //所以我们要把 wxaml 直接转换成 html，
            //并且在相应的 JS 用 DOM API 来操作。

            //并且我会 编译时 按需提供 IPageConverter 的实现，以及那些工厂的实现。也就是说这些东西在 .g.cs 里面。

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

                if (UseASPNET)
                {
                    /*
            this.DynamicSource.AddRoute(""/todos"", async context =>
            {
                var todos = new Todo[]
                {
                    new Todo(1,""Learn WebGen"",DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                    new Todo(2,""Build awesome apps"",DateOnly.FromDateTime(DateTime.Now.AddDays(7)),true)
                };
                context.Response.ContentType = ""application/json"";
                await JsonSerializer.SerializeAsync(context.Response.Body, todos, AppJsonSerializerContext.Default.TodoArray);
            });

                     */
                    if(baseClass.ToString() == "WebGen.Controls.RoutePage" && root.GetAttribute("Prefix") is string pref)
                    {
                        if (string.IsNullOrWhiteSpace(root.GetAttribute("Prefix")))
                        {
                            var syntaxReceiver = (ClassDeclarationReceiver)context.SyntaxReceiver;
                            foreach (var classDecl in syntaxReceiver.Candidates)
                            {
                                if (classDecl.GetFullName() == classFullName)
                                {
                                    var location = classDecl.GetLocation().GetLineSpan();
                                    var lineNumber = location.StartLinePosition.Line + 1;
                                    var filePath = location.Path;

                                    // TODO 给编译器报一个提示，展示行号
                                    context.ReportDiagnostic(Diagnostic.Create(
                                        new DiagnosticDescriptor("WGN001", "需要 Prefix。",
                                            $"类 {classDecl.Identifier.Text} 在 {filePath}:{lineNumber}",
                                            "SourceGen", DiagnosticSeverity.Info, true),
                                        classDecl.GetLocation()));
                                }
                            }
                        }
                        else
                        {
                            var text = @"this.DynamicSource.AddRoute(""" + pref + @""", async context =>
            {
                context.Response.ContentType = ""application/json"";
                await context.Response.WriteAsync(""{}"");
            });";
                            MinimalAPIExpressions.Add((InvocationExpressionSyntax)SyntaxFactory.ParseExpression(
                                text));
                        }
                    }
                }

                @namespace = @namespace.AddMembers(myClass);
                var code = @namespace.NormalizeWhitespace().ToFullString();
                code = 
@"/*
<auto-generated>
     由 WebGen.CodeGen.WXAMLGenerator 自动生成。
     请勿手动更改此文件，更改会在下次生成时被覆盖。
</auto-generated>
*/" 
+ Environment.NewLine 
+code;
                context.AddSource(Path.GetFileNameWithoutExtension(a.Path) + ".g.cs", SourceText.From(code,Encoding.UTF8));
            }

            //wwwrootResources
        }
        public bool GetASPUsage(GeneratorExecutionContext context)
        {
            var useASP = false; 
            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var root = syntaxTree.GetRoot();

                // 查找所有类声明
                var classDeclarations = root.DescendantNodes()
                                            .OfType<ClassDeclarationSyntax>();

                foreach (var classDecl in classDeclarations)
                {
                    if (classDecl.Identifier.Text == "Program")
                    {
                        var sema = context.Compilation.GetSemanticModel(syntaxTree);
                        var Main = classDecl.Members.OfType<MethodDeclarationSyntax>().FirstOrDefault(m => m.Identifier.Text == "Main" && m.Modifiers.Any(SyntaxKind.StaticKeyword));
                        useASP = Main.Body.HasInvocated("WebGen.ASPNET.WebGenASPApplication.UseASPNET()", sema);
                    }
                }
            }
            return useASP;

        }
        public void Execute(GeneratorExecutionContext context)
        {
            UseASPNET = GetASPUsage(context);
            HandleWXAMLs(context);
            HandleAppCode(context);
        }
    }
}
