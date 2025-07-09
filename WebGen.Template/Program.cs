namespace WebGen.Template
{
    internal class Program
    {
        /// <summary>
        /// 不推荐更改这里的代码，除非你知道自己在做什么。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
#if DEBUG
            args = new string[] { "--urls", "http://localhost:5000" };
#endif
            var app = Activator.CreateInstance(typeof(App)) as App;
            app.Run();
        }
    }
}
