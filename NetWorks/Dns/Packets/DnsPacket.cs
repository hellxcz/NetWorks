using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NetWorks.Common;

namespace NetWorks.Dns.Packets
{
    public class DnsPacket
    {
        public class HeaderClass
        {
            public enum OpCodes
            {
                Query = 0,
                InverseQuery = 1,
                Status = 2,
                Notify = 4,
                Update = 5
            }

            public enum ResponseCodes
            {
                NoError = 0,
                FormatError = 1,
                ServerFailure = 2,
                NameError = 3,
                NotImplemented = 4,
                Refused = 5,
            }

            public UInt16 Id { get; set; }

            /// <summary>
            /// query (0) or response (1)
            /// </summary>
            public bool Qr { get; set; }

            public OpCodes OpCode { get; set; }

            public bool IsAuthorative { get; set; }

            public bool IsTruncated { get; set; }

            public bool IsRecursionDesired { get; set; }

            public bool IsRecursionAvailable { get; set; }

            public UInt16 QuestionCount { get; set; }

            public UInt16 AnswerCount { get; set; }
            public UInt16 NSCount { get; set; }
            public UInt16 ARCount { get; set; }
        }

        public class QuestionClass : IHaveGetBytes
        {
            public enum QTypes
            {
                [Description("a host address [RFC1035]")]
                A = 1,
                [Description("an authoritative name server [RFC1035]")]
                NS = 2,
                [Description("a mail destination (OBSOLETE - use MX) [RFC1035]")]
                MD = 3,
                [Description("a mail forwarder (OBSOLETE - use MX) [RFC1035]")]
                MF = 4,
                [Description("the canonical name for an alias [RFC1035]")]
                CNAME = 5,
                [Description("marks the start of a zone of authority [RFC1035]")]
                SOA = 6,
                [Description("a mailbox domain name (EXPERIMENTAL) [RFC1035]")]
                MB = 7,
                [Description("a mail group member (EXPERIMENTAL) [RFC1035]")]
                MG = 8,
                [Description("a mail rename domain name (EXPERIMENTAL) [RFC1035]")]
                MR = 9,
                [Description(" a null RR (EXPERIMENTAL) [RFC1035]")]
                NULL = 10,
                [Description(" a well known service description [RFC1035]")]
                WKS = 11,
                [Description(" a domain name pointer [RFC1035]")]
                PTR = 12,
                [Description(" host information [RFC1035]")]
                HINFO = 13,
                [Description(" mailbox or mail list information [RFC1035]")]
                MINFO = 14,
                [Description(" mail exchange [RFC1035]")]
                MX = 15,
                [Description(" text strings [RFC1035]")]
                TXT = 16,
                [Description(" for Responsible Person [RFC1183]")]
                RP = 17,
                [Description(" for AFS Data Base location [RFC1183][RFC5864]")]
                AFSDB = 18,
                [Description(" for X.25 PSDN address [RFC1183]")]
                X25 = 19,
                [Description(" for ISDN address [RFC1183]")]
                ISDN = 20,
                [Description(" for Route Through [RFC1183]")]
                RT = 21,
                [Description(" for NSAP address, NSAP style A record [RFC1706]")]
                NSAP = 22,
                [Description(" for domain name pointer, NSAP style [RFC1348][RFC1637][RFC1706]")]
                NSAP_PTR = 23,
                [Description(" for security signature [RFC4034][RFC3755][RFC2535][RFC2536][RFC2537][RFC2931][RFC3110][RFC3008]")]
                SIG = 24,
                [Description(" for security key [RFC4034][RFC3755][RFC2535][RFC2536][RFC2537][RFC2539][RFC3008][RFC3110]")]
                KEY = 25,
                [Description(" X.400 mail mapping information [RFC2163]")]
                PX = 26,
                [Description(" Geographical Position [RFC1712]")]
                GPOS = 27,
                [Description(" IP6 Address [RFC3596]")]
                AAAA = 28,
                [Description(" Location Information [RFC1876]")]
                LOC = 29,
                [Description(" Next Domain (OBSOLETE) [RFC3755][RFC2535]")]
                NXT = 30,
                [Description(" Endpoint Identifier [Patton][Patton1995]")]
                EID = 31,
                [Description(" Nimrod Locator [Patton][Patton1995]")]
                NIMLOC = 32,
                [Description(" Server Selection [RFC2782]")]
                SRV = 33,
                [Description(" ATM Address [ATMDOC]")]
                ATMA = 34,
                [Description(" Naming Authority Pointer [RFC2915][RFC2168][RFC3403]")]
                NAPTR = 35,
                [Description(" Key Exchanger [RFC2230]")]
                KX = 36,
                [Description(" CERT [RFC4398]")]
                CERT = 37,
                [Description(" A6 (OBSOLETE - use AAAA) [RFC3226][RFC2874][RFC6563]")]
                A6 = 38,
                [Description(" DNAME [RFC6672]")]
                DNAME = 39,
                [Description(" SINK [Eastlake][Eastlake2002]")]
                SINK = 40,
                [Description(" OPT [RFC2671][RFC3225]")]
                OPT = 41,
                [Description(" APL [RFC3123]")]
                APL = 42,
                [Description(" Delegation Signer [RFC4034][RFC3658]")]
                DS = 43,
                [Description(" SSH Key Fingerprint [RFC4255]")]
                SSHFP = 44,
                [Description(" IPSECKEY [RFC4025]")]
                IPSECKEY = 45,
                [Description(" RRSIG [RFC4034][RFC3755]")]
                RRSIG = 46,
                [Description(" NSEC [RFC4034][RFC3755]")]
                NSEC = 47,
                [Description(" DNSKEY [RFC4034][RFC3755]")]
                DNSKEY = 48,
                [Description(" DHCID [RFC4701]")]
                DHCID = 49,
                [Description(" NSEC3 [RFC5155]")]
                NSEC3 = 50,
                [Description(" NSEC3PARAM [RFC5155]")]
                NSEC3PARAM = 51,
                [Description(" TLSA [RFC6698]")]
                TLSA = 52,
                [Description(" Host Identity Protocol [RFC5205]")]
                HIP = 55,
                [Description(" NINFO [Reid]")]
                NINFO = 56,
                [Description(" RKEY [Reid]")]
                RKEY = 57,
                [Description(" Trust Anchor LINK [Wijngaards]")]
                TALINK = 58,
                [Description(" Child DS [Barwood]")]
                CDS = 59,
                [Description(" [IANA-Reserved]")]
                SPF = 99,
                [Description(" [IANA-Reserved]")]
                UINFO = 100,
                [Description(" [IANA-Reserved]")]
                UID = 101,
                [Description(" [IANA-Reserved]")]
                GID = 102,
                [Description(" [RFC-irtf-rrg-ilnp-dns-06.txt]")]
                UNSPEC = 103,
                [Description(" [RFC-irtf-rrg-ilnp-dns-06.txt]")]
                NID = 104,
                [Description(" [RFC-irtf-rrg-ilnp-dns-06.txt]")]
                L32 = 105,
                [Description(" [RFC-irtf-rrg-ilnp-dns-06.txt]")]
                L64 = 106,
                [Description("")]
                LP = 107,
                [Description("Transaction Key [RFC2930]")]
                TKEY = 249,
                [Description("Transaction Signature [RFC2845]")]
                TSIG = 250,
                [Description("incremental transfer [RFC1995]")]
                IXFR = 251,
                [Description("transfer of an entire zone [RFC1035][RFC5936]")]
                AXFR = 252,
                [Description("mailbox-related RRs (MB, MG or MR) [RFC1035]")]
                MAILB = 253,
                [Description("mail agent RRs (OBSOLETE - see MX) [RFC1035]")]
                MAILA = 254,
                //[Description("A request for all records [RFC1035]")] *=   255 ,
                [Description("URI [Faltstrom]")]
                URI = 256,
                [Description("Certification Authority Authorization [Hallam-Baker]")]
                CAA = 257,
                [Description("DNSSEC Trust Authorities [Weiler] 2005-12-13")]
                TA = 32768,
                [Description("DNSSEC Lookaside Validation [RFC4431]")]
                DLV = 32769,
            }

            public enum QClasses : ushort
            {
                [Description("Internet [RFC1035]")]
                IN = 1,
                [Description("Chaos [Moon1981]")]
                CH = 3,
                [Description("Hesiod [Dyer1987]")]
                HS = 4,
                [Description("QCLASS NONE [RFC2136]")]
                NONE = 254,
                [Description("QCLASS * (ANY) [RFC1035]")]
                ANY = 255,
            }

            public List<byte> Question { get; private set; }

            public QTypes QType { get; set; }

            public QClasses QClass { get; set; }

            public QuestionClass()
            {
                Question = new List<byte>();
            }

            public void AddQuestion(string question)
            {
                foreach (var chunk in question.Split('.'))
                {
                    Question.Add((byte)chunk.Length);
                    Question.AddRange(chunk.Select(ch => (byte)ch));
                    Question.Add(0x00);
                }
            }

            public byte[] GetBytes()
            {
                return null;
            }
        }

        public HeaderClass Header { get; private set; }

        public QuestionClass Question { get; private set; }
    }
}
