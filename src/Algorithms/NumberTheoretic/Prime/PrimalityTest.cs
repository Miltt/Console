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

                if (GCD.EuclidNaive(n, m) != 1) 
                    return false;
                if (Exponentiation.Pow(m, n - 1, n) != 1) 
                    return false; 
            } 
            
            return true; 
        }
    }
}