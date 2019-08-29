using System;

namespace DynArray
{
    public class DynamicArray<T>
    {
        private const int DefaultSize = 0;

        private T[] _array;

        public int Length => _array.Length;
        public int Count { get; private set; }

        public DynamicArray() 
            : this(DefaultSize) { }

        public DynamicArray(int size)
        {
            if (size < 0)
                throw new ArgumentException("Must be at least 0", nameof(size));

            _array = new T[size];
        }

        public int IndexOf(T value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_array[i].Equals(value))
                    return i;
            }

            return -1;
        }

        public T this[int index]
        {
            get
            {
                ThrowIfInvalidIndex(index);
                return _array[index];                
            }

            set
            {
                ThrowIfInvalidIndex(index);
                _array[index] = value;
            }
        }

        public void Add(int index, T value)
        {
            ThrowIfInvalidIndex(index);

            if (_array.Length == Count)
                Grow();
                        
            Array.Copy(_array, index, _array, index + 1, Count - index); // shift all items following index to the right

            _array[index] = value;
            Count++;
        }

        public void RemoveAt(int index)
        {
            ThrowIfInvalidIndex(index);

            var shiftStart = index + 1;
            if (shiftStart < Count)
                Array.Copy(_array, shiftStart, _array, index, Count - shiftStart); // shift all items following index to the left

            Count--;
        }

        public void Add(T value)
        {
            if (_array.Length == Count)
                Grow();

            _array[Count++] = value;
        }

        public void Clear()
        {
            _array = new T[0];
            Count = 0;
        }

        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        public void CopyTo(T[] array, int index)
        {
            Array.Copy(_array, 0, array, index, Count);
        }

        public bool Remove(T value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_array[i].Equals(value))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        private void ThrowIfInvalidIndex(int index)
        {
            if (index < 0 || index >= _array.Length)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
        }

        private void Grow()
        {
            var newSize = _array.Length << 1;
            var newArray = new T[newSize];
            _array.CopyTo(newArray, 0);
            _array = newArray;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var array = new DynamicArray<int>(2);
            array.Add(2);
            array.Add(4);
            array.Add(3);
            array.Add(3, 7);
            array.Add(1, 5);
            array.Add(9);

            Console.WriteLine($"Value is: {array[3]}");

            if (array.Contains(4))
                Console.WriteLine($"Index of: {array.IndexOf(4)}");

            array.RemoveAt(2);
            array[3] = 18;

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
