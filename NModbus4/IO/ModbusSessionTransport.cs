using Modbus.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Modbus.IO
{
    internal class ModbusSessionTransport : ModbusIpTransport
    { 
        internal ModbusSessionTransport(IStreamResource streamResource) : base(streamResource)
        {
        }

        internal override byte[] BuildMessageFrame(IModbusMessage message)
        {
            byte[] header = GetMbapHeader(message);
            byte[] pdu = message.ProtocolDataUnit;
            var messageBody = new MemoryStream(header.Length + pdu.Length);

            messageBody.Write(header, 0, header.Length);
            messageBody.Write(pdu, 0, pdu.Length);

            return messageBody.ToArray();
        }

        internal override void OnValidateResponse(IModbusMessage request, IModbusMessage response)
        {
            throw new NotImplementedException();
        }

        internal override byte[] ReadRequest()
        {
            throw new NotImplementedException();
        }

        internal override IModbusMessage ReadResponse<T>()
        {
            throw new NotImplementedException();
        }

        internal override void Write(IModbusMessage message)
        {
            message.TransactionId = GetNewTransactionId();
            byte[] frame = BuildMessageFrame(message);
            //Debug.WriteLine("TX: {0}", string.Join(", ", frame));
            StreamResource.Write(frame, 0, frame.Length);
        }
    }
}
