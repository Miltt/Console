using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Cocktail : BaseSorter
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

            var left = 0;
            var rigth = array.Length - 1;

            do
            {
                for (int i = left; i < rigth; i++)
                {
                    if (Compare(array[i + 1], array[i], isDescending) < 0)
                        array.Swap(i + 1, i);
                }
                rigth--;

                for (int i = rigth; i > left; i--)
                {
                    if (Compare(array[i - 1], array[i], isDescending) > 0)
                        array.Swap(i - 1, i);
                }
                left++;

            } while (left <= rigth);
        }
    }
}