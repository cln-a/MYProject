using Application.IDAL;
using Application.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Application.Hailu
{
    public class HaiLuManager : IManager
    {
        private readonly ILogger _logger;
        private readonly IPartsInfoDAL _partsInfoDAL;
        private readonly ISinglePartInfoDAL _singlePartInfoDAL;
        private readonly IEventAggregator _eventAggregator;
        private ConcurrentDictionary<int, SinglePartInfo> _dir 
            = new ConcurrentDictionary<int, SinglePartInfo>();

        [Dependency("HaiLu")] public ParameterFactory ParameterFactory { get; set; }
        
        public HaiLuManager(
            ILogger logger,
            IEventAggregator eventAggregator,
            IPartsInfoDAL partsInfoDAL,
            ISinglePartInfoDAL singlePartInfoDAL)
        {
            this._logger = logger;
            this._partsInfoDAL = partsInfoDAL;
            this._singlePartInfoDAL = singlePartInfoDAL;
            this._eventAggregator = eventAggregator;
        }

        private void SendReadyFlagToPLC(string batchcode)
        {
            try
            {
                if (batchcode != null)
                {
                    var result = _partsInfoDAL.QueryProduceDataCount(batchcode);
                    if (result != 0)
                    {
                        ParameterFactory.ReadyFlag = 1;
                        _eventAggregator.GetEvent<SendMessageEvent>().Publish($"开始处理合同号：{ParameterFactory.BatchCode}，批次：{ParameterFactory.Batch}，名称：{ParameterFactory.Name}的板件");
                    }
                    else
                    {
                        ParameterFactory.ReadyFlag = 0;
                        _eventAggregator.GetEvent<SendMessageEvent>().Publish($"未查找到合同号：{ParameterFactory.BatchCode}，批次：{ParameterFactory.Batch}，名称：{ParameterFactory.Name}的数据");
                    }
                }
            }
            catch (Exception e) 
            {
                _logger.LogError(e.Message);
            }
        }

        private async Task WorkActionAsync()
        {
            try
            {
                var result = await _partsInfoDAL.QueryProduceDataAsync(ParameterFactory.Identity!);
                if (result != null)
                {
                    result.Countinfo += 1;
                    await _partsInfoDAL.UpdatePartsInfoAsync(result);
                    var singlePart = new SinglePartInfo()
                    {
                        CountNumber = result.Countinfo,
                        BatchCode = result.BatchCode,
                        Batch = result.Batch,
                        Identity = result.Identity,
                        Name = result.Name,
                        Description = result.Description,
                        Length = result.Length,
                        Width1 = result.Width1,
                        Thickness = result.Thickness,
                        HoleLengthRight = result.HoleLengthRight,
                        HoleDistanceRight = result.HoleDistanceRight,
                        HoleLengthMiddle = result.HoleLengthMiddle,
                        HoleDistanceMiddle = result.HoleDistanceMiddle,
                        HoleLengthLeft = result.HoleLengthLeft,
                        HoleDistanceLeft = result.HoleDistanceLeft,
                        McOrNot = result.McOrNot,
                        StateInfo = "",
                    };
                    var identity = await _singlePartInfoDAL.InsertSingleAsync(singlePart);
                    singlePart.Id = identity;
                    ushort boardtype = 0;
                    if(result.Description!.Contains("L3") || result.Description!.Contains("C2") || result.Description!.Contains("C2A") || result.Description!.Contains("L3S") || result.Description!.Contains("C2S"))
                        boardtype = 1;
                    else if (result.Description!.Contains("L2"))
                        boardtype = 3;
                    await Task.Run(() =>
                    {
                        ParameterFactory.Length = result.Length;
                        ParameterFactory.Width = result.Width1;
                        ParameterFactory.Thickness = result.Thickness;
                        ParameterFactory.HoleLengthRight = result.HoleLengthRight;
                        ParameterFactory.HoleDistanceRight = result.HoleDistanceRight;
                        ParameterFactory.HoleLengthMiddle = result.HoleLengthMiddle;
                        ParameterFactory.HoleDistanceMiddle = result.HoleDistanceMiddle;
                        ParameterFactory.HoleLengthLeft = result.HoleLengthLeft;
                        ParameterFactory.HoleDistanceLeft = result.HoleDistanceLeft;
                        ParameterFactory.BoardType = boardtype;
                    });
                    _dir[identity] = singlePart;
                    _eventAggregator.GetEvent<RefreshUiEvent>().Publish();
                }
                else
                {
                    ParameterFactory.ReadyFlag = 0;
                    _eventAggregator.GetEvent<SendMessageEvent>().Publish($"合同号：{ParameterFactory.BatchCode}，批次：{ParameterFactory.Batch}，名称：{ParameterFactory.Name}已处理完成");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void StartService()
        {
            _eventAggregator.GetEvent<RequestFlagReadedEvent>().Subscribe(async void () =>
            {
                try
                {
                    await WorkActionAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }, ThreadOption.BackgroundThread);

            _eventAggregator.GetEvent<MeasureWidthFlagReadedEvent>()
                .Subscribe(async () =>
                {
                    await ProcessMeasureWidthFlagAsync();
                }, ThreadOption.BackgroundThread);
            _eventAggregator.GetEvent<OffLineFlagReadedEvent>()
                .Subscribe(async () =>
                {
                    await ProcessOffLineFlagAsync();
                }, ThreadOption.BackgroundThread);

            _eventAggregator.GetEvent<BatchCodeChangedEvent>().Subscribe(SendReadyFlagToPLC, ThreadOption.BackgroundThread);
        }

        private async Task ProcessMeasureWidthFlagAsync()
        {
            var record = _dir.Values
                .Where(x => x.StateInfo == "")
                .OrderBy(x => x.Id)
                .FirstOrDefault();
            if (record != null)
            {
                record.StateInfo = "测量完成";
                var result = await _singlePartInfoDAL.UpdateSingleAsync(record.Id, record);

                if (_dir.TryGetValue(record.Id, out var singlePart))
                    singlePart.StateInfo = "测量完成";

                _logger.LogDebug($"产品{record.Id}状态--{record.StateInfo}");
                _eventAggregator.GetEvent<RefreshUiEvent>().Publish();
            }
            ParameterFactory.MeasureWidthFlag = 0;
        }

        private async Task ProcessOffLineFlagAsync()
        {
            var record = _dir.Values
                .Where(x => x.StateInfo == "测量完成")
                .OrderBy(x => x.Id)
                .FirstOrDefault();
            if(record != null)
            {
                record.StateInfo = "工件下线";
                var result = await _singlePartInfoDAL.UpdateSingleAsync(record.Id, record);
                if(_dir.TryRemove(record.Id,out var singlePart))
                {
                    _logger.LogDebug($"序号为{record.Id}的板件处理完成");
                    _eventAggregator.GetEvent<RefreshUiEvent>().Publish();
                }
            }
            ParameterFactory.OffLineFlag = 0;
        }

        public void StopService()
        {
        }
    }
}
