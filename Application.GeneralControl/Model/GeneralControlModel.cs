namespace Application.GeneralControl
{
    public class GeneralControlModel : BindableBase
    {
        private float _setDelayOne;
        private float _setDelayTwo;
        private float _setDelayThree;
        private float _setDelayFour;
        private float _setDelayFive;
        private float _setDelaySix;
        private float _setDelaySeven;
        private float _setDelayEight;
        private float _setDelayNine;
        private float _setDelayTen;
        private float _setDelayEleven;

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

        public float SetDelayOne
        {
            get => _setDelayOne;
            set => SetProperty(ref _setDelayOne,value);
        }

        public float SetDelayTwo
        {
            get => _setDelayTwo;
            set => SetProperty(ref _setDelayTwo, value);
        }

        public float SetDelayThree
        {
            get => _setDelayThree;
            set => SetProperty(ref _setDelayThree, value);
        }

        public float SetDelayFour
        {
            get => _setDelayFour;
            set => SetProperty(ref _setDelayFour, value);
        }

        public float SetDelayFive
        {
            get => _setDelayFive;
            set => SetProperty(ref _setDelayFive, value);
        }

        public float SetDelaySix
        {
            get => _setDelaySix;
            set => SetProperty(ref _setDelaySix, value);
        }

        public float SetDelaySeven
        { 
            get => _setDelaySeven; 
            set => SetProperty(ref _setDelaySeven, value);
        }

        public float SetDelayEight
        {
            get => _setDelayEight; 
            set => SetProperty(ref _setDelayEight, value);
        }

        public float SetDelayNine
        {
            get => _setDelayNine;
            set => SetProperty(ref _setDelayNine, value);
        }

        public float SetDelayTen
        {
            get => _setDelayTen; 
            set => SetProperty(ref _setDelayTen, value);
        }

        public float SetDelayEleven
        {
            get => _setDelayEleven; 
            set => SetProperty(ref _setDelayEleven, value);
        }

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

        public GeneralControlModel() {  }
    }
}
