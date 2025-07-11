using Application.Common;

namespace Application.SemiAuto
{
    public class SetParamsFactory : BindableBase
    {
        private readonly SetParameterOption _option;
        private readonly IEventAggregator _eventAggregator;

        private AutoResetEvent _oneresetEvent = new(false);
        private AutoResetEvent _tworesetEvent = new(false);
        private AutoResetEvent _threeresetEvent = new(false);
        private AutoResetEvent _fourresetEvent = new(false);
        private AutoResetEvent _fiveresetEvent = new(false);
        private AutoResetEvent _sixresetEvent = new(false);
        private AutoResetEvent _sevenresetEvent = new(false);
        private AutoResetEvent _eightresetEvent = new(false);
        private AutoResetEvent _nineresetEvent = new(false);
        private AutoResetEvent _tenresetEvent = new(false);
        private AutoResetEvent _elevenresetEvent = new(false);

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
            get => TriggerEnableFiveVariable.GetValueEx<bool>();
            set => TriggerEnableFiveVariable.WriteAnyValueEx(value);
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

        public bool SetEnableOne
        {
            get => SetEnableOneVariable.GetValueEx<bool>();
            set => SetEnableOneVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableTwo
        {
            get => SetEnableTwoVariable.GetValueEx<bool>();
            set => SetEnableTwoVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableThree
        {
            get => SetEnableThreeVariable.GetValueEx<bool>();
            set => SetEnableThreeVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableFour
        {
            get => SetEnableFourVariable.GetValueEx<bool>();
            set => SetEnableFourVariable.WriteAnyValueEx(value);

        }

        public bool SetEnableFive
        {
            get => SetEnableFiveVariable.GetValueEx<bool>();
            set => SetEnableFiveVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableSix
        {
            get => SetEnableSixVariable.GetValueEx<bool>();
            set => SetEnableSixVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableSeven
        {
            get => SetEnableSevenVariable.GetValueEx<bool>();
            set => SetEnableSevenVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableEight
        {
            get => SetEnableEightVariable.GetValueEx<bool>();
            set => SetEnableEightVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableNine
        {
            get => SetEnableNineVariable.GetValueEx<bool>();
            set => SetEnableNineVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableTen
        {
            get => SetEnableTenVariable.GetValueEx<bool>();
            set => SetEnableTenVariable.WriteAnyValueEx(value);
        }

        public bool SetEnableEleven
        {
            get => SetEnableElevenVariable.GetValueEx<bool>();
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

            IO.TryGet(_option.TriggerEnableOneUri!, out _triggerEnableOneVariable);
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
            EventAggregator.GetEvent<EnableEvent>().Subscribe(EnableChange);
        }

        private void EnableChange()
        {
            RaisePropertyChanged(nameof(SetEnableOne));
            RaisePropertyChanged(nameof(SetEnableTwo));
            RaisePropertyChanged(nameof(SetEnableThree));
            RaisePropertyChanged(nameof(SetEnableFour));
            RaisePropertyChanged(nameof(SetEnableFive));
            RaisePropertyChanged(nameof(SetEnableSix));
            RaisePropertyChanged(nameof(SetEnableSeven));
            RaisePropertyChanged(nameof(SetEnableEight));
            RaisePropertyChanged(nameof(SetEnableNine));
            RaisePropertyChanged(nameof(SetEnableTen));
            RaisePropertyChanged(nameof(SetEnableEleven));
        }

        private void SetTimeChange() => RaisePropertyChanged(nameof(SetTime));

        private void SetTimeDelayChange() => RaisePropertyChanged(nameof(SetTimeDelay));

        public async Task<bool> TriggerEnableOneAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _oneresetEvent.Reset();

                    TriggerEnableOne = true;
                    if (_oneresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableOne = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableTwoAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _tworesetEvent.Reset();

                    TriggerEnableTwo = true;
                    if (_tworesetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableTwo = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableThreeAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _threeresetEvent.Reset();

                    TriggerEnableThree = true;
                    if (_threeresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableThree = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableFourAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _fourresetEvent.Reset();

                    TriggerEnableFour = true;
                    if (_fourresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableFour = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableFiveAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _fiveresetEvent.Reset();

                    TriggerEnableFive = true;
                    if (_fiveresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableFive = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableSixAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _sixresetEvent.Reset();

                    TriggerEnableSix = true;
                    if (_sixresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableSix = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableSevenAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _sevenresetEvent.Reset();

                    TriggerEnableSeven = true;
                    if (_sevenresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableSeven = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableEightAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _eightresetEvent.Reset();

                    TriggerEnableEight = true;
                    if (_eightresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableEight = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableNineAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _nineresetEvent.Reset();

                    TriggerEnableNine = true;
                    if (_nineresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableNine = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableTenAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _tenresetEvent.Reset();

                    TriggerEnableTen = true;
                    if (_tenresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableTen = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> TriggerEnableElevenAsync()
        {
            try
            {
                var result = await Task.Run(() =>
                {
                    _elevenresetEvent.Reset();

                    TriggerEnableEleven = true;
                    if (_elevenresetEvent.WaitOne(10000))
                    {
                        return true;
                    }
                    else
                    {
                        TriggerEnableEleven = false;
                        return false;
                    }
                });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
