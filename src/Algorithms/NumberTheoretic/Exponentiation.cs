using System;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class Exponentiation
    {
        public static long ModExp(long a, long b, long mod)
        {
            if (a < 0)
                throw new ArgumentException("Must be at least 0", nameof(a));
            if (mod < 0)
                throw new ArgumentException("Must be at least 0", nameof(mod));
            if (b == 0)
                return 1;

            var c = ModExp(a, b / 2, mod);
            return b % 2 == 0
                ? BinExp(c, 2) % mod
                : a * BinExp(c, 2) % mod;
        }

        /// <summary>
        /// Exponentiation by squaring
        /// </summary>
        public static long BinExp(long value, long power)
        {
            if (power == 0)
                return 1;
            if (power == 1)
                return value;
            
            return power % 2 == 0
                ? BinExp(value * value, power / 2)
                : value * BinExp(value * value, (power - 1) / 2);
        }
    }
}