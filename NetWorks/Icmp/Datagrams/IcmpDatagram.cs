using System;
using System.Collections.Generic;
using NetWorks.Common;
using Type = NetWorks.Common.Type;

namespace NetWorks.Icmp.Datagrams
{
    public class IcmpDatagram : IHaveGetBytes
    {
        private static Dictionary<byte, Type> Types { get; set; }
        public static Type GetType(byte code)
        {
            return Types[code];
        }

        static IcmpDatagram()
        {
            Types = new Dictionary<byte, Type>()
                        {
                            {00, new Type(00, "Echo Reply"             , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                     {00, new Type.Code(00, "")},
                                                                                 })},
                            {03, new Type(03, "Destination Unreachable", new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00,"Net Unreachable"                                                       )},
                                                                                    {01, new Type.Code(01,"Host Unreachable"                                                      )},
                                                                                    {02, new Type.Code(02,"Protocol Unreachable"                                                  )},
                                                                                    {03, new Type.Code(03,"Port Unreachable"                                                      )},
                                                                                    {04, new Type.Code(04,"Fragmentation Needed and Don't Fragment was Set"                       )},
                                                                                    {05, new Type.Code(05,"Source Route Failed"                                                   )},
                                                                                    {06, new Type.Code(06,"Destination Network Unknown"                                           )},
                                                                                    {07, new Type.Code(07,"Destination Host Unknown"                                              )},
                                                                                    {08, new Type.Code(08,"Source Host Isolated"                                                  )},
                                                                                    {09, new Type.Code(09,"Communication with Destination Network is Administratively Prohibited" )},
                                                                                    {10, new Type.Code(10,"Communication with Destination Host is Administratively Prohibited"    )},
                                                                                    {11, new Type.Code(11,"Destination Network Unreachable for Type of Service"                   )},
                                                                                    {12, new Type.Code(12,"Destination Host Unreachable for Type of Service"                      )},
                                                                                    {13, new Type.Code(13,"Communication Administratively Prohibited"                             )},
                                                                                    {14, new Type.Code(14,"Host Precedence Violation"                                             )},
                                                                                    {15, new Type.Code(15,"Precedence cutoff in effect"                                           )},
                                                                                 })},
                            {05,new Type(05,"Redirect"                 , new Dictionary<byte, Type.Code>()
                                                                                {
                                                                                    {00,new Type.Code(00, "Redirect Datagram for the Network (or subnet)"           )},
                                                                                    {01,new Type.Code(01, "Redirect Datagram for the Host"                          )},
                                                                                    {02,new Type.Code(02, "Redirect Datagram for the Type of Service and Network"   )},
                                                                                    {03,new Type.Code(03, "Redirect Datagram for the Type of Service and Host"      )},
                                                                                    })},
                            {06,new Type(06,"Alternate Host Address"   , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                     {00, new Type.Code(00, "Alternate Address for Host")},
                                                                                 })},
                            {08,new Type(08,"Echo"                     , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {09,new Type(09,"Router Advertisement"     , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "Normal router advertisement")}, 
                                                                                    {16, new Type.Code(16, "Does not route common traffic")}, 
                                                                                 })},
                            {10,new Type(10,"Router Solicitation"      , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {11,new Type(11,"Time Exceeded"            , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "Time to Live exceeded in Transit")}, 
                                                                                    {01, new Type.Code(01, "Fragment Reassembly Time Exceeded")}, 
                                                                                 })},
                            {12,new Type(12,"Parameter Problem"        , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "Pointer indicates the error")}, 
                                                                                    {01, new Type.Code(01, "Missing a Required Option")}, 
                                                                                    {02, new Type.Code(02, "Bad Length")}, 
                                                                                 })},
                            {13,new Type(13,"Timestamp"                , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {14,new Type(14,"Timestamp Reply"          , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {15,new Type(15,"Information Request"      , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {16,new Type(16,"Information Reply"        , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {17,new Type(17,"Address Mask Request"     , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {18,new Type(18,"Address Mask Reply"       , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00, "")},
                                                                                 })},
                            {30,new Type(30,"Traceroute"                    )},
                            {31,new Type(31,"Datagram Conversion Error"     )},
                            {32,new Type(32,"Mobile Host Redirect"          )},
                            {33,new Type(33,"IPv6 Where-Are-You"            )},
                            {34,new Type(34,"IPv6 I-Am-Here"                )},
                            {35,new Type(35,"Mobile Registration Request"   )},
                            {36,new Type(36,"Mobile Registration Reply"     )},
                            {37,new Type(37,"Domain Name Request"           )},
                            {38,new Type(38,"Domain Name Reply"             )},
                            {39,new Type(39,"SKIP"                          )},
                            {40,new Type(40,"Photuris"                 , new Dictionary<byte, Type.Code>()
                                                                                 {
                                                                                    {00, new Type.Code(00,"Bad SPI"                )},
                                                                                    {01, new Type.Code(01,"Authentication Failed"  )},
                                                                                    {02, new Type.Code(02,"Decompression Failed"   )},
                                                                                    {03, new Type.Code(03,"Decryption Failed"      )},
                                                                                    {04, new Type.Code(04,"Need Authentication"    )},
                                                                                    {05, new Type.Code(05,"Need Authorization"     )},
                                                                                 })},
                        };
        }

        protected byte[] IcmpPayload = new byte[1500];

        private Type type;
        public Type Type
        {
            get { return type; }
            set
            {
                type = value;
                Buffer.BlockCopy(BitConverter.GetBytes(value.Value), 0, IcmpPayload, 0, 1);
            }
        }

        private Type.Code code;
        public Type.Code Code
        {
            get { return code; }
            set
            {
                code = value;
                Buffer.BlockCopy(BitConverter.GetBytes(value.Value), 0, IcmpPayload, 1, 1);
            }
        }

        private readonly UInt16? checksum;
        public UInt16 Checksum
        {
            get { return checksum ?? GetChecksum(); }
        }

        protected int LastDataLength;
        protected byte[] data;
        public virtual byte[] Data
        {
            get { return data; }
            set
            {
                data = value;
                LastDataLength = data.Length;
                Buffer.BlockCopy(value, 0, IcmpPayload, DataOffset, data.Length);
            }
        }

        public virtual int DatagramSize
        {
            get { return LastDataLength + DataOffset; }
        }

        protected int IpHeaderLength
        {
            get { return 20; }
        }

        protected virtual int DataOffset
        {
            get { return 4; }
        }

        public IcmpDatagram()
        {
        }

        public IcmpDatagram(byte[] input, int size)
        {
            if (input == null)
            {
                return;
            }

            Type = GetType(input[IpHeaderLength + 0]);
            Code = Type.GetCode(input[IpHeaderLength + 1]);

            checksum = BitConverter.ToUInt16(input, IpHeaderLength + 2);

            int realDataOffset = (IpHeaderLength + DataOffset);
            int dataLength = size - realDataOffset;
            data = new byte[dataLength];

            Buffer.BlockCopy(input, realDataOffset, data, 0, dataLength);
        }

        public virtual byte[] GetBytes()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(GetChecksum()), 0, IcmpPayload, 2, 2);

            byte[] data = new byte[LastDataLength + DataOffset + 1];

            Buffer.BlockCopy(IcmpPayload, 0, data,0, data.Length - 1);

            return data;
        }


        private UInt16 GetChecksum()
        {
            UInt32 chcksm = 0;
            byte[] data = IcmpPayload;
            int packetsize = LastDataLength + DataOffset;
            int index = 0;
            while (index < packetsize)
            {
                chcksm += Convert.ToUInt32(BitConverter.ToUInt16(data, index));
                index += 2;
            }
            chcksm = (chcksm >> 16) + (chcksm & 0xffff);
            chcksm += (chcksm >> 16);
            return (UInt16)(~chcksm);
        }
    }
}