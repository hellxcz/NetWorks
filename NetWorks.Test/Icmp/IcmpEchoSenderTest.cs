using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetWorks.Icmp;
using NetWorks.Icmp.Datagrams;
using NetWorks.Icmp.Senders;

namespace NetWorks
{
    [TestClass]
    public class IcmpEchoSenderTest
    {
        private IcmpEchoSender GetTestee()
        {
            return new IcmpEchoSender();
        }

        [TestMethod]
        public void IcmpEchoSender_cTor()
        {
            var testee = GetTestee();

            Assert.IsNotNull(testee);
        }

        [TestMethod]
        public void IcmpEchoSender_Send()
        {
            var testeee = GetTestee();

            var responseRecieved = new ManualResetEvent(false);

            IcmpEchoResponseDatagram responseDatagram = null;
            long latency = -1;

            testeee.Send(IPAddress.Parse("127.0.0.1"),
                         (result) =>
                         {
                             responseDatagram = result.Response;
                             latency = result.Latency;

                             responseRecieved.Set();
                         });

            responseRecieved.WaitOne(10000);

            Assert.IsTrue(latency >= 0);
            Assert.IsNotNull(responseDatagram);
        }
    }

    [TestClass]
    public class IcmpTracertSenderTest
    {
        private IcmpTracertSender GetTestee()
        {
            return new IcmpTracertSender();
        }

        [TestMethod]
        public void IcmpTracertSender_cTor()
        {
            var testee = GetTestee();

            Assert.IsNotNull(testee);
        }

        [TestMethod]
        public void IcmpTracertSender_Send()
        {
            var testeee = GetTestee();

            var responseRecieved = new ManualResetEvent(false);

            IList<SendResult<IcmpEchoResponseDatagram>> actual = null;

            testeee.Send(IPAddress.Parse("74.125.232.211"),
                         (result) =>
                         {
                             actual = result;

                             responseRecieved.Set();
                         });

            responseRecieved.WaitOne(10000);

            Assert.IsNotNull(actual);
        }
    }

}