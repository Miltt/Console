using System;

namespace EuclideanAlgorithm
{
    public class Euclid
    {
        public int Naive(int a, int b)
        {
            if (b == 0)
                return a;

            return Naive(b, a % b);
        }

        public int Extended(int a, int b)
        {
            int x, y, d;
            Extended(a, b, out x, out y, out d);
            return d;
        }

        private void Extended(int a, int b, out int _x, out int _y, out int _d)
        {            
            if (b == 0)
            {
                _x = 1;
                _y = 0;
                _d = a;
                return;             
            }

            int x, y, d;
            Extended(b, a % b, out x, out y, out d);

            _x = y;
            _y = x - (a / b) * y;
            _d = d;            
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
