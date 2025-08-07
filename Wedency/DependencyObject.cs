using WebGen.Attributes;

namespace Wedency;

/// <summary>表示参与依赖属性系统的对象。</summary>
[CodeGenClass]
public class DependencyObject //不是 : DispatcherObject 就别来了
{
    /// <summary>获取包装此实例 CLR 类型的 DependencyObjectType。</summary>
    /// <returns>包装此实例 CLR 类型的 DependencyObjectType 对象。</returns>
    public DependencyObjectType DependencyObjectType
    {
        get
        {
            throw null;
        }
    }

    /// <summary>获取一个值，指示此实例当前是否被封存（只读）。</summary>
    /// <returns>如果实例被封存，返回 true；否则返回 false。</returns>
    public bool IsSealed
    {
        get
        {
            throw null;
        }
    }

    /// <summary>初始化 DependencyObject 类的新实例。</summary>
    public DependencyObject()
    {
    }

    /// <summary>清除指定依赖属性的本地值。</summary>
    /// <param name="dp">要清除的依赖属性，由 DependencyProperty 标识。</param>
    /// <exception cref="System.InvalidOperationException">尝试在已封存的 DependencyObject 上调用 ClearValue 会抛出异常。</exception>
    public void ClearValue(DependencyProperty dp)
    {
    }

    /// <summary>清除只读依赖属性的本地值。</summary>
    /// <param name="key">要清除的只读依赖属性的键。</param>
    /// <exception cref="System.InvalidOperationException">尝试在已封存的 DependencyObject 上调用 ClearValue 会抛出异常。</exception>
    public void ClearValue(DependencyPropertyKey key)
    {
    }

    /// <summary>强制指定依赖属性的值。调用属性元数据中的 CoerceValueCallback 回调。</summary>
    /// <param name="dp">要强制的依赖属性标识。</param>
    /// <exception cref="System.InvalidOperationException">参数无效或不存在时抛出。</exception>
    public void CoerceValue(DependencyProperty dp)
    {
    }

    /// <summary>判断传入的 DependencyObject 是否与当前实例相等。</summary>
    /// <param name="obj">要比较的 DependencyObject。</param>
    /// <returns>如果两者相同，返回 true；否则 false。</returns>
    public sealed override bool Equals(object obj)
    {
        throw null;
    }

    /// <summary>获取当前 DependencyObject 的哈希代码。</summary>
    /// <returns>返回 32 位有符号整数的哈希码。</returns>
    public sealed override int GetHashCode()
    {
        throw null;
    }

    /// <summary>获取一个专门的枚举器，用于枚举在该 DependencyObject 上本地设置的依赖属性。</summary>
    /// <returns>返回一个 LocalValueEnumerator 实例。</returns>
    public LocalValueEnumerator GetLocalValueEnumerator()
    {
        throw null;
    }

    /// <summary>获取指定依赖属性在此 DependencyObject 上的当前有效值。</summary>
    /// <param name="dp">要获取值的依赖属性标识。</param>
    /// <exception cref="System.InvalidOperationException">参数无效或不存在时抛出。</exception>
    /// <returns>返回该属性的当前有效值。</returns>
    public object GetValue(DependencyProperty dp)
    {
        throw null;
    }

    /// <summary>使指定的依赖属性无效，重新计算其值。</summary>
    /// <param name="dp">要使无效的依赖属性标识。</param>
    public void InvalidateProperty(DependencyProperty dp)
    {
    }

    /// <summary>当该 DependencyObject 上任意依赖属性的有效值更新时调用。事件参数包含具体属性及其旧新值。</summary>
    /// <param name="e">包含依赖属性标识、类型元数据、旧值和新值的事件数据。</param>
    protected virtual void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
    }

    /// <summary>返回指定依赖属性的本地值（如果存在）。</summary>
    /// <param name="dp">要读取本地值的依赖属性标识。</param>
    /// <returns>返回本地值，若未设置本地值，则返回 DependencyProperty.UnsetValue。</returns>
    public object ReadLocalValue(DependencyProperty dp)
    {
        throw null;
    }

    /// <summary>设置依赖属性的值，但不改变值的来源。</summary>
    /// <param name="dp">要设置的依赖属性标识。</param>
    /// <param name="value">新本地值。</param>
    /// <exception cref="System.InvalidOperationException">尝试修改只读属性或已封存对象时抛出。</exception>
    /// <exception cref="System.ArgumentException">值类型与属性注册时不匹配时抛出。</exception>
    public void SetCurrentValue(DependencyProperty dp, object value)
    {
    }

    /// <summary>设置指定依赖属性的本地值。</summary>
    /// <param name="dp">依赖属性标识。</param>
    /// <param name="value">新的本地值。</param>
    /// <exception cref="System.InvalidOperationException">尝试修改只读属性或已封存对象时抛出。</exception>
    /// <exception cref="System.ArgumentException">值类型与属性注册时不匹配时抛出。</exception>
    public void SetValue(DependencyProperty dp, object value)
    {
    }

    /// <summary>设置只读依赖属性的本地值，通过 DependencyPropertyKey 标识。</summary>
    /// <param name="key">只读依赖属性的键。</param>
    /// <param name="value">新的本地值。</param>
    public void SetValue(DependencyPropertyKey key, object value)
    {
    }

    /// <summary>确定是否应序列化指定的依赖属性的值。</summary>
    /// <param name="dp">要序列化的依赖属性标识。</param>
    /// <returns>如果应序列化，返回 true；否则返回 false。</returns>
    protected internal virtual bool ShouldSerializeProperty(DependencyProperty dp)
    {
        throw null;
    }
}
