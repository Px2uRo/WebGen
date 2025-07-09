using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters
{
    public class ButtonConverter : XamlElementConverter
    {
        public ButtonConverter(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)=>ConvertToHtmlXElement(element).ToString();
        public override XElement ConvertToHtmlXElement(XElement element)
        {
            var content = element.Value;
            if (element.Attribute("Content") is XAttribute s)
            {
                if (!string.IsNullOrEmpty(content)) throw new Exception("多次设置属性 Content");
                content = s.Value;
            }
            var clickEvent = element.Attribute("Click")?.Value; // 获取 Click 属性
            var buttonElement = new XElement("button", content);

            if (!string.IsNullOrEmpty(clickEvent))
            {
                buttonElement.SetAttributeValue("onclick", $"{clickEvent}()");
            }
            return base.HandleDependencyProperties(element,buttonElement);
        }

    }
}
