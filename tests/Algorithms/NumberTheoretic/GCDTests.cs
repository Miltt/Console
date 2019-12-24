using Cnsl.Algorithms.NumberTheoretic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.NumberTheoretic
{
    [TestClass]
    public class GCDTests
    {
        [TestMethod]
        public void NaiveTest()
        {
            const int a = 816;
            const int b = 2260;

            var result = GCD.EuclidNaive(a, b);

            const long expectedResult = 4;

            Assert.IsTrue(result == expectedResult, "Euclidean naive algorithm has incorrect result");
        }

        [TestMethod]
        public void EuclidExtendedTest()
        {
            const int a = 816;
            const int b = 2260;

            var result = GCD.EuclidExtended(a, b);

            const long expectedResult = 4;

            Assert.IsTrue(result.D == expectedResult, "Euclidean extended algorithm has incorrect result");
        }
    }
}