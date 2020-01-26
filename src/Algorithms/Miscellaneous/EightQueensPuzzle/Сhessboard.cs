using System;

namespace Cnsl.Algorithms.Miscellaneous
{
    public readonly struct Сhessboard
    {
        private readonly bool[,] _grid;
        public int Size { get; }

        public Сhessboard(int size)
        {
            if (size < 1)
                throw new ArgumentException("Must be at least 1", nameof(size));

            _grid = new bool[size, size];
            Size = size;
        }

        public bool this[int i, int j]
        {
            get => _grid[i, j];
            set => _grid[i, j] = value;
        }
    }
}