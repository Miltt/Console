using System;

namespace Cnsl.Algorithms.Sorting
{
    public class Insertion : ISort
    {
        public void Sort(int[] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            for (int i = 1; i < array.Length; i++)
            {
                var j = i;
                var temp = array[i];
                
                while (j > 0 && temp < array[j - 1])
                {
                    array[j] = array[j - 1];
                    j--;
                }

                array[j] = temp;
            }              
        }
    }
}