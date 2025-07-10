namespace WebGen.JS.Rules
{
    /// <summary>
    /// 用于将典型 JavaScript 原生函数，转换为其对应的 JS 代码字符串。
    /// 因为 C# 没有头文件声明机制，需手动统一收录在此类中。
    /// </summary>
    internal class JSGlobalFunctionsConvertRule : IAutoRegstry
    {
        public string Alert(string msg) =>
            $"alert({EscapeLiteral(msg)})";

        public string Confirm(string msg) =>
            $"confirm({EscapeLiteral(msg)})";

        public string Prompt(string msg, string defaultValue = "") =>
            $"prompt({EscapeLiteral(msg)}, {EscapeLiteral(defaultValue)})";

        public string ParseInt(string str, int radix = 10) =>
            $"parseInt({EscapeLiteral(str)}, {radix})";

        public string ParseFloat(string str) =>
            $"parseFloat({EscapeLiteral(str)})";

        public string IsNaN(string expr) =>
            $"isNaN({expr})";

        public string IsFinite(string expr) =>
            $"isFinite({expr})";

        public string Eval(string jsExpr) =>
            $"eval({EscapeLiteral(jsExpr)})";

        [Obsolete("escape 已被弃用，请考虑 encodeURIComponent 替代")]
        public string Escape(string s) =>
            $"escape({EscapeLiteral(s)})";

        [Obsolete("unescape 已被弃用，请考虑 decodeURIComponent 替代")]
        public string Unescape(string s) =>
            $"unescape({EscapeLiteral(s)})";

        /// <summary>
        /// 用于转义字符串常量，包裹在双引号中以生成合法 JS 字面量
        /// </summary>
        private string EscapeLiteral(string s)
        {
            return $"\"{s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r")}\"";
        }
    }

}