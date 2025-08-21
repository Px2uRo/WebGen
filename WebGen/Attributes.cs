using Microsoft.CodeAnalysis;
using WebGen.Converters.CSharp;

namespace WebGen.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class GenMinimalAPIAttribute : Attribute
    {
        // This is a positional argument
        public GenMinimalAPIAttribute(string positionalString)
        {

        }
    }
    /// <summary>
    /// 该属性用于指明源生成器用别的方法把这个类转换成转换成JS。
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class WebGenES3Attribute : Attribute
    {
        CSSyntaxConverter _cs;
        /// <summary>
        /// 如果你想，可以编译时反射来生成代码。
        /// </summary>
        /// <param name="CSSyntaxConverter">反射的代码转换器</param>
        public WebGenES3Attribute(Type CSSyntaxConverter)
        {
            var ins = Activator.CreateInstance(CSSyntaxConverter);
            if (ins is CSSyntaxConverter cs)
            {
                _cs = cs;
            }
        }
        public WebGenES3Attribute()
        {
            
        }
        /// <summary>
        /// 这个方法是给源生成器用的。
        /// </summary>
        /// <returns>代码</returns>
        public string GetCode(SyntaxNode node)
        {
            return _cs.ConvertToJSString(node);
        }
    }

    [System.AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    sealed class WebGenBaseClassAttribute : WebGenES3Attribute
    {
        public WebGenBaseClassAttribute(Type CSSyntaxConverter) :base(CSSyntaxConverter)
        {
            
        }
        public WebGenBaseClassAttribute()
        {
            
        }
    }
}
