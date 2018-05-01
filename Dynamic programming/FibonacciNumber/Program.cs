using System;

namespace FibonacciNumber
{
    public class Fibonacci
    {
        public int NaiveCalc(int n)
        {
            var result = 0;

            if (n == 0)
                result = 0;
            else if (n == 1)
                result = 1;
            else
                result = n > 0 
                    ? NaiveCalc(n - 1) + NaiveCalc(n - 2) 
                    : NaiveCalc(n + 2) - NaiveCalc(n + 1);

            return result;
        }

        public int MemoizationCalc(int n)
        {
            var result = 0;

            if (n == 0)
                result = 0;
            else if (n == 1)
                result = 1;
            else
            {
                var memResults = new int[n + 1];
                memResults[0] = 0;
                memResults[1] = 1;

                for (var i = 2; i <= n; i++)
                    memResults[i] = memResults[i - 1] + memResults[i - 2];

                result = memResults[n];
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var fib = new Fibonacci();

            Console.WriteLine(fib.NaiveCalc(8));
            Console.WriteLine(fib.MemoizationCalc(9));

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
