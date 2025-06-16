using Application.Common;

namespace Application.Modbus
{
    public class UnwriteableException : Exception
    {
        public UnwriteableException(IVariable variable) : base($"{variable.Path}没有写入权限！") { }
    }
}
