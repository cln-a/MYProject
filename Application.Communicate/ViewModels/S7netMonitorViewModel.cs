using Application.Common;
using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;
using System.Windows;

namespace Application.Communicate
{
    public class S7netMonitorViewModel : BasePageViewModel<S7netRegisterDto>
    {
        private readonly IS7netRegisterDAL _s7NetRegisterDAL;

        public IS7netRegisterDAL S7NetRegisterDAL => _s7NetRegisterDAL;

        public S7netMonitorViewModel(IS7netRegisterDAL s7NetRegisterDAL)
            => this._s7NetRegisterDAL = s7NetRegisterDAL;

        protected async override Task<PageResult<S7netRegisterDto>> GetPage()
        {
            var result = await S7NetRegisterDAL.GetPage(pageNumber, pageSize);
            return result.Map(x => Mapper.Map<S7netRegisterDto>(x));
        }

        protected override void WriteValueCmd(S7netRegisterDto dto)
        {
            try
            {
                if (IO.TryGet(dto.RegisterUri!, out var variable))
                {
                    variable.WriteAnyValue(dto.WriteValue);
                    InfoGlobal($"写入{dto.Description}-值:{dto.WriteValue}完成");
                }
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message);
            }
        }

        protected override void PageUpdateCmd()
        {
            base.PageUpdateCmd();
            ItemEventBinding();
        }

        private void ItemEventBinding()
        {
            foreach (var item in Items)
            {
                if (IO.TryGet(item.RegisterUri!, out var variable))
                {
                    variable.ValueChangedEvent += Variable_ValueChangedEvent!;
                    item.Value = variable.AnyValue;
                }
            }
        }

        private void Variable_ValueChangedEvent(object sender, Common.ValueChangedEventArgs<object> e)
        {
            try
            {
                var item = Items.Where(x => x.RegisterUri == e.Variable.Path).FirstOrDefault();
                if (item != null)
                    item.Value = e.NewValue;
            }
            catch (Exception ex)
            {
                ErrorGlobal(ex.Message);
            }
        }
    }
}
