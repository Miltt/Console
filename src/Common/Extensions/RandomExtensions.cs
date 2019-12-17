using System;

namespace Cnsl.Common.Extensions
{
    public static class RandomExtensions
    {
        public static long Next(this Random random, long minValue, long maxValue)
        {
            if (maxValue <= minValue)
                throw new ArgumentException("Ьust be greater than the min value", nameof(maxValue));

            var range = (ulong)(maxValue - minValue);
            var value = (ulong)0;
            
            do
            {
                var buffer = new byte[8];                
                random.NextBytes(buffer);
                value = (ulong)BitConverter.ToInt64(buffer, 0);
            } while (value > ulong.MaxValue - ((ulong.MaxValue % range) + 1) % range);

            return (long)(value % range) + minValue;
        }
    }
}