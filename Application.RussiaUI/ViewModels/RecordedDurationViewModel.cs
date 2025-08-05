using Application.Russia;
using Application.UI;

namespace Application.RussiaUI
{
    public class RecordedDurationViewModel : BaseViewModel
    {
        private readonly IWorkStationManager _workStationManager;

        public IWorkStationManager WorkStationManager => _workStationManager;

        public RecordedDurationViewModel(IWorkStationManager workStationManager)
            => this._workStationManager = workStationManager;
    }
}
