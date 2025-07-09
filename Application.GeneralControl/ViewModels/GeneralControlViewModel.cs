using Application.SemiAuto;
using Application.UI.Dialog;
using System.ComponentModel;

namespace Application.GeneralControl
{
    public class GeneralControlViewModel : BindableBase
    {
        private DelegateCommand _sendStartCommand;
        private DelegateCommand _sendSetTimeCommand;
        private DelegateCommand _sendDeleayTimeCommand;

        [Unity.Dependency("Set")] 
        public SetParamsFactory SetParamsFactory { get; set; }
        
        [Unity.Dependency("Cur")]
        public CurParamsFactory CurParamsFactory { get; set; }

        [Unity.Dependency("Trigger")]
        public TriggerParamsFactory TriggerParamsFactory { get; set; }

        [Unity.Dependency("GeneralControl")]
        public GeneralControlModel GeneralControlModel { get; set; }

        public DelegateCommand SendStartCommand => _sendStartCommand ??= new DelegateCommand(SendStartCmd);
        public DelegateCommand SendSetTimeCommand => _sendSetTimeCommand ??= new DelegateCommand(SendSetTimeCmd);
        public DelegateCommand SendDeleayTimeCommand => _sendDeleayTimeCommand ??= new DelegateCommand(SendDeleayTimeCmd);

        public GeneralControlViewModel()
        {

        }

        private void SendStartCmd()
        {
            try
            {
                Task.Run(async () => 
                {
                    await SetAndWaitClearAsync(
                        TriggerParamsFactory,
                        nameof(TriggerParamsFactory.TriggerEnable),
                        20);
                    SetParamsFactory.SetEnableOne = GeneralControlModel.SetEnableOne;
                    SetParamsFactory.SetEnableTwo = GeneralControlModel.SetEnableTwo;
                    SetParamsFactory.SetEnableThree = GeneralControlModel.SetEnableThree;
                    SetParamsFactory.SetEnableFour = GeneralControlModel.SetEnableFour;
                    SetParamsFactory.SetEnableFive = GeneralControlModel.SetEnableFive;
                    SetParamsFactory.SetEnableSix = GeneralControlModel.SetEnableSix;
                    SetParamsFactory.SetEnableSeven = GeneralControlModel.SetEnableSeven;
                    SetParamsFactory.SetEnableEight = GeneralControlModel.SetEnableEight;
                    SetParamsFactory.SetEnableNine = GeneralControlModel.SetEnableNine;
                    SetParamsFactory.SetEnableTen = GeneralControlModel.SetEnableTen;
                    SetParamsFactory.SetEnableEleven = GeneralControlModel.SetEnableEleven;
                } );
            }
            catch (Exception ex) 
            {
                PopupBox.Show(ex.Message);
            }
        }

        private void SendSetTimeCmd()
        {
            try
            {
                Task.Run(async () => 
                {
                    await SetAndWaitClearAsync(
                        TriggerParamsFactory,
                        nameof(TriggerParamsFactory.TriggerTime),
                        20);
                    SetParamsFactory.SetTime = GeneralControlModel.SetTime;
                });
                
            }
            catch(Exception ex)
            {
                PopupBox.Show(ex.Message);
            }
        }

        private void SendDeleayTimeCmd()
        {
            try
            {
                Task.Run(async () =>
                {
                    await SetAndWaitClearAsync(
                        TriggerParamsFactory,
                        nameof(TriggerParamsFactory.TriggerTimeDelay),
                        20);
                    SetParamsFactory.SetDelayOne = GeneralControlModel.SetDelayOne;
                    SetParamsFactory.SetDelayTwo = GeneralControlModel.SetDelayTwo;
                    SetParamsFactory.SetDelayThree = GeneralControlModel.SetDelayThree;
                    SetParamsFactory.SetDelayFour = GeneralControlModel.SetDelayFour;
                    SetParamsFactory.SetDelayFive = GeneralControlModel.SetDelayFive;
                    SetParamsFactory.SetDelaySix = GeneralControlModel.SetDelaySix;
                    SetParamsFactory.SetDelaySeven = GeneralControlModel.SetDelaySeven;
                    SetParamsFactory.SetDelayEight = GeneralControlModel.SetDelayEight;
                    SetParamsFactory.SetDelayNine = GeneralControlModel.SetDelayNine;
                    SetParamsFactory.SetDelayTen = GeneralControlModel.SetDelayTen;
                    SetParamsFactory.SetDelayEleven = GeneralControlModel.SetDelayEleven;
                });
            }
            catch(Exception ex) 
            {
                PopupBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 通用等待属性清零方法
        /// </summary>
        /// <param name="factory">TriggerParamsFactory实例</param>
        /// <param name="propertyName">要监听的属性名</param>
        /// <param name="waitSeconds">等待秒数</param>
        public async Task SetAndWaitClearAsync(TriggerParamsFactory factory, string propertyName, int waitSeconds)
        {
            switch (propertyName)
            {
                case nameof(factory.TriggerTime):
                    factory.TriggerTime = true;
                    break;
                case nameof(factory.TriggerEnable):
                    factory.TriggerEnable = true;
                    break;
                case nameof(factory.TriggerTimeDelay):
                    factory.TriggerTimeDelay = true;
                    break;
                default:
                    throw new ArgumentException("属性名无效");
            }

            var tcs = new TaskCompletionSource<bool>();

            PropertyChangedEventHandler handler = null!;
            handler = (sender, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    bool isSet = false;
                    switch (propertyName)
                    {
                        case nameof(factory.TriggerTime):
                            isSet = factory.TriggerTime;
                            break;
                        case nameof(factory.TriggerEnable):
                            isSet = factory.TriggerEnable;
                            break;
                        case nameof(factory.TriggerTimeDelay):
                            isSet = factory.TriggerTimeDelay;
                            break;
                    }
                    if (!isSet)
                        tcs.TrySetResult(true);
                }
            };

            factory.PropertyChanged += handler;
            var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(waitSeconds)));
            factory.PropertyChanged -= handler;

            if (completedTask == tcs.Task)
                PopupBox.Show($"{propertyName} 已被清零！操作完成");
            else
                PopupBox.Show($"等待超时，{propertyName} 未被清零！");
        }
    }
}
