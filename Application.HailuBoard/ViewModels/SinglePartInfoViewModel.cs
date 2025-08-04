using Application.Hailu;
using Application.HailuBoard.Event;
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

        public SinglePartInfoViewModel(ISinglePartInfoDAL singlePartInfoDAL, IEventAggregator eventAggregator)
        {
            this._singlePartInfoDAL = singlePartInfoDAL;
            this._eventAggregator = eventAggregator;

            this._eventAggregator.GetEvent<RefreshUiEvent>().Subscribe(Initialize);
            this._eventAggregator.GetEvent<BatchDeleteByIdEvent>().Subscribe(async (idList) => await BatchDeleteById(idList));
        }

        private async Task BatchDeleteById(List<int> identitylist)
        {
            var affectrows = await _singlePartInfoDAL.BatchDeleteByIdAsync(identitylist);
            Initialize();
        }

        protected override async Task<PageResult<SinglePartInfoDto>> GetPage()
        {
            var result = await _singlePartInfoDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<SinglePartInfoDto>(x));
        }
    }
}