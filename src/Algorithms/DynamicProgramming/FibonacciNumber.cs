using System;

namespace Cnsl.Algorithms.DynamicProgramming
{
    public class FibonacciNumber
    {
        public static long Naive(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            
            return n > 0 
                ? Naive(n - 1) + Naive(n - 2) 
                : Naive(n + 2) - Naive(n + 1);
        }

        public static ulong Memoization(int n)
        {
            if (n < 0)
                throw new ArgumentException("Must be at least 0", nameof(n));
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            
            var cache = new ulong[n + 1];
            cache[0] = 0;
            cache[1] = 1;

            for (int i = 2; i <= n; i++)
                cache[i] = cache[i - 1] + cache[i - 2];

            return cache[n];
        }
    }
}