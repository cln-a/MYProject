
using NLog;
using NLog.Extensions.Logging;

namespace Application.Logger
{
    public class LoggerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            LogManager.Setup().LoadConfigurationFromFile("Nlog.config");
            LogManager.AutoShutdown = true;
            var _loggerFactory = new NLogLoggerFactory(new NLogLoggerProvider(new NLogProviderOptions(), LogManager.LogFactory));
            var _logger = _loggerFactory.CreateLogger("MainLogger");
            containerRegistry.RegisterSingleton<Microsoft.Extensions.Logging.ILogger>(X => _logger);
        }
    }
}
