using AutoMapper;

namespace Application.Mapper
{
    public class ApplicationMapperModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();
            unityContainer.RegisterInstance(new MapperConfiguration(cfg =>
            {
                 cfg.AddProfile<DtosMapperProfile>();
            }).CreateMapper());
        }
    }
}
