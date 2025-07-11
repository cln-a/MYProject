using Application.Common;

namespace Application.SemiAuto
{
    public class SetParamsFactory : BindableBase
    {
        private readonly SetParameterOption _option;
        private readonly IEventAggregator _eventAggregator;

        private AutoResetEvent _oneresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _tworesetEvent = new AutoResetEvent(false);
        private AutoResetEvent _threeresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _fourresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _fiveresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _sixresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _sevenresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _eightresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _nineresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _tenresetEvent = new AutoResetEvent(false);
        private AutoResetEvent _elevenresetEvent = new AutoResetEvent(false);

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

        private readonly IVariable _triggerEnableOneVariable;
        private readonly IVariable _triggerEnableTwoVariable;
        private readonly IVariable _triggerEnableThreeVariable;
        private readonly IVariable _triggerEnableFourVariable;
        private readonly IVariable _triggerEnableFiveVariable;
        private readonly IVariable _triggerEnableSixVariable;
        private readonly IVariable _triggerEnableSevenVariable;
        private readonly IVariable _triggerEnableEightVariable;
        private readonly IVariable _triggerEnableNineVariable;
        private readonly IVariable _triggerEnableTenVariable;
        private readonly IVariable _triggerEnableElevenVariable;

        private ushort _setEnableOneProxy;

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

        public IVariable TriggerEnableOneVariable => _triggerEnableOneVariable;
        public IVariable TriggerEnableTwoVariable => _triggerEnableTwoVariable;
        public IVariable TriggerEnableThreeVariable => _triggerEnableThreeVariable;
        public IVariable TriggerEnableFourVariable => _triggerEnableFourVariable;
        public IVariable TriggerEnableFiveVariable => _triggerEnableFiveVariable;
        public IVariable TriggerEnableSixVariable => _triggerEnableSixVariable;
        public IVariable TriggerEnableSevenVariable => _triggerEnableSevenVariable;
        public IVariable TriggerEnableEightVariable => _triggerEnableEightVariable;
        public IVariable TriggerEnableNineVariable => _triggerEnableNineVariable;
        public IVariable TriggerEnableTenVariable => _triggerEnableTenVariable;
        public IVariable TriggerEnableElevenVariable => _triggerEnableElevenVariable;


        public bool TriggerEnableOne
        {
            get => TriggerEnableOneVariable.GetValueEx<bool>();
            set => TriggerEnableOneVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableTwo
        {
            get => TriggerEnableTwoVariable.GetValueEx<bool>();
            set => TriggerEnableTwoVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableThree
        {
            get => TriggerEnableThreeVariable.GetValueEx<bool>();
            set => TriggerEnableThreeVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableFour
        {
            get => TriggerEnableFourVariable.GetValueEx<bool>();
            set => TriggerEnableFourVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableFive
        {
            get => TriggerEnableFourVariable.GetValueEx<bool>();
            set => TriggerEnableFourVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableSix
        {
            get => TriggerEnableSixVariable.GetValueEx<bool>();
            set => TriggerEnableSixVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableSeven
        {
            get => TriggerEnableSevenVariable.GetValueEx<bool>();
            set => TriggerEnableSevenVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableEight
        {
            get => TriggerEnableEightVariable.GetValueEx<bool>();
            set => TriggerEnableEightVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableNine
        {
            get => TriggerEnableNineVariable.GetValueEx<bool>();
            set => TriggerEnableNineVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableTen
        {
            get => TriggerEnableTenVariable.GetValueEx<bool>(); 
            set => TriggerEnableTenVariable.WriteAnyValueEx(value);
        }

        public bool TriggerEnableEleven
        {
            get => TriggerEnableElevenVariable.GetValueEx<bool>(); 
            set => TriggerEnableElevenVariable.WriteAnyValueEx(value);
        }

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
            IO.TryGet(_option.SetEnableTwoUri!, out _SetEnableTwoVariable);
            IO.TryGet(_option.SetEnableThreeUri!, out _setEnableThreeVariable);
            IO.TryGet(_option.SetEnableFourUri!, out _setEnableFourVariable);
            IO.TryGet(_option.SetEnableFiveUri!, out _setEnableFiveVariable);
            IO.TryGet(_option.SetEnableSixUri!, out _setEnableSixVariable);
            IO.TryGet(_option.SetEnableSevenUri!, out _setEnableSevenVariable);
            IO.TryGet(_option.SetEnableEightUri!, out _setEnableEightVariable);
            IO.TryGet(_option.SetEnableNineUri!, out _setEnableNineVariable);
            IO.TryGet(_option.SetEnableTenUri!, out _setEnableTenVariable);
            IO.TryGet(_option.SetEnableElevenUri!, out _setEnableElevenVariable);

            IO.TryGet(_option.SetTimeUri!, out _setTimeVariable);
            IO.TryGet(_option.SetTimeDelayUri!, out _setTimeDelayVariable);

            IO.TryGet(_option.TriggerEnableOneUri!,out _triggerEnableOneVariable);
            TriggerEnableOneVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _oneresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableTwoUri!, out _triggerEnableTwoVariable);
            TriggerEnableTwoVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _tworesetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableThreeUri!, out _triggerEnableThreeVariable);
            TriggerEnableThreeVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _threeresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableFourUri!, out _triggerEnableFourVariable);
            TriggerEnableFourVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _fourresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableFiveUri!, out _triggerEnableFiveVariable);
            TriggerEnableFiveVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _fiveresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableSixUri!, out _triggerEnableSixVariable);
            TriggerEnableSixVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _sixresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableSevenUri!, out _triggerEnableSevenVariable);
            TriggerEnableSevenVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _sevenresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableEightUri!, out _triggerEnableEightVariable);
            TriggerEnableEightVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _eightresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableNineUri!, out _triggerEnableNineVariable);
            TriggerEnableNineVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _nineresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableTenUri!, out _triggerEnableTenVariable);
            TriggerEnableTenVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _tenresetEvent.Set(); } };
            IO.TryGet(_option.TriggerEnableElevenUri!, out _triggerEnableElevenVariable);
            TriggerEnableElevenVariable.ValueChangedEvent += (s, e) => { if (!e.GetNewValue<bool>()) { _elevenresetEvent.Set(); } };

            EventAggregator.GetEvent<SetTimeEvent>().Subscribe(SetTimeChange);
            EventAggregator.GetEvent<SetTimeDelayEvent>().Subscribe(SetTimeDelayChange);
        }

        private void SetTimeChange() => RaisePropertyChanged(nameof(SetTime));

        private void SetTimeDelayChange() => RaisePropertyChanged(nameof(SetTimeDelay));
    }
}
