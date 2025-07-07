using Application.Model;

namespace Application.IDAL
{
    public interface IBaseDomainDAL<DomainType> 
    {
        void CreateTable();
        List<DomainType> GetAllEnabled();
        PageResult<DomainType> GetPage(int pagenumber, int pagesize);
    }
}
