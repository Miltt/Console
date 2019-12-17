using System;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class Euclidean
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

        public static long Naive(long a, long b)
        {
            ThrowIfArgsInvalid(a, b);

            return b == 0 ? a : Naive(b, a % b);
        }

        public static Result Extended(long a, long b)
        {
            ThrowIfArgsInvalid(a, b);

            if (b == 0)
                return new Result(x: 1, y: 0, d: a);

            var result = Extended(b, a % b);
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