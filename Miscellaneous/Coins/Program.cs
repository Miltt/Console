using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins
{
    public class Coins
    {
        public static int Count(int[] s, int m, int n)
        {
            if (n < 0 || m <= 0)
                return 0;
            if (n == 0)
                return 1;
            
            return Count(s, m - 1, n) + Count(s, m, n - s[m - 1]);
        }

        public static void ShowAllCombination(int[] coins, int[] counts, int index, int amount)
        {            
            if (index >= coins.Length)
            {
                for (var i = 0; i < coins.Length; i++)
                    Console.Write($"{ counts[i]} * {coins[i]} ");

                Console.WriteLine();
                return;
            }
                                    
            if (index == coins.Length - 1)
            {
                if (amount % coins[index] == 0)
                {                    
                    counts[index] = amount / coins[index];                 
                    ShowAllCombination(coins, counts, index + 1, 0);
                }
            }
            else
            {
                for (var i = 0; i <= amount / coins[index]; i++)
                {                    
                    counts[index] = i;
                    ShowAllCombination(coins, counts, index + 1, amount - coins[index] * i);                    
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var coins = { 1, 2, 3 };
            var count = new int[coins.Length];
            var n = 4;

            Console.WriteLine(Count(coins, coins.Length, n));
            PrintAllCombination(coins, count, 0, n);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}