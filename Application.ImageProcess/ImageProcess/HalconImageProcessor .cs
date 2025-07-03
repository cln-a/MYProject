using Application.Common.Helper;
using Application.Common;
using HalconDotNet;
using Microsoft.Extensions.Logging;

namespace Application.ImageProcess
{
    public class HalconImageProcessor : IImageProcessor
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        public ILogger Logger => _logger;

        public HalconImageProcessor(IEventAggregator eventAggregator, ILogger logger)
        {
            this._eventAggregator = eventAggregator;    
            this._logger = logger;
        }

        public void Process(ImageData image)
        {
            try
            {
                Logger.LogDebug($"Consume image: {image.Width}x{image.Height}, {image.PixelType}, size={image.Buffer.Length}");

                HObject halconImage
                    = ImageConvertHelper.ConvertToHalconImage(image.Width, image.Height, image.PixelType, image.pData);

                _eventAggregator.GetEvent<ImageProcessedEvent>().Publish(halconImage);
                Logger.LogDebug($"Consume image Completed！");
            }
            catch(Exception ex)
            {
                Logger.LogDebug(ex.ToString());
            }
        }
    }
}
