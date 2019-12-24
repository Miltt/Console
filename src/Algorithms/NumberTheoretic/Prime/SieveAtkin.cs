using System;
using System.Collections.Generic;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class SieveAtkin
    {
        public static IEnumerable<uint> GetPrimeNumbers(uint limit)
        {
            if (limit < 3)
                throw new ArgumentException("Must be at least 3", nameof(limit));

            var isPrime = new bool[limit + 1];
            isPrime[2] = true;
            isPrime[3] = true;

            var sqrI = (ulong)0;
            var sqrJ = (ulong)0;
            var sqrLimit = (uint)Math.Sqrt(limit);
            
            for (uint i = 1; i <= sqrLimit; ++i)
            {
                sqrI += 2 * i - 1;
                sqrJ = 0;

                for (uint j = 1; j <= sqrLimit; ++j) 
                {
                    sqrJ += 2 * j - 1;
                    
                    var n = 4 * sqrI + sqrJ;
                    if ((n <= limit) && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] = !isPrime[n];
                    
                    // n = 3 * x2 + y2; 
                    n -= sqrI;
                    if ((n <= limit) && (n % 12 == 7))
                        isPrime[n] = !isPrime[n];
                    
                    // n = 3 * x2 - y2;
                    n -= 2 * sqrJ;
                    if ((i > j) && (n <= limit) && (n % 12 == 11))
                        isPrime[n] = !isPrime[n];
                }
            }

            for (int i = 5; i <= sqrLimit; ++i) 
            {
                if (isPrime[i]) 
                {
                    var n = i * i;
                    for (int j = n; j <= limit; j += n)
                        isPrime[j] = false;
                }
            }

            yield return 2;
            yield return 3;
            yield return 5;

            for (uint i = 7; i <= limit; ++i)
            {
                if ((isPrime[i]) && (i % 3 != 0) && (i % 5 !=  0))
                    yield return i;
            }
        }
    }
}