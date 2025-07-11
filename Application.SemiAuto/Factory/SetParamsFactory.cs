using Application.Common;

namespace Application.SemiAuto
{
    public class SetParamsFactory : BindableBase
    {
        private readonly SetParameterOption _option;
        private readonly IEventAggregator _eventAggregator;

        private readonly IVariable _setEnableOneVariable;
        private readonly IVariable _SetEnableTwoVariable;
        private readonly IVariable _setEnableThreeVariable;
        private readonly IVariable _setEnableFourVariable;
        private readonly IVariable _setEnableFiveVariable;
        private readonly IVariable _setEnableSixVariable;
        private readonly IVariable _setEnableSevenVariable;
        private readonly IVariable _setEnableEightVariable;
        private readonly IVariable _setEnableNineVariable;
        private readonly IVariable _setEnableTenVariable;
        private readonly IVariable _setEnableElevenVariable;

        private readonly IVariable _setTimeVariable;
        private readonly IVariable _setTimeDelayVariable;

        public IEventAggregator EventAggregator => _eventAggregator;

        public IVariable SetEnableOneVariable => _setEnableOneVariable;
        public IVariable SetEnableTwoVariable => _SetEnableTwoVariable;
        public IVariable SetEnableThreeVariable => _setEnableThreeVariable;
        public IVariable SetEnableFourVariable => _setEnableFourVariable;
        public IVariable SetEnableFiveVariable => _setEnableFiveVariable;
        public IVariable SetEnableSixVariable => _setEnableSixVariable;
        public IVariable SetEnableSevenVariable => _setEnableSevenVariable;
        public IVariable SetEnableEightVariable => _setEnableEightVariable;
        public IVariable SetEnableNineVariable => _setEnableNineVariable;
        public IVariable SetEnableTenVariable => _setEnableTenVariable;
        public IVariable SetEnableElevenVariable => _setEnableElevenVariable;

        public IVariable SetTimeVariable => _setTimeVariable;
        public IVariable SetTimeDelayVariable => _setTimeDelayVariable;

        public ushort SetEnableOne
        {
            get => SetEnableOneVariable.GetValueEx<ushort>();
            set => SetEnableOneVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableTwo
        {
            get => SetEnableTwoVariable.GetValueEx<ushort>(); 
            set => SetEnableTwoVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableThree
        {
            get => SetEnableThreeVariable.GetValueEx<ushort>();
            set => SetEnableThreeVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableFour
        {
            get => SetEnableFourVariable.GetValueEx<ushort>();
            set => SetEnableFourVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableFive
        {
            get => SetEnableFiveVariable.GetValueEx<ushort>();
            set => SetEnableFiveVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableSix
        {
            get => SetEnableSixVariable.GetValueEx<ushort>();
            set => SetEnableSixVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableSeven
        {
            get => SetEnableSevenVariable.GetValueEx<ushort>();
            set => SetEnableSevenVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableEight
        {
            get => SetEnableEightVariable.GetValueEx<ushort>();
            set => SetEnableEightVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableNine
        {
            get => SetEnableNineVariable.GetValueEx<ushort>();
            set => SetEnableNineVariable.WriteAnyValueEx(value);
        }
        
        public ushort SetEnableTen
        {
            get => SetEnableTenVariable.GetValueEx<ushort>();
            set => SetEnableTenVariable.WriteAnyValueEx(value);
        }

        public ushort SetEnableEleven
        {
            get => SetEnableElevenVariable.GetValueEx<ushort>();
            set => SetEnableElevenVariable.WriteAnyValueEx(value);
        }

        public float SetTime
        {
            get => SetTimeVariable.GetValueEx<float>();
            set => SetTimeVariable.WriteAnyValueEx(value);
        }

        public float SetTimeDelay
        {
            get => _setTimeDelayVariable.GetValueEx<float>();
            set => _setTimeDelayVariable.WriteAnyValueEx(value);
        }

        public SetParamsFactory(SetParameterOption option, IEventAggregator eventAggregator)
        {
            _option = option;
            this._eventAggregator = eventAggregator;

            IO.TryGet(_option.SetEnableOneUri!, out _setEnableOneVariable);
            SetEnableOneVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableOne));
            IO.TryGet(_option.SetEnableTwoUri!, out _SetEnableTwoVariable);
            SetEnableTwoVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableTwo));
            IO.TryGet(_option.SetEnableThreeUri!, out _setEnableThreeVariable);
            SetEnableThreeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableThree));
            IO.TryGet(_option.SetEnableFourUri!, out _setEnableFourVariable);
            SetEnableFourVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableFour));
            IO.TryGet(_option.SetEnableFiveUri!, out _setEnableFiveVariable);
            SetEnableFiveVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableFive));
            IO.TryGet(_option.SetEnableSixUri!, out _setEnableSixVariable);
            SetEnableSixVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableSix));
            IO.TryGet(_option.SetEnableSevenUri!, out _setEnableSevenVariable);
            SetEnableSevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableSeven));
            IO.TryGet(_option.SetEnableEightUri!, out _setEnableEightVariable);
            SetEnableEightVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableEight));
            IO.TryGet(_option.SetEnableNineUri!, out _setEnableNineVariable);
            SetEnableNineVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableNine));
            IO.TryGet(_option.SetEnableTenUri!, out _setEnableTenVariable);
            SetEnableTenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableTen));
            IO.TryGet(_option.SetEnableElevenUri!, out _setEnableElevenVariable);
            SetEnableElevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableEleven));

            IO.TryGet(_option.SetTimeUri!, out _setTimeVariable);
            IO.TryGet(_option.SetTimeDelayUri!, out _setTimeDelayVariable);

            EventAggregator.GetEvent<SetTimeEvent>().Subscribe(SetTimeChange);
            EventAggregator.GetEvent<SetTimeDelayEvent>().Subscribe(SetTimeDelayChange);
        }

        private void SetTimeChange() => RaisePropertyChanged(nameof(SetTime));

        private void SetTimeDelayChange() => RaisePropertyChanged(nameof(SetTimeDelay));
    }
}
