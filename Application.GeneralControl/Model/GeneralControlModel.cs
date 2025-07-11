namespace Application.GeneralControl
{
    public class GeneralControlModel : BindableBase
    {
        private float _setTime;
        private float _setDelayTime;

        public float SetTime
        {
            get => _setTime;
            set => SetProperty(ref _setTime, value);
        }

        public float SetDelayTime
        {
            get => _setDelayTime;
            set => SetProperty(ref _setDelayTime, value);
        }

        public GeneralControlModel() {  }
    }
}
