using Application.Model;

namespace Application.Mapper
{
    public class DtosMapperProfile : AutoMapper.Profile
    {
        public DtosMapperProfile() 
        {
            CreateMap<ModbusDevice, ModbusDeviceDto>().ReverseMap();
            CreateMap<ModbusRegister, ModbusRegisterDto>().ReverseMap();
            CreateMap<S7netDevice, S7netDeviceDto>().ReverseMap();
            CreateMap<S7netRegister, S7netRegisterDto>().ReverseMap();
            CreateMap<PartsInfo, PartsInfoDto>().ReverseMap();
            CreateMap<SinglePartInfo, SinglePartInfoDto>().ReverseMap();
        }
    }
}
