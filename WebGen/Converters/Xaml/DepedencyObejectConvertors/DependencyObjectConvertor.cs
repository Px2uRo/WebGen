using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGen.Core;

namespace WebGen.Converters.Xaml.DepedencyObejectConvertors
{
    /// <summary>
    /// 有依赖的对象转换器基类
    /// </summary>
    internal class DependencyObjectConvertor : XamlElementConverter
    {
        public DependencyObjectConvertor(XamlElementConverterFactory factory) : base(factory)
        {
        }

        public override string ConvertToHtmlString(XElement element)
        {
            throw new NotImplementedException();
        }

        public override XElement ConvertToHtmlXElement(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}
