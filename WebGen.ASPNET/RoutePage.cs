using System;
using System.Collections.Generic;
using System.Text;
using WebGen.Attributes;
using WebGen.Controls;
using WebGen.Converters.CSharp;
using Wedency;

namespace WebGen.Controls
{
    /// <summary>
    /// 一个有 Route 的页面，仅在 WebGen.ASPNET 有这个 Control。但是命名空间在 WebGen.Controls 下面，运行时可访问 URL / {<see cref="Prefix"/>} 来查看该页面。
    /// </summary>
    public class RoutePage : Control, IRoutable
    {
        //TODO WedencyProperty<string> WedencyProperty = WedencyPropertyJS
        public virtual string Pattern { get; set; }
    }

    /// <summary>
    /// 这个就是 Minimal API 的 app.MapGroup 方法用的。
    /// </summary>
    [WebGenBase]
    internal interface IRoutable
    {
        /// <summary>
        /// Route 名字
        /// </summary>
        string Pattern { get; }
    }
}
