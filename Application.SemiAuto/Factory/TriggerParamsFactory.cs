using Application.Common;

namespace Application.SemiAuto
{
    public class TriggerParamsFactory : BindableBase
    {
        private readonly TriggerParameterOption _option;

        private readonly IVariable _triggerTimeVariable;
        private readonly IVariable _triggerEnableVariable;
        private readonly IVariable _triggerTimeDelayVariable;

        public IVariable TriggerTimeVariable => _triggerTimeVariable;
        public IVariable TriggerEnableVariable => _triggerEnableVariable;
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

        public bool TriggerEnable
        {
            get => TriggerEnableVariable.GetValueEx<bool>();
            set
            {
                TriggerEnableVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(TriggerEnable));
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
            _triggerTimeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(TriggerTime));
            IO.TryGet(_option.TriggerEnableUri!, out _triggerEnableVariable);
            _triggerEnableVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(TriggerEnable));
            IO.TryGet(_option.TriggerTimeDelayUri!, out _triggerTimeDelayVariable);
            _triggerTimeDelayVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(TriggerTimeDelay));
        }
    }
}
