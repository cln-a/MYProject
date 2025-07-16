using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;
using System.Drawing.Printing;

namespace Application.HailuBoard
{
    public class SinglePartInfoViewModel : BasePageViewModel<SinglePartInfoDto>
    {
        private readonly ISinglePartInfoDAL _singlePartInfoDAL;

        public ISinglePartInfoDAL SinglePartInfoDAL => _singlePartInfoDAL;

        public SinglePartInfoViewModel(ISinglePartInfoDAL singlePartInfoDAL)
        {
            this._singlePartInfoDAL = singlePartInfoDAL;
        }

        protected override async Task<PageResult<SinglePartInfoDto>> GetPage()
        {
            var result = await SinglePartInfoDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<SinglePartInfoDto>(x));
        }
    }
}