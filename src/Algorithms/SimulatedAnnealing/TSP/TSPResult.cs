using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.SimulatedAnnealing
{
    public class TSPResult
    {
        public int[] State { get; set; }
        public double Energy { get; set; }

        public TSPResult(int length)
        {
            if (length < 1)
                throw new ArgumentException("Must be at least 1", nameof(length));

            State = InitState(length);
        }

        private int[] InitState(int length)
        {
            var array = new int[length];
            for (int i = 0; i < array.Length; i++)
                array[i] = i;            
            
            var random = new Random();
            for (int i = 0; i < array.Length; i++)
                array.Swap(i, random.Next(array.Length));

            return array;
        }
    }
}