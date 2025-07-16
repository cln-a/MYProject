using Application.Model;

namespace Application.IDAL
{
    public interface IPartsInfoDAL : IBaseDomainDAL<PartsInfo>
    {
        Task<PartsInfo> QueryProduceDataAsync(string batchcode);

        int QueryProduceDataCount(string batchcode);

        Task<int> UpdatePartsInfoAsync(PartsInfo partsInfo);
    }
}
