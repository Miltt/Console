using System;

namespace Cnsl.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static void Swap<T>(this T[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static bool IsSortedByAsc<T>(this T[] array)
            where T : IComparable<T>
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1].CompareTo(array[i]) > 0)
                    return false;
            }

            return true;
        }

        public static bool IsSortedByDesc<T>(this T[] array)
            where T : IComparable<T>
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1].CompareTo(array[i]) < 0)
                    return false;
            }

            return true;
        }
    }
}