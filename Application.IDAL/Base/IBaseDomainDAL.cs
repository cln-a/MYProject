using Application.Model;

namespace Application.IDAL
{
    public interface IBaseDomainDAL<DomainType> 
    {
        void CreateTable();
        List<DomainType> GetAllEnabled();
        Task<PageResult<DomainType>> GetPage(int pagenumber, int pagesize);
    }
}
