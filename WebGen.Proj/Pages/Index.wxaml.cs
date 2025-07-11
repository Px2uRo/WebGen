using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebGen.WorlWideWeb.JS.JSGlobalFunctions;

namespace WebGen.Proj.Pages
{
    internal partial class Index
    {
        public void ShowMessage()
        {
            Alert("Hello, WebGen!");

            var res = Confirm("支不支持我们？");

            WebGen.WorlWideWeb.JS.JSGlobalFunctions.Alert(res.ToString());

            if (res)
            {
                WebGen.WorlWideWeb.JS.JSGlobalFunctions.Alert("用户说：是的。");
            }
            else
            {
                WebGen.WorlWideWeb.JS.JSGlobalFunctions.Alert("用户说：不是的。");
            }
        }
    }
}
