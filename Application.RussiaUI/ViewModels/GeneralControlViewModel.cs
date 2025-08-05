using Application.Common;
using Application.Russia;
using Application.UI;

namespace Application.RussiaUI
{
    public class GeneralControlViewModel : BaseViewModel
    {
        private readonly IWorkStationManager _workStationManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILanguageManager _languageManager;
        private string _identity;

        public IWorkStationManager WorkStationManager => _workStationManager;
        public string Identity 
        {
            get => _identity;
            set => SetProperty(ref _identity, value);
        }

        public GeneralControlViewModel(
            IWorkStationManager workStationManager, 
            IEventAggregator eventAggregator,
            ILanguageManager languageManager)
        {
            this._workStationManager = workStationManager;
            this._eventAggregator = eventAggregator;
            this._languageManager = languageManager;

            Identity = _languageManager["工位序号"];

            _eventAggregator.GetEvent<DialogMessageEvent>().Subscribe(InfoGlobal);
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(() =>
            {
                Identity = _languageManager["工位序号"];
            });
            
        }
    }
}
