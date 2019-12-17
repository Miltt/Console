using System;

namespace Cnsl.Algorithms.Searching
{
    public class Binary
    {
        public const int Unknown = -1;

        public static int Search(int[] array, int value)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            var low = array[0];
            var high = array[array.Length - 1];
            var mid = 0; 

            while (low < high)
            {
                mid = (low + high) / 2;
                
                if (array[mid] == value)
                    return mid;
                if (array[mid] < value)
                    low = ++mid;
                else
                    high = --mid;
            }
            
            return Unknown;
        }
    }
}