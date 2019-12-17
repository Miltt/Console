using Cnsl.Algorithms.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Searching
{
    [TestClass]
    public class KnuthMorrisPrattTests
    {
        [TestMethod]
        public void SearchTest()
        {
            const string text = "abcdabcabcdabcdab";
            const string pattern = "abca";

            var result = KnuthMorrisPratt.Search(text, pattern);

            const int expectedResult = 4;

            Assert.IsTrue(result == expectedResult, "Found an incorrect result");
        }
    }
}