using Application.Model;

namespace Application.IDAL
{
    public interface ISinglePartInfoDAL : IBaseDomainDAL<SinglePartInfo>
    {
        Task<int> InsertSingleAsync(SinglePartInfo singlepartinfo);
    }
}
