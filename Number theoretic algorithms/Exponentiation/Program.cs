using System;

namespace Exponentiation
{
    public class Exponentiation
    {
        /// <summary>
        /// Modular exponentiation - O(n^3)
        /// </summary>
        public static long ModExp(int a, int b, int mod)
        {
            if (b == 0)
                return 1;

            var c = ModExp(a, b / 2, mod);
            if (b % 2 == 0)
                return BinExp(c, 2) % mod;
            
            return a * BinExp(c, 2) % mod;          
        }

        /// <summary>
        /// binary exponentiation
        /// </summary>
        public static long BinExp(long value, int power)
        {
            if (power == 0)
                return 1;
            if (power % 2 == 0)
                return BinExp(value * value, power / 2);
            
            return value * BinExp(value * value, (power - 1) / 2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var value = 4;
            var power = 13;
            var mod = 497;

            Console.WriteLine(Exponentiation.ModExp(value, power, mod));
            Console.WriteLine(Exponentiation.BinExp(value, power));

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }        
    }
}
