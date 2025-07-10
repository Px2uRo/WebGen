using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters.Xaml
{
    public class TextBoxConverter : XamlElementConverter
    {
        public TextBoxConverter(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)
        {
            return "<input type='text' />";
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {
            var input = new XElement("input");
            input.Add(new XAttribute("type", "text"));
            return HandleDependencyProperties(element,input);
        }
    }
}
