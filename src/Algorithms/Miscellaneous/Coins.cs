using System;
using System.Collections.Generic;

namespace Cnsl.Algorithms.Miscellaneous
{
    public class Coins
    {
        public readonly struct Combination
        {
            public int Coin { get; }
            public int Count { get; }

            public Combination(int coin, int count)
            {
                if (coin < 0)
                    throw new ArgumentException("Must be at least 0", nameof(coin));
                if (count < 0)
                    throw new ArgumentException("Must be at least 0", nameof(count));

                Coin = coin;
                Count = count;
            }

            public override string ToString()
            {
                return $"{Coin}:{Count}";
            }
        }

        public static int CountCombinations(int[] coins, int amount)
        {
            ThrowIfArgsInvalid(coins, amount);
            return Count(coins, coins.Length, amount);
        }
        
        public static IEnumerable<Combination> GenerateCombinations(int[] coins, int amount)
        {
            ThrowIfArgsInvalid(coins, amount);
            return Generate(coins, new int[coins.Length], 0, amount);
        }

        private static int Count(int[] coins, int coinsCount, int amount)
        {
            if (amount < 0 || coinsCount <= 0)
                return 0;
            if (amount == 0)
                return 1;
            
            return Count(coins, coinsCount - 1, amount) + Count(coins, coinsCount, amount - coins[coinsCount - 1]);
        }

        private static IEnumerable<Combination> Generate(int[] coins, int[] counts, int index, int amount)
        {            
            if (index >= coins.Length)
            {
                for (int i = 0; i < coins.Length; i++)
                    yield return new Combination(coins[i], counts[i]);

                yield break;
            }
            else
            {
                if (index == coins.Length - 1)
                {
                    if (amount % coins[index] == 0)
                    {                    
                        counts[index] = amount / coins[index];                 
                        foreach (var combination in Generate(coins, counts, index + 1, 0))
                            yield return combination;
                    }
                }
                else
                {
                    for (int i = 0; i <= amount / coins[index]; i++)
                    {                    
                        counts[index] = i;
                        foreach (var combination in Generate(coins, counts, index + 1, amount - coins[index] * i))
                            yield return combination;
                    }
                }
            }
        }

        private static void ThrowIfArgsInvalid(int[] coins, int amount)
        {
            if (coins is null)
                throw new ArgumentNullException(nameof(coins));
            if (amount < 0)
                throw new ArgumentException("Must be at least 0", nameof(amount));
        }
    }
}