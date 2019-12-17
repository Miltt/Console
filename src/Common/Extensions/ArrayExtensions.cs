namespace Cnsl.Common.Extensions
{
    public static class ArrayExtensions
    {
        public static void Swap(this int[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static bool IsSortedByAsc(this int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                    return false;
            }

            return true;
        }

        public static bool IsSortedByDesc(this int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] < array[i])
                    return false;
            }

            return true;
        }
    }
}