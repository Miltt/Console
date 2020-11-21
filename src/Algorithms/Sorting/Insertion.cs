using System;

namespace Cnsl.Algorithms.Sorting
{
    public class Insertion : BaseSorter
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

            for (int i = 1; i < array.Length; i++)
            {
                var j = i;
                var temp = array[i];
                
                while (j > 0 && Compare(temp, array[j - 1], isDescending) < 0)
                {
                    array[j] = array[j - 1];
                    j--;
                }

                array[j] = temp;
            }
        }
    }
}