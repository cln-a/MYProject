using Application.Common;

namespace Application.SemiAuto
{
    public class CurParamsFactory : BindableBase
    {
        private readonly CurParameterOption _option;

        private readonly IVariable _curTimeConsumeOneVariable;
        private readonly IVariable _curTimeConsumeTwoVariable;
        private readonly IVariable _curTimeConsumeThreeVariale;
        private readonly IVariable _curTimeConsumeFourVariable;
        private readonly IVariable _curTimeConsumeFiveVariable;
        private readonly IVariable _curTimeConsumeSixVariable;
        private readonly IVariable _curTimeConsumeSevenVariable;
        private readonly IVariable _curTimeConsumeEightVariable;
        private readonly IVariable _curTimeConsumeNineVariable;
        private readonly IVariable _curTimeConsumeTenVariable;
        private readonly IVariable _curTimeConsumeElevenVariable;

        private readonly IVariable _curDelayOneVariable;
        private readonly IVariable _curDelayTwoVariable;
        private readonly IVariable _curDelayThreeVariable;
        private readonly IVariable _curDelayFourVariable;
        private readonly IVariable _curDelayFiveVariable;
        private readonly IVariable _curDelaySixVariable;
        private readonly IVariable _curDelaySevenVariable;
        private readonly IVariable _curDelayEightVariable;
        private readonly IVariable _curDelayNineVariable;
        private readonly IVariable _curDelayTenVariable;
        private readonly IVariable _curDelayElevenVariable;

        private readonly IVariable _curTimeVariable;

        public IVariable CurTimeConsumeOneVariable => _curTimeConsumeOneVariable;
        public IVariable CurTimeConsumeTwoVariable => _curTimeConsumeTwoVariable;
        public IVariable CurTimeConsumeThreeVariale => _curTimeConsumeThreeVariale;
        public IVariable CurTimeConsumeFourVariable => _curTimeConsumeFourVariable;
        public IVariable CurTimeConsumeFiveVariable => _curTimeConsumeFiveVariable;
        public IVariable CurTimeConsumeSixVariable => _curTimeConsumeSixVariable;
        public IVariable CurTimeConsumeSevenVariable => _curTimeConsumeSevenVariable;
        public IVariable CurTimeConsumeEightVariable => _curTimeConsumeEightVariable;
        public IVariable CurTimeConsumeNineVariable => _curTimeConsumeNineVariable;
        public IVariable CurTimeConsumeTenVariable => _curTimeConsumeTenVariable;
        public IVariable CurTimeConsumeElevenVariable => _curTimeConsumeElevenVariable;

        public IVariable CurDelayOneVariable => _curDelayOneVariable;
        public IVariable CurDelayTwoVariable => _curDelayTwoVariable;
        public IVariable CurDelayThreeVariable => _curDelayThreeVariable;
        public IVariable CurDelayFourVariable => _curDelayFourVariable;
        public IVariable CurDelayFiveVariable => _curDelayFiveVariable;
        public IVariable CurDelaySixVariable => _curDelaySixVariable;
        public IVariable CurDelaySevenVariable => _curDelaySevenVariable;
        public IVariable CurDelayEightVariable => _curDelayEightVariable;
        public IVariable CurDelayNineVariable => _curDelayNineVariable;
        public IVariable CurDelayTenVariable => _curDelayTenVariable;
        public IVariable CurDelayElevenVariable => _curDelayElevenVariable;

        public IVariable CurTimeVariable => _curTimeVariable;

        public float CurTimeConsumeOne
        {
            get => CurTimeConsumeOneVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeOneVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeOne));
            }
        } 

        public float CurTimeConsumeTwo
        {
            get => CurTimeConsumeTwoVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeTwoVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeTwo));
            }
            
        }

        public float CurTimeConsumeThree
        {
            get => CurTimeConsumeThreeVariale.GetValueEx<float>();
            set
            {
                CurTimeConsumeThreeVariale.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeThree));
            }

        }

        public float CurTimeConsumeFour
        {
            get => CurTimeConsumeFourVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeFourVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeFour));
            }
        }

        public float CurTimeConsumeFive
        {
            get => CurTimeConsumeFiveVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeFiveVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeFive));
            }
        }

        public float CurTimeConsumeSix
        {
            get => CurTimeConsumeSixVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeSixVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeSix));
            }
        }

        public float CurTimeConsumeSeven
        {
            get => CurTimeConsumeSevenVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeSevenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeSeven));
            }
        }

        public float CurTimeConsumeEight
        {
            get => CurTimeConsumeEightVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeEightVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeEight));
            }
        }

        public float CurTimeConsumeNine
        {
            get => CurTimeConsumeNineVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeNineVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeNine));
            }
        }

        public float CurTimeConsumeTen
        {
            get => CurTimeConsumeTenVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeTenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeTen));
            }
        }

        public float CurTimeConsumeEleven
        {
            get => CurTimeConsumeElevenVariable.GetValueEx<float>();
            set
            {
                CurTimeConsumeElevenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeConsumeEleven));
            }
           
        }

        public float CurDelayOne
        {
            get => CurDelayOneVariable.GetValueEx<float>();
            set
            {
                CurDelayOneVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayOne));
            }
        }

        public float CurDelayTwo
        {
            get => CurDelayTwoVariable.GetValueEx<float>();
            set
            {
                CurDelayTwoVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayTwo));
            }
        }

        public float CurDelayThree
        {
            get => CurDelayThreeVariable.GetValueEx<float>();
            set
            {
                CurDelayThreeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayThree));
            }
        }

        public float CurDelayFour
        {
            get => CurDelayFourVariable.GetValueEx<float>();
            set
            {
                CurDelayFourVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayFour));
            }
            
        }

        public float CurDelayFive
        {
            get => CurDelayFiveVariable.GetValueEx<float>();
            set
            {
                CurDelayFiveVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayFive));
            }
        }

        public float CurDelaySix
        {
            get => CurDelaySixVariable.GetValueEx<float>();
            set
            {
                CurDelaySixVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelaySix));
            }
        }

        public float CurDelaySeven
        {
            get => CurDelaySevenVariable.GetValueEx<float>();
            set
            {
                CurDelaySevenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelaySeven));
            }
        }

        public float CurDelayEight
        {
            get => CurDelayEightVariable.GetValueEx<float>();
            set
            {
                CurDelayEightVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayEight));
            }
        }

        public float CurDelayNine
        {
            get => CurDelayNineVariable.GetValueEx<float>();
            set
            {
                CurDelayNineVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayNine));
            }
        }

        public float CurDelayTen
        {
            get => CurDelayTenVariable.GetValueEx<float>();
            set
            {
                CurDelayTenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayTen));
            }
        }

        public float CurDelayEleven
        {
            get => CurDelayElevenVariable.GetValueEx<float>();
            set
            {
                CurDelayElevenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurDelayEleven));
            }
        }

        public float CurTime
        {
            get => CurTimeVariable.GetValueEx<float>();
            set
            {
                CurTimeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTime));
            }
        }

        public CurParamsFactory(CurParameterOption option)
        {
            this._option = option;

            IO.TryGet(_option.CurTimeConsumeOneUri!, out _curTimeConsumeOneVariable);
            _curTimeConsumeOneVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTimeConsumeOne));
            IO.TryGet(_option.CurTimeConsumeTwoUri!, out _curTimeConsumeTwoVariable);
            _curTimeConsumeTwoVariable.ValueChangedEvent += (s,e) => RaisePropertyChanged(nameof(CurTimeConsumeTwo));
            IO.TryGet(_option.CurTimeConsumeThreeUri!,out _curTimeConsumeThreeVariale);
            _curTimeConsumeThreeVariale.ValueChangedEvent += (s,e) => RaisePropertyChanged(nameof(CurTimeConsumeThree));
            IO.TryGet(_option.CurTimeConsumeFourUri!, out _curTimeConsumeFourVariable);
            _curTimeConsumeFourVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTimeConsumeFour));
            IO.TryGet(_option.CurTimeConsumeFiveUri!, out _curTimeConsumeFiveVariable);
            _curTimeConsumeFiveVariable.ValueChangedEvent += (s,e) => RaisePropertyChanged(nameof(CurTimeConsumeFive));
            IO.TryGet(_option.CurTimeConsumeSixUri!, out _curTimeConsumeSixVariable);
            _curTimeConsumeSixVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTimeConsumeSix));
            IO.TryGet(_option.CurTimeConsumeSevenUri!,out _curTimeConsumeSevenVariable);
            _curTimeConsumeSevenVariable.ValueChangedEvent += (s,e) => RaisePropertyChanged(nameof(CurTimeConsumeSeven));
            IO.TryGet(_option.CurTimeConsumeEightUri!,out _curTimeConsumeEightVariable);
            _curTimeConsumeEightVariable.ValueChangedEvent += (s,e) => RaisePropertyChanged(nameof(CurTimeConsumeEight));
            IO.TryGet(_option.CurTimeConsumeNineUri!, out _curTimeConsumeNineVariable);
            _curTimeConsumeNineVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTimeConsumeNine));
            IO.TryGet(_option.CurTimeConsumeTenUri!, out _curTimeConsumeTenVariable);
            _curTimeConsumeTenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTimeConsumeTen));
            IO.TryGet(/*_option.CurTimeConsumeElevenUri!*/"CurTimeConsumeEleven", out _curTimeConsumeElevenVariable);
            _curTimeConsumeElevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTimeConsumeEleven));

            IO.TryGet(_option.CurDelayOneUri!,out _curDelayOneVariable);
            _curDelayOneVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayOne));
            IO.TryGet(_option.CurDelayTwoUri!,out _curDelayTwoVariable);
            _curDelayTwoVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayTwo));
            IO.TryGet(_option.CurDelayThreeUri!,out _curDelayThreeVariable);
            _curDelayThreeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayThree));
            IO.TryGet(_option.CurDelayFourUri!,out _curDelayFourVariable);
            _curDelayFourVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayFour));
            IO.TryGet(_option.CurDelayFiveUri!,out _curDelayFiveVariable);
            _curDelayFiveVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayFive));
            IO.TryGet(_option.CurDelaySixUri!,out _curDelaySixVariable);
            _curDelaySixVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelaySix));
            IO.TryGet(_option.CurDelaySevenUri!,out _curDelaySevenVariable);
            _curDelaySevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelaySeven));
            IO.TryGet(_option.CurDelayEightUri!,out _curDelayEightVariable);
            _curDelayEightVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayEight));
            IO.TryGet(_option.CurDelayNineUri!,out _curDelayNineVariable);
            _curDelayNineVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayNine));
            IO.TryGet(_option.CurDelayTenUri!,out _curDelayNineVariable);
            _curDelayNineVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayTen));
            IO.TryGet(_option.CurDelayElevenUri!, out _curDelayElevenVariable);
            _curDelayElevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurDelayEleven));

            IO.TryGet(_option.CurTimeUri!,out _curTimeVariable);
            _curTimeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTime));
        }
    }
}
