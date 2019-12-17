using System;

namespace Cnsl.Algorithms.Searching
{
    public class Linear
    {
        public const int Unknown = -1;

        public static int Search(int[] array, int value)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            var i = 0;
            while (i < array.Length && array[i] != value)
                i++;
            
            return i < array.Length ? i : Unknown;
        }
    }
}