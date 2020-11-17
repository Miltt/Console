using System;

namespace Cnsl.Algorithms.Sorting
{
    public class Insertion : ISort
    {
        public void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            for (int i = 1; i < array.Length; i++)
            {
                var j = i;
                var temp = array[i];
                
                while (j > 0 && temp.CompareTo(array[j - 1]) < 0)
                {
                    array[j] = array[j - 1];
                    j--;
                }

                array[j] = temp;
            }
        }
    }
}