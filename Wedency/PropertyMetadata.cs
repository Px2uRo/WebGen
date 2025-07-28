namespace Wedency
{


    /// <summary>定义依赖属性应用于特定类型时的某些行为特性，包括注册时的条件。</summary>
    public class PropertyMetadata
    {
        /// <summary>提供一个方法模板，当依赖属性的值被重新评估，或者显式请求强制时调用该方法。</summary>
        /// <param name="d">该属性所属的对象。回调被调用时，属性系统会传入此值。</param>
        /// <param name="baseValue">属性的新值，在任何强制处理之前的原始值。</param>
        /// <returns>返回经过强制处理后的值（类型适当）。</returns>
        public delegate object CoerceValueCallbackEvent(DependencyObject d, object baseValue);

        /// <summary>获取或设置此元数据中指定的 CoerceValueCallback（强制值回调）实现的引用。</summary>
        /// <exception cref="System.InvalidOperationException">一旦元数据被应用到依赖属性操作，不能再设置该属性。</exception>
        /// <returns>返回一个 CoerceValueCallback 的引用。</returns>
        public CoerceValueCallbackEvent CoerceValueCallback
        {
            get { throw null; }
            set { }
        }

        /// <summary>获取或设置依赖属性的默认值。</summary>
        /// <exception cref="System.ArgumentException">创建后不能设置为 DependencyProperty.UnsetValue。</exception>
        /// <exception cref="System.InvalidOperationException">一旦元数据被应用到依赖属性操作，不能再设置该属性。</exception>
        /// <returns>属性的默认值。使用无参构造函数创建的 PropertyMetadata 实例默认值为 DependencyProperty.UnsetValue。</returns>
        public object DefaultValue
        {
            get { throw null; }
            set { }
        }

        /// <summary>获取一个值，指示此元数据是否已被应用于属性，导致该元数据实例变为不可变状态。</summary>
        /// <returns>如果元数据实例已被封存（不可变），则返回 true，否则返回 false。</returns>
        protected bool IsSealed
        {
            get { throw null; }
        }

        /// <summary>表示在依赖属性的有效值发生变化时调用的回调方法。
        /// <param name="d">发生属性值变化的 <see cref="Wedency.DependencyObject" /> 对象。</param>
        /// <param name="e">跟踪该属性有效值变化的事件所携带的事件数据。</param>
        /// </summary>
        public delegate void PropertyChangedCallbackEvent(DependencyObject d, DependencyPropertyChangedEventArgs e);

        /// <summary>获取或设置此元数据中指定的 PropertyChangedCallback（属性更改回调）实现的引用。</summary>
        /// <exception cref="System.InvalidOperationException">一旦元数据被应用到依赖属性操作，不能再设置该属性。</exception>
        /// <returns>返回一个 PropertyChangedCallback 的引用。</returns>
        public PropertyChangedCallbackEvent PropertyChangedCallback
        {
            get { throw null; }
            set { }
        }

        /// <summary>初始化 PropertyMetadata 类的新实例。</summary>
        public PropertyMetadata()
        {
        }

        /// <summary>用指定的依赖属性默认值初始化 PropertyMetadata 类的新实例。</summary>
        /// <param name="defaultValue">依赖属性的默认值，通常为某个具体类型的值。</param>
        /// <exception cref="System.ArgumentException">defaultValue 不能设置为 DependencyProperty.UnsetValue。</exception>
        public PropertyMetadata(object defaultValue)
        {
        }

        /// <summary>用指定的默认值和属性更改回调初始化 PropertyMetadata 类的新实例。</summary>
        /// <param name="defaultValue">依赖属性的默认值，通常为某个具体类型的值。</param>
        /// <param name="propertyChangedCallback">属性系统在属性值变化时调用的回调处理程序的引用。</param>
        /// <exception cref="System.ArgumentException">defaultValue 不能设置为 DependencyProperty.UnsetValue。</exception>
        public PropertyMetadata(object defaultValue, PropertyChangedCallbackEvent propertyChangedCallback)
        {
        }

        /// <summary>用指定的默认值、属性更改回调和强制值回调初始化 PropertyMetadata 类的新实例。</summary>
        /// <param name="defaultValue">依赖属性的默认值，通常为某个具体类型的值。</param>
        /// <param name="propertyChangedCallback">属性值变化时调用的回调处理程序引用。</param>
        /// <param name="coerceValueCallback">当属性系统调用 DependencyObject.CoerceValue 方法时调用的回调处理程序引用。</param>
        /// <exception cref="System.ArgumentException">defaultValue 不能设置为 DependencyProperty.UnsetValue。</exception>
        public PropertyMetadata(object defaultValue, PropertyChangedCallbackEvent propertyChangedCallback, CoerceValueCallbackEvent coerceValueCallback)
        {
        }

        /// <summary>用指定的属性更改回调初始化 PropertyMetadata 类的新实例。</summary>
        /// <param name="propertyChangedCallback">属性系统在属性值变化时调用的回调处理程序引用。</param>
        public PropertyMetadata(PropertyChangedCallbackEvent propertyChangedCallback)
        {
        }

        /// <summary>将此元数据与基类元数据合并。</summary>
        /// <param name="baseMetadata">要合并的基类元数据。</param>
        /// <param name="dp">此元数据将应用到的依赖属性。</param>
        protected virtual void Merge(PropertyMetadata baseMetadata, DependencyProperty dp)
        {
        }

        /// <summary>当此元数据被应用到某属性时调用，表示元数据即将被封存。</summary>
        /// <param name="dp">元数据被应用的依赖属性。</param>
        /// <param name="targetType">与此元数据相关联的类型（如果是类型特定元数据），如果是默认元数据，则为 null。</param>
        protected virtual void OnApply(DependencyProperty dp, Type targetType)
        {
        }
    }
}