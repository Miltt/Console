using System;

namespace Cnsl.Algorithms.Sorting
{
    public interface ISort
    {
        void Sort<T>(T[] array) 
            where T : IComparable<T>;
    }
}