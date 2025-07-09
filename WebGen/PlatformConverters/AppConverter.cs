using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Converters;
using WebGen.Core;

namespace WebGen.PlatformConverters
{
    public class AppConverter
    {
        private readonly XamlElementConverterFactory _factory;
        private readonly CssStyleManager _styleManager;
        private readonly CSharpToJsConverter _jsConverter;

        public AppConverter()
        {
            _factory = new XamlElementConverterFactory();
            _styleManager = new CssStyleManager();
            _jsConverter = new CSharpToJsConverter();

            // 注册默认控件转换器
            _factory.Register("Grid", new GridConverter(_factory));
            _factory.Register("Button", new ButtonConverter(_factory));
            _factory.Register("TextBox", new TextBoxConverter(_factory));
            _factory.Register("TextBlock",new TextBlockConverter(_factory));
        }

        public string Convert(string xaml, string csharpCode)
        {
            var xml = XDocument.Parse(xaml);
            var htmlBody = _factory.ConvertElementToString(xml.Root);
            var styles = _styleManager.GenerateStyles();
            var js = _jsConverter.Convert(csharpCode);

            return $"<!DOCTYPE html><html><head><style>{styles}</style></head><body>{htmlBody}</body><script>{js}</script></html>";
        }
    }
}
