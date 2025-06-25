using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    public class S7netDeviceDAL : BaseDomainDAL<S7netDevice>, IS7netDeviceDAL
    {
        public S7netDeviceDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient) 
            : base(sqlSugarClient)
        {
        }
    }
}
