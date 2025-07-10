namespace Application.Dialog
{
    public class DialogViewModel : BindableBase, IDialogAware
    {
        private string? _title;
        private DelegateCommand _okCommand;
        private DelegateCommand _cancelCommand;

        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);  
        }

        public DelegateCommand OkCommand => _okCommand ??= new DelegateCommand(() =>
            RequestClose.Invoke(new DialogResult(ButtonResult.OK)));

        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(() =>
            RequestClose.Invoke(new DialogResult(ButtonResult.Cancel)));

        public DialogCloseListener RequestClose { get; }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters) 
            => Title = parameters.GetValue<string>("Title");
    }
}
