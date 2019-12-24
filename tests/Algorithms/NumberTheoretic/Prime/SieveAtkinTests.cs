using System.Linq;
using Cnsl.Algorithms.NumberTheoretic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.NumberTheoretic
{
    [TestClass]
    public class SieveAtkinTests
    {
        [TestMethod]
        public void PrimeTestFirst10000000ItemsTest()
        {
            var result = SieveAtkin.GetPrimeNumbers(limit: 10000000).ToArray();

            var valid = true;
            for (int i = 0; i < result.Length; i++)
            {
                if (!PrimalityTest.Fermat(result[i]))
                {
                    valid = false;
                    break;
                }
            }

            Assert.IsTrue(valid, "Not prime found");
        }
    }
}