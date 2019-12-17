using System;
using System.Collections.Generic;

namespace Cnsl.Algorithms.LinearProgramming
{
    public class SimplexAlgorithmResult
    {
        public double[,] Table { get; set; }
        public bool[] BasicSolution { get; }

        public SimplexAlgorithmResult(double[,] table, bool[] basicSolution)
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
}