using Application.IDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Unity.Injection;

namespace Application.Hailu
{
    public class ApplicationHailuModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var mannager = containerProvider.Resolve<IManager>();
            mannager.StartService();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("HailuOption.json", false, true)
                .Build();
            var parameteroption = builder
                .GetSection("ParameterOption")
                .Get<ParameterOption[]>()!
                .FirstOrDefault();
            unityContainer.RegisterSingleton<ParameterFactory>
                (parameteroption?.Name, new InjectionConstructor(parameteroption, typeof(IEventAggregator)));

            unityContainer.RegisterType<IManager, HaiLuManager>(new InjectionConstructor(typeof(ILogger), typeof(IEventAggregator), typeof(IPartsInfoDAL), typeof(ISinglePartInfoDAL)));
        }
    }
}
