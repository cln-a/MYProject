using Application.IDAL;
using CommonServiceLocator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using Unity.Injection;

namespace Application.S7net
{
    public class S7netModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) => StartService();

        private void StartService()
        {
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            var clients = ServiceLocator.Current.GetAllInstances<S7netClient>();
            foreach (var client in clients)
            {
                try
                {
                    client.Init();
                    client.Start();
                }
                catch (System.Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }
            var heartBeatMasters = ServiceLocator.Current.GetAllInstances<HeartBeatMaster>();
            foreach (var master in heartBeatMasters)
            {
                try
                {
                    master.Start();
                }
                catch (System.Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer container = containerRegistry.GetContainer();
            var deviceBLL = container.Resolve<IS7netDeviceDAL>();
            var registerBLL = container.Resolve<IS7netRegisterDAL>();
            var devices = deviceBLL.GetAllEnabled();
            foreach (var device in devices)
            {
                container.RegisterType<S7netClient>(
                    device.DeviceUri!.Trim(),
                    TypeLifetime.Singleton,
                    new InjectionConstructor(device)
                );
                var registers = registerBLL.GetAllReadableByDeviceId(device.Id);
                foreach (var register in registers)
                    container.RegisterVariableType(register, device);
                var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("s7settings.json", false, true)
                .Build();
                var hearbeatMasterOptions = builder
                .GetSection("HeartBeatMasterOptions")
                .Get<HeartBeatMasterOption[]>();
                if (hearbeatMasterOptions != null)
                {
                    foreach (var option in hearbeatMasterOptions)
                        container.RegisterType<HeartBeatMaster>(
                            option.DeviceName.Trim(),
                            TypeLifetime.Singleton,
                            new InjectionConstructor(option)
                        );
                }
            }
        }
    }
}
