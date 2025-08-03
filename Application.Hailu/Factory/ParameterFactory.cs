using Application.Common;

namespace Application.Hailu
{
    public class ParameterFactory : BindableBase
    {
        private readonly ParameterOption _option;
        private readonly IEventAggregator _eventAggregator;
        private readonly IVariable _readyFlagVariable;
        private readonly IVariable _requestFlagVariable;
        private readonly IVariable _lengthVariable;
        private readonly IVariable _widthVariable;
        private readonly IVariable _thicknessVariable;
        private readonly IVariable _holeLengthRightVariable;
        private readonly IVariable _holeDistanceRightVariable;
        private readonly IVariable _holeLengthMiddleVariable;
        private readonly IVariable _holeDistanceMiddleVariable;
        private readonly IVariable _holeLengthLeftVariable;
        private readonly IVariable _holeDistanceLeftVariable;
        private readonly IVariable _offLineFlagVariable;
        private readonly IVariable _measureWidthFlagVariable;
        private readonly IVariable _boardTypeVariable;
        private readonly IVariable _pLCStateFalgVariable;
        private string? _batchCode;
        private string? _batch;
        private string? _identity;

        public IVariable ReadyFlagVariable => _readyFlagVariable;
        public IVariable RequestFlagVariable => _requestFlagVariable;
        public IVariable LengthVariable => _lengthVariable;
        public IVariable WidthVariable => _widthVariable;
        public IVariable ThicknessVariable => _thicknessVariable;
        public IVariable HoleLengthRightVariable => _holeLengthRightVariable;
        public IVariable HoleDistanceRightVariable => _holeDistanceRightVariable;
        public IVariable HoleLengthMiddleVariable => _holeLengthMiddleVariable;
        public IVariable HoleDistanceMiddleVariable => _holeDistanceMiddleVariable;
        public IVariable HoleLengthLeftVariable => _holeLengthLeftVariable;
        public IVariable HoleDistanceLeftVariable => _holeDistanceLeftVariable;
        public IVariable OffLineFlagVariable => _offLineFlagVariable;
        public IVariable MeasureWidthFlagVariable => _measureWidthFlagVariable;
        public IVariable BoardTypeVariable => _boardTypeVariable;
        public IVariable PLCStateFalgVariable => _pLCStateFalgVariable;

        public ushort ReadyFlag
        {
            get => ReadyFlagVariable.GetValueEx<ushort>();
            set => ReadyFlagVariable.WriteAnyValueEx(value);
        }

        public ushort RequestFlag
        {
            get => RequestFlagVariable.GetValueEx<ushort>();
            set => RequestFlagVariable.WriteAnyValueEx(value);
        }

        public ushort Length
        {
            get => LengthVariable.GetValueEx<ushort>();
            set => LengthVariable.WriteAnyValueEx(value);
        }

        public ushort Width
        {
            get => WidthVariable.GetValueEx<ushort>();
            set => WidthVariable.WriteAnyValueEx(value);
        }

        public ushort Thickness
        {
            get => ThicknessVariable.GetValueEx<ushort>(); 
            set => ThicknessVariable.WriteAnyValueEx(value);
        }

        public ushort HoleLengthRight
        {
            get => HoleLengthRightVariable.GetValueEx<ushort>();
            set => HoleLengthRightVariable.WriteAnyValueEx(value);
        }

        public ushort HoleDistanceRight
        {
            get => HoleDistanceRightVariable.GetValueEx<ushort>();
            set => HoleDistanceRightVariable.WriteAnyValueEx(value);
        }

        public ushort HoleLengthMiddle
        {
            get => HoleLengthMiddleVariable.GetValueEx<ushort>();
            set => HoleLengthMiddleVariable.WriteAnyValueEx(value);
        }

        public ushort HoleDistanceMiddle
        {
            get => HoleDistanceMiddleVariable.GetValueEx<ushort>(); 
            set => HoleDistanceMiddleVariable.WriteAnyValueEx(value);
        }

        public ushort HoleLengthLeft
        {
            get => HoleLengthLeftVariable.GetValueEx<ushort>();
            set => HoleLengthLeftVariable.WriteAnyValueEx(value);
        }

        public ushort HoleDistanceLeft
        {
            get => HoleDistanceLeftVariable.GetValueEx<ushort>();
            set => HoleDistanceLeftVariable.WriteAnyValueEx(value);
        }

        public ushort OffLineFlag
        {
            get => OffLineFlagVariable.GetValueEx<ushort>(); 
            set => OffLineFlagVariable.WriteAnyValueEx(value);
        }

        public ushort MeasureWidthFlag
        {
            get => MeasureWidthFlagVariable.GetValue<ushort>();
            set => MeasureWidthFlagVariable.WriteAnyValueEx(value);
        }

        public ushort BoardType
        {
            get => BoardTypeVariable.GetValueEx<ushort>();
            set => BoardTypeVariable.WriteAnyValueEx(value);
        }

        public ushort PLCStateFalg
        {
            get => PLCStateFalgVariable.GetValueEx<ushort>();
            set => PLCStateFalgVariable.WriteAnyValue(value);
        }

        public string? BatchCode
        {
            get => _batchCode;
            set => SetProperty(ref _batchCode, value);
        }

        public string? Batch
        {
            get => _batch;
            set => SetProperty(ref _batch, value);  
        }

        public string? Identity
        {
            get => _identity;
            set => SetProperty(ref _identity, value);
        }

        public ParameterFactory(ParameterOption option, IEventAggregator eventAggregator)
        {
            this._option = option;
            this._eventAggregator = eventAggregator;

            IO.TryGet(_option.ReadyFlagUri!, out _readyFlagVariable);
            IO.TryGet(_option.RequestFlagUri!, out _requestFlagVariable);
            RequestFlagVariable.ValueChangedEvent += (s, e) =>
            {
                if(e.GetNewValue<ushort>() == 1)
                    eventAggregator.GetEvent<RequestFlagReadedEvent>().Publish();
            };
            IO.TryGet(_option.LengthUri!,out _lengthVariable);
            IO.TryGet(_option.WidthUri!, out _widthVariable);
            IO.TryGet(_option.ThicknessUri!, out _thicknessVariable);
            IO.TryGet(_option.HoleLengthRightUri!,out _holeLengthRightVariable);
            IO.TryGet(_option.HoleDistanceRightUri!,out _holeDistanceRightVariable);
            IO.TryGet(_option.HoleLengthMiddleUri!, out _holeLengthMiddleVariable);
            IO.TryGet(_option.HoleDistanceMiddleUri!, out _holeDistanceMiddleVariable);
            IO.TryGet(_option.HoleLengthLeftUri!, out _holeLengthLeftVariable);
            IO.TryGet(_option.HoleDistanceLeftUri!, out _holeDistanceLeftVariable);
            IO.TryGet(_option.OffLineFlagUri!,out _offLineFlagVariable);
            OffLineFlagVariable.ValueChangedEvent += (s, e) =>
            {
                if (e.GetNewValue<ushort>() == 1) 
                    eventAggregator.GetEvent<OffLineFlagReadedEvent>().Publish();
            };
            IO.TryGet(_option.MeasureWidthFlagUri!, out _measureWidthFlagVariable);
            MeasureWidthFlagVariable.ValueChangedEvent += (s, e) =>
            {
                if (e.GetNewValue<ushort>() == 1)
                    eventAggregator.GetEvent<MeasureWidthFlagReadedEvent>().Publish();
            };
            IO.TryGet(_option.BoardTypeFlagUri!, out _boardTypeVariable);
            IO.TryGet(_option.PLCStateFalgUri!, out _pLCStateFalgVariable);
        }
    }
}
