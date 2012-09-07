using System;

namespace NetWorks.Icmp.Datagrams
{
    public class IcmpEchoDatagram : IcmpDatagram
    {
        private short _identifier;

        public Int16 Identifier
        {
            get { return _identifier; }
            set
            {
                _identifier = value;
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, IcmpPayload, 4, 2);
            }
        }

        private short _sequence;

        public Int16 Sequence
        {
            get { return _sequence; }
            set
            {
                _sequence = value;
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, IcmpPayload, 6, 2);
            }
        }

        protected override int DataOffset
        {
            get { return 8; }
        }

        protected IcmpEchoDatagram()
        {
        }

        protected IcmpEchoDatagram(byte[] input, int size)
            : base(input, size)
        {
            if (input == null)
            {
                return;
            }

            _identifier = BitConverter.ToInt16(input, IpHeaderLength + 4);
            _sequence = BitConverter.ToInt16(input, IpHeaderLength + 6);
        }
    }
}