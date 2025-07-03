using Application.Common;
using Application.IDAL;
using Application.Model.System;
using SqlSugar;

namespace Application.DAL
{
    public class SystemMenuDAL : BaseDomainDAL<SystemMenu>, ISystemMenuDAL
    {
        public SystemMenuDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient) 
            : base(sqlSugarClient)
        {
        }
    }
}
