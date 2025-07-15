using Application.Common;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.IO;

namespace ApplicationFrame.ImportUtil
{
    public class ImportUtilHaiLu : IImportUtil
    {
        private readonly ILogger _logger;
        private readonly List<string> _folderPaths;
        private readonly string _historyFolderPath;
        private List<FileSystemWatcher> _watchers;
        private ConcurrentQueue<string> _fileQueue;
        private CancellationTokenSource _cts;
        private string[] _supportFileTypes;

        public bool Enabled { get; set; }

        public ImportUtilHaiLu(List<string> folderPaths, string historyFolderPath, ILogger logger)
        {
            this._folderPaths = folderPaths;
            this._historyFolderPath = historyFolderPath;
            this._watchers = new List<FileSystemWatcher>();
            this._logger = logger;
            foreach (var path in folderPaths)
            {
                var watcher = new FileSystemWatcher(path);
                _watchers.Add(watcher);
            }
            _fileQueue = new ConcurrentQueue<string>();
            _cts = new CancellationTokenSource();
            _supportFileTypes = new string[] { ".xls", ".xlsx" };
        }

        private int InitFileQueue()
        {
            foreach (var folderPath in _folderPaths)
            {
                var files = Directory.GetFiles(folderPath, "*.xls*");
                foreach (var file in files)
                    _fileQueue.Enqueue(file);
            }
            return _fileQueue.Count;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string newFilePath = e.FullPath;

            if (_fileQueue.Contains(newFilePath))
            {
                _logger.LogDebug($"文件{newFilePath}的数据导入已准备就绪，将不会被添加到队列！");
                return;
            }

            if (File.Exists(newFilePath))
            {
                if (_supportFileTypes.Contains(Path.GetExtension(newFilePath).ToLower()))
                {
                    _logger.LogDebug($"文件{newFilePath}已创建，准备导入数据！");
                    _fileQueue.Enqueue(newFilePath);
                }
                else
                    _logger.LogDebug($"新创建的文件{newFilePath}使用了不受支持的文件格式，将不会导入该文件数据！");
            }
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string newFilePath = e.FullPath;

            if (_fileQueue.Contains(newFilePath))
            {
                _logger.LogDebug($"文件{newFilePath}的数据导入已准备就绪，将不会被添加到队列！");
                return;
            }

            if (File.Exists(newFilePath))
            {
                if (_supportFileTypes.Contains(Path.GetExtension(newFilePath).ToLower()))
                {
                    _logger.LogDebug($"文件{newFilePath}已下载完成，准备导入数据！");
                    _fileQueue.Enqueue(newFilePath);
                }
            }
            else
                _logger.LogDebug($"重命名的文件{newFilePath}使用了不受支持的文件格式，将不会导入该文件数据！");
        }

        private bool IsFileReady(string filePath)
        {
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (fileStream.Length > 0)
                        return true;
                    else
                        _logger.LogDebug($"文件{filePath}未就绪！");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return false;
        }

        private void MoveFileToHistoryFolder(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string destinationPath = Path.Combine(_historyFolderPath, fileName);
                if (File.Exists(destinationPath))
                    File.Delete(destinationPath);
                File.Move(filePath, destinationPath);
            }
            catch (Exception e)
            {
                _logger.LogDebug($"移动板件数据文件{filePath}失败：{e.Message}");
            }
        }

        private void ProcessQueue()
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                if (_fileQueue.TryDequeue(out string filePath))
                {
                    if (!File.Exists(filePath))
                    {
                        _logger.LogDebug($"文件{filePath}已被移除，跳过该文件的数据导入！");
                        continue;
                    }

                    if (!IsFileReady(filePath))
                        _fileQueue.Enqueue(filePath);
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        ExecuteImport();
                        MoveFileToHistoryFolder(filePath);
                    }
                }
                else
                    Thread.Sleep(1000);
            }
        }

        public void ExecuteImport()
        {
            
        }

        public void StartImport()
        {
            if (Enabled)
                return;
            _logger.LogDebug($"文件夹{InitFileQueue()}个初始文件的数据导入已准备就绪！");
            foreach (var watcher in _watchers)
            {
                watcher.Created += Watcher_Created;
                watcher.Renamed += Watcher_Renamed; ;
                watcher.EnableRaisingEvents = true;
            }
            Task.Factory.StartNew(ProcessQueue, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            Enabled = true;
        }

        public void StopImport()
        {
            foreach (var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = false;
            }
            _cts.Cancel();
            Enabled = false;
            _logger.LogDebug("文件自动导入服务已停止！");
        }
    }
}
