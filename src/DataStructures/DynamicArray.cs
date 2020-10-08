using System;
using System.Collections;
using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public class DynamicArray<T> : IReadOnlyCollection<T>
        where T : IEquatable<T>
    {
        public static readonly DynamicArray<T> Empty = new DynamicArray<T>();

        private const int DefaultSize = 0;

        private T[] _array;
        private int _itemsCount;

        public int Length => _itemsCount;
        int IReadOnlyCollection<T>.Count => Length;

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
            for (int i = 0; i < _itemsCount; i++)
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

            if (_array.Length == _itemsCount)
                Grow();
                        
            Array.Copy(_array, index, _array, index + 1, _itemsCount - index); // shift all items following index to the right

            _array[index] = value;
            _itemsCount++;
        }

        public void RemoveAt(int index)
        {
            ThrowIfInvalidIndex(index);

            var shiftStart = index + 1;
            if (shiftStart < _itemsCount)
                Array.Copy(_array, shiftStart, _array, index, _itemsCount - shiftStart); // shift all items following index to the left

            _itemsCount--;
        }

        public void Add(T value)
        {
            if (_array.Length == _itemsCount)
                Grow();

            _array[_itemsCount++] = value;
        }

        public void Clear()
        {
            _array = new T[DefaultSize];
            _itemsCount = 0;
        }

        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        public void CopyTo(T[] array, int index)
        {
            Array.Copy(_array, 0, array, index, _itemsCount);
        }

        public bool Remove(T value)
        {
            for (int i = 0; i < _itemsCount; i++)
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
            var newSize = _array.Length > 0 ? _array.Length << 1 : 1;
            var newArray = new T[newSize];
            _array.CopyTo(newArray, 0);
            _array = newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _itemsCount; i++)
                yield return _array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}