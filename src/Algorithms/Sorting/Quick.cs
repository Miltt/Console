using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Quick : ISort
    {
        public void Sort(int[] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            SortInternal(array, 0, array.Length - 1);
        }

        private void SortInternal(int[] array, int start, int end)
        {
            if (start < end)
            {
                var pivot = GetPivot(array, start, end);
                SortInternal(array, start, pivot - 1);
                SortInternal(array, pivot + 1, end);
            }
        }

        private int GetPivot(int[] array, int start, int end)
        {
            var pivot = start;

            for (int i = start; i <= end; i++)
            {
                if (array[i] <= array[end])
                    array.Swap(pivot++, i);
            }

            return --pivot;
        }
    }
}