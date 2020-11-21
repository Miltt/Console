using System;

namespace Cnsl.Algorithms.Sorting
{
    public abstract class BaseSorter : ISort
    {
        public abstract void Sort<T>(T[] array)
            where T : IComparable<T>;
        
        public abstract void SortByDescending<T>(T[] array)
            where T : IComparable<T>;

        protected int Compare<T>(T x, T y, bool isDescending)
            where T : IComparable<T>
        {
            var result = x.CompareTo(y);
            if (result == 0)
                return 0;

            return isDescending ? -result : result;
        }
    }
}