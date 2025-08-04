using Application.Model;

namespace Application.IDAL
{
    public interface ISinglePartInfoDAL : IBaseDomainDAL<SinglePartInfo>
    {
        Task<int> InsertSingleAsync(SinglePartInfo singlepartinfo);
        
        Task<int> UpdateSingleAsync(int id,SinglePartInfo singlepartinfo);

        Task<int> BatchDeleteByIdAsync(List<int> identityList);
    }
}
