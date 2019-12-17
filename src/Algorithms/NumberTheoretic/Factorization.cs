using System;
using System.Collections.Generic;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class Factorization
    {
        public readonly ref struct Result
        {
            public long X { get; }
            public long Y { get; }

            public Result(long x, long y)
            {
                X = x + y;
                Y = x - y;
            }
        }

        /// <summary>
        /// Based on a complete enumeration of all potential factors
        /// </summary>
        public static IEnumerable<long> TrialDivision(long n)
        {
            if (n < 1)
                throw new ArgumentException("Must be at least 1", nameof(n));

            long i = 2;
            while (i * i <= n)
            {
                if (n % i == 0)
                {
                    n /= i;
                    yield return i;
                }
                else
                    i = i == 2 ? i + 1 : i + 2;
            }
            
            yield return n;
        }

        /// <summary>
        /// Fermat's method is based on the representation of an odd integer as the difference of two squares:
        /// N = x^2 - y^2 = (x + y) * (x - y)
        /// </summary>
        public static Result FermatFactor(long n)
        {
            if (n < 1)
                throw new ArgumentException("Must be at least 1", nameof(n));

            var x = (long)Math.Sqrt(n);
            var y = x * x - n;
            
            while (!IsSquare(y))
            {
                x++;
                y = x * x - n;
            }

            return new Result(x, (long)Math.Sqrt(y));
        }

        private static bool IsSquare(long n)
            => Math.Sqrt(n) % 1 == 0;
    }
}