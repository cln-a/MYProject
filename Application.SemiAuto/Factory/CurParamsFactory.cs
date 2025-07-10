using Application.Common;
using System.Collections.ObjectModel;

namespace Application.SemiAuto
{
    public class CurParamsFactory : BindableBase
    {
        private readonly CurParameterOption _option;
        private ObservableCollection<CurTimeDisplayItem> curTimeDisplayItems 
            = new ObservableCollection<CurTimeDisplayItem>();

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
        private readonly IVariable _curTimeDelayVariable;

        private readonly IVariable _curEnableOneVariable;
        private readonly IVariable _curEnableTwoVariable;
        private readonly IVariable _curEnableThreeVariable;
        private readonly IVariable _curEnableFourVariable;
        private readonly IVariable _curEnableFiveVariable;
        private readonly IVariable _curEnableSixVariable;
        private readonly IVariable _curEnableSevenVariable;
        private readonly IVariable _curEnableEightVariable;
        private readonly IVariable _curEnableNineVariable;
        private readonly IVariable _curEnableTenVariable;
        private readonly IVariable _curEnableElevenVariable;

        public ObservableCollection<CurTimeDisplayItem> CurTimeDisplayItems => curTimeDisplayItems;

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
        public IVariable CurTimeDelayVariable => _curTimeDelayVariable;

        public IVariable CurEnableOneVariable => _curEnableOneVariable;
        public IVariable CurEnableTwoVariable => _curEnableTwoVariable;
        public IVariable CurEnableThreeVariable => _curEnableThreeVariable;
        public IVariable CurEnableFourVariable => _curEnableFourVariable;
        public IVariable CurEnableFiveVariable => _curEnableFiveVariable;
        public IVariable CurEnableSixVariable => _curEnableSixVariable;
        public IVariable CurEnableSevenVariable => _curEnableSevenVariable;
        public IVariable CurEnableEightVariable => _curEnableEightVariable;
        public IVariable CurEnableNineVariable => _curEnableNineVariable;
        public IVariable CurEnableTenVariable => _curEnableTenVariable;
        public IVariable CurEnableElevenVariable => _curEnableElevenVariable;

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

        public ushort CurEnableOne
        {
            get => CurEnableOneVariable.GetValueEx<ushort>();
            set
            {
                CurEnableOneVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableOne));
            }
        }

        public ushort CurEnableTwo
        {
            get => CurEnableTwoVariable.GetValueEx<ushort>();
            set
            {
                CurEnableTwoVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableTwo));
            }
        }

        public ushort CurEnableThree
        {
            get => CurEnableThreeVariable.GetValueEx<ushort>(); set
            {
                CurEnableThreeVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableThree));   
            }
        }

        public ushort CurEnableFour
        {
            get => CurEnableFourVariable.GetValueEx<ushort>();
            set
            {
                CurEnableFourVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableFour));
            }
        }

        public ushort CurEnableFive
        {
            get => CurEnableFiveVariable.GetValueEx<ushort>();
            set
            {
                CurEnableFiveVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableFive));
            }
        }

        public ushort CurEnableSix
        {
            get => CurEnableSixVariable.GetValueEx<ushort>();
            set
            {
                CurEnableFiveVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableSix));
            }
        }

        public ushort CurEnableSeven
        {
            get => CurEnableSevenVariable.GetValueEx<ushort>();
            set
            {
                CurEnableSevenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableSeven));
            }
        }

        public ushort CurEnableEight
        {
            get => CurEnableEightVariable.GetValueEx<ushort>();
            set
            {
                CurEnableEightVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableEight));
            }
        }

        public ushort CurEnableNine
        {
            get => CurEnableNineVariable.GetValueEx<ushort>();
            set
            {
                CurEnableNineVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableNine));
            }
        }

        public ushort CurEnableTen
        {
            get => CurEnableTenVariable.GetValueEx<ushort>();
            set
            {
                CurEnableTenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableTen));
            }
        }

        public ushort CurEnableEleven
        {
            get => CurEnableElevenVariable.GetValueEx<ushort>();
            set
            {
                CurEnableElevenVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurEnableEleven));
            }
        }

        public float CurTimeDelay
        {
            get => CurTimeDelayVariable.GetValueEx<float>();
            set
            {
                CurTimeDelayVariable.WriteAnyValueEx(value);
                RaisePropertyChanged(nameof(CurTimeDelay));
            }
        }

        public CurParamsFactory(CurParameterOption option)
        {
            this._option = option;

            for (int i = 0; i < 11; i++) 
                CurTimeDisplayItems.Add(new CurTimeDisplayItem() { DeviceName = $"工位{i}" });

            IO.TryGet(_option.CurTimeConsumeOneUri!, out _curTimeConsumeOneVariable);
            _curTimeConsumeOneVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[0].CurTimeConsume = _curTimeConsumeOneVariable.GetValueEx<float>();
                CurTimeDisplayItems[0].TimeNow = DateTime.Now;
                CurTimeDisplayItems[0].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[0].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeOne));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeTwoUri!, out _curTimeConsumeTwoVariable);
            _curTimeConsumeTwoVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[1].CurTimeConsume = _curTimeConsumeTwoVariable.GetValueEx<float>();
                CurTimeDisplayItems[1].TimeNow = DateTime.Now;
                CurTimeDisplayItems[1].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[1].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeTwo));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeThreeUri!, out _curTimeConsumeThreeVariale);
            _curTimeConsumeThreeVariale.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[2].CurTimeConsume = _curTimeConsumeThreeVariale.GetValueEx<float>();
                CurTimeDisplayItems[2].TimeNow = DateTime.Now;
                CurTimeDisplayItems[2].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[2].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeThree));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeFourUri!, out _curTimeConsumeFourVariable);
            _curTimeConsumeFourVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[3].CurTimeConsume = _curTimeConsumeFourVariable.GetValueEx<float>();
                CurTimeDisplayItems[3].TimeNow = DateTime.Now;
                CurTimeDisplayItems[3].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[3].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeFour));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeFiveUri!, out _curTimeConsumeFiveVariable);
            _curTimeConsumeFiveVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[4].CurTimeConsume = _curTimeConsumeFiveVariable.GetValueEx<float>();
                CurTimeDisplayItems[4].TimeNow = DateTime.Now;
                CurTimeDisplayItems[4].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[4].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeFive));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeSixUri!, out _curTimeConsumeSixVariable);
            _curTimeConsumeSixVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[5].CurTimeConsume = _curTimeConsumeSixVariable.GetValueEx<float>();
                CurTimeDisplayItems[5].TimeNow = DateTime.Now;
                CurTimeDisplayItems[5].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[5].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeSix));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeSevenUri!,out _curTimeConsumeSevenVariable);
            _curTimeConsumeSevenVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[6].CurTimeConsume = _curTimeConsumeSevenVariable.GetValueEx<float>();
                CurTimeDisplayItems[6].TimeNow = DateTime.Now;
                CurTimeDisplayItems[6].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[6].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeSeven));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeEightUri!,out _curTimeConsumeEightVariable);
            _curTimeConsumeEightVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[7].CurTimeConsume = _curTimeConsumeEightVariable.GetValueEx<float>();
                CurTimeDisplayItems[7].TimeNow = DateTime.Now;
                CurTimeDisplayItems[7].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[7].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeEight));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeNineUri!, out _curTimeConsumeNineVariable);
            _curTimeConsumeNineVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[8].CurTimeConsume = _curTimeConsumeNineVariable.GetValueEx<float>();
                CurTimeDisplayItems[8].TimeNow = DateTime.Now;
                CurTimeDisplayItems[8].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[8].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeNine));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeTenUri!, out _curTimeConsumeTenVariable);
            _curTimeConsumeTenVariable.ValueChangedEvent += (s, e) => 
            {
                CurTimeDisplayItems[9].CurTimeConsume = _curTimeConsumeTenVariable.GetValueEx<float>();
                CurTimeDisplayItems[9].TimeNow = DateTime.Now;
                CurTimeDisplayItems[9].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[9].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeTen));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };
            IO.TryGet(_option.CurTimeConsumeElevenUri!, out _curTimeConsumeElevenVariable);
            _curTimeConsumeElevenVariable.ValueChangedEvent += (s, e) =>
            {
                CurTimeDisplayItems[10].CurTimeConsume = _curTimeConsumeElevenVariable.GetValueEx<float>();
                CurTimeDisplayItems[10].TimeNow = DateTime.Now;
                CurTimeDisplayItems[10].TimeBefore = DateTime.Now.AddSeconds(-CurTimeDisplayItems[10].CurTimeConsume);
                RaisePropertyChanged(nameof(CurTimeConsumeEleven));
                RaisePropertyChanged(nameof(CurTimeDisplayItems));
            };

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
            IO.TryGet(_option.CurTimeDelayUri!, out _curTimeDelayVariable);
            _curTimeDelayVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurTimeDelay));

            IO.TryGet(_option.CurEnableOneUri!, out _curEnableOneVariable);
            _curEnableOneVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableOne));
            IO.TryGet(_option.CurEnableTwoUri!, out _curEnableTwoVariable);
            _curEnableTwoVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableTwo));
            IO.TryGet(_option.CurEnableThreeUri!, out _curEnableThreeVariable);
            _curEnableThreeVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableThree));
            IO.TryGet(_option.CurEnableFourUri!, out _curEnableFourVariable);
            _curEnableFourVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableFour));
            IO.TryGet(_option.CurEnableFiveUri! , out _curEnableFiveVariable);
            _curEnableFiveVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableFive));
            IO.TryGet(_option.CurEnableSixUri!, out _curEnableSixVariable);
            _curEnableSixVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableSix));
            IO.TryGet(_option.CurEnableSevenUri!, out _curEnableSevenVariable);
            _curEnableSevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableSeven));
            IO.TryGet(_option.CurEnableEightUri!, out _curEnableEightVariable);
            _curEnableEightVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableEight));
            IO.TryGet(_option.CurEnableNineUri!, out _curEnableNineVariable);
            _curEnableNineVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableNine));
            IO.TryGet(_option.CurEnableTenUri!, out _curEnableTenVariable);
            _curEnableTenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableTen));
            IO.TryGet(_option.CurEnableElevenUri!, out _curEnableElevenVariable);
            _curEnableElevenVariable.ValueChangedEvent += (s, e) => RaisePropertyChanged(nameof(CurEnableEleven));
        }
    }
}
