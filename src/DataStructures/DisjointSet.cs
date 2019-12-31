using System;

namespace Cnsl.DataStructures
{
    public class DisjointSet
    {
        private class Item
        {
            public Item Parent { get; set; }
            public int Rank { get; set; }
            public int Value { get; }

            public Item(int value)
            {
                Value = value;
            }
        }

        public const int Unknown = -1;

        private readonly Item[] _set;

        public DisjointSet(int size)
        {
            if (size < 0)
                throw new ArgumentException("Must be at least 0", nameof(size));

            _set = new Item[size];
            MakeSet();
        }

        public int Find(int item)
            => item >= 0 && item < _set.Length
                ? Find(_set[item]).Value
                : Unknown;

        public void Union(int itemX, int itemY)
            => Union(_set[itemX], _set[itemY]);

        private Item Find(Item x)
        {
            if (x is null)
                throw new ArgumentNullException(nameof(x));
          
            return x.Parent != null && x.Parent != x 
                ? x.Parent = Find(x.Parent) 
                : x;
        }

        private void Union(Item x, Item y)
        {
            var parentX = Find(x);
            var parentY = Find(y);
            
            if (parentX == parentY) 
                return;

            if (parentX.Rank < parentY.Rank)
            {
                parentX.Parent = parentY;
            }
            else
            {
                parentY.Parent = parentX;
                
                if (parentX.Rank == parentY.Rank)
                    parentX.Rank++;
            }
        }

        private void MakeSet()
        {
            for (int i = 0; i < _set.Length; i++)
                _set[i] = new Item(i);
        }
    }
} 