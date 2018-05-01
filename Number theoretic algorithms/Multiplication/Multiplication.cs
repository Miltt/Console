using System;

namespace Exponentiation
{
    public class Multiplication
    {
        private const double MinValue = 10;

        /// <summary>
        /// Naive myltiple - O(n^2)
        /// </summary>
        public static long Naive(long a, long b)
        {
            if (b == 0)
                return 0;
            if (b == 1)
                return a;

            var c = Naive(a, b / 2);
            if (b % 2 == 0)            
                return 2 * c;

            return 2 * c + a;
        }

        public static long Bitwise(long x, long y)
        {
            long result = 0;

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
        /// The Karatsuba algorithm is a fast multiplication algorithm
        /// </summary>
        public static long Karatsuba(long x, long y)
        {
            var xLength = GetLength(x);
            var yLength = GetLength(y);

            var n = Math.Max(xLength, yLength);
            if (n < MinValue)
                return x * y;

            long xL, xR;
            long yL, yR;
            SplitNumber(x, xLength, out xL, out xR);
            SplitNumber(y, yLength, out yL, out yR);

            long a = Karatsuba(xL, yL);
            long b = Karatsuba(xR, yR);
            long c = Karatsuba(xL + xR, yL + yR);

            return (long)(a * Math.Pow(10, n) + (c - a - b) * Math.Pow(10, n / 2) + b);
        }

        private static int GetLength(long value)
        {
            return (int)Math.Log10(value) + 1;
        }

        private static void SplitNumber(long value, int length, out long left, out long right)
        {
            if (length % 2 != 0)
                length++;

            var divider = (long)Math.Pow(10, length / 2);
            left = value / divider;
            right = value % divider;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var x = 1234567895;
            var y = 989654321;

            Console.WriteLine(Multiplication.Karatsuba(x, y));
            Console.WriteLine(Multiplication.Bitwise(x, y));
            Console.WriteLine(Multiplication.Naive(x, y));

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
