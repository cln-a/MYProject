using System.Collections.ObjectModel;
using System.IO;

namespace Application.Journal
{
    public class JournalViewModel : BindableBase
    {
        private DelegateCommand _openLogFolderCommand;
        private ObservableCollection<FileInfo> _logFiles;
        private FileInfo _selectedLogFile;

        public DelegateCommand OpenLogFolderCommand => _openLogFolderCommand ??= new DelegateCommand(OpenLogFolderCmd);
        public ObservableCollection<FileInfo> LogFiles { get => _logFiles; set => SetProperty(ref _logFiles, value); }
        public FileInfo SelectedLogFile { get => _selectedLogFile; set => SetProperty(ref _selectedLogFile, value); }

        public JournalViewModel() 
        {
            _logFiles = new ObservableCollection<FileInfo>();
        }

        private void OpenLogFolderCmd()
        {
        }
    }
}
