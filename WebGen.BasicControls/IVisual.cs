using System;
using System.Collections.Generic;
using System.Text;
using WebGen.Controls.LogicalTree;
using Wedency;

namespace WebGen.Controls
{
    /// <summary>
    /// 表示具有屏幕可视化表示的控件。里面只是有一些事件，用来适应 DOM 的。
    /// </summary>
    /// <remarks>
    /// <see cref="IVisual"/> 接口定义了渲染器渲染控件所需的接口。
    /// 通常不需要直接引用此接口，除非你正在编写渲染器；显然这句话对 WebGen 不适用。
    /// 用 <see cref="ILogical"/>
    /// 相反，可以使用 <see cref="VisualExtensions"/> 中定义的扩展方法来遍历可视化树。
    /// 此接口由 <see cref="Visual"/> 实现，不应在其他地方实现。
    /// </remarks>
    [WebGenBase]
    internal interface IVisual
    {
#if false //没用的家伙
        /// <summary>
        /// 当控件附加到已根植的可视化树时触发。
        /// </summary>
        [WebGenEvent]
        event EventHandler<VisualTreeAttachmentEventArgs> AttachedToVisualTree;

        /// <summary>
        /// 当控件从已根植的可视化树分离时触发。
        /// </summary>
        [WebGenEvent]
        event EventHandler<VisualTreeAttachmentEventArgs> DetachedFromVisualTree;
#endif
    }
}
