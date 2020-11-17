using System;

namespace Cnsl.Algorithms.Sorting
{
    public class Merge : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            SortInternal(array, 0, array.Length);
        }

        private void SortInternal<T>(T[] array, int start, int end)
            where T : IComparable<T>
        {
            if (end - start <= 1)
                return;
            
            var mid = start + (end - start) / 2;
            
            SortInternal(array, start, mid);
            SortInternal(array, mid, end);
            MergeInternal(array, start, mid, end);
        }

        private void MergeInternal<T>(T[] array, int start, int mid, int end)
            where T : IComparable<T>
        {
            var result = new T[end - start];
            var l = 0;
            var r = 0;
            var i = 0;

            while (l < mid - start && r < end - mid)
            {
                result[i++] = array[start + l].CompareTo(array[mid + r]) < 0
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