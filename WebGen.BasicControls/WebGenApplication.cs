using System;
using System.Collections.Generic;
using System.Text;
using Wedency;

namespace WebGen.Controls
{
    /// <summary>
    /// 自己继承这个类吧，记得一定要是一个 partical 里面的 Run() 方法会在源生成器处理之后出现。
    /// </summary>
    [WebGenBase]
    public class WebGenApplication//: IGlobalDataTemplates //TODO 不知道怎么用
    {
        public WebGenApplication Current { get; protected set; }
    }
}
