using System.Windows.Input;

namespace Application.S7net
{
    public class S7netMessage
    {
        protected ushort _dataLength;
        protected int _dbNumber;
        protected List<S7netVariable> _variables;

        public ushort StartAddress { get; set; }
        public ushort DataLength { get => _dataLength; }
        public int DbNumber { get => _dbNumber; }
        public List<S7netVariable> Variables => _variables;

        public S7netMessage(ushort startAddress, ushort dataLength, int dbNumber, List<S7netVariable> variables) 
        {
            this.StartAddress = startAddress;
            this._dataLength = dataLength;
            this._dbNumber = dbNumber;
            this._variables = variables;
        }
    }

    public class S7netMessage<T> : S7netMessage where T : IComparable
    {
        public S7netMessage(ushort startAddress, ushort dataLength, int dbNumber, List<S7netVariable> variables) 
            : base(startAddress, dataLength, dbNumber, variables)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                //S7的StartAddress本身就是字节地址，已经是字节偏移，无需再转换
                //S7协议直接操作连续的字节数组，就像访问byte[]，直接用偏移量拿就行
                variables[i].Index = variables[i].RegisterModel.StartAddress - variables[0].RegisterModel.StartAddress;
                variables[i].Message = this;
            }
        }
    }

    public class RegisterMessage : S7netMessage<ushort>
    {
        public RegisterMessage(ushort startAddress, ushort dataLength, int dbNumber, List<S7netVariable> variables) 
            : base(startAddress, dataLength, dbNumber, variables)
        {
        }

        public void SetData(byte[] value)
        {
            foreach (var variable in Variables)
            {
                variable.SetValue(value, variable.Index);
            }
        }
    }
}
