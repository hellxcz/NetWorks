using System.Collections.Generic;

namespace NetWorks.Common
{
    public class Type
    {
        public class Code
        {
            public string Name { get; private set; }
            public byte Value { get; private set; }

            public Code(byte value, string name)
            {
                Name = name;
                Value = value;
            }
        }

        public string Name { get; private set; }
        public byte Value { get; private set; }

        private Dictionary<byte, Code> Codes { get; set; }

        public Type(byte value, string name)
            : this(value, name, new Dictionary<byte, Code>())
        {

        }

        public Type(byte value, string name, Dictionary<byte, Code> codes)
        {
            Name = name;
            Value = value;
            Codes = codes;
        }

        public Code GetCode(byte code)
        {
            return Codes[code];
        }

        public ICollection<Code> GetCodes()
        {
            return Codes.Values;
        }
    }
}