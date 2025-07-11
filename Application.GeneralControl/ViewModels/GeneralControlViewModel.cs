using Application.SemiAuto;
using Application.UI.Dialog;

namespace Application.GeneralControl
{
    public class GeneralControlViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _setTimeCommand;
        private DelegateCommand _setDelayTimeCommand;

        public IEventAggregator EventAggregator => _eventAggregator;

        public DelegateCommand SetTimeCommand => _setTimeCommand ??= new DelegateCommand(() => 
        {
            _dialogService.Show("DialogView",new DialogParameters
            {
                { "Title", $"是否确认设定每个工位耗时时间为{GeneralControlModel.SetTime}？" }
            }, async result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    if (GeneralControlModel.SetTime == 0)
                    {
                        PopupBox.Show("输入各工位耗时时间不应为0，请重新输入");
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
                            PopupBox.Show(ex.Message);
                            return;
                        }

                        try
                        {
                            var boolresult = await Task.Run(()
                                => TriggerParamsFactory.SetTimeTrigger());

                            if (boolresult)
                            {
                                EventAggregator.GetEvent<SetTimeEvent>().Publish();
                                PopupBox.Show("每个工位耗时时间设定成功");
                            }
                            else
                            {
                                GeneralControlModel.SetTime = 0;
                                SetParamsFactory.SetTime = 0;
                                TriggerParamsFactory.TriggerTime = false;
                                EventAggregator.GetEvent<SetTimeEvent>().Publish();
                                PopupBox.Show("每个工位耗时时间设定失败，请重新设定");
                            }
                        }
                        catch (Exception ex)
                        {
                            PopupBox.Show(ex.Message);
                        }
                    }
                }
                else
                    PopupBox.Show("每个工位耗时时间设定已取消");
            });
        });

        public DelegateCommand SetDelayTimeCommand => _setDelayTimeCommand ??= new DelegateCommand(() =>
        {
            _dialogService.Show("DialogView", new DialogParameters
            {
                { "Title", $"是否确认设定每个工位延时时间为{GeneralControlModel.SetDelayTime}？" }
            }, async result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    if (GeneralControlModel.SetDelayTime == 0)
                    {
                        PopupBox.Show("输入各工位延时时间不应为0，请重新输入");
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
                            PopupBox.Show(ex.Message);
                            return;
                        }

                        try
                        {
                            var boolresult = await Task.Run(()
                                => TriggerParamsFactory.SetTimeDelayTrigger()); 

                            if (boolresult)
                            {
                                EventAggregator.GetEvent<SetTimeDelayEvent>().Publish();
                                PopupBox.Show("每个工位延时时间设定成功");
                            }
                            else
                            {
                                GeneralControlModel.SetDelayTime = 0;
                                SetParamsFactory.SetTimeDelay = 0;
                                TriggerParamsFactory.TriggerTimeDelay = false;
                                EventAggregator.GetEvent<SetTimeDelayEvent>().Publish();
                                PopupBox.Show("每个工位延时时间设定失败，请重新设定");
                            }
                        }
                        catch (Exception ex)
                        {
                            PopupBox.Show(ex.Message);
                        }
                    }
                }
                else
                    PopupBox.Show("每个工位延时时间设定已取消");
            });
        });

        [Unity.Dependency("Set")] 
        public SetParamsFactory SetParamsFactory { get; set; }
        
        [Unity.Dependency("Cur")]
        public CurParamsFactory CurParamsFactory { get; set; }

        [Unity.Dependency("Trigger")]
        public TriggerParamsFactory TriggerParamsFactory { get; set; }

        [Unity.Dependency("GeneralControl")]
        public GeneralControlModel GeneralControlModel { get; set; }

        public GeneralControlViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            this._dialogService = dialogService;
            this._eventAggregator = eventAggregator;
        }
    }
}
