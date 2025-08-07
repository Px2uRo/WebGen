using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGen.Converters.CSharp;

namespace WebGen.Attributes
{
    /// <summary>
    /// 该属性用于指明源生成器不要用将这个类转成js。而是用生成器用别的方法把这个类转换成别的东西。
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class CodeGenClassAttribute : Attribute
    {
        readonly string positionalString;
        /// <summary>
        /// 如果你想，可以编译时反射来生成代码。
        /// </summary>
        /// <param name="CSSyntaxConverter">反射的代码转换器</param>
        public CodeGenClassAttribute(Type CSSyntaxConverter) 
        {
            var ins = Activator.CreateInstance(CSSyntaxConverter);
            if (ins is CSSyntaxConverter cs)
            {

            }
        }
        public CodeGenClassAttribute()
        {
            
        }
    }
}
