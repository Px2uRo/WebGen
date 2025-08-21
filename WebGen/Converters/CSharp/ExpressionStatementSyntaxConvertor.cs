using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebGen.Core;

namespace WebGen.Converters.CSharp
{
    public class ExpressionStatementSyntaxConvertor : CSSyntaxConverter
    {
        public ExpressionStatementSyntaxConvertor(CSSyntaxConverterFactory factory) : base(factory)
        {

        }

        public override string ConvertToJSString(SyntaxNode syntax)
        {
            var es = syntax as ExpressionStatementSyntax;
            if (es.Expression is InvocationExpressionSyntax invo)
            {
                return Factory.Converters[typeof(InvocationExpressionSyntax)].ConvertToJSString(invo);
            }
            else
            {
                throw new InvalidOperationException($"不支持的语法节点类型: {syntax.GetType()}");
            }
        }
    }
}