using MvCamCtrl.NET;

namespace Application.Camera
{
    public interface ICameraFactory
    {
        ICamera Create(MyCamera.MV_CC_DEVICE_INFO device);
    }
}
