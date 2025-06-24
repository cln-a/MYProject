using Microsoft.Extensions.Options;

namespace Application.S7net
{
    public class HeartBeatMasterOption : IOptions<HeartBeatMasterOption>
    {
        public HeartBeatMasterOption Value => this;
        
        public string DeviceName { get; set; }
        
        public string VariablePath { get; set; }
        
        public int HeartBeatInterval { get; set; }
        
        public ushort[] HeartBeatValues { get; set; }
    }
}
