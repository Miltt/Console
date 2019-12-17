using System;
using System.Collections;
using System.Collections.Generic;
using Cnsl.Common.Extensions;

namespace Cnsl.DataStructures
{
    public class BinaryMaxHeap : IEnumerable<int>
    {
        private int[] _items;

        public int Size => _items.Length;

        public BinaryMaxHeap(int[] array)
        {
            _items = array ?? throw new ArgumentNullException(nameof(array));
            
            for (int i = _items.Length / 2; i >= 0; i--)
                Heapify(i);
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
                    _items.Swap(i, childMax);
                    i = childMax;
                }
                else
                {
                    break;                        
                }
            }
        }
    }
}