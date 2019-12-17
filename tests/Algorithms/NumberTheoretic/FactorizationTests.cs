using System.Linq;
using Cnsl.Algorithms.NumberTheoretic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.NumberTheoretic
{
    [TestClass]
    public class FactorizationTests
    {
        [TestMethod]
        public void TrialDivisionTest()
        {
            const long value = 7399;

            var result = Factorization.TrialDivision(value);

            var expectedResult = new long[] { 7, 7, 151 };
            var isEqual = result.SequenceEqual(expectedResult);

            Assert.IsTrue(isEqual, "Trial Division has an incorrect result");
        }

        [TestMethod]
        public void FermatFactorTest()
        {
            const long value = 10873;

            var result = Factorization.FermatFactor(value);

            const long expectedX = 131;
            const long expectedY = 83;

            Assert.IsTrue(result.X == expectedX && result.Y == expectedY, "Fermat Factor has an incorrect result");
        }
    }
}