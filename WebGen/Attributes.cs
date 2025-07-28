using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGen.Converters.CSharp;

namespace WebGen.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class WebGenClassAttribute : Attribute
    {
        readonly string positionalString;

        public WebGenClassAttribute(Type CSSyntaxConverter) 
        {
            var ins = Activator.CreateInstance(CSSyntaxConverter);
            if (ins is CSSyntaxConverter cs)
            {

            }
        }
        public WebGenClassAttribute()
        {
            
        }
    }
}
