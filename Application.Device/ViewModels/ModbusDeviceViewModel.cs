using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;

namespace Application.Device
{
    public class ModbusDeviceViewModel : BasePageViewModel<ModbusDeviceDto>
    {
        private readonly IModbusDeviceDAL _modbusDeviceDAL;

        public IModbusDeviceDAL ModbusDeviceDAL => _modbusDeviceDAL;

        public ModbusDeviceViewModel(IModbusDeviceDAL modbusDeviceDAL)
        {
            this._modbusDeviceDAL = modbusDeviceDAL;
        }

        protected override PageResult<ModbusDeviceDto> GetPage()
            => ModbusDeviceDAL.GetPage(pageNumber, pageSize).Map(Mapper.Map<ModbusDeviceDto>);
    }
}
