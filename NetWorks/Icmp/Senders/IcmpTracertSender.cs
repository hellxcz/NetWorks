using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetWorks.Icmp.Datagrams;

namespace NetWorks.Icmp.Senders
{
    public class IcmpTracertSender
    {
        public class SendTracertInfo : IcmpEchoSender.SendEchoInfo
        {
            public int Ttl { get; private set; }

            public SendTracertInfo(IcmpEchoSender.SendEchoInfo echoInfo, int ttl) 
                : this(echoInfo.IpAddress, echoInfo.ReceiveTimeout, echoInfo.Identifier, echoInfo.Sequence, echoInfo.Data, ttl)
            {}

            public SendTracertInfo(IPAddress ipAddress, int receiveTimeout, short identifier, short sequence,
                                   string data, int ttl)
                : base(ipAddress, receiveTimeout, identifier, sequence, data)
            {
                Ttl = ttl;
            }
        }

        protected class IcmpTracertSenderImpl : IcmpEchoSender
        {
            protected Socket Host { get; set; }

            protected override void AfterRecieved(Socket host)
            {
            }

            protected override Socket GetHost(SendEchoInfo info)
            {
                var tracertInfo = info as SendTracertInfo;

                if (Host == null)
                {
                    Host = base.GetHost(info);
                }

                Host.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, tracertInfo.Ttl);

                return Host;
            }

            public void CloseHost()
            {
                Host.Close();
            }
        }

        protected short LastSequence = 1;

        public void Send(IPAddress ipAddress, Action<IList<SendResult<IcmpEchoResponseDatagram>>> finished)
        {
            var info = new IcmpEchoSender.SendEchoInfo(ipAddress, 3000, 1, 1, "test datagram");

            Send(info, finished);
        }

        public void Send(IcmpEchoSender.SendEchoInfo info, Action<IList<SendResult<IcmpEchoResponseDatagram>>> finished)
        {
            IPEndPoint endPoint = null;

            var finishedResult = new List<SendResult<IcmpEchoResponseDatagram>>();

            var senderImpl = new IcmpTracertSenderImpl();

            var lastTtl = 1;

            var manualResetevent = new ManualResetEvent(false);

            while (endPoint == null || endPoint.Address != info.IpAddress)
            {
                senderImpl.Send(new SendTracertInfo(info, lastTtl++),
                                result =>
                                    {
                                        endPoint = result.EndPoint as IPEndPoint;

                                        finishedResult.Add(result);

                                        manualResetevent.Set();
                                    });

                info.IncreaseSequence();

                manualResetevent.WaitOne();
                manualResetevent.Reset();
            }

            senderImpl.CloseHost();

            finished(finishedResult);
        }
    }
}