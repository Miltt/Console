using System;
using System.Text;

namespace Matrix
{
    public class Matrix
    {
        private const int RowDim = 0;
        private const int ColumnDim = 1;

        private int[,] _matrix;

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Matrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new IndexOutOfRangeException();

            Rows = rows;
            Columns = columns;
            _matrix = new int[rows, columns];
        }

        public Matrix(int[,] matrix)
        {
            Rows = matrix.GetLength(RowDim);
            Columns = matrix.GetLength(ColumnDim);
            _matrix = (int[,])matrix.Clone();
        }

        public bool IsEqual(Matrix matrix)
        {            
            if (Rows != matrix.Rows || Columns != matrix.Columns)
                return false;
            
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (_matrix[i, j] != matrix[i, j])                    
                        return false;
                }
            }

            return true;            
        }

        public void Transposition()
        {
            var tmpMatrix = new int[Columns, Rows];
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                    tmpMatrix[j, i] = _matrix[i, j];
            }

            Assign(tmpMatrix);
        }

        public void Addition(Matrix matrix)
        {
            if (Rows != matrix.Rows || Columns != matrix.Columns)
                throw new Exception("Unequal sizes");

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    var value = this[i, j] + matrix[i, j];
                    this[i, j] = value;
                }
            }
        }

        public void Multiplication(Matrix matrix)
        {
            if (Columns != matrix.Rows)
                throw new ArgumentException("Invalid matrix size");

            var tmpMatrix = new int[Rows, matrix.Columns];
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    for (var k = 0; k < Columns; k++)
                        tmpMatrix[i, j] += this[i, k] * matrix[k, j];
                }
            }

            Assign(tmpMatrix);
        }

        public int this[int i, int j]
        {            
            get
            {
                if (i < 0 || i >= Rows || j < 0 || j >= Columns)
                    throw new IndexOutOfRangeException();

                return _matrix[i, j];
            }
            
            set
            {
                if (i < 0 || i >= Rows || j < 0 || j >= Columns)
                    throw new IndexOutOfRangeException();

                _matrix[i, j] = value;
            }
        }

        private void Assign(int[,] matrix)
        {
            _matrix = matrix;
            Rows = matrix.GetLength(RowDim);
            Columns = matrix.GetLength(ColumnDim);
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
            Console.WriteLine(a.IsEqual(c));
            a.Output();

            c.Transposition();
            c.Output();            
            
            a.Addition(c);
            a.Output();
            
            a.Multiplication(new Matrix(new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } }));
            a.Output();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public static class Extensions
    {
        public static void Output(this Matrix matrix)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                    sb.Append($"{matrix[i, j]} ");

                sb.AppendLine();
            }

            Console.WriteLine(sb);
        }
    }
}