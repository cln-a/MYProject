using Application.Model;

namespace Application.IDAL
{
    public interface IPartsInfoDAL : IBaseDomainDAL<PartsInfo>
    {
        Task<PartsInfo> QueryProduceData(string batchcode);
        
        Task<int> UpdatePartsInfoAsync(PartsInfo partsInfo);
    }
}
