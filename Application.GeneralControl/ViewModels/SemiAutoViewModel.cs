using Application.Common;
using Application.SemiAuto;
using Application.UI;

namespace Application.GeneralControl
{
    public class SemiAutoViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILanguageManager _languageManager;
        private DelegateCommand _languageChangeCommand;

        [Unity.Dependency("Cur")] public CurParamsFactory? CurParamsFactory { get; set; }

        public DelegateCommand LanguageChangeCommand => _languageChangeCommand ??= new DelegateCommand(LanguageChanged);
        public event Action RefreshDataGridHeaderRequested;

        public SemiAutoViewModel(IEventAggregator eventAggregator, ILanguageManager languageManager)
        {
            this._eventAggregator = eventAggregator;
            this._languageManager = languageManager;
            _eventAggregator.GetEvent<LanguageChangedEvent>().Subscribe(LanguageChanged);
        }

        private void LanguageChanged() => RefreshDataGridHeaderRequested?.Invoke();
    }
}
