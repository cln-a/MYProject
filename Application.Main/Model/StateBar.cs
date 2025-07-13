namespace Application.Main
{
    public class StateBar : BindableBase
    {
        private object _tag;
        private string _stateImageSource;
        private string _deviceName;

        public object Tag
        {
            get => _tag;
            set => SetProperty(ref _tag, value);
        }

        public string StateImageSource
        {
            get => _stateImageSource;
            set => SetProperty(ref _stateImageSource, value);
        }

        public string DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }
    }
}
