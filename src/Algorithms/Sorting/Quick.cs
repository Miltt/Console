using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Quick : BaseSorter
    {
        public override void Sort<T>(T[] array)
        {
            SortInternal(array: array,
                start: 0,
                end: array.Length - 1,
                isDescending: false);
        }

        public override void SortByDescending<T>(T[] array)
        {
            SortInternal(array: array,
                start: 0,
                end: array.Length - 1,
                isDescending: true);
        }

        private void SortInternal<T>(T[] array, int start, int end, bool isDescending)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (start < end)
            {
                var pivot = GetPivot(array, start, end, isDescending);
                SortInternal(array, start, pivot - 1, isDescending);
                SortInternal(array, pivot + 1, end, isDescending);
            }
        }

        private int GetPivot<T>(T[] array, int start, int end, bool isDescending)
            where T : IComparable<T>
        {
            var pivot = start;

            for (int i = start; i <= end; i++)
            {
                if (Compare(array[i], array[end], isDescending) <= 0)
                    array.Swap(pivot++, i);
            }

            return --pivot;
        }
    }
}