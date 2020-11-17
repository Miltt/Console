using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Heap : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            // build
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                Shift(array, i, array.Length);
            }

            // sort
            for (int i = array.Length - 1; i >= 1; i--)
            {
                array.Swap(0, i);
                Shift(array, 0, i);
            }
        }

        private void Shift<T>(T[] array, int curIndex, int curLength)
            where T : IComparable<T>
        {
            var maxChildIndex = 0;

            while (2 * curIndex + 1 < curLength)
            {
                maxChildIndex = (2 * curIndex + 1 == curLength - 1) || (array[2 * curIndex + 1].CompareTo(array[2 * curIndex + 2]) > 0)
                    ? 2 * curIndex + 1
                    : 2 * curIndex + 2;

                if (array[curIndex].CompareTo(array[maxChildIndex]) >= 0)
                    break;
                
                array.Swap(curIndex, maxChildIndex);
                curIndex = maxChildIndex;
            }
        }
    }
}