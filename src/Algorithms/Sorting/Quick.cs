using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Quick : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            SortInternal(array, 0, array.Length - 1);
        }

        private void SortInternal<T>(T[] array, int start, int end)
            where T : IComparable<T>
        {
            if (start < end)
            {
                var pivot = GetPivot(array, start, end);
                SortInternal(array, start, pivot - 1);
                SortInternal(array, pivot + 1, end);
            }
        }

        private int GetPivot<T>(T[] array, int start, int end)
            where T : IComparable<T>
        {
            var pivot = start;

            for (int i = start; i <= end; i++)
            {
                if (array[i].CompareTo(array[end]) <= 0)
                    array.Swap(pivot++, i);
            }

            return --pivot;
        }
    }
}