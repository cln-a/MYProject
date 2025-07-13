using Application.Common;
using CommonServiceLocator;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace Application.Modbus
{
    public class PLCState : ICommunicationStateMachine
    {
        public string Name => _device.DeviceUri!;
        public string Description => _device.DeviceName!;
        public string RemoteIPAddress { set; get; }
        public string LocalIPAddress { set; get; }
        public int Port { set; get; }
        public int ConnectInterval { get; set; }
        public ILogger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = ServiceLocator.Current.GetInstance<ILogger>();
                return _logger;
            }
        }
        public bool Enable { get; set; }
        public bool IsConnected { get => Enable && State == CommunicationStateEnum.Connected; }
        public bool IsNotConnected { get => Enable && State != CommunicationStateEnum.Connected; }
        public bool IsCommunicating { get => Enable && State == CommunicationStateEnum.Communicating; }

        public TcpClient Client { get => _tcpClient; }

        public event EventHandler ConnectEvent;
        public event EventHandler DisConnectEvent;
        public event EventHandler<CommunicationStateChangedEventArgs> CommunicationStateChangedEvent;

        public PLCState(Model.ModbusDevice device)
        {
            _device = device;
            Init(_device.LocalIpAddress!, _device.RemoteIpAddress!, _device.RemotePort, _device.ReconnectInterval);
        }

        public PLCState(string localIp, string remoteIp, int port, int interval)
        {
            Init(localIp, remoteIp, port, interval);
        }

        public void Init(string localIp, string remoteIp, int port, int interval)
        {
            LocalIPAddress = localIp;
            RemoteIPAddress = remoteIp;
            Port = port;
            ConnectInterval = interval;
            _state = CommunicationStateEnum.NotConnected;
            timer = new System.Timers.Timer(ConnectInterval)
            {
                AutoReset = false
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, EventArgs e)
        {
            if (!IsConnected)
            {
                SetCommunicating();
                try
                {
                    _tcpClient = new TcpClient();
                    if (!string.IsNullOrEmpty(LocalIPAddress))
                        _tcpClient.Client.Bind(new IPEndPoint(IPAddress.Parse(LocalIPAddress), 0));
                    _tcpClient.Connect(RemoteIPAddress, Port);
                    SetConnected();
                }
                catch (Exception ex)
                {
                    Logger.LogDebug(ex, ex.Message);
                    SetDisConnected();
                }
            }
        }

        private void SetCommunicating()
        {
            if (IsNotConnected)
                State = CommunicationStateEnum.Communicating;
        }

        private void SetConnected()
        {
            if (IsCommunicating)
            {
                timer.Stop();
                timer.AutoReset = false;
                State = CommunicationStateEnum.Connected;
            }
        }

        public void SetDisConnected()
        {
            if (IsConnected)
                State = CommunicationStateEnum.NotConnected;
            timer.Interval = ConnectInterval;
            if (IsNotConnected)
                timer.Start();
            else
                timer.Stop();
        }

        public IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                    _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
                return _eventAggregator;
            }
        }

        public CommunicationStateEnum State
        {
            get => _state;
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
                                ConnectEvent?.Invoke(this, new EventArgs());
                                break;
                            case CommunicationStateEnum.NotConnected:
                                DisConnectEvent?.Invoke(this, new EventArgs());
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
            if (_tcpClient != null && _tcpClient.Connected)
                _tcpClient.Close();
            _tcpClient = null;
        }

        private System.Timers.Timer timer;
        private ILogger _logger;
        private TcpClient _tcpClient;
        private IEventAggregator _eventAggregator;
        private readonly Model.ModbusDevice _device;
        private CommunicationStateEnum _state;
        private readonly object _tcpLock = new object();
    }
}
