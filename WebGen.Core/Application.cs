using System;
using System.Linq;
using System.Net.Sockets;
using WebGen.Core.Arguments;

namespace WebGen.Core
{
    /// <summary>
    /// 这个类代表一个 WebGen 应用程序的入口点，处理命令行参数并提供应用程序的基本信息。
    /// </summary>
    public class Application
    {
        #region Fields
        private string[] _args;
        private string _address;
        private int _port;
        private Socket socket;
        #endregion

        #region Props
        public string[] Args { get=>_args; }
        public static Application Current { get; private set; }
        public string Address => _address;
        public int Port => _port;

        #endregion

        #region Methods
        /// <summary>
        /// Args 处理方法，解析命令行参数并设置应用程序的地址和端口。
        /// Args 用法示例：--urls http://localhost:5000
        /// </summary>
        public virtual void HandleArgs()
        {
            var li = _args.ToList();
            var url = li[li.IndexOf("--urls") + 1];
            url = url.Replace("https://", "");
            url = url.Replace("http://", "");
            var ar = url.Split(':');
            if (ar.Length > 1)
            {
                _address = ar[0]; _port = System.Convert.ToInt32(ar[1]);
            }
            else if(ar.Length > 0)
            {
#if DEBUG                
                _address = ar[0];
                _port = new Random().Next(49152, 65535);

#else
                var ishttps = string.Join(" ",_args).Contains("https");
                _address = ar[0];
                if (ishttps)
                {
                    _port = 443;
                }
                else
                {
                    _port = 80;
                }

#endif
            }
#if DEBUG
            else
            {
                _address = "127.0.0.1";
                _port = new Random().Next(49152, 65535);
            }
#endif
        }
        public Application(string[] args)
        {
            _args = args;
        }
        #endregion

        #region Events
        public event EventHandler<StartUpArgs> OnStartUp;
        #endregion
    }
}
