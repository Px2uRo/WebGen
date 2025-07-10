using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebGen.Converters
{
    internal interface DependencyPropertyConverter
    {
        string GetJSCode(XElement context, string csharp);
        XElement EditXElement(string propty, string value, XElement ownerXaml, XElement HtmlElement);
        XElement HandleAfterAdded(XElement xaml, XElement HtmlElement);
    }
}
