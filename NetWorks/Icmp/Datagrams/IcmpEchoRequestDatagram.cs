namespace NetWorks.Icmp.Datagrams
{
    public class IcmpEchoRequestDatagram : IcmpEchoDatagram
    {
        public IcmpEchoRequestDatagram()
        {
            Type = GetType(8);
            Code = Type.GetCode(0);
        }
    }
}