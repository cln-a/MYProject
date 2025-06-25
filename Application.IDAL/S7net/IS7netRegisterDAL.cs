using Application.Model;

namespace Application.IDAL
{
    public interface IS7netRegisterDAL : IBaseDomainDAL<S7netRegister>
    {
        public List<S7netRegister> GetAllReadableByDeviceId(int deviceId);
    }
}
