using System;

namespace Cnsl.DataStructures
{
    public class Matrix : IMatrix, IEquatable<IMatrix>
    {
        private int[,] _array;

        public int RowsCount { get; private set; }
        public int ColumnsCount { get; private set; }

        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1)
                throw new ArgumentException("Must be at least 1", nameof(rowsCount)); 
            if (columnsCount < 1)
                throw new ArgumentException("Must be at least 1", nameof(columnsCount));

            RowsCount = rowsCount;
            ColumnsCount = columnsCount;

            _array = new int[RowsCount, ColumnsCount];
        }

        public Matrix(int[,] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            RowsCount = array.GetLength(0);
            ColumnsCount = array.GetLength(1);

            _array = new int[RowsCount, ColumnsCount];

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                    _array[i, j] = array[i, j];
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

            _array = new int[RowsCount, ColumnsCount];

            for (int i = 0; i < RowsCount; i++)
            {
                var v = graph[i];
                for (int j = 0; j < ColumnsCount; j++)
                {
                    var u = graph[j];
                    if (v.TryGetEdge(u, out var edge))
                        _array[i, j] = edge.Weight;
                }
            }
        }

        public void Addition(IMatrix matrix)
        {
            if (matrix is null)            
                throw new ArgumentNullException(nameof(matrix));
            if (RowsCount != matrix.RowsCount)
                throw new ArgumentException("The number of rows of two matrices does not match");
            if (ColumnsCount != matrix.ColumnsCount)
                throw new ArgumentException("The number of columns of two matrices does not match");

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    var value = _array[i, j] + matrix[i, j];
                    _array[i, j] = value;
                }
            }
        }

        public void Transposition()
        {
            var array = new int[ColumnsCount, RowsCount];
            
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                    array[j, i] = _array[i, j];
            }

            Reassign(array, ColumnsCount, RowsCount);
        }

        public void Multiplication(IMatrix matrix)
        {
            if (matrix is null)            
                throw new ArgumentNullException(nameof(matrix));
            if (ColumnsCount != matrix.RowsCount)
                throw new ArgumentException("The number of columns must be equal to the number of rows");

            var array = new int[RowsCount, matrix.ColumnsCount];
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < ColumnsCount; k++)
                    {
                        var value = _array[i, k] * matrix[k, j];
                        array[i, j] += value;
                    }
                }
            }

            Reassign(array, RowsCount, matrix.ColumnsCount);
        }

        public int this[int i, int j]
        {            
            get
            {
                ThrowIfIndexOutOfRange(i, j);
                return _array[i, j];
            }
            
            set
            {
                ThrowIfIndexOutOfRange(i, j);
                _array[i, j] = value;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Matrix);
        }

        public bool Equals(IMatrix other)
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
                    if (_array[i, j] != other[i, j])                    
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
                hashCode = (hashCode * 37) ^ _array.GetHashCode();
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

        private void Reassign(int[,] matrix, int countRows, int countColumns)
        {
            RowsCount = countRows;
            ColumnsCount = countColumns;
            _array = matrix;
        }
    }
}