using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    public class SystemUserDAL : BaseDomainDAL<SystemUser>, ISystemUserDAL
    {
        public SystemUserDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient) 
            : base(sqlSugarClient)
        {
        }
    }
}
