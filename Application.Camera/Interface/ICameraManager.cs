using MvCamCtrl.NET;

namespace Application.Camera
{
    public interface ICameraManager
    {
        List<MyCamera.MV_CC_DEVICE_INFO> EnumerateDevices();
    }
}
