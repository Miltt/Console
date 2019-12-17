using Cnsl.Algorithms.NumberTheoretic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.NumberTheoretic
{
    [TestClass]
    public class ExponentiationTests
    {
        [TestMethod]
        public void ModExpTest()
        {
            const long value = 4;
            const long power = 13;
            const long mod = 497;

            var result = Exponentiation.ModExp(value, power, mod);

            const long expectedResult = 445;

            Assert.IsTrue(result == expectedResult, "Modular exponentiation has an incorrect result");
        }

        [TestMethod]
        public void BinExpTest()
        {
            const long value = 21;
            const long power = 13;

            var result = Exponentiation.BinExp(value, power);

            const long expectedResult = 154472377739119461;

            Assert.IsTrue(result == expectedResult, "Binary exponentiation has an incorrect result");
        }
    }
}