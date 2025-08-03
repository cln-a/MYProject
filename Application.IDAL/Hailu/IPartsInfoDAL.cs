using Application.Model;

namespace Application.IDAL
{
    public interface IPartsInfoDAL : IBaseDomainDAL<PartsInfo>
    {
        int QueryProduceDataCount(string batchcode);
        int BatchInsertWithOutSame(List<PartsInfo> partsInfos, HashSet<string> identities);
        Task<PartsInfo> QueryProduceDataAsync(string batchcode);
        Task<int> UpdatePartsInfoAsync(PartsInfo partsInfo);
        Task<int> SingleInsertAsync(PartsInfo partsInfo);
        Task<int> SingleDeleteByIdAsync(int identity);
    }
}
