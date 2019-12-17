using System.Linq;
using Cnsl.Algorithms.LinearProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.LinearProgramming
{
    [TestClass]
    public class SimplexAlgorithmTests
    {
        [TestMethod]
        public void MaximizeTest()
        {
            // f(x) = 3x1+x2 -> max
            var funcCoefficients = new double[] { 3, 1 };
            
            //  1x1 - 2x2 <= 1
            // -2x1 + 1x2 <= 2
            //  2x1 + 1x2 <= 6
            var bounds = new double[,] { { 1, -2, 1 }, { -2, 1, 2 }, { 2, 1, 6} };

            var result = new SimplexAlgorithm().Maximize(funcCoefficients, bounds);
            var solution = result.GetSolution().ToArray();

            const double x1 = 2.6;
            const double x2 = 0.8;

            Assert.IsTrue(solution[0] == x1 && solution[1] == x2, "Incorrect answer");
        }
    }
}