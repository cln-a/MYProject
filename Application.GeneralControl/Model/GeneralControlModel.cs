namespace Application.GeneralControl
{
    public class GeneralControlModel : BindableBase
    {
        private ushort _setEnableOne;
        private ushort _setEnableTwo;
        private ushort _setEnableThree;
        private ushort _setEnableFour;
        private ushort _setEnableFive;
        private ushort _setEnableSix;
        private ushort _setEnableSeven;
        private ushort _setEnableEight;
        private ushort _setEnableNine;
        private ushort _setEnableTen;
        private ushort _setEnableEleven;

        private float _setTime;
        private float _setDelayTime;


        public ushort SetEnableOne
        {
            get => _setEnableOne;
            set => SetProperty(ref _setEnableOne, value);
        }

        public ushort SetEnableTwo
        {
            get => _setEnableTwo;
            set => SetProperty(ref _setEnableTwo, value);
        }

        public ushort SetEnableThree
        {
            get => _setEnableThree; 
            set => SetProperty(ref _setEnableThree, value);
        }

        public ushort SetEnableFour
        {
            get => _setEnableFour;
            set => SetProperty(ref _setEnableFour, value);
        }

        public ushort SetEnableFive
        {
            get => _setEnableFive; 
            set => SetProperty(ref _setEnableFive, value);
        }

        public ushort SetEnableSix
        {
            get => _setEnableSix; 
            set => SetProperty(ref _setEnableSix, value);
        }

        public ushort SetEnableSeven
        {
            get => _setEnableSeven;
            set => SetProperty(ref _setEnableSeven, value);
        }

        public ushort SetEnableEight
        {
            get => _setEnableEight; 
            set => SetProperty(ref _setEnableEight, value);
        }

        public ushort SetEnableNine
        {
            get => _setEnableNine;
            set => SetProperty(ref _setEnableNine, value); 
        }

        public ushort SetEnableTen
        {
            get => _setEnableTen;
            set => SetProperty(ref _setEnableTen, value);
        }

        public ushort SetEnableEleven
        {
            get => _setEnableEleven;
            set => SetProperty(ref _setEnableEleven, value);
        }

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
