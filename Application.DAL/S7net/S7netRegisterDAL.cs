using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    public class S7netRegisterDAL : BaseDomainDAL<S7netRegister>, IS7netRegisterDAL
    {
        public S7netRegisterDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient) 
            : base(sqlSugarClient)
        {
        }
    }
}
