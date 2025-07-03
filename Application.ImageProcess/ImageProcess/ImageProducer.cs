using Application.Common;
using System.Collections.Concurrent;

namespace Application.ImageProcess
{
    public class ImageProducer : IProducer
    {
        private readonly BlockingCollection<ImageData> _imageQueue;
        private readonly IEventAggregator _eventAggregator;

        public ImageProducer(BlockingCollection<ImageData> imagequeue, IEventAggregator eventAggregator)
        {
            this._imageQueue = imagequeue;  
            this._eventAggregator = eventAggregator;
        }

        public void StartProducer() 
            => _eventAggregator.GetEvent<CameraFrameEvent>().Subscribe(OnCameraFrameReceived);

        private void OnCameraFrameReceived(CameraFrameEventArgs args)
        {
            var image = new ImageData(args.Buffer, args.Width, args.Height, args.PixelType, args.pData);
            _imageQueue.Add(image);
        }
    }
}
