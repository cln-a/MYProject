using System.Collections.Concurrent;

namespace Application.ImageProcess
{
    public class ImageProcessModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var producer = containerProvider.Resolve<IProducer>();
            producer.StartProducer();

            var consumer = containerProvider.Resolve<IConsumer>();
            consumer.StartConsum();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            unityContainer.RegisterSingleton<BlockingCollection<ImageData>>();
            unityContainer.RegisterSingleton<IProducer, ImageProducer>();
            unityContainer.RegisterSingleton<IConsumer, ImageConsumer>();
        }
    }
}
