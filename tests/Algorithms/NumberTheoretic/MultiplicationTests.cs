using Cnsl.Algorithms.NumberTheoretic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.NumberTheoretic
{
    [TestClass]
    public class MultiplicationTests
    {
        [TestMethod]
        public void NaiveTest()
        {
            const long a = 2345678925;
            const long b = 3448261605;

            var result = Multiplication.Naive(a, b);

            const long expectedResult = 8088514574735174625;

            Assert.IsTrue(result == expectedResult, "Naive Multiplication has an incorrect result");
        }

        [TestMethod]
        public void BitwiseTest()
        {
            const long a = 2345678925;
            const long b = 3448261605;

            var result = Multiplication.Bitwise(a, b);

            const long expectedResult = 8088514574735174625;

            Assert.IsTrue(result == expectedResult, "Bitwise Multiplication has an incorrect result");
        }

        [TestMethod]
        public void KaratsubaTest()
        {
            const long a = 2345678925;
            const long b = 3448261605;

            var result = Multiplication.Karatsuba(a, b);

            const long expectedResult = 8088514574735174625;

            Assert.IsTrue(result == expectedResult, "Karatsuba Multiplication has an incorrect result");
        }
    }
}