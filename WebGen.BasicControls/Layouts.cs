using System;
using System.Collections.Generic;
using System.Text;
using WebGen.Controls.Structs;
using Wedency;

namespace WebGen.Controls.Layouts
{
    /// <summary>
    /// 定义控件在其父控件中水平对齐的方式。
    /// </summary>
    public enum HorizontalAlignment
    {
        /// <summary>
        /// 控件拉伸以填充父控件的宽度。
        /// </summary>
        Stretch,

        /// <summary>
        /// 控件对齐到父控件的左侧。
        /// </summary>
        Left,

        /// <summary>
        /// 控件在父控件中居中。
        /// </summary>
        Center,

        /// <summary>
        /// 控件对齐到父控件的右侧。
        /// </summary>
        Right,
    }
    /// <summary>
    /// 定义控件在其父控件中垂直对齐的方式。
    /// </summary>
    public enum VerticalAlignment
    {
        /// <summary>
        /// 控件拉伸以填充父控件的高度。
        /// </summary>
        Stretch,

        /// <summary>
        /// 控件对齐到父控件的顶部。
        /// </summary>
        Top,

        /// <summary>
        /// 控件在父控件中居中。
        /// </summary>
        Center,

        /// <summary>
        /// 控件对齐到父控件的底部。
        /// </summary>
        Bottom,
    }
    /// <summary>
    /// 继承该类以适配布局。里面的属性大多是在 CSS 内的属性。所以我带了 PropToCSS 特性。
    /// </summary>
    [WebGenBase]
    public interface ILayoutable
    {
#if false //未想好要怎么转换的 Avalonia / WPF 控件属性主要是Measure和Desire的东西。。。不然我做的是 WebGL？
        /// <summary>
        /// Gets the size that this element computed during the measure pass of the layout process.
        /// </summary>
        Size DesiredSize { get; }

        /// <summary>
        /// Gets the minimum width of the element.
        /// </summary>
        double MinWidth { get; }

        
        /// <summary>
        /// Gets the maximum width of the element.
        /// </summary>
        double MaxWidth { get; }        
        /// <summary>
        /// Gets the minimum height of the element.
        /// </summary>
        double MinHeight { get; }

        /// <summary>
        /// Gets the maximum height of the element.
        /// </summary>
        double MaxHeight { get; }

                /// <summary>
        /// Gets a value indicating whether the control's layout measure is valid.
        /// </summary>
        bool IsMeasureValid { get; }

        /// <summary>
        /// Gets a value indicating whether the control's layouts arrange is valid.
        /// </summary>
        bool IsArrangeValid { get; }

        /// <summary>
        /// Gets the available size passed in the previous layout pass, if any.
        /// </summary>
        Size? PreviousMeasure { get; }

        /// <summary>
        /// Gets the layout rect passed in the previous layout pass, if any.
        /// </summary>
        Rect? PreviousArrange { get; }
#endif
        /// <summary>
        /// 元素的宽度。
        /// </summary>
        [CSSPropDef]
        double Width { get; }

        /// <summary>
        /// 元素的高度。
        /// </summary>
        [CSSPropDef]
        double Height { get; }

        /// <summary>
        /// 元素的边距。
        /// </summary>
        [MutilCssPropDef(new string[0])]
        Thickness Margin { get; }

        /// <summary>
        /// 获取元素在其父控件中首选的水平对齐方式。在CSS就是为 0% 的 left right 属性，Center 也许可能会有Bug。
        /// </summary>
        [MutilCssPropDef(new string[0])]
        HorizontalAlignment HorizontalAlignment { get; }
        /// <summary>
        /// 获取元素在其父控件中首选的垂直对齐方式。在CSS就是 0% 的 top button 属性。
        /// </summary>
        [MutilCssPropDef(new string[0])]
        VerticalAlignment VerticalAlignment { get; }    
        
    
    }
}
