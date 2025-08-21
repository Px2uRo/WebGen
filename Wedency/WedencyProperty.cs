using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Text;
using System.Xml.Linq;
using WebGen.Attributes;
using WebGen.Collections;

namespace Wedency
{    
    /// <summary>
    /// Defines possible binding modes.
    /// </summary>
    public enum BindingMode
    {
        /// <summary>
        /// Uses the default binding mode specified for the property.
        /// </summary>
        Default,

        /// <summary>
        /// Binds one way from source to target.
        /// </summary>
        OneWay,

        /// <summary>
        /// Binds two-way with the initial value coming from the target.
        /// </summary>
        TwoWay,

        /// <summary>
        /// Updates the target when the application starts or when the data context changes.
        /// </summary>
        OneTime,

        /// <summary>
        /// Binds one way from target to source.
        /// </summary>
        OneWayToSource,
    }
    /// <summary>
    /// Base class for WebGen property metadata.
    /// </summary>
    public class PropertyMetadata
    {
        private BindingMode _defaultBindingMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata"/> class.
        /// </summary>
        /// <param name="defaultBindingMode">The default binding mode.</param>
        public PropertyMetadata(
            BindingMode defaultBindingMode = BindingMode.Default)
        {
            _defaultBindingMode = defaultBindingMode;
        }

        /// <summary>
        /// Gets the default binding mode for the property.
        /// </summary>
        public BindingMode DefaultBindingMode
        {
            get
            {
                return _defaultBindingMode == BindingMode.Default ?
                    BindingMode.OneWay : _defaultBindingMode;
            }
        }

        /// <summary>
        /// Merges the metadata with the base metadata.
        /// </summary>
        /// <param name="baseMetadata">The base metadata to merge.</param>
        /// <param name="property">The property to which the metadata is being applied.</param>
        public virtual void Merge(
            PropertyMetadata baseMetadata,
            WedencyProperty property)
        {
            if (_defaultBindingMode == BindingMode.Default)
            {
                _defaultBindingMode = baseMetadata.DefaultBindingMode;
            }
        }
    }

    /// <summary>
    /// 类比 Avalonia 的 AvaloniaProperty，里面的可执行代码其实不会执行。只需要里面的成员就行了，这个类只是为了好生成动画和资源字典用的。
    /// </summary>
    [WedencyClass]
    public class WedencyProperty : IEquatable<WedencyProperty>
    {
        public static readonly object UnsetValue = new object();

        int Id = 0;

        public WedencyProperty(string name, Type propertyType, Type ownerType)
        {
            Name = name;
            PropertyType = propertyType;
            OwnerType = ownerType;
        }

        [WedencyCtor]
        protected WedencyProperty(string name, Type propertyType, Type ownerType, PropertyMetadata metadata)
        {
#if false //废弃的
            WedencyContract.Requires<ArgumentNullException>(name != null);
            WedencyContract.Requires<ArgumentNullException>(propertyType != null);
            WedencyContract.Requires<ArgumentNullException>(ownerType != null);
            WedencyContract.Requires<ArgumentNullException>(metadata != null);
            Name = name;
            PropertyType = propertyType;
            OwnerType = ownerType;

            
            if (name.Contains("."))
            {
                throw new ArgumentException("'name' may not contain periods.");
            }

            _metadata = new Dictionary<Type, PropertyMetadata>();

            Name = name;
            PropertyType = valueType;
            OwnerType = ownerType;  
            Notifying = notifying;
            Id = s_nextId++;

            _metadata.Add(ownerType, metadata);
            _defaultMetadata = metadata;
#endif
        }

        public string Name { get; }
        public Type PropertyType { get; }
        public Type OwnerType { get; }
        [WedencyMethod]
        public bool Equals(WedencyProperty other)
        {
            return GetHashCode() == other.GetHashCode();
        }
    }
    /// <summary>
    /// 根据 <see cref="Type"/> 作为一个实际属性来定义的 <see cref="WedencyProperty" />
    /// </summary>
    /// <typeparam name="TValue">数据类型，如 typeof(<see cref="string")/></typeparam>
    [WedencyClass]
    public class WedencyProperty<TValue> : WedencyProperty
    {
        protected WedencyProperty(string name, Type propertyType, Type ownerType) : base(name, propertyType, ownerType)
        {

        }
    }
    [WedencyClass]
    public class StyleedPropertyBase<TValue> : WedencyProperty<TValue>
    {
        protected StyleedPropertyBase(string name, Type propertyType, Type ownerType) : base(name, propertyType, ownerType)
        {

        }
    }

    /// <summary>
    /// 支持 <see cref="WedencyProperty"/> 的对象
    /// </summary>
    /// <remarks>
    /// This class is analogous to DependencyObject in WPF.
    /// </remarks>
    [WebGenBase]
    public class WedencyObject : IWedencyObject,
        INotifyPropertyChanged
#if false //TODO 都是Avalonia的一些东西
        ,IAvaloniaObjectDebug
        ,IPriorityValueOwner
#endif
    {
        public event PropertyChangedEventHandler PropertyChanged;

        event EventHandler<WedencyPropertyChangedEventArgs> IWedencyObject.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void Bind(WedencyProperty property, IObservable<object> source, BindingPriority priority = BindingPriority.LocalValue)
        {
            throw new NotImplementedException();
        }

        public IDisposable Bind<T>(WedencyProperty<T> property, IObservable<T> source, BindingPriority priority = BindingPriority.LocalValue)
        {
            throw new NotImplementedException();
        }

        public object GetValue(WedencyProperty property)
        {
            throw new NotImplementedException();
        }

        public T GetValue<T>(WedencyProperty<T> property)
        {
            throw new NotImplementedException();
        }

        public bool IsSet(WedencyProperty property)
        {
            throw new NotImplementedException();
        }

        public void SetValue(WedencyProperty property, object value, BindingPriority priority = BindingPriority.LocalValue)
        {
            throw new NotImplementedException();
        }

        public void SetValue<T>(WedencyProperty<T> property, T value, BindingPriority priority = BindingPriority.LocalValue)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// 给<see cref="WedencyObject"> 赋值用的接口。
    /// </summary>
    [WebGenBase]
    public interface IWedencyObject
    {
        /// <summary>
        /// <see cref="WedencyProperty"/> 的值改变时触发。
        /// </summary>
        [WebGenEvent]
        event EventHandler<WedencyPropertyChangedEventArgs> PropertyChanged;

        /// <summary>
        /// 得到 <see cref="WedencyProperty "/> 的值
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>值</returns>
        object GetValue(WedencyProperty  property);

        /// <summary>
        /// 得到 <see cref="WedencyProperty"/> 的值
        /// </summary>
        /// <typeparam name="T">属性的类型</typeparam>
        /// <param name="property">属性</param>
        /// <returns>值</returns>
        T GetValue<T>(WedencyProperty<T> property);

        /// <summary>
        /// Checks whether a <see cref="WedencyProperty"/> is set on this object.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>True if the property is set, otherwise false.</returns>
        bool IsSet(WedencyProperty property);

        /// <summary>
        /// Sets a <see cref="AvaloniaProperty"/> value.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority of the value.</param>
        void SetValue(
            WedencyProperty property,
            object value,
            BindingPriority priority = BindingPriority.LocalValue);

        /// <summary>
        /// Sets a <see cref="AvaloniaProperty"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="priority">The priority of the value.</param>
        void SetValue<T>(
            WedencyProperty<T> property,
            T value,
            BindingPriority priority = BindingPriority.LocalValue);

        /// <summary>
        /// Binds a <see cref="AvaloniaProperty"/> to an observable.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        void Bind(
            WedencyProperty property,
            IObservable<object> source,
            BindingPriority priority = BindingPriority.LocalValue);

        /// <summary>
        /// Binds a <see cref="AvaloniaProperty"/> to an observable.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="source">The observable.</param>
        /// <param name="priority">The priority of the binding.</param>
        IDisposable Bind<T>(
            WedencyProperty<T> property,
            IObservable<T> source,
            BindingPriority priority = BindingPriority.LocalValue);
    }

    /// <summary>
    /// 提供有关 Avalonia 属性更改的信息。
    /// </summary>
    public class WedencyPropertyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化 <see cref="WedencyPropertyChangedEventArgs"/> 类的新实例。
        /// </summary>
        /// <param name="sender">属性发生更改的对象。</param>
        /// <param name="property">发生更改的属性。</param>
        /// <param name="oldValue">属性的旧值。</param>
        /// <param name="newValue">属性的新值。</param>
        /// <param name="priority">生成该值的绑定优先级。</param>
        public WedencyPropertyChangedEventArgs(
            WedencyObject sender,
            WedencyProperty property,
            object oldValue,
            object newValue,
            BindingPriority priority)
        {
            Sender = sender;
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
            Priority = priority;
        }

        /// <summary>
        /// 获取属性发生更改的 <see cref="WedencyObject"/>。
        /// </summary>
        /// <value>触发更改的对象。</value>
        public WedencyObject Sender { get; private set; }

        /// <summary>
        /// 获取发生更改的属性。
        /// </summary>
        /// <value>
        /// 发生更改的属性。
        /// </value>
        public WedencyProperty Property { get; private set; }

        /// <summary>
        /// 获取属性的旧值。
        /// </summary>
        /// <value>
        /// 属性的旧值。
        /// </value>
        public object OldValue { get; private set; }

        /// <summary>
        /// 获取属性的新值。
        /// </summary>
        /// <value>
        /// 属性的新值。
        /// </value>
        public object NewValue { get; private set; }

        /// <summary>
        /// 获取生成该值的绑定优先级。
        /// </summary>
        /// <value>
        /// 生成该值的绑定优先级。
        /// </value>
        public BindingPriority Priority { get; private set; }
    }

    /// <summary>
    /// 绑定的优先级。
    /// </summary>
    public enum BindingPriority
    {
        /// <summary>
        /// 来自动画的值。
        /// </summary>
        Animation = -1,

        /// <summary>
        /// 本地值。
        /// </summary>
        LocalValue = 0,

        /// <summary>
        /// 触发器样式绑定。
        /// </summary>
        /// <remarks>
        /// 样式触发器是类似 `.class` 这样的选择器，它会覆盖
        /// <see cref="TemplatedParent"/> 绑定。  
        /// 这样，一个基础控件可以例如从模板父级继承一个 `Background`，并且在控件具有
        /// `:pointerover` 类时发生变化。
        /// </remarks>
        StyleTrigger,

        /// <summary>
        /// 绑定到模板父级上的某个属性。
        /// </summary>
        TemplatedParent,

        /// <summary>
        /// 样式绑定。
        /// </summary>
        Style,

        /// <summary>
        /// 绑定未初始化。
        /// </summary>
        Unset = int.MaxValue,
    }
    /// <summary>
    /// A collection of <see cref="PropertyTransition"/> definitions.
    /// </summary>
    public class PropertyTransitions : 
        WebGenList<PropertyTransition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTransitions"/> class.
        /// </summary>
        public PropertyTransitions()
        {
            ResetBehavior = ResetBehavior.Remove;
        }
    }

    /// <summary>
    /// Defines how a property should be animated using a transition.
    /// </summary>
    public class PropertyTransition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTransition"/> class.
        /// </summary>
        /// <param name="property">The property to be animated/</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="easing">The easing function to use.</param>
        public PropertyTransition(WedencyProperty property, TimeSpan duration, IEasing easing)
        {
            Property = property;
            Duration = duration;
            Easing = easing;
        }

        /// <summary>
        /// Gets the property to be animated.
        /// </summary>
        /// <value>
        /// The property to be animated.
        /// </value>
        public WedencyProperty Property { get; }

        /// <summary>
        /// Gets the duration of the animation.
        /// </summary>
        /// <value>
        /// The duration of the animation.
        /// </value>
        public TimeSpan Duration { get; }

        /// <summary>
        /// Gets the easing function used.
        /// </summary>
        /// <value>
        /// The easing function.
        /// </value>
        public IEasing Easing { get; }
    }

    /// <summary>
    /// 缓动方法的接口
    /// </summary>
    public interface IEasing
    {
        /// <summary>
        /// 函数。
        /// </summary>
        /// <param name="progress">The progress of the transition, from 0 to 1.</param>
        /// <param name="start">The start value of the transition.</param>
        /// <param name="finish">The end value of the transition.</param>
        /// <returns>
        /// A value between <paramref name="start"/> and <paramref name="finish"/> as determined
        /// by <paramref name="progress"/>.
        /// </returns>
        object Ease(double progress, object start, object finish);
    }


    public class Animatable : WedencyObject
    {
        /// <summary>
        /// The property transitions for the control.
        /// </summary>
        private PropertyTransitions _propertyTransitions;

        /// <summary>
        /// Gets or sets the property transitions for the control.
        /// </summary>
        /// <value>
        /// The property transitions for the control.
        /// </value>
        public PropertyTransitions PropertyTransitions
        {
            get
            {
                return _propertyTransitions ?? (_propertyTransitions = new PropertyTransitions());
            }

            set
            {
                _propertyTransitions = value;
            }
        }
#if false //TODO 我还没想好怎么取代。
        /// <summary>
        /// Reacts to a change in a <see cref="AvaloniaProperty"/> value in order to animate the
        /// change if a <see cref="PropertyTransition"/> is set for the property..
        /// </summary>
        /// <param name="e">The event args.</param>
        protected override void OnPropertyChanged(WedencyPropertyChangedEventArgs e)
        {
            if (e.Priority != BindingPriority.Animation && _propertyTransitions != null)
            {
                var match = _propertyTransitions.FirstOrDefault(x => x.Property == e.Property);

                if (match != null)
                {
                    Animate.Property(this, e.Property, e.OldValue, e.NewValue, match.Easing, match.Duration);
                }
            }
        }
#endif
    }

}

