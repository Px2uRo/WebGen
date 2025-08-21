using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using WebGen.Core;

namespace WebGen.Converters.CSharp
{
    public class InvocationExpressionSyntaxConvertor : CSSyntaxConverter
    {
        public InvocationExpressionSyntaxConvertor(CSSyntaxConverterFactory factory) : base(factory)
        {

        }

        public override string ConvertToJSString(SyntaxNode syntax)
        {
            var invo = syntax as InvocationExpressionSyntax;
            var expr = invo.Expression;
            var a = invo.Expression.GetType();
            return Factory.Converters[invo.Expression.GetType()].ConvertToJSString(invo.Expression);
            throw new InvalidOperationException($"不支持的语法节点类型: {syntax.GetType()}");
        }
    }
}