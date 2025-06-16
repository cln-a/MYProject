using System.ComponentModel;

namespace Application.Common
{
    [Flags]
    public enum ModbusDataType : int
    {
        /// <summary>
        /// 保持寄存器：16位，读写
        /// </summary>
        [Description("保持寄存器")] HoldingRegister = 0,

        /// <summary>
        /// 输入寄存器：16位，只读
        /// </summary>
        [Description("输入寄存器")] InputRegister = 1,

        /// <summary>
        /// 线圈：1位，读写
        /// </summary>
        [Description("线圈")] Coil = 2,

        /// <summary>
        /// 离散量输入：1位，只读
        /// </summary>
        [Description("输入")] Input = 3
    }
}
