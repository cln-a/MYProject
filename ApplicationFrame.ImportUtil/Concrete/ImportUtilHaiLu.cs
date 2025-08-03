using Application.Common;
using Application.Hailu;
using Application.IDAL;
using Application.Model;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Concurrent;
using System.IO;

namespace Application.ImportUtil
{
    public class ImportUtilHaiLu : IImportUtil
    {
        private readonly ILogger _logger;
        private readonly IPartsInfoDAL _partsInfoDAL;
        private readonly IEventAggregator _eventAggregator;
        private readonly List<string> _folderPaths;
        private readonly string _historyFolderPath;
        private List<FileSystemWatcher> _watchers;
        private ConcurrentQueue<string> _fileQueue;
        private CancellationTokenSource _cts;
        private string[] _supportFileTypes;

        public bool Enabled { get; set; }

        public ImportUtilHaiLu(List<string> folderPaths, 
            string historyFolderPath, 
            ILogger logger, 
            IPartsInfoDAL partsInfoDAL,
            IEventAggregator eventAggregator)
        {
            this._folderPaths = folderPaths;
            this._historyFolderPath = historyFolderPath;
            this._watchers = new List<FileSystemWatcher>();
            this._logger = logger;
            this._partsInfoDAL = partsInfoDAL;
            this._eventAggregator = eventAggregator;
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
                if (_fileQueue.TryDequeue(out var filePath))
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
                        ExecuteImport(filePath);
                        MoveFileToHistoryFolder(filePath);
                    }
                }
                else
                    Thread.Sleep(1000);
            }
        }

        public void ExecuteImport(string filePath)
        {
            try
            {
                _logger.LogDebug($"准备读取文件 {filePath}");
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read)) 
                {
                    IWorkbook workbook;
                    if (filePath.EndsWith(".xls"))
                    {
                        workbook = new HSSFWorkbook(fileStream);
                    }
                    else if (filePath.EndsWith(".xlsx"))  
                    {
                        workbook = new XSSFWorkbook(fileStream);
                    }
                    else
                    {
                        _logger.LogError("所导入文件格式不为EXCEL文件,已终止导入");
                        return;
                    }
                    var sheet = workbook.GetSheetAt(0);
                    var partsInfoList = new List<PartsInfo>();
                    var identities = new HashSet<string>();
                    for (var rowIndex = 2; rowIndex <= sheet.LastRowNum; rowIndex++) 
                    {
                        var row = sheet.GetRow(rowIndex);
                        if (row == null)
                            continue;
                        var partsinfo = new PartsInfo();
                        partsinfo.BatchCode = row.GetCell(0).ToString(); //合同号
                        partsinfo.Batch = row.GetCell(1).ToString();     //批次
                        partsinfo.Name = row.GetCell(5).ToString(); //名称
                        partsinfo.Identity = row.GetCell(0).ToString() + row.GetCell(1).ToString() + row.GetCell(5).ToString(); //唯一标识符
                        partsinfo.Description = row.GetCell(6).ToString();//型号
                        partsinfo.Length = ushort.Parse(row.GetCell(7).ToString()!);//长度L
                        partsinfo.Width1 = ushort.Parse(row.GetCell(8).ToString()!);//宽度W1
                        partsinfo.Thickness = ushort.Parse(row.GetCell(10).ToString()!);//厚度
                        partsinfo.Quautity = ushort.Parse(row.GetCell(13).ToString()!);//数量
                        partsinfo.Countinfo = 0; //数量信息
                        partsinfo.HoleLengthRight = row.GetCell(17) != null ? ushort.Parse(row.GetCell(17).ToString()!) : (ushort)0;//右侧铣刀长度
                        partsinfo.HoleDistanceRight = row.GetCell(18) != null ? ushort.Parse(row.GetCell(18).ToString()!) : (ushort)0;//右侧铣刀距离底部距离
                        partsinfo.HoleLengthMiddle = row.GetCell(21) != null ? ushort.Parse(row.GetCell(21).ToString()!) : (ushort)0;//中间铣刀长度
                        partsinfo.HoleDistanceMiddle = row.GetCell(22) != null ? ushort.Parse(row.GetCell(22).ToString()!) : (ushort)0;//中间铣刀距离底部距离
                        partsinfo.HoleLengthLeft = row.GetCell(25) != null ? ushort.Parse(row.GetCell(25).ToString()!) : (ushort)0;//左侧铣刀长度
                        partsinfo.HoleDistanceLeft = row.GetCell(26) != null ? ushort.Parse(row.GetCell(26).ToString()!) : (ushort)0;//左侧铣刀距离底部距离
                        partsinfo.McOrNot = ((row.GetCell(17).ToString() != null) || (row.GetCell(18).ToString() != null) || (row.GetCell(21) != null) || (row.GetCell(22) != null) || (row.GetCell(25) != null) || (row.GetCell(26) != null)) ? true : false;
                        partsInfoList.Add(partsinfo);
                        identities.Add(partsinfo.Identity);
                    }
                    _partsInfoDAL.BatchInsertWithOutSame(partsInfoList, identities);
                    _eventAggregator.GetEvent<RefreshUiEvent>().Publish();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                _logger.LogError($"文件导入失败:{filePath}");
            }
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
