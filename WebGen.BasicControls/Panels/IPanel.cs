using System;
using System.Collections.Generic;
using System.Text;
using Wedency;

namespace WebGen.Controls.Panels
{
    [WebGenBase]
    public interface IPanel
    {
        /// <summary>
        /// 得到或者设定 <see cref="Panel"/> 的子集。
        /// TODO 看需要不需要专门继承。
        /// </summary>
        [RelatedToHtml]
        IList<IControl> Children { get; set; }
    }
}
