using Application.IDAL;
using Application.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Application.Hailu
{
    public class HaiLuManager : IManager
    {
        private readonly ParameterFactory _parameterFactory;
        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPartsInfoDAL _partsInfoDAL;
        private readonly ISinglePartInfoDAL _singlePartInfoDAL;
        private ConcurrentDictionary<int, SinglePartInfo> _dir 
            = new ConcurrentDictionary<int, SinglePartInfo>();
        private CancellationTokenSource _cancellationTokenSource;

        public HaiLuManager(ParameterFactory parameterFactory, 
            ILogger logger,
            IEventAggregator eventAggregator,
            IPartsInfoDAL partsInfoDAL,
            ISinglePartInfoDAL singlePartInfoDAL)
        {
            this._parameterFactory = parameterFactory;
            this._logger = logger;
            this._cancellationTokenSource = new CancellationTokenSource();
            this._eventAggregator = eventAggregator;
            this._partsInfoDAL = partsInfoDAL;
            this._singlePartInfoDAL = singlePartInfoDAL;

            eventAggregator.GetEvent<RequestFlagReadedEvent>().Subscribe(async () =>
            {
                await WorkActionAsync();
            });
        }

        private async Task WorkActionAsync()
        {
            var result = await _partsInfoDAL.QueryProduceData(_parameterFactory.BatchCode!);
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
                _parameterFactory.IdentityToPLC = identity;
                _parameterFactory.Length = result.Length;
                _parameterFactory.Width = result.Width1;
                _parameterFactory.Thickness = result.Thickness;
            });
            _dir[identity] = singlePart;
        }

        public void StartService() =>
            Task.Factory.StartNew(SendReadyFlagToPLC,
                _cancellationTokenSource.Token, 
                TaskCreationOptions.LongRunning, 
                TaskScheduler.Default);

        private async void SendReadyFlagToPLC()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    if(_parameterFactory.BatchCode != null)
                    {
                        var result = await _partsInfoDAL.QueryProduceData(_parameterFactory.BatchCode);
                        if(result != null)
                            _parameterFactory.ReadyFlag = 1;
                        else
                            _parameterFactory.ReadyFlag = 0;
                    }
                    await Task.Delay(100, _cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex.Message);
                }
            }
        }

        public void StopService()
            => _cancellationTokenSource?.Cancel();
    }
}
