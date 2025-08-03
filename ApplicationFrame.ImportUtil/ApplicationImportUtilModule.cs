using Application.Common;
using Application.IDAL;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using Unity.Injection;

namespace Application.ImportUtil
{
    public class ApplicationFrameImportUtilModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var importutil = CommonServiceLocator.ServiceLocator.Current.GetInstance<IImportUtil>();
            importutil.StartImport();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UtilConfig.json");
            var json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<ImportUtilConfig>(json);

            unityContainer.RegisterType<IImportUtil, ImportUtilHaiLu>
                (new InjectionConstructor(config?.FolderPaths, config?.HistoryFolderPath, typeof(ILogger), typeof(IPartsInfoDAL), typeof(IEventAggregator)));
        }
    }
}
