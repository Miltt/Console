using System;
using Cnsl.Algorithms.NumberTheoretic;
using System.Linq;

namespace Cnsl.Algorithms.Cryptography
{
    public class RSA
    {
        public readonly struct Key
        {
            public long Modulus { get; }
            public long Value { get; }

            public Key(long modulus, long value)
            {
                Modulus = modulus;
                Value = value;
            }
        }
        
        public Key PrivateKey { get; }
        public Key PublicKey { get; }

        public RSA()
        {
            var primeNumbers = SieveAtkin.GetPrimeNumbers(limit: 10000000U).ToArray();
            var random = new Random();

            var p = GetRandomPrime(primeNumbers, random);
            var q = GetRandomPrime(primeNumbers, random);
            var modulus = Multiplication.Karatsuba(p, q);
            var totient = modulus - (p + q - 1); // Euler`s function
            var publicExponent = GetRandomPrime(primeNumbers, random);
            var privateExponent = GetModularMultiplicativeInverse(publicExponent, totient);
            
            PrivateKey = new Key(modulus, privateExponent);
            PublicKey = new Key(modulus, publicExponent);
        }

        public long Encrypt(long message, Key publicKey)
        {
            return Exponentiation.ModPow(message, publicKey.Value, publicKey.Modulus);
        }

        public long Decrypt(long encryptMessage)
        { 
            return Exponentiation.ModPow(encryptMessage, PrivateKey.Value, PrivateKey.Modulus);
        }

        private long GetRandomPrime(uint[] primeNumbers, Random random)
        {
            var index = random.Next(2, primeNumbers.Length - 1);
            var result = primeNumbers[index];

            if (!PrimalityTest.Fermat(result))
                throw new InvalidOperationException($"The number is not prime: {result}");

            return result;
        }

        private long GetModularMultiplicativeInverse(long exponent, long modulus)
        {
            var res = GCD.EuclidExtended(exponent, modulus);
            if (res.D != 1)
                throw new InvalidOperationException("The greatest common divisor is not 1");
        
            return (res.X % modulus + modulus) % modulus;
        }
    }
}