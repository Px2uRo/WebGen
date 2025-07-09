using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGen.Proj.Pages
{
    internal partial class Index
    {
        public void ShowMessage()
        {
            WebGen.JS.JSGlobalFunctions.Alert("Hello, WebGen!");

            var res = WebGen.JS.JSGlobalFunctions.Confirm("支不支持我们？");

            WebGen.JS.JSGlobalFunctions.Alert(res.ToString());

            if (res)
            {
                WebGen.JS.JSGlobalFunctions.Alert("用户说：是的。");
            }
            else
            {
                WebGen.JS.JSGlobalFunctions.Alert("用户说：不是的。");
            }
        }
    }
}
