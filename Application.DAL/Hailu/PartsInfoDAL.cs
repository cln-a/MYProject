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
                var result = SqlSugarClient
                    .Queryable<PartsInfo>()
                    .Where(x => x.BatchCode == batchcode && x.Countinfo < x.Quautity);
                return result.Count();
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SingleDeleteByIdAsync(int identity)
        {
            try
            {
                var result = await SqlSugarClient
                    .Deleteable<PartsInfo>()
                    .Where(x => x.Id == identity)
                    .ExecuteCommandAsync();
                return result;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SingleInsertAsync(PartsInfo partsInfo)
        {
            try
            {
                var result = await SqlSugarClient.Insertable<PartsInfo>(partsInfo).ExecuteCommandAsync();
                return result;
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
