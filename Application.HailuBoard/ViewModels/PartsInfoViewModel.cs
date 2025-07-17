using Application.Hailu;
using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;

namespace Application.HailuBoard
{
    public class PartsInfoViewModel : BasePageViewModel<PartsInfoDto>
    {
        private readonly IPartsInfoDAL _partsInfoDAL;
        private readonly IEventAggregator _eventAggregator;
        private string? _batchcode;
        private DelegateCommand _setBatchCodeCommmand;

        [Dependency("HaiLu")] public ParameterFactory ParameterFactory { get; set; }
        public IPartsInfoDAL PartsInfoDAL => _partsInfoDAL;
        public string? BatchCode { get => _batchcode; set => SetProperty(ref _batchcode, value); }
        public DelegateCommand SetBatchCodeCommand => _setBatchCodeCommmand ??= new DelegateCommand(() =>
        {
            ParameterFactory.BatchCode = BatchCode;
            _eventAggregator.GetEvent<BatchCodeChangedEvent>().Publish(BatchCode!);
        });

        public PartsInfoViewModel(IPartsInfoDAL partsInfoDAL,IEventAggregator eventAggregator)
        {
            this._partsInfoDAL = partsInfoDAL;
            this._eventAggregator = eventAggregator;

            this._eventAggregator.GetEvent<RefreshUiEvent>().Subscribe(Initialize);
        }
        protected override async Task<PageResult<PartsInfoDto>> GetPage()
        {
            var result = await PartsInfoDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<PartsInfoDto>(x));
        }
    }
}