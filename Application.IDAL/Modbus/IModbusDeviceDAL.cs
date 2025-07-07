using Application.Model;

namespace Application.IDAL
{
    public interface IModbusDeviceDAL : IBaseDomainDAL<ModbusDevice>
    {
        List<ModbusDevice> GetAllEnabledDevices();
    }
}
