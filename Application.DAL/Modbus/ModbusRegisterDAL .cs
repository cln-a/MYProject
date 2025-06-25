using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    public class ModbusRegisterDAL : BaseDomainDAL<ModbusRegister>, IModbusRegisterDAL
    {
        public ModbusRegisterDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient) 
            : base(sqlSugarClient)
        {
        }

        public List<ModbusRegister> GetAllReadableByDeviceId(int deviceId)
        {
            try
            {
                return SqlSugarClient.Queryable<ModbusRegister>().Where(x => x.DeviceId == deviceId && x.Readable).ToList();
            }
            catch(Exception e)
            {
                //Logger
                throw;
            }
        }
    }
}
