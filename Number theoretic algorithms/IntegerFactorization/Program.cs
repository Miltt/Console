using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegerFactorization
{
    public class Factorizator
    {
        public struct Result
        {
            public int X;
            public int Y;

            public Result(int x, int y)
            {
                X = x + y;
                Y = x - y;
            }
        }

        /// <summary>
        /// Based on a complete enumeration of all potential factors
        /// </summary>
        public IEnumerable<int> TrialDivision(int n)
        {
            var i = 2;
            while (i * i <= n)
            {
                if (n % i == 0)
                {
                    n /= i;
                    yield return i;
                }
                else
                    i = i == 2 ? i + 1 : i + 2;
            }
            
            yield return n;
        }

        /// <summary>
        /// Fermat's method is based on the representation of an odd integer as the difference of two squares:
        /// N = x^2 - y^2 = (x + y) * (x - y)
        /// </summary>
        public Result FermatFactor(int n)
        {
            var x = (int)Math.Sqrt(n);
            var y = x * x - n;
            
            while (!IsSquare(y))
            {
                x++;
                y = x * x - n;
            }

            return new Result(x, (int)Math.Sqrt(y));
        }

        private bool IsSquare(int n)
            => Math.Sqrt(n) % 1 == 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var n = 89755;

            var factorizator = new Factorizator();

            var factors = factorizator.TrialDivision(n);
            Console.WriteLine($"{n} = {factors.ToStr()}");

            var result = factorizator.FermatFactor(n);
            Console.WriteLine($"{n} = {result.X} {result.Y}");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public static class Extension
    {
        public static string ToStr<T>(this IEnumerable<T> source)
        {
            if (source is null)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var item in source)
                sb.Append($"{item} ");

            return sb.ToString();
        }
    }
}