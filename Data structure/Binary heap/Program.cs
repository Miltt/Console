using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryHeap
{
    public class BinaryHeap
    {
        private List<int> _items;

        public int HeapSize => _items.Count;

        public BinaryHeap(int[] array)
        {
            _items = array.ToList();
            for (var i = HeapSize / 2; i >= 0; i--)
                Heapify(i);
        }

        public void Add(int value)
        {
            _items.Add(value);

            var current = HeapSize - 1;
            var parent = (current - 1) / 2;

            while (current > 0 && _items[parent] < _items[current])
            {
                Swap(current, parent);

                current = parent;
                parent = (current - 1) / 2;
            }
        }

        public void Heapify(int i)
        {
            int childLeft, childRight, childMax;

            var complete = false;
            while (2 * i + 1 < HeapSize && !complete)
            {
                childLeft = 2 * i + 1;
                childRight = 2 * i + 2;
                childMax = i;

                if (childLeft < HeapSize && _items[childLeft] > _items[childMax])
                    childMax = childLeft;
                if (childRight < HeapSize && _items[childRight] > _items[childMax])
                    childMax = childRight;

                if (_items[i] < _items[childMax])
                {
                    Swap(i, childMax);
                    i = childMax;
                }
                else
                    complete = true;
            }
        }

        public int GetMax()
        {
            int result = _items[0];

            _items[0] = _items[HeapSize - 1];
            _items.RemoveAt(HeapSize - 1);

            return result;
        }

        private void Swap(int i, int j)
        {
            int temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var array = new int[] { 1, 8, 22, 7, 6, 2, 0, 5, 3, 4 };
            var heap = new BinaryHeap(array);

            for (var i = array.Length - 1; i >= 0; i--)
            {
                Console.Write($"{heap.GetMax()} ");
                heap.Heapify(0);
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
