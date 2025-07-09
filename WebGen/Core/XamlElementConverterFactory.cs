using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WebGen.Converters;
using WebGen.XmlUtil;

namespace WebGen.Core
{
    /// <summary>
    /// 这个类是用来注册和管理 XAML 元素转换器的工厂类。
    /// </summary>
    public class XamlElementConverterFactory
    {
        public readonly Dictionary<string, XamlElementConverter> Converters = new();

        public void Register(string elementName, XamlElementConverter converter)
        {
            Converters[elementName] = converter;
        }
        public XElement ConvertElementToHTMLXElement(XElement xamlElement)
        {

            var name = xamlElement.Name.LocalName;
            if (Converters.TryGetValue(name, out var converter))
            {
                var ele = converter.ConvertToHtmlXElement(xamlElement); 

                return ele;
            }
            else
            {
                throw new InvalidOperationException($"不支持{name}");
            }
        }
        public string ConvertElementToString(XElement element)
        {
            var name = element.Name.LocalName;
            if (Converters.TryGetValue(name, out var converter))
            {
                var html = converter.ConvertToHtmlXElement(element); 
                return html.ToString();
            }
            
            return $"<div>{element.Value}</div>"; // 默认转换
        }
    }
}
