using System;
using System.Collections.Generic;

namespace Cnsl.Algorithms.DynamicProgramming
{
    public class KnapsackProblem
    {
        public static int[,] Search(KnapsackItem[] items, int capacity)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));
            if (capacity < 0)
                throw new ArgumentException("Must be at least 0", nameof(capacity));

            var result = new int[capacity + 1, items.Length + 1];

            for (int j = 1; j <= items.Length; j++)
            {
                for (int i = 1; i <= capacity; i++)
                {
                    var item = items[j - 1];
                    result[i, j] = item.Weight <= i
                        ? Math.Max(result[i, j - 1], result[i - item.Weight, j - 1] + item.Value)
                        : result[i, j - 1];
                }
            }

            return result;
        }

        public static IEnumerable<KnapsackItem> GetBackTrack(int[,] result, KnapsackItem[] items, int i, int j)
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));
            if (items is null)
                throw new ArgumentNullException(nameof(items));
            
            if (result[i, j] != 0)
            {
                if (result[i, j - 1] == result[i, j])
                {
                    foreach (var item in GetBackTrack(result, items, i, j - 1))
                        yield return item;
                }
                else
                {
                    var resultItem = items[j - 1];
                    
                    foreach (var item in GetBackTrack(result, items, i - resultItem.Weight, j - 1))
                        yield return item;
                    
                    yield return resultItem;
                }
            }
        }
    }
}