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

        public async Task<int> BatchDeleteByIdAsync(List<int> identityList)
        {
            try
            {
                var affectrows = await SqlSugarClient
                    .Deleteable<SinglePartInfo>()
                    .Where(x => identityList.Contains(x.ProductId))
                    .ExecuteCommandAsync();
                return affectrows;
            }
            catch (Exception ex) 
            {
                return 0;
            }
        }

        public async Task<int> InsertSingleAsync(SinglePartInfo singlepartinfo)
        {
            try
            {
                var id = await SqlSugarClient.Insertable<SinglePartInfo>(singlepartinfo).ExecuteReturnIdentityAsync();
                return id;
            }
            catch(Exception ex) 
            {
                return 0;
            }
        }

        public async Task<int> UpdateSingleAsync(int id,SinglePartInfo singlepartinfo)
        {
            try
            {
                var result = await SqlSugarClient
                    .Updateable(singlepartinfo)
                    .Where(x=>x.Id==id)
                    .ExecuteCommandAsync();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
