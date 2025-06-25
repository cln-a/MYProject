using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    public class ModbusDeviceDAL : BaseDomainDAL<ModbusDevice>, IModbusDeviceDAL
    {
        public ModbusDeviceDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient)
            : base(sqlSugarClient)
        {
        }
    }
}
