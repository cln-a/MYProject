using System.Windows;
using Application.Common;

namespace Application.UI;

public class LanguageManager : ILanguageManager
{
    private ResourceDictionary ResourceDictionary { get; set; }
    
    private string Uri { get; set; } 

    public string this[string key]
    {
        get
        {
            if (this.ResourceDictionary != null && this.ResourceDictionary.Contains(key))
                return this.ResourceDictionary[key].ToString()!;
            
            return $"!{key}!";
        }
    }
    
    public LanguageType CurrentLanguageType { get; private set; }

    public LanguageManager() { }
    
    public void SetLanguage(LanguageType languageType)
    {
        Assert.NotNull(languageType);

        if (this.Uri == null)
        {
            ResourceDictionary resourceDictionary = System.Windows.Application.Current.Resources.MergedDictionaries[0];
            string path = resourceDictionary.Source.AbsolutePath;
            this.Uri = path.Remove(path.LastIndexOf("/"));
        }

        string target = $"{this.Uri}/{languageType}.xaml";
        this.ResourceDictionary =
            (ResourceDictionary)System.Windows.Application.LoadComponent(new Uri(target, UriKind.RelativeOrAbsolute));
        System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(0);
        System.Windows.Application.Current.Resources.MergedDictionaries.Insert(0, this.ResourceDictionary);

        if (CurrentLanguageType != languageType)
        {
            CurrentLanguageType = languageType;
            //todo 保存到系统设置里
        }
    }
}