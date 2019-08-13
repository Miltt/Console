using System;

namespace FibonacciNumber
{
    public class Fibonacci
    {
        public static int NaiveCalc(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            
            return n > 0 
                ? NaiveCalc(n - 1) + NaiveCalc(n - 2) 
                : NaiveCalc(n + 2) - NaiveCalc(n + 1);
        }

        public static int MemoizationCalc(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            
            var cache = new int[n + 1];
            cache[0] = 0;
            cache[1] = 1;

            for (int i = 2; i <= n; i++)
                cache[i] = cache[i - 1] + cache[i - 2];

            return cache[n];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Fibonacci.NaiveCalc(8));
            Console.WriteLine(Fibonacci.MemoizationCalc(9));

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
