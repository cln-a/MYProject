using HalconDotNet;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Application.Common;
using Application.Common.Helper;

namespace Application.ImageProcess
{
    public class ImageConsumer : IConsumer
    {
        private readonly BlockingCollection<ImageData> _imageQueue;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;
        private CancellationTokenSource _cancellationTokenSource;

        public ILogger Logger => _logger;

        public ImageConsumer(BlockingCollection<ImageData> imageQueue, IEventAggregator eventAggregator, ILogger logger)
        {
            this._imageQueue = imageQueue;   
            this._eventAggregator = eventAggregator;
            this._logger = logger;  

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
                            Logger.LogDebug($"Consume image: {image.Width}x{image.Height}, {image.PixelType}, size={image.Buffer.Length}");

                            HObject halconImage
                                = ImageConvertHelper.ConvertToHalconImage(image.Width, image.Height, image.PixelType, image.pData);

                            _eventAggregator.GetEvent<ImageProcessedEvent>().Publish(halconImage);
                            Logger.LogDebug($"Consume image Completed！");

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
            //_cancellationTokenSource.Cancel();
            _imageQueue.CompleteAdding();
        }
    }
}
