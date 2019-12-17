using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Shell : ISort
    {
        public void Sort(int[] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            var offset = array.Length / 2;
            while (offset > 0)
            {
                for (int i = 0; i < array.Length - offset; i++)
                {
                    var j = i;
                    while (j >= 0 && array[j] > array[j + offset])
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