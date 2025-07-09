using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGen.JS
{
    /// <summary>
    /// 目前只有一些简单的 JS 方法，主要是为了方便在 WebGen 中使用 JavaScript 进行交互，调试的内容以后再学习吧。
    /// </summary>
    public static class JSGlobalFunctions
    {
        #region ChatGPT 仿照JS写的方法
        /// <summary>
        /// 由 ChatGPT 生成的 _parseFloat 方法，主要是为了将字符串转换为浮点数。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static double? _parseFloat(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            int index = 0;
            int length = s.Length;

            // 跳过前导空白
            while (index < length && char.IsWhiteSpace(s[index]))
                index++;

            if (index == length)
                return null;

            int start = index;

            // 处理符号
            if (s[index] == '+' || s[index] == '-')
                index++;

            bool hasDigits = false;

            // 读取整数部分数字
            while (index < length && char.IsDigit(s[index]))
            {
                index++;
                hasDigits = true;
            }

            // 读取小数点和小数部分
            if (index < length && s[index] == '.')
            {
                index++;

                while (index < length && char.IsDigit(s[index]))
                {
                    index++;
                    hasDigits = true;
                }
            }

            // 读取科学计数法部分
            if (hasDigits && index < length && (s[index] == 'e' || s[index] == 'E'))
            {
                int eIndex = index;
                index++;

                // 指数符号
                if (index < length && (s[index] == '+' || s[index] == '-'))
                    index++;

                bool hasExpDigits = false;
                while (index < length && char.IsDigit(s[index]))
                {
                    index++;
                    hasExpDigits = true;
                }

                if (!hasExpDigits)
                {
                    // 指数部分无数字，回退，不算科学计数
                    index = eIndex;
                }
            }

            if (!hasDigits)
                return null;

            string numStr = s.Substring(start, index - start);

            if (double.TryParse(numStr, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double value))
                return value;

            return null;
        }

        /// <summary>
        /// 由 ChatGPT 生成的 _parseFloat 方法，主要是为了将字符串转换为整数。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? _parseInt(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            int index = 0;
            int length = s.Length;

            while (index < length && char.IsWhiteSpace(s[index]))
                index++;

            if (index == length)
                return null;

            bool negative = false;
            if (s[index] == '+' || s[index] == '-')
            {
                negative = s[index] == '-';
                index++;
            }

            if (index == length || !char.IsDigit(s[index]))
                return null;

            long result = 0;

            while (index < length && char.IsDigit(s[index]))
            {
                result = result * 10 + (s[index] - '0');

                if (!negative && result > int.MaxValue)
                    return null;  // 超出范围返回 null

                if (negative && -result < int.MinValue)
                    return null;  // 超出范围返回 null

                index++;
            }

            return negative ? (int)(-result) : (int)result;
        }
        /// <summary>
        /// 由 ChatGPT 生成的 _isNaN 方法，主要是为了将字符串转换为整数。由 ChatGPT 生成的 _parseFloat 方法，主要是为了将字符串转换为整数。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool _isNaN(object value)
        {
            if (value == null)
                return false;

            if (value is double d)
                return double.IsNaN(d);

            try
            {
                double parsed = Convert.ToDouble(value);
                return double.IsNaN(parsed);
            }
            catch
            {
                return true; // 转换失败（如 "abc"） → 视为 NaN
            }
        }
        public static bool _isFinite(object value)
        {
            if (value == null) return false;

            try
            {
                double d = Convert.ToDouble(value);
                return !double.IsNaN(d) && !double.IsInfinity(d);
            }
            catch
            {
                return false;
            }
        }
        public static object _eval(string expr)
        {
            throw new NotImplementedException();
        }
        public static string _escape(string s)
        {
            if (s == null) return null;

            var sb = new System.Text.StringBuilder();
            foreach (char c in s)
            {
                if ((c >= 'A' && c <= 'Z') ||
                    (c >= 'a' && c <= 'z') ||
                    (c >= '0' && c <= '9') ||
                    "-_.!~*'()".IndexOf(c) != -1)
                {
                    sb.Append(c);
                }
                else if (c < 256)
                {
                    sb.Append('%');
                    sb.Append(((int)c).ToString("X2"));  // 把 char 转为 Unicode 编码值（int）再格式化成两位十六进制
                }
                else
                {
                    sb.Append("%u" + ((int)c).ToString("X4"));
                }
            }
            return sb.ToString();
        }
        public static string _unescape(string s)
        {
            if (s == null) return null;

            return System.Text.RegularExpressions.Regex.Replace(s, @"%u([0-9A-Fa-f]{4})|%([0-9A-Fa-f]{2})", m =>
            {
                if (m.Groups[1].Success) // %uXXXX
                {
                    int code = Convert.ToInt32(m.Groups[1].Value, 16);
                    return ((char)code).ToString();
                }
                else // %XX
                {
                    int code = Convert.ToInt32(m.Groups[2].Value, 16);
                    return ((char)code).ToString();
                }
            });
        }


        #endregion
        /// <summary>
        /// 弹窗提示（模拟 JavaScript 的 alert 函数）
        /// </summary>
        /// <param name="msg">提示消息</param>
        public static void Alert(string msg)
        {
            Console.WriteLine($"alert(\"{msg}\")");
            Console.Read();
        }

        /// <summary>
        /// 确认对话框（模拟 JavaScript 的 confirm 函数）
        /// </summary>
        /// <param name="msg">确认提示消息</param>
        /// <returns>用户是否确认，true 表示“是”，false 表示“否”</returns>
        public static bool Confirm(string msg)
        {
            while (true)
            {
                Console.WriteLine($"{msg} (Y/n)");
                if (Console.ReadKey().Equals(ConsoleKey.Enter) || Console.ReadKey().Equals(ConsoleKey.Y))
                {
                    return true;
                }
                else if (Console.ReadKey().Equals(ConsoleKey.N))
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 输入对话框（模拟 JavaScript 的 prompt 函数）
        /// </summary>
        /// <param name="msg">输入提示消息</param>
        /// <returns>用户输入的字符串</returns>
        public static string Prompt(string msg) => Console.ReadLine();

        /// <summary>
        /// 将字符串转换为整数（模拟 JavaScript 的 parseInt 函数）
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>解析得到的整数，失败时返回 null</returns>
        public static int? ParseInt(string str) => _parseInt(str);

        /// <summary>
        /// 将字符串转换为浮点数（模拟 JavaScript 的 parseFloat 函数）
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>解析得到的浮点数，失败时返回 null</returns>
        public static double? ParseFloat(string str) => _parseFloat(str);

        /// <summary>
        /// 判断输入是否为 NaN（模拟 JavaScript 的 isNaN 函数）
        /// </summary>
        /// <param name="obj">输入对象</param>
        /// <returns>true 表示是 NaN，false 表示不是</returns>
        public static bool IsNaN(object obj) => _isNaN(obj);

        /// <summary>
        /// 判断数值是否为有限数（模拟 JavaScript 的 isFinite 函数）
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <returns>true 表示是有限数，false 表示是 Infinity/-Infinity/NaN</returns>
        public static bool IsFinite(object value) => _isFinite(value);

        /// <summary>
        /// 执行一段 JavaScript 表达式（模拟 JavaScript 的 eval 函数）
        /// </summary>
        /// <param name="expr">JavaScript 表达式</param>
        /// <returns>表达式计算结果</returns>
        public static object Eval(string expr) => _eval(expr);

        /// <summary>
        /// 编码字符串为转义格式（模拟 JavaScript 的 escape 函数）
        /// </summary>
        /// <param name="s">原始字符串</param>
        /// <returns>已编码的字符串</returns>
        [Obsolete("escape 在现代 JavaScript 中已弃用，请使用 encodeURIComponent 替代")]
        public static string Escape(string s) => _escape(s);

        /// <summary>
        /// 解码转义字符串（模拟 JavaScript 的 unescape 函数）
        /// </summary>
        /// <param name="s">已转义的字符串</param>
        /// <returns>解码后的原始字符串</returns>
        [Obsolete("unescape 在现代 JavaScript 中已弃用，请使用 decodeURIComponent 替代")]
        public static string Unescape(string s) => _unescape(s);

    }

    /// <summary>
    /// 用于将典型 JavaScript 原生函数，转换为其对应的 JS 代码字符串。
    /// 因为 C# 没有头文件声明机制，需手动统一收录在此类中。
    /// </summary>
    internal static class JSGlobalFunctionsConvertRule
    {
        public static string Alert(string msg) =>
            $"alert({EscapeLiteral(msg)});";

        public static string Confirm(string msg) =>
            $"confirm({EscapeLiteral(msg)});";

        public static string Prompt(string msg, string defaultValue = "") =>
            $"prompt({EscapeLiteral(msg)}, {EscapeLiteral(defaultValue)});";

        public static string ParseInt(string str, int radix = 10) =>
            $"parseInt({EscapeLiteral(str)}, {radix});";

        public static string ParseFloat(string str) =>
            $"parseFloat({EscapeLiteral(str)});";

        public static string IsNaN(string expr) =>
            $"isNaN({expr});";

        public static string IsFinite(string expr) =>
            $"isFinite({expr});";

        public static string Eval(string jsExpr) =>
            $"eval({EscapeLiteral(jsExpr)});";

        [Obsolete("escape 已被弃用，请考虑 encodeURIComponent 替代")]
        public static string Escape(string s) =>
            $"escape({EscapeLiteral(s)});";

        [Obsolete("unescape 已被弃用，请考虑 decodeURIComponent 替代")]
        public static string Unescape(string s) =>
            $"unescape({EscapeLiteral(s)});";

        /// <summary>
        /// 用于转义字符串常量，包裹在双引号中以生成合法 JS 字面量
        /// </summary>
        private static string EscapeLiteral(string s)
        {
            return $"\"{s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r")}\"";
        }
    }

}
