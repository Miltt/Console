using System;

namespace Cnsl.Algorithms.DynamicProgramming
{
    public readonly struct KnapsackItem
    {
        public int Weight { get; }
        public int Value { get; }

        public KnapsackItem(int weight, int value)
        {
            if (weight < 0)
                throw new ArgumentException("Must be at least 0", nameof(weight));
            if (value < 0)
                throw new ArgumentException("Must be at least 0", nameof(value));
            
            Weight = weight;
            Value = value;
        }

        public override string ToString()
        {
            return $"Value:{Value}, Weight:{Weight}";
        }
    }
}