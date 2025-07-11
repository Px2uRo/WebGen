using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace WebGen.Utils
{
    public static class CallerUtil
    {
        public static Assembly? GetImmediateCallerAssembly()
        {
            var stackTrace = new StackTrace(skipFrames: 1); // 跳过当前这个方法本身

            foreach (var frame in stackTrace.GetFrames())
            {
                var method = frame.GetMethod();
                if (method == null) continue;

                // 排除系统、反射等无意义调用
                var asm = method.DeclaringType?.Assembly;
                if (asm == null) continue;

                // 你可以添加条件过滤掉某些框架/库
                return asm;
            }

            return null;
        }
        public static Assembly GetCallingWebAPIAssembly()=> Assembly.GetEntryAssembly();
    }
}
