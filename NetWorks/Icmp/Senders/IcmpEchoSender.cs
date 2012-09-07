using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using NetWorks.Icmp.Datagrams;

namespace NetWorks.Icmp.Senders
{
    public class IcmpEchoSender : IcmpSender<IcmpEchoSender.SendEchoInfo, IcmpEchoResponseDatagram>
    {
        public class SendEchoInfo : SendInfo
        {
            public short Identifier { get; private set; }
            public short Sequence { get; private set; }

            public void IncreaseSequence()
            {
                Sequence++;
            }

            public SendEchoInfo(IPAddress ipAddress, int receiveTimeout, short identifier, short sequence, string data) : base(ipAddress, receiveTimeout, data)
            {
                Identifier = identifier;
                Sequence = sequence;
            }
        }

        protected override IcmpDatagram GetDatagramToSend(SendEchoInfo info)
        {
            var icmpDatagram = new IcmpEchoRequestDatagram();

            icmpDatagram.Data = Encoding.ASCII.GetBytes(info.Data);
            icmpDatagram.Identifier = info.Identifier;
            icmpDatagram.Sequence = info.Sequence;

            return icmpDatagram;
        }

        public void Send(SendEchoInfo info, Action<SendResult<IcmpEchoResponseDatagram>> finished)
        {
            SendToHost(info, finished);
        }

        public void Send(IPAddress ipAddress, Action<SendResult<IcmpEchoResponseDatagram>> finished)
        {
            var info = new SendEchoInfo(ipAddress, 3000, 1, 1, "test datagram");

            Send(info, finished);
        }

        protected override IcmpEchoResponseDatagram GetParsedResponse(byte[] data, int size)
        {
            return new IcmpEchoResponseDatagram(data, size);
        }

        protected override SendResult<IcmpEchoResponseDatagram> GetSendResult(IcmpEchoResponseDatagram response, long ticks, EndPoint endPoint)
        {
            return new SendResult<IcmpEchoResponseDatagram>(response, ticks, endPoint);
        }
    }
}