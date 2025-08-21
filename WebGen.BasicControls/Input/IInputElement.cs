using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using WebGen.Controls.Layouts;
using Wedency;

namespace WebGen.Controls.Input
{
    [WebGenBase]
    public interface IInputElement : IInteractive
    {
#if false //TODO 以下内容来自 Avalonia 其实都是一些事件。

        /// <summary>
        /// Occurs when the control receives focus.
        /// </summary>
        event EventHandler<RoutedEventArgs> GotFocus;

        /// <summary>
        /// Occurs when the control loses focus.
        /// </summary>
        event EventHandler<RoutedEventArgs> LostFocus;

        /// <summary>
        /// Occurs when a key is pressed while the control has focus.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// Occurs when a key is released while the control has focus.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Occurs when a user typed some text while the control has focus.
        /// </summary>
        event EventHandler<TextInputEventArgs> TextInput;

        /// <summary>
        /// Occurs when the pointer enters the control.
        /// </summary>
        event EventHandler<PointerEventArgs> PointerEnter;

        /// <summary>
        /// Occurs when the pointer leaves the control.
        /// </summary>
        event EventHandler<PointerEventArgs> PointerLeave;

        /// <summary>
        /// Occurs when the pointer is pressed over the control.
        /// </summary>
        event EventHandler<PointerPressedEventArgs> PointerPressed;

        /// <summary>
        /// Occurs when the pointer moves over the control.
        /// </summary>
        event EventHandler<PointerEventArgs> PointerMoved;

        /// <summary>
        /// Occurs when the pointer is released over the control.
        /// </summary>
        event EventHandler<PointerReleasedEventArgs> PointerReleased;

        /// <summary>
        /// Occurs when the mouse wheen is scrolled over the control.
        /// </summary>
        event EventHandler<PointerWheelEventArgs> PointerWheelChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the control can receive keyboard focus.
        /// </summary>  
        bool Focusable { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the control is enabled for user interaction.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Gets or sets the associated mouse cursor.
        /// </summary>
        Cursor Cursor { get; }

        /// <summary>
        /// Gets a value indicating whether the control is effectively enabled for user interaction.
        /// </summary>
        /// <remarks>
        /// The <see cref="IsEnabled"/> property is used to toggle the enabled state for individual
        /// controls. The <see cref="IsEnabledCore"/> property takes into account the
        /// <see cref="IsEnabled"/> value of this control and its parent controls.
        /// </remarks>
        bool IsEnabledCore { get; }

        /// <summary>
        /// Gets a value indicating whether the control is focused.
        /// </summary>
        bool IsFocused { get; }

        /// <summary>
        /// Gets a value indicating whether the control is considered for hit testing.
        /// </summary>
        bool IsHitTestVisible { get; }

        /// <summary>
        /// Gets a value indicating whether the pointer is currently over the control.
        /// </summary>
        bool IsPointerOver { get; }

        /// <summary>
        /// Focuses the control.
        /// </summary>
        void Focus();

        /// <summary>
        /// Gets the key bindings for the element.
        /// </summary>
        List<KeyBinding> KeyBindings { get; }
#endif
    }
    /// <summary>
    /// 可触发路由事件的控件接口。
    /// </summary>
    public interface IInteractive:ILayoutable
    {
        /// <summary>
        /// 冒泡事件和隧道事件的父 Interactive。
        /// </summary>
        IInteractive InteractiveParent { get; }

        /// <summary>
        /// 为特定路由事件添加处理程序。
        /// </summary>
        /// <param name="routedEvent">路由事件。</param>
        /// <param name="handler">处理程序。</param>
        /// <param name="routes">要监听的路由策略。</param>
        /// <param name="handledEventsToo">是否也监听已处理的事件。</param>
        /// <returns>终止事件订阅的可释放对象。</returns>
        IDisposable AddHandler(
            RoutedEvent routedEvent,
            Delegate handler,
            RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
            bool handledEventsToo = false);

        /// <summary>
        /// 为特定路由事件添加处理程序。
        /// </summary>
        /// <typeparam name="TEventArgs">事件参数类型。</typeparam>
        /// <param name="routedEvent">路由事件。</param>
        /// <param name="handler">处理程序。</param>
        /// <param name="routes">要监听的路由策略。</param>
        /// <param name="handledEventsToo">是否也监听已处理的事件。</param>
        /// <returns>终止事件订阅的可释放对象。</returns>
        IDisposable AddHandler<TEventArgs>(
            RoutedEvent<TEventArgs> routedEvent,
            EventHandler<TEventArgs> handler,
            RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
            bool handledEventsToo = false) where TEventArgs : RoutedEventArgs;

        /// <summary>
        /// 移除特定路由事件的处理程序。
        /// </summary>
        /// <param name="routedEvent">路由事件。</param>
        /// <param name="handler">处理程序。</param>
        void RemoveHandler(RoutedEvent routedEvent, Delegate handler);

        /// <summary>
        /// 移除特定路由事件的处理程序。
        /// </summary>
        /// <typeparam name="TEventArgs">事件参数类型。</typeparam>
        /// <param name="routedEvent">路由事件。</param>
        /// <param name="handler">处理程序。</param>
        void RemoveHandler<TEventArgs>(RoutedEvent<TEventArgs> routedEvent, EventHandler<TEventArgs> handler)
            where TEventArgs : RoutedEventArgs;

        /// <summary>
        /// 引发路由事件。
        /// </summary>
        /// <param name="e">事件参数。</param>
        void RaiseEvent(RoutedEventArgs e);
    }

    public class RoutedEventArgs : EventArgs
    {
        public RoutedEventArgs()
        {
        }

        public RoutedEventArgs(RoutedEvent routedEvent)
        {
            RoutedEvent = routedEvent;
        }

        public RoutedEventArgs(RoutedEvent routedEvent, IInteractive source)
        {
            RoutedEvent = routedEvent;
            Source = source;
        }

        public bool Handled { get; set; }

        public RoutedEvent RoutedEvent { get; set; }

        public RoutingStrategies Route { get; set; }

        public IInteractive Source { get; set; }
    }



    [Flags]
    public enum RoutingStrategies
    {
        Direct = 0x01,
        Tunnel = 0x02,
        Bubble = 0x04,
    }

    public class RoutedEvent
    {
        private Subject<Tuple<object, RoutedEventArgs>> _raised = new Subject<Tuple<object, RoutedEventArgs>>();
        private Subject<RoutedEventArgs> _routeFinished = new Subject<RoutedEventArgs>();

        public RoutedEvent(
            string name,
            RoutingStrategies routingStrategies,
            Type eventArgsType,
            Type ownerType)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(eventArgsType != null);
            Contract.Requires<ArgumentNullException>(ownerType != null);
            Contract.Requires<InvalidCastException>(typeof(RoutedEventArgs).GetTypeInfo().IsAssignableFrom(eventArgsType.GetTypeInfo()));

            EventArgsType = eventArgsType;
            Name = name;
            OwnerType = ownerType;
            RoutingStrategies = routingStrategies;
        }

        public Type EventArgsType
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Type OwnerType
        {
            get;
            private set;
        }

        public RoutingStrategies RoutingStrategies
        {
            get;
            private set;
        }

        public IObservable<Tuple<object, RoutedEventArgs>> Raised => _raised;
        public IObservable<RoutedEventArgs> RouteFinished => _routeFinished;

        public static RoutedEvent<TEventArgs> Register<TOwner, TEventArgs>(
            string name,
            RoutingStrategies routingStrategy)
                where TEventArgs : RoutedEventArgs
        {
            Contract.Requires<ArgumentNullException>(name != null);

            return new RoutedEvent<TEventArgs>(name, routingStrategy, typeof(TOwner));
        }

        public static RoutedEvent<TEventArgs> Register<TEventArgs>(
            string name,
            RoutingStrategies routingStrategy,
            Type ownerType)
                where TEventArgs : RoutedEventArgs
        {
            Contract.Requires<ArgumentNullException>(name != null);

            return new RoutedEvent<TEventArgs>(name, routingStrategy, ownerType);
        }

        public IDisposable AddClassHandler(
            Type targetType,
            EventHandler<RoutedEventArgs> handler,
            RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
            bool handledEventsToo = false)
        {
            return Raised.Subscribe(args =>
            {
                var sender = args.Item1;
                var e = args.Item2;

                if (targetType.GetTypeInfo().IsAssignableFrom(sender.GetType().GetTypeInfo()) &&
                    ((e.Route == RoutingStrategies.Direct) || (e.Route & routes) != 0) &&
                    (!e.Handled || handledEventsToo))
                {
                    try
                    {
                        handler.DynamicInvoke(sender, e);
                    }
                    catch (TargetInvocationException ex)
                    {
                        // Unwrap the inner exception.
                        // TODO ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                    }
                }
            });
        }

        internal void InvokeRaised(object sender, RoutedEventArgs e)
        {
            _raised.OnNext(Tuple.Create(sender, e));
        }

        internal void InvokeRouteFinished(RoutedEventArgs e)
        {
            _routeFinished.OnNext(e);
        }
    }

    public class RoutedEvent<TEventArgs> : RoutedEvent
        where TEventArgs : RoutedEventArgs
    {
        public RoutedEvent(string name, RoutingStrategies routingStrategies, Type ownerType)
            : base(name, routingStrategies, typeof(TEventArgs), ownerType)
        {
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(ownerType != null);
        }

        public IDisposable AddClassHandler<TTarget>(
            Func<TTarget, Action<TEventArgs>> handler,
            RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
            bool handledEventsToo = false)
                where TTarget : class, IInteractive
        {
            EventHandler<RoutedEventArgs> adapter = (sender, e) =>
            {
                var target = sender as TTarget;
                var args = e as TEventArgs;

                if (target != null && args != null)
                {
                    handler(target)(args);
                }
            };

            return AddClassHandler(typeof(TTarget), adapter, routes, handledEventsToo);
        }
    }
}
