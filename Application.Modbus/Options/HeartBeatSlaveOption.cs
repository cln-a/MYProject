using Microsoft.Extensions.Options;

namespace Application.Modbus
{
    public class HeartBeatSlaveOption : IOptions<HeartBeatSlaveOption>
    {
        public HeartBeatSlaveOption Value => this;
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string VariablePath { get; set; }
        public int TimeOut { get; set; }
    }
}
