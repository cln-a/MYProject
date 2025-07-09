using Application.Common;
using System.Reflection;

namespace Application.SemiAuto
{
    public class SetParamsFactory : BindableBase
    {
        private readonly SetParameterOption _option;

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

        private readonly IVariable _setDelayOneVariable;
        private readonly IVariable _setDelayTwoVariable;
        private readonly IVariable _setDelayThreeVariable;
        private readonly IVariable _setDelayFourVariable;
        private readonly IVariable _setDelayFiveVariable;
        private readonly IVariable _setDelaySixVariable;
        private readonly IVariable _setDelaySevenVariable;
        private readonly IVariable _setDelayEightVariable;
        private readonly IVariable _setDelayNineVariable;
        private readonly IVariable _setDelayTenVariable;
        private readonly IVariable _setDelayElevenVariable;

        private readonly IVariable _setTimeVariable;

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

        public IVariable SetDelayOneVariable => _setDelayOneVariable;
        public IVariable SetDelayTwoVariable => _setDelayTwoVariable;
        public IVariable SetDelayThreeVariable => _setDelayThreeVariable;
        public IVariable SetDelayFourVariable => _setDelayFourVariable;
        public IVariable SetDelayFiveVariable => _setDelayFiveVariable;
        public IVariable SetDelaySixVariable => _setDelaySixVariable;
        public IVariable SetDelaySevenVariable => _setDelaySevenVariable;
        public IVariable SetDelayEightVariable => _setDelayEightVariable;
        public IVariable SetDelayNineVariable => _setDelayNineVariable;
        public IVariable SetDelayTenVariable => _setDelayTenVariable;
        public IVariable SetDelayElevenVariable => _setDelayElevenVariable;

        public IVariable SetTimeVariable => _setTimeVariable;

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
            set => SetEnableEightVariable.WriteAnyValueEx(value);
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

        public float SetDelayOne
        {
            get => SetDelayOneVariable.GetValueEx<float>();
            set => SetDelayOneVariable.WriteAnyValueEx(value);
        }

        public float SetDelayTwo
        {
            get => SetDelayTwoVariable.GetValueEx<float>();
            set => SetDelayTwoVariable.WriteAnyValueEx(value);
        }

        public float SetDelayThree
        {
            get => SetDelayThreeVariable.GetValueEx<float>();
            set => SetDelayThreeVariable.WriteAnyValueEx(value);
        }

        public float SetDelayFour
        {
            get => SetDelayFourVariable.GetValueEx<float>();
            set => SetDelayFourVariable.WriteAnyValueEx(value);
        }

        public float SetDelayFive
        {
            get => SetDelayFiveVariable.GetValueEx<float>();
            set => SetDelayFiveVariable.WriteAnyValueEx(value);
        }

        public float SetDelaySix
        {
            get => SetDelaySixVariable.GetValueEx<float>();
            set => SetDelaySixVariable.WriteAnyValueEx(value);
        }

        public float SetDelaySeven
        {
            get => SetDelaySevenVariable.GetValueEx<float>();
            set => SetDelaySevenVariable.WriteAnyValueEx(value);
        }

        public float SetDelayEight
        {
            get => SetDelayEightVariable.GetValueEx<float>();
            set => SetDelayEightVariable.WriteAnyValueEx(value);
        }

        public float SetDelayNine
        {
            get => SetDelayNineVariable.GetValueEx<float>();
            set => SetDelayNineVariable.WriteAnyValueEx(value);
        }

        public float SetDelayTen
        {
            get => SetDelayTenVariable.GetValueEx<float>();
            set => SetDelayTenVariable.WriteAnyValueEx(value);
        }

        public float SetDelayEleven
        {
            get => SetDelayElevenVariable.GetValueEx<float>();
            set => SetDelayElevenVariable.WriteAnyValueEx(value);
        }

        public float SetTime
        {
            get => SetTimeVariable.GetValueEx<float>();
            set => SetTimeVariable.WriteAnyValueEx(value);
        }

        public SetParamsFactory(SetParameterOption option)
        {
            _option = option;

            IO.TryGet(_option.SetEnableOneUri!, out _setEnableOneVariable);
            _setEnableOneVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableOne));
            IO.TryGet(_option.SetEnableTwoUri!, out _SetEnableTwoVariable);
            _SetEnableTwoVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableTwo));
            IO.TryGet(_option.SetEnableThreeUri!, out _setEnableThreeVariable);
            _setEnableThreeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableThree));
            IO.TryGet(_option.SetEnableFourUri!, out _setEnableFourVariable);
            _setEnableFourVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableFour));
            IO.TryGet(_option.SetEnableFiveUri!, out _setEnableFiveVariable);
            _setEnableFiveVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableFive));
            IO.TryGet(_option.SetEnableSixUri!, out _setEnableSixVariable);
            _setEnableSixVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableSix));
            IO.TryGet(_option.SetEnableSevenUri!, out _setEnableSevenVariable);
            _setEnableSevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableSeven));
            IO.TryGet(_option.SetEnableEightUri!, out _setEnableEightVariable);
            _setEnableEightVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableEight));
            IO.TryGet(_option.SetEnableNineUri!, out _setEnableNineVariable);
            _setEnableNineVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableNine));
            IO.TryGet(_option.SetEnableTenUri!, out _setEnableTenVariable);
            _setEnableTenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableTen));
            IO.TryGet(/*_option.SetEnableElevenUri!*/"SetEnableEleven", out _setEnableElevenVariable);
            _setEnableElevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetEnableEleven));

            IO.TryGet(_option.SetDelayOneUri!, out _setDelayOneVariable);
            _setDelayOneVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayOne));
            IO.TryGet(_option.SetDelayTwoUri!, out _setDelayTwoVariable);
            _setDelayTwoVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayTwo));
            IO.TryGet(_option.SetDelayThreeUri!, out _setDelayThreeVariable);
            _setDelayThreeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayThree));
            IO.TryGet(_option.SetDelayFourUri!, out _setDelayFourVariable);
            _setDelayFourVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayFour));
            IO.TryGet(_option.SetDelayFiveUri!, out _setDelayFiveVariable);
            _setDelayFiveVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayFive));
            IO.TryGet(_option.SetDelaySixUri!, out _setDelaySixVariable);
            _setDelaySixVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelaySix));
            IO.TryGet(_option.SetDelaySevenUri!, out _setDelaySevenVariable);
            _setDelaySevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelaySeven));
            IO.TryGet(_option.SetDelayEightUri!, out _setDelayEightVariable);
            _setDelayEightVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayEight));
            IO.TryGet(_option.SetDelayNineUri!, out _setDelayNineVariable);
            _setDelayNineVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayNine));
            IO.TryGet(_option.SetDelayTenUri!, out _setDelayTenVariable);
            _setDelayTenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayTen));
            IO.TryGet(_option.SetDelayElevenUri!, out _setDelayElevenVariable);
            _setDelayElevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetDelayEleven));

            IO.TryGet(_option.SetTimeUri!, out _setTimeVariable);
            _setTimeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(SetTime));
        }
    }
}
