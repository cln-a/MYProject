using Application.Common;
using Application.Mapper;
using System.Windows;
using System.Windows.Controls;

namespace Application.UI
{
    public class IOMonitorDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CheckBoxTemplate { get; set; }
        public DataTemplate NumericUpDownTemplate { get; set; }
        public DataTemplate ByteNumericUpDownTemplate { get; set; }
        public DataTemplate Int16NumericUpDownTemplate { get; set; }
        public DataTemplate UInt16NumericUpDownTemplate { get; set; }
        public DataTemplate Int32NumericUpDownTemplate { get; set; }
        public DataTemplate UInt32NumericUpDownTemplate { get; set; }
        public DataTemplate Int64NumericUpDownTemplate { get; set; }
        public DataTemplate UInt64NumericUpDownTemplate { get; set; }
        public DataTemplate SingleNumericUpDownTemplate { get; set; }
        public DataTemplate DoubleNumericUpDownTemplate { get; set; }
        public DataTemplate TextBoxTemplate { get; set; }
        public DataTemplate WordNumericUpDownTemplate { get; set; }
        public DataTemplate DWordNumericUpDownTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ModbusRegisterDto modbusDto)
            {
                return modbusDto.ValueDataType switch
                {
                    ValueDataType.Boolean => CheckBoxTemplate,
                    ValueDataType.Byte => ByteNumericUpDownTemplate,
                    ValueDataType.Int16 => Int16NumericUpDownTemplate,
                    ValueDataType.UInt16 => UInt16NumericUpDownTemplate,
                    ValueDataType.Int32 => Int32NumericUpDownTemplate,
                    ValueDataType.UInt32 => UInt32NumericUpDownTemplate,
                    ValueDataType.Int64 => Int64NumericUpDownTemplate,
                    ValueDataType.UInt64 => UInt64NumericUpDownTemplate,
                    ValueDataType.Single => SingleNumericUpDownTemplate,
                    ValueDataType.Double => DoubleNumericUpDownTemplate,
                    ValueDataType.Word => WordNumericUpDownTemplate,
                    ValueDataType.DWord => DWordNumericUpDownTemplate,
                    _ => TextBoxTemplate,
                };
            }
            return base.SelectTemplate(item, container);
        }
    }
}
