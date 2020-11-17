using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Bubble : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[i].CompareTo(array[j]) < 0)
                        array.Swap(i, j);
                }
            }
        }
    }
}