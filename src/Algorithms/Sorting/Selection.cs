using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Selection : BaseSorter
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
                var min = i;

                for (int j = i + 1; j < array.Length; j++)
                {
                    if (Compare(array[j], array[min], isDescending) < 0)
                        min = j;
                }

                array.Swap(i, min);
            }
        }
    }
}