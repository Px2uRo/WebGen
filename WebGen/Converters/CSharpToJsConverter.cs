using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebGen.JS;

namespace WebGen.Core;
public class CSharpToJsConverter
{
    /// <summary>
    /// 代码翻译入口，将 C# 代码转换为 JavaScript 代码。
    /// </summary>
    /// <param name="csharpCode"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string Convert(string csharpCode)
    {
        var tree = CSharpSyntaxTree.ParseText(csharpCode);
        var root = tree.GetRoot() as CompilationUnitSyntax;

        if (root == null)
        {
            throw new InvalidOperationException("无法解析代码：输入的 C# 代码不完整或无效。");
        }

        return ProcessClasses(root);
    }
    /// <summary>
    /// 找到所有的类，并处理它们的方法。
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    private string ProcessClasses(CompilationUnitSyntax root)
    {
        // 遍历所有类，找到方法
        var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
        return string.Join("\n", classes.SelectMany(c => ProcessMethods(c)));
    }
    /// <summary>
    /// 找到所有的方法
    /// </summary>
    /// <param name="classNode"></param>
    /// <returns></returns>
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
            ? "/* 方法体为空 */"
            : string.Join(" ", body.Statements.Select(ConvertStatement));
    }

    private string ConvertStatement(StatementSyntax statement)
    {
        if (statement is ExpressionStatementSyntax expression)
        {
            return ConvertExpression(expression.Expression) + ";";
        }
        else if(statement is LocalDeclarationStatementSyntax declaration)
        {

        }
        return $"/* 未实现的陈述 (statement) 分析: {statement} */";
    }

    private string ConvertExpression(ExpressionSyntax expression)
    {
        if (expression is InvocationExpressionSyntax invocation)
        {
            var result = ConvertInvocation(invocation);
            if (!string.IsNullOrEmpty(result))
                return result;

            var methodName = invocation.Expression.ToString();
            var args = string.Join(", ", invocation.ArgumentList.Arguments.Select(a => a.ToString()));
            return $"{methodName}({args})";
        }

        return expression.ToString();
    }

    private string? ConvertInvocation(InvocationExpressionSyntax invocation)
    {
        var methodName = invocation.Expression.ToString();
        
        if(invocation.Expression is MemberAccessExpressionSyntax memberAccess)
        {
            methodName = memberAccess.Name.Contains("");
        }

        //if (methodName.Contains(JSGlobalFunctions.Alert()))
        //{
        //    var messageArg = invocation.ArgumentList.Arguments.First().ToString();
        //    return $"alert({messageArg});";
        //}

        return null;
    }
}
