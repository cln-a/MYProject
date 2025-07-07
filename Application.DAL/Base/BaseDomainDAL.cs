using Application.Common;
using Application.IDAL;
using Application.Model;
using SqlSugar;

namespace Application.DAL
{
    /// <summary>
    /// 泛型基类，类型参数为DomainType，并且限制了DomainType 必须继承自BaseDomain
    /// </summary>
    /// <typeparam name="DomainType"></typeparam>
    public class BaseDomainDAL<DomainType> : IBaseDomainDAL<DomainType> where DomainType : BaseDomain
    {
        protected readonly ISqlSugarClient _sqlSugarClient;

        protected ISqlSugarClient SqlSugarClient => _sqlSugarClient;

        public BaseDomainDAL([Dependency(ConstName.ApplicationDataBase)]ISqlSugarClient sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }

        public void CreateTable()
        {
            try
            {
                SqlSugarClient.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(DomainType));
            }
            catch(Exception e)
            {
                //Logger
                throw;
            }
        }

        public List<DomainType> GetAllEnabled()
        {
            try
            {
                return SqlSugarClient.Queryable<DomainType>().Where(x => x.IsEnabled).ToList();
            }
            catch (Exception e)
            {
                //Logger
                throw;
            }
        }

        public async Task<PageResult<DomainType>> GetPage(int pagenumber, int pagesize)
        {
            try
            {
                RefAsync<int> totalCount = 1;
                RefAsync<int> totalPage = 5;
                var pageData = await SqlSugarClient.Queryable<DomainType>().ToPageListAsync(pagenumber, pagesize, totalCount, totalPage);
                return PageResult<DomainType>.CreatePageFromSqlSugar(pageData, pagenumber, pagesize, totalCount, totalPage);
            }
            catch (Exception ex)
            {
                //Logger
                throw;
            }
        }
    }
}
