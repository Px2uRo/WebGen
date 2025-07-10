using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using WebGen.Core;
using WebGen.JS.Rules;

namespace WebGen.Converters.CSharp
{
    internal class InvocationExpressionSyntaxConvertor : CSSyntaxConverter
    {
        public InvocationExpressionSyntaxConvertor(CSSyntaxConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToJSString(SyntaxNode syntax)
        {
            var invo = syntax as InvocationExpressionSyntax;
            var expr = invo.Expression;
            if (expr is IdentifierNameSyntax methodName)
            {
                string name = methodName.Identifier.Text;  // 就是 "Alert"
                return name;
            }
            else if (expr is MemberAccessExpressionSyntax member)
            {
                Dictionary<string[], string> mapping = _getMapping(invo.ArgumentList.Arguments.Select(a => a.ToString()).ToArray());

                var chain = _getMemberChain(expr);

                foreach (var kv in mapping)
                {
                    if (chain.Contains(kv.Key[0]))
                    {
                        var args = string.Join(", ", invo.ArgumentList.Arguments.Select(a => a.ToString()));
                        return $"{kv.Value};";
                    }
                }
                // 其他情况...
                throw new InvalidOperationException($"不支持的语法节点类型: {syntax.GetType()}");
            }
            throw new InvalidOperationException($"不支持的语法节点类型: {syntax.GetType()}");
        }

        private static Dictionary<string[], string> _getMapping(object[] args = null)
        {
            var res = new Dictionary<string[], string>();

            // 获取方法列表
            var methodInfos = typeof(JSGlobalFunctionsConvertRule)
                .GetMethods();
            var inst = new JSGlobalFunctionsConvertRule();
            // 遍历方法
            foreach (var method in methodInfos)
            {
                try
                {
                    res.Add(
                    new[] { method.Name },
                    method.Invoke(inst, args) as String
                );
                }
                catch (Exception ex)
                {
                    //throw new InvalidOperationException($"无法转换方法 {method.Name} 的参数信息: {ex.Message}", ex);
                }
                
            }

            return res;

        }

        private List<string> _getMemberChain(ExpressionSyntax expr)
        {
            var list = new List<string>();
            while (expr is MemberAccessExpressionSyntax ma)
            {
                list.Insert(0, ma.Name.Identifier.Text);
                expr = ma.Expression;
            }

            if (expr is IdentifierNameSyntax id)
                list.Insert(0, id.Identifier.Text);

            return list;
        }

    }
}