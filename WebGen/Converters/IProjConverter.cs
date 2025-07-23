using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Converters.CSharp;
using WebGen.Converters.Xaml;
using WebGen.Core;

namespace WebGen.Converters
{
    /// <summary>
    /// 文档转换入口，负责将 XAML 和 C# 代码转换为 HTML、CSS 和 JavaScript。等待接口化来适应各种各样浏览器
    /// </summary>
    public interface IProjConverter
    {
        XamlElementConverterFactory Xfactory { get; set; }
        CSSyntaxConverterFactory Sfactory { get; set; }
        CssStyleManager StyleManager { get; set; }
        public string Convert(string xaml, string csharpCode);
    }
}
