using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebGen.Core;

namespace WebGen.Converters.CSharp
{
    internal class StatementSyntaxConvertor : CSSyntaxConverter
    {
        public StatementSyntaxConvertor(CSSyntaxConverterFactory factory) : base(factory)
        {
            
        }

        public override string ConvertToJSString(SyntaxNode syntax)
        {
            if (syntax is ExpressionElementSyntax expression )
            {
                if (expression.Expression is InvocationExpressionSyntax invocation)
                {
                    return _factory.Converters[typeof(InvocationExpressionSyntax)].ConvertToJSString(invocation) +";";
                }
                throw new InvalidOperationException($"不支持的语法节点类型: {syntax.GetType()}");
            }
            else
            {
                throw new InvalidOperationException($"不支持的语法节点类型: {syntax.GetType()}");
            }
        }
    }
}