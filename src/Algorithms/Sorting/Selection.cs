using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Selection : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            for (int i = 0; i < array.Length; i++)
            {
                var min = i;

                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[min]) < 0)
                        min = j;
                }

                array.Swap(i, min);
            }
        }
    }
}