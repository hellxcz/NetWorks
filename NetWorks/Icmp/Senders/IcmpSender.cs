using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using NetWorks.Icmp.Datagrams;

namespace NetWorks.Icmp.Senders
{
    public abstract class IcmpSender<TSenderInfo, TIcmpResponse>
        where TSenderInfo : SendInfo
        where TIcmpResponse : IcmpDatagram
    {
        protected virtual Socket GetHost(TSenderInfo info)
        {
            var host = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);

            host.Bind(new IPEndPoint(IPAddress.Any, 0));

            host.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, info.ReceiveTimeout);

            return host;
        }
        
        protected virtual void AfterRecieved(Socket host)
        {
            host.Close();
        }

        protected abstract IcmpDatagram GetDatagramToSend(TSenderInfo info);
        protected abstract TIcmpResponse GetParsedResponse(byte[] data, int size);
        protected abstract SendResult<TIcmpResponse> GetSendResult(TIcmpResponse response, long ticks, EndPoint endPoint);

        protected virtual void SendToHost(TSenderInfo info, Action<SendResult<TIcmpResponse>> finished)
        {
            var host = GetHost(info);

            var ipEndPoint = new IPEndPoint(info.IpAddress, 0);

            EndPoint endPoint = ipEndPoint;

            var icmpDatagram = GetDatagramToSend(info);

            long startTicks = DateTime.Now.Ticks;
            long endTicks = 0;

            host.SendTo(icmpDatagram.GetBytes(), icmpDatagram.DatagramSize, SocketFlags.None, ipEndPoint);

            int size;

            byte[] buffer;

            try
            {
                buffer = new byte[1024];

                size = host.ReceiveFrom(buffer, SocketFlags.None, ref endPoint);
                endTicks = DateTime.Now.Ticks;
            }
            catch (SocketException)
            {
                finished(GetSendResult(null, endTicks - startTicks, null));

                var lastError = (int)Marshal.GetLastWin32Error();
                return;
            }
            finally
            {
                AfterRecieved(host);
            }
             
            finished(GetSendResult(GetParsedResponse(buffer, size), endTicks - startTicks, endPoint));
        }
    }
}