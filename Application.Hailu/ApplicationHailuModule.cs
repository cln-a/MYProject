using Microsoft.Extensions.Configuration;
using Unity.Injection;

namespace Application.Hailu
{
    public class ApplicationHailuModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
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
        }
    }
}
