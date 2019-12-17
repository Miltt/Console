using Cnsl.Algorithms.Miscellaneous;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Miscellaneous
{
    [TestClass]
    public class LuckyTicketsTests
    {
        [TestMethod]
        public void NaiveTest()
        {
            var result = LuckyTickets.Naive();

            const int expectedResult = 55252;

            Assert.IsTrue(result == expectedResult, "Naive method has an incorrect result");
        }

        [TestMethod]
        public void CombinatoricsTest()
        {
            var result = LuckyTickets.Combinatorics();

            const int expectedResult = 55252;

            Assert.IsTrue(result == expectedResult, "Combinatorics method has an incorrect result");
        }
    }
}