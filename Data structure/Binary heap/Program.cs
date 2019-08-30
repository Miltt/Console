using System;
using System.Collections.Generic;
using System.Linq;

namespace Heap
{
    public sealed class BinaryMaxHeap : IEnumerable<int>
    {
        private int[] _items;

        public int Size => _items.Length;

        public BinaryMaxHeap(int[] array)
        {
            _items = array ?? throw new ArgumentNullException(nameof(array));
            
            for (int i = _items.Length / 2; i >= 0; i--)
                Heapify(i);
        }

        public void Add(int value)
        {
            var currentIndex = _items.Length - 1;
            var parentIndex = GetParentIndex(currentIndex);
            
            _items[currentIndex] = value;

            while (currentIndex > 0 && _items[parentIndex] < _items[currentIndex])
            {
                Swap(currentIndex, parentIndex);

                currentIndex = parentIndex;
                parentIndex = GetParentIndex(currentIndex);
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = _items.Length - 1; i >= 0; i--)
                yield return GetMax();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int GetMax()
        {
            var result = _items[0];
            _items[0] = _items[_items.Length - 1];

            for (int a = _items.Length - 1; a < _items.Length - 1; a++)
                _items[a] = _items[a + 1];
            
            Array.Resize(ref _items, _items.Length - 1);
            Heapify(0);

            return result;
        }

        private void Swap(int i, int j)
        {
            var temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }

        private int GetParentIndex(int index)
            => (index - 1) / 2;

        private void Heapify(int i)
        {
            int childLeft, childRight, childMax;

            while (2 * i + 1 < _items.Length)
            {
                childLeft = 2 * i + 1;
                childRight = 2 * i + 2;
                childMax = i;

                if (childLeft < _items.Length && _items[childLeft] > _items[childMax])
                    childMax = childLeft;
                if (childRight < _items.Length && _items[childRight] > _items[childMax])
                    childMax = childRight;

                if (_items[i] < _items[childMax])
                {
                    Swap(i, childMax);
                    i = childMax;
                }
                else
                {
                    break;                        
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var array = new int[] { 1, 8, 22, 7, 6, 2, 0, 5, 3, 4 };
            var heap = new BinaryMaxHeap(array);

            foreach (var item in heap)
                Console.Write($"{item} ");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
