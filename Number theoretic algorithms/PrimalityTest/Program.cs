using System;

namespace Prim
{
    public class PrimalityTest
    {
        private static readonly Random _random = new Random();

        public static bool Naive(int n)
        {
            if (n <= 1)
                return false;
            if (n == 2)            
                return true;
            if (n % 2 == 0)            
                return false;

            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)                
                    return false;                
            }

            return true;
        }

        public static bool Fermat(int n)
        {
            if (n == 1)
                return false;
                            
            var m = _random.Next(1, n - 1);
            return GetGCD(m, n) == 1 && Pows(m, n - 1, n) == 1;
        }

        /// <summary>
        /// Euclid algorithm for get greatest common divisor
        /// </summary>
        private static int GetGCD(int a, int b)
        {
            return b == 0 ? a : GetGCD(b, a % b);
        }

        /// <summary>
        /// fast exponentiation
        /// </summary>
        private static int Pows(int a, int b, int m)
        {
            if (b == 0)            
                return 1;         
            if (b % 2 == 0)
            {
                var r = Pows(a, b / 2, m);
                return Mul(r, r, m) % m;
            }

            return (Mul(Pows(a, b - 1, m), a, m)) % m;
        }

        private static int Mul(int a, int b, int m)
        {
            if (b == 1)
                return a;
            if (b % 2 == 0)
            {
                var r = Mul(a, b / 2, m);
                return (2 * r) % m;
            }

            return (Mul(a, b - 1, m) + a) % m;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var n = 16769023;

            Console.WriteLine($"{n} is prime: {PrimalityTest.Naive(n)}");
            Console.WriteLine($"{n} is prime: {PrimalityTest.Fermat(n)}");

            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }
}
