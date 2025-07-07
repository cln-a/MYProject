using Application.Common;
using Application.IDAL;
using CommonServiceLocator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Unity.Injection;

namespace Application.Modbus
{
    public class ModbusModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) => StartService();

        private void StartService()
        {
            var logger = ServiceLocator.Current.GetInstance<ILogger>();

            var clients = ServiceLocator.Current.GetAllInstances<ModbusClient>();
            foreach (var client in clients)
            {
                try
                {
                    client.Init();
                    client.Start();
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                }
            }

            var heartBeatMasters = ServiceLocator.Current.GetAllInstances<HeartBeatMaster>();
            foreach (var master in heartBeatMasters)
            {
                try
                {
                    master.Start();
                }
                catch (Exception e)
                {
                    logger.LogError( e.Message);
                }
            }
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            var deviceDAL = unityContainer.Resolve<IModbusDeviceDAL>();
            var registerDAL = unityContainer.Resolve<IModbusRegisterDAL>();
            var devices = deviceDAL.GetAllEnabledDevices();
            foreach (var device in devices)
            {
                unityContainer.RegisterType<ICommunicationStateMachine, PlcState>(
                    device.DeviceUri!.Trim(), 
                    TypeLifetime.Singleton,
                    new InjectionConstructor(device));

                unityContainer.RegisterType<ModbusClient>(
                    device.DeviceUri.Trim(),
                    TypeLifetime.Singleton,
                    new InjectionConstructor(device));

                var registers = registerDAL.GetAllEnabledByDeviceId(device.Id);
                foreach (var register in registers)
                    unityContainer.RegisterVariableType(register, device);
            }

            var builder = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("modbussettings.json", false, true)
              .Build();

            var hearbeatMasterOptions = builder.GetSection("HeartBeatMasterOptions").Get<HeartBeatMasterOption[]>();

            if (hearbeatMasterOptions != null)
            {
                foreach (var option in hearbeatMasterOptions)
                {
                    unityContainer.RegisterType<HeartBeatMaster>(
                        option.DeviceName.Trim(), 
                        TypeLifetime.Singleton,
                        new InjectionConstructor(option));
                }
            }
        }
    }
}
