using Application.Common;
using Application.Hailu;
using Application.HailuBoard.Event;
using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;
using HandyControl.Controls;
using System;

namespace Application.HailuBoard
{
    public class PartsInfoViewModel : BasePageViewModel<PartsInfoDto>
    {
        private readonly IPartsInfoDAL _partsInfoDAL;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
        private readonly ViewModelDtoMapepr _viewModelDtoMapepr;
        private string? _batchcode;
        private string? _batch;
        private string? _name;
        private DelegateCommand _setBatchCodeCommmand;
        private DelegateCommand<PartsInfoDto> _forceQuitCommand;
        private DelegateCommand _batchDeleteCommand;

        [Dependency("HaiLu")] public ParameterFactory ParameterFactory { get; set; }
        public string? BatchCode { get => _batchcode; set => SetProperty(ref _batchcode, value); }
        public string? Batch { get => _batch; set => SetProperty(ref _batch, value); }
        public string? Name { get => _name; set => SetProperty(ref _name, value); }
        public DelegateCommand SetBatchCodeCommand => _setBatchCodeCommmand ??= new DelegateCommand(() =>
        {
            _dialogService.Show("DialogView",new DialogParameters
            {
                { "Title", $"是否设定合同号：{BatchCode}，批次：{Batch}，名称：{Name}的生产数据？" }
            }, result =>
            {
                if(result.Result == ButtonResult.OK)
                {
                    ParameterFactory.BatchCode = BatchCode;
                    ParameterFactory.Batch = Batch;
                    ParameterFactory.Name = Name;
                    ParameterFactory.Identity = BatchCode + Batch + Name;
                    _eventAggregator.GetEvent<BatchCodeChangedEvent>().Publish((BatchCode + Batch + Name)!);
                }
                else
                {
                    InfoGlobal($"合同号：{ParameterFactory.BatchCode}，批次：{ParameterFactory.Batch}，名称：{ParameterFactory.Name}已取消设定，请重新输入");
                }
            });
        });
        public DelegateCommand<PartsInfoDto> ForceQuitCommand => _forceQuitCommand ??= new DelegateCommand<PartsInfoDto>(ForceQuitCmd);
        public DelegateCommand BatchDeleteCommand => _batchDeleteCommand ??= new DelegateCommand(BatchDeleteCmd);

        public PartsInfoViewModel(
            IPartsInfoDAL partsInfoDAL, 
            IEventAggregator eventAggregator, 
            IDialogService dialogService,
            ViewModelDtoMapepr viewModelDtoMapepr)
        {
            this._partsInfoDAL = partsInfoDAL;
            this._eventAggregator = eventAggregator;
            this._dialogService = dialogService;
            this._viewModelDtoMapepr = viewModelDtoMapepr;

            this._eventAggregator.GetEvent<RefreshUiEvent>().Subscribe(Initialize);
            this._eventAggregator.GetEvent<SendMessageEvent>().Subscribe(InfoGlobal);
        }

        protected override async Task<PageResult<PartsInfoDto>> GetPage() 
        {
            var result = await _partsInfoDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<PartsInfoDto>(x));
        }

        protected override void InsertCmd()
        {
            var parameter = new DialogParameters
            {
                {TitleKey,"新增岩棉线生产数据" },
                {ModelKey,new PartsInfoDto() },
                {CommandTypeKey,CommandTypeEnum.Add }
            };
            _dialogService.ShowDialog("PartsInfoEditDialog", parameter, arg => { Initialize(); });
        }

        protected void BatchDeleteCmd()
        {
            if (SelectedItems == null)
            {
                Growl.Error("未选中需要删除的数据！");
                return;
            }
            else
            {
                _dialogService.Show("DialogView", new DialogParameters
            {
                { "Title", $"是否需要删除所选中生产数据？" }
            }, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    foreach (var entity in SelectedItems) 
                    {
                        _partsInfoDAL.SingleDeleteByIdAsync(entity.Id);
                    }
                    var identityList = SelectedItems.Select(item => item.Id).ToList();
                    _eventAggregator.GetEvent<BatchDeleteByIdEvent>().Publish(identityList);
                    Initialize();
                }
                else
                {
                    InfoGlobal($"操作已取消");
                }
            });
            }
        }

        protected override void UpdateCmd(PartsInfoDto entity)
        {
            if (entity == null)
            {
                Growl.Error("未选中需要更新的数据！");
                return;
            }
            else
            {
                var parameter = new DialogParameters
                {
                    {TitleKey,"编辑岩棉线生产数据" },
                    {ModelKey,_viewModelDtoMapepr.GetNewPartsInfoDtoModel(entity)},
                    {CommandTypeKey,CommandTypeEnum.Edit }
                };
                _dialogService.ShowDialog("PartsInfoEditDialog", parameter, arg => { Initialize(); });
            }
        }

        private void ForceQuitCmd(PartsInfoDto entity)
        {
            if (entity == null)
            {
                Growl.Error("未选中需要强制结束的数据！");
                return;
            }
            else
            {
                entity.Countinfo = entity.Quautity;
                _partsInfoDAL.UpdatePartsInfoAsync(Mapper.Map<PartsInfo>(entity));
                Initialize();
                InfoGlobal("所选中数据已强制结束！");
            }
        }
    }
}