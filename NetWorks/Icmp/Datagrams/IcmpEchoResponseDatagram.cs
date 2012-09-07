namespace NetWorks.Icmp.Datagrams
{
    public class IcmpEchoResponseDatagram : IcmpEchoDatagram
    {
        public IcmpEchoResponseDatagram(byte[] input, int size) : base(input, size)
        {
            
        }
    }
}