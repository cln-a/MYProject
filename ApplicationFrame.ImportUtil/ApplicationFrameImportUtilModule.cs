using Application.Common;
using ApplicationFrame.ImportUtil;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using Unity.Injection;

namespace ApplicationFrame
{
    public class ApplicationFrameImportUtilModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "importUtilHaiLuConfig.json");
            var json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<ImportUtilConfig>(json);

            unityContainer.RegisterType<IImportUtil, ImportUtilHaiLu>
                (new InjectionConstructor(config?.FolderPaths, config?.HistoryFolderPath, typeof(ILogger)));
        }
    }
}
