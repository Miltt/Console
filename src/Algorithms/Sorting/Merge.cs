using System;

namespace Cnsl.Algorithms.Sorting
{
    public class Merge : BaseSorter
    {
        public override void Sort<T>(T[] array)
        {
            SortInternal(array: array,
                start: 0,
                end: array.Length,
                isDescending: false);
        }

        public override void SortByDescending<T>(T[] array)
        {
            SortInternal(
                array: array,
                start: 0,
                end: array.Length,
                isDescending: true);
        }

        private void SortInternal<T>(T[] array, int start, int end, bool isDescending)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (end - start <= 1)
                return;
            
            var mid = start + (end - start) / 2;
            
            SortInternal(array, start, mid, isDescending);
            SortInternal(array, mid, end, isDescending);
            MergeInternal(array, start, mid, end, isDescending);
        }

        private void MergeInternal<T>(T[] array, int start, int mid, int end, bool isDescending)
            where T : IComparable<T>
        {
            var result = new T[end - start];
            var l = 0;
            var r = 0;
            var i = 0;

            while (l < mid - start && r < end - mid)
            {
                result[i++] = Compare(array[start + l], array[mid + r], isDescending) < 0
                    ? array[start + l++]
                    : array[mid + r++];
            }

            while (r < end - mid)
                result[i++] = array[mid + r++];

            while (l < mid - start)
                result[i++] = array[start + l++];

            Array.Copy(result, 0, array, start, result.Length);
        }
    }
}