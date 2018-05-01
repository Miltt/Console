using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegerFactorization
{
    public class Factorizator
    {
        /// <summary>
        /// Based on a complete enumeration of all potential factors
        /// </summary>
        public List<int> TrialDivision(int n)
        {
            var factors = new List<int>();

            var i = 2;
            while (i * i <= n)
            {
                if (n % i == 0)
                {
                    n = n / i;
                    factors.Add(i);
                }
                else
                    i = i == 2 ? i + 1 : i + 2;
            }

            factors.Add(n);
            return factors;
        }

        /// <summary>
        /// Fermat's method is based on the representation of an odd integer as the difference of two squares:
        /// N = a^2 - b^2 = (a + b) * (a - b)
        /// </summary>
        public void FermatFactor(int n, out int a, out int b)
        {
            var x = (int)Math.Sqrt(n);
            var y = x * x - n;
            
            while (!IsSquare(y))
            {
                x++;
                y = x * x - n;
            }

            a = x;
            b = (int)Math.Sqrt(y);
        }

        private bool IsSquare(int n)
        {
            return Math.Sqrt(n) % 1 == 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var n = 89755;

            var factorizator = new Factorizator();
            var result = factorizator.TrialDivision(n);
            Console.WriteLine($"{n} = {result.ToStr()}");

            int a, b;
            factorizator.FermatFactor(n, out a, out b);
            Console.WriteLine($"{n} = {a + b} {a - b}");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public static class Helper
    {
        public static string ToStr<T>(this List<T> collection)
        {
            if (collection == null || !collection.Any())
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var item in collection)
                sb.Append($"{item} ");

            return sb.ToString();
        }
    }
}