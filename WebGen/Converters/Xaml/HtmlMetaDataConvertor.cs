using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters.Xaml
{
    public class HtmlMetaDataConvertor : XamlElementConverter
        {
            public string GetExternalPage(string a)
            {
                var client = new HttpClient();
                var html = client.GetStringAsync(a).GetAwaiter().GetResult();
                return html;
            }
        public HtmlMetaDataConvertor(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)
        {
            return ConvertToHtmlXElement(element).ToString();
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {
            if (element.Name.LocalName.Equals("HtmlMetaData", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var attr in element.Attributes())

                    if (attr.Name.LocalName.Equals("data", StringComparison.OrdinalIgnoreCase))
                    {
                        string value = attr.Value;
                        try
                        {
                            if (value.StartsWith("<"))
                            {
                                return XElement.Parse(value);
                            }
                            if (value.StartsWith("http"))
                            {
                                return XElement.Parse(GetExternalPage(value));
                            }
                            if(_factory is IProvideRequestInfo _info)
                            {
                                if (value.StartsWith("/"))
                                {
                                    var a = _info.GetHost();
                                    return XElement.Parse(GetExternalPage($"http://{a}{value}"));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // 如果 data 不是合法的 XML，可以做一些错误处理或日志记录
                            Console.WriteLine($"无法解析 HtmlMetaData.data：{ex.Message}");
                            return null;
                        }
                    }
                return null;
            }
            else
            {
                return null;
            }

        }
    }
}
