using System.Runtime.InteropServices;

namespace Wedency;

/// <summary>
/// 为各种属性更改事件提供数据。通常，这些事件报告只读依赖属性的有效值更改。
/// 另一个用途是作为 <see cref="System.Windows.PropertyChangedCallback" /> 实现的一部分。
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct DependencyPropertyChangedEventArgs
{
    /// <summary>
    /// 获取属性更改后的值。
    /// </summary>
    /// <returns>更改后的属性值。</returns>
    public object NewValue
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 获取属性更改前的值。
    /// </summary>
    /// <returns>更改前的属性值。</returns>
    public object OldValue
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 获取发生值更改的依赖属性标识符。
    /// </summary>
    /// <returns>发生值更改的依赖属性的标识符字段。</returns>
    public DependencyProperty Property
    {
        get
        {
            throw null;
        }
    }

    /// <summary>
    /// 初始化 <see cref="DependencyPropertyChangedEventArgs" /> 结构的新实例。
    /// </summary>
    /// <param name="property">发生更改的依赖属性标识符。</param>
    /// <param name="oldValue">事件或状态更改报告的更改前的属性值。</param>
    /// <param name="newValue">事件或状态更改报告的更改后的属性值。</param>
    public DependencyPropertyChangedEventArgs(DependencyProperty property, object oldValue, object newValue)
    {
        throw null;
    }

    /// <summary>
    /// 判断提供的对象是否等于当前 <see cref="DependencyPropertyChangedEventArgs" /> 实例。
    /// </summary>
    /// <param name="obj">要与当前实例进行比较的对象。</param>
    /// <returns>
    /// 如果指定对象等于当前实例，则返回 <see langword="true" />；否则返回 <see langword="false" />。
    /// </returns>
    public override bool Equals(object obj)
    {
        throw null;
    }

    /// <summary>
    /// 判断提供的 <see cref="DependencyPropertyChangedEventArgs" /> 是否等于当前实例。
    /// </summary>
    /// <param name="args">要比较的 <see cref="DependencyPropertyChangedEventArgs" /> 实例。</param>
    /// <returns>
    /// 如果两个实例相等，则返回 <see langword="true" />；否则返回 <see langword="false" />。
    /// </returns>
    public bool Equals(DependencyPropertyChangedEventArgs args)
    {
        throw null;
    }

    /// <summary>
    /// 获取此 <see cref="DependencyPropertyChangedEventArgs" /> 的哈希码。
    /// </summary>
    /// <returns>32 位有符号整数哈希码。</returns>
    public override int GetHashCode()
    {
        throw null;
    }

    /// <summary>
    /// 判断两个 <see cref="DependencyPropertyChangedEventArgs" /> 实例是否相等。
    /// </summary>
    /// <param name="left">第一个要比较的实例。</param>
    /// <param name="right">第二个要比较的实例。</param>
    /// <returns>
    /// 如果两个实例相等，则返回 <see langword="true" />；否则返回 <see langword="false" />。
    /// </returns>
    public static bool operator ==(DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
    {
        throw null;
    }

    /// <summary>
    /// 判断两个 <see cref="DependencyPropertyChangedEventArgs" /> 实例是否不相等。
    /// </summary>
    /// <param name="left">第一个要比较的实例。</param>
    /// <param name="right">第二个要比较的实例。</param>
    /// <returns>
    /// 如果两个实例不相等，则返回 <see langword="true" />；否则返回 <see langword="false" />。
    /// </returns>
    public static bool operator !=(DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
    {
        throw null;
    }
}
