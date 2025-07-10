using Application.Common;

namespace Application.SemiAuto
{
    public class TriggerParamsFactory : BindableBase
    {
        private System.Threading.AutoResetEvent _TimeresetEvent = new System.Threading.AutoResetEvent(false);
        private System.Threading.AutoResetEvent _TimerDelayesetEvent = new System.Threading.AutoResetEvent(false);

        private readonly TriggerParameterOption _option;
        private CancellationTokenSource _cts;

        private readonly IVariable _triggerTimeVariable;
        private readonly IVariable _triggerTimeDelayVariable;

        public IVariable TriggerTimeVariable => _triggerTimeVariable;
        public IVariable TriggerTimeDelayVariable => _triggerTimeDelayVariable;

        public bool TriggerTime
        {
            get => TriggerTimeVariable.GetValueEx<bool>();
            set
            {
                TriggerTimeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(TriggerTime));
            }
        }

        public bool TriggerTimeDelay
        {
            get => TriggerTimeDelayVariable.GetValueEx<bool>();
            set
            {
                TriggerTimeDelayVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(TriggerTimeDelay));
            }
        }

        public TriggerParamsFactory(TriggerParameterOption option)
        {
            this._option = option;

            IO.TryGet(_option.TriggerTimeUri!, out _triggerTimeVariable);
            _triggerTimeVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _TimeresetEvent.Set(); } };

            IO.TryGet(_option.TriggerTimeDelayUri!, out _triggerTimeDelayVariable);
            _triggerTimeDelayVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _TimerDelayesetEvent.Set(); } };
        }

        public bool SetTimeTrigger()
        {
            try
            {
                _TimeresetEvent.Reset();

                TriggerTime = true;
                if (_TimeresetEvent.WaitOne(10000))
                    return true;
                else
                {
                    TriggerTime = false;
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SetTimeDelayTrigger()
        {
            try
            {
                _TimerDelayesetEvent.Reset();

                TriggerTimeDelay = true;
                if (_TimerDelayesetEvent.WaitOne(10000))
                    return true;
                else
                {
                    TriggerTimeDelay = false;
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
