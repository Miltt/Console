using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Shell : BaseSorter
    {
        public override void Sort<T>(T[] array)
        {
            SortInternal(array, isDescending: false);
        }

        public override void SortByDescending<T>(T[] array)
        {
            SortInternal(array, isDescending: true);
        }

        private void SortInternal<T>(T[] array, bool isDescending)
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
                    while (j >= 0 && Compare(array[j], array[j + offset], isDescending) > 0)
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