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
    public interface IPageConverter
    {
        XamlElementConverterFactory Xfactory { get; set; }
        CSSyntaxConverterFactory Sfactory { get; set; }
        CssStyleManager StyleManager { get; set; }
        public string Convert(string xaml, string csharpCode);
    }

    /// <summary>
    /// 虽然很不负责，但是这个转换器是用来实现 IPageConverter 接口用的示例类，
    /// 并且是一个运行时的转换器（不涉及反射）。
    /// 我们的 Generator 就是用的这个工厂合集。
    /// </summary>
    public class RuntimeConverterDemo : IPageConverter
    {
        public XamlElementConverterFactory Xfactory { get ; set ; }
        public CSSyntaxConverterFactory Sfactory { get ; set ; }
        public CssStyleManager StyleManager { get ; set ; }

        public string Convert(string xaml, string csharpCode)
        {
            return "";
        }
    }
}
