using Application.Common;
using System.Windows.Navigation;

namespace Application.UI
{
    public class BaseEditViewModel<T> : BaseViewModel, IDialogAware
    {
        protected IDictionary<string, object> _settings;
        protected string? _title;
        protected T _dto;
        protected CommandTypeEnum _commandType;
        protected DelegateCommand _cancelCommand;
        protected DelegateCommand<T> _submitCommand;

        public IDictionary<string, object> Settings { get => _settings; set => _settings = value; }

        public string? Title { get => _title; set => SetProperty(ref _title, value); }

        public T Dto { get => _dto; set => SetProperty(ref _dto, value); }

        public CommandTypeEnum CommandType { get => _commandType; set => _commandType = value; }

        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(CancelCmd);

        public DelegateCommand<T> SubmitCommand => _submitCommand ??= new DelegateCommand<T>(SubmitCmd);

        public DialogCloseListener RequestClose { get; }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey(TitleKey))
                Title = parameters.GetValue<string>(TitleKey);
            if (parameters.ContainsKey(ModelKey))
                Dto = parameters.GetValue<T>(ModelKey);
            if (parameters.ContainsKey(CommandTypeKey))
                CommandType = parameters.GetValue<CommandTypeEnum>(CommandTypeKey);
        }

        protected virtual void CancelCmd() => RequestClose.Invoke(ButtonResult.Cancel);

        protected virtual void SubmitCmd(T dto) => RequestClose.Invoke(ButtonResult.OK);
    }
}
