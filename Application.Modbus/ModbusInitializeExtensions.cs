using Application.Common;
using Application.Model;

namespace Application.Modbus
{
    public static class ModbusInitializeExtensions
    {
        public static void RegisterVariableType<T>(this IUnityContainer container,
           ModbusRegister register, ModbusDevice device) where T : IComparable
        {
            if (!string.IsNullOrEmpty(device.DeviceBrand))
                IO.Add(register.RegisterUri.Trim(), new ModbusVariable<T>(register, device));
            else
                IO.Add(register.RegisterUri.Trim(), new ModbusVariable<T>(register, device));
        }

        public static void RegisterVariableType(this IUnityContainer container,
          ModbusRegister register, ModbusDevice device)
        {
            switch (register.ValueDataType)
            {
                case ValueDataType.Boolean:
                    container.RegisterVariableType<bool>(register, device);
                    break;
                case ValueDataType.Byte:
                    container.RegisterVariableType<byte>(register, device);
                    break;
                case ValueDataType.Int16:
                    container.RegisterVariableType<short>(register, device);
                    break;
                case ValueDataType.UInt16:
                case ValueDataType.Word:
                    container.RegisterVariableType<ushort>(register, device);
                    break;
                case ValueDataType.Int32:
                    container.RegisterVariableType<int>(register, device);
                    break;
                case ValueDataType.UInt32:
                case ValueDataType.DWord:
                    container.RegisterVariableType<uint>(register, device);
                    break;
                case ValueDataType.Int64:
                    container.RegisterVariableType<long>(register, device);
                    break;
                case ValueDataType.UInt64:
                    container.RegisterVariableType<ulong>(register, device);
                    break;
                case ValueDataType.Single:
                    container.RegisterVariableType<float>(register, device);
                    break;
                case ValueDataType.Double:
                    container.RegisterVariableType<double>(register, device);
                    break;
                case ValueDataType.String:
                    container.RegisterVariableType<string>(register, device);
                    break;
                case ValueDataType.Ascii:
                    container.RegisterVariableType<string>(register, device);
                    break;
                case ValueDataType.Unicode:
                    container.RegisterVariableType<string>(register, device);
                    break;
                default:
                    break;
            }
        }
    }
}
