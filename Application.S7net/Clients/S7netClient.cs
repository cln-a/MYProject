using Application.Common;
using Application.DAL;
using Application.Model;
using CommonServiceLocator;
using S7.Net;
using System.Runtime.CompilerServices;

namespace Application.S7net
{
    public class S7netClient
    {
        private object _lockobj = new object();
        private System.Timers.Timer _timer;
        private List<S7netVariable> _variables;
        private List<RegisterMessage> _s7Messages;
        private S7netDevice _s7netDevice;
        private Plc _plcClient;
        
        public S7netDevice DeviceModel => _s7netDevice;
        public Plc PlcClient => _plcClient;
        public int DeviceId => DeviceModel.Id;
        public string DeviceName => DeviceModel.DeviceName;
        public CpuType PlcCpuType => /*_s7netDevice.DeviceBrand.GetValueByDescription<CpuType>();*/ CpuType.S71500;
        public string IpAddress => _s7netDevice.RemoteIpAddress;
        public int Port => _s7netDevice.RemotePort;
        public short Rack => _s7netDevice.DeviceRack;
        public short Slot => _s7netDevice.DeviceSlot;
        public bool Connected => _plcClient.IsConnected;
        public int ConnectedInterval => _s7netDevice.ReconnectInterval;

        public event EventHandler ConnectEvent;
        public event EventHandler DisConnectEvent;

        public S7netClient(S7netDevice device)
        {
            this._s7netDevice = device;
            this._s7Messages = new List<RegisterMessage>();
            this._plcClient = new Plc(PlcCpuType, IpAddress, Port, Rack, Slot);
            this._timer = new System.Timers.Timer() { AutoReset = false };
            this._timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!Connected)
            {
                try
                {
                    _plcClient.Open();
                    SetConnected();
                }
                catch(Exception ex)
                {
                    //Logger
                    DisConnected();
                }
            }
        }

        public void Start()
        {
            try
            {
                _timer.Start();
                ThreadPool.QueueUserWorkItem(ReadAction);
            }
            catch(Exception ex)
            {
                //Logger
                throw;
            }
        }

        public void Stop()
        {
            if (_plcClient != null && _plcClient.IsConnected)
                _plcClient.Close();
            _plcClient = null!;
        }

        private void SetConnected()
        {
            if (Connected)
            {
                _timer.Stop();
                _timer.AutoReset = false;
                ConnectEvent?.Invoke(this, new EventArgs());
            }
        }

        private void DisConnected()
        {
            DisConnectEvent?.Invoke(this, new EventArgs());
            _timer.Interval = ConnectedInterval;
            _timer.Start();
        }

        private void ReadAction(object? state)
        {
            while (true)
            {
                try
                {
                    try
                    {
                        if (Connected && _plcClient != null)
                        {
                            foreach (var s7Register in _s7Messages)
                            {
                                byte[] data = (byte[])_plcClient.Read(DataType.DataBlock, s7Register.DbNumber, s7Register.StartAddress, VarType.Byte, s7Register.DataLength)!;
                                if (data.Length > 0)
                                    s7Register.SetData(data);
                                //else
                                    //Logger
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //Logger.LogDebug(e, e.Message);
                        DisConnected();
                    }
                    finally
                    {
                        Thread.Sleep(_s7netDevice.ReadInterval);
                    }
                }
                catch (Exception e)
                {
                    //Logger
                }
            }
        }

        public void WriteValue(S7netRegister register, byte[] value)
        {
            if (Connected)
            {
                try
                {
                    PlcClient.Write(DataType.DataBlock, register.DbNumber, register.StartAddress, value);
                }
                catch (Exception e)
                {
                    //Logger
                    DisConnected();
                    throw;
                }
            }
        }

        /// <summary>
        /// 为什么写字符串数据的时候不是从StartAddress开始写？
        /// (1) 第0字节：是MaxLen，PLC设计的时候就已经设定，一般不改
        /// (2) 第1字节：是CurLen，告诉PLC字符串当前长度是多少（必须写这个）
        /// (3) 第2字节：才是字符串的内容
        /// </summary>
        /// <param name="register"></param>
        /// <param name="value"></param>
        public void WriteValueForString(S7netRegister register, byte[] value)
        {
            if (Connected)
            {
                try
                {
                    //WriteBytes：直接向指定地址写入原始字节数组(byte[]),不做数据类型转换
                    //此处写入第一字节：是CurLen，告诉PLC字符串当前长度是多少
                    PlcClient.WriteBytes(DataType.DataBlock, register.DbNumber, register.StartAddress + 1, new byte[] { Convert.ToByte(value.Length) });
                    //Write：向PLC写入一个已知类型的数据，自动完成类型到字节的转换
                    PlcClient.Write(DataType.DataBlock, register.DbNumber, register.StartAddress + 2, value.Where(b => b != 0).ToArray());
                }
                catch (Exception e)
                {
                    //Logger
                    DisConnected();
                    throw;
                }
            }
        }

        /// <summary>
        /// 初始化所有变量（从数据库查出的所有设备并分组）
        /// </summary>
        public void Init()
        {
            if (_variables == null)
                _variables = [];
            else
                _variables.Clear();
            var registerBLL = ServiceLocator.Current.GetInstance<S7netRegisterDAL>();
            try
            {
                var tempList = registerBLL.GetAllReadableByDeviceId(_s7netDevice.Id);
                var readList = tempList.OrderBy(x => x.StartAddress).ToList();
                foreach (var item in readList)
                {
                    if (IO.TryGet(item.RegisterUri.Trim(), out S7netVariable variable))
                        _variables.Add(variable);
                }
                InitRegisters(ref _s7Messages, 0, _variables);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void InitRegisters(ref List<RegisterMessage> messages, int firstIndex, List<S7netVariable> varList)
        {
            //获取起始变量
            var first = varList[firstIndex];
            var dbNumber = first.DbNumber;
            if (first != null)
            {
                //查找最大连续区域的末尾变量
                var lastIndex = varList.FindLastIndex(x => x.StartAddress + x.NumberOfPoints <= first.StartAddress + 100);
                //如果找到合适的末尾变量，则合并生成一个RegisterMessage
                if (lastIndex != -1)
                {
                    var last = varList[lastIndex];
                    if (last != null)
                    {
                        var msg = new RegisterMessage(first.StartAddress,
                            (ushort)(last.StartAddress + last.NumberOfPoints - first.StartAddress), dbNumber,
                            varList.GetRange(firstIndex, lastIndex - firstIndex + 1));
                        messages.Add(msg);
                    }
                    //递归处理剩余的变量
                    if (lastIndex + 1 < varList.Count)
                        InitRegisters(ref messages, lastIndex + 1, varList);
                }
            }
        }
    }
}
