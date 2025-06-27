namespace Application.Common;

public interface ILanguageManager
{
    /// <summary>
    /// 索引器声明，可以像访问数组一样，通过字符串key来获取对应的值
    /// </summary>
    /// <param name="key"></param>
    string this[string key] { get;}
    
    /// <summary>
    /// 当前的语言类别
    /// </summary>
    LanguageType CurrentLanguageType { get; }
    
    /// <summary>
    /// 设置语言
    /// </summary>
    /// <param name="languageType"></param>
    void SetLanguage(LanguageType languageType);
}