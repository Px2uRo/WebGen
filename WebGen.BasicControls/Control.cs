using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;
using WebGen.Collections;
using WebGen.Controls.Input;
using WebGen.Controls.Layouts;
using WebGen.Controls.LogicalTree;
using WebGen.Controls.Structs;
using Wedency;

namespace WebGen.Controls
{
    public class Layoutable : Animatable, ILayoutable
    {
        public double Width => throw new NotImplementedException();

        public double Height => throw new NotImplementedException();

        public Thickness Margin => throw new NotImplementedException();

        public HorizontalAlignment HorizontalAlignment => throw new NotImplementedException();

        public VerticalAlignment VerticalAlignment => throw new NotImplementedException();
    }
    public class Interactive : Layoutable, IInteractive
    {
        public IInteractive InteractiveParent => throw new NotImplementedException();

        public IDisposable AddHandler(RoutedEvent routedEvent, Delegate handler, RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble, bool handledEventsToo = false)
        {
            throw new NotImplementedException();
        }

        public IDisposable AddHandler<TEventArgs>(RoutedEvent<TEventArgs> routedEvent, EventHandler<TEventArgs> handler, RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble, bool handledEventsToo = false) where TEventArgs : RoutedEventArgs
        {
            throw new NotImplementedException();
        }

        public void RaiseEvent(RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void RemoveHandler(RoutedEvent routedEvent, Delegate handler)
        {
            throw new NotImplementedException();
        }

        public void RemoveHandler<TEventArgs>(RoutedEvent<TEventArgs> routedEvent, EventHandler<TEventArgs> handler) where TEventArgs : RoutedEventArgs
        {
            throw new NotImplementedException();
        }
    }
    public class InputElement : Interactive, IInputElement
    {

    }
    public partial class Control : InputElement
        , IControl,
        INamed,
        ISetInheritanceParent,
        ISetLogicalParent,
        ISupportInitialize
    {
        public object DataContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DataTemplates DataTemplates => throw new NotImplementedException();

        public bool IsAttachedToLogicalTree => throw new NotImplementedException();

        public ILogical LogicalParent => throw new NotImplementedException();

        public IWebGenReadOnlyList<ILogical> LogicalChildren => throw new NotImplementedException();

        public IObservable<IStyleable> StyleDetach => throw new NotImplementedException();

        public IWebGenReadOnlyList<string> Classes => throw new NotImplementedException();

        public Type StyleKey => throw new NotImplementedException();

        public ITemplatedControl TemplatedParent => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public IStyleHost StylingParent => throw new NotImplementedException();

        public event EventHandler Initialized;
        public event EventHandler<LogicalTreeAttachmentEventArgs> AttachedToLogicalTree;
        public event EventHandler<LogicalTreeAttachmentEventArgs> DetachedFromLogicalTree;

        public void BeginInit()
        {
            throw new NotImplementedException();
        }

        public void EndInit()
        {
            throw new NotImplementedException();
        }

        public void NotifyDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void SetParent(IWedencyObject parent)
        {
            throw new NotImplementedException();
        }

        public void SetParent(ILogical parent)
        {
            throw new NotImplementedException();
        }
    }
    public interface ISupportInitialize
    {
        void BeginInit();
        void EndInit();
    }

    /// <summary>
    /// 666 Html 用自己的id属性不带我，代码分析器 见。
    /// </summary>
    [WebGenBase]
    public interface INamed
    {
        /// <summary>
        /// 代码分析器见。
        /// </summary>
        [WendecyCodeGen]
        string Name { get; }
    }

    /// <summary>
    /// 使一个 <see cref="Control"/> 的逻辑父级可以设置。
    /// </summary>
    /// <remarks>
    /// 通常不用这个，除非你做的很高级。
    /// </remarks>
    public interface ISetLogicalParent
    {
        /// <summary>
        /// 设置逻辑父级
        /// </summary>
        /// <param name="parent">父级</param>
        void SetParent(ILogical parent);
    }

    [WebGenBase]
    public interface IControl: 
        ILogical, 
        ILayoutable, 
        IInputElement, 
        INamed,
        IStyleable,
        IStyleHost
    {
        /// <summary>
        ///初始化完成后触发。
        /// </summary>
        event EventHandler Initialized;
#if flase //TODO 未完成
        /// <summary>
        /// Gets or sets the control's styling classes.
        /// </summary>
        new Classes Classes { get; set; }
#endif
        /// <summary>
        /// 数据上下文。
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// 数据模板
        /// </summary>
        DataTemplates DataTemplates { get; }
    }
    #region DataTemplates
    /// <summary>
    /// Interface representing a template used to build a control for a piece of data.
    /// </summary>
    public interface IDataTemplate : ITemplate<object, IControl>
    {
        /// <summary>
        /// Gets a value indicating whether the data template supports recycling of the generated
        /// control.
        /// </summary>
        bool SupportsRecycling { get; }

        /// <summary>
        /// Checks to see if this data template matches the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// True if the data template can build a control for the data, otherwise false.
        /// </returns>
        bool Match(object data);
    }
    [WendecyCodeGen]
    public class DataTemplates : WebGenList<IDataTemplate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTemplates"/> class.
        /// </summary>
        public DataTemplates()
        {
            ResetBehavior = ResetBehavior.Remove;
        }
    }

    /// <summary>
    /// Creates a control based on a parameter.
    /// </summary>
    /// <typeparam name="TParam">The type of the parameter.</typeparam>
    /// <typeparam name="TControl">The type of control.</typeparam>
    public interface ITemplate<TParam, TControl> where TControl : IControl
    {
        /// <summary>
        /// Creates the control.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// The created control.
        /// </returns>
        TControl Build(TParam param);
    }

    #endregion

    /// <summary>
    /// 定义一个接口，用于设置 <see cref="Control"/> 的继承父级。
    /// </summary>
    /// <remarks>
    /// 通常不需要使用此接口 —— 它仅用于高级场景。
    /// 此外，<see cref="ISetLogicalParent"/> 也会设置继承父级；
    /// 仅当逻辑父级与继承父级不同的情况下才需要此接口。
    /// </remarks>
    public interface ISetInheritanceParent
    {
        /// <summary>
        /// 设置控件的继承父级。
        /// </summary>
        /// <param name="parent">父级。</param>
        void SetParent(IWedencyObject parent);
    }

}
