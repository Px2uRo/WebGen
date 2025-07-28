using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGen.Attributes;

namespace Wedency
{
    [WebGenClass]
    public sealed class DependencyProperty
    {
        /// <summary>指定WPF属性系统使用的一个静态值，而不是 <see langword="null" />，表示该属性存在，但未通过属性系统设置其值。</summary>
        public static readonly object UnsetValue;

        /// <summary>获取依赖属性的默认元数据。</summary>
        /// <returns>依赖属性的默认元数据。</returns>
        public PropertyMetadata DefaultMetadata
        {
            get
            {
                throw null;
            }
        }

        /// <summary>获取唯一标识该依赖属性的内部生成值。</summary>
        /// <returns>唯一的数值标识符。</returns>
        public int GlobalIndex
        {
            get
            {
                throw null;
            }
        }

        /// <summary>获取依赖属性的名称。</summary>
        /// <returns>属性的名称。</returns>
        public string Name
        {
            get
            {
                throw null;
            }
        }

        /// <summary>获取注册该依赖属性或添加自身作为该属性所有者的对象类型。</summary>
        /// <returns>注册该属性或添加自身为属性所有者的对象类型。</returns>
        public Type OwnerType
        {
            get
            {
                throw null;
            }
        }

        /// <summary>获取依赖属性用于存储值的类型。</summary>
        /// <returns>属性值的 <see cref="T:System.Type" />。</returns>
        public Type PropertyType
        {
            get
            {
                throw null;
            }
        }

        /// <summary>获取一个值，该值指示由此<see cref="Wedency.DependencyProperty" />实例标识的依赖属性是否为只读属性。</summary>
        /// <returns>
        /// 如果该依赖属性是只读的，则返回<see langword="true" />；否则返回<see langword="false" />。
        /// </returns>
        public bool ReadOnly
        {
            get
            {
                throw null;
            }
        }

        /// <summary>表示用于验证依赖属性有效值的回调方法。</summary>
        /// <param name="value">要验证的值。</param>
        /// <returns>
        ///   如果值通过验证，则返回 <see langword="true" />；如果提交的值无效，则返回 <see langword="false" />。</returns>
        public delegate bool ValidateValueCallbackDelegate(object value);

        /// <summary>获取该依赖属性的值验证回调函数。</summary>
        /// <returns>在依赖属性注册时通过<paramref name="validateValueCallback" />参数提供的值验证回调函数。</returns>
        public ValidateValueCallbackDelegate ValidateValueCallback
        {
            get
            {
                throw null;
            }
        }

        internal DependencyProperty()
        {
        }

        /// <summary>将另一个类型添加为已注册的依赖属性的所有者。</summary>
        /// <param name="ownerType">添加为该依赖属性所有者的类型。</param>
        /// <returns>标识该依赖属性的原始<see cref="Wedency.DependencyProperty" />引用。应将此标识符作为<see langword="public static readonly" />字段公开。</returns>
        public DependencyProperty AddOwner(Type ownerType)
        {
            throw null;
        }

        /// <summary>将另一个类型添加为已注册的依赖属性的所有者，并提供该依赖属性在指定所有者类型上的元数据。</summary>
        /// <param name="ownerType">要添加为该依赖属性所有者的类型。</param>
        /// <param name="typeMetadata">该依赖属性在指定类型上的元数据。</param>
        /// <returns>标识该依赖属性的原始<see cref="Wedency.DependencyProperty" />引用。应将此标识符作为<see langword="public static readonly" />字段公开。</returns>
        public DependencyProperty AddOwner(Type ownerType, PropertyMetadata typeMetadata)
        {
            throw null;
        }

        /// <summary>返回此<see cref="Wedency.DependencyProperty" />的哈希码。</summary>
        /// <returns>此<see cref="Wedency.DependencyProperty" />的哈希码。</returns>
        public override int GetHashCode()
        {
            throw null;
        }

        /// <summary>返回指定类型上的依赖属性元数据。</summary>
        /// <param name="forType">要从中检索元数据的类型。</param>
        /// <returns>属性元数据对象。</returns>
        public PropertyMetadata GetMetadata(Type forType)
        {
            throw null;
        }

        /// <summary>返回指定对象实例上的依赖属性元数据。</summary>
        /// <param name="dependencyObject">用于检查类型的依赖对象，以确定元数据应来自哪种特定类型。</param>
        /// <returns>属性元数据对象。</returns>
        public PropertyMetadata GetMetadata(DependencyProperty dependencyObject)
        {
            throw null;
        }

        /// <summary>返回指定类型上的依赖属性元数据。</summary>
        /// <param name="dependencyObjectType">记录希望获得元数据的依赖对象类型的特定对象。</param>
        /// <returns>属性元数据对象。</returns>
        public PropertyMetadata GetMetadata(DependencyObjectType dependencyObjectType)
        {
            throw null;
        }

        /// <summary>确定指定的值是否适用于此依赖属性的类型，与原始依赖属性注册时提供的属性类型进行检查。</summary>
        /// <param name="value">要检查的值。</param>
        /// <returns>若指定的值为已注册的属性类型或可接受的派生类型，则为<see langword="true" />；否则为<see langword="false" />。</returns>
        public bool IsValidType(object value)
        {
            throw null;
        }

        /// <summary>确定提供的值是否适合该属性的类型，并检查其是否在允许的值范围内。</summary>
        /// <param name="value">要检查的值。</param>
        /// <returns>若值可接受且属于正确的类型或派生类型，则为<see langword="true" />；否则为<see langword="false" />。</returns>
        public bool IsValidValue(object value)
        {
            throw null;
        }

        /// <summary>当依赖属性存在于指定类型实例上时，指定该依赖属性的替代元数据，以覆盖从基类型继承的元数据。</summary>
        /// <param name="forType">继承该依赖属性并应用提供的替代元数据的类型。</param>
        /// <param name="typeMetadata">要应用于覆盖类型上的依赖属性的元数据。</param>
        public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata)
        {
        }

        /// <summary>使用指定的属性名称、类型和所有者类型注册依赖属性。</summary>
        /// <param name="name">要注册的依赖属性的名称。</param>
        /// <param name="propertyType">属性的类型。</param>
        /// <param name="ownerType">注册该依赖属性的所有者类型。</param>
        /// <returns>依赖属性标识符。</returns>
        public static DependencyProperty Register(string name, Type propertyType, Type ownerType)
        {
            throw null;
        }

        // 其他Register、RegisterAttached、RegisterReadOnly等方法的中文注释同理，此处因篇幅省略，但格式一致。

        /// <summary>返回依赖属性的字符串表示。</summary>
        /// <returns>依赖属性的字符串表示。</returns>
        public override string ToString()
        {
            throw null;
        }
    }
}
