using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WebGen.Converters;
using WebGen.Converters.Xaml;

namespace WebGen.Core
{
    /// <summary>
    /// 这个类是抽象层面的用来注册和管理 XAML 元素转换器的工厂类。
    /// </summary>
    public abstract class XamlElementConverterFactory
    {
        public abstract Dictionary<string, XamlElementConverter> Converters { get; set; }
        public abstract XElement HtmlHead { get; set; }

        public virtual void Register(string elementName, XamlElementConverter converter)
        {
            Converters[elementName] = converter;
        }
        public virtual XElement ConvertElementToHTMLXElement(XElement xamlElement)
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
        public virtual string ConvertElementToString(XElement element)
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

    public interface IProvideRequestInfo
    {
            string GetMethod();
            string GetScheme();
            string GetHost();
            string GetPath();
            string GetQueryString();
            string GetFullUrl();

            string? GetHeader(string name);
            IDictionary<string, string> GetAllHeaders();

            string? GetQueryValue(string key);
            IDictionary<string, string> GetAllQueryParameters();

            string? GetRemoteIpAddress();
            string? GetUserAgent();

            bool HasFormContentType();
            IDictionary<string, string> GetFormFields();
        }

    
}