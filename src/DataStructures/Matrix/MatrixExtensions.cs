using System;

namespace Cnsl.DataStructures
{
    public static class MatrixExtensions
    {
        public static void Addition(this IMatrix matrix, IMatrix other)
        {
            if (other is null)            
                throw new ArgumentNullException(nameof(other));
            if (matrix.RowsCount != other.RowsCount)
                throw new ArgumentException("The number of rows of two matrices does not match");
            if (matrix.ColumnsCount != other.ColumnsCount)
                throw new ArgumentException("The number of columns of two matrices does not match");

            for (int i = 0; i < matrix.RowsCount; i++)
            {
                for (int j = 0; j < matrix.ColumnsCount; j++)
                {
                    var value = matrix[i, j] + other[i, j];
                    matrix[i, j] = value;
                }
            }
        }
    }
}