using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WebGen.Core;
using WebGen.Utils.XmlUtil;

namespace WebGen.Converters.Xaml
{
    public class GridConverter : XamlElementConverter, IDependencyPropertyConverter
    {

        public GridConverter(XamlElementConverterFactory factory):base(factory) { }

        public override string ConvertToHtmlString(XElement element)
        {

            // 提取简化的 RowDefinitions 和 ColumnDefinitions
            var rowDefinitions = parseDefinitions(element.Attribute("RowDefinitions")?.Value);
            var columnDefinitions = parseDefinitions(element.Attribute("ColumnDefinitions")?.Value);

            // 生成 HTML 和 CSS
            return generateHtmlXElement(element,rowDefinitions, columnDefinitions).ToString();
        }
        private List<string> parseDefinitions(string definitionString)
        {
            if (string.IsNullOrEmpty(definitionString))
                return new List<string>();

            return definitionString.Split(',')
                                   .Select(def => def.Trim())
                                   .ToList();
        }

        private XElement generateHtmlXElement(XElement element, List<string> rowDefinitions, List<string> columnDefinitions)
        {
            XElement table = new XElement("table");
            table.SetAttributeValue("width", "100%");
            table.SetAttributeValue("border", "0");

            foreach (var child in element.Elements())
            {
                var hc = _factory.ConvertElementToHTMLXElement(child);
                table.Add(hc);
                TreeUtil.HandleDPAfterAdded(_factory, child, hc);
            }

            return base.HandleDependencyProperties(element,table);
        }



        // 生成行和列的布局 CSS
        private string GenerateGridLayoutCSS(List<string> rowDefinitions, List<string> columnDefinitions)
        {
            var css = "";

            // 行布局
            if (rowDefinitions.Any())
            {
                for (int i = 0; i < rowDefinitions.Count; i++)
                {
                    var height = rowDefinitions[i];
                    css += $".grid-item:nth-child({i + 1}) {{ height: {height}; }}\n";
                }
            }

            // 列布局
            if (columnDefinitions.Any())
            {
                for (int i = 0; i < columnDefinitions.Count; i++)
                {
                    var width = columnDefinitions[i];
                    css += $".grid-item:nth-child({i + 1}) {{ width: {width}; }}\n";
                }
            }

            return css;
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {

            var rowDefinitions = parseDefinitions(element.Attribute("RowDefinitions")?.Value);
            var columnDefinitions = parseDefinitions(element.Attribute("ColumnDefinitions")?.Value);

            // 生成 HTML 和 CSS
            return generateHtmlXElement(element, rowDefinitions, columnDefinitions);
        }

        public string GetJSCode(XElement context, string csharp)
        {
            throw new NotImplementedException();
        }

        public XElement EditXElement(string property, string value, XElement ownerXaml, XElement element) => element;

        #region 
        public XElement HandleTable(XElement grid, XElement html)
        {

            if (html.Elements().Where(x => x.Name == "tr").Count() != 0)
            {
                return html;
            }
            else if (grid?.Attribute("RowDefinitions") is XAttribute rowd)
            {
                var rows = rowd.Value.Split(',').Select(r => r.Trim()).ToList();

                if (rows != null)
                {
                    var rowHeights = rows.Select(r =>
                    {
                        if (r.EndsWith("px"))
                            return r.Replace("px", "");
                        else if (r.EndsWith("*"))
                            return CalculatePercentage(r, rows);
                        else
                            throw new NotSupportedException($"不支持 {r}");
                    }).ToList();

                    foreach (var row in rowHeights)
                    {
                        html.Add(new XElement("tr"));
                    }
                }
            }
            else if (html.Elements().Where(x => x.Name == "tr").Count() == 0)
            {
                html.Add(new XElement("tr"));
            }
            else
            {
                return html;
            }
            foreach (var tr in html.Elements().Where(x => x.Name == "tr"))
            {
                var cols = grid?.Attribute("ColumnDefinitions")?.Value.Split(',').Select(c => c.Trim()).ToList();

                if (cols != null)
                {
                    var colHeights = cols.Select(r =>
                    {
                        if (r.EndsWith("px"))
                            return r.Replace("px", "");
                        else if (r.EndsWith("*"))
                            return CalculatePercentage(r, cols);
                        else
                            throw new NotSupportedException($"不支持 {r}");
                    }).ToList();

                    foreach (var col in colHeights)
                    {
                        var td = new XElement("td");
                        td.SetAttributeValue("width", col);
                        tr.Add(td);
                    }
                }
                else
                {
                    var td = new XElement("td");
                    tr.Add(td);
                }
            }
            return html;
        }

        private string CalculatePercentage(string r, List<string>? datas)
        {
            var b = datas.Where(x=>x.EndsWith("*"));
            var t = 0d;
            foreach (var item in b)
            {
                t += System.Convert.ToDouble(item.Replace("*",""));
            }
            return (Math.Floor(System.Convert.ToDouble(r.Replace("*","")) / t * 100) / 100d * 100d).ToString() +"%" ;
        }
        #endregion

        public XElement HandleAfterAdded(XElement xaml, XElement htmlElement)
        {
            if(TreeUtil.FindParent(htmlElement, "table")is XElement table)
            {
                if(TreeUtil.FindParent(xaml, "Grid")is XElement grid)
                {
                    HandleTable(grid,table);
                    ReleaseElement(xaml,htmlElement,table);
                }
            }
            return htmlElement;
            //throw new NotImplementedException();
        }

        private void ReleaseElement(XElement xaml,XElement htmlElement, XElement table)
        {
            htmlElement.Remove();
            if (xaml.Attribute("Grid.Column") is XAttribute atr)
            {
                if(xaml.Attribute("Grid.Row")is XAttribute atr2)
                {
                    table.Elements("tr").ToArray()[System.Convert.ToInt32(atr2.Value)].Elements("td").ToArray()[System.Convert.ToInt32(atr.Value)].Add(htmlElement);
                    return;
                }
                table.Elements("tr").First().Elements("td").ToArray()[System.Convert.ToInt32(atr.Value)].Add(htmlElement);
                return;
            }
            else if (xaml.Attribute("Grid.Row") is XAttribute atr2)
            {
                table.Elements("tr").ToArray()[System.Convert.ToInt32(atr2.Value)].Elements("td").First().Add(htmlElement);
                return;
            }
        }
    }
}
