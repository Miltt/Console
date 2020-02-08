using System;

namespace Cnsl.DataStructures
{
    public class Matrix : IEquatable<Matrix>
    {
        private readonly int[,] _matrix;

        public int RowsCount { get; }
        public int ColumnsCount { get; }

        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1)
                throw new ArgumentException("Must be at least 1", nameof(rowsCount)); 
            if (columnsCount < 1)
                throw new ArgumentException("Must be at least 1", nameof(columnsCount));

            RowsCount = rowsCount;
            ColumnsCount = columnsCount;

            _matrix = new int[RowsCount, ColumnsCount];
        }

        public Matrix(int[,] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            RowsCount = array.GetLength(0);
            ColumnsCount = array.GetLength(1);

            _matrix = new int[RowsCount, ColumnsCount];

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                    _matrix[i, j] = array[i, j];
            }
        }

        /// <summary>
        /// Adjacency matrix
        /// </summary>
        public Matrix(IGraph graph)
        {
            if (graph is null)
                throw new ArgumentNullException(nameof(graph));
            if (graph.Count < 1)
                throw new ArgumentException("Must be at least 1", nameof(graph.Count));

            RowsCount = graph.Count;
            ColumnsCount = graph.Count;

            _matrix = new int[RowsCount, ColumnsCount];

            for (int i = 0; i < RowsCount; i++)
            {
                var v = graph[i];
                for (int j = 0; j < ColumnsCount; j++)
                {
                    var u = graph[j];
                    if (v.TryGetEdge(u, out var edge))
                        _matrix[i, j] = edge.Weight;
                }
            }
        }

        public int this[int i, int j]
        {            
            get
            {
                ThrowIfIndexOutOfRange(i, j);
                return _matrix[i, j];
            }
            
            set
            {
                ThrowIfIndexOutOfRange(i, j);
                _matrix[i, j] = value;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Matrix);
        }

        public bool Equals(Matrix other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            if (this.GetType() != other.GetType())
                return false;
            if (RowsCount != other.RowsCount || ColumnsCount != other.ColumnsCount)
                return false;

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    if (_matrix[i, j] != other[i, j])                    
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;
                hashCode = (hashCode * 37) ^ _matrix.GetHashCode();
                hashCode = (hashCode * 37) ^ RowsCount.GetHashCode();
                hashCode = (hashCode * 37) ^ ColumnsCount.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"Matrix:{RowsCount}x{ColumnsCount}";
        }

        private void ThrowIfIndexOutOfRange(int i, int j)
        {
            if (i < 0 || i >= RowsCount)
                throw new IndexOutOfRangeException("Index 'i' outside the range of the matrix");
            if (j < 0 || j >= ColumnsCount)
                throw new IndexOutOfRangeException("Index 'j' outside the range of the matrix");
        }
    }
}