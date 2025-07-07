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

        public PageResult<ModbusDevice> GetPage(int pagenumber, int pagesize)
        {
            try
            {
                var totalCount = 1;
                var totalPage = 5;
                var pageData = SqlSugarClient.Queryable<ModbusDevice>().ToPageList(pagenumber, pagesize, ref totalCount, ref totalPage);
                return PageResult<ModbusDevice>.CreatePageFromSqlSugar(pageData, pagenumber, pagesize, totalCount, totalPage);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
