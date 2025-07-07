using Application.Common;
using Application.IDAL;
using Application.Model;
using CommonServiceLocator;
using Microsoft.Extensions.Logging;
using Modbus.Device;

namespace Application.Modbus
{
    public class ModbusClient
    {
        private volatile bool _readRunning;
        private List<ModbusVariable> _variables;
        /// <summary>
        /// 线圈
        /// </summary>
        private List<BitMessage> _coils;
        /// <summary>
        /// 保持寄存器
        /// </summary>
        private List<RegisterMessage> _holdingRegisters;
        /// <summary>
        /// 离散输入
        /// </summary>
        private List<BitMessage> _inputs;
        /// <summary>
        /// 输入寄存器
        /// </summary>
        private List<RegisterMessage> _inputRegisters;
        private PlcState _plcState;
        private ModbusIpMaster _master;
        private Model.ModbusDevice _modbusDevice;

        [Unity.Dependency] public ILogger Logger { get; set; }  
        
        public Model.ModbusDevice DeviceModel => _modbusDevice;
        public byte SlaveId => DeviceModel.slaveId;
        public long DeviceId => DeviceModel.Id;
        public string DeviceName => DeviceModel.DeviceName;
        public bool Connected => _plcState.IsConnected;
        public List<BitMessage> Coils => _coils;
        public List<RegisterMessage> HoldingRegisters => _holdingRegisters;
        public List<BitMessage> Inputs => _inputs;
        public List<RegisterMessage> InputRegisters => _inputRegisters;

        public event EventHandler<ModbusClientConnectedEventArgs> ModbusClientConnectedEvent;

        public ModbusClient(Model.ModbusDevice device)
        {
            this._modbusDevice = device;
            this._coils = new List<BitMessage>();
            this._holdingRegisters = new List<RegisterMessage>();
            this._inputs = new List<BitMessage>();
            this._inputRegisters = new List<RegisterMessage>();
            _plcState = (PlcState)ServiceLocator.Current.GetInstance<ICommunicationStateMachine>(DeviceModel.DeviceUri);
            _plcState.ConnectEvent += _plcState_ConnectEvent;
            _plcState.DisConnectEvent += _plcState_DisConnectEvent;
        }

        private void _plcState_ConnectEvent(object? sender, EventArgs e)
        {
            try
            {
                //谁主动发起连接（Connect）并且发出Modbus请求（如读写寄存器），谁就是主站
                //改行代码用于创建一个ModbusTCP主站
                _master = ModbusIpMaster.CreateIp(_plcState.Client);
                ModbusClientConnectedEvent?.Invoke(this,new ModbusClientConnectedEventArgs(_modbusDevice,true));
            }
            catch (Exception ex)
            {
                //Logger
                _plcState.SetDisConnected();
            }
        }

        private void _plcState_DisConnectEvent(object? sender, EventArgs e)
        {
            try
            {
                ModbusClientConnectedEvent?.Invoke(this, new ModbusClientConnectedEventArgs(_modbusDevice, false));
            }
            catch (Exception ex)
            {
                //Logger
                _plcState.SetDisConnected();
            }
        }

        public void Start()
        {
            try
            {
                _plcState.Start();
                if (!_readRunning)
                {
                    _readRunning = true;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ReadAction));
                }
            }
            catch(Exception ex)
            {
                _readRunning = false;
                //Logger
                throw;
            }
        }

        public void Stop() => _readRunning = false;

        public void DisConnect() => _plcState.SetDisConnected();

        private void ReadAction(object? state)
        {
            while (_readRunning)
            {
                try
                {
                    try
                    {
                        if (Connected && _master != null)
                        {
                            foreach (var coil in _coils)
                            {
                                var data = _master.ReadCoils(SlaveId, coil.StartAddress, coil.DataLength);
                                if (data.Length > 0)
                                    coil.SetData(data);
                                else
                                    Logger.LogDebug("{0}获取PLC线圈数据失败,地址{1}长度{2}", _modbusDevice.DeviceName, coil.StartAddress, coil.DataLength);
                            }
                            
                            foreach (var holdingRegister in _holdingRegisters)
                            {
                                var data = _master.ReadHoldingRegisters(SlaveId, holdingRegister.StartAddress, holdingRegister.DataLength);
                                if (data.Length > 0)
                                    holdingRegister.SetData(data);
                                else
                                    Logger.LogDebug("{0}获取PLC保持寄存器数据失败,地址{1}长度{2}", _modbusDevice.DeviceName, holdingRegister.StartAddress, holdingRegister.DataLength);
                            }
                            foreach (var input in _inputs)  
                            {
                                var data = _master.ReadInputs(SlaveId, input.StartAddress, input.DataLength);
                                if (data.Length > 0)
                                    input.SetData(data);
                                else
                                    Logger.LogDebug("{0}获取PLC输入数据失败,地址{1}长度{2}", SlaveId, input.StartAddress, input.DataLength);
                            }
                            foreach (var inputRegister in _inputRegisters)
                            {
                                var data = _master.ReadHoldingRegisters(SlaveId, inputRegister.StartAddress, inputRegister.DataLength);
                                if (data.Length > 0)
                                    inputRegister.SetData(data);
                                else
                                    Logger.LogDebug("{0}获取PLC输入寄存器数据失败,地址{1}长度{2}", _modbusDevice.DeviceName, inputRegister.StartAddress, inputRegister.DataLength);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        //Logger
                        DisConnect();
                    }
                    finally
                    {
                        Thread.Sleep(_modbusDevice.ReadInterval);
                    }
                }
                catch(Exception e)
                {
                    //Logger
                }
            }
        }

        /// <summary>
        /// 异步写多个线圈
        /// </summary>
        /// <param name="register"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task WriteValueAsync(ModbusRegister register, bool[] value)
        {
            if (Connected)
            {
                switch (register.ModbusType)
                {
                    case ModbusDataType.Coil:
                        //Modbus协议中，写多个线圈（coils）时，需要提供一组bool值，每个值代表一个线圈的状态
                        var sendData = new bool[register.NumberOfPoints];
                        //防止拷贝超出任意一个数组的范围，确保不会出现数组越界异常
                        Array.Copy(value, 0, sendData, 0, new int[] { value.Length, sendData.Length }.Min());
                        try
                        {
                            return _master.WriteMultipleCoilsAsync(SlaveId, register.StartAddress, sendData);
                        }
                        catch (Exception ex) 
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                    default:
                        break;
                }
            }
            return null!;
        }

        /// <summary>
        /// 异步写多个保持寄存器
        /// </summary>
        /// <param name="register"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task WriteValueAsync(ModbusRegister register, ushort[] value)
        {
            if (Connected)
            {
                switch (register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        var sendData = new ushort[register.NumberOfPoints];
                        //用字节级精度控制复制长度（例如按数据总字节数复制，不只是个数），就得用 Buffer.BlockCopy
                        //这里转换为字节进行拷贝时的×2其实也对应了一个寄存器 == ushort == 16bit（2字节）== byte(1个字节)*2（起到了处理数据长度和对齐的作用）
                        Buffer.BlockCopy(value, 0, sendData, 0, new int[] { value.Length * 2, sendData.Length * 2 }.Min());
                        try
                        {
                            return _master.WriteMultipleRegistersAsync(SlaveId, register.StartAddress, sendData);
                        }
                        catch (Exception ex)
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                    default:
                        break;
                }
            }
            return null!;
        }

        /// <summary>
        /// 同步写多个线圈
        /// </summary>
        /// <param name="register"></param>
        /// <param name="value"></param>
        public void WriteValue(ModbusRegister register, bool[] value)
        {
            if (Connected)
            {
                switch (register.ModbusType)
                {
                    case ModbusDataType.Coil:
                        var sendData = new bool[register.NumberOfPoints];
                        //这一步的Array.Copy以及下文的Buffer.BlockCopy其实是防御性编程
                        Array.Copy(value, 0, sendData, 0, new int[] { value.Length, sendData.Length }.Min());
                        try
                        {
                            _master.WriteMultipleCoils(SlaveId, register.StartAddress, sendData);
                        }
                        catch(Exception ex)
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 同步写多个保持寄存器
        /// </summary>
        public void WriteValue(ModbusRegister register, ushort[] value)
        {
            if (Connected)
            {
                switch (register.ModbusType) 
                {
                    case ModbusDataType.HoldingRegister:
                        var sendData = new ushort[register.NumberOfPoints];
                        Buffer.BlockCopy(value, 0, sendData, 0, new int[] { value.Length * 2, sendData.Length * 2 }.Min());
                        try
                        {
                            _master.WriteMultipleRegisters(SlaveId, register.StartAddress, sendData);
                        }
                        catch(Exception ex)
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 异步读多个线圈
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public async Task<bool[]> ReadMultipleCoilsAsync(ModbusRegister register)
        {
            if (Connected)
            {
                switch (register.ModbusType)
                {
                    case ModbusDataType.Coil:
                        try
                        {
                            return await _master.ReadCoilsAsync(SlaveId, register.StartAddress, register.NumberOfPoints);
                        }
                        catch (Exception ex)
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                    default:
                        break;
                }
            }
            return new bool[register.NumberOfPoints];
        }

        /// <summary>
        /// 异步读多个保持寄存器
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public async Task<ushort[]> ReadHoldingRegistersAsync(ModbusRegister register)
        {
            if (Connected)
            {
                switch (register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        try
                        {
                            return await _master.ReadHoldingRegistersAsync(SlaveId, register.StartAddress, register.NumberOfPoints);
                        }
                        catch (Exception ex)
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                    default:
                        break;
                }
            }
            return new ushort[register.NumberOfPoints];
        }

        /// <summary>
        /// Unicode占用多个寄存器数，字节数可变，故需要特殊处理，Unicode是字符串，解析方式与基本数据完全不同(按字符解析，不能按位处理)
        /// 
        /// 为什么不能只写数据：
        /// 因为字符串是变长的内容,Modbus本身不支持“动态长度字段”
        /// 所以我们要手动：
        /// (1)把字符串转换为ushort[]写入寄存器
        /// (2)再额外写入字符串长度
        /// (3)从站就能正确截取字符串。避免读出多余空值或乱码
        /// </summary>
        /// <param name="register"></param>
        /// <param name="value"></param>
        /// <param name="addLengh">字符串实际占用的寄存器数量(可能包含填充)</param>
        /// <param name="strLength">字符串原始字符数（真正的字符串长度）</param>
        public void WriteUnicodeValue(ModbusRegister register, ushort[] value, int addLengh, int strLength)
        {
            if (Connected) 
            {
                switch (register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        try
                        {
                            //Modbus 寄存器是 16 位（即 2 字节），正好可以存一个 Unicode 字符
                            _master.WriteMultipleRegisters(SlaveId, register.StartAddress, value);
                            var length = new ushort[2] { (ushort)addLengh, (ushort)strLength };
                            _master.WriteMultipleRegisters(SlaveId, (ushort)(register.StartAddress - 2), length);
                        }
                        catch(Exception ex)
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void WriteSingleValue(ModbusRegister register, ushort[] value)
        {
            if (Connected)
            {
                switch (register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        try
                        {
                            _master.WriteMultipleRegisters(SlaveId, register.StartAddress, value);
                        }
                        catch(Exception ex)
                        {
                            //Logger
                            DisConnect();
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 初始化所有变量（从数据库查出的所有设备并分组）
        /// </summary>
        public void Init()
        {
            if (_variables == null)
                _variables = new List<ModbusVariable>();
            else
                _variables.Clear();
            var registerDAL = ServiceLocator.Current.GetInstance<IModbusRegisterDAL>();
            var tempList = registerDAL.GetAllReadableByDeviceId(_modbusDevice.Id);
            var readList = tempList.OrderBy(x=>x.StartAddress).ToList();
            foreach ( var item in readList ) 
            {
                if (IO.TryGet(item.RegisterUri.Trim(), out ModbusVariable variable))
                    _variables.Add(variable);
            }
            var groupList = _variables.GroupBy(x => x.Model.ModbusType);
            foreach (var group in groupList)
            {
                switch ((global::Modbus.Data.ModbusDataType)(group.Key))
                {
                    case global::Modbus.Data.ModbusDataType.HoldingRegister:
                        InitRegisters(global::Modbus.Data.ModbusDataType.HoldingRegister, ref _holdingRegisters, 0, group.ToList());
                        break;
                    case global::Modbus.Data.ModbusDataType.InputRegister:
                        InitRegisters(global::Modbus.Data.ModbusDataType.InputRegister, ref _inputRegisters, 0, group.ToList());
                        break;
                    case global::Modbus.Data.ModbusDataType.Coil:
                        InitBit(global::Modbus.Data.ModbusDataType.Coil, ref _coils, 0, group.ToList());
                        break;
                    case global::Modbus.Data.ModbusDataType.Input:
                        InitBit(global::Modbus.Data.ModbusDataType.Input, ref _inputs, 0, group.ToList());
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 递归的将varList中的ModbusVariable按地址区间拆分为一系列RegisterMessage对象，加入到message列表中
        /// </summary>
        /// <param name="dataType">表示寄存器的数据类型（例如输入寄存器，保持寄存器等）</param>
        /// <param name="messages">引用传入的消息列表，用于存储生成的RegisterMessage</param>
        /// <param name="firstIndex">当前处理的起始索引</param>
        /// <param name="varList">所有变量的列表，每个变量都包含一个Model，其定义了StartAddress和NumberOfPoints</param>
        private void InitRegisters(global::Modbus.Data.ModbusDataType dataType, ref List<RegisterMessage> messages, int firstIndex, List<ModbusVariable> varList)
        {
            //获取起始变量
            var first = varList[firstIndex];
            if (first != null)
            {
                // 查找最大连续区域的末尾变量
                var lastIndex = varList.FindLastIndex(x => x.Model.StartAddress + x.Model.NumberOfPoints <= first.Model.StartAddress + 100);
                //如果找到合适的末尾变量，则合并生成一个RegisterMessage
                if (lastIndex != -1)
                {
                    var last = varList[lastIndex];
                    if (last != null)
                    {
                        var msg = new RegisterMessage(dataType,
                            first.Model.StartAddress,
                            (ushort)(last.Model.StartAddress + last.Model.NumberOfPoints - first.Model.StartAddress),
                            varList.GetRange(firstIndex, lastIndex - firstIndex + 1));
                        messages.Add(msg);
                    }
                    //递归处理剩余的变量
                    if(lastIndex + 1< varList.Count)
                        InitRegisters(dataType, ref messages, lastIndex + 1, varList);
                }
            }
        }

        private void InitBit(global::Modbus.Data.ModbusDataType dataType, ref List<BitMessage> messages, int firstIndex, List<ModbusVariable> varList)
        {
            var first = varList[firstIndex];
            if (first != null)
            {
                var lastIndex = varList.FindLastIndex(x => x.Model.StartAddress + x.Model.NumberOfPoints <= first.Model.StartAddress + 100);
                if (lastIndex != -1)
                {
                    var last = varList[lastIndex];
                    if (last != null)
                    {
                        var msg = new BitMessage(dataType,
                            first.Model.StartAddress,
                            (ushort)(last.Model.StartAddress + last.Model.NumberOfPoints - first.Model.StartAddress),
                            varList.GetRange(firstIndex, lastIndex - firstIndex + 1));
                        messages.Add(msg);
                    }
                    //递归处理剩余的变量
                    if (lastIndex + 1 < varList.Count)
                        InitBit(dataType, ref messages, lastIndex + 1, varList);
                }
            }
        }
    }
}