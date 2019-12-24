using System;

namespace Cnsl.Algorithms.NumberTheoretic
{
    public class Exponentiation
    {
        public static long ModExp(long a, long b, long mod)
        {
            if (a < 0)
                throw new ArgumentException("Must be at least 0", nameof(a));
            if (mod < 0)
                throw new ArgumentException("Must be at least 0", nameof(mod));
            if (b == 0)
                return 1;

            long c = ModExp(a, b / 2, mod);
            return b % 2 == 0
                ? BinExp(c, 2) % mod
                : a * BinExp(c, 2) % mod;
        }

        /// <summary>
        /// Exponentiation by squaring
        /// </summary>
        public static long BinExp(long value, long power)
        {
            if (power == 0)
                return 1;
            if (power == 1)
                return value;
            
            return power % 2 == 0
                ? BinExp(value * value, power / 2)
                : value * BinExp(value * value, (power - 1) / 2);
        }

        public static long Pow(long value, long exponent, long modulus) 
        { 
            if (exponent == 0)
                return 1;

            long res = 1;
            value = value % modulus;
        
            while (exponent > 0) 
            {
                if ((exponent & 1) == 1) 
                    res = (res * value) % modulus;
                
                exponent = exponent >> 1;
                value = (value * value) % modulus; 
            }

            return res; 
        }

        /// <summary>
        /// Modular exponentiation
        /// </summary>
        public static long ModPow(long value, long exponent, long modulus)
        {
            if (exponent == 0)
                return 1;

            if (exponent % 2 == 0)
            {
                var res = ModPow(value, exponent / 2, modulus);
                return Mul(res, res, modulus) % modulus;
            }

            return (Mul(ModPow(value, exponent - 1, modulus), value, modulus)) % modulus;
        }

        private static long Mul(long value, long exponent, long modulus)
        {
            if (exponent == 1)                
                return value;

            if (exponent % 2 == 0)
            {
                var res = Mul(value, exponent / 2, modulus);
                return (2 * res) % modulus;
            }

            return (Mul(value, exponent - 1, modulus) + value) % modulus;
        }
    }
}