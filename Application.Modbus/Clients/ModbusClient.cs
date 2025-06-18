using Application.Common;
using CommonServiceLocator;
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

    }
}