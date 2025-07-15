using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    public class SinglePartInfoDAL : BaseDomainDAL<SinglePartInfo>, ISinglePartInfoDAL
    {
        public SinglePartInfoDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        public async Task<int> InsertSingleAsync(SinglePartInfo singlepartinfo)
        {
            try
            {
                var id = await SqlSugarClient.Insertable<SinglePartInfo>(singlepartinfo).ExecuteCommandAsync();
                return id;
            }
            catch(Exception ex) 
            {
                return default;
            }
        }
    }
}
