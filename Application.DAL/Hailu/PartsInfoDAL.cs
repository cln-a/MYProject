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

        public async Task<PartsInfo> QueryProduceDataAsync(string batchcode)
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

        public int QueryProduceDataCount(string batchcode)
        {
            try
            {
                return SqlSugarClient
                    .Queryable<PartsInfo>()
                    .Where(x => x.BatchCode == batchcode && x.Countinfo < x.Quautity)
                    .Count();
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdatePartsInfoAsync(PartsInfo partsInfo)
        {
            try
            {
                var result = await SqlSugarClient.Updateable(partsInfo).ExecuteCommandAsync();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
