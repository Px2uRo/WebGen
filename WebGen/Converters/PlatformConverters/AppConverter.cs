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

namespace WebGen.PlatformConverters
{
    /// <summary>
    /// 文档转换入口，负责将 XAML 和 C# 代码转换为 HTML、CSS 和 JavaScript。等待接口化来适应各种各样浏览器
    /// </summary>
    public class AppConverter
    {
        private readonly XamlElementConverterFactory _xfactory;
        private readonly CSSyntaxConverterFactory _sfactory;
        private readonly CssStyleManager _styleManager;

        public AppConverter()
        {
            _xfactory = new XamlElementConverterFactory();
            _styleManager = new CssStyleManager();
            _sfactory = new CSSyntaxConverterFactory();

            // 注册默认控件转换器
            _xfactory.Register("Grid", new GridConverter(_xfactory));
            _xfactory.Register("Button", new ButtonConverter(_xfactory));
            _xfactory.Register("TextBox", new TextBoxConverter(_xfactory));
            _xfactory.Register("TextBlock",new TextBlockConverter(_xfactory));

            _sfactory.Register(typeof(StatementSyntax), new StatementSyntaxConvertor(_sfactory));
            _sfactory.Register(typeof(ExpressionStatementSyntax), new ExpressionStatementSyntaxConvertor(_sfactory));
            _sfactory.Register(typeof(InvocationExpressionSyntax), new InvocationExpressionSyntaxConvertor(_sfactory));
            _sfactory.Register(typeof(MemberAccessExpressionSyntax), new MemberAccessExpressionSyntaxConvertor(_sfactory));
            _sfactory.Register(typeof(IdentifierNameSyntax), new IdentifierNameSyntaxConvertor(_sfactory));
        }

        public string Convert(string xaml, string csharpCode)
        {
            var xml = XDocument.Parse(xaml);
            var htmlBody = _xfactory.ConvertElementToString(xml.Root);
            var styles = _styleManager.GenerateStyles();
            var js = _sfactory.Convert(csharpCode);

            return $"<!DOCTYPE html><html><head><style>{styles}</style></head><body>{htmlBody}</body><script>{js}</script></html>";
        }
    }
}
