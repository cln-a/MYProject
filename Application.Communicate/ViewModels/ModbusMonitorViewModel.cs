using Application.Common;
using Application.IDAL;
using Application.Mapper;
using Application.Model;
using Application.UI;
using Application.UI.Dialog;

namespace Application.Communicate
{
    public class ModbusMonitorViewModel : BasePageViewModel<ModbusRegisterDto>
    {
        private readonly IModbusRegisterDAL _modbusRegisterDAL;

        public IModbusRegisterDAL ModbusRegisterDAL => _modbusRegisterDAL;

        public ModbusMonitorViewModel(IModbusRegisterDAL modbusRegisterDAL)
        {
            this._modbusRegisterDAL = modbusRegisterDAL;    
        }

        protected override PageResult<ModbusRegisterDto> GetPage()
            => ModbusRegisterDAL.GetPage(pageNumber, pageSize).Map(Mapper.Map<ModbusRegisterDto>);

        protected override void WriteValueCmd(ModbusRegisterDto dto)
        {
            try
            {
                if (IO.TryGet(dto.RegisterUri, out var variable))
                {
                    variable.WriteAnyValue(dto.WriteValue);
                    PopupBox.Show($"写入{dto.Description}-值:{dto.WriteValue}完成");
                }
            }
            catch (Exception ex)
            {
                PopupBox.Show(ex.Message);
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
                PopupBox.Show(ex.Message);
            }
        }
    }
}
