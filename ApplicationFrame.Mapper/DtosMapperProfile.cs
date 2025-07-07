using Application.Model;

namespace Application.Mapper
{
    public class DtosMapperProfile : AutoMapper.Profile
    {
        public DtosMapperProfile() 
        {
            CreateMap<ModbusDevice, ModbusDeviceDto>().ReverseMap();
        }
    }
}
