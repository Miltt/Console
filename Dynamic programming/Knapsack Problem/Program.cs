using System;
using System.Linq;

namespace KnapsackProblem
{
    public class Knapsack
    {
        private readonly int _capacity;
        private int[,] _result;

        public Knapsack(int capacity)
        {
            if (capacity < 1)
                throw new ArgumentException(nameof(capacity));

            _capacity = capacity;
        }

        public void Solve(int[] itemWeight, int[] itemValue)
        {
            if (itemWeight.IsNullOrEmpty())
                throw new ArgumentException(nameof(itemWeight));
            if (itemValue.IsNullOrEmpty())
                throw new ArgumentException(nameof(itemValue));

            CalcResult(itemWeight, itemValue);
            ShowBackTrack(itemWeight, itemWeight.Length - 1, _capacity - 1);

            Console.WriteLine($"Maximum value is: {_result[itemWeight.Length - 1, _capacity - 1]}");
        }

        private void CalcResult(int[] weight, int[] value)
        {
            _result = new int[weight.Length, _capacity];

            for (var item = 1; item < weight.Length; item++)
            {
                for (var j = 1; j < _capacity; j++)
                {
                    if (j >= weight[item])
                        _result[item, j] = Math.Max(_result[item - 1, j], _result[item - 1, j - weight[item]] + value[item]);
                    else
                        _result[item, j] = _result[item - 1, j];
                }
            }
        }

        private void ShowBackTrack(int[] weight, int i, int j)
        {
            if (_result[i, j] == 0)            
                return;            

            if (_result[i - 1, j] == _result[i, j])
            {
                ShowBackTrack(weight, i - 1, j);
            }
            else
            {
                ShowBackTrack(weight, i - 1, j - weight[i]);
                Console.WriteLine($"Item: {i}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var itemWeight = new int[] { 5, 10, 6, 5 };
            var itemValue = new int[] { 3, 5, 4, 2 };
            var capacity = 14;

            var knapsack = new Knapsack(capacity);
            knapsack.Solve(itemWeight, itemValue);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public static class Helper
    {
        public static bool IsNullOrEmpty(this int[] collection)
        {
            return collection == null || !collection.Any();
        }
    }
}
