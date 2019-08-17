using System;

namespace KnapsackProblem
{
    public struct Item
    {
        public int Weight;
        public int Value;

        public Item(int weight, int value)
        {
            Weight = weight;
            Value = value;
        }
    }

    public sealed class Knapsack
    {
        public Result Fill(Item[] items, int capacity)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            var result = new Result(items.Length + 1, capacity + 1);

            for (var j = 1; j <= items.Length; j++)
            {
                for (var i = 1; i <= capacity; i++)
                {
                    var item = items[j - 1];
                    result.Track[i, j] = item.Weight <= i
                        ? Math.Max(result.Track[i, j - 1], result.Track[i - item.Weight, j - 1] + item.Value)
                        : result.Track[i, j - 1];
                }
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var capacity = 14;
            var items = new Item[] 
            {
                new Item(weight: 1, value: 13),
                new Item(weight: 6, value: 5),
                new Item(weight: 6, value: 4),
                new Item(weight: 5, value: 2)
            };

            var result = new Knapsack().Fill(items, capacity);
            result.ShowBackTrack(items, capacity, items.Length);
            Console.WriteLine($"Maximum value is: {result.GetMaxValue()}");
            
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public class Result
    {
        public int[,] Track { get; }

        public Result(int itemsCount, int capacity)
        {
            if (itemsCount < 0)
                throw new ArgumentException("Must be at least 0", nameof(itemsCount));
            if (capacity < 0)
                throw new ArgumentException("Must be at least 0", nameof(capacity));

            Track = new int[capacity, itemsCount];
        }

        public int GetMaxValue()
        {
            return Track?[Track.GetLength(0) - 1, Track.GetLength(1) - 1] ?? 0;
        }

        public void ShowBackTrack(Item[] items, int i, int j)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));

            if (Track is null || Track[i, j] == 0)
                return;            

            if (Track[i, j - 1] == Track[i, j])
            {
                ShowBackTrack(items, i, j - 1);
            }
            else
            {
                var item = items[j - 1];
                ShowBackTrack(items, i - item.Weight, j - 1);
                Console.WriteLine($"Item {j - 1}: Weight {item.Weight}, Value {item.Value}");
            }
        }
    }
}
