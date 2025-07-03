namespace Application.IDAL
{
    public interface IBaseDomainDAL<DomainType> 
    {
        void CreateTable();
        List<DomainType> GetAllEnabled();
    }
}
