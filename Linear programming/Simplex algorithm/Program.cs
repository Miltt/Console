using System;
using System.Text;

namespace LinearProgramming
{
    public sealed class Simplex
    {
        private const int RowsDim = 0;
        private const int ColumnsDim = 1;

        private double[] _funcCoefficients;
        private double[,] _bounds;
        private double[,] _table;
        private bool[] _basicSolution;
        private IPerformer _performer;

        public Simplex(double[] funcCoefficients, double[,] bounds, IPerformer performer)
        {
            _funcCoefficients = funcCoefficients;
            _bounds = bounds;
            _performer = performer;
        }

        public void Maximize()
        {
            Init();

            while (!IsOptimalSolution())
            {
                double pivotValue;
                int pivotRow;
                int pivotColumn;
                PreparePivot(out pivotValue, out pivotRow, out pivotColumn);

                // create new table and calc all values except the pivot row and column
                var tempTable = new double[_table.GetLength(RowsDim), _table.GetLength(ColumnsDim)];
                for (int i = 0; i < tempTable.GetLength(RowsDim); i++)
                {
                    for (int j = 0; j < tempTable.GetLength(ColumnsDim); j++)
                        tempTable[i, j] = (pivotValue * _table[i, j] - _table[pivotRow, j] * _table[i, pivotColumn]) / pivotValue;
                }

                // calc of the remaining values of the pivot element
                for (int i = 0; i < _table.GetLength(ColumnsDim); i++)
                    tempTable[pivotRow, i] = _table[pivotRow, i] / pivotValue;

                for (int i = 0; i < _table.GetLength(RowsDim); i++)
                    tempTable[i, pivotColumn] = (_table[i, pivotColumn] / pivotValue) * -1;

                tempTable[pivotRow, pivotColumn] = 1 / pivotValue;
                _table = tempTable;
            }

            _performer.Perform(_table, _basicSolution);
        }

        private void Init()
        {
            _table = new double[_bounds.GetLength(RowsDim) + 1, _funcCoefficients.Length + 1];
            _basicSolution = new bool[_bounds.GetLength(RowsDim) + 1];

            for (int i = 0; i < _bounds.GetLength(RowsDim); i++)
            {
                for (int j = 0; j < _bounds.GetLength(ColumnsDim); j++)
                    _table[i, j] = _bounds[i, j];
            }

            for (int i = 0; i < _funcCoefficients.Length; i++)
                _table[_table.GetUpperBound(RowsDim), i] = _funcCoefficients[i] * -1;
        }

        private bool IsOptimalSolution()
        {
            for (int i = 0; i < _funcCoefficients.Length; i++)
            {
                if (_table[_table.GetUpperBound(RowsDim), i] < 0)
                    return false;
            }

            return true;
        }

        private void PreparePivot(out double pivotValue, out int row, out int column)
        {
            column = int.MinValue;
            row = int.MinValue;

            var min = double.MaxValue;
            for (int i = 0; i < _funcCoefficients.Length; i++)
            {
                if (min > _table[_table.GetUpperBound(RowsDim), i])
                {
                    min = _table[_table.GetUpperBound(RowsDim), i];
                    column = i;
                }
            }

            min = double.MaxValue;
            for (int i = 0; i < _table.GetLength(RowsDim); i++)
            {
                if (_table[i, column] > 0)
                {
                    var temp = _table[i, _table.GetUpperBound(ColumnsDim)] / _table[i, column];
                    if (min > temp)
                    {
                        min = temp;
                        row = i;
                    }
                }
            }

            if (row == int.MinValue || column == int.MinValue)
                throw new Exception("There is no solution");

            pivotValue = _table[row, column];
            _basicSolution[row] = true; 
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

            var simplex = new Simplex(funcCoefficients, bounds, new Performer());
            simplex.Maximize();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public interface IPerformer
    {
        void Perform(double[,] _table, bool[] _basicSolution);
    }

    public class Performer : IPerformer
    {
        private StringBuilder _sb = new StringBuilder();

        public void Perform(double[,] _table, bool[] _basicSolution)
        {
            for (int i = 0; i < _basicSolution.Length; i++)
            {
                if (_basicSolution[i])
                    _sb.Append($"{_table[i, _table.GetUpperBound(1)]} ");
            }

            Console.WriteLine(_sb);
        }
    }
}