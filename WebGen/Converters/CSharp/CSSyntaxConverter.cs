using Microsoft.CodeAnalysis;
using WebGen.Core;

namespace WebGen.Converters.CSharp
{
    public abstract class CSSyntaxConverter
    {
        public virtual CSSyntaxConverterFactory _factory { get; private set; }
        public CSSyntaxConverter(CSSyntaxConverterFactory factory)
        {
            _factory = factory;
        }
        public abstract string ConvertToJSString(SyntaxNode syntax);
    }
}
