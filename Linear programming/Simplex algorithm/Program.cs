using System;
using System.Text;

namespace LinearProgramming
{
    public struct Result
    {
        public double[,] Table;
        public bool[] BasicSolution;

        public Result(double[,] table, bool[] basicSolution)
        {
            Table = table;
            BasicSolution = basicSolution;
        }

        public string GetSolution()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < BasicSolution.Length; i++)
            {
                if (BasicSolution[i])
                    sb.Append($"{Table[i, Table.GetUpperBound(1)]} ");
            }

            return sb.ToString();
        } 
    }

    public sealed class Simplex
    {
        private struct Pivot
        {
            public double Value;
            public int Row;
            public int Column;
        }

        private const int RowDimension = 0;
        private const int ColumnDimensions = 1;

        public Result Maximize(double[] funcCoefficients, double[,] bounds)
        {
            var result = Init(funcCoefficients, bounds);

            while (!IsOptimalSolution(funcCoefficients, result.Table))
            {
                var pivot = GetPivot(funcCoefficients, result);

                // create new table and calc all values except the pivot row and column
                var tempTable = new double[result.Table.GetLength(RowDimension), result.Table.GetLength(ColumnDimensions)];
                for (int i = 0; i < tempTable.GetLength(RowDimension); i++)
                {
                    for (int j = 0; j < tempTable.GetLength(ColumnDimensions); j++)
                        tempTable[i, j] = (pivot.Value * result.Table[i, j] - result.Table[pivot.Row, j] * result.Table[i, pivot.Column]) / pivot.Value;
                }

                // calc of the remaining values of the pivot element
                for (int i = 0; i < result.Table.GetLength(ColumnDimensions); i++)
                    tempTable[pivot.Row, i] = result.Table[pivot.Row, i] / pivot.Value;
                for (int i = 0; i < result.Table.GetLength(RowDimension); i++)
                    tempTable[i, pivot.Column] = (result.Table[i, pivot.Column] / pivot.Value) * -1;

                tempTable[pivot.Row, pivot.Column] = 1 / pivot.Value;
                result.Table = tempTable;
            }

            return result;
        }

        private Result Init(double[] funcCoefficients, double[,] bounds)
        {
            var table = new double[bounds.GetLength(RowDimension) + 1, funcCoefficients.Length + 1];

            for (int i = 0; i < bounds.GetLength(RowDimension); i++)
            {
                for (int j = 0; j < bounds.GetLength(ColumnDimensions); j++)
                    table[i, j] = bounds[i, j];
            }

            for (int i = 0; i < funcCoefficients.Length; i++)
                table[table.GetUpperBound(RowDimension), i] = funcCoefficients[i] * -1;

            return new Result(table, new bool[bounds.GetLength(RowDimension) + 1]);
        }

        private bool IsOptimalSolution(double[] funcCoefficients, double[,] table)
        {
            for (int i = 0; i < funcCoefficients.Length; i++)
            {
                if (table[table.GetUpperBound(RowDimension), i] < 0)
                    return false;
            }

            return true;
        }

        private Pivot GetPivot(double[] funcCoefficients, Result result)
        {
            var pivot = new Pivot();

            var min = double.MaxValue;
            for (int i = 0; i < funcCoefficients.Length; i++)
            {
                if (min > result.Table[result.Table.GetUpperBound(RowDimension), i])
                {
                    min = result.Table[result.Table.GetUpperBound(RowDimension), i];
                    pivot.Column = i;
                }
            }

            min = double.MaxValue;
            for (int i = 0; i < result.Table.GetLength(RowDimension); i++)
            {
                if (result.Table[i, pivot.Column] > 0)
                {
                    var temp = result.Table[i, result.Table.GetUpperBound(ColumnDimensions)] / result.Table[i, pivot.Column];
                    if (min > temp)
                    {
                        min = temp;
                        pivot.Row = i;
                    }
                }
            }

            if (pivot.Row == int.MinValue || pivot.Column == int.MinValue)
                throw new InvalidOperationException("There is no solution");

            pivot.Value = result.Table[pivot.Row, pivot.Column];
            result.BasicSolution[pivot.Row] = true; 

            return pivot;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // f(x)=3x1+x2 -> max
            var funcCoefficients = new double[] { 3, 1 };
            
            // canonical:
            // x1 - 2x2 + u1 = 1
            // -2x1 + x2 + u2 = 2
            // 2x1 + x2 + u3 = 6
            var bounds = new double[,] { { 1, -2, 1 }, { -2, 1, 2 }, { 2, 1, 6} };

            var result = new Simplex().Maximize(funcCoefficients, bounds);
            Console.WriteLine(result.GetSolution());

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}