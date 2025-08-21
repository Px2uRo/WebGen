
namespace Wedency
{

    internal class WedencyContract
    {
        /// <summary>
        /// 这个方法就是用来要求你特定的东西的，不遵循就直接丢异常，虽然if else 也能做，但是这个是用来简化代码的
        /// </summary>
        /// <typeparam name="TException">什么异常你要丢</typeparam>
        /// <param name="condition">条件</param>
        /// <param name="info">异常信息（不填也行）</param>
        [NETMethodRewrite]
        internal static void Requires<TException>(bool condition,string info = null) where TException : Exception, new()
        {

        }
        
    }

}