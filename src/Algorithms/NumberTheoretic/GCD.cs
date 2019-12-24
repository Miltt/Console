using System;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class GCD
    {
        public readonly ref struct Result
        {
            public long X { get; }
            public long Y { get; }
            public long D { get; }

            public Result(long x, long y, long d)
            {
                X = x;
                Y = y;
                D = d;
            }
        }

        /// <summary>
        /// Euclid algorithm for get greatest common divisor
        /// </summary>
        public static long EuclidNaive(long a, long b)
        {
            ThrowIfArgsInvalid(a, b);

            return a < b
                ? EuclidNaive(b, a)
                : a % b == 0 ? b : EuclidNaive(b, a % b);  
        }

        /// <summary>
        /// Extended Euclid algorithm for get greatest common divisor
        /// </summary>
        public static Result EuclidExtended(long a, long b)
        {
            ThrowIfArgsInvalid(a, b);

            if (b == 0)
                return new Result(x: 1, y: 0, d: a);

            var result = EuclidExtended(b, a % b);
            return new Result(x: result.Y, y: result.X - (a / b) * result.Y, d: result.D);
        }

        private static void ThrowIfArgsInvalid(long a, long b)
        {
            if (a < 0)
                throw new ArgumentException("Must be at least 0", nameof(a));
            if (b < 0)
                throw new ArgumentException("Must be at least 0", nameof(b));
        }
    }
}