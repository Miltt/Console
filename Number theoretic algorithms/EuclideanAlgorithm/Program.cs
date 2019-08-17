using System;

namespace EuclideanAlgorithm
{
    public sealed class Euclid
    {
        public int Naive(int a, int b)
        {
            return b == 0 ? a : Naive(b, a % b);
        }

        public int Extended(int a, int b)
        {
            return Extended(a, b, out int x, out int y);
        }

        private int Extended(int a, int b, out int x, out int y)
        {            
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            var d = Extended(b, a % b, out int _x, out int _y);

            x = _y;
            y = _x - (a / b) * _y;
            return d;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = 161;
            var b = 28;

            var euclid = new Euclid();
            Console.WriteLine(euclid.Naive(a, b));
            Console.WriteLine(euclid.Extended(a, b));

            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }
}
