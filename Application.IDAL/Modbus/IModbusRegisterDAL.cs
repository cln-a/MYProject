using Application.Model;

namespace Application.IDAL
{
    public interface IModbusRegisterDAL : IBaseDomainDAL<ModbusRegister>
    {
        List<ModbusRegister> GetAllReadableByDeviceId(int deviceId);

        List<ModbusRegister> GetAllEnabledByDeviceId(int deviceId);
    }
}
