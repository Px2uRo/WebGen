using System;
using System.Collections.Generic;
using System.Text;
using WebGen.Converters.CSharp;

namespace Wedency
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class WendecyCodeGenAttribute : Attribute
    {
        public CSSyntaxConverter CSSyntax { get; }
        public WendecyCodeGenAttribute()
        {

        }

        public WendecyCodeGenAttribute(Type CSSyntaxConverter)
        {
            CSSyntax = Activator.CreateInstance(CSSyntaxConverter) as CSSyntaxConverter;
        }
    }
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class WedencyClassAttribute : WendecyCodeGenAttribute
    {
        public WedencyClassAttribute():base()
        {

        }

        public WedencyClassAttribute(Type CSSyntaxConverter) : base(CSSyntaxConverter)
        {

        }
    }

    /// <summary>
    /// 这个是给接口打的标签。
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Interface|AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class WebGenBaseAttribute : WendecyCodeGenAttribute
    {
        public WebGenBaseAttribute() : base()
        {

        }

        public WebGenBaseAttribute(Type CSSyntaxConverter) : base(CSSyntaxConverter)
        {

        }
    }
    /// <summary>
    /// 这个是给事件打的标签。
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Interface | AttributeTargets.Event, Inherited = true, AllowMultiple = true)]
    public sealed class WebGenEventAttribute : WendecyCodeGenAttribute
    {
        public WebGenEventAttribute() : base()
        {

        }

        public WebGenEventAttribute(Type CSSyntaxConverter) : base(CSSyntaxConverter)
        {

        }
    }

    [System.AttributeUsage(AttributeTargets.Constructor, Inherited = true, AllowMultiple = true)]
    sealed public class WedencyCtorAttribute : WedencyClassAttribute
    {

    }

    [System.AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    sealed public class WedencyMethodAttribute : WedencyClassAttribute
    {

    }
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class RelatedToHtmlAttribute : WendecyCodeGenAttribute
    {

    }
    /// <summary>
    /// 加在一个类上，主要是用来给View用的
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class MinimalAPICodeGenAttribute : WedencyClassAttribute
    {
        public MinimalAPICodeGenAttribute()
        {
        }

        public MinimalAPICodeGenAttribute(Type ClassDeclarationConvertor) : base(ClassDeclarationConvertor)
        {
            if (Activator.CreateInstance(ClassDeclarationConvertor) is ClassDeclarationConvertor cs)
            {
                return;
            }
            WedencyContract.Requires<Exception>(false);
        }
    }



    [System.AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    sealed public class NETMethodRewriteAttribute : Attribute
    {
        /// <summary>
        /// 如果被打标签的是一个方法，则生成器会在调用了此方法的地方替换代码。
        /// 方法的主体不需要写代码。
        /// 如果打标签的是一个类
        /// </summary>
        public NETMethodRewriteAttribute()
        {

        }
    }
    /// <summary>
    /// get 和 set 的方法体都替换成 JS 可执行代码。
    /// 可通过在添加 <see cref="SeleniumEnvAttribute"/> 在 Selenium 环境
    /// 将 Css 的 value 转换为 .NET 对象便于调试。
    /// </summary>

    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class CSSPropDefAttribute : Attribute
    {
        /// <summary>
        /// 属性名称，如果是多个属性，用这个<see cref="MutilCssPropDefAttribute" />
        /// </summary>
        public string PropName { get; }
        public CSSPropDefAttribute(string propName)
        {
            PropName = propName;
        }
        /// <summary>
        /// 没有参数就是说明这个 Css 属性对应 .NET 属性的小写。
        /// </summary>
        public CSSPropDefAttribute()
        {
            
        }
    }
    /// <summary>
    /// 适用于一个成员对应Css的多个成员。
    /// 因为不能在特性的构造函数中添加 lambda 表达式，
    /// 自己填 MethodName 弄 Mapping 吧。
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public sealed class MutilCssPropDefAttribute : CSSPropDefAttribute
    {
        /// <summary>
        /// 涉及了的 Css 属性名，前面填属性后面填 .NET 属性的Member。
        /// </summary>
        public IEnumerable<string> ReferedPropMappingMethods { get; }
        public string PropName => throw new MemberAccessException($"在{nameof(MutilCssPropDefAttribute)}中，" +
            $"成员{nameof(PropName)}被弃用，改用{nameof(MutilCssPropDefAttribute)}.{nameof(ReferedPropMappingMethods)}进行分析");
        /// <summary>
        /// 适用于一个成员对应Css的多个成员。
        /// 因为不能在特性的构造函数中添加 lambda 表达式，
        /// 自己填 MethodName 弄 Mapping 吧
        /// </summary>
        /// <param name="referedPropMappingMethods">涉及到的属性的方法名，规定方法名就是css属性名。</param>
        public MutilCssPropDefAttribute(string[] referedPropMappingMethods)
        {
            ReferedPropMappingMethods = referedPropMappingMethods;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    sealed class SeleniumEnvAttribute : Attribute
    {

    }

}
