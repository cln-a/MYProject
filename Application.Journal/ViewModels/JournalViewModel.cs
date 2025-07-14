using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Windows;

namespace Application.Journal
{
    public class JournalViewModel : BindableBase
    {
        private readonly ILogger _logger;
        private DelegateCommand _openLogFolderCommand;
        private DelegateCommand<FileInfo> _selectedLogFileChangedCommand;
        private ObservableCollection<FileInfo> _logFiles;
        private ObservableCollection<LogEntry> _logEntries;
        private FileInfo _selectedLogFile;


        public ILogger Logger => _logger;
        public DelegateCommand OpenLogFolderCommand => _openLogFolderCommand ??= new DelegateCommand(OpenLogFolderCmd);
        public DelegateCommand<FileInfo> SelectedLogFileChangedCommand => 
            _selectedLogFileChangedCommand ??= new DelegateCommand<FileInfo>(SelectedLogFileChangedCmd);

        public ObservableCollection<FileInfo> LogFiles { get => _logFiles; set => SetProperty(ref _logFiles, value); }
        public ObservableCollection<LogEntry> LogEntries { get => _logEntries; set => SetProperty(ref _logEntries, value); }
        public FileInfo SelectedLogFile { get => _selectedLogFile; set => SetProperty(ref _selectedLogFile, value); }

        public JournalViewModel(ILogger logger) 
        {
            this._logFiles = new ObservableCollection<FileInfo>();
            this._logEntries = new ObservableCollection<LogEntry>();
            this._logger = logger;
        }

        private void OpenLogFolderCmd()
        {
            try
            {
                LogFiles.Clear();

                var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (Directory.Exists(logPath)) 
                {
                    //遍历一周的日志文件
                    var currentData = DateTime.Now;
                    var oneWeekAgo = currentData.AddDays(-30);

                    //获取所有以日期命名的子文件夹
                    var dataFolders = Directory.GetDirectories(logPath)
                        .Where(dir => DateTime.TryParse(Path.GetFileName(dir), out _))
                        .Select(dir => new DirectoryInfo(dir));

                    var recentFolders = dataFolders.Where(dir =>
                    {
                        if (DateTime.TryParse(dir.Name, out DateTime folderDate))
                        {
                            return folderDate >= oneWeekAgo && folderDate <= currentData;
                        }
                        return false;
                    });

                    //获取满足日期的文件夹下的所有日志文件
                    var logFiles = recentFolders
                        .SelectMany(dir => dir.GetFiles("*.log", SearchOption.AllDirectories))
                        .OrderByDescending(f => f.LastWriteTime);

                    LogFiles = new ObservableCollection<FileInfo>(logFiles);
                }
            }
            catch(Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }

        private void SelectedLogFileChangedCmd(FileInfo value)
        {
            if (value != null)
            {
                try
                {
                    NLog.LogManager.Flush(); // 强制刷新日志到磁盘

                    var entries = new ObservableCollection<LogEntry>();
                    // 用 FileStream + StreamReader 以共享方式读取
                    using (var fs = new FileStream(value.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs))
                    {
                        string line;
                        while ((line = sr.ReadLine()!) != null)
                        {
                            var entry = LogEntry.Prase(line);
                            if (entry != null)
                                entries.Add(entry);
                        }
                    }
                    LogEntries = new ObservableCollection<LogEntry>(entries);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex.Message);
                }
            }
        }
    }
}
