using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;
using WebGen.XmlUtil;

namespace WebGen.Converters.Xaml
{
    public abstract class XamlElementConverter
    {
        public virtual XamlElementConverterFactory _factory { get; private set; }
        public XamlElementConverter(XamlElementConverterFactory factory)
        {
            _factory = factory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public abstract string ConvertToHtmlString(XElement element);
        public abstract XElement ConvertToHtmlXElement(XElement element);
        public virtual XElement HandleDependencyProperties(XElement sourceElement,XElement htmlElement)
        {
            var attributes = sourceElement.Attributes()
            .Where(attr => attr.Name.LocalName.Contains("."))
            .ToDictionary(attr => attr.Name.LocalName, attr => attr.Value);

            foreach (var attribute in attributes)   
            {

                var name = attribute.Key[..attribute.Key.ToString().IndexOf('.')];
                if (_factory.Converters.TryGetValue(name, out var converter))
                {
                    if(converter is DependencyPropertyConverter c)
                    {
                        c.EditXElement(attribute.Key, attribute.Value, sourceElement.FindParent(name), htmlElement); ;
                    }
                }
            }

            return htmlElement;
        }
    }
}
