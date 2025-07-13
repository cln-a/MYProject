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
        private object _lockobj = new object();
        private volatile bool _readRunning;
        private List<ModbusVariable> _variables;
        private List<BitMessage> _coils;
        private List<RegisterMessage> _holdingRegisters;
        private List<BitMessage> _inputs;
        private List<RegisterMessage> _inputRegisters;
        private PLCState _plcState;
        private global::Modbus.Device.ModbusIpMaster _master;
        private Model.ModbusDevice _modbusDevice;
        public event EventHandler<ModbusClientConnectedEventArgs> ModbusClientConnectedEvent;
        [Dependency] public ILogger Logger { get; set; }

        public byte SlaveId => DeviceModel.slaveId;
        public long DeviceId => DeviceModel.Id;
        public string DeviceName => DeviceModel.DeviceName!;
        public Model.ModbusDevice DeviceModel => _modbusDevice;
        public bool Connected => _plcState.IsConnected;
        public List<BitMessage> Coils => _coils;
        public List<RegisterMessage> HoldingRegisters => _holdingRegisters;
        public List<BitMessage> Inputs => _inputs;
        public List<RegisterMessage> InputRegisters => _inputRegisters;

        public ModbusClient(Model.ModbusDevice device)
        {
            _modbusDevice = device;
            _coils = new List<BitMessage>();
            _holdingRegisters = new List<RegisterMessage>();
            _inputs = new List<BitMessage>();
            _inputRegisters = new List<RegisterMessage>();
            _plcState = ServiceLocator.Current.GetInstance<ICommunicationStateMachine>(DeviceModel.DeviceUri) as PLCState;
            _plcState.ConnectEvent += PlcState_ConnectEvent;
            _plcState.DisConnectEvent += PlcState_DisConnectEvent;
        }

        private void PlcState_DisConnectEvent(object sender, EventArgs e)
        {
            try
            {
                ModbusClientConnectedEvent?.Invoke(this, new ModbusClientConnectedEventArgs(_modbusDevice, false));
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, ex.Message);
                _plcState.SetDisConnected();
            }
        }

        private void PlcState_ConnectEvent(object sender, EventArgs e)
        {
            try
            {
                _master = ModbusIpMaster.CreateIp(_plcState.Client);
                ModbusClientConnectedEvent?.Invoke(this, new ModbusClientConnectedEventArgs(_modbusDevice, true));
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, ex.Message);
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
            catch (Exception ex)
            {
                _readRunning = false;
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public void Stop()
        {
            _readRunning = false;
        }

        public void DisConnect()
        {
            _plcState.SetDisConnected();
        }

        private void ReadAction(object state)
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
                                    Logger.LogError("{0}获取PLC线圈数据失败,地址{1}长度{2}", _modbusDevice.DeviceName, coil.StartAddress, coil.DataLength);
                            }
                            foreach (var holdingRegister in _holdingRegisters)
                            {
                                var data = _master.ReadHoldingRegisters(SlaveId, holdingRegister.StartAddress, holdingRegister.DataLength);
                                if (data.Length > 0)
                                    holdingRegister.SetData(data);
                                else
                                    Logger.LogError("{0}获取PLC保持寄存器数据失败,地址{1}长度{2}", _modbusDevice.DeviceName, holdingRegister.StartAddress, holdingRegister.DataLength);
                            }
                            foreach (var inputRegister in _inputRegisters)
                            {
                                var data = _master.ReadHoldingRegisters(SlaveId, inputRegister.StartAddress, inputRegister.DataLength);
                                if (data.Length > 0)
                                    inputRegister.SetData(data);
                                else
                                    Logger.LogError("{0}获取PLC输入寄存器数据失败,地址{1}长度{2}", _modbusDevice.DeviceName, inputRegister.StartAddress, inputRegister.DataLength);
                            }
                            foreach (var input in _inputs)
                            {
                                var data = _master.ReadInputs(SlaveId, input.StartAddress, input.DataLength);
                                if (data.Length > 0)
                                    input.SetData(data);
                                else
                                    Logger.LogError("{0}获取PLC输入数据失败,地址{1}长度{2}", input.StartAddress, input.DataLength);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Logger.LogDebug(e, e.Message);
                        DisConnect();
                    }
                    finally
                    {
                        Thread.Sleep(_modbusDevice.ReadInterval);
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError(e, e.Message);
                }
            }
        }

        public Task WriteValueAsync(ModbusRegister register, bool[] value)
        {
            switch ((ModbusDataType)register.ModbusType)
            {
                case ModbusDataType.Coil:
                    var sendData = new bool[register.NumberOfPoints];
                    Array.Copy(value, 0, sendData, 0, new int[] { value.Length, sendData.Length }.Min());
                    return _master?.WriteMultipleCoilsAsync(SlaveId, (ushort)register.StartAddress, sendData)!;
                default:
                    break;
            }
            return null!;
        }

        public async Task<ushort[]> ReadHoldingRegistersAsync(ModbusRegister register)
        {
            if (Connected)
            {
                switch ((ModbusDataType)register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        try
                        {
                            return await _master.ReadHoldingRegistersAsync(SlaveId, (ushort)register.StartAddress,
                                (ushort)register.NumberOfPoints);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e, e.Message);
                            DisConnect();
                            throw;
                        }
                    default:
                        break;
                }
            }
            return new ushort[register.NumberOfPoints];
        }

        public async Task<bool[]> ReadMultipleCoilsAsync(ModbusRegister register)
        {
            if (Connected)
            {
                switch ((ModbusDataType)register.ModbusType)
                {
                    case ModbusDataType.Coil:
                        try
                        {
                            return await _master.ReadCoilsAsync(SlaveId, (ushort)register.StartAddress, (ushort)register.NumberOfPoints);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e, e.Message);
                            DisConnect();
                            throw;
                        }
                    default:
                        break;
                }
            }
            return new bool[register.NumberOfPoints];
        }

        public void WriteValue(ModbusRegister register, bool[] value)
        {
            if (Connected)
            {
                switch ((ModbusDataType)register.ModbusType)
                {
                    case ModbusDataType.Coil:
                        var sendData = new bool[register.NumberOfPoints];
                        Array.Copy(value, 0, sendData, 0, new int[] { value.Length, sendData.Length }.Min());
                        try
                        {
                            _master?.WriteMultipleCoils(SlaveId, (ushort)register.StartAddress, sendData);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e, e.Message);
                            DisConnect();
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public Task WriteValueAsync(ModbusRegister register, ushort[] value)
        {
            if (Connected)
            {
                switch ((ModbusDataType)register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        var sendData = new ushort[register.NumberOfPoints];
                        Buffer.BlockCopy(value, 0, sendData, 0, new int[] { value.Length * 2, sendData.Length * 2 }.Min());
                        try
                        {
                            return _master?.WriteMultipleRegistersAsync(SlaveId, (ushort)register.StartAddress, sendData)!;
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e, e.Message);
                            DisConnect();
                            throw;
                        }
                    default:
                        break;
                }
            }
            return null!;
        }

        public void WriteUnicodeValue(ModbusRegister register, ushort[] value, int addLengh, int strLength)
        {
            if (Connected)
            {
                switch ((ModbusDataType)register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        try
                        {
                            _master?.WriteMultipleRegisters(SlaveId, (ushort)register.StartAddress, value);
                            var length = new ushort[2] { (ushort)addLengh, (ushort)strLength };
                            _master?.WriteMultipleRegisters(SlaveId, (ushort)(register.StartAddress - 2), length);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e, e.Message);
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
                switch ((ModbusDataType)register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        try
                        {
                            _master?.WriteMultipleRegisters(SlaveId, (ushort)register.StartAddress, value);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e, e.Message);
                            DisConnect();
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void WriteValue(ModbusRegister register, ushort[] value)
        {
            if (Connected)
            {
                switch ((ModbusDataType)register.ModbusType)
                {
                    case ModbusDataType.HoldingRegister:
                        var sendData = new ushort[register.NumberOfPoints];
                        Buffer.BlockCopy(value, 0, sendData, 0, new int[] { value.Length * 2, sendData.Length * 2 }.Min());
                        try
                        {
                            _master?.WriteMultipleRegisters(SlaveId, (ushort)register.StartAddress, sendData);
                        }
                        catch (Exception e)
                        {
                            Logger.LogDebug(e, e.Message);
                            DisConnect();
                            throw;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        const ushort DataStartAddress = 0x3217;

        public void SendData(ushort address, List<ushort> data = null!)
        {
            if (Connected)
            {
                try
                {
                    if (data == null || data.Count == 0)
                        _master?.WriteSingleRegister(address == 0 ? DataStartAddress : address, 0);
                    else
                    {
                        int index = 0;
                        while (index + 100 < data.Count)
                        {
                            _master?.WriteMultipleRegisters(Convert.ToUInt16(DataStartAddress + index), data.GetRange(index, 100).ToArray());
                            index += 100;
                        }
                        if (index < data.Count)
                            _master?.WriteMultipleRegisters(Convert.ToUInt16(DataStartAddress + index), data.GetRange(index, data.Count - index).ToArray());
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Init()
        {
            if (_variables == null)
                _variables = new List<ModbusVariable>();
            else
                _variables.Clear();
            IModbusRegisterDAL registerDAL = ServiceLocator.Current.GetInstance<IModbusRegisterDAL>();
            var tempList = registerDAL.GetAllReadableByDeviceId(_modbusDevice.Id).ToList();
            var readList = tempList.OrderBy(x => x.StartAddress).ToList();
            foreach (var item in readList)
            {
                if (IO.TryGet(item.RegisterUri!.Trim(), out ModbusVariable variable))
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
                        var msg = new BitMessage(dataType, (ushort)first.Model.StartAddress,
                            (ushort)(last.Model.StartAddress + last.Model.NumberOfPoints - first.Model.StartAddress),
                            varList.GetRange(firstIndex, lastIndex - firstIndex + 1));
                        messages.Add(msg);
                    }
                    if (lastIndex + 1 < varList.Count)
                        InitBit(dataType, ref messages, lastIndex + 1, varList);
                }
            }
        }

        private void InitRegisters(global::Modbus.Data.ModbusDataType dataType, ref List<RegisterMessage> messages, int firstIndex, List<ModbusVariable> varList)
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
                        var msg = new RegisterMessage(dataType, first.Model.StartAddress,
                            (ushort)(last.Model.StartAddress + last.Model.NumberOfPoints - first.Model.StartAddress),
                            varList.GetRange(firstIndex, lastIndex - firstIndex + 1));
                        messages.Add(msg);
                    }
                    if (lastIndex + 1 < varList.Count)
                        InitRegisters(dataType, ref messages, lastIndex + 1, varList);
                }
            }
        }
    }
}