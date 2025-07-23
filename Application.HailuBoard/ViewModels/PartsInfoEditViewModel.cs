using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;

namespace Application.HailuBoard
{
    public class PartsInfoEditViewModel : BaseEditViewModel<PartsInfoDto>
    {
        private readonly IPartsInfoDAL _partsInfoDAL;

        public PartsInfoEditViewModel(IPartsInfoDAL partsInfoDAL)
        => this._partsInfoDAL = partsInfoDAL;

        protected override void SubmitCmd(PartsInfoDto dto)
        {
            if (dto != null)
            {
                switch (CommandType)
                {
                    case Common.CommandTypeEnum.Add:
                        _partsInfoDAL.SingleInsertAsync(Mapper.Map<PartsInfo>(dto));
                        break;
                    case Common.CommandTypeEnum.Edit:
                        _partsInfoDAL.UpdatePartsInfoAsync(Mapper.Map<PartsInfo>(dto));
                        break;
                }
            }
            RequestClose.Invoke(ButtonResult.OK);
        }
    }
}
