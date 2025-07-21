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
        private readonly IVariable _remarkVariable;
        private readonly IVariable _offLineFlagVariable;
        private readonly IVariable _measureWidthFlagVariable;
        private readonly IVariable _millingCutterFlagVariable;
        private string? _batchCode;

        public IEventAggregator EventAggregator => _eventAggregator;
        public IVariable ReadyFlagVariable => _readyFlagVariable;
        public IVariable RequestFlagVariable => _requestFlagVariable;
        public IVariable LengthVariable => _lengthVariable;
        public IVariable WidthVariable => _widthVariable;
        public IVariable ThicknessVariable => _thicknessVariable;
        public IVariable RemarkVariable => _remarkVariable;
        public IVariable OffLineFlagVariable => _offLineFlagVariable;
        public IVariable MeasureWidthFlagVariable => _measureWidthFlagVariable;
        public IVariable MillingCutterFlagVariable => _millingCutterFlagVariable;

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

        public int Length
        {
            get => LengthVariable.GetValueEx<int>();
            set => LengthVariable.WriteAnyValueEx(value);
        }

        public int Width
        {
            get => WidthVariable.GetValueEx<int>();
            set => WidthVariable.WriteAnyValueEx(value);
        }

        public int Thickness
        {
            get => ThicknessVariable.GetValueEx<int>(); 
            set => ThicknessVariable.WriteAnyValueEx(value);
        }

        public int Remark
        {
            get => RemarkVariable.GetValueEx<int>(); 
            set => RemarkVariable.WriteAnyValueEx(value);
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

        public ushort MillingCutterFlag
        {
            get => MillingCutterFlagVariable.GetValue<ushort>();
            set => MillingCutterFlagVariable.WriteAnyValueEx(value);
        }

        public string? BatchCode
        {
            get => _batchCode;
            set => SetProperty(ref _batchCode, value);
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
            IO.TryGet(_option.RemarkUri!, out _remarkVariable);
            IO.TryGet(_option.OffLineFlagUri!,out _offLineFlagVariable);
            IO.TryGet(_option.MeasureWidthFlagUri!, out _measureWidthFlagVariable);
            IO.TryGet(_option.MillingCutterFlagUri!, out _millingCutterFlagVariable);
        }
    }
}
