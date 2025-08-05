using Application.Common;
using System.Collections.ObjectModel;

namespace Application.Russia
{
    public class WorkStationManager : IWorkStationManager
    {
        private ObservableCollection<WorkStationFactory> _workStations;

        public ObservableCollection<WorkStationFactory> WorkStations => _workStations;

        public WorkStationManager()
        {
            _workStations = [];
            Initialization();
        }

        private void Initialization()
        {
            for (var index = 1; index <= 18; index++)
            {
                var setEnableKey = $"SetEnable{index}";
                var setTimeKey = $"SetTime{index}";
                var setDelayKey = $"SetDelayTime{index}";
                var triggerKey = $"TriggerSignal{index}";
                var curTimeConsumeKey = $"CurTimeConsume{index}";
                var triggerTimeConsumeKey = $"TriggerTimeConsume{index}";

                if (IO.TryGet(setEnableKey, out var setEnableValue) &&
                    IO.TryGet(setTimeKey, out var setTimeValue) &&
                    IO.TryGet(setDelayKey, out var setDelayValue) &&
                    IO.TryGet(triggerKey, out var triggerValue) &&
                    IO.TryGet(curTimeConsumeKey, out var curTimeConsumeValue) &&
                    IO.TryGet(triggerTimeConsumeKey, out var triggerTimeConsumeValue))   
                {
                    var workStation = new WorkStationFactory(index,
                        setEnableValue,
                        setTimeValue,
                        setDelayValue,
                        triggerValue,
                        curTimeConsumeValue,
                        triggerTimeConsumeValue);
                    WorkStations.Add(workStation);
                }
            }
        }
    }
}
