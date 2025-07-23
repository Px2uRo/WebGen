using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters.Xaml
{
    public class TextBlockConverter : XamlElementConverter
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
        private string AppendStyle(XElement htmlElement, string newStyle)
        {
            var existing = htmlElement.Attribute("style")?.Value ?? "";
            if (!existing.EndsWith(";")) existing += ";";
            return $"{existing}{newStyle};";
        }

        public override XElement HandleDependencyProperties(XElement sourceElement, XElement htmlElement)
        {
            // 提取所有形如 TextBlock.FontSize 的依赖属性
            var attributes = sourceElement.Attributes()
                .ToDictionary(attr => attr.Name.LocalName, attr => attr.Value);

            foreach (var attribute in attributes)
            {
                var fullPropertyName = attribute.Key;      // 例如 TextBlock.FontSize
                var value = attribute.Value;

                var property = fullPropertyName;   // FontSize, FontWeight 等

                switch (property)
                {
                    case "FontSize":
                        htmlElement.SetAttributeValue("style", AppendStyle(htmlElement, $"font-size:{value}px"));
                        break;

                    case "FontWeight":
                        htmlElement.SetAttributeValue("style", AppendStyle(htmlElement, $"font-weight:{value}"));
                        break;

                    case "FontFamily":
                        htmlElement.SetAttributeValue("style", AppendStyle(htmlElement, $"font-family:{value}"));
                        break;

                    case "Foreground":
                        htmlElement.SetAttributeValue("style", AppendStyle(htmlElement, $"color:{value}"));
                        break;

                        // 你可以在这里继续扩展其他依赖属性，例如 TextAlignment 等
                }
            }

            return htmlElement;
        }
    }
}
