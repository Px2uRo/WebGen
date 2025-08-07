using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Converters;
using WebGen.Core;

namespace WebGen.Utils.XmlUtil
{
    internal static class TreeUtil
    {
        internal static void HandleDPAfterAdded(XamlElementConverterFactory factory,XElement xamlElement, XElement html)
        {
            var attributes = xamlElement.Attributes()
.Where(attr => attr.Name.LocalName.Contains(".")).ToList();
            for (int i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes.ToList()[i];
                var name1 = attribute.Name.ToString().Substring(0,attribute.Name.ToString().IndexOf('.'));
                if (factory.Converters.TryGetValue(name1, out var dpc))
                {
                    if (dpc is IDependencyPropertyConverter c)
                    {
                        c.HandleAfterAdded(xamlElement, html);
                        var atrs = xamlElement.Attributes().Where(x => x.Name.ToString().StartsWith(name1)).ToList();
                        for(int j = 0; j < atrs.Count; j++)
                        {
                            var atr = atrs[j];
                            atr.Remove();
                            atrs.Remove(atr);
                            attributes.Remove(atr);
                            j--;
                            i--;
                        }
                        if (i < 0)
                        {
                            break;
                        }
                    }
                }

            }
        }

        internal static XElement FindParent(this XElement current, string targetElementName) => FindParentFromElement(current,targetElementName);

        internal static XElement FindParentFromElement(XElement current, string targetElementName)
        {
            var parent = current.Parent;
            if(parent == null)
            {
                return null;
            }
            if(parent?.Name.ToString().ToLower() != targetElementName.ToLower())
            {
                return FindParentFromElement(parent,targetElementName);
            }
            return parent;
        }
    }
}
