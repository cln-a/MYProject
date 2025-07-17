using Application.Hailu;
using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;

namespace Application.HailuBoard
{
    public class SinglePartInfoViewModel : BasePageViewModel<SinglePartInfoDto>
    {
        private readonly ISinglePartInfoDAL _singlePartInfoDAL;
        private readonly IEventAggregator _eventAggregator;

        public ISinglePartInfoDAL SinglePartInfoDAL => _singlePartInfoDAL;

        public SinglePartInfoViewModel(ISinglePartInfoDAL singlePartInfoDAL, IEventAggregator eventAggregator)
        {
            this._singlePartInfoDAL = singlePartInfoDAL;
            this._eventAggregator = eventAggregator

            this._eventAggregator.GetEvent<RefreshUiEvent>().Subscribe(Initialize);
        }

        protected override async Task<PageResult<SinglePartInfoDto>> GetPage()
        {
            var result = await SinglePartInfoDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<SinglePartInfoDto>(x));
        }
    }
}