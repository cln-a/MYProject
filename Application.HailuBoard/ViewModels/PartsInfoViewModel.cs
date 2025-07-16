using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;

namespace Application.HailuBoard
{
    public class PartsInfoViewModel : BasePageViewModel<PartsInfoDto>
    {
        private readonly IPartsInfoDAL _partsInfoDAL;

        public IPartsInfoDAL PartsInfoDAL => _partsInfoDAL;

        public PartsInfoViewModel(IPartsInfoDAL partsInfoDAL)
        {
            this._partsInfoDAL = partsInfoDAL;
        }
        protected override async Task<PageResult<PartsInfoDto>> GetPage()
        {
            var result = await PartsInfoDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<PartsInfoDto>(x));
        }
    }
}