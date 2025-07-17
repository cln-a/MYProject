using Application.Common;
using Application.IDAL;
using Application.Modbus;
using Application.Model;
using CommonServiceLocator;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Application.Hailu
{
    public class HaiLuManager : IManager
    {
        
        private readonly ILogger _logger;
        private readonly IPartsInfoDAL _partsInfoDAL;
        private readonly ISinglePartInfoDAL _singlePartInfoDAL;
        private ConcurrentDictionary<int, SinglePartInfo> _dir 
            = new ConcurrentDictionary<int, SinglePartInfo>();
        private CancellationTokenSource _cancellationTokenSource;
        private bool IsEnabled { get; set; }

        [Dependency("HaiLu")] public ParameterFactory ParameterFactory { get; set; }
        
        public HaiLuManager(
            ILogger logger,
            IEventAggregator eventAggregator,
            IPartsInfoDAL partsInfoDAL,
            ISinglePartInfoDAL singlePartInfoDAL)
        {
            this._logger = logger;
            this._cancellationTokenSource = new CancellationTokenSource();
            this._partsInfoDAL = partsInfoDAL;
            this._singlePartInfoDAL = singlePartInfoDAL;

            eventAggregator.GetEvent<RequestFlagReadedEvent>().Subscribe(async void () =>
            {
                try
                {
                    await WorkActionAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            },ThreadOption.BackgroundThread);

            eventAggregator.GetEvent<IdentityChangedEvent>().Subscribe(async void (id) =>
            {
                try
                {
                    await ProcessReturnSignalAsync(id);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }, ThreadOption.BackgroundThread);

            LoadDeviceState();
        }

        private void LoadDeviceState()
        {
            var plcStates = ServiceLocator.Current.GetAllInstances<ICommunicationStateMachine>();
            try
            {
                foreach (var plcState in plcStates)
                {
                    if (plcState is PLCState state)
                    {
                        state.ConnectEvent += StateMachine_ConnectEvent;
                        state.DisConnectEvent += StateMachine_DisConnectEvent;
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void StateMachine_DisConnectEvent(object? sender, EventArgs e) => IsEnabled = false;

        private void StateMachine_ConnectEvent(object? sender, EventArgs e) => IsEnabled = true;

        private async Task ProcessReturnSignalAsync(int id)
        {
            try
            {
                var stateinfo = "";
            
                if (ParameterFactory.OffLineFlag == 1)
                {
                    stateinfo = "工件下线";
                    _logger.LogDebug($"产品{id}状态--{stateinfo}");
                    ParameterFactory.OffLineFlag = 0;
                }

                if (ParameterFactory.MeasureOKFlag == 1)
                {
                    stateinfo = "测量正确";
                    _logger.LogDebug($"产品{id}状态--{stateinfo}");
                    ParameterFactory.MeasureOKFlag = 0;
                }

                if (ParameterFactory.MeasureErrorFlag == 1)
                {
                    stateinfo = "测量错误";
                    _logger.LogDebug($"产品{id}状态--{stateinfo}");
                    ParameterFactory.MeasureErrorFlag = 0;
                }
                
                if (_dir.TryRemove(id, out var partInfo)) 
                {
                    partInfo.StateInfo = stateinfo;
                    var result = await _singlePartInfoDAL.UpdateSingleAsync(id, partInfo);
                    if (result == 1)
                    {
                        var partsInfo = await _partsInfoDAL.QueryProduceDataAsync(ParameterFactory.BatchCode!);
                        partsInfo.Countinfo += 1;
                        var rowNumber = await _partsInfoDAL.UpdatePartsInfoAsync(partsInfo);
                        if (rowNumber == 1)
                        {
                            _logger.LogDebug($"序号为{id}的板件处理完成");
                        }
                    }
                    await Task.Run(() =>
                    {
                        ParameterFactory.RequestFlag = 0;
                    });
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
                var result = await _partsInfoDAL.QueryProduceDataAsync(ParameterFactory.BatchCode!);
                if (result != null)
                {
                    var singlePart = new SinglePartInfo()
                    {
                        CountNumber = result.Countinfo + 1,
                        BatchCode = result.BatchCode,
                        Code = result.Code,
                        Name = result.Name,
                        CodeName = result.CodeName,
                        Length = result.Length,
                        Width1 = result.Width1,
                        Thickness = result.Thickness,
                        Remark = result.Remark,
                    };
                    var identity = await _singlePartInfoDAL.InsertSingleAsync(singlePart);
                    await Task.Run(() =>
                    {
                        ParameterFactory.IdentityToPLC = identity;
                        ParameterFactory.Length = result.Length;
                        ParameterFactory.Width = result.Width1;
                        ParameterFactory.Thickness = result.Thickness;
                    });
                    _dir[identity] = singlePart;
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void StartService() =>
            Task.Factory.StartNew(SendReadyFlagToPLC,
                _cancellationTokenSource.Token, 
                TaskCreationOptions.LongRunning, 
                TaskScheduler.Default);

        private async void SendReadyFlagToPLC()
        {
            try
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        if (IsEnabled && ParameterFactory.BatchCode != null)
                        {
                            var result = _partsInfoDAL.QueryProduceDataCount(ParameterFactory.BatchCode);
                            if (result != 0)
                                ParameterFactory.ReadyFlag = 1;
                            else
                                ParameterFactory.ReadyFlag = 0;
                        }
                        await Task.Delay(100, _cancellationTokenSource.Token);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
            }
        }

        public void StopService()
            => _cancellationTokenSource.Cancel();
    }
}
