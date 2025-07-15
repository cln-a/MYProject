using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    public class PartsInfoDAL : BaseDomainDAL<PartsInfo>, IPartsInfoDAL
    {
        public PartsInfoDAL([Dependency(ConstName.ApplicationDataBase)] ISqlSugarClient sqlSugarClient) : base(sqlSugarClient)
        {
        }

        public async Task<PartsInfo> QueryProduceData(string batchcode)
        {
            try
            {
                var result = await SqlSugarClient.Queryable<PartsInfo>()
                    .Where(x => x.BatchCode == batchcode && x.Countinfo < x.Quautity)
                    .OrderBy(x => x.Id)
                    .FirstAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

    }
}
