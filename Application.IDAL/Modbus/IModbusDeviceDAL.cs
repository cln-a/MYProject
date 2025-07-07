using Application.Model;

namespace Application.IDAL
{
    public interface IModbusDeviceDAL : IBaseDomainDAL<ModbusDevice>
    {
        PageResult<ModbusDevice> GetPage(int pagenumber, int pagesize);
    }
}
