using System;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters.Xaml
{
    internal class ImageConverter : XamlElementConverter
    {
        public ImageConverter(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)
        {
            var imgElement = ConvertToHtmlXElement(element);
            return imgElement.ToString(SaveOptions.DisableFormatting);
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {
            var source = element.Attribute("Source")?.Value ?? "";
            var width = element.Attribute("Width")?.Value;
            var height = element.Attribute("Height")?.Value;
            var cssClass = element.Attribute("CssClass")?.Value ?? element.Attribute("class")?.Value ?? "";

            var img = new XElement("img",
                new XAttribute("src", ConvertSourceToUrl(source)),
                new XAttribute("alt", ""));

            if (!string.IsNullOrWhiteSpace(width))
                img.SetAttributeValue("width", width);
            if (!string.IsNullOrWhiteSpace(height))
                img.SetAttributeValue("height", height);

            if (!string.IsNullOrWhiteSpace(cssClass))
                img.SetAttributeValue("class", cssClass);

            // **不写object-fit相关style，兼容老浏览器**

            return img;
        }

        /// <summary>
        /// 简单示例：把 XAML 的 Source 转成 HTML 可用的 URL
        /// 可扩展成相对路径转换，或者资源前缀添加等逻辑
        /// </summary>
        private string ConvertSourceToUrl(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return "";

            // 如果是绝对 URL 或 data URI，直接返回
            if (source.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                source.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                source.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                return source;
            }

            // 如果是相对路径（比如 "/images/pic.png" 或 "Assets/pic.png"）
            // 你可以在这里做映射，比如加前缀或转成相对 URL
            // 简单示例：确保用正斜杠，并且默认相对路径前加 /
            var normalized = source.Replace('\\', '/');
            if (!normalized.StartsWith("/"))
                normalized = "/" + normalized;

            return normalized;
        }
    }
}
