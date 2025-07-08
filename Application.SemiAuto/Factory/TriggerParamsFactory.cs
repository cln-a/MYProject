using Application.Common;

namespace Application.SemiAuto
{
    public class TriggerParamsFactory : BindableBase
    {
        private readonly TriggerParameterOption _option;

        private readonly IVariable _triggerTimeVariable;
        private readonly IVariable _triggerTimeConsumeVariable;
        private readonly IVariable _triggerTimeDelayVariable;

        public IVariable TriggerTimeVariable => _triggerTimeVariable;
        public IVariable TriggerTimeConsumeVariable => _triggerTimeConsumeVariable;
        public IVariable TriggerTimeDelayVariable => _triggerTimeDelayVariable;

        public ushort TriggerTime
        {
            get => TriggerTimeVariable.GetValueEx<ushort>();
            set
            {
                TriggerTimeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(TriggerTime));
            }
        }

        public ushort TriggerTimeConsume
        {
            get => TriggerTimeConsumeVariable.GetValueEx<ushort>();
            set
            {
                TriggerTimeConsumeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(TriggerTimeConsume));
            }
        }

        public ushort TriggerTimeDelay
        {
            get => TriggerTimeDelayVariable.GetValueEx<ushort>();
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
            IO.TryGet(_option.TriggerTimeConsumeUri!, out _triggerTimeConsumeVariable);
            IO.TryGet(_option.TriggerTimeDelayUri!, out _triggerTimeDelayVariable);
        }
    }
}
