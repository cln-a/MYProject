namespace Application.S7net
{
    public class UnwriteableException : Exception
    {
        public UnwriteableException(Common.IVariable variable) : base(string.Format("{0}没有写入权限!", variable.Path))
        {
        }
    }
}
