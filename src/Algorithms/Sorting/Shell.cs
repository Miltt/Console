using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Shell : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            var offset = array.Length / 2;
            while (offset > 0)
            {
                for (int i = 0; i < array.Length - offset; i++)
                {
                    var j = i;
                    while (j >= 0 && array[j].CompareTo(array[j + offset]) > 0)
                    {
                        array.Swap(j, j + offset);
                        j -= offset;
                    }
                }

                offset /= 2;
            }
        }
    }
}