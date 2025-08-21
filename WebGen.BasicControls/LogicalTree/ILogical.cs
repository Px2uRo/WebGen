using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using WebGen.Collections;
using Wedency;

namespace WebGen.Controls.LogicalTree
{
    /// <summary>
    /// 保存 <see cref="ILogical.AttachedToLogicalTree"/> 和 
    /// <see cref="ILogical.DetachedFromLogicalTree"/> 事件的参数。
    /// </summary>
    public class LogicalTreeAttachmentEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化 <see cref="LogicalTreeAttachmentEventArgs"/> 类的新实例。
        /// </summary>
        /// <param name="root">逻辑树的根。</param>
        public LogicalTreeAttachmentEventArgs(IStyleHost root)
        {
            Contract.Requires<ArgumentNullException>(root != null);

            Root = root;
        }

        /// <summary>
        /// 获取控件所附加到或分离自的逻辑树根。
        /// </summary>
        public IStyleHost Root { get; }
    }
    /// <summary>
    /// 定义具有 <see cref="Styles"/> 集合的元素。
    /// </summary>
    public interface IStyleHost
    {
#if false //Style 有关。
        /// <summary>
        /// 获取该元素的样式集合。
        /// </summary>
        Styles Styles { get; }
#endif
        /// <summary>
        /// 获取父级样式宿主元素。
        /// </summary>
        IStyleHost StylingParent { get; }
    }
    /// <summary>
    /// 可应用样式元素的接口。
    /// </summary>
    public interface IStyleable : IWedencyObject, INamed
    {
        /// <summary>
        /// 当控件的样式应被移除时发出信号。
        /// </summary>
        IObservable<IStyleable> StyleDetach { get; }

        /// <summary>
        /// 获取控件的类列表。
        /// </summary>
        IWebGenReadOnlyList<string> Classes { get; }

        /// <summary>
        /// 获取用于样式化控件的类型。
        /// </summary>
        Type StyleKey { get; }

        /// <summary>
        /// 如果控件来自模板，则获取该元素的模板父级。
        /// </summary>
        ITemplatedControl TemplatedParent { get; }
    }
    /// <summary>
    /// 我也不知道为什么 Avalonia 0.5.0 这个接口是空的。
    /// </summary>
    [WebGenBase]
    public interface ITemplatedControl : IWedencyObject
    {
    }

    /// <summary>
    /// 呈现逻辑树的一个节点。转换器是<see cref=""/>
    /// </summary>
    [WebGenBase]
    public interface ILogical
    {
        /// <summary>
        /// 当控件附加到已根植的逻辑树时触发。
        /// </summary>
        [WebGenEvent]
        event EventHandler<LogicalTreeAttachmentEventArgs> AttachedToLogicalTree;

        /// <summary>
        /// 当控件从已根植的逻辑树分离时触发。
        /// </summary>
        [WebGenEvent]
        event EventHandler<LogicalTreeAttachmentEventArgs> DetachedFromLogicalTree;

        /// <summary>
        /// 获取一个值，指示元素是否已附加到已根植的逻辑树。
        /// </summary>
        bool IsAttachedToLogicalTree { get; }

        /// <summary>
        /// 获取逻辑父级。
        /// </summary>
        ILogical LogicalParent { get; }

        /// <summary>
        /// 获取逻辑子级。
        /// </summary>
        IWebGenReadOnlyList<ILogical> LogicalChildren { get; }

        /// <summary>
        /// 通知控件它正在从已根植的逻辑树中分离。
        /// </summary>
        /// <param name="e">事件参数。</param>
        /// <remarks>
        /// 此方法将由框架自动调用，通常不需要手动调用此方法。
        /// </remarks>
        void NotifyDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e);
    }
}
