using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Bubble : BaseSorter
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

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (Compare(array[i], array[j], isDescending) < 0)
                        array.Swap(i, j);
                }
            }
        }
    }
}