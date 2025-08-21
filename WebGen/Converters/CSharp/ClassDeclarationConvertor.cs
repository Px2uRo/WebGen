using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using WebGen.Core;

namespace WebGen.Converters.CSharp
{
    /// <summary>
    /// 用于转换一个 .NET 类到 浏览器的东西
    /// </summary>
    public abstract class ClassDeclarationConvertor : CSSyntaxConverter
    {
        protected ClassDeclarationConvertor(CSSyntaxConverterFactory factory) : base(factory)
        {
        }

        public abstract override string ConvertToJSString(SyntaxNode syntax);
    }
}
