using Application.Common;

namespace Application.Russia
{
    public class WorkStationFactory : BindableBase
    {
        private IVariable _setEnableVariable;
        private IVariable _setTimeVariable;
        private IVariable _setDelayTimeVariable;
        private IVariable _triggerSignalVariable;

        public IVariable SetEnableVariable => _setEnableVariable;
        public IVariable SetTimeVariable => _setTimeVariable;
        public IVariable SetDelayTimeVariable => _setDelayTimeVariable;
        public IVariable TriggerSignalVariable => _triggerSignalVariable;

        /// <summary>
        /// AutoResetEvent（自动重置事件）是一个线程同步工具,用来控制线程的执行，起到类似门闩的作用，让一个线程等待，直到另一个线程通知它可以继续执行。
        /// </summary>
        private AutoResetEvent _autoResetEvent;

        public ushort SetTimeEnable
        {
            get => SetEnableVariable.GetValue<ushort>();
            set => SetEnableVariable.WriteAnyValueEx(value);
        }

        public ushort SetTime
        {
            get => SetTimeVariable.GetValue<ushort>();
            set => SetTimeVariable.WriteAnyValueEx(value);
        }

        public ushort SetDelayTime
        {
            get => SetDelayTimeVariable.GetValue<ushort>();
            set => SetDelayTimeVariable.WriteAnyValueEx(value);
        }

        public ushort TriggerSignal
        {
            get => TriggerSignalVariable.GetValue<ushort>();
            set => TriggerSignalVariable.WriteAnyValueEx(value);
        }

        public WorkStationFactory(IVariable setEnableVariable, 
            IVariable setTimeVariable, 
            IVariable setDelayTimeVariable, 
            IVariable triggerSignalVariable)
        {
            this._setEnableVariable = setEnableVariable;
            this._setTimeVariable = setTimeVariable;
            this._setDelayTimeVariable = setDelayTimeVariable;
            this._triggerSignalVariable = triggerSignalVariable;

            _autoResetEvent = new AutoResetEvent(false);

            this._triggerSignalVariable.ValueChangedEvent += (s, e) =>
            {
                if (!e.GetNewValue<bool>())
                    _autoResetEvent.Set();
            };
        }

        public bool SetParameterTrigger()
        {
            try
            {
                _autoResetEvent.Reset();
                TriggerSignal = 1;
                if (_autoResetEvent.WaitOne(10000))
                    return true;
                else
                {
                    TriggerSignal = 0;
                    return false;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
