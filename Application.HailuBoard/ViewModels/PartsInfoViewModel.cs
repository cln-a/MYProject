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
        private readonly IDialogService _dialogService;
        private string? _batchcode;
        private DelegateCommand _setBatchCodeCommmand;

        [Dependency("HaiLu")] public ParameterFactory ParameterFactory { get; set; }
        public IPartsInfoDAL PartsInfoDAL => _partsInfoDAL;
        public string? BatchCode { get => _batchcode; set => SetProperty(ref _batchcode, value); }
        public DelegateCommand SetBatchCodeCommand => _setBatchCodeCommmand ??= new DelegateCommand(() =>
        {
            _dialogService.Show("DialogView",new DialogParameters
            {
                { "Title", $"是否设定当前批次为{BatchCode}？" }
            }, result =>
            {
                if(result.Result == ButtonResult.OK)
                {
                    ParameterFactory.BatchCode = BatchCode;
                    _eventAggregator.GetEvent<BatchCodeChangedEvent>().Publish(BatchCode!);
                }
                else
                {
                    InfoGlobal($"{BatchCode}批次已取消设定，请重新输入");
                }
            });
            
        });
public PartsInfoViewModel(IPartsInfoDAL partsInfoDAL,
            IEventAggregator eventAggregator, 
            IDialogService dialogService)
        {
            this._partsInfoDAL = partsInfoDAL;
            this._eventAggregator = eventAggregator;
            this._dialogService = dialogService;

            this._eventAggregator.GetEvent<RefreshUiEvent>().Subscribe(Initialize);
            this._eventAggregator.GetEvent<SendMessageEvent>().Subscribe(InfoGlobal);
        }
        protected override async Task<PageResult<PartsInfoDto>> GetPage()
        {
            var result = await PartsInfoDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<PartsInfoDto>(x));
        }
    }
}