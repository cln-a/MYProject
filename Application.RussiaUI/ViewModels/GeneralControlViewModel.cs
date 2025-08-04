using Application.Russia;

namespace Application.RussiaUI
{
    public class GeneralControlViewModel : BindableBase
    {
        private readonly IWorkStationManager _workStationManager;

        public IWorkStationManager WorkStationManager => _workStationManager;

        public GeneralControlViewModel(IWorkStationManager workStationManager)
        {
            this._workStationManager = workStationManager;
        }
    }
}
