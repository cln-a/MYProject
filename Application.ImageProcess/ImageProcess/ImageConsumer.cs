using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Application.ImageProcess
{
    public class ImageConsumer : IConsumer
    {
        private readonly BlockingCollection<ImageData> _imageQueue;
        private readonly ILogger _logger;
        public readonly IImageProcessor _imageProcessor;
        private CancellationTokenSource _cancellationTokenSource;

        public ILogger Logger => _logger;

        public ImageConsumer(BlockingCollection<ImageData> imageQueue, ILogger logger, IImageProcessor imageProcessor)
        {
            this._imageQueue = imageQueue;   
            this._logger = logger;  
            this._imageProcessor = imageProcessor;  

            _cancellationTokenSource = new CancellationTokenSource();   
        }

        public void StartConsum()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    foreach (var image in _imageQueue.GetConsumingEnumerable(_cancellationTokenSource.Token))
                    {
                        try
                        {
                            _imageProcessor.Process(image);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e.ToString());
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.LogDebug("图像消费线程已取消");
                }
                catch (Exception ex)
                {
                    Logger.LogDebug(ex, "消费者线程发生未处理异常");
                }
            }, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void StopConsum()
        {
            _cancellationTokenSource.Cancel();
            _imageQueue.CompleteAdding();
        }
    }
}
