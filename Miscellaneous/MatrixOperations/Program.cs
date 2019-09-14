using System;
using System.Text;

namespace Mtrx
{
    public struct Matrix : IEquatable<Matrix>
    {
        private int[,] _array;

        public int CountRows { get; private set; }
        public int CountColumns { get; private set; }

        public Matrix(int countRows, int countColumns)
        {
            if (countRows <= 0)
                throw new ArgumentException("Must be at less 1", nameof(countRows)); 
            if (countColumns <= 0)
                throw new ArgumentException("Must be at less 1", nameof(countColumns));

            CountRows = countRows;
            CountColumns = countColumns;
            _array = new int[countRows, countColumns];
        }

        public Matrix(int[,] matrix)
        {
            CountRows = matrix.GetLength(0);
            CountColumns = matrix.GetLength(1);
            _array = new int[CountRows, CountColumns];

            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                    _array[i, j] = matrix[i, j];
            }
        }

        public bool Equals(Matrix matrix)
        {            
            if (CountRows != matrix.CountRows || CountColumns != matrix.CountColumns)
                return false;
            
            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                {
                    if (_array[i, j] != matrix[i, j])                    
                        return false;
                }
            }

            return true;            
        }

        public void Transposition()
        {
            var array = new int[CountColumns, CountRows];
            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                    array[j, i] = _array[i, j];
            }

            Reassign(array, CountColumns, CountRows);
        }

        public void Addition(Matrix matrix)
        {
            if (CountRows != matrix.CountRows)
                throw new ArgumentException("The number of rows are not equals");
            if (CountColumns != matrix.CountColumns)
                throw new ArgumentException("The number of columns are not equals");

            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                {
                    var value = _array[i, j] + matrix[i, j];
                    _array[i, j] = value;
                }
            }
        }

        public void Multiplication(Matrix matrix)
        {
            if (CountColumns != matrix.CountRows)
                throw new ArgumentException("The number of columns must be equals to the number of rows");

            var array = new int[CountRows, matrix.CountColumns];
            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < matrix.CountColumns; j++)
                {
                    for (int k = 0; k < CountColumns; k++)
                    {
                        var value = _array[i, k] * matrix[k, j];
                        array[i, j] += value;
                    }
                }
            }

            Reassign(array, CountRows, matrix.CountColumns);
        }

        public int this[int i, int j]
        {            
            get
            {
                ThrowIfIndexInvalid(i, j);
                return _array[i, j];
            }
            
            set
            {
                ThrowIfIndexInvalid(i, j);
                _array[i, j] = value;
            }
        }

        private void Reassign(int[,] array, int countRows, int countColumns)
        {
            CountRows = countRows;
            CountColumns = countColumns;
            _array = array;
        }

        private void ThrowIfIndexInvalid(int i, int j)
        {
            if (i < 0 || i >= CountRows)
                throw new IndexOutOfRangeException($"The index '{nameof(i)}' is outside the matrix");
            if (j < 0 || j >= CountColumns)
                throw new IndexOutOfRangeException($"The index '{nameof(j)}' is outside the matrix");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new Matrix(2, 3);
            a[0, 0] = 1;
            a[0, 1] = 2;
            a[0, 2] = 3;
            a[1, 0] = 4;
            a[1, 1] = 5;
            a[1, 2] = 6;
            
            var c = new Matrix(new int[,]{ { 1, 2 }, { 3, 4 }, { 5, 7 } });
            Console.WriteLine(a.Equals(c));
            Show(a);

            c.Transposition();
            Show(c);          
            
            a.Addition(c);
            Show(a);
            
            a.Multiplication(new Matrix(new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } }));
            Show(a);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void Show(Matrix matrix)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < matrix.CountRows; i++)
            {
                for (var j = 0; j < matrix.CountColumns; j++)
                    sb.Append($"{matrix[i, j]} ");

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }
}