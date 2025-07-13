using Application.SemiAuto;
using Application.Common;
using Application.UI;

namespace Application.GeneralControl
{
    public class GeneralControlViewModel : BaseViewModel
    {
        
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILanguageManager _languageManager;
        private DelegateCommand _setTimeCommand;
        private DelegateCommand _setDelayTimeCommand;
        private DelegateCommand _enableOneCommand;
        private DelegateCommand _enableTwoCommand;
        private DelegateCommand _enableThreeCommand;
        private DelegateCommand _enableFourCommand;
        private DelegateCommand _enableFiveCommand;
        private DelegateCommand _enableSixCommand;
        private DelegateCommand _enableSevenCommand;
        private DelegateCommand _enableEightCommand;
        private DelegateCommand _enableNineCommand;
        private DelegateCommand _enableTenCommand;
        private DelegateCommand _enableElevenCommand;
        private ushort _enableOne;

        public IEventAggregator EventAggregator => _eventAggregator;
        public DelegateCommand SetTimeCommand => _setTimeCommand ??= new DelegateCommand(() => 
        {
            _dialogService.Show("DialogView",new DialogParameters
            {
                { "Title", $"{_languageManager["是否确认设定每个工位耗时时间为"]}{GeneralControlModel.SetTime}？" }
            }, async result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    if (GeneralControlModel.SetTime == 0)
                    {
                        InfoGlobal(_languageManager["输入各工位耗时时间不应为0，请重新输入"]);
                        return;
                    }
                    else
                    {
                        try
                        {
                            SetParamsFactory.SetTime = GeneralControlModel.SetTime;
                            TriggerParamsFactory.TriggerTime = true;
                        }
                        catch(Exception ex)
                        {
                            ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
                            return;
                        }
                        try
                        {
                            var boolresult = await Task.Run(()
                                => TriggerParamsFactory.SetTimeTrigger());

                            if (boolresult)
                            {
                                EventAggregator.GetEvent<SetTimeEvent>().Publish();
                                InfoGlobal(_languageManager["每个工位耗时时间设定成功"]);
                            }
                            else
                            {
                                GeneralControlModel.SetTime = 0;
                                SetParamsFactory.SetTime = 0;
                                TriggerParamsFactory.TriggerTime = false;
                                EventAggregator.GetEvent<SetTimeEvent>().Publish();
                                ErrorGlobal(_languageManager["每个工位耗时时间设定失败，请重新设定"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorGlobal(ex.Message);
                        }
                    }
                }
                else
                    InfoGlobal(_languageManager["每个工位耗时时间设定已取消"]);
            });
        });
        public DelegateCommand SetDelayTimeCommand => _setDelayTimeCommand ??= new DelegateCommand(() =>
        {
            _dialogService.Show("DialogView", new DialogParameters
            {
                { "Title", $"{_languageManager["是否确认设定每个工位延时时间为"]}{GeneralControlModel.SetDelayTime}？" }
            }, async result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    if (GeneralControlModel.SetDelayTime == 0)
                    {
                        InfoGlobal(_languageManager["输入各工位延时时间不应为0，请重新输入"]);
                        return;
                    }
                    else
                    {
                        try
                        {
                            SetParamsFactory.SetTimeDelay = GeneralControlModel.SetDelayTime;
                            TriggerParamsFactory.TriggerTimeDelay = true;
                        }
                        catch (Exception ex)
                        {
                            ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
                            return;
                        }
                        try
                        {
                            var boolresult = await Task.Run(()
                                => TriggerParamsFactory.SetTimeDelayTrigger()); 

                            if (boolresult)
                            {
                                EventAggregator.GetEvent<SetTimeDelayEvent>().Publish();
                                InfoGlobal(_languageManager["每个工位延时时间设定成功"]);
                            }
                            else
                            {
                                GeneralControlModel.SetDelayTime = 0;
                                SetParamsFactory.SetTimeDelay = 0;
                                TriggerParamsFactory.TriggerTimeDelay = false;
                                EventAggregator.GetEvent<SetTimeDelayEvent>().Publish();
                                ErrorGlobal(_languageManager["每个工位延时时间设定失败，请重新设定"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorGlobal(ex.Message);
                        }
                    }
                }
                else
                    InfoGlobal(_languageManager["每个工位延时时间设定已取消"]);
            });
        });
        public DelegateCommand EnableOneCommand => _enableOneCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableOne = !SetParamsFactory.SetEnableOne;
                var result = await SetParamsFactory.TriggerEnableOneAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableOne = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch(Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableTwoCommand => _enableTwoCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableTwo = !SetParamsFactory.SetEnableTwo;
                var result = await SetParamsFactory.TriggerEnableTwoAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableTwo = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableThreeCommand => _enableThreeCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableThree = !SetParamsFactory.SetEnableThree;
                var result = await SetParamsFactory.TriggerEnableThreeAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableThree = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableFourCommand => _enableFourCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableFour = !SetParamsFactory.SetEnableFour;
                var result = await SetParamsFactory.TriggerEnableFourAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableFour = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableFiveCommand => _enableFiveCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableFive = !SetParamsFactory.SetEnableFive;
                var result = await SetParamsFactory.TriggerEnableFiveAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableFive = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableSixCommand => _enableSixCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableSix = !SetParamsFactory.SetEnableSix;
                var result = await SetParamsFactory.TriggerEnableSixAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableSix = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableSevenCommand => _enableSevenCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableSeven = !SetParamsFactory.SetEnableSeven;
                var result = await SetParamsFactory.TriggerEnableSevenAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableSeven = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableEightCommand => _enableEightCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableEight = !SetParamsFactory.SetEnableEight;
                var result = await SetParamsFactory.TriggerEnableEightAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableEight = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableNineCommand => _enableNineCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableNine = !SetParamsFactory.SetEnableNine;
                var result = await SetParamsFactory.TriggerEnableNineAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableNine = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableTenCommand => _enableTenCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableTen = !SetParamsFactory.SetEnableTen;
                var result = await SetParamsFactory.TriggerEnableTenAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableTen = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });
        public DelegateCommand EnableElevenCommand => _enableElevenCommand ??= new DelegateCommand(async () =>
        {
            try
            {
                SetParamsFactory.SetEnableEleven = !SetParamsFactory.SetEnableEleven;
                var result = await SetParamsFactory.TriggerEnableElevenAsync();
                if (result)
                {
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                }
                else
                {
                    SetParamsFactory.SetEnableEleven = false;
                    EventAggregator.GetEvent<EnableEvent>().Publish();
                    ErrorGlobal(_languageManager["系统未响应，请重新启动工位"]);
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message + "---" + _languageManager["设备未连接"]);
            }
        });

        [Unity.Dependency("Set")] public SetParamsFactory SetParamsFactory { get; set; }
        [Unity.Dependency("Trigger")] public TriggerParamsFactory TriggerParamsFactory { get; set; }
        [Unity.Dependency("GeneralControl")] public GeneralControlModel GeneralControlModel { get; set; }

        public GeneralControlViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILanguageManager languageManager)
        {
            this._dialogService = dialogService;
            this._eventAggregator = eventAggregator;
            this._languageManager = languageManager;
        }
    }
}
