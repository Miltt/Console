using Cnsl.Algorithms.NumberTheoretic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.NumberTheoretic
{
    [TestClass]
    public class PrimalityTestTests
    {
        [TestMethod]
        public void NaiveTest()
        {
            const long value = 16769023;

            var isPrime = PrimalityTest.Naive(value);

            Assert.IsTrue(isPrime, "Naive Primality Test has an incorrect result");
        }

        [TestMethod]
        public void FermatTest()
        {
            const long value = 16769023;

            var isPrime = PrimalityTest.Fermat(value);

            Assert.IsTrue(isPrime, "Fermat Primality Test has an incorrect result");
        }
    }
}