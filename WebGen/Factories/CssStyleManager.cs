using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGen.Core
{
    internal class CssStyleManager
    {
        /// <summary>
        /// 暂时未形成有个性化的样式生成逻辑，所以是直接返回一段默认的样式。
        /// </summary>
        /// <returns></returns>
        public string GenerateStyles()
            {
                return @"
                div { font-family: Arial; }
                button { border: 1px solid black; }
                input { border: 1px solid gray; }
            ";
        }
    }
}
