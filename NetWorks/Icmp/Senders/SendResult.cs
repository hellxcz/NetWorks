using System.Net;
using NetWorks.Icmp.Datagrams;

namespace NetWorks.Icmp.Senders
{
    public class SendResult<TIcmpResponse>
        where TIcmpResponse : IcmpDatagram
    {
        public TIcmpResponse Response { get; private set; }
        public long Latency { get; private set; }
        public EndPoint EndPoint { get; private set; }

        public SendResult(TIcmpResponse response, long latency, EndPoint endPoint)
        {
            Response = response;
            Latency = latency;
            EndPoint = endPoint;
        }
    }
}