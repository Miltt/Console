using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Heap : BaseSorter
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

            // build
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                Shift(array, i, array.Length, isDescending);
            }

            // sort
            for (int i = array.Length - 1; i >= 1; i--)
            {
                array.Swap(0, i);
                Shift(array, 0, i, isDescending);
            }
        }

        private void Shift<T>(T[] array, int curIndex, int curLength, bool isDescending)
            where T : IComparable<T>
        {
            var maxChildIndex = 0;

            while (2 * curIndex + 1 < curLength)
            {
                maxChildIndex = (2 * curIndex + 1 == curLength - 1) || 
                                (Compare(array[2 * curIndex + 1], array[2 * curIndex + 2], isDescending) > 0)
                    ? 2 * curIndex + 1
                    : 2 * curIndex + 2;

                if (Compare(array[curIndex], array[maxChildIndex], isDescending) >= 0)
                    break;
                
                array.Swap(curIndex, maxChildIndex);
                curIndex = maxChildIndex;
            }
        }
    }
}