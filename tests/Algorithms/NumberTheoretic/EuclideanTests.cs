using Cnsl.Algorithms.NumberTheoretic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.NumberTheoretic
{
    [TestClass]
    public class EuclideanTests
    {
        [TestMethod]
        public void NaiveTest()
        {
            const int a = 816;
            const int b = 2260;

            var result = Euclidean.Naive(a, b);

            const long expectedResult = 4;

            Assert.IsTrue(result == expectedResult, "Euclidean algorithm has incorrect result");
        }

        [TestMethod]
        public void ExtendedTest()
        {
            const int a = 816;
            const int b = 2260;

            var result = Euclidean.Extended(a, b);

            const long expectedResult = 4;

            Assert.IsTrue(result.D == expectedResult, "Euclidean algorithm has incorrect result");
        }
    }
}