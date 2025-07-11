using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters.Xaml
{
    internal class HtmlMetaDataConvertor : XamlElementConverter
    {
        public HtmlMetaDataConvertor(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)
        {
            if (element.Name.LocalName.Equals("HtmlMetaData", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var attr in element.Attributes())
                {
                    string innerXml = attr.Value;

                    try
                    {
                        return innerXml;
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
            return null;
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {
            if (element.Name.LocalName.Equals("HtmlMetaData", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var attr in element.Attributes())
                {
                    string innerXml = attr.Value;

                    try
                    {
                        var parsedElement = XElement.Parse(innerXml);
                        return parsedElement;
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
