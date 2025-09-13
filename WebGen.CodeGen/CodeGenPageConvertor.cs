using System;
using System.Collections.Generic;
using System.Text;
using WebGen.Converters;
using WebGen.Core;

namespace WebGen.CodeGen
{
    internal class CodeGenPageConvertor : IPageConverter
    {
        public XamlElementConverterFactory Xfactory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CSSyntaxConverterFactory Sfactory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CssStyleManager StyleManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Convert(string xaml, string csharpCode)
        {
            throw new NotImplementedException();
        }
    }
}
