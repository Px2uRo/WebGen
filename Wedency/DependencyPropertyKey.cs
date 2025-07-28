namespace Wedency;

/// <summary>为只读依赖属性提供有限写入访问的依赖属性标识符。</summary>
public sealed class DependencyPropertyKey
{
    /// <summary>获取与此专门的只读依赖属性标识符关联的依赖属性标识符。</summary>
    /// <returns>相关的依赖属性标识符。</returns>
    public DependencyProperty DependencyProperty
    {
        get
        {
            throw null;
        }
    }

    internal DependencyPropertyKey()
    {
    }

    /// <summary>覆盖由此依赖属性标识符表示的只读依赖属性的元数据。</summary>
    /// <param name="forType">该依赖属性所属且应被覆盖元数据的类型。</param>
    /// <param name="typeMetadata">为该类型提供的元数据。</param>
    /// <exception cref="System.InvalidOperationException">尝试覆盖一个可读写依赖属性的元数据（此签名不支持）。</exception>
    /// <exception cref="System.ArgumentException">该属性在指定类型上已经存在元数据。</exception>
    public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata)
    {
    }
}
