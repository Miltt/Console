using Cnsl.Algorithms.DynamicProgramming;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.DynamicProgramming
{
    [TestClass]
    public class FibonacciNumberTests
    {
        [TestMethod]
        public void CalculationNaiveTest()
        {
            var n = 9;
            var result = FibonacciNumber.Naive(n);

            const int expectedResult = 34;

            Assert.IsTrue(result == expectedResult, "The result is incorrect");
        }

        [TestMethod]
        public void CalculationMemoizationTest()
        {
            var n = 93;
            var result = FibonacciNumber.Memoization(n);
            
            const ulong expectedResult = 12200160415121876738;

            Assert.IsTrue(result == expectedResult, "The result is incorrect");
        }
    }
}