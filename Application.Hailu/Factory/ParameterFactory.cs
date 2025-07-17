using Application.Common;
using Application.Hailu.Events;

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
        private readonly IVariable _countVariable;
        private readonly IVariable _offLineFlagVariable;
        private readonly IVariable _measureOKFlagVariable;
        private readonly IVariable _measureErrorFlagVariable;
        private readonly IVariable _identityToPLVariable;
        private readonly IVariable _identityFromPLCVariable;
        private string? _batchCode;

        public IEventAggregator EventAggregator => _eventAggregator;
        public IVariable ReadyFlagVariable => _readyFlagVariable;
        public IVariable RequestFlagVariable => _requestFlagVariable;
        public IVariable LengthVariable => _lengthVariable;
        public IVariable WidthVariable => _widthVariable;
        public IVariable ThicknessVariable => _thicknessVariable;
        public IVariable RemarkVariable => _remarkVariable;
        public IVariable CountVariable => _countVariable;
        public IVariable OffLineFlagVariable => _offLineFlagVariable;
        public IVariable MeasureOKFlagVariable => _measureOKFlagVariable;
        public IVariable MeasureErrorFlagVariable => _measureErrorFlagVariable;
        public IVariable IdentityToPLVariable => _identityToPLVariable;
        public IVariable IdentityFromPLCVariable => _identityFromPLCVariable;

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

        public int Count
        {
            get => CountVariable.GetValueEx<int>();
            set => CountVariable.WriteAnyValueEx(value);
        }

        public ushort OffLineFlag
        {
            get => OffLineFlagVariable.GetValueEx<ushort>(); 
            set => OffLineFlagVariable.WriteAnyValueEx(value);
        }

        public ushort MeasureOKFlag
        {
            get => MeasureOKFlagVariable.GetValueEx<ushort>(); 
            set => MeasureOKFlagVariable.WriteAnyValueEx(value);
        }

        public ushort MeasureErrorFlag
        {
            get => MeasureErrorFlagVariable.GetValueEx<ushort>();
            set => MeasureErrorFlagVariable.WriteAnyValueEx(value);
        }

        public int IdentityToPLC
        {
            get => IdentityToPLVariable.GetValueEx<int>();
            set => IdentityToPLVariable.WriteAnyValueEx(value);
        }

        public int IdentityFromPLC
        {
            get => IdentityFromPLCVariable.GetValueEx<int>();
            set => IdentityFromPLCVariable.WriteAnyValueEx(value);
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
            IO.TryGet(_option.CountUri!, out _countVariable);
            IO.TryGet(_option.OffLineFlagUri!,out _offLineFlagVariable);
            IO.TryGet(_option.MeasureOKFlagUri!,out _measureOKFlagVariable);
            IO.TryGet(_option.MeasureErrorFlagUri!, out _measureErrorFlagVariable);
            IO.TryGet(_option.IdentityToPLCUri!,out _identityToPLVariable);
            IO.TryGet(_option.IdentityFromPLCUri!, out _identityFromPLCVariable);
            IdentityFromPLCVariable.ValueChangedEvent += (s, e) =>
            {
                eventAggregator.GetEvent<IdentityChangedEvent>().Publish(e.GetNewValue<int>());
            };
        }
    }
}
