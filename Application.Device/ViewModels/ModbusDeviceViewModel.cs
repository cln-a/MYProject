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

        protected async override Task<PageResult<ModbusDeviceDto>> GetPage()
        {
            var result = await ModbusDeviceDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<ModbusDeviceDto>(x));
        }
    }
}
