namespace Application.SemiAuto
{
    public class CurTimeDisplayItem : BindableBase
    {
        private string? _deviceName;
        private float _curTimeConsume;
        private DateTime _timeNow = DateTime.Now;
        private DateTime _timeBefore = DateTime.Now;

        public string? DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        public float CurTimeConsume
        {
            get => _curTimeConsume;
            set => SetProperty(ref _curTimeConsume, value);
        }

        public DateTime TimeNow
        {
            get => _timeNow;
            set => SetProperty(ref _timeNow, value);
        }

        public DateTime TimeBefore
        {
            get => _timeBefore; 
            set => SetProperty(ref _timeBefore, value);
        }
    }
}
