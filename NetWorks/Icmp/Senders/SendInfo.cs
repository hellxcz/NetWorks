using System.Net;

namespace NetWorks.Icmp.Senders
{
    public class SendInfo
    {
        public IPAddress IpAddress { get; private set; }
        public int ReceiveTimeout { get; private set; }
        public string Data { get; private set; }

        public SendInfo(IPAddress ipAddress, int receiveTimeout, string data)
        {
            IpAddress = ipAddress;
            ReceiveTimeout = receiveTimeout;
            Data = data;
        }
    }
}