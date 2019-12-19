using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class PrimalityTest
    {
        private const int FermatIterationCount = 5;

        public static bool Naive(long n)
        {
            if (n <= 1 || n == 4) 
                return false; 
            if (n <= 3) 
                return true;

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)                
                    return false;                
            }

            return true;
        }

        public static bool Fermat(long n)
        { 
            if (n <= 1 || n == 4) 
                return false; 
            if (n <= 3) 
                return true;
        
            var random = new Random();
            for (int i = 0; i < FermatIterationCount; i++) 
            { 
                var m = random.Next(2, n - 2);

                if (GCD(n, m) != 1) 
                    return false;
                if (Pow(m, n - 1, n) != 1) 
                    return false; 
            } 
            
            return true; 
        } 

        /// <summary>
        /// Euclid algorithm for get greatest common divisor
        /// </summary>
        private static long GCD(long a, long b) 
        { 
            if (a < b) 
                return GCD(b, a); 
            return a % b == 0 ? b : GCD(b, a % b);   
        }

        /// <summary>
        /// (a ^ n) % p in O(log(p))
        /// <summary>
        private static long Pow(long a, long n, long p) 
        { 
            if (n == 0)
                return 1;

            long res = 1;
            a = a % p;
        
            while (n > 0) 
            {
                if ((n & 1) == 1) 
                    res = (res * a) % p;
                
                n = n >> 1;
                a = (a * a) % p; 
            }

            return res; 
        }
    }
}