using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Gnome : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            var i = 1;
            
            while (i < array.Length)
            {
                if (i == 0 || array[i - 1].CompareTo(array[i]) <= 0)
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