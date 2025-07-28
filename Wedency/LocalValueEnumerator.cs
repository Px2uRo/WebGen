// Wedency.LocalValueEnumerator
using System.Collections;
using System.Runtime.InteropServices;
using Wedency;

namespace Wedency;

/// <summary>
/// 为 <see cref="Wedency.DependencyObject" /> 上存在的任何依赖属性的本地值提供枚举支持。
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct LocalValueEnumerator : IEnumerator
{
    /// <summary>
    /// 获取集合中表示的项的数量。
    /// </summary>
    /// <returns>集合中的项数。</returns>
    public int Count
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 获取集合中的当前元素。
    /// </summary>
    /// <returns>集合中当前的 <see cref="Wedency.LocalValueEntry" />。</returns>
    public LocalValueEntry Current
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 有关此成员的说明，请参见 <see cref="IEnumerator.Current" />。
    /// </summary>
    /// <returns>集合中的当前元素。</returns>
    object IEnumerator.Current
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 判断提供的 <see cref="LocalValueEnumerator" /> 是否与当前的 <see cref="LocalValueEnumerator" /> 相等。
    /// </summary>
    /// <param name="obj">要与当前枚举器比较的对象。</param>
    /// <returns>
    /// 若指定对象等于当前对象，则为 <see langword="true" />，否则为 <see langword="false" />。
    /// </returns>
    public override bool Equals(object obj)
    {
        throw null;
    }

    /// <summary>
    /// 返回当前 <see cref="LocalValueEnumerator" /> 的哈希码。
    /// </summary>
    /// <returns>一个 32 位整数的哈希码。</returns>
    public override int GetHashCode()
    {
        throw null;
    }

    /// <summary>
    /// 将枚举器推进到集合中的下一个元素。
    /// </summary>
    /// <returns>
    /// 若成功移动到下一个元素，则为 <see langword="true" />；若已越过集合末尾，则为 <see langword="false" />。
    /// </returns>
    public bool MoveNext()
    {
        throw null;
    }

    /// <summary>
    /// 比较两个 <see cref="LocalValueEnumerator" /> 是否相等。
    /// </summary>
    /// <param name="obj1">第一个要比较的对象。</param>
    /// <param name="obj2">第二个要比较的对象。</param>
    /// <returns>
    /// 若相等则为 <see langword="true" />，否则为 <see langword="false" />。
    /// </returns>
    public static bool operator ==(LocalValueEnumerator obj1, LocalValueEnumerator obj2)
    {
        throw null;
    }

    /// <summary>
    /// 比较两个 <see cref="LocalValueEnumerator" /> 是否不相等。
    /// </summary>
    /// <param name="obj1">第一个要比较的对象。</param>
    /// <param name="obj2">第二个要比较的对象。</param>
    /// <returns>
    /// 若不相等则为 <see langword="true" />，否则为 <see langword="false" />。
    /// </returns>
    public static bool operator !=(LocalValueEnumerator obj1, LocalValueEnumerator obj2)
    {
        throw null;
    }

    /// <summary>
    /// 将枚举器重置为其初始位置，即在集合中第一个元素之前。
    /// </summary>
    public void Reset()
    {
    }
}
