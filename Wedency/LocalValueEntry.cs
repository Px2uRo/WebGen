using System.Runtime.InteropServices;

namespace Wedency;

/// <summary>
/// 表示一个本地设置的依赖属性的属性标识符以及该属性的值。
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct LocalValueEntry
{
    /// <summary>
    /// 获取由此项表示的本地设置的依赖属性的标识符。
    /// </summary>
    /// <returns>本地设置的依赖属性标识符。</returns>
    public DependencyProperty Property
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 获取本地设置的依赖属性的值。
    /// </summary>
    /// <returns>本地设置的依赖属性的值，类型为 <see cref="object" />。</returns>
    public object Value
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 判断当前实例是否等于指定的 <see cref="LocalValueEntry" />。
    /// </summary>
    /// <param name="obj">要与当前实例比较的 <see cref="LocalValueEntry" />。</param>
    /// <returns>
    /// 若两个实例的 <see cref="Property" /> 和 <see cref="Value" /> 相同，则返回 <see langword="true" />；
    /// 否则返回 <see langword="false" />。
    /// <para>
    /// 对于 <see cref="Property" />，始终是按位值类型比较；
    /// 对于 <see cref="Value" />，如果是值类型也按位比较；
    /// 若为引用类型，则使用该类型自身的相等判断机制（即 `==` 操作符），通常表现为引用相等。
    /// </para>
    /// </returns>
    public override bool Equals(object obj)
    {
        throw null;
    }

    /// <summary>
    /// 返回此 <see cref="LocalValueEntry" /> 的哈希代码。
    /// </summary>
    /// <returns>一个带符号的 32 位整数哈希值。</returns>
    public override int GetHashCode()
    {
        throw null;
    }

    /// <summary>
    /// 比较两个 <see cref="LocalValueEntry" /> 实例是否相等。
    /// </summary>
    /// <param name="obj1">第一个待比较实例。</param>
    /// <param name="obj2">第二个待比较实例。</param>
    /// <returns>
    /// 若两个实例的 <see cref="Property" /> 和 <see cref="Value" /> 成员相等，则返回 <see langword="true" />；
    /// 否则返回 <see langword="false" />。
    /// </returns>
    public static bool operator ==(LocalValueEntry obj1, LocalValueEntry obj2)
    {
        throw null;
    }

    /// <summary>
    /// 比较两个 <see cref="LocalValueEntry" /> 实例是否不相等。
    /// </summary>
    /// <param name="obj1">第一个待比较实例。</param>
    /// <param name="obj2">第二个待比较实例。</param>
    /// <returns>
    /// 若两个实例的 <see cref="Property" /> 和 <see cref="Value" /> 成员不相等，则返回 <see langword="true" />；
    /// 否则返回 <see langword="false" />。
    /// </returns>
    public static bool operator !=(LocalValueEntry obj1, LocalValueEntry obj2)
    {
        throw null;
    }
}
