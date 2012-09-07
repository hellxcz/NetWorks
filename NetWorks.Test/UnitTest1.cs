using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetWorks.Icmp;
using NetWorks.Icmp.Datagrams;

namespace NetWorks
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ICMPTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var buffer = new byte[1024];
            int size;
            Socket host = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);

            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("101.0.0.1"), 0);
            EndPoint ep = iep;

            var icmpDatagram = new IcmpEchoRequestDatagram();

            icmpDatagram.Data = Encoding.ASCII.GetBytes("test packet");
            icmpDatagram.Identifier = 11;
            icmpDatagram.Sequence = 22;

            host.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
            host.SendTo(icmpDatagram.GetBytes(), icmpDatagram.DatagramSize, SocketFlags.None, iep);

            try
            {
                buffer = new byte[1024];
                size = host.ReceiveFrom(buffer, ref ep);
            }
            catch (SocketException)
            {
                Console.WriteLine("No response from remote host");
                return;
            }

            var response = new IcmpEchoResponseDatagram(buffer, size);

            Debug.WriteLine(string.Format("response from: {0}", ep));
            Debug.WriteLine(string.Format(" Type {0}", response.Type));
            Debug.WriteLine(string.Format(" Code: {0}", response.Code));

            Debug.WriteLine(string.Format(" Identifier: {0}", response.Identifier));
            Debug.WriteLine(string.Format(" Sequence: {0}", response.Sequence));
            string stringData = Encoding.ASCII.GetString(response.Data);
            Debug.WriteLine(string.Format(" data: {0}", stringData));
            host.Close();
        }

        [TestMethod]
        public void JustICMP()
        {
            ICMP.Icmp.Trace("74.125.232.211");
        }
    }
}
