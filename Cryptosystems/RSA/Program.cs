using System;

namespace Cryptosystem
{
    public struct CryptoRSAData
    {
        public long Modulus;
        public long Key;

        public CryptoRSAData(long modulus, long key)
        {
            Modulus = modulus;
            Key = key;
        }
    }

    public class CryptoRSA
    {
        private const int MaxPrimeP = 9991111;
        private const int MaxPrimeQ = 9999999;

        private readonly CryptoCore _core;
        private readonly long _p;
        private readonly long _q;
        private readonly long _totient; // Euler`s function
        private readonly long _d; // private exponent
        private readonly long _modulus; // modulus for the public and private keys
        private readonly long _key; // public exponent

        public CryptoRSAData CryptoData { get; private set; }

        public CryptoRSA()
        {
            _core = new CryptoCore();
            _p = _core.GetRandomPrime(1, MaxPrimeP);
            _q = _core.GetRandomPrime(1, MaxPrimeQ);
            _modulus = _core.KaratsubaMultiply(_p, _q);
            _totient = _modulus - (_p + _q - 1);
            _key = _core.GetRandomPrime(1, _totient);
            _d = _core.ModularMultiplicativeInverse(_key, _totient);
            CryptoData = new CryptoRSAData(_modulus, _key);
        }

        public long Encryption(long message, CryptoRSAData cryptoData)
        {
            return _core.Pows(message, cryptoData.Key, cryptoData.Modulus);
        }

        public long Decryption(long encryptMessage)
        {
            return _core.Pows(encryptMessage, _d, _modulus);    
        }

        private class CryptoCore
        {
            private const int MinBitValue = 10;

            private readonly Random _rand = new Random();

            public long GetRandomPrime(long min, long max)
            {
                var result = LongRandom(min, max);
                while (FermatPrimalityTest(result) != true)                
                    result = LongRandom(min, max);

                return result;
            }

            public long KaratsubaMultiply(long x, long y)
            {
                var xLength = GetLength(x);
                var yLength = GetLength(y);

                var n = Math.Max(xLength, yLength);
                if (n < MinBitValue)
                    return x * y;

                long xL, xR;
                long yL, yR;
                SplitNumber(x, xLength, out xL, out xR);
                SplitNumber(y, yLength, out yL, out yR);

                var a = KaratsubaMultiply(xL, yL);
                var b = KaratsubaMultiply(xR, yR);
                var c = KaratsubaMultiply(xL + xR, yL + yR);

                return (long)(a * Math.Pow(10, n) + (c - a - b) * Math.Pow(10, n / 2) + b);
            }

            public long ModularMultiplicativeInverse(long e, long mod)
            {
                long x, y, d;
                ExtendedEuclid(e, mod, out x, out y, out d);

                if (d != 1)
                    throw new InvalidOperationException(nameof(d));
            
                x = (x % mod + mod) % mod;
                return x;
            }

            public long Pows(long a, long b, long m)
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

            private void SplitNumber(long value, long length, out long left, out long right)
            {
                if (length % 2 != 0)                
                    length++;

                var divider = (int)Math.Pow(10, length / 2);
                left = value / divider;
                right = value % divider;
            }

            private long GetLength(long value)
            {
                return (long)Math.Log10(value) + 1;
            }

            private void ExtendedEuclid(long a, long b, out long _x, out long _y, out long _d)
            {
                if (b == 0)
                {
                    _x = 1;
                    _y = 0;
                    _d = a;
                    return;
                }

                long x, y, d;
                ExtendedEuclid(b, a % b, out x, out y, out d);

                _x = y;
                _y = x - (a / b) * y;
                _d = d;
            }

            private long LongRandom(long min, long max)
            {
                var buffer = new byte[8];
                _rand.NextBytes(buffer);
                var longRand = BitConverter.ToInt64(buffer, 0);

                return Math.Abs(longRand % (max - min)) + min;
            }

            private bool FermatPrimalityTest(long n)
            {
                if (n == 1)
                    return false;

                var a = LongRandom(1, n - 1);
                if (Euclid(a, n) != 1)
                    return false;
                if (Pows(a, n - 1, n) != 1)
                    return false;

                return true;
            }

            private long Euclid(long a, long b)
            {
                if (b == 0)
                    return a;

                return Euclid(b, a % b);
            }

            private long Mul(long a, long b, long m)
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
    }

    class Program
    {      
        static void Main(string[] args)
        {
            var alica = new CryptoRSA();
            var bob = new CryptoRSA();

            var msg = 1122334455;
            var encryptedMsg = bob.Encryption(msg, alica.CryptoData);
            var decryptedMsg = alica.Decryption(encryptedMsg);

            Console.WriteLine(msg);
            Console.WriteLine(encryptedMsg);
            Console.WriteLine(decryptedMsg);

            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }
}
