using System.ComponentModel;

namespace Application.Common;

/// <summary>
/// 语言类型
/// </summary>
public enum LanguageType
{
    [Description(("简体中文"))]
    CN,
    [Description("Россия")]
    Russia,
    [Description("English")]
    US
}