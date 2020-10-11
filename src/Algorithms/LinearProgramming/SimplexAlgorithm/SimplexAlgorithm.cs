using System;
using System.Collections.Generic;

namespace Cnsl.Algorithms.LinearProgramming
{
    public class SimplexAlgorithm
    {
        public class Result
        {
            public double[,] Table { get; set; }
            public bool[] BasicSolution { get; }

            public Result(double[,] table, bool[] basicSolution)
            {
                Table = table ?? throw new ArgumentNullException(nameof(table));
                BasicSolution = basicSolution ?? throw new ArgumentNullException(nameof(basicSolution));
            }

            public IEnumerable<double> GetSolution()
            {
                var j = Table.GetUpperBound(1);

                for (int i = 0; i < BasicSolution.Length; i++)
                {
                    if (BasicSolution[i])
                        yield return Table[i, j];
                }
            } 
        }

        private struct Pivot
        {
            public double Value { get; internal set; }
            public int Row { get; set; }
            public int Column { get; set; }

            public Pivot(double value, int row, int column)
            {
                Value = value;
                Row = row;
                Column = column;
            }
        }

        private const int RowDimension = 0;
        private const int ColumnDimensions = 1;

        public Result Maximize(double[] funcCoefficients, double[,] bounds)
        {
            if (funcCoefficients is null)
                throw new ArgumentNullException(nameof(funcCoefficients));
            if (bounds is null)
                throw new ArgumentNullException(nameof(bounds));

            var result = Init(funcCoefficients, bounds);

            while (!IsOptimalSolution(funcCoefficients, result.Table))
            {
                var pivot = GetPivot(funcCoefficients, result);

                // create new table and calc all values except the pivot row and column
                var table = new double[result.Table.GetLength(RowDimension), result.Table.GetLength(ColumnDimensions)];
                for (int i = 0; i < table.GetLength(RowDimension); i++)
                {
                    for (int j = 0; j < table.GetLength(ColumnDimensions); j++)
                        table[i, j] = (pivot.Value * result.Table[i, j] - result.Table[pivot.Row, j] * result.Table[i, pivot.Column]) / pivot.Value;
                }

                // calc of the remaining values of the pivot element
                for (int i = 0; i < result.Table.GetLength(ColumnDimensions); i++)
                    table[pivot.Row, i] = result.Table[pivot.Row, i] / pivot.Value;
                for (int i = 0; i < result.Table.GetLength(RowDimension); i++)
                    table[i, pivot.Column] = (result.Table[i, pivot.Column] / pivot.Value) * -1;

                table[pivot.Row, pivot.Column] = 1 / pivot.Value;
                result.Table = table;
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
}