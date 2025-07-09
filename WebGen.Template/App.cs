using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGen.Core;

namespace WebGen.Template
{
    internal class App : Application
    {
        public App(string[] args) : base(args)
        {
            // 在这里可以添加自定义的初始化代码
            // 例如，设置默认的地址和端口，或者处理特定的命令行参数
            HandleArgs();
        }

        internal void Run()
        {

        }
    }
}
