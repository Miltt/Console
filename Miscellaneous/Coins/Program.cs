using System;

namespace Misc
{
    public class Coins
    {
        public static int CountCombinations(int[] coins, int coinsCount, int amount)
        {
            if (amount < 0 || coinsCount <= 0)
                return 0;
            if (amount == 0)
                return 1;
            
            return CountCombinations(coins, coinsCount - 1, amount)
                 + CountCombinations(coins, coinsCount, amount - coins[coinsCount - 1]);
        }

        public static void ShowCombinations(int[] coins, int[] counts, int index, int amount)
        {            
            if (index >= coins.Length)
            {
                for (int i = 0; i < coins.Length; i++)
                    Console.Write($"{ counts[i]} * {coins[i]} ");
                
                Console.WriteLine();
                return;
            }
                                    
            if (index == coins.Length - 1)
            {
                if (amount % coins[index] == 0)
                {                    
                    counts[index] = amount / coins[index];                 
                    ShowCombinations(coins, counts, index + 1, 0);
                }
            }
            else
            {
                for (int i = 0; i <= amount / coins[index]; i++)
                {                    
                    counts[index] = i;
                    ShowCombinations(coins, counts, index + 1, amount - coins[index] * i);              
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var coins = new int[] { 1, 2, 3 };
            var amount = 4;

            var numCombinations = Coins.CountCombinations(coins, coins.Length, amount);
            Console.WriteLine(numCombinations);
            
            var count = new int[coins.Length];
            Coins.ShowCombinations(coins, count, 0, amount);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}