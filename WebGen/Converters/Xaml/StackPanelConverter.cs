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
    public class StackPanelConverter : XamlElementConverter, IDependencyPropertyConverter
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
            var isVertical = IsVertical(element);

            // 外层容器 table
            var containerTable = new XElement("table");
            //containerTable.SetAttributeValue("width", "100%");
            containerTable.SetAttributeValue("border", "0");

            if (isVertical)
            {
                // 每个子元素放在一个 tr 中
                foreach (var child in element.Elements())
                {
                    var childContent = _factory.ConvertElementToHTMLXElement(child);
                    var td = new XElement("td", childContent);
                    var tr = new XElement("tr", td);
                    containerTable.Add(tr);
                    TreeUtil.HandleDPAfterAdded(_factory, child, childContent);
                }
            }
            else
            {
                // 所有子元素在同一行
                var tr = new XElement("tr");
                foreach (var child in element.Elements())
                {
                    var childContent = _factory.ConvertElementToHTMLXElement(child);
                    var td = new XElement("td", childContent);
                    tr.Add(td);
                    TreeUtil.HandleDPAfterAdded(_factory, child, childContent);
                }
                containerTable.Add(tr);
            }

            // 处理对齐属性
            var alignAttr = element.Attribute("HorizontalAlignment")?.Value;
            if (alignAttr == "Center")
            {
                // 添加 style（只添加一次）
                EnsureCenteredStyleAdded();
                containerTable.SetAttributeValue("class", "centered");
            }
            else if (alignAttr == "Right")
            {
                //TODO 右对齐的处理方式
                //EnsureRightStyleAdded();
                //containerTable.SetAttributeValue("style", "margin-left:auto; margin-right:0;");
            }

            return base.HandleDependencyProperties(element, containerTable);
        }
        private void EnsureCenteredStyleAdded()
        {
            if (!_factory.HtmlHead.Elements("style").Any(e => e.Value.Contains(".centered")))
            {
                _factory.HtmlHead.Add(new XElement("style", @"
.centered {
  /*display: block;*/
  margin-left: auto;
  margin-right: auto;
}"));
            }
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
