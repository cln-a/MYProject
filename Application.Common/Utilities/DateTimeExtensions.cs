namespace Application.Common.Utilities;

public static class DateTimeExtensions
{
    /// <summary>
    /// 定义了一个标准的时间格式字符串，常用于格式化时间的输出
    /// </summary>
    public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    
    /// <summary>
    /// 是Unix时间的起点（1970-01-01 00:00:00）
    /// </summary>
    private static readonly DateTime TimeStart = new DateTime(1970, 1, 1);
    
    private const long BaseTicks = 621355968000000000;

    /// <summary>
    /// 扩展方法，用于把任意的DateTime 实例转换成 Unix 时间戳（单位：毫秒）
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long DateToTicks(this DateTime time) => (time.Ticks - BaseTicks) / 10000;
}