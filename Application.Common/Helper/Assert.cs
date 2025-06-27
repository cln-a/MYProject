using System.Runtime.CompilerServices;

namespace Application.Common;

/// <summary>
/// 断言类型，判断参数或对象是否为空
/// </summary>
public static class Assert
{
    /// <summary>
    ///  静态类，通常用于编写断言工具（如调试检查、单元测试辅助）
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="memberName">CallerMemberName 是一个 编译器特性,会自动把 调用这个方法的函数名 作为参数传入</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    public static void NotNull<T>(T obj, [CallerMemberName] string memberName = null!)
    {
        if (obj == null)
            throw new Exception($"Parameter cannot be null: {nameof(obj)} in {memberName}");
    }
}