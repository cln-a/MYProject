using Application.Mapper;

namespace Application.HailuBoard
{
    public class ViewModelDtoMapepr
    {
        public ViewModelDtoMapepr()
        {
            
        }

        public PartsInfoDto GetNewPartsInfoDtoModel(PartsInfoDto dto)
        => new PartsInfoDto()
        {
            Id = dto.Id,
            BatchCode = dto.BatchCode,
            Batch = dto.Batch,
            Identity = dto.Identity,
            Code = dto.Code,
            Name = dto.Name,
            Length = dto.Length,
            Width1 = dto.Width1,
            Thickness = dto.Thickness,
            HoleLengthRight = dto.HoleLengthRight,
            HoleDistanceRight = dto.HoleDistanceRight,
            HoleLengthMiddle = dto.HoleLengthMiddle,
            HoleDistanceMiddle = dto.HoleDistanceMiddle,
            HoleLengthLeft = dto.HoleLengthLeft,
            HoleDistanceLeft = dto.HoleDistanceLeft,
            Quautity = dto.Quautity,
            Creator = dto.Creator,
            CreateTime = dto.CreateTime,
            Updater = dto.Updater,
            UpdateTime = dto.UpdateTime,
            IsEnabled = dto.IsEnabled,
            McOrNot = dto.McOrNot,
            Description = dto.Description,
            Countinfo = dto.Countinfo
        };
    }
}
