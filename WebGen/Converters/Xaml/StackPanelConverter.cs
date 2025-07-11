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
    public class StackPanelConverter : XamlElementConverter, DependencyPropertyConverter
    {
        public StackPanelConverter(XamlElementConverterFactory factory) : base(factory) { }

        // 将 StackPanel 转换为 HTML 字符串
        public override string ConvertToHtmlString(XElement element)
        {
            // 获取 Orientation 和 Spacing 属性
            var orientationVal = element.Attribute("Orientation")?.Value ?? "Vertical";
            var spacingVal = element.Attribute("Spacing")?.Value;
            // 生成 HTML 并返回字符串
            return generateHtmlXElement(element).ToString();
        }

        // 解析 Orientation 属性（默认为 Vertical）
        private bool IsVertical(XElement element)
        {
            var orientationVal = element.Attribute("Orientation")?.Value;
            return string.IsNullOrEmpty(orientationVal)
                   || orientationVal.Equals("Vertical", StringComparison.OrdinalIgnoreCase);
        }

        // 生成 HTML <table> 元素并添加子项（初始为直接子元素）
        private XElement generateHtmlXElement(XElement element)
        {
            // 创建表格容器
            XElement table = new XElement("table");
            table.SetAttributeValue("width", "100%");
            table.SetAttributeValue("border", "0");
            // 遍历 StackPanel 的所有子元素，转换为 HTML，并添加到表格中（后续会重组行列）
            var isver = IsVertical(element);
            foreach (var child in element.Elements())
            {
                var hc = _factory.ConvertElementToHTMLXElement(child);
                if (isver)
                {
                    XElement td = new XElement("td", hc);
                    XElement tr = new XElement("tr", td);
                    table.Add(tr);
                }
                else
                {
                    XElement td = new XElement("td", hc);
                    table.Add(td);
                }
                // 处理依赖属性（如 Margin、Alignment 等）
                TreeUtil.HandleDPAfterAdded(_factory, child, hc);
            }
            // 处理 StackPanel 本身的其他依赖属性
            return base.HandleDependencyProperties(element, table);
        }

        // 将 StackPanel 转换为 HTML XElement（与 ConvertToHtmlString 类似）
        public override XElement ConvertToHtmlXElement(XElement element)
        {
            return generateHtmlXElement(element);
        }

        public string GetJSCode(XElement context, string csharp) => throw new NotImplementedException();
        public XElement EditXElement(string property, string value, XElement ownerXaml, XElement element) => element;

        // 处理生成后的 <table> 结构：根据 Orientation 将子元素包裹到 <tr><td> 中，并添加间距
        public XElement HandleAfterAdded(XElement xaml, XElement htmlElement)
        {
            // 如果当前 HTML 元素位于 table 内，且对应的 XAML 父元素是 StackPanel，则重组表格
            if (TreeUtil.FindParent(htmlElement, "table") is XElement table &&
                TreeUtil.FindParent(xaml, "StackPanel") is XElement stackPanel)
            {
                HandleTable(stackPanel, table);
            }
            return htmlElement;
        }

        // 重组表格：根据 StackPanel 的 Orientation 和 Spacing 插入 <tr> 和 <td>
        public XElement HandleTable(XElement stackPanel, XElement html)
        {
            bool isVertical = IsVertical(stackPanel);
            // 获取间距值（如果有）
            string spacingVal = stackPanel.Attribute("Spacing")?.Value;
            double spacing = 0;
            if (!string.IsNullOrEmpty(spacingVal))
            {
                // 解析可能带单位的间距值
                var val = spacingVal.Trim();
                if (val.EndsWith("px", StringComparison.OrdinalIgnoreCase))
                    val = val.Substring(0, val.Length - 2);
                double.TryParse(val, out spacing);
            }
            // 如果表格已经有 <tr>，说明已经处理过，直接返回
            if (html.Elements().Any(x => x.Name == "tr"))
                return html;

            var children = html.Elements().ToList(); // 先拷贝当前直接子元素列表

            if (isVertical)
            {
                // 每个子元素占据一行
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var tr = new XElement("tr");
                    var td = new XElement("td");
                    // 如果不是最后一个子元素，添加底部间距
                    if (spacing > 0 && i < children.Count - 1)
                        td.SetAttributeValue("style", $"padding-bottom:{spacing}px");
                    child.Remove();
                    td.Add(child);
                    tr.Add(td);
                    html.Add(tr);
                }
            }
            else
            {
                // 所有子元素放在同一行，逐个在单元格中添加右侧间距
                var tr = new XElement("tr");
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var td = new XElement("td");
                    if (spacing > 0 && i < children.Count - 1)
                        td.SetAttributeValue("style", $"padding-right:{spacing}px");
                    child.Remove();
                    td.Add(child);
                    tr.Add(td);
                }
                html.Add(tr);
            }
            return html;
        }
    }
}
