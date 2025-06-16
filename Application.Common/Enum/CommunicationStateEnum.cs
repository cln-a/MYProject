using System.ComponentModel;

namespace Application.Common
{
    public enum CommunicationStateEnum : int
    {
        [Description("通讯断开")] NotConnected = 0,
        [Description("连接中")] Communicating = 1,
        [Description("已连接")] Connected = 2
    }
}
