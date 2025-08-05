using Application.Common;
using Application.Russia;
using Application.UI;

namespace Application.RussiaUI
{
    public class GeneralControlViewModel : BaseViewModel
    {
        private readonly IWorkStationManager _workStationManager;
        private readonly IEventAggregator _eventAggregator;

        public IWorkStationManager WorkStationManager => _workStationManager;
        public event Action RefreshDataGridHeaderRequested;

        public GeneralControlViewModel(
            IWorkStationManager workStationManager, 
            IEventAggregator eventAggregator)
        {
            this._workStationManager = workStationManager;
            this._eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<DialogMessageEvent>().Subscribe(InfoGlobal);
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(() => RefreshDataGridHeaderRequested?.Invoke());
        }
    }
}
