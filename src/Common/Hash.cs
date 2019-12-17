using System;

namespace Cnsl.Common.Extensions
{
    public static class Hash
    {   
        private const int Module = 65521;
        private const int NumBit = 16;

        public static int GetByAdler32(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var s1 = 1;
            var s2 = 0;

            for (int i = 0; i < value.Length; i++)
            {
                s1 = (s1 + value[i]) % Module;
                s2 = (s2 + s1) % Module;
            }

            return (s2 << NumBit) + s1;
        }
    }
}