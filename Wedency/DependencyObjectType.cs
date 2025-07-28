namespace Wedency;

/// <summary>实现了所有从 <see cref="Wedency" /> 派生类型的底层类型缓存。</summary>
public class DependencyObjectType
{
    /// <summary>获取当前 <see cref="Wedency.DependencyObjectType" /> 的直接基类的 <see cref="Wedency.DependencyObjectType" />。</summary>
    /// <returns>基类的类型。</returns>
    public DependencyObjectType BaseType
    {
        get
        {
            throw null;
        }
    }

    /// <summary>获取一个从零开始的唯一标识符，用于常量时间的数组查找操作。</summary>
    /// <returns>内部标识符。</returns>
    public int Id
    {
        get
        {
            throw null;
        }
    }

    /// <summary>获取所表示的公共语言运行时（CLR）系统类型的名称。</summary>
    /// <returns>所表示的 CLR 系统类型名称。</returns>
    public string Name
    {
        get
        {
            throw null;
        }
    }

    /// <summary>获取此 <see cref="Wedency.DependencyObjectType" /> 所表示的公共语言运行时（CLR）系统类型。</summary>
    /// <returns>此 <see cref="Wedency.DependencyObjectType" /> 所表示的 CLR 系统类型。</returns>
    public Type SystemType
    {
        get
        {
            throw null;
        }
    }

    internal DependencyObjectType()
    {
    }

    /// <summary>返回一个表示给定系统（CLR）类型的 <see cref="Wedency.DependencyObjectType" />。</summary>
    /// <param name="systemType">要转换的系统（CLR）类型。</param>
    /// <returns>表示该系统（CLR）类型的 <see cref="Wedency.DependencyObjectType" />。</returns>
    public static DependencyObjectType FromSystemType(Type systemType)
    {
        throw null;
    }

    /// <summary>返回此 <see cref="Wedency.DependencyObjectType" /> 的哈希码。</summary>
    /// <returns>一个 32 位有符号整数的哈希码。</returns>
    public override int GetHashCode()
    {
        throw null;
    }

    /// <summary>确定指定对象是否是当前 <see cref="Wedency.DependencyObjectType" /> 的实例。</summary>
    /// <param name="dependencyObject">用于与当前 <see cref="Wedency.DependencyObjectType" /> 进行比较的对象。</param>
    /// <returns>
    /// 如果当前 <see cref="Wedency.DependencyObjectType" /> 所代表的类位于传入的 <paramref name="dependencyObject" /> 的继承层次中，则返回 <see langword="true" />；否则返回 <see langword="false" />。</returns>
    public bool IsInstanceOfType(DependencyObject dependencyObject)
    {
        throw null;
    }

    /// <summary>确定当前 <see cref="Wedency.DependencyObjectType" /> 是否派生自指定的 <see cref="Wedency.DependencyObjectType" />。</summary>
    /// <param name="dependencyObjectType">要比较的 <see cref="Wedency.DependencyObjectType" />。</param>
    /// <returns>
    /// 如果参数 <paramref name="dependencyObjectType" /> 和当前实例均表示类类型，且当前实例所代表的类派生自参数所代表的类，则返回 <see langword="true" />；否则返回 <see langword="false" />。若两者代表相同类，也返回 <see langword="false" />。</returns>
    public bool IsSubclassOf(DependencyObjectType dependencyObjectType)
    {
        throw null;
    }
}
