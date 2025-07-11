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

        public CurParamsFactory(CurParameterOption option)
        {
            this._option = option;

            for (int i = 0; i < 11; i++) 
                CurTimeDisplayItems.Add(new CurTimeDisplayItem() { DeviceName = $"{i}" });

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
        }
    }
}
