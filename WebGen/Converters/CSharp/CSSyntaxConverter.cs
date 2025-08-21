using Microsoft.CodeAnalysis;
using WebGen.Core;

namespace WebGen.Converters.CSharp
{
    public abstract class CSSyntaxConverter
    {
        public virtual CSSyntaxConverterFactory Factory { get; }
        protected CSSyntaxConverter(CSSyntaxConverterFactory factory)
        {
            Factory = factory;
        }
        public abstract string ConvertToJSString(SyntaxNode syntax);
    }
}
