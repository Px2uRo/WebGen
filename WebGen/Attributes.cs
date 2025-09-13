using Microsoft.CodeAnalysis;
using WebGen.Converters.CSharp;

namespace WebGen.Attributes
{
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

    /// <summary>
    /// 就是一个 HTTP 动词表。全部大写是因为 Swagger 就是这么干的。
    /// </summary>
    public enum HttpVerbs
    {
        GET = 0,
        POST = 2,
        PUT = 4,
        PATCH = 8,
        DELETE = 16,
        HEAD = 32,
        OPTIONS = 64
    }

    /// <summary>
    /// 加在一个方法上，用来快速生成 WebAPI，说明这个方法会被导出到 Minimal API 的 Endpoint 中。
    /// 方法返回的东西会被序列化成 JSON，序列化器用的是 System.Text.JSON。
    /// 访问该方法使用 api.<see cref=""/>
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    sealed class MinimalAPIExportAttribute:Attribute
    {
        public MinimalAPIExportAttribute(HttpVerbs verb = HttpVerbs.GET)
        {

        }
    }
}
