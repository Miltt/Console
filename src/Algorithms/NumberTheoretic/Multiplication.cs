using System;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class Multiplication
    {
        private const int KaratsubaMinValue = 10;

        /// <summary>
        /// Naive multiple - O(n^2)
        /// </summary>
        public static long Naive(long a, long b)
        {
            if (b == 0)
                return 0;
            if (b == 1)
                return a;

            var c = Naive(a, b / 2);
            return b % 2 == 0 ? 2 * c : 2 * c + a;
        }

        /// <summary>
        /// Russian peasant 
        /// </summary>
        public static long Bitwise(long x, long y)
        {
            var result = (long)0;

            while (y != 0)
            {
                if ((y & 01) != 0)
                    result += x;

                x <<= 1;
                y >>= 1;
            }

            return result;
        }

        /// <summary>
        /// Karatsuba is a fast multiplication algorithm
        /// </summary>
        public static long Karatsuba(long x, long y)
        {
            if (x < KaratsubaMinValue || y < KaratsubaMinValue)
                return x * y;

            var n = Math.Max(GetSize(x), GetSize(y));            
            var m = (n / 2) + (n % 2);
            var k = Exponentiation.BinExp(10, m);

            var a = (x / k);
            var b = (x % k);
            var c = (y / k);
            var d = (y % k);

            var z0 = Karatsuba(a, c);
            var z1 = Karatsuba(b, d);
            var z2 = Karatsuba(a + b, c + d);

            return (Exponentiation.BinExp(10, m * 2) * z0) + z1 + ((z2 - z1 - z0) * k);
        }

        private static int GetSize(long value)
            => value == 0 ? 1 : 1 + (int)Math.Log10(Math.Abs(value));
    }
}