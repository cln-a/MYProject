using Application.Common;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace Application.Modbus.Clients
{
    public class PlcState : ICommunicationStateMachine
    {
        private System.Timers.Timer _timer = null!;
        private TcpClient _tcpClient = null!;
        private readonly Model.ModbusDevice _device = null!;
        private CommunicationStateEnum _state;

        public string Name => _device.DeviceUri;
        public string Description => _device.DeviceName;
        private string RemoteIpAddress { set; get; } = null!;
        private string LocalIpAddress { set; get; } = null!;
        private int RemotePort { set; get; }
        private int LocalPort => _device.LocalPort;
        private int ConnectInterval { get; set; }
        private bool Enable { get; set; }
        public bool IsConnected => Enable && State == CommunicationStateEnum.Connected; 
        public bool IsNotConnected => Enable && State != CommunicationStateEnum.Connected;
        public bool IsCommunicating => Enable && State == CommunicationStateEnum.Communicating;

        public CommunicationStateEnum State
        {
            get  => _state;
            private set
            {
                if (_state != value)
                {
                    _state = value;
                    if (Enable)
                    {
                        switch (_state)
                        {
                            case CommunicationStateEnum.Connected:
                                ConnectEvent?.Invoke(this, EventArgs.Empty);
                                break;
                            case CommunicationStateEnum.NotConnected:
                                DisConnectEvent?.Invoke(this, EventArgs.Empty);
                                break;
                            case CommunicationStateEnum.Communicating:
                                break;
                        }
                        CommunicationStateChangedEvent?.Invoke(this,
                            new CommunicationStateChangedEventArgs(Name, Description, State));
                    }
                }
            }
        }
        public TcpClient Client => _tcpClient;

        public event EventHandler ConnectEvent = null!;
        public event EventHandler DisConnectEvent = null!;
        public event EventHandler<CommunicationStateChangedEventArgs>? CommunicationStateChangedEvent;

        public PlcState(Model.ModbusDevice device)
        {
            _device = device;
            Init(_device.LocalIpAddress, _device.RemoteIpAddress, _device.RemotePort, _device.ReconnectInterval);
        }

        public PlcState(string localIp, string remoteIp, int port, int interval) =>
            Init(localIp, remoteIp, port, interval);
        
        
        private void Init(string localIpAddress, string remoteIpAddress, int remotePort, int reconnectInterval)
        {
            LocalIpAddress = localIpAddress;
            RemoteIpAddress = remoteIpAddress;
            RemotePort = remotePort;
            ConnectInterval = reconnectInterval;
            _state = CommunicationStateEnum.NotConnected;
            _timer = new System.Timers.Timer(ConnectInterval) { AutoReset = false, Interval = ConnectInterval };
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
        {
            if (!IsConnected)
            {
                SetCommunicating();
                try
                {
                    _tcpClient = new TcpClient();
                    if (!string.IsNullOrEmpty(LocalIpAddress))
                        _tcpClient.Client.Bind(new IPEndPoint(IPAddress.Parse(LocalIpAddress), LocalPort));
                    _tcpClient.Connect(RemoteIpAddress, RemotePort);
                    SetConnected();
                }
                catch (Exception ex)
                {
                    SetDisConnected();
                }
            }
        }
        
        /// <summary>
        /// 设置状态为正在连接中
        /// </summary>
        public void SetCommunicating()
        {
            if (IsNotConnected)
                State = CommunicationStateEnum.Communicating;
        }

        /// <summary>
        /// 设置状态为已经连接
        /// </summary>
        public void SetConnected()
        {
            if (IsCommunicating)
            {
                _timer.Stop();
                State = CommunicationStateEnum.Connected;
            }
        }

        /// <summary>
        /// 该方法为出现异常时的重连方法
        /// </summary>
        public void SetDisConnected()
        {
            if (IsConnected)
                State = CommunicationStateEnum.NotConnected;
            if (IsNotConnected)
                _timer.Start();
            else
                _timer.Stop();
        }
        
        public void Start()
        {
            if (!Enable)
            {
                Enable = true;
                SetDisConnected();
            }
        }

        public void Stop()
        {
            Enable = false;
            if (_tcpClient.Connected)
                _tcpClient.Close();
            _tcpClient = null!;
        }
    }
}
