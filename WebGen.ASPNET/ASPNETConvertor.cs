using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Converters;
using WebGen.Converters.Xaml;
using WebGen.Core;
using WebGen.Converters.CSharp;
using Microsoft.AspNetCore.Http;

namespace WebGen.ASPNET
{
    [Obsolete("这个代码是我用来备忘用的，我怕我忘了")]
    public class ASPNETConvertor:IPageConverter
    {
        internal void SetRequest(HttpRequest request)
        {
            if(Xfactory is ASPNETXamlFactory factory)
            {
                factory.Request = request;
            }
        }
        public XamlElementConverterFactory Xfactory{ get; set; }
        public CSSyntaxConverterFactory Sfactory { get; set; }
        public CssStyleManager StyleManager { get; set; }

        public ASPNETConvertor()
        {
            Xfactory = new ASPNETXamlFactory();
            StyleManager = new CssStyleManager();
            Sfactory = new CSSyntaxConverterFactory();

            // 注册默认控件转换器
            Xfactory.Register("Grid", new GridConverter(Xfactory));
            Xfactory.Register("Button", new ButtonConverter(Xfactory));
            Xfactory.Register("TextBox", new TextBoxConverter(Xfactory));
            Xfactory.Register("TextBlock", new TextBlockConverter(Xfactory));
            Xfactory.Register("StackPanel", new StackPanelConverter(Xfactory));
            Xfactory.Register("HtmlMetaData", new HtmlMetaDataConvertor(Xfactory));
            Xfactory.Register("Image", new ImageConverter(Xfactory));
            Xfactory.Register("Page", new PageConvertor(Xfactory));

            Sfactory.Register(typeof(StatementSyntax), new StatementSyntaxConvertor(Sfactory));
            Sfactory.Register(typeof(ExpressionStatementSyntax), new ExpressionStatementSyntaxConvertor(Sfactory));
            Sfactory.Register(typeof(InvocationExpressionSyntax), new InvocationExpressionSyntaxConvertor(Sfactory));
            Sfactory.Register(typeof(MemberAccessExpressionSyntax), new MemberAccessExpressionSyntaxConvertor(Sfactory));
            Sfactory.Register(typeof(IdentifierNameSyntax), new IdentifierNameSyntaxConvertor(Sfactory));
        }
        public string Convert(string xaml, string csharpCode)
        {
            var xml = XDocument.Parse(xaml);
            var html = Xfactory.ConvertElementToHTMLXElement(xml.Root);
            var styles = StyleManager.GenerateStyles();
            var js = Sfactory.Convert(csharpCode);
            html.Add(new XElement("script", js));
            return html.ToString();
            //return $"<!DOCTYPE html><html><head><style>{styles}</style></head><body>{htmlBody}</body><script>{js}</script></html>";
        }
    }
}
