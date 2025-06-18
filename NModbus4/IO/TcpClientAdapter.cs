namespace Modbus.IO
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Threading; 
    using Unme.Common;

    /// <summary>
    ///     Concrete Implementor - http://en.wikipedia.org/wiki/Bridge_Pattern
    /// </summary>
    internal class TcpClientAdapter : IStreamResource
    {
        private TcpClient _tcpClient;

        public TcpClientAdapter(TcpClient tcpClient)
        {
            //Debug.Assert(tcpClient != null, "Argument tcpClient cannot be null.");

            _tcpClient = tcpClient;
        }

        public int InfiniteTimeout => Timeout.Infinite;
        private NetworkStream Stream => _tcpClient.GetStream();
        public int ReadTimeout
        {
            get => Stream.ReadTimeout;
            set => Stream.ReadTimeout = value;
        }

        public int WriteTimeout
        {
            get => Stream.WriteTimeout;
            set => Stream.WriteTimeout = value;
        }

        public void Write(byte[] buffer, int offset, int size) => Stream.Write(buffer, offset, size);

        public int Read(byte[] buffer, int offset, int size) => Stream.Read(buffer, offset, size);

        public void DiscardInBuffer() => Stream.Flush();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposableUtility.Dispose(ref _tcpClient);
        }
    }

}
