using Application.Common;
using System.Net.Sockets;

namespace Application.Modbus.Clients
{
    public class PlcState : ICommunicationStateMachine
    {
        private readonly Model.ModbusDevice _device;
        private TcpClient _tcpClient;
        private CommunicationStateEnum _state;
        private System.Timers.Timer _timer;

        public string Name => _device.DeviceName;

        public string Description => _device.Description;
        public string RemoteIPAddress { get; set; }
        public string LocalIPAddress { get; set; }
        public int RemotePort { get; set; }
        public int LocalPort => _device.LocalPort;
        public int ConnectInterval { get; set; }
        public bool Enabled { get; set; }
        public bool IsConnected => Enabled && State == CommunicationStateEnum.Connected;
        public bool IsNotConnected => Enabled && State == CommunicationStateEnum.NotConnected;
        public bool IsCommunicating => Enabled && State == CommunicationStateEnum.Communicating;
        public TcpClient TcpClient => _tcpClient;
        public CommunicationStateEnum State 
        {
            get => _state;
            private set
            {
                if (_state != value)
                {
                    _state = value;
                    if (Enabled)
                    {
                        switch (_state)
                        {
                            case CommunicationStateEnum.Connected:
                                ConnectEvent?.Invoke(this, new EventArgs());
                                break;
                            case CommunicationStateEnum.NotConnected:
                                DisconnectEvent?.Invoke(this, new EventArgs());
                                break;
                            case CommunicationStateEnum.Communicating:
                                break;
                            default:
                                break;
                        }
                        CommunicationStateChangedEvent?.Invoke(this, new CommunicationStateChangedEventArgs(Name, Description, State));
                    }
                }
            }
        }

        public event EventHandler ConnectEvent;
        public event EventHandler DisconnectEvent;
        public event EventHandler<CommunicationStateChangedEventArgs> CommunicationStateChangedEvent;

        public PlcState(Model.ModbusDevice device)
        {
            this._device = device;
            Init(_device.LocalIpAddress, _device.RemoteIpAddress, _device.RemotePort, _device.ReconnectInterval);
        }

        public PlcState(string localIpAddress, string remoteIpAddress, int remotePort, int reconnectInterval)
            => Init(localIpAddress, remoteIpAddress, remotePort, reconnectInterval);

        private void Init(string localIpAddress, string remoteIpAddress, int remotePort, int reconnectInterval)
        {
            this.LocalIPAddress = localIpAddress;
            this.RemoteIPAddress = remoteIpAddress;
            this.RemotePort = remotePort;
            this.ConnectInterval = reconnectInterval;
            _state = CommunicationStateEnum.NotConnected;
            _timer = new System.Timers.Timer(ConnectInterval) { AutoReset = false, Interval = ConnectInterval };
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsConnected)
            {
                SetCommunicating();
                try
                {
                    _tcpClient = new TcpClient();
                    if (!string.IsNullOrEmpty(LocalIPAddress))
                        _tcpClient.Client.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(LocalIPAddress), LocalPort));
                    _tcpClient.Connect(RemoteIPAddress, RemotePort);
                    SetConnected();
                }
                catch (Exception)
                {
                    //Loger
                    SetDisConnected();
                }
            }
        }

        public void Start()
        {
            if (!Enabled)
            {
                Enabled = true;
                SetDisConnected();
            }
        }

        public void Stop()
        {
            Enabled = false;
            if (_tcpClient != null && _tcpClient.Connected) 
                _tcpClient.Close();
            _tcpClient = null!;
        }

        /// <summary>
        /// 初始化时设置状态为正在连接中
        /// </summary>
        public void SetCommunicating()
        {
            if (IsNotConnected)
                State = CommunicationStateEnum.Communicating;
        }

        /// <summary>
        /// 初始化时设置状态为已连接
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
        /// 出现异常情况时处理，可以理解为断线重连操作
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
    }
}
