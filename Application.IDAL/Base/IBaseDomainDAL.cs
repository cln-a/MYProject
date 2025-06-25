using Application.Model;

namespace Application.IDAL
{
    public interface IBaseDomainDAL<DomainType> where DomainType : BaseDomain
    {
    }
}
