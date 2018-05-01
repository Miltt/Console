using System;

namespace DynamicArray
{
    public struct Item<T>
    {
        public T Value;       

        public Item(T value)
        {
            Value = value;
        }
    }

    public class DynamicArray<T>
    {
        private const int DefaultSize = 32;

        private Item<T>[] _array;

        public int Length => _array.Length;
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public DynamicArray() 
            : this(DefaultSize) { }

        public DynamicArray(int size)
        {
            if (size < 0)
                throw new ArgumentException(nameof(size));

            _array = new Item<T>[size];
        }

        private void GrowArray()
        {
            var newSize = Length << 1;
            var newArray = new Item<T>[newSize];
            _array.CopyTo(newArray, 0);
            _array = newArray;
        }

        public int IndexOf(T value)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_array[i].Value.Equals(value))
                    return i;
            }

            return -1;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Length)
                    throw new IndexOutOfRangeException(nameof(index));

                return _array[index].Value;                
            }

            set
            {
                if (index >= Length)
                    throw new IndexOutOfRangeException(nameof(index));

                _array[index].Value = value;
            }
        }

        public void Add(int index, T value)
        {
            if (index >= Length)
                throw new IndexOutOfRangeException(nameof(index));
            if (Length == Count)
                GrowArray();
                        
            Array.Copy(_array, index, _array, index + 1, Count - index); // shift all items following index to the right

            _array[index].Value = value;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index >= Length)
                throw new IndexOutOfRangeException(nameof(index));

            var shiftStart = index + 1;
            if (shiftStart < Count)
                Array.Copy(_array, shiftStart, _array, index, Count - shiftStart); // shift all items following index to the left

            Count--;
        }

        public void Add(T value)
        {
            if (Length == Count)
                GrowArray();

            _array[Count++].Value = value;
        }

        public void Clear()
        {
            _array = new Item<T>[0];
            Count = 0;
        }

        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        public void CopyTo(Item<T>[] array, int index)
        {
            Array.Copy(_array, 0, array, index, Count);
        }

        public bool Remove(T value)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_array[i].Value.Equals(value))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
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
            Console.WriteLine($"Is read only: {array.IsReadOnly}");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
