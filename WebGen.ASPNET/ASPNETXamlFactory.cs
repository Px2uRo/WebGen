using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Converters.Xaml;
using WebGen.Core;

namespace WebGen.ASPNET
{
    public class ASPNETXamlFactory: XamlElementConverterFactory,IProvideRequestInfo
    {
        public HttpRequest Request { get; set; }
        public ASPNETXamlFactory()
        {
            
        }
        public ASPNETXamlFactory(HttpRequest request)
            {
            Request = request;
        }
            public XElement Head { get; internal set; }
        public override Dictionary<string, XamlElementConverter> Converters { get; set; } = new();
        public override XElement HtmlHead { get ;  set ; }

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

        #region 接口实现

        public string GetMethod() => Request.Method;
        public string GetScheme() => Request.Scheme;
        public string GetHost() => Request.Host.Value;
        public string GetPath() => Request.Path;
        public string GetQueryString() => Request.QueryString.Value ?? "";
        public string GetFullUrl() => Request.GetDisplayUrl();

        public string? GetHeader(string name)
            => Request.Headers.TryGetValue(name, out var value) ? value.ToString() : null;

        public IDictionary<string, string> GetAllHeaders()
            => Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());

        public string? GetQueryValue(string key)
            => Request.Query.TryGetValue(key, out var value) ? value.ToString() : null;

        public IDictionary<string, string> GetAllQueryParameters()
            => Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());

        public string? GetRemoteIpAddress()
            => Request.HttpContext.Connection.RemoteIpAddress?.ToString();

        public string? GetUserAgent()
            => GetHeader("User-Agent");

        public bool HasFormContentType()
            => Request.HasFormContentType;

        public IDictionary<string, string> GetFormFields()
            => Request.HasFormContentType
                ? Request.Form.ToDictionary(f => f.Key, f => f.Value.ToString())
                : new Dictionary<string, string>();        
        #endregion
    }

    }
