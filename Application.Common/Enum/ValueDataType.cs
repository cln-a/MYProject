using System.ComponentModel;

namespace Application.Common
{
    [Flags]
    public enum ValueDataType : int
    {
        [Description("Boolean")] Boolean = 0,
        [Description("Byte")] Byte = 1,
        [Description("Int16")] Int16 = 2,
        [Description("UInt16")] UInt16 = 3,
        [Description("Int32")] Int32 = 4,
        [Description("UInt32")] UInt32 = 5,
        [Description("Int64")] Int64 = 6,
        [Description("UInt64")] UInt64 = 7,
        [Description("Single")] Single = 8,
        [Description("Double")] Double = 9,
        [Description("String")] String = 10,
        [Description("Ascii")] Ascii = 11,
        [Description("Word")] Word = 12,
        [Description("DWord")] DWord = 13,
        [Description("Unicode")] Unicode = 14
    }
}
