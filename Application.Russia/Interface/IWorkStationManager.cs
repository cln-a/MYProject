using System.Collections.ObjectModel;

namespace Application.Russia
{
    public interface IWorkStationManager
    {
        public ObservableCollection<WorkStationFactory> WorkStations {  get; }
    }
}
