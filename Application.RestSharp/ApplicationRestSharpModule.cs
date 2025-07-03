using Application.Common;

namespace Application.RestSharp
{
    public class ApplicationRestSharpModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            unityContainer.RegisterSingleton<RestSharpOptions>();
            unityContainer.RegisterSingleton<IRestSharpService, RestSharpService>();
        }
    }
}
