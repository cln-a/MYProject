using Application.Model;

namespace Application.IDAL
{
    public interface IBaseDomainDAL<DomainType> 
    {
        void CreateTable();
    }
}
