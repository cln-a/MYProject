using Microsoft.Extensions.Configuration;
using Unity.Injection;

namespace Application.SemiAuto
{
    public class SemiAutoModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("SemiAuto.json", false, true)
                .Build();
            var setparameteroption = builder
                .GetSection("SetParameterOption")
                .Get<SetParameterOption[]>()!
                .FirstOrDefault();
            var curparameteroption = builder
                .GetSection("CurParameterOption")
                .Get<CurParameterOption[]>()!
                .FirstOrDefault();
            var triggerparameteroption = builder
                .GetSection("TriggerParameterOption")
                .Get<TriggerParameterOption[]>()!
                .FirstOrDefault();

            unityContainer.RegisterSingleton<SetParamsFactory>
                (setparameteroption?.Name, new InjectionConstructor(setparameteroption, typeof(IEventAggregator)));
            unityContainer.RegisterSingleton<CurParamsFactory>
                (curparameteroption?.Name, new InjectionConstructor(curparameteroption));
            unityContainer.RegisterSingleton<TriggerParamsFactory>
                (triggerparameteroption?.Name, new InjectionConstructor(triggerparameteroption));
        }
    }
}
