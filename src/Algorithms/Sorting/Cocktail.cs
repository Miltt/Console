using System;
using Cnsl.Common.Extensions;

namespace Cnsl.Algorithms.Sorting
{
    public class Cocktail : ISort
    {
        public void Sort<T>(T[] array)
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
                    if (array[i + 1].CompareTo(array[i]) < 0)
                        array.Swap(i + 1, i);
                }
                rigth--;

                for (int i = rigth; i > left; i--)
                {
                    if (array[i - 1].CompareTo(array[i]) > 0)
                        array.Swap(i - 1, i);
                }
                left++;

            } while (left <= rigth);
        }
    }
}