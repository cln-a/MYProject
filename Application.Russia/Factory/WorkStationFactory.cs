using Application.Common;
using Application.Modbus;
using CommonServiceLocator;
using Microsoft.Extensions.Logging;

namespace Application.Russia
{
    public class WorkStationFactory : BindableBase
    {
        private IVariable _setEnableVariable;
        private IVariable _setTimeVariable;
        private IVariable _setDelayTimeVariable;
        private IVariable _triggerSignalVariable;
        private IVariable _curTimeConsumeVariable;
        private IVariable _triggerTimeConsumeVariable;
        private DelegateCommand _optionCommand;
        private int _id;
        private bool _isConnected;
        private DateTime _recordedTime = DateTime.Now;
        private DateTime _pastTime = DateTime.Now;
        private readonly IDialogService _dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
        private readonly IEventAggregator _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

        [Unity.Dependency] public ILogger Logger { get; set; }  
        public IVariable SetEnableVariable => _setEnableVariable;
        public IVariable SetTimeVariable => _setTimeVariable;
        public IVariable SetDelayTimeVariable => _setDelayTimeVariable;
        public IVariable TriggerSignalVariable => _triggerSignalVariable;
        public IVariable CurTimeConsumeVariable => _curTimeConsumeVariable;
        public IVariable TriggerTimeConsumeVariable => _triggerTimeConsumeVariable;
        public DelegateCommand OptionCommand => _optionCommand ??= new DelegateCommand(SetParameterTrigger);
        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public bool IsConnected { get => _isConnected; set => SetProperty(ref _isConnected, value); }
        public DateTime RecordedTime { get => _recordedTime; set => SetProperty(ref _recordedTime, value); }
        public DateTime PastTime { get => _pastTime; set => SetProperty(ref _pastTime, value); }

        /// <summary>
        /// AutoResetEvent（自动重置事件）是一个线程同步工具,用来控制线程的执行，起到类似门闩的作用，让一个线程等待，直到另一个线程通知它可以继续执行。
        /// </summary>
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        public ushort SetEnable
        {
            get => SetEnableVariable.GetValue<ushort>();
            set
            {
                SetEnableVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(SetEnable));
            }
        }

        public ushort SetTime
        {
            get => SetTimeVariable.GetValue<ushort>();
            set
            {
                SetTimeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(SetTime));
            }
        }

        public ushort SetDelayTime
        {
            get => SetDelayTimeVariable.GetValue<ushort>();
            set
            {
                SetDelayTimeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(SetDelayTime));
            }
        }

        public ushort TriggerSignal
        {
            get => TriggerSignalVariable.GetValue<ushort>();
            set => TriggerSignalVariable.WriteAnyValueEx(value);
        }

        public ushort CurTimeConsume
        {
            get => CurTimeConsumeVariable.GetValue<ushort>();
            set => CurTimeConsumeVariable.WriteAnyValueEx(value);
        }

        public ushort TriggerTimeConsume
        {
            get => TriggerTimeConsumeVariable.GetValue<ushort>();
            set => TriggerTimeConsumeVariable.WriteAnyValueEx(value);
        }

        public WorkStationFactory(int id,
            IVariable setEnableVariable, 
            IVariable setTimeVariable, 
            IVariable setDelayTimeVariable, 
            IVariable triggerSignalVariable,
            IVariable curTimeConsumeVariable,
            IVariable triggerTimeConsumeVariable)
        {
            this._id = id;
            this._setEnableVariable = setEnableVariable;
            this._setTimeVariable = setTimeVariable;
            this._setDelayTimeVariable = setDelayTimeVariable;
            this._triggerSignalVariable = triggerSignalVariable;
            this._curTimeConsumeVariable = curTimeConsumeVariable;
            this._triggerTimeConsumeVariable = triggerTimeConsumeVariable;

            this._triggerSignalVariable.ValueChangedEvent += (s, e) =>
            {
                if (!e.GetNewValue<bool>())
                    _autoResetEvent.Set();
            };
            this._triggerTimeConsumeVariable.ValueChangedEvent += (s, e) =>
            {
                if (e.GetNewValue<bool>())
                    RecordedDuration();
            };

            Initialization();
        }

        private void RecordedDuration()
        {
            RecordedTime = DateTime.Now;
            PastTime = RecordedTime.AddSeconds(-CurTimeConsume);
            TriggerTimeConsume = 0;
        }

        private void Initialization()
        {
            var plcStates = ServiceLocator.Current.GetAllInstances<ICommunicationStateMachine>();
            try
            {
                foreach (var plcState in plcStates) 
                {
                    if (plcState is PLCState state)
                    {
                        state.ConnectEvent += (sender, e) => IsConnected = true;
                        state.DisConnectEvent += (sender, e) => IsConnected = false;
                    }
                }
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex, ex.Message);
            }
        }

        public void SetParameterTrigger()
        {
            try
            {
                _dialogService.Show("DialogView", new DialogParameters
                {
                    { "Title", $"是否启用工位{Id}，设定耗时时间为{SetTime},设定延时时间为{SetDelayTime}?" }
                }, 
                result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        Task.Run(() =>
                        {
                            _autoResetEvent.Reset();
                            TriggerSignal = 1;
                            if (_autoResetEvent.WaitOne(10000))
                            {
                                _eventAggregator.GetEvent<DialogMessageEvent>()
                                .Publish($"工位{Id}已启用,耗时时间{SetTime}设定成功,延时时间{SetDelayTime}设定成功");
                            }
                            else
                            {
                                TriggerSignal = 0;
                                _eventAggregator.GetEvent<DialogMessageEvent>().Publish($"系统失去响应，请重新设定");
                            }
                        });
                    }
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
