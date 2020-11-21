using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Gnome : BaseSorter
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

            var i = 1;
            
            while (i < array.Length)
            {
                if (i == 0 || Compare(array[i - 1], array[i], isDescending) <= 0)
                {
                    i++; 
                }
                else
                {
                    array.Swap(i - 1, i);
                    i--;
                }
            }
        }
    }
}