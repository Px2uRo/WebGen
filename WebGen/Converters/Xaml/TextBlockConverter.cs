using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters.Xaml
{
    internal class TextBlockConverter : XamlElementConverter
    {
        public TextBlockConverter(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)
        {
            var textAtr = element.Attribute("Text");
            return $"<div>{textAtr?.Value}</div>";
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {
            var textAtr = element.Attribute("Text");
            var div = new XElement("div");
            div.Add(textAtr?.Value);
            return HandleDependencyProperties(element,div);
        }
    }
}
