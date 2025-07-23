using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;
using WebGen.Utils.XmlUtil;

namespace WebGen.Converters.Xaml
{
    public class PageConvertor : XamlElementConverter, IDependencyPropertyConverter
    {
        public override XElement HandleDependencyProperties(XElement sourceElement, XElement htmlElement)
        {
            var res = new XElement("html");

            var title = sourceElement.Attribute("Title")?.Value ?? "Untitled Page";
            var iconHref = sourceElement.Attribute("Icon")?.Value ?? "/favicon.ico";

            var head = new XElement("head",
                new XElement("meta", new XAttribute("charset", "UTF-8")),
                new XElement("title", title),
                new XElement("link",
                    new XAttribute("rel", "icon"),
                    new XAttribute("href", iconHref),
                    new XAttribute("type", "image/x-icon")
                )
            );
            res.Add(head);
            _factory.HtmlHead = head;
            var body = _factory.ConvertElementToHTMLXElement(sourceElement.Elements().ToArray()[0]);
            res.Add(new XElement("body", body)); 
            TreeUtil.HandleDPAfterAdded(_factory, res, res);
            return res;
            //return $"<!DOCTYPE html><html>
            //<head>
            //<style>{styles}</style></head>
            //<body>{htmlBody}</body>
            //<script>{js}</script></html>";

        }
        public PageConvertor(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)
        {
            return ConvertToHtmlXElement(element).ToString(SaveOptions.DisableFormatting);
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {
            var pageName = element.Attribute("Name")?.Value ?? "Untitled Page";


            XElement head =
            // 调用处理函数，将依赖属性注入 head
            HandleDependencyProperties(element, element);

            // 构造最终 html 元素
            var pageHtml = new XElement("html", head);

            return pageHtml;
        }

        public XElement EditXElement(string propty, string value, XElement ownerXaml, XElement HtmlElement)
        {
            throw new NotImplementedException();
        }

        public string GetJSCode(XElement context, string csharp)
        {
            throw new NotImplementedException();
        }

        public XElement HandleAfterAdded(XElement xaml, XElement HtmlElement)
        {
            throw new NotImplementedException();
        }
    }
}
