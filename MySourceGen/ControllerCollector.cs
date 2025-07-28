
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySourceGen
{

    public class ControllerCollector
    {
        public static Dictionary<ClassDeclarationSyntax,SyntaxTree> CollectControllers(GeneratorExecutionContext context)
        {
            var controllers = new Dictionary<ClassDeclarationSyntax, SyntaxTree>();
            var compilation = context.Compilation;

            foreach (var tree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(tree);
                var root = tree.GetRoot();

                var classDeclarations = root.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>();

                foreach (var classDecl in classDeclarations)
                {
                    var symbol = semanticModel.GetDeclaredSymbol(classDecl);
                    if (symbol == null) continue;

                    var attrs = symbol.GetAttributes();

                    // 判断是否有 ControllerAttribute
                    bool hasControllerAttr = attrs.Any(attr =>
                    {
                        var fullName = attr.AttributeClass?.ToDisplayString();
                        return fullName == "Microsoft.AspNetCore.Mvc.ApiControllerAttribute"
                            || fullName == "Microsoft.AspNetCore.Mvc.ApiController"; // 预防写法
                    });

                    if (hasControllerAttr)
                    {
                        controllers.Add(classDecl, tree);
                    }
                }
            }

            return controllers;
        }
    }

}